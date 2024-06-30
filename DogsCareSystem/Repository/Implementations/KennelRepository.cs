using DogsCareSystem.Models;
using DogsCareSystem.Repository.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace DogsCareSystem.Repository.Implementations;

public class KennelRepository(ApplicationContext _context) : IKennelRepository
{
    public bool Add(Kennel kennel)
    {
        _context.Add(kennel);
        return Save();
    }

    public bool Update(Kennel kennel)
    {
        _context.Update(kennel);
        return Save();
    }

    public bool Delete(Kennel kennel)
    {
        _context.Remove(kennel);
        return Save();
    }

    public bool Save()
    {
        var res = _context.SaveChanges();
        return res > 0;
    }

    public async Task<List<Kennel>> GetAll()
    {
        return await _context.Kennels.ToListAsync();
    }

    public async Task<Kennel> GetByIdAsync(int id)
    {
        return await _context.Kennels.Include(x=>x.Dogs).FirstOrDefaultAsync(x => x.KennelId == id);
    }
}