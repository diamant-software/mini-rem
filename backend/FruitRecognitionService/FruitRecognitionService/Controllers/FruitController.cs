using FruitRecognitionService.Models;
using FruitRecognitionService.Services;
using FruitRecognitionService.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
//using Rem.Infrastructure.Authentication;
using Serilog.Context;

namespace FruitRecognitionService.Controllers;

/// <summary>
/// Controller loading and classifying fruits.
/// </summary>
[Route("api/Fruit")]
[ApiController]
//[Authorize (Policy = "unrestricted")]
public class FruitController : ControllerBase
{
    private readonly ILogger<FruitController> _logger;
    private readonly IFruitRepository _fruitRepository;
    private readonly IFruitClassificationService _fruitClassificationService;

    /// <summary>
    /// Instantiate the controller 
    /// </summary>
    public FruitController(
        ILogger<FruitController> logger,
        IFruitRepository fruitRepository,
        IFruitClassificationService fruitClassificationService)
    {
        _logger = logger;
        _fruitRepository = fruitRepository;
        _fruitClassificationService = fruitClassificationService;
    }

    /// <summary>
    /// Returns all fruits
    /// </summary>
    [HttpGet]
    [Route("")]
    [ProducesResponseType(typeof(IEnumerable<Fruit>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status204NoContent)]
    public IActionResult GetAllFruits()
    {
        _logger.LogInformation("GetAllFruits requested.");

        try
        {
            var fruits = _fruitRepository.GetAll();

            if (!fruits.Any())
            {
                _logger.LogInformation("No fruits available.");
                return NoContent();
            }

            _logger.LogInformation("Finished GetAllFruits request.");
            return Ok(fruits);
        }
        catch (Exception ex)
        {
            _logger.LogError("Exception when getting fruits: {exception}.", ex);
            return NoContent();
        }
    }

    /// <summary>
    /// Classify a specific fruit
    /// </summary>
    /// <param name="id">string Key</param>
    /// <param name="type">Fruit Type</param>
    [HttpPost]
    [Route("{id}/classify")]
    [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status204NoContent)]
    public IActionResult Classify(string id, [FromBody] UserClassification userClassification)
    {
        LogContext.PushProperty("fruitId", id);
        _logger.LogInformation("Fruit classification requested");

        try
        {
            if (_fruitClassificationService.SetUserClassification(id, userClassification.Type))
            {
                return Ok();
            }

            _logger.LogError("Could not classify the fruit.");
            return NotFound();
        }
        catch (Exception ex)
        {
            _logger.LogError("Could not classify the fruit. {exception}", ex);
            return NotFound();
        }
    }
}