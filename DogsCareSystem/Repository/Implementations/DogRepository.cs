using DogsCareSystem.Models;
using DogsCareSystem.Repository.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace DogsCareSystem.Repository.Implementations;

public class DogRepository(ApplicationContext _context) : IDogRepository
{
    public bool Add(Dog dog)
    {
        _context.Add(dog);
        return Save();
    }

    public bool Update(Dog dog)
    {
        _context.Update(dog);
        return Save();
    }

    public bool Delete(Dog dog)
    {
        _context.Remove(dog);
        return Save();
    }

    public bool Save()
    {
        var res = _context.SaveChanges();
        return res > 0;
    }

    public async Task<List<Dog>> GetAll()
    {
        return await _context.Dogs.Include(x=>x.Breed).ToListAsync();
    }

    public async Task<Dog> GetByIdAsync(int id)
    {
        return await _context.Dogs.Include(x=>x.Breed)
            .Include(x=>x.AppUser)
            .Include(x=> x.Kennel).FirstOrDefaultAsync(x => x.DogId == id);
    }
    
    public async Task<Dog?> GetByIdAsyncNoTracking(int id)
    {
        return await _context.Dogs.Include(i => i.Breed).AsNoTracking().FirstOrDefaultAsync(i => i.DogId == id);
    }

}