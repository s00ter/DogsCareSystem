namespace DogsCareSystem.Models.Dto;

public class CreateBreedDto
{
    public string Name { get; set; }
    public string? About { get; set; }
    public string? Country { get; set; }
    public string? Size { get; set; }
    public string? AvgLifeTime { get; set; }
    public IFormFile? Image { get; set; }
}