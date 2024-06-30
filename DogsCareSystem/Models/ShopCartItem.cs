using System.ComponentModel.DataAnnotations;

namespace DogsCareSystem.Models;

public class ShopCartItem
{
    public int Id { get; set; }

    public string ShopCartId { get; set; }

    public float Price { get; set; }

    public Product Product { get; set; }
}