using FruitsApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace FruitsApi;

[ApiController]
[Route("api/fruit")]
public class FruitsController : ControllerBase
{
    private readonly ISendToKafkaService _sendToKafkaService;

    public FruitsController(ISendToKafkaService sendToKafkaService)
    {
        _sendToKafkaService = sendToKafkaService;
    }

    [HttpPost]
    public async Task<IActionResult> PostDocument(IFormFile document, CancellationToken cancellationToken)
    {
        await _sendToKafkaService.SendAsync(document, cancellationToken);
        return Ok();
    }
}