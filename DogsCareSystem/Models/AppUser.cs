using DogsCareSystem.Models.Dto;
using Microsoft.AspNetCore.Identity;

namespace DogsCareSystem.Models;

public class AppUser : IdentityUser
{
    public string? Address { get; set; }
    
    public string? PhoneNumber { get; set; }
    
    
    public List<Dog> Dogs { get; set; }
    
    public List<Review> Reviews { get; set; }
}