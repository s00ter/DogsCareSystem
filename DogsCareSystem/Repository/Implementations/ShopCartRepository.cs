using DogsCareSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace DogsCareSystem.Repository.Implementations;

public class ShopCartRepository(ApplicationContext _context)
{
    public string ShopCartId { get; set; }
    public List<ShopCartItem> ListShopItems { get; set; }

    
    public static ShopCartRepository GetCart(IServiceProvider services)
    {
        ISession session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;
        var context = services.GetService<ApplicationContext>();
        string shopCartId = session.GetString("CartId") ?? Guid.NewGuid().ToString();

        session.SetString("CartId", shopCartId);

        return new ShopCartRepository(context)
        {
            ShopCartId = shopCartId
        };
    }

    public void AddToCart(Product product)
    {
        _context.ShoppingCartItems.Add(new ShopCartItem()
        {
            ShopCartId = ShopCartId,
            Product = product,
            Price = product.Cost
        });
        _context.SaveChanges();
    }
    
    public void DeleteFromCart(int id)
    {
        var item = _context.ShoppingCartItems.FirstOrDefault(x => x.Id == id);
        _context.ShoppingCartItems.Remove(item);
        
        _context.SaveChanges();
    }
    
    public List<ShopCartItem> GetShopItems()
    {
        return _context.ShoppingCartItems.Where(x => x.ShopCartId == ShopCartId)
            .Include(x => x.Product)
            .ToList();
    }
}