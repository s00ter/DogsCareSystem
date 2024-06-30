using System.ComponentModel.DataAnnotations.Schema;

namespace DogsCareSystem.Models;

public class Dog
{
    public int DogId { get; set; }
    
    public string? AppUserId { get; set; }
    
    public int? BreedId { get; set; }
    
    public int? KennelId { get; set; }
    
    public string Name { get; set; }
    
    public int? Age { get; set; }
    
    public string? Gender { get; set; }
    
    public float? Weight { get; set; }
    
    public byte[]? Image { get; set; }
    
    public bool IsLost { get; set; }
    
    
    public AppUser? AppUser { get; set; }
    
    public Breed? Breed { get; set; }
    
    public Kennel? Kennel { get; set; }
    
    
}