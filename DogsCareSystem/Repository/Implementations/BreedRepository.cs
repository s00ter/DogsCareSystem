using DogsCareSystem.Models;
using DogsCareSystem.Repository.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace DogsCareSystem.Repository.Implementations;

public class BreedRepository(
    ApplicationContext _context
    ) 
    : IBreedRepository
{
    public bool Add(Breed breed)
    {
        _context.Add(breed);
        return Save();
    }

    public bool Update(Breed breed)
    {
        _context.Update(breed);
        return Save();
    }

    public bool Delete(Breed breed)
    {
        _context.Remove(breed);
        return Save();
    }

    public bool Save()
    {
        var res = _context.SaveChanges();
        return res > 0;
    }

    public async Task<List<Breed>> GetAll()
    {
        return await _context.Breeds.ToListAsync();
    }

    public async Task<Breed> GetByIdAsync(int id)
    {
        return await _context.Breeds.Include(x=>x.Dogs).FirstOrDefaultAsync(x => x.BreedId == id);
    }

    public bool AddRange(List<Breed> breeds)
    {
        _context.AddRange(breeds);
        return Save();
    }
}