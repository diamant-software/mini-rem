using FruitRecognitionService.Models;

namespace FruitRecognitionService.Persistence;

public interface IFruitRepository
{
    public IEnumerable<Fruit> GetAll();
    public Fruit? GetById(string fruitId);
    public void Insert(Fruit fruit);
    public void Update(Fruit fruit);
    void Delete(string fruitId);
}
