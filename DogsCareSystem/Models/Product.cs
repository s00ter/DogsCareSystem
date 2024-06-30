namespace DogsCareSystem.Models;

public class Product
{
    public int ProductId { get; set; }
    
    public string Name { get; set; }
    
    public string? Description { get; set; }
    
    public float Cost { get; set; }
    
    public byte[]? Image { get; set; }
    
    
    public List<Review>? Reviews { get; set; }
}