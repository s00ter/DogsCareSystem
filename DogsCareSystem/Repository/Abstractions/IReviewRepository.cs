using DogsCareSystem.Models;

namespace DogsCareSystem.Repository.Abstractions;

public interface IReviewRepository
{
    bool Add(Review review);
    bool Update(Review review);
    bool Delete(Review review);
    bool Save();
    Task <List<Review>> GetAll();
    Task <Review> GetByIdAsync(int id);
}