namespace FruitsApi.Services;

public interface ISendToKafkaService
{
    Task SendAsync(IFormFile file, CancellationToken cancellationToken);
}