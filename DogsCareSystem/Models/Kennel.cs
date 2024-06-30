namespace DogsCareSystem.Models;

public class Kennel
{
    public string Name { get; set; }
    
    public int KennelId { get; set; }
    
    public string? Address { get; set; }
    
    public string? PhoneNumber { get; set; }
    
    public string? Email { get; set; }
    
    public byte[]? Image { get; set; }
    
    public List<Dog>? Dogs { get; set; }
}