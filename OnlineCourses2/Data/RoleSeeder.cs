using Microsoft.AspNetCore.Identity;
using OnlineCourses2.Models;

namespace OnlineCourses2.Data
{
    public class RoleSeeder
    {
        public static async Task SeedRolesAsync(RoleManager<IdentityRole> roleManager)
        {
            string[] roles = { "Admin", "Organizer", "User" };

            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }
        }

        public static async Task SeedAdminAsync(
    UserManager<ApplicationUser> userManager,
    RoleManager<IdentityRole> roleManager)
        {
            string adminEmail = "admin@site.com";
            string adminPassword = "Admin123!";

            if (await userManager.FindByEmailAsync(adminEmail) == null)
            {
                var adminUser = new ApplicationUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    EmailConfirmed = true,
                    City = "София",
                    FirstName = "Преслава",
                    MiddleName= "Пейчева",
                    LastName = "Пейчева",
                    
                };

                var result = await userManager.CreateAsync(adminUser, adminPassword);

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, "Admin");
                }
            }
        }
        public static async Task SeedOrganizerAsync(
    UserManager<ApplicationUser> userManager,
    RoleManager<IdentityRole> roleManager)
        {
            string organizerEmail = "organizer@site.com";
            string organizerPassword = "Organizer123!";

            if (await userManager.FindByEmailAsync(organizerEmail) == null)
            {
                var organizerUser = new ApplicationUser
                {
                    UserName = organizerEmail,
                    Email = organizerEmail,
                    EmailConfirmed = true,
                    FirstName = "Айрис",
                    MiddleName = "Пейчева",
                    LastName = "Пейчева",
                    City = "София"
                };

                var result = await userManager.CreateAsync(organizerUser, organizerPassword);

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(organizerUser, "Organizer");
                }
            }
        }

    }
}
