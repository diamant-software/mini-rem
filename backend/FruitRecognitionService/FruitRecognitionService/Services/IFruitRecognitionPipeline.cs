namespace FruitRecognitionService.Services;

public interface IFruitRecognitionPipeline
{
    public void Recognize(CancellationToken cancellationToken);
}
