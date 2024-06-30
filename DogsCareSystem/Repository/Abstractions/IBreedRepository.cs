using DogsCareSystem.Models;

namespace DogsCareSystem.Repository.Abstractions;

public interface IBreedRepository
{
    bool Add(Breed breed);
    bool Update(Breed breed);
    bool Delete(Breed breed);
    bool Save();
    Task <List<Breed>> GetAll();
    Task <Breed> GetByIdAsync(int id);
    bool AddRange(List<Breed> breeds);
}