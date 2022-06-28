using Confluent.Kafka;
using FruitRecognitionService.Models;
using FruitRecognitionService.Persistence;

namespace FruitRecognitionService.Services;

public class FruitRecognitionPipeline : IFruitRecognitionPipeline
{
    private readonly ILogger<FruitRecognitionPipeline> _logger;
    private readonly IConsumer<Ignore, string> _consumer;
    private readonly IFruitClassificationService _fruitClassificationService;
    private readonly IFruitRepository _fruitRepository;

    public FruitRecognitionPipeline(
        ILogger<FruitRecognitionPipeline> logger,
        IFruitClassificationService fruitClassificationService,
        IFruitRepository fruitRepository,
        IConfiguration configuration)
    {
        _logger = logger;
        _fruitClassificationService = fruitClassificationService;
        _fruitRepository = fruitRepository;

        var consumerConfig = new ConsumerConfig
        {
            GroupId = "mini-rem",
            EnableAutoCommit = false,
            BootstrapServers = configuration.GetValue<string>("KafkaBootstrapServers"),
            AutoOffsetReset = AutoOffsetReset.Earliest
        };
        var builder = new ConsumerBuilder<Ignore, string>(consumerConfig);

        var retry = true;
        while (retry)
        {
            retry = false;
            try
            {
                _consumer = builder.Build();
                _consumer.Subscribe(configuration.GetValue<string>("KafkaTopic"));
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Retry cause of {exception}", ex);
                Thread.Sleep(5000);
                retry = true;
            }
        }
    }


    public void Recognize(CancellationToken stoppingToken)
    {
        var retry = true;
        while (retry)
        {
            retry = false;
            try
            {
                var consumeResult = _consumer.Consume(stoppingToken);
                var fruit = GetFruit(consumeResult.Message.Value);
                ProcessFruit(fruit);
                _consumer.Commit();
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Retry cause of {exception}", ex);
                Thread.Sleep(5000);
                retry = true;
            }
        }
    }

    private void ProcessFruit(Fruit fruit)
    {
        _fruitClassificationService.ClassifyByAi(fruit);
        _fruitRepository.Insert(fruit);
    }

    private Fruit GetFruit(string message)
    {
        return new Fruit(Guid.NewGuid().ToString(), message, "", false);
    }
}