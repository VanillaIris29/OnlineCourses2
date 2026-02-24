using OnlineCourses2.Models;

namespace OnlineCourses2.Data.Seeders
{
    public static class CourseSeeder
    {
        public static void SeedCourses(ApplicationDbContext context)
        {
            // Взимаме организаторите
            var organizer1 = context.Users.FirstOrDefault(u => u.Email == "organizer@site.com");
            var organizer2 = context.Users.FirstOrDefault(u => u.Email == "organizer1@site.com");
            var organizer3 = context.Users.FirstOrDefault(u => u.Email == "organizer2@site.com");

            if (organizer1 == null || organizer2 == null || organizer3 == null)
                return;

            // Взимаме категориите по име
            var psychology = context.Categories.FirstOrDefault(c => c.Name == "Психология");
            var marketing = context.Categories.FirstOrDefault(c => c.Name == "Маркетинг");
            var music = context.Categories.FirstOrDefault(c => c.Name == "Музика");
            var personal = context.Categories.FirstOrDefault(c => c.Name == "Личностно развитие");
            var design = context.Categories.FirstOrDefault(c => c.Name == "Дизайн и креативност");

            if (psychology == null || marketing == null || music == null || personal == null || design == null)
                return;

            // -------------------------------------------------------
            // 1) Първи курс – Психология (обновява стария, ако го има)
            // -------------------------------------------------------
            var course1 = context.Courses.FirstOrDefault(c => c.Title == "Въведение в психологията");
            if (course1 == null)
            {
                course1 = new Course();
                context.Courses.Add(course1);
            }

            course1.Title = "Въведение в психологията";
            course1.ShortDescription = "Основи на човешкото поведение.";
            course1.Description = "Когнитивна, социална и личностна психология.";
            course1.DurationHours = 30;
            course1.Price = 180;
            course1.MaxParticipants = 15;
            course1.CurrentParticipants = 0;
            course1.HasCertificate = true;
            course1.StartDate = DateTime.Now.AddDays(10);
            course1.EndDate = DateTime.Now.AddDays(50);
            course1.CategoryId = psychology.Id;
            course1.OrganizerId = organizer1.Id;

            // -------------------------------------------------------
            // 2) Втори курс – Маркетинг (обновява стария, ако го има)
            // -------------------------------------------------------
            var course2 = context.Courses.FirstOrDefault(c => c.Title == "Дигитален маркетинг за начинаещи");
            if (course2 == null)
            {
                course2 = new Course();
                context.Courses.Add(course2);
            }

            course2.Title = "Дигитален маркетинг за начинаещи";
            course2.ShortDescription = "Основи на онлайн рекламата.";
            course2.Description = "Социални мрежи, SEO, Google Ads, съдържание.";
            course2.DurationHours = 25;
            course2.Price = 160;
            course2.MaxParticipants = 12;
            course2.CurrentParticipants = 0;
            course2.HasCertificate = false;
            course2.StartDate = DateTime.Now.AddDays(7);
            course2.EndDate = DateTime.Now.AddDays(27);
            course2.CategoryId = marketing.Id;
            course2.OrganizerId = organizer1.Id;

            // -------------------------------------------------------
            // 3) Трети курс – Музика (оставяме стария ти курс)
            // -------------------------------------------------------
            if (!context.Courses.Any(c => c.Title == "Китара за начинаещи"))
            {
                context.Courses.Add(new Course
                {
                    Title = "Китара за начинаещи",
                    ShortDescription = "Основни акорди и първи песни.",
                    Description = "Акорди, ритъм, свирене на популярни песни.",
                    DurationHours = 24,
                    Price = 150,
                    MaxParticipants = 12,
                    CurrentParticipants = 0,
                    HasCertificate = false,
                    StartDate = DateTime.Now.AddDays(14),
                    EndDate = DateTime.Now.AddDays(44),
                    CategoryId = music.Id,
                    OrganizerId = organizer2.Id
                });
            }

            // -------------------------------------------------------
            // 4) Четвърти курс – Личностно развитие (старият ти курс)
            // -------------------------------------------------------
            if (!context.Courses.Any(c => c.Title == "Ефективен тайм мениджмънт"))
            {
                context.Courses.Add(new Course
                {
                    Title = "Ефективен тайм мениджмънт",
                    ShortDescription = "Как да управляваме времето си по-добре.",
                    Description = "Приоритизиране, цели, навици, продуктивност.",
                    DurationHours = 18,
                    Price = 110,
                    MaxParticipants = 20,
                    CurrentParticipants = 0,
                    HasCertificate = true,
                    StartDate = DateTime.Now.AddDays(5),
                    EndDate = DateTime.Now.AddDays(25),
                    CategoryId = personal.Id,
                    OrganizerId = organizer3.Id
                });
            }

            // -------------------------------------------------------
            // 5) Пети курс – Дизайн (старият ти курс)
            // -------------------------------------------------------
            if (!context.Courses.Any(c => c.Title == "Основи на графичния дизайн"))
            {
                context.Courses.Add(new Course
                {
                    Title = "Основи на графичния дизайн",
                    ShortDescription = "Композиция, цветове и работа с Canva.",
                    Description = "Типография, цветови схеми, банери и маркетинг материали.",
                    DurationHours = 20,
                    Price = 140,
                    MaxParticipants = 15,
                    CurrentParticipants = 0,
                    HasCertificate = true,
                    StartDate = DateTime.Now.AddDays(12),
                    EndDate = DateTime.Now.AddDays(42),
                    CategoryId = design.Id,
                    OrganizerId = organizer3.Id
                });
            }

            context.SaveChanges();
        }
    }
}