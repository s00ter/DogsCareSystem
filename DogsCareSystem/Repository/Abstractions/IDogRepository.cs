using DogsCareSystem.Models;

namespace DogsCareSystem.Repository.Abstractions;

public interface IDogRepository
{
    bool Add(Dog dog);
    bool Update(Dog dog);
    bool Delete(Dog dog);
    bool Save();
    Task <List<Dog>> GetAll();
    Task <Dog> GetByIdAsync(int id);
    Task<Dog?> GetByIdAsyncNoTracking(int id);
}