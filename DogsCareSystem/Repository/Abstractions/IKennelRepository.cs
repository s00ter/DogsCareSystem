using DogsCareSystem.Models;

namespace DogsCareSystem.Repository.Abstractions;

public interface IKennelRepository
{
    bool Add(Kennel kennel);
    bool Update(Kennel kennel);
    bool Delete(Kennel kennel);
    bool Save();
    Task <List<Kennel>> GetAll();
    Task <Kennel> GetByIdAsync(int id);
}