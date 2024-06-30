using DogsCareSystem.Models;

namespace DogsCareSystem.Repository.Abstractions;

public interface IProductRepository
{
    bool Add(Product product);
    bool Update(Product product);
    bool Delete(Product product);
    bool Save();
    Task <List<Product>> GetAll();
    Task <Product> GetByIdAsync(int id);
}