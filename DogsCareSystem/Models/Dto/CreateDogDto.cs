namespace DogsCareSystem.Models.Dto;

public class CreateDogDto
{
    public int DogId { get; set; }
    
    public string? AppUserId { get; set; }
    
    public int? BreedId { get; set; }
    
    public int? KennelId { get; set; }
    
    public string Name { get; set; }
    
    public int? Age { get; set; }
    
    public string? Gender { get; set; }
    
    public float? Weight { get; set; }
    
    public IFormFile? Image { get; set; }
    
    public bool IsLost { get; set; }
    
}