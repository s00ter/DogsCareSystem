using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DogsCareSystem.Models;

public class ApplicationContext : IdentityDbContext<AppUser>
{
    public ApplicationContext(DbContextOptions<ApplicationContext> options) 
        : base(options)
    {
        
    }
    
    public DbSet<Dog> Dogs { get; set; }
    
    public DbSet<Kennel> Kennels { get; set; }
    
    public DbSet<Product> Products { get; set; }
    
    public DbSet<Review> Reviews { get; set; }
    
    public DbSet<Breed> Breeds { get; set; }
    
    public DbSet<ShopCartItem> ShoppingCartItems { get; set; }
    
    public void DetachEntity<TEntity>(TEntity entity)
    {
        Entry(entity).State = EntityState.Detached;
    }
}