using System.ComponentModel.DataAnnotations;

namespace DogsCareSystem.Models.Dto;

public class CreateReviewDto
{
    [Range(1,10, ErrorMessage = "Рейтинг от 1 до 10")]
    public int? Rating { get; set; }
    
    public string? Comment { get; set; }
    
}