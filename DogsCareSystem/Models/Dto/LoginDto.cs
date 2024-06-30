using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DogsCareSystem.Models.Dto;

public class LoginDto
{
    [Display(Name = "Email Address")]
    [Required(ErrorMessage = "Email address is required")]
    public string Email { get; set; }
    
    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }
}