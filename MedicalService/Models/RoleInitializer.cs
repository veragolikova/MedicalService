using MedicalService.Models;  
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
 
namespace MedicalService
{
    public class RoleInitializer
    {
        public static async Task InitializeAsync(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            string adminEmail = "MSAdmin@gmail.com";
            string password = "MSAdmin-12345";
            if (await roleManager.FindByNameAsync("admin") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("admin"));
            }
            if (await roleManager.FindByNameAsync("doctor") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("doctor"));
            }
            if (await roleManager.FindByNameAsync("patient") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("patient"));
            }
            if (await userManager.FindByNameAsync(adminEmail) == null)
            {
                User admin = new User { Email = adminEmail, UserName = adminEmail };
                IdentityResult result = await userManager.CreateAsync(admin, password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, "admin");
                }
            }
        }
    }
}