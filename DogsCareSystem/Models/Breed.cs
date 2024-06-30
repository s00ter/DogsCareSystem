namespace DogsCareSystem.Models;

public class Breed
{
    public int BreedId { get; set; }
    public string Name { get; set; }
    public string? About { get; set; }
    public string? Country { get; set; }
    public string? Size { get; set; }
    public string? AvgLifeTime { get; set; }
    public byte[]? Image { get; set; }
    
    public List<Dog>? Dogs { get; set; }
}