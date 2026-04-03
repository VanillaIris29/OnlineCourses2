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
                    MiddleName = "Пейчева",
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
        ), ( new ApplicationUser { UserName = "ivan@site.com", Email = "ivan@site.com", EmailConfirmed = true, FirstName = "Иван", MiddleName = "Георгиев", LastName = "Иванов", City = "София", Country = "България", Age = 28 }, "Ivan123!" ),
( new ApplicationUser { UserName = "dimitar@site.com", Email = "dimitar@site.com", EmailConfirmed = true, FirstName = "Димитър", MiddleName = "Петров", LastName = "Димитров", City = "Пловдив", Country = "България", Age = 34 }, "Dimitar123!" ),
( new ApplicationUser { UserName = "nikolay@site.com", Email = "nikolay@site.com", EmailConfirmed = true, FirstName = "Николай", MiddleName = "Стефанов", LastName = "Николов", City = "Варна", Country = "България", Age = 41 }, "Nikolay123!" ),
( new ApplicationUser { UserName = "hristo@site.com", Email = "hristo@site.com", EmailConfirmed = true, FirstName = "Христо", MiddleName = "Маринов", LastName = "Христов", City = "Добрич", Country = "България", Age = 26 }, "Hristo123!" ),
( new ApplicationUser { UserName = "martin@site.com", Email = "martin@site.com", EmailConfirmed = true, FirstName = "Мартин", MiddleName = "Стоянов", LastName = "Мартинов", City = "Шумен", Country = "България", Age = 31 }, "Martin123!" ),
( new ApplicationUser { UserName = "boris@site.com", Email = "boris@site.com", EmailConfirmed = true, FirstName = "Борис", MiddleName = "Красимиров", LastName = "Борисов", City = "Перник", Country = "България", Age = 29 }, "Boris123!" ),
( new ApplicationUser { UserName = "kaloyan@site.com", Email = "kaloyan@site.com", EmailConfirmed = true, FirstName = "Калоян", MiddleName = "Ивайлов", LastName = "Колев", City = "Хасково", Country = "България", Age = 24 }, "Kaloyan123!" ),
( new ApplicationUser { UserName = "daniel@site.com", Email = "daniel@site.com", EmailConfirmed = true, FirstName = "Даниел", MiddleName = "Руменов", LastName = "Даниелов", City = "Ямбол", Country = "България", Age = 33 }, "Daniel123!" ),
( new ApplicationUser { UserName = "viktor@site.com", Email = "viktor@site.com", EmailConfirmed = true, FirstName = "Виктор", MiddleName = "Здравков", LastName = "Викторов", City = "Пазарджик", Country = "България", Age = 40 }, "Viktor123!" ),
( new ApplicationUser { UserName = "kiril@site.com", Email = "kiril@site.com", EmailConfirmed = true, FirstName = "Кирил", MiddleName = "Йорданов", LastName = "Кирилов", City = "Благоевград", Country = "България", Age = 52 }, "Kiril123!" ),
( new ApplicationUser { UserName = "filip@site.com", Email = "filip@site.com", EmailConfirmed = true, FirstName = "Филип", MiddleName = "Любомиров", LastName = "Филипов", City = "Велико Търново", Country = "България", Age = 19 }, "Filip123!" ),
( new ApplicationUser { UserName = "mariya@site.com", Email = "mariya@site.com", EmailConfirmed = true, FirstName = "Мария", MiddleName = "Иванова", LastName = "Маринова", City = "София", Country = "България", Age = 25 }, "Mariya123!" ),
( new ApplicationUser { UserName = "elena@site.com", Email = "elena@site.com", EmailConfirmed = true, FirstName = "Елена", MiddleName = "Петрова", LastName = "Димитрова", City = "Пловдив", Country = "България", Age = 30 }, "Elena123!" ),
( new ApplicationUser { UserName = "desi@site.com", Email = "desi@site.com", EmailConfirmed = true, FirstName = "Десислава", MiddleName = "Георгиева", LastName = "Николова", City = "Варна", Country = "България", Age = 27 }, "Desi123!" ),
( new ApplicationUser { UserName = "viktoriya@site.com", Email = "viktoriya@site.com", EmailConfirmed = true, FirstName = "Виктория", MiddleName = "Николаева", LastName = "Александрова", City = "Бургас", Country = "България", Age = 32 }, "Viktoriya123!" ),
( new ApplicationUser { UserName = "radoslava@site.com", Email = "radoslava@site.com", EmailConfirmed = true, FirstName = "Радослава", MiddleName = "Стефанова", LastName = "Георгиева", City = "Русе", Country = "България", Age = 36 }, "Radoslava123!" ),
( new ApplicationUser { UserName = "anna@site.com", Email = "anna@site.com", EmailConfirmed = true, FirstName = "Анна", MiddleName = "Тодорова", LastName = "Стефанова", City = "Стара Загора", Country = "България", Age = 21 }, "Anna123!" ),
( new ApplicationUser { UserName = "silviya@site.com", Email = "silviya@site.com", EmailConfirmed = true, FirstName = "Силвия", MiddleName = "Василева", LastName = "Петрова", City = "Плевен", Country = "България", Age = 44 }, "Silviya123!" ),
( new ApplicationUser { UserName = "gergana@site.com", Email = "gergana@site.com", EmailConfirmed = true, FirstName = "Гергана", MiddleName = "Драганова", LastName = "Христова", City = "Сливен", Country = "България", Age = 29 }, "Gergana123!" ),
( new ApplicationUser { UserName = "yoana@site.com", Email = "yoana@site.com", EmailConfirmed = true, FirstName = "Йоана", MiddleName = "Ангелова", LastName = "Мартинова", City = "Добрич", Country = "България", Age = 23 }, "Yoana123!" ),
( new ApplicationUser { UserName = "teodora@site.com", Email = "teodora@site.com", EmailConfirmed = true, FirstName = "Теодора", MiddleName = "Ивайлова", LastName = "Здравкова", City = "Ямбол", Country = "България", Age = 31 }, "Teodora123!" ),
( new ApplicationUser { UserName = "daniela@site.com", Email = "daniela@site.com", EmailConfirmed = true, FirstName = "Даниела", MiddleName = "Руменова", LastName = "Кирилова", City = "Габрово", Country = "България", Age = 28 }, "Daniela123!" ),
( new ApplicationUser { UserName = "kalina@site.com", Email = "kalina@site.com", EmailConfirmed = true, FirstName = "Калина", MiddleName = "Здравкова", LastName = "Филипова", City = "Враца", Country = "България", Age = 35 }, "Kalina123!" )
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
        ),
