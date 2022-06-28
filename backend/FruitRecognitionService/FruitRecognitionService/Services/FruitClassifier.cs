using System.Net.Http.Headers;
using System.Text.Json;
using FruitRecognitionService.Models;

namespace FruitRecognitionService.Services;

public class FruitClassifier : IFruitClassifier
{
    private readonly IConfiguration _configuration;
    public FruitClassifier(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    public async Task<ClassificationResult> Classify(Fruit fruit)
    {
        var bytes = Convert.FromBase64String(fruit.FileBase64String);
        using var memStream = new MemoryStream(bytes);

        var httpClient = new HttpClient();
        var response = await httpClient.PostAsync(_configuration.GetValue<string>("ClassifierBaseUrl") + "/predict",
            GetContent(memStream, fruit.Id));
        var responseContent = await response.Content.ReadAsStringAsync();
        
        var classAndScore = JsonSerializer.Deserialize<ClassAndScore>(responseContent,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        var classificationResult = new ClassificationResult(fruit.Id, classAndScore!);
        return classificationResult;
    }

    private MultipartFormDataContent GetContent(MemoryStream memoryStream, string fruitId)
    {
        var multipartFormDataContent = new MultipartFormDataContent();
        var streamContentImage = new StreamContent(memoryStream);
        multipartFormDataContent.Add(streamContentImage, "image");
        streamContentImage.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
        {
            Name = "image",
            FileName = "myFile.jpg"
        };

        multipartFormDataContent.Add(new StringContent(fruitId), "id");
        

        multipartFormDataContent.Add(streamContentImage);
        return multipartFormDataContent;
    }
}
