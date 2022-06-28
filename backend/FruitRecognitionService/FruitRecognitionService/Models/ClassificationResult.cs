namespace FruitRecognitionService.Models;

public class ClassificationResult
{
    public string Id { get; set; }
    public ClassAndScore Classification { get; set; }

    public ClassificationResult(string id, ClassAndScore classification)
    {
        Id = id;
        Classification = classification;
    }
}

public class ClassAndScore
{
    public string Label { get; set; }
    public double Confidence { get; set; }

    public ClassAndScore(string label, double confidence)
    {
        Label = label;
        Confidence = confidence;
    }
}