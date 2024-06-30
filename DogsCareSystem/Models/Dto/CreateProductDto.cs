namespace DogsCareSystem.Models.Dto;

public class CreateProductDto
{
    public string Name { get; set; }
    
    public string? Description { get; set; }
    
    public float Cost { get; set; }
    
    public IFormFile Image { get; set; }
}