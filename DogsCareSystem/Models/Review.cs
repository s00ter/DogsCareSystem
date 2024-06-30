using Microsoft.AspNetCore.Mvc;

namespace DogsCareSystem.Models;

public class Review
{
    public int ReviewId { get; set; }
    
    public string? AppUserId { get; set; }
    
    public int ProductId { get; set; }
    
    public int? Rating { get; set; }
    
    public string? Comment { get; set; }
    
    public DateTime CreatedAt { get; set; }
    
    
    public AppUser? AppUser { get; set; }
    
}