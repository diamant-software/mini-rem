namespace FruitRecognitionService.Models;

public class Fruit
{
    public string Id { get; set; }
    public string FileBase64String { get; set; }
    public string Type { get; set; }
    public bool ClassifiedByUser { get; set; }

    public Fruit(string id, string fileBase64String, string type, bool classifiedByUser)
    {
        Id = id;
        FileBase64String = fileBase64String;
        Type = type;
        ClassifiedByUser = classifiedByUser;
    }
}
