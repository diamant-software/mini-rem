using FruitRecognitionService.Models;

namespace FruitRecognitionService.Services;

public interface IFruitClassifier
{
    public Task<ClassificationResult> Classify(Fruit fruit);
}
