using OnlineCourses2.Models;

namespace OnlineCourses2.Data.Seeders
{
    public static class CourseSeeder
    {
        public static void SeedCourses(ApplicationDbContext context)
        {
            var organizer1 = context.Users.FirstOrDefault(u => u.Email == "organizer@site.com");
            var organizer2 = context.Users.FirstOrDefault(u => u.Email == "organizer1@site.com");
            var organizer3 = context.Users.FirstOrDefault(u => u.Email == "organizer2@site.com");

            if (organizer1 == null || organizer2 == null || organizer3 == null)
                return;

            var psychology = context.Categories.FirstOrDefault(c => c.Name == "Психология");
            var marketing = context.Categories.FirstOrDefault(c => c.Name == "Маркетинг");
            var music = context.Categories.FirstOrDefault(c => c.Name == "Музика");
            var personal = context.Categories.FirstOrDefault(c => c.Name == "Личностно развитие");
            var design = context.Categories.FirstOrDefault(c => c.Name == "Дизайн и креативност");

            if (psychology == null || marketing == null || music == null || personal == null || design == null)
                return;

            // COURSE 1
            if (!context.Courses.Any(c => c.Title == "Въведение в психологията"))
            {
                var start = new DateTime(2026, 4, 10);
                int duration = 40;

                context.Courses.Add(new Course
                {
                    Title = "Въведение в психологията",
                    ShortDescription = "Основи на човешкото поведение.",
                    Description = "Когнитивна, социална и личностна психология.",
                    DurationHours = 30,
                    DurationDays = duration,
                    Price = 180,
                    MaxParticipants = 15,
                    CurrentParticipants = 0,
                    HasCertificate = true,
                    StartDate = start,
                    EndDate = start.AddDays(duration),
                    CategoryId = psychology.Id,
                    OrganizerId = organizer1.Id
                });
            }

            // COURSE 2
            if (!context.Courses.Any(c => c.Title == "Дигитален маркетинг за начинаещи"))
            {
                var start = new DateTime(2026, 6, 24);
                int duration = 46;

                context.Courses.Add(new Course
                {
                    Title = "Дигитален маркетинг за начинаещи",
                    ShortDescription = "Основи на онлайн рекламата.",
                    Description = "Социални мрежи, SEO, Google Ads, съдържание.",
                    DurationHours = 29,
                    DurationDays = duration,
                    Price = 180,
                    MaxParticipants = 18,
                    CurrentParticipants = 0,
                    HasCertificate = false,
                    StartDate = start,
                    EndDate = start.AddDays(duration),
                    CategoryId = marketing.Id,
                    OrganizerId = organizer2.Id
                });
            }

            // COURSE 3
            if (!context.Courses.Any(c => c.Title == "Китара за начинаещи"))
            {
                var start = new DateTime(2026, 6, 12);
                int duration = 46;

                context.Courses.Add(new Course
                {
                    Title = "Китара за начинаещи",
                    ShortDescription = "Основни акорди и първи песни.",
                    Description = "Акорди, ритъм, свирене на популярни песни.",
                    DurationHours = 24,
                    DurationDays = duration,
                    Price = 150,
                    MaxParticipants = 12,
                    CurrentParticipants = 0,
                    HasCertificate = false,
                    StartDate = start,
                    EndDate = start.AddDays(duration),
                    CategoryId = music.Id,
                    OrganizerId = organizer2.Id
                });
            }

            // COURSE 4
            if (!context.Courses.Any(c => c.Title == "Ефективен тайм мениджмънт"))
            {
                var start = new DateTime(2026, 5, 29);
                int duration = 26;

                context.Courses.Add(new Course
                {
                    Title = "Ефективен тайм мениджмънт",
                    ShortDescription = "Как да управляваме времето си по-добре.",
                    Description = "Приоритизиране, цели, навици, продуктивност.",
                    DurationHours = 18,
                    DurationDays = duration,
                    Price = 110,
                    MaxParticipants = 20,
                    CurrentParticipants = 0,
                    HasCertificate = true,
                    StartDate = start,
                    EndDate = start.AddDays(duration),
                    CategoryId = personal.Id,
                    OrganizerId = organizer3.Id
                });
            }

            // COURSE 5
            if (!context.Courses.Any(c => c.Title == "Основи на графичния дизайн"))
            {
                var start = new DateTime(2026, 9, 12);
                int duration = 34;

                context.Courses.Add(new Course
                {
                    Title = "Основи на графичния дизайн",
                    ShortDescription = "Композиция, цветове и работа с Canva.",
                    Description = "Типография, цветови схеми, банери и маркетинг материали.",
                    DurationHours = 20,
                    DurationDays = duration,
                    Price = 140,
                    MaxParticipants = 15,
                    CurrentParticipants = 0,
                    HasCertificate = true,
                    StartDate = start,
                    EndDate = start.AddDays(duration),
                    CategoryId = design.Id,
                    OrganizerId = organizer3.Id
                });
            }

            context.SaveChanges();
        }


    }
}