using DogsCareSystem.Models;
using DogsCareSystem.Repository.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace DogsCareSystem.Repository.Implementations;

public class ReviewRepository(ApplicationContext _context) : IReviewRepository
{
    public bool Add(Review review)
    {
        _context.Add(review);
        return Save();
    }

    public bool Update(Review review)
    {
        _context.Update(review);
        return Save();
    }

    public bool Delete(Review review)
    {
        _context.Remove(review);
        return Save();
    }

    public bool Save()
    {
        var res = _context.SaveChanges();
        return res > 0;
    }

    public async Task<List<Review>> GetAll()
    {
        return await _context.Reviews.ToListAsync();
    }

    public async Task<Review> GetByIdAsync(int id)
    {
        return await _context.Reviews.FirstOrDefaultAsync(x => x.ReviewId == id);
    }
}