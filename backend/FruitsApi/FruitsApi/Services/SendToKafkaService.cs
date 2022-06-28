using Confluent.Kafka;

namespace FruitsApi.Services;

public class SendToKafkaService : ISendToKafkaService
{
    private readonly IConfiguration _configuration;

    public SendToKafkaService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task SendAsync(IFormFile file, CancellationToken cancellationToken)
    {
        using var memStream = new MemoryStream();
        await file.CopyToAsync(memStream, cancellationToken);
        var fileString = Convert.ToBase64String(memStream.ToArray());
        
        var config = new ProducerConfig
        {
            BootstrapServers = _configuration.GetValue<string>("KafkaBootstrapServers")
        };

        var producer = new ProducerBuilder<Null, string>(config).Build();
        await producer.ProduceAsync(_configuration.GetValue<string>("KafkaTopic"), 
            new Message<Null, string> { Value=fileString }, cancellationToken);
    }
}