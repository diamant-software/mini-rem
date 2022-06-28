using FruitRecognitionService.Models;

namespace FruitRecognitionService.Persistence;

public class FruitRepository : IFruitRepository
{
    private readonly ILogger<FruitRepository> _logger;
    private readonly Dictionary<string, Fruit> _fruits;

    public FruitRepository(ILogger<FruitRepository> logger)
    {
        _logger = logger;
        _fruits = new Dictionary<string, Fruit>();
    }

    public IEnumerable<Fruit> GetAll()
    {
        return _fruits.Values;
    }

    public Fruit? GetById(string fruitId)
    {
        var fruitExists = _fruits.TryGetValue(fruitId, out Fruit? fruit);
        if (!fruitExists)
        {
            _logger?.LogInformation("Fruit with {Id} doesn't exist.", fruitId);
        }

        return fruit;
    }

    public void Insert(Fruit fruit)
    {

        if (!_fruits.ContainsKey(fruit.Id))
        {
            _fruits.Add(fruit.Id, fruit);
        }
        else
        {
            _logger?.LogInformation("Fruit with {Id} already exists.", fruit.Id);
        }
    }

    public void Update(Fruit fruit)
    {
        Delete(fruit.Id);
        _fruits.Add(fruit.Id, fruit);
    }

    public void Delete(string fruitId)
    {
        _fruits.Remove(fruitId);
    }

}
