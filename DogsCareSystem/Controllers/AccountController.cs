using DogsCareSystem.Data;
using DogsCareSystem.Models;
using DogsCareSystem.Models.Dto;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace DogsCareSystem.Controllers;

public class AccountController(
    UserManager<AppUser> _userManager, 
    SignInManager<AppUser> _signInManager, 
    ApplicationContext _context
    ) 
    : Controller
{
    
    public IActionResult Login()
    {
        var response = new LoginDto();
        return View(response);
    }
    
    [HttpPost]
    public async Task<IActionResult> Login(LoginDto loginDto)
    {
        if (!ModelState.IsValid) return View(loginDto);

        var user = await _userManager.FindByEmailAsync(loginDto.Email);

        if (user != null)
        {
            //User is found, check password
            var passwordCheck = await _userManager.CheckPasswordAsync(user, loginDto.Password);
            if (passwordCheck)
            {
                //Password correct, sign in
                var result = await _signInManager.PasswordSignInAsync(user, loginDto.Password, false, false);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            //Password is incorrect
            TempData["Error"] = "Неверные данные";
            return View(loginDto);
        }
        //User not found
        TempData["Error"] = "Неверные данные";
        return View(loginDto);
    }
    
    [HttpGet]
    public IActionResult Register()
    {
        var response = new RegisterDto();
        return View(response);
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterDto registerViewModel)
    {
        if (!ModelState.IsValid) return View(registerViewModel);

        var user = await _userManager.FindByEmailAsync(registerViewModel.Email);
        if (user != null)
        {
            TempData["Error"] = "This email address is already in use";
            return View(registerViewModel);
        }

        var newUser = new AppUser()
        {
            Email = registerViewModel.Email,
            UserName = registerViewModel.Email,
            PhoneNumber = registerViewModel.PhoneNumber
        };
        var newUserResponse = await _userManager.CreateAsync(newUser, registerViewModel.Password);

        if (newUserResponse.Succeeded)
            await _userManager.AddToRoleAsync(newUser, UserRoles.User);

        return RedirectToAction("Index", "Home");
    }
    
    [HttpPost]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Index", "Home");
    }
}