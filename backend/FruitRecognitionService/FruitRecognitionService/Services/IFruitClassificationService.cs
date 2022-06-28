using FruitRecognitionService.Models;

namespace FruitRecognitionService.Services;

public interface IFruitClassificationService
{
    public void ClassifyByAi(Fruit fruit);

    public bool SetUserClassification(string fruitId, string type);
}
