namespace DogsCareSystem.Models.Dto;

public class CreateKennelDto
{
    public int KennelId { get; set; }
    
    public string? Name { get; set; }
    public string? Address { get; set; }
    
    public string? PhoneNumber { get; set; }
    
    public string? Email { get; set; }
    
    public IFormFile? Image { get; set; }
}