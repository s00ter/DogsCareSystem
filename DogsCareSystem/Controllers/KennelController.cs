using DogsCareSystem.Extensions;
using DogsCareSystem.Models;
using DogsCareSystem.Models.Dto;
using DogsCareSystem.Repository.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace DogsCareSystem.Controllers;

public class KennelController(IKennelRepository kennelRepository) : Controller
{
    public async Task<IActionResult> Index()
    {
        var res = await kennelRepository.GetAll();
        return View(res);
    }
    
    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
        var breed = await kennelRepository.GetByIdAsync(id);
        kennelRepository.Delete(breed);
        return RedirectToAction("Index");
    }
    
    public async Task<IActionResult> Detail(int id)
    {
        var dog = await kennelRepository.GetByIdAsync(id);
        return View(dog);
    }
    
    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateKennelDto clubVM)
    {
        using var fileStream = clubVM.Image.OpenReadStream();
        byte[] bytes = new byte[clubVM.Image.Length];
        fileStream.Read(bytes, 0, (int)clubVM.Image.Length);
        
        if (ModelState.IsValid)
        {
            var club = new Kennel
            {
                Name = clubVM.Name,
                Address = clubVM.Address,
                Image = bytes,
                PhoneNumber = clubVM.PhoneNumber,
                Email = clubVM.Email
            };
            kennelRepository.Add(club);
            return RedirectToAction("Index");
        }
        else
        {
            ModelState.AddModelError("", "Photo upload failed");
        }

        return View(clubVM);
    }
}