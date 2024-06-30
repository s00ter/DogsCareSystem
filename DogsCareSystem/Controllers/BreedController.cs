using DogsCareSystem.Models;
using DogsCareSystem.Models.Dto;
using DogsCareSystem.Repository.Abstractions;
using DogsCareSystem.Services.Abstractions;
using HtmlAgilityPack;
using Microsoft.AspNetCore.Mvc;
using MoreLinq.Extensions;

namespace DogsCareSystem.Controllers;

public class BreedController(
    IBreedRepository breedRepository,
    IBreedService breedService,
    ISearchService searchService
    ) 
    : Controller
{
    public async Task<IActionResult> Index()
    {
        var res = await breedRepository.GetAll();
        return View(res);
    }
    
    public async Task<IActionResult> Detail(int id)
    {
        var breed = await breedRepository.GetByIdAsync(id);
        return View(breed);
    }
    
    public async Task<IActionResult> Parse()
    {
        await breedService.Parse();
        
        return RedirectToAction("Index", "Breed");
    }
    
    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
        var breed = await breedRepository.GetByIdAsync(id);
        breedRepository.Delete(breed);
        return RedirectToAction("Index");
    }
    
    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }
    
    public async Task<IActionResult> Create(CreateBreedDto clubVM)
    {
        byte[] bytes = null;
        
        if (clubVM.Image != null)
        {
            using var fileStream = clubVM.Image.OpenReadStream();
            bytes = new byte[clubVM.Image.Length];
            fileStream.Read(bytes, 0, (int)clubVM.Image.Length);
        }
        
        if (ModelState.IsValid)
        {
            var club = new Breed()
            {
                Name = clubVM.Name,
                About = clubVM.About,
                Country = clubVM.Country,
                Size = clubVM.Size,
                AvgLifeTime = clubVM.AvgLifeTime,
                Image = bytes
            };
            breedRepository.Add(club);
            return RedirectToAction("Index");
        }
        else
        {
            ModelState.AddModelError("", "Failed");
        }

        return View(clubVM);
    }
    
    [HttpPost]
    public async Task<IActionResult> Search(string str)
    {
        if (str == null)
        {
            return RedirectToAction("Index");
        }
        
        var res = await searchService.Search(str);
        
        return View(res);
    }
}