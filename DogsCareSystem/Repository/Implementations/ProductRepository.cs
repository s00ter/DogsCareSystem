using DogsCareSystem.Models;
using DogsCareSystem.Repository.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace DogsCareSystem.Repository.Implementations;

public class ProductRepository(ApplicationContext _context) : IProductRepository
{
    public bool Add(Product product)
    {
        _context.Add(product);
        return Save();
    }

    public bool Update(Product product)
    {
        _context.Update(product);
        return Save();
    }

    public bool Delete(Product product)
    {
        _context.Remove(product);
        return Save();
    }

    public bool Save()
    {
        var res = _context.SaveChanges();
        return res > 0;
    }

    public async Task<List<Product>> GetAll()
    {
        return await _context.Products.Include(x=>x.Reviews).ToListAsync();
    }

    public async Task<Product> GetByIdAsync(int id)
    {
        return await _context.Products.Include(x=>x.Reviews).FirstOrDefaultAsync(x => x.ProductId == id);
    }
}