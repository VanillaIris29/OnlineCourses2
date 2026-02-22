using Microsoft.AspNetCore.Identity;
using OnlineCourses2.Models;

namespace OnlineCourses2.Data.Seeders
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
        public static async Task SeedUsersAsync(UserManager<ApplicationUser> userManager)
        {
            var users = new List<(ApplicationUser User, string Password)>
    {
        (
            new ApplicationUser
            {
                UserName = "user4@site.com",
                Email = "user4@site.com",
                EmailConfirmed = true,
                FirstName = "Ива",
                MiddleName = "Георгиева",
                LastName = "Петрова",
                City = "Варна",
                Country = "България",
                Age = 22
            },
            "User4123!"
        ),
        (
            new ApplicationUser
            {
                UserName = "user5@site.com",
                Email = "user5@site.com",
                EmailConfirmed = true,
                FirstName = "Калоян",
                MiddleName = "Илиев",
                LastName = "Димитров",
                City = "Бургас",
                Country = "България",
                Age = 25
            },
            "User5123!"
        ),
        (
            new ApplicationUser
            {
                UserName = "user6@site.com",
                Email = "user6@site.com",
                EmailConfirmed = true,
                FirstName = "Силвия",
                MiddleName = "Маринова",
                LastName = "Костова",
                City = "Русе",
                Country = "България",
                Age = 27
            },
            "User6123!" // парола за user3
        ),
        (
            new ApplicationUser
            {
                UserName = "user7@site.com",
                Email = "user7@site.com",
                EmailConfirmed = true,
                FirstName = "Петър",
                MiddleName = "Николов",
                LastName = "Колев",
                City = "Стара Загора",
                Country = "България",
                Age = 30
            },
            "User7123!" // парола за user4
        ),
        (
            new ApplicationUser
            {
                UserName = "user8@site.com",
                Email = "user8@site.com",
                EmailConfirmed = true,
                FirstName = "Елица",
                MiddleName = "Христова",
                LastName = "Ангелова",
                City = "Плевен",
                Country = "България",
                Age = 24
            },
            "User8123!" // парола за user5
        )
    };

            foreach (var (user, password) in users)
            {
                if (await userManager.FindByEmailAsync(user.Email) == null)
                {
                    var result = await userManager.CreateAsync(user, password);

                    if (result.Succeeded)
                    {
                        await userManager.AddToRoleAsync(user, "User");
                    }
                }
            }
        }

        public static async Task SeedOrganizerAsync(
    UserManager<ApplicationUser> userManager,
    RoleManager<IdentityRole> roleManager)
        {
            var organizers = new List<(ApplicationUser User, string Password)>
    {
        (
            new ApplicationUser
            {
                UserName = "organizer@site.com",
                Email = "organizer@site.com",
                EmailConfirmed = true,
                FirstName = "Айрис",
                MiddleName = "Пейчева",
                LastName = "Пейчева",
                City = "София"
            },
            "Organizer123!" // парола за първия организатор
        ),
        (
            new ApplicationUser
            {
                UserName = "organizer1@site.com",
                Email = "organizer1@site.com",
                EmailConfirmed = true,
                FirstName = "Мария",
                MiddleName = "Иванова",
                LastName = "Стоянова",
                City = "Пловдив"
            },
            "Organizer1123!" // парола за втория организатор
        ),
        (
            new ApplicationUser
            {
                UserName = "organizer2@site.com",
                Email = "organizer2@site.com",
                EmailConfirmed = true,
                FirstName = "Георги",
                MiddleName = "Петров",
                LastName = "Колев",
                City = "Варна"
            },
            "Organizer2123!" // парола за третия организатор
        )
    };

            foreach (var (user, password) in organizers)
            {
                if (await userManager.FindByEmailAsync(user.Email) == null)
                {
                    var result = await userManager.CreateAsync(user, password);

                    if (result.Succeeded)
                    {
                        await userManager.AddToRoleAsync(user, "Organizer");
                    }
                }
            }
        }


    }
}
