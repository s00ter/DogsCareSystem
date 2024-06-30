using DogsCareSystem.Models.Dto;
using DogsCareSystem.Repository.Abstractions;
using DogsCareSystem.Repository.Implementations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace DogsCareSystem.Controllers;

public class ShopCartController(IProductRepository productRepository,
    ShopCartRepository shopCartRepository) : Controller
{
    public ViewResult Index()
    {
        var items = shopCartRepository.GetShopItems();
        shopCartRepository.ListShopItems = items;

        var obj = new ShopCartDto()
        {
            ShopCartRepository = shopCartRepository
        };

        return View(obj);
    }

    public async Task<IActionResult> addToCart(int id)
    {
        var products = await productRepository.GetAll();
        var item = products.FirstOrDefault(x => x.ProductId == id);

        if (item != null)
        {
            shopCartRepository.AddToCart(item);
        }

        return RedirectToAction("Index", "Product");
    }
    
    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
        shopCartRepository.DeleteFromCart(id);

        return RedirectToAction("Index", "ShopCart");
    }
    
    public ViewResult Complete()
    {
        var items = shopCartRepository.GetShopItems();
        float x = 0;
        foreach (var item in items)
        {
            x += item.Price;
        }
        
        ViewBag.Cost = x;
        
        return View();
    }
}