( new ApplicationUser { UserName = "alex@site.com", Email = "alex@site.com", EmailConfirmed = true, FirstName = "Александър", MiddleName = "Тодоров", LastName = "Александров", City = "Бургас", Country = "България", Age = 22 }, "Alex123!" ),
( new ApplicationUser { UserName = "georgi@site.com", Email = "georgi@site.com", EmailConfirmed = true, FirstName = "Георги", MiddleName = "Василев", LastName = "Георгиев", City = "Стара Загора", Country = "България", Age = 30 }, "Georgi123!" ),
( new ApplicationUser { UserName = "stefan@site.com", Email = "stefan@site.com", EmailConfirmed = true, FirstName = "Стефан", MiddleName = "Драганов", LastName = "Стефанов", City = "Плевен", Country = "България", Age = 45 }, "Stefan123!" ),
( new ApplicationUser { UserName = "petar@site.com", Email = "petar@site.com", EmailConfirmed = true, FirstName = "Петър", MiddleName = "Ангелов", LastName = "Петров", City = "Сливен", Country = "България", Age = 37 }, "Petar123!" ),
( new ApplicationUser { UserName = "kristina@site.com", Email = "kristina@site.com", EmailConfirmed = true, FirstName = "Кристина", MiddleName = "Маринова", LastName = "Борисова", City = "Шумен", Country = "България", Age = 38 }, "Kristina123!" ),
( new ApplicationUser { UserName = "snezhana@site.com", Email = "snezhana@site.com", EmailConfirmed = true, FirstName = "Снежана", MiddleName = "Стоянова", LastName = "Колева", City = "Перник", Country = "България", Age = 49 }, "Snezhana123!" ),
( new ApplicationUser { UserName = "mihaela@site.com", Email = "mihaela@site.com", EmailConfirmed = true, FirstName = "Михаела", MiddleName = "Красимирова", LastName = "Руменова", City = "Хасково", Country = "България", Age = 20 }, "Mihaela123!" )
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
