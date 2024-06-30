using DogsCareSystem.Extensions;
using DogsCareSystem.Models;
using DogsCareSystem.Models.Dto;
using DogsCareSystem.Repository.Abstractions;
using DogsCareSystem.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace DogsCareSystem.Controllers;

public class DogController(
    IDogRepository dogRepository,
    IBreedRepository breedRepository,
    IKennelRepository kennelRepository,
    IHttpContextAccessor httpContextAccessor,
    ApplicationContext context) 
    : Controller
{
    public async Task<IActionResult> Index()
    {
        var res = await dogRepository.GetAll();
        return View(res);
    }
    
    public async Task<IActionResult> LostIndex()
    {
        var res = await dogRepository.GetAll();
        var list = res.Where(x => x.IsLost == true).ToList();
        return View(list);
    }

    public async Task<List<Dog>> GetAllUserDogs()
    {
        var curUser = httpContextAccessor.HttpContext?.User;
        var dbDogs = await dogRepository.GetAll();
        var userDogs = dbDogs.Where(d => d.AppUser.Id == curUser.ToString()).ToList();

        return userDogs;
    }
    
    [HttpGet]
    public async Task<IActionResult> Create()
    {
        var breeds = await breedRepository.GetAll();
        ViewBag.Breeds = breeds;
        
        var kennel = await kennelRepository.GetAll();
        ViewBag.Kennels = kennel;
        
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateDogDto clubVM)
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
            var club = new Dog
            {
                Name = clubVM.Name,
                Age = clubVM.Age,
                Image = bytes,
                Gender = clubVM.Gender,
                Weight = clubVM.Weight,
                AppUserId = clubVM.AppUserId,
                BreedId = clubVM.BreedId,
                KennelId = clubVM.KennelId,
                IsLost = clubVM.IsLost
            };

            if (httpContextAccessor.HttpContext.User.IsInRole("user"))
            {
                club.AppUserId = httpContextAccessor.HttpContext.User.GetUserId();
            }
            
            dogRepository.Add(club);
            return RedirectToAction("Index");
        }
        else
        {
            ModelState.AddModelError("", "Failed");
        }

        return View(clubVM);
    }
    
    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
        var breed = await dogRepository.GetByIdAsync(id);
        dogRepository.Delete(breed);
        return RedirectToAction("Index");
    }
    
    public async Task<IActionResult> Detail(int id)
    {
        var dog = await dogRepository.GetByIdAsync(id);
        return View(dog);
    }
    
    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var clubVM = await dogRepository.GetByIdAsync(id);
        
        var dog = new CreateDogDto()
        {
            Name = clubVM.Name,
            Age = clubVM.Age,
            Gender = clubVM.Gender,
            Weight = clubVM.Weight,
            AppUserId = clubVM.AppUserId,
            BreedId = clubVM.BreedId,
            KennelId = clubVM.KennelId,
            IsLost = clubVM.IsLost
        };
        
        var breeds = await breedRepository.GetAll();
        ViewBag.Breeds = breeds;
        
        var kennel = await kennelRepository.GetAll();
        ViewBag.Kennels = kennel;
        
        return View(dog);
    }
    
    [HttpPost]
    public async Task<IActionResult> Edit(int id, CreateDogDto clubVM)
    {
        var DbDog = await dogRepository.GetByIdAsync(id);
        context.DetachEntity(DbDog);
        
        if (!ModelState.IsValid)
        {
            ModelState.AddModelError("", "Failed to edit");
            return View("Edit", clubVM);
        }

        byte[] bytes = null;
        
        if (clubVM.Image != null)
        {
            using var fileStream = clubVM.Image.OpenReadStream();
            bytes = new byte[clubVM.Image.Length];
            fileStream.Read(bytes, 0, (int)clubVM.Image.Length);
        }
        
        var club = new Dog()
        {
            DogId = id,
            Name = clubVM.Name,
            Age = clubVM.Age,
            Image = bytes,
            Gender = clubVM.Gender,
            Weight = clubVM.Weight,
            AppUserId = DbDog.AppUserId,
            BreedId = clubVM.BreedId,
            KennelId = clubVM.KennelId,
            IsLost = clubVM.IsLost
        };

        if (club.Image == null)
        {
            club.Image = DbDog.Image;
        }

        dogRepository.Update(club);

        return RedirectToAction("Index");
    }
    
    
}