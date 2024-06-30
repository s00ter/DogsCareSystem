using DogsCareSystem.Models;
using Microsoft.AspNetCore.Identity;

namespace DogsCareSystem.Data;

public class Seed
{
    public static void SeedData(IApplicationBuilder applicationBuilder)
    {
        using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
        {
            var context = serviceScope.ServiceProvider.GetService<ApplicationContext>();

            context.Database.EnsureCreated();

            if (!context.Dogs.Any())
            {
                context.Dogs.AddRange(new List<Dog>()
                {
                    new Dog()
                    {
                        Name = "собака 1",
                        Age = 5,
                        Gender = "сука",
                        Weight = 10,
                        Image = []
                    },
                    new Dog()
                    {
                        Name = "собака 2",
                        Age = 7,
                        Gender = "кобель",
                        Weight = 12
                    },

                });
                context.SaveChanges();
            }
            
            if (!context.Kennels.Any())
            {
                context.Kennels.AddRange(new List<Kennel>()
                {
                    new Kennel()
                    {
                        Name = "Отделение 1",
                        Address = "Ленина 1",
                        PhoneNumber = "+234",
                        Email = "авымвм@gmail.com"
                    },
                    new Kennel()
                    {
                        Name = "Отделение 2",
                        Address = "Ленина 2",
                        PhoneNumber = "+2345",
                        Email = "dsfdgf@gmail.com"
                    }
                });
                context.SaveChanges();
            }
            
            if (!context.Products.Any())
            {
                context.Products.AddRange(new List<Product>()
                {
                    new Product()
                    {
                        Name = "еда 1",
                        Description = "омисание 1",
                        Cost = 15
                    },
                    new Product()
                    {
                        Name = "еда 2",
                        Description = "описание 2",
                        Cost = 20
                    },

                });
                context.SaveChanges();
            }
            
            if (!context.Breeds.Any())
            {
                context.Breeds.AddRange(new List<Breed>()
                {
                    new Breed()
                    {
                        Name = "порода 1",
                        Size = "размер 1",
                        About = "описание 1"
                    },
                    new Breed()
                    {
                        Name = "порода 2",
                        Size = "размер 2",
                        About = "описание 2"
                    },

                });
                context.SaveChanges();
            }
        }
    }

    public static async Task SeedUsersAndRolesAsync(IApplicationBuilder applicationBuilder)
    {
        using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
        {
            //Roles
            var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
                await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
            if (!await roleManager.RoleExistsAsync(UserRoles.User))
                await roleManager.CreateAsync(new IdentityRole(UserRoles.User));

            //Users
            var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
            string adminUserEmail = "developer@gmail.com";

            var adminUser = await userManager.FindByEmailAsync(adminUserEmail);
            if (adminUser == null)
            {
                var newAdminUser = new AppUser()
                {
                    UserName = "dev",
                    Email = adminUserEmail,
                    EmailConfirmed = true,
                    Address = "123 Main St",
                    PhoneNumber = "+3751"
                };
                await userManager.CreateAsync(newAdminUser, "Coding@1234?");
                await userManager.AddToRoleAsync(newAdminUser, UserRoles.Admin);
            }

            string appUserEmail = "user@etickets.com";

            var appUser = await userManager.FindByEmailAsync(appUserEmail);
            if (appUser == null)
            {
                var newAppUser = new AppUser()
                {
                    UserName = "app-user",
                    Email = appUserEmail,
                    EmailConfirmed = true,
                    Address = "123 Main St",
                    PhoneNumber = "+375"
                };
                await userManager.CreateAsync(newAppUser, "Coding@1234?");
                await userManager.AddToRoleAsync(newAppUser, UserRoles.User);
            }
        }
    }
}