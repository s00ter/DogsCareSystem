namespace DogsCareSystem.Models.Dto;

public class EditDogDto
{
    public int? BreedId { get; set; }
    
    public int? KennelId { get; set; }
    
    public string Name { get; set; }
    
    public int? Age { get; set; }
    
    public string? Gender { get; set; }
    
    public float? Weight { get; set; }
    
    public string IsLost { get; set; }
}