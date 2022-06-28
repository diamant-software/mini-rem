namespace FruitRecognitionService.Services;

public class ListenerService : BackgroundService
{
    private readonly ILogger<ListenerService> _logger;
    private readonly IServiceProvider _serviceProvider;

    public ListenerService(
        ILogger<ListenerService> logger,
        IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
    }

#pragma warning disable CS1998
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var recognitionService = _serviceProvider.GetService<IFruitRecognitionPipeline>();

        if (recognitionService != null)
        {
            // Reading on async execution
            // https://www.pluralsight.com/guides/using-task-run-async-await
            // https://stackoverflow.com/questions/9343594/how-to-call-asynchronous-method-from-synchronous-method-in-c

            _ = Task.Run(async () => Process(stoppingToken, recognitionService), stoppingToken);
        }
        else
        {
            _logger.LogError("Could not resolve RecognitionService");
        }
    }

    private void Process(CancellationToken stoppingToken, IFruitRecognitionPipeline recognitionService)
    {
        using var scope = _serviceProvider.CreateScope();

        while (!stoppingToken.IsCancellationRequested)
        {
            recognitionService.Recognize(stoppingToken);
        }
    }
#pragma warning restore CS1998

}

