using DogsCareSystem.Models;
using DogsCareSystem.Models.Dto;
using DogsCareSystem.Repository.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace DogsCareSystem.Controllers;

public class ProductController(
    IProductRepository productRepository,
    IHttpContextAccessor httpContextAccessor) 
    : Controller
{
    public async Task<IActionResult> Index()
    {
        var res = await productRepository.GetAll();
        return View(res);
    }
    
    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateProductDto clubVM)
    {
        using var fileStream = clubVM.Image.OpenReadStream();
        byte[] bytes = new byte[clubVM.Image.Length];
        fileStream.Read(bytes, 0, (int)clubVM.Image.Length);
        
        if (ModelState.IsValid)
        {
            var club = new Product
            {
                Name = clubVM.Name,
                Description = clubVM.Description,
                Image = bytes,
                Cost = clubVM.Cost
            };
            productRepository.Add(club);
            return RedirectToAction("Index");
        }
        else
        {
            ModelState.AddModelError("", "Photo upload failed");
        }

        return View(clubVM);
    }
    
    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
        var breed = await productRepository.GetByIdAsync(id);
        productRepository.Delete(breed);
        return RedirectToAction("Index");
    }
    
    public async Task<IActionResult> Detail(int id)
    {
        var dog = await productRepository.GetByIdAsync(id);
        return View(dog);
    }
}