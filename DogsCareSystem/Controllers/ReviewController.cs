using DogsCareSystem.Extensions;
using DogsCareSystem.Models;
using DogsCareSystem.Models.Dto;
using DogsCareSystem.Repository.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace DogsCareSystem.Controllers;

public class ReviewController(IReviewRepository reviewRepository,
    IProductRepository productRepository,
    IHttpContextAccessor httpContextAccessor) : Controller
{
    public async Task<List<Review>> Index()
    {
        var res = await reviewRepository.GetAll();
        return res;
    }
    
    [HttpGet]
    public async Task<IActionResult> Create(int id)
    {
        
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(int id, CreateReviewDto clubVM)
    {
        
        
        if (ModelState.IsValid)
        {
            var club = new Review()
            {
                AppUserId = httpContextAccessor.HttpContext.User.GetUserId(),
                Comment = clubVM.Comment,
                CreatedAt = DateTime.Now,
                ProductId = id,
                Rating = clubVM.Rating
            };
            reviewRepository.Add(club);
            return RedirectToAction("Index", "Product");
        }
        else
        {
            ModelState.AddModelError("", "Photo upload failed");
        }

        return View(clubVM);
    }
    
}