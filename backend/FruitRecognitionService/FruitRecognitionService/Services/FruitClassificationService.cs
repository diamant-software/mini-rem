using FruitRecognitionService.Models;
using FruitRecognitionService.Persistence;

namespace FruitRecognitionService.Services;

public class FruitClassificationService : IFruitClassificationService
{
    private readonly ILogger<FruitClassificationService> _logger;
    private readonly IFruitRepository _fruitRepository;
    private readonly IFruitClassifier _fruitClassifier;
    private IConfiguration _configuration;
    private const int ScoreThresholdForClassification = 70;


    public FruitClassificationService(
        ILogger<FruitClassificationService> logger,
        IFruitRepository fruitRepository,
        IFruitClassifier fruitClassifier, IConfiguration configuration)
    {
        _logger = logger;
        _fruitRepository = fruitRepository;
        _fruitClassifier = fruitClassifier;
        _configuration = configuration;
    }

    public void ClassifyByAi(Fruit fruit)
    {
        var classificationResult = _fruitClassifier.Classify(fruit).Result;
        fruit.Type = classificationResult.Classification.Label;
        
        // classWithHighestScore?.Score > ScoreThresholdForClassification ? classWithHighestScore.Label : ""
    }

    

    public bool SetUserClassification(string fruitId, string type)
    {
        var fruit = _fruitRepository.GetById(fruitId);

        if (fruit == null)
        {
            return false;
        }

        SendUserCorrectionToClassifier(fruitId, type);

        fruit.Type = type;
        fruit.ClassifiedByUser = true;
        _fruitRepository.Update(fruit);
        return true;
    }

    private void SendUserCorrectionToClassifier(string fruitId, string type)
    {
        // var response = _configuration.GetValue<string>("ClassifierBaseUrl")
        //     .AppendPathSegment("user_correction")
        //     .PostMultipartAsync(mp => mp
        //         .AddString("id", fruitId)
        //         .AddString("label", type));
        //
        // _logger.LogInformation("Sent user correction to classifier with {response}", response.Result);
    }
}
