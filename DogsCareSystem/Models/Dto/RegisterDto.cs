using System.ComponentModel.DataAnnotations;

namespace DogsCareSystem.Models.Dto;

public class RegisterDto
{
    [Display(Name = "Email address")]
    [Required(ErrorMessage = "Email address is required")]
    public string Email { get; set; }
    
    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }
    
    [Display(Name = "Confirm password")]
    [Required(ErrorMessage = "Confirm password is required")]
    [DataType(DataType.Password)]
    [Compare("Password", ErrorMessage = "Password do not match")]
    public string ConfirmPassword { get; set; }
    
    [DataType(DataType.PhoneNumber)]
    public string PhoneNumber { get; set; }
}