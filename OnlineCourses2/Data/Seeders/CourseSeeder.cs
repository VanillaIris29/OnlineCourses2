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
            var organizer4 = context.Users.FirstOrDefault(u => u.Email == "alex@site.com");
            var organizer5 = context.Users.FirstOrDefault(u => u.Email == "georgi@site.com");
            var organizer6 = context.Users.FirstOrDefault(u => u.Email == "stefan@site.com");
            var organizer7 = context.Users.FirstOrDefault(u => u.Email == "petar@site.com");
            var organizer8 = context.Users.FirstOrDefault(u => u.Email == "kristina@site.com");
            var organizer9 = context.Users.FirstOrDefault(u => u.Email == "snezhana@site.com");
            var organizer10 = context.Users.FirstOrDefault(u => u.Email == "mihaela@site.com");

            if (organizer1 == null || organizer2 == null || organizer3 == null || organizer4 == null || organizer5 == null || organizer6 == null ||
    organizer7 == null || organizer8 == null || organizer9 == null || organizer10 == null) 
                return;

            var psychology = context.Categories.FirstOrDefault(c => c.Name == "Психология");
            var marketing = context.Categories.FirstOrDefault(c => c.Name == "Маркетинг");
            var music = context.Categories.FirstOrDefault(c => c.Name == "Музика");
            var personal = context.Categories.FirstOrDefault(c => c.Name == "Личностно развитие");
            var design = context.Categories.FirstOrDefault(c => c.Name == "Дизайн и креативност");
            var business = context.Categories.FirstOrDefault(c => c.Name == "Бизнес и предприемачество");
            var video = context.Categories.FirstOrDefault(c => c.Name == "Видео обработка и монтаж");
            var makeup = context.Categories.FirstOrDefault(c => c.Name == "Грим");
            var interior = context.Categories.FirstOrDefault(c => c.Name == "Интериорен дизайн");
            var fashion = context.Categories.FirstOrDefault(c => c.Name == "Мода и стил");
            var gamedev = context.Categories.FirstOrDefault(c => c.Name == "Разработка на игри");
            var finance = context.Categories.FirstOrDefault(c => c.Name == "Финанси и инвестиции");

            if (psychology == null || marketing == null || music == null || personal == null || design == null || business == null || video == null || makeup == null || interior == null ||
    fashion == null || gamedev == null || finance == null)
                return;
            // =========================
            //      ПСИХОЛОГИЯ
            // =========================
            //5
            if (!context.Courses.Any(c => c.Title == "Основи на когнитивната психология"))
            {
                var start = new DateTime(2026, 5, 14);
                int duration = 32;

                context.Courses.Add(new Course
                {
                    Title = "Основи на когнитивната психология",
                    ShortDescription = "Разбери как работи човешкият ум и как взимаме решения.",
                    Description = "Курсът разглежда основните когнитивни процеси – внимание, памет, възприятие и мислене. Подходящ е за начинаещи, които искат да разберат как мозъкът обработва информация. Включва практически примери и кратки упражнения.",
                    DurationHours = 28,
                    DurationDays = duration,
                    Price = 160, // EUR
                    MaxParticipants = 5,
                    CurrentParticipants = 0,
                    HasCertificate = true,
                    StartDate = start,
                    EndDate = start.AddDays(duration),
                    CategoryId = psychology.Id,
                    OrganizerId = organizer1.Id
                });
            }
            if (!context.Courses.Any(c => c.Title == "Емоционална интелигентност в ежедневието"))
            {
                var start = new DateTime(2026, 6, 3);
                int duration = 24;

                context.Courses.Add(new Course
                {
                    Title = "Емоционална интелигентност в ежедневието",
                    ShortDescription = "Научи се да разпознаваш и управляваш емоциите си.",
                    Description = "Фокусира се върху самосъзнанието, емпатията и уменията за общуване. Подходящ за хора, които искат да подобрят личните и професионалните си взаимоотношения. Включва ролеви ситуации и практически техники.",
                    DurationHours = 20,
                    DurationDays = duration,
                    Price = 140,
                    MaxParticipants = 15,
                    CurrentParticipants = 0,
                    HasCertificate = false,
                    StartDate = start,
                    EndDate = start.AddDays(duration),
                    CategoryId = psychology.Id,
                    OrganizerId = organizer4.Id
                });
            }
            if (!context.Courses.Any(c => c.Title == "Психология на човешкото поведение"))
            {
                var start = new DateTime(2026, 7, 1);
                int duration = 40;

                context.Courses.Add(new Course
                {
                    Title = "Психология на човешкото поведение",
                    ShortDescription = "Разбери какво движи хората и защо действат по определен начин.",
                    Description = "Курсът разглежда мотивацията, социалното влияние и личностните фактори. Подходящ е за бъдещи психолози, HR специалисти и всеки, който работи с хора. Включва анализ на реални казуси.",
                    DurationHours = 34,
                    DurationDays = duration,
                    Price = 180,
                    MaxParticipants = 20,
                    CurrentParticipants = 0,
                    HasCertificate = true,
                    StartDate = start,
                    EndDate = start.AddDays(duration),
                    CategoryId = psychology.Id,
                    OrganizerId = organizer7.Id
                });
            }
            //6
            if (!context.Courses.Any(c => c.Title == "Стрес и управление на напрежението"))
            {
                var start = new DateTime(2026, 4, 27);
                int duration = 18;

                context.Courses.Add(new Course
                {
                    Title = "Стрес и управление на напрежението",
                    ShortDescription = "Практични техники за справяне със стреса.",
                    Description = "Обучението включва методи за релаксация, дишане, управление на мислите и изграждане на устойчивост. Подходящо за хора с натоварено ежедневие. Включва кратки упражнения и домашни задачи.",
                    DurationHours = 14,
                    DurationDays = duration,
                    Price = 120,
                    MaxParticipants = 6,
                    CurrentParticipants = 0,
                    HasCertificate = false,
                    StartDate = start,
                    EndDate = start.AddDays(duration),
                    CategoryId = psychology.Id,
                    OrganizerId = organizer9.Id
                });
            }
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

            // =========================
            //        МАРКЕТИНГ
            // =========================
            if (!context.Courses.Any(c => c.Title == "Дигитални стратегии за растеж"))
            {
                var start = new DateTime(2026, 5, 22);
                int duration = 28;

                context.Courses.Add(new Course
                {
                    Title = "Дигитални стратегии за растеж",
                    ShortDescription = "Научи как бизнесите увеличават продажбите си онлайн.",
                    Description = "Курсът разглежда ключови дигитални канали, оптимизация на кампании и изграждане на устойчиви маркетинг стратегии. Подходящ е за начинаещи и собственици на малък бизнес. Включва практически задачи и анализ на реални кампании.",
                    DurationHours = 24,
                    DurationDays = duration,
                    Price = 170, // EUR
                    MaxParticipants = 16,
                    CurrentParticipants = 0,
                    HasCertificate = true,
                    StartDate = start,
                    EndDate = start.AddDays(duration),
                    CategoryId = marketing.Id,
                    OrganizerId = organizer2.Id
                });
            }
            //6
            if (!context.Courses.Any(c => c.Title == "Социални мрежи за професионалисти"))
            {
                var start = new DateTime(2026, 6, 11);
                int duration = 20;

                context.Courses.Add(new Course
                {
                    Title = "Социални мрежи за професионалисти",
                    ShortDescription = "Изгради силно присъствие в социалните мрежи.",
                    Description = "Обучението покрива Facebook, Instagram, TikTok и LinkedIn стратегии. Участниците ще научат как да създават съдържание, което привлича внимание и води до реални резултати. Включва практически упражнения и анализ на профили.",
                    DurationHours = 18,
                    DurationDays = duration,
                    Price = 150,
                    MaxParticipants = 6,
                    CurrentParticipants = 0,
                    HasCertificate = false,
                    StartDate = start,
                    EndDate = start.AddDays(duration),
                    CategoryId = marketing.Id,
                    OrganizerId = organizer5.Id
                });
            }
            if (!context.Courses.Any(c => c.Title == "SEO оптимизация за начинаещи"))
            {
                var start = new DateTime(2026, 7, 4);
                int duration = 34;

                context.Courses.Add(new Course
                {
                    Title = "SEO оптимизация за начинаещи",
                    ShortDescription = "Подобри видимостта на сайта си в Google.",
                    Description = "Курсът включва ключови думи, техническо SEO, линк билдинг и оптимизация на съдържание. Подходящ е за собственици на сайтове и маркетинг специалисти. Включва практически задачи и анализ на реални сайтове.",
                    DurationHours = 30,
                    DurationDays = duration,
                    Price = 190,
                    MaxParticipants = 20,
                    CurrentParticipants = 0,
                    HasCertificate = true,
                    StartDate = start,
                    EndDate = start.AddDays(duration),
                    CategoryId = marketing.Id,
                    OrganizerId = organizer8.Id
                });
            }
            if (!context.Courses.Any(c => c.Title == "Google Ads кампании от нулата"))
            {
                var start = new DateTime(2026, 4, 30);
                int duration = 22;

                context.Courses.Add(new Course
                {
                    Title = "Google Ads кампании от нулата",
                    ShortDescription = "Създай ефективни реклами, които носят резултати.",
                    Description = "Обучението покрива настройка на кампании, избор на ключови думи, оптимизация на бюджет и анализ на резултати. Подходящо е за начинаещи и собственици на бизнес. Включва реални примери и упражнения.",
                    DurationHours = 16,
                    DurationDays = duration,
                    Price = 165,
                    MaxParticipants = 12,
                    CurrentParticipants = 0,
                    HasCertificate = false,
                    StartDate = start,
                    EndDate = start.AddDays(duration),
                    CategoryId = marketing.Id,
                    OrganizerId = organizer10.Id
                });
            }
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
            // =========================
            //          МУЗИКА
            // =========================
            if (!context.Courses.Any(c => c.Title == "Акустична китара за начинаещи"))
            {
                var start = new DateTime(2026, 5, 18);
                int duration = 30;

                context.Courses.Add(new Course
                {
                    Title = "Акустична китара за начинаещи",
                    ShortDescription = "Започни да свириш любимите си песни още от първите уроци.",
                    Description = "Курсът покрива основни акорди, ритмика и техники за акомпанимент. Подходящ е за напълно начинаещи, които искат да изградят стабилна основа. Включва упражнения, индивидуална обратна връзка и кратки музикални задачи.",
                    DurationHours = 24,
                    DurationDays = duration,
                    Price = 130, // EUR
                    MaxParticipants = 12,
                    CurrentParticipants = 0,
                    HasCertificate = false,
                    StartDate = start,
                    EndDate = start.AddDays(duration),
                    CategoryId = music.Id,
                    OrganizerId = organizer3.Id
                });
            }
            if (!context.Courses.Any(c => c.Title == "Основи на вокалната техника"))
            {
                var start = new DateTime(2026, 6, 7);
                int duration = 26;

                context.Courses.Add(new Course
                {
                    Title = "Основи на вокалната техника",
                    ShortDescription = "Подобри гласа си и открий своя вокален стил.",
                    Description = "Обучението включва дишане, артикулация, вокален контрол и работа с диапазон. Подходящо е за начинаещи и любители, които искат да пеят по-уверено. Включва индивидуални упражнения и групови изпълнения.",
                    DurationHours = 20,
                    DurationDays = duration,
                    Price = 150,
                    MaxParticipants = 10,
                    CurrentParticipants = 0,
                    HasCertificate = true,
                    StartDate = start,
                    EndDate = start.AddDays(duration),
                    CategoryId = music.Id,
                    OrganizerId = organizer6.Id
                });
            }
            if (!context.Courses.Any(c => c.Title == "Музикална теория за практици"))
            {
                var start = new DateTime(2026, 7, 2);
                int duration = 34;

                context.Courses.Add(new Course
                {
                    Title = "Музикална теория за практици",
                    ShortDescription = "Разбери основите на музиката и подобри свиренето си.",
                    Description = "Курсът разглежда ноти, ритъм, хармония и структура на музикални произведения. Подходящ е за музиканти, които искат да задълбочат знанията си. Включва анализ на песни и практически упражнения.",
                    DurationHours = 28,
                    DurationDays = duration,
                    Price = 170,
                    MaxParticipants = 18,
                    CurrentParticipants = 0,
                    HasCertificate = true,
                    StartDate = start,
                    EndDate = start.AddDays(duration),
                    CategoryId = music.Id,
                    OrganizerId = organizer9.Id
                });
            }
            if (!context.Courses.Any(c => c.Title == "Електронна музика и продукция"))
            {
                var start = new DateTime(2026, 4, 29);
                int duration = 22;

                context.Courses.Add(new Course
                {
                    Title = "Електронна музика и продукция",
                    ShortDescription = "Създай свой първи електронен трак с модерни инструменти.",
                    Description = "Курсът включва работа с DAW софтуер, синтезатори, бийт мейкинг и основи на миксирането. Подходящ е за начинаещи продуценти и любители на електронната музика. Включва практически проекти и финален мини трак.",
                    DurationHours = 18,
                    DurationDays = duration,
                    Price = 190,
                    MaxParticipants = 14,
                    CurrentParticipants = 0,
                    HasCertificate = false,
                    StartDate = start,
                    EndDate = start.AddDays(duration),
                    CategoryId = music.Id,
                    OrganizerId = organizer1.Id
                });
            }
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

            // =========================
            //   ЛИЧНОСТНО РАЗВИТИЕ
            // =========================
            //5
            if (!context.Courses.Any(c => c.Title == "Умения за уверена комуникация"))
            {
                var start = new DateTime(2026, 5, 9);
                int duration = 24;

                context.Courses.Add(new Course
                {
                    Title = "Умения за уверена комуникация",
                    ShortDescription = "Подобри начина, по който говориш и се представяш пред другите.",
                    Description = "Курсът развива умения за ясно изразяване, активното слушане и увереното присъствие. Подходящ е за хора, които искат да подобрят личните и професионалните си взаимоотношения. Включва ролеви игри, упражнения и индивидуална обратна връзка.",
                    DurationHours = 20,
                    DurationDays = duration,
                    Price = 140, // EUR
                    MaxParticipants = 5,
                    CurrentParticipants = 0,
                    HasCertificate = false,
                    StartDate = start,
                    EndDate = start.AddDays(duration),
                    CategoryId = personal.Id,
                    OrganizerId = organizer2.Id
                });
            }
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
            //5
            if (!context.Courses.Any(c => c.Title == "Изграждане на продуктивни навици"))
            {
                var start = new DateTime(2026, 6, 2);
                int duration = 28;

                context.Courses.Add(new Course
                {
                    Title = "Изграждане на продуктивни навици",
                    ShortDescription = "Научи как да създаваш навици, които издържат във времето.",
                    Description = "Обучението разглежда психологията на навиците, мотивацията и устойчивите промени. Подходящо е за хора, които искат да подобрят ежедневието си и да постигат целите си по-лесно. Включва практични техники и седмични задачи.",
                    DurationHours = 22,
                    DurationDays = duration,
                    Price = 155,
                    MaxParticipants = 5,
                    CurrentParticipants = 0,
                    HasCertificate = true,
                    StartDate = start,
                    EndDate = start.AddDays(duration),
                    CategoryId = personal.Id,
                    OrganizerId = organizer7.Id
                });
            }
            if (!context.Courses.Any(c => c.Title == "Самоувереност и лична мотивация"))
            {
                var start = new DateTime(2026, 7, 6);
                int duration = 32;

                context.Courses.Add(new Course
                {
                    Title = "Самоувереност и лична мотивация",
                    ShortDescription = "Открий вътрешната си сила и развий увереност.",
                    Description = "Курсът включва техники за повишаване на самочувствието, работа с вътрешни блокажи и изграждане на позитивно мислене. Подходящ е за хора, които искат да развият стабилна вътрешна мотивация. Включва упражнения, дискусии и лични цели.",
                    DurationHours = 26,
                    DurationDays = duration,
                    Price = 170,
                    MaxParticipants = 14,
                    CurrentParticipants = 0,
                    HasCertificate = true,
                    StartDate = start,
                    EndDate = start.AddDays(duration),
                    CategoryId = personal.Id,
                    OrganizerId = organizer10.Id
                });
            }
            if (!context.Courses.Any(c => c.Title == "Управление на стреса и емоциите"))
            {
                var start = new DateTime(2026, 4, 25);
                int duration = 20;

                context.Courses.Add(new Course
                {
                    Title = "Управление на стреса и емоциите",
                    ShortDescription = "Овладей напрежението и подобри емоционалния си баланс.",
                    Description = "Обучението включва техники за релаксация, дишане, управление на негативните мисли и изграждане на устойчивост. Подходящо е за хора с динамично ежедневие. Включва практични упражнения и кратки домашни задачи.",
                    DurationHours = 16,
                    DurationDays = duration,
                    Price = 135,
                    MaxParticipants = 12,
                    CurrentParticipants = 0,
                    HasCertificate = false,
                    StartDate = start,
                    EndDate = start.AddDays(duration),
                    CategoryId = personal.Id,
                    OrganizerId = organizer4.Id
                });
            }

            // =========================
            //   ДИЗАЙН И КРЕАТИВНОСТ
            // =========================
            if (!context.Courses.Any(c => c.Title == "Основи на графичния дизайн с Canva"))
            {
                var start = new DateTime(2026, 5, 12);
                int duration = 26;

                context.Courses.Add(new Course
                {
                    Title = "Основи на графичния дизайн с Canva",
                    ShortDescription = "Създай модерни дизайни без да имаш опит.",
                    Description = "Курсът покрива композиция, цветови схеми, типография и работа с готови шаблони. Подходящ е за начинаещи, които искат да създават професионални визии за социални мрежи и маркетинг. Включва практически задачи и персонална обратна връзка.",
                    DurationHours = 20,
                    DurationDays = duration,
                    Price = 145, // EUR
                    MaxParticipants = 18,
                    CurrentParticipants = 0,
                    HasCertificate = true,
                    StartDate = start,
                    EndDate = start.AddDays(duration),
                    CategoryId = design.Id,
                    OrganizerId = organizer1.Id
                });
            }
            if (!context.Courses.Any(c => c.Title == "Креативно мислене и визуално изразяване"))
            {
                var start = new DateTime(2026, 6, 4);
                int duration = 22;

                context.Courses.Add(new Course
                {
                    Title = "Креативно мислене и визуално изразяване",
                    ShortDescription = "Развий въображението си и научи как да визуализираш идеи.",
                    Description = "Обучението включва техники за генериране на идеи, визуално разказване и основи на композицията. Подходящо е за творци, маркетолози и всеки, който иска да мисли по-нестандартно. Включва упражнения и мини проекти.",
                    DurationHours = 18,
                    DurationDays = duration,
                    Price = 160,
                    MaxParticipants = 14,
                    CurrentParticipants = 0,
                    HasCertificate = false,
                    StartDate = start,
                    EndDate = start.AddDays(duration),
                    CategoryId = design.Id,
                    OrganizerId = organizer8.Id
                });
            }
            if (!context.Courses.Any(c => c.Title == "Adobe Photoshop за начинаещи"))
            {
                var start = new DateTime(2026, 7, 8);
                int duration = 34;

                context.Courses.Add(new Course
                {
                    Title = "Adobe Photoshop за начинаещи",
                    ShortDescription = "Научи основните инструменти и създай първите си професионални визии.",
                    Description = "Курсът разглежда работа със слоеве, маски, ретуш, цветови корекции и композиция. Подходящ е за хора, които искат да навлязат в професионалния графичен дизайн. Включва практически проекти и анализ на реални примери.",
                    DurationHours = 28,
                    DurationDays = duration,
                    Price = 190,
                    MaxParticipants = 20,
                    CurrentParticipants = 0,
                    HasCertificate = true,
                    StartDate = start,
                    EndDate = start.AddDays(duration),
                    CategoryId = design.Id,
                    OrganizerId = organizer5.Id
                });
            }
            if (!context.Courses.Any(c => c.Title == "Илюстрация и рисуване за начинаещи"))
            {
                var start = new DateTime(2026, 4, 28);
                int duration = 30;

                context.Courses.Add(new Course
                {
                    Title = "Илюстрация и рисуване за начинаещи",
                    ShortDescription = "Открий основите на рисуването и развий своя стил.",
                    Description = "Курсът включва основи на формата, светлината, сенките и композицията. Подходящ е за хора, които искат да започнат да рисуват или да подобрят уменията си. Включва практически упражнения и индивидуална обратна връзка.",
                    DurationHours = 24,
                    DurationDays = duration,
                    Price = 155,
                    MaxParticipants = 12,
                    CurrentParticipants = 0,
                    HasCertificate = false,
                    StartDate = start,
                    EndDate = start.AddDays(duration),
                    CategoryId = design.Id,
                    OrganizerId = organizer9.Id
                });
            }
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

            // =========================
            //  БИЗНЕС И ПРЕДПРИЕМАЧЕСТВО
            // =========================
            if (!context.Courses.Any(c => c.Title == "Стартиране на собствен бизнес"))
            {
                var start = new DateTime(2026, 5, 16);
                int duration = 30;

                context.Courses.Add(new Course
                {
                    Title = "Стартиране на собствен бизнес",
                    ShortDescription = "Научи как да превърнеш идеята си в реален бизнес.",
                    Description = "Курсът разглежда основите на предприемачеството, валидиране на идеи, бизнес модели и първи стъпки към реализация. Подходящ е за начинаещи предприемачи и хора с бизнес идеи. Включва практически задачи и анализ на успешни примери.",
                    DurationHours = 26,
                    DurationDays = duration,
                    Price = 210, // EUR
                    MaxParticipants = 18,
                    CurrentParticipants = 0,
                    HasCertificate = true,
                    StartDate = start,
                    EndDate = start.AddDays(duration),
                    CategoryId = business.Id,
                    OrganizerId = organizer4.Id
                });
            }
            if (!context.Courses.Any(c => c.Title == "Финансово планиране за предприемачи"))
            {
                var start = new DateTime(2026, 6, 9);
                int duration = 24;

                context.Courses.Add(new Course
                {
                    Title = "Финансово планиране за предприемачи",
                    ShortDescription = "Управлявай финансите на бизнеса си уверено и ефективно.",
                    Description = "Обучението включва бюджетиране, прогнози, разходи, приходи и основи на финансовия анализ. Подходящо е за собственици на малък бизнес и стартиращи предприемачи. Включва реални казуси и практични инструменти.",
                    DurationHours = 20,
                    DurationDays = duration,
                    Price = 180,
                    MaxParticipants = 14,
                    CurrentParticipants = 0,
                    HasCertificate = false,
                    StartDate = start,
                    EndDate = start.AddDays(duration),
                    CategoryId = business.Id,
                    OrganizerId = organizer9.Id
                });
            }
            if (!context.Courses.Any(c => c.Title == "Лидерски умения за мениджъри"))
            {
                var start = new DateTime(2026, 7, 3);
                int duration = 36;

                context.Courses.Add(new Course
                {
                    Title = "Лидерски умения за мениджъри",
                    ShortDescription = "Развий лидерски качества и управлявай екипи уверено.",
                    Description = "Курсът разглежда мотивация, делегиране, управление на конфликти и ефективна комуникация. Подходящ е за мениджъри, ръководители на екипи и бъдещи лидери. Включва ролеви игри и анализ на реални ситуации.",
                    DurationHours = 30,
                    DurationDays = duration,
                    Price = 230,
                    MaxParticipants = 20,
                    CurrentParticipants = 0,
                    HasCertificate = true,
                    StartDate = start,
                    EndDate = start.AddDays(duration),
                    CategoryId = business.Id,
                    OrganizerId = organizer1.Id
                });
            }
            if (!context.Courses.Any(c => c.Title == "Продажби и преговори в бизнеса"))
            {
                var start = new DateTime(2026, 4, 27);
                int duration = 22;

                context.Courses.Add(new Course
                {
                    Title = "Продажби и преговори в бизнеса",
                    ShortDescription = "Овладей техниките за успешни продажби и ефективни преговори.",
                    Description = "Обучението включва стратегии за убеждаване, изграждане на доверие и затваряне на сделки. Подходящо е за търговци, предприемачи и хора, които работят с клиенти. Включва практически упражнения и симулации.",
                    DurationHours = 18,
                    DurationDays = duration,
                    Price = 175,
                    MaxParticipants = 12,
                    CurrentParticipants = 0,
                    HasCertificate = false,
                    StartDate = start,
                    EndDate = start.AddDays(duration),
                    CategoryId = business.Id,
                    OrganizerId = organizer7.Id
                });
            }
            // =========================
            //  ВИДЕО ОБРАБОТКА И МОНТАЖ
            // =========================
            if (!context.Courses.Any(c => c.Title == "Основи на видеомонтажа с Adobe Premiere Pro"))
            {
                var start = new DateTime(2026, 5, 20);
                int duration = 28;

                context.Courses.Add(new Course
                {
                    Title = "Основи на видеомонтажа с Adobe Premiere Pro",
                    ShortDescription = "Научи се да монтираш професионални видеа от нулата.",
                    Description = "Курсът покрива основните инструменти, работа с таймлайн, рязане, цветови корекции и аудио обработка. Подходящ е за начинаещи, които искат да създават качествено видео съдържание. Включва практически задачи и работа по реални проекти.",
                    DurationHours = 24,
                    DurationDays = duration,
                    Price = 180, // EUR
                    MaxParticipants = 16,
                    CurrentParticipants = 0,
                    HasCertificate = true,
                    StartDate = start,
                    EndDate = start.AddDays(duration),
                    CategoryId = video.Id,
                    OrganizerId = organizer3.Id
                });
            }
            if (!context.Courses.Any(c => c.Title == "Видео заснемане и работа с камера"))
            {
                var start = new DateTime(2026, 6, 6);
                int duration = 24;

                context.Courses.Add(new Course
                {
                    Title = "Видео заснемане и работа с камера",
                    ShortDescription = "Овладей основите на заснемането и настройките на камерата.",
                    Description = "Обучението включва работа с експонация, фокус, композиция и движение на камерата. Подходящо е за начинаещи видеографи и създатели на съдържание. Включва практически упражнения и заснемане на кратки сцени.",
                    DurationHours = 20,
                    DurationDays = duration,
                    Price = 165,
                    MaxParticipants = 14,
                    CurrentParticipants = 0,
                    HasCertificate = false,
                    StartDate = start,
                    EndDate = start.AddDays(duration),
                    CategoryId = video.Id,
                    OrganizerId = organizer8.Id
                });
            }
            if (!context.Courses.Any(c => c.Title == "Цветови корекции и визуален стил"))
            {
                var start = new DateTime(2026, 7, 10);
                int duration = 32;

                context.Courses.Add(new Course
                {
                    Title = "Цветови корекции и визуален стил",
                    ShortDescription = "Създай професионален визуален стил чрез цветови корекции.",
                    Description = "Курсът разглежда работа с Lumetri Color, цветови профили, контраст, тонове и изграждане на цялостна визия. Подходящ е за видеографи, които искат да подобрят качеството на своите видеа. Включва практически задачи и анализ на примери.",
                    DurationHours = 26,
                    DurationDays = duration,
                    Price = 200,
                    MaxParticipants = 12,
                    CurrentParticipants = 0,
                    HasCertificate = true,
                    StartDate = start,
                    EndDate = start.AddDays(duration),
                    CategoryId = video.Id,
                    OrganizerId = organizer6.Id
                });
            }
            //7
            if (!context.Courses.Any(c => c.Title == "Монтаж за социални мрежи"))
            {
                var start = new DateTime(2026, 4, 30);
                int duration = 20;

                context.Courses.Add(new Course
                {
                    Title = "Монтаж за социални мрежи",
                    ShortDescription = "Създавай кратки и динамични видеа за TikTok, Reels и YouTube.",
                    Description = "Курсът включва техники за бърз монтаж, добавяне на текст, ефекти, музика и оптимизация за различни платформи. Подходящ е за създатели на съдържание и маркетинг специалисти. Включва практически мини проекти.",
                    DurationHours = 16,
                    DurationDays = duration,
                    Price = 150,
                    MaxParticipants = 7,
                    CurrentParticipants = 0,
                    HasCertificate = false,
                    StartDate = start,
                    EndDate = start.AddDays(duration),
                    CategoryId = video.Id,
                    OrganizerId = organizer10.Id
                });
            }

            // =========================
            //          ГРИМ
            // =========================
            //5
            if (!context.Courses.Any(c => c.Title == "Дневен грим за начинаещи"))
            {
                var start = new DateTime(2026, 5, 14);
                int duration = 20;

                context.Courses.Add(new Course
                {
                    Title = "Дневен грим за начинаещи",
                    ShortDescription = "Научи как да създаваш естествен и свеж дневен грим.",
                    Description = "Курсът включва основи на подготовката на кожата, избор на подходящи продукти и техники за лек и естествен грим. Подходящ е за начинаещи, които искат да изглеждат добре в ежедневието си. Включва демонстрации и практическа работа.",
                    DurationHours = 16,
                    DurationDays = duration,
                    Price = 120, // EUR
                    MaxParticipants = 5,
                    CurrentParticipants = 0,
                    HasCertificate = false,
                    StartDate = start,
                    EndDate = start.AddDays(duration),
                    CategoryId = makeup.Id,
                    OrganizerId = organizer5.Id
                });
            }
            if (!context.Courses.Any(c => c.Title == "Професионален вечерен грим"))
            {
                var start = new DateTime(2026, 6, 8);
                int duration = 26;

                context.Courses.Add(new Course
                {
                    Title = "Професионален вечерен грим",
                    ShortDescription = "Овладей техники за впечатляващ вечерен и парти грим.",
                    Description = "Обучението включва контуриране, работа с пигменти, опушен грим и техники за дълготрайност. Подходящо е за любители и начинаещи гримьори. Включва индивидуална практика и професионални съвети.",
                    DurationHours = 20,
                    DurationDays = duration,
                    Price = 160,
                    MaxParticipants = 14,
                    CurrentParticipants = 0,
                    HasCertificate = true,
                    StartDate = start,
                    EndDate = start.AddDays(duration),
                    CategoryId = makeup.Id,
                    OrganizerId = organizer9.Id
                });
            }
            if (!context.Courses.Any(c => c.Title == "Грим за фотосесии и камера"))
            {
                var start = new DateTime(2026, 7, 5);
                int duration = 30;

                context.Courses.Add(new Course
                {
                    Title = "Грим за фотосесии и камера",
                    ShortDescription = "Научи как да създаваш грим, който изглежда перфектно на снимка.",
                    Description = "Курсът разглежда техники за работа със светлина, текстури и продукти, които стоят добре пред камера. Подходящ е за бъдещи гримьори и създатели на съдържание. Включва работа с модели и мини фотосесии.",
                    DurationHours = 24,
                    DurationDays = duration,
                    Price = 190,
                    MaxParticipants = 10,
                    CurrentParticipants = 0,
                    HasCertificate = true,
                    StartDate = start,
                    EndDate = start.AddDays(duration),
                    CategoryId = makeup.Id,
                    OrganizerId = organizer2.Id
                });
            }
            if (!context.Courses.Any(c => c.Title == "Корекции и техники за перфектна кожа"))
            {
                var start = new DateTime(2026, 4, 29);
                int duration = 18;

                context.Courses.Add(new Course
                {
                    Title = "Корекции и техники за перфектна кожа",
                    ShortDescription = "Овладей корекции, прикриване и изравняване на тена.",
                    Description = "Обучението включва работа с коректори, фон дьо тен, техники за прикриване на несъвършенства и изграждане на естествен финиш. Подходящо е за начинаещи и любители. Включва демонстрации и практическа работа.",
                    DurationHours = 14,
                    DurationDays = duration,
                    Price = 135,
                    MaxParticipants = 16,
                    CurrentParticipants = 0,
                    HasCertificate = false,
                    StartDate = start,
                    EndDate = start.AddDays(duration),
                    CategoryId = makeup.Id,
                    OrganizerId = organizer7.Id
                });
            }

            // =========================
            //     ИНТЕРИОРЕН ДИЗАЙН
            // =========================
            if (!context.Courses.Any(c => c.Title == "Основи на интериорния дизайн"))
            {
                var start = new DateTime(2026, 5, 17);
                int duration = 28;

                context.Courses.Add(new Course
                {
                    Title = "Основи на интериорния дизайн",
                    ShortDescription = "Научи основните принципи за създаване на красиви и функционални пространства.",
                    Description = "Курсът разглежда композиция, цветови схеми, материали и основни дизайнерски концепции. Подходящ е за начинаещи, които искат да навлязат в света на интериорния дизайн. Включва практически задачи и анализ на реални интериори.",
                    DurationHours = 24,
                    DurationDays = duration,
                    Price = 180, // EUR
                    MaxParticipants = 18,
                    CurrentParticipants = 0,
                    HasCertificate = true,
                    StartDate = start,
                    EndDate = start.AddDays(duration),
                    CategoryId = interior.Id,
                    OrganizerId = organizer1.Id
                });
            }
            if (!context.Courses.Any(c => c.Title == "Декорация и стил за дома"))
            {
                var start = new DateTime(2026, 6, 10);
                int duration = 22;

                context.Courses.Add(new Course
                {
                    Title = "Декорация и стил за дома",
                    ShortDescription = "Открий как да преобразиш дома си с малки, но ефективни промени.",
                    Description = "Обучението включва избор на декорации, текстили, осветление и акценти, които създават уют и стил. Подходящо е за любители и начинаещи декоратори. Включва практически примери и мини проекти.",
                    DurationHours = 18,
                    DurationDays = duration,
                    Price = 150,
                    MaxParticipants = 14,
                    CurrentParticipants = 0,
                    HasCertificate = false,
                    StartDate = start,
                    EndDate = start.AddDays(duration),
                    CategoryId = interior.Id,
                    OrganizerId = organizer6.Id
                });
            }
            if (!context.Courses.Any(c => c.Title == "3D моделиране за интериорен дизайн"))
            {
                var start = new DateTime(2026, 7, 7);
                int duration = 36;

                context.Courses.Add(new Course
                {
                    Title = "3D моделиране за интериорен дизайн",
                    ShortDescription = "Научи се да създаваш професионални 3D визуализации.",
                    Description = "Курсът включва работа със софтуер като SketchUp и Blender, моделиране на мебели, текстури и осветление. Подходящ е за бъдещи интериорни дизайнери и визуализатори. Включва практически проекти и финална 3D визуализация.",
                    DurationHours = 30,
                    DurationDays = duration,
                    Price = 220,
                    MaxParticipants = 12,
                    CurrentParticipants = 0,
                    HasCertificate = true,
                    StartDate = start,
                    EndDate = start.AddDays(duration),
                    CategoryId = interior.Id,
                    OrganizerId = organizer10.Id
                });
            }
            if (!context.Courses.Any(c => c.Title == "Минималистичен дизайн и функционални пространства"))
            {
                var start = new DateTime(2026, 4, 26);
                int duration = 24;

                context.Courses.Add(new Course
                {
                    Title = "Минималистичен дизайн и функционални пространства",
                    ShortDescription = "Овладей принципите на минимализма и създай хармонични интериори.",
                    Description = "Курсът разглежда концепции за подредба, функционалност, избор на материали и цветове, които създават спокойна и модерна атмосфера. Подходящ е за любители и начинаещи дизайнери. Включва практически упражнения и анализ на примери.",
                    DurationHours = 20,
                    DurationDays = duration,
                    Price = 165,
                    MaxParticipants = 16,
                    CurrentParticipants = 0,
                    HasCertificate = false,
                    StartDate = start,
                    EndDate = start.AddDays(duration),
                    CategoryId = interior.Id,
                    OrganizerId = organizer8.Id
                });
            }
            // =========================
            //       МОДА И СТИЛ
            // =========================
            if (!context.Courses.Any(c => c.Title == "Основи на личния стил"))
            {
                var start = new DateTime(2026, 5, 19);
                int duration = 24;

                context.Courses.Add(new Course
                {
                    Title = "Основи на личния стил",
                    ShortDescription = "Открий своя стил и научи как да се обличаш уверено.",
                    Description = "Курсът разглежда основни стилове, цветови комбинации, силуети и избор на дрехи според фигурата. Подходящ е за хора, които искат да подобрят визията си и да изградят личен стил. Включва практически задачи и анализ на гардероб.",
                    DurationHours = 20,
                    DurationDays = duration,
                    Price = 150, // EUR
                    MaxParticipants = 16,
                    CurrentParticipants = 0,
                    HasCertificate = false,
                    StartDate = start,
                    EndDate = start.AddDays(duration),
                    CategoryId = fashion.Id,
                    OrganizerId = organizer3.Id
                });
            }
            if (!context.Courses.Any(c => c.Title == "Модни тенденции и комбиниране на облекло"))
            {
                var start = new DateTime(2026, 6, 12);
                int duration = 22;

                context.Courses.Add(new Course
                {
                    Title = "Модни тенденции и комбиниране на облекло",
                    ShortDescription = "Научи как да съчетаваш дрехи и аксесоари като професионалист.",
                    Description = "Обучението включва анализ на актуални модни тенденции, избор на цветове, текстури и аксесоари. Подходящо е за любители на модата и хора, които искат да изглеждат модерно. Включва практически упражнения и мини стилизации.",
                    DurationHours = 18,
                    DurationDays = duration,
                    Price = 140,
                    MaxParticipants = 12,
                    CurrentParticipants = 0,
                    HasCertificate = false,
                    StartDate = start,
                    EndDate = start.AddDays(duration),
                    CategoryId = fashion.Id,
                    OrganizerId = organizer8.Id
                });
            }
            if (!context.Courses.Any(c => c.Title == "Стайлинг за фотосесии и събития"))
            {
                var start = new DateTime(2026, 7, 9);
                int duration = 30;

                context.Courses.Add(new Course
                {
                    Title = "Стайлинг за фотосесии и събития",
                    ShortDescription = "Създай професионални визии за фотосесии и специални поводи.",
                    Description = "Курсът включва избор на облекло според концепция, работа с модели, цветови решения и подготовка за фотосесии. Подходящ е за стилисти, фотографи и любители на модата. Включва практически задачи и работа по реални проекти.",
                    DurationHours = 24,
                    DurationDays = duration,
                    Price = 190,
                    MaxParticipants = 14,
                    CurrentParticipants = 0,
                    HasCertificate = true,
                    StartDate = start,
                    EndDate = start.AddDays(duration),
                    CategoryId = fashion.Id,
                    OrganizerId = organizer10.Id
                });
            }
            if (!context.Courses.Any(c => c.Title == "Капсулен гардероб и минимализъм"))
            {
                var start = new DateTime(2026, 4, 30);
                int duration = 20;

                context.Courses.Add(new Course
                {
                    Title = "Капсулен гардероб и минимализъм",
                    ShortDescription = "Научи как да създадеш практичен и стилен гардероб.",
                    Description = "Обучението разглежда принципите на минимализма, избор на универсални дрехи и изграждане на функционален гардероб. Подходящо е за хора, които искат да намалят хаоса и да изглеждат добре с по-малко усилия. Включва анализ на личен стил и практични примери.",
                    DurationHours = 16,
                    DurationDays = duration,
                    Price = 135,
                    MaxParticipants = 18,
                    CurrentParticipants = 0,
                    HasCertificate = false,
                    StartDate = start,
                    EndDate = start.AddDays(duration),
                    CategoryId = fashion.Id,
                    OrganizerId = organizer6.Id
                });
            }

            // =========================
            //    РАЗРАБОТКА НА ИГРИ
            // =========================
            if (!context.Courses.Any(c => c.Title == "Основи на гейм дизайна"))
            {
                var start = new DateTime(2026, 5, 21);
                int duration = 28;

                context.Courses.Add(new Course
                {
                    Title = "Основи на гейм дизайна",
                    ShortDescription = "Научи как се създават забавни и ангажиращи игри.",
                    Description = "Курсът разглежда основните принципи на гейм дизайна – механики, динамики, баланс и игрови цикъл. Подходящ е за начинаещи, които искат да навлязат в света на игрите. Включва анализ на популярни игри и практически мини проекти.",
                    DurationHours = 24,
                    DurationDays = duration,
                    Price = 180, // EUR
                    MaxParticipants = 18,
                    CurrentParticipants = 0,
                    HasCertificate = true,
                    StartDate = start,
                    EndDate = start.AddDays(duration),
                    CategoryId = gamedev.Id,
                    OrganizerId = organizer4.Id
                });
            }
            if (!context.Courses.Any(c => c.Title == "Unity за начинаещи"))
            {
                var start = new DateTime(2026, 6, 9);
                int duration = 34;

                context.Courses.Add(new Course
                {
                    Title = "Unity за начинаещи",
                    ShortDescription = "Създай първата си 2D или 3D игра с Unity.",
                    Description = "Обучението включва работа със сцената, обекти, физика, скриптове и основи на C#. Подходящо е за хора без опит, които искат да започнат да разработват игри. Включва практически задачи и финален мини проект.",
                    DurationHours = 28,
                    DurationDays = duration,
                    Price = 210,
                    MaxParticipants = 16,
                    CurrentParticipants = 0,
                    HasCertificate = true,
                    StartDate = start,
                    EndDate = start.AddDays(duration),
                    CategoryId = gamedev.Id,
                    OrganizerId = organizer7.Id
                });
            }
            if (!context.Courses.Any(c => c.Title == "Pixel Art и 2D графика за игри"))
            {
                var start = new DateTime(2026, 7, 4);
                int duration = 26;

                context.Courses.Add(new Course
                {
                    Title = "Pixel Art и 2D графика за игри",
                    ShortDescription = "Научи се да създаваш красиви 2D персонажи и среди.",
                    Description = "Курсът включва основи на пиксел арт, работа с цветове, анимации и създаване на игрови спрайтове. Подходящ е за художници и начинаещи разработчици. Включва практически задачи и създаване на собствен набор от ресурси.",
                    DurationHours = 20,
                    DurationDays = duration,
                    Price = 160,
                    MaxParticipants = 12,
                    CurrentParticipants = 0,
                    HasCertificate = false,
                    StartDate = start,
                    EndDate = start.AddDays(duration),
                    CategoryId = gamedev.Id,
                    OrganizerId = organizer2.Id
                });
            }
            if (!context.Courses.Any(c => c.Title == "Level Design и изграждане на игрови светове"))
            {
                var start = new DateTime(2026, 4, 28);
                int duration = 30;

                context.Courses.Add(new Course
                {
                    Title = "Level Design и изграждане на игрови светове",
                    ShortDescription = "Създай интересни и добре структурирани игрови нива.",
                    Description = "Курсът разглежда композиция на нивата, ритъм, трудност, навигация и визуални ориентири. Подходящ е за бъдещи гейм дизайнери и разработчици. Включва практически задачи и изграждане на собствено ниво.",
                    DurationHours = 24,
                    DurationDays = duration,
                    Price = 190,
                    MaxParticipants = 20,
                    CurrentParticipants = 0,
                    HasCertificate = true,
                    StartDate = start,
                    EndDate = start.AddDays(duration),
                    CategoryId = gamedev.Id,
                    OrganizerId = organizer9.Id
                });
            }
            // =========================
            //   ФИНАНСИ И ИНВЕСТИЦИИ
            // =========================

            // FIN 1
            if (!context.Courses.Any(c => c.Title == "Лични финанси и управление на бюджет"))
            {
                var start = new DateTime(2026, 5, 23);
                int duration = 24;

                context.Courses.Add(new Course
                {
                    Title = "Лични финанси и управление на бюджет",
                    ShortDescription = "Научи как да управляваш парите си умно и ефективно.",
                    Description = "Курсът разглежда основите на личните финанси, бюджетиране, контрол на разходите и изграждане на финансови навици. Подходящ е за хора, които искат да подобрят финансовата си стабилност. Включва практични упражнения и реални примери.",
                    DurationHours = 20,
                    DurationDays = duration,
                    Price = 150, // EUR
                    MaxParticipants = 18,
                    CurrentParticipants = 0,
                    HasCertificate = false,
                    StartDate = start,
                    EndDate = start.AddDays(duration),
                    CategoryId = finance.Id,
                    OrganizerId = organizer6.Id
                });
            }

            // FIN 2
            if (!context.Courses.Any(c => c.Title == "Инвестиране за начинаещи"))
            {
                var start = new DateTime(2026, 6, 14);
                int duration = 30;

                context.Courses.Add(new Course
                {
                    Title = "Инвестиране за начинаещи",
                    ShortDescription = "Започни да инвестираш уверено и информирано.",
                    Description = "Обучението включва основи на инвестирането, видове активи, риск, доходност и изграждане на портфолио. Подходящо е за хора без опит, които искат да започнат да инвестират. Включва анализи, симулации и практически задачи.",
                    DurationHours = 24,
                    DurationDays = duration,
                    Price = 190,
                    MaxParticipants = 16,
                    CurrentParticipants = 0,
                    HasCertificate = true,
                    StartDate = start,
                    EndDate = start.AddDays(duration),
                    CategoryId = finance.Id,
                    OrganizerId = organizer10.Id
                });
            }

            // FIN 3
            if (!context.Courses.Any(c => c.Title == "Криптовалути и блокчейн основи"))
            {
                var start = new DateTime(2026, 7, 8);
                int duration = 26;

                context.Courses.Add(new Course
                {
                    Title = "Криптовалути и блокчейн основи",
                    ShortDescription = "Разбери как работят криптовалутите и блокчейн технологиите.",
                    Description = "Курсът разглежда основните криптовалути, принципите на блокчейн, сигурност и потенциални рискове. Подходящ е за хора, които искат да навлязат в света на дигиталните активи. Включва практически примери и анализ на пазара.",
                    DurationHours = 20,
                    DurationDays = duration,
                    Price = 175,
                    MaxParticipants = 12,
                    CurrentParticipants = 0,
                    HasCertificate = false,
                    StartDate = start,
                    EndDate = start.AddDays(duration),
                    CategoryId = finance.Id,
                    OrganizerId = organizer3.Id
                });
            }

            // FIN 4
            if (!context.Courses.Any(c => c.Title == "Финансов анализ и оценка на компании"))
            {
                var start = new DateTime(2026, 4, 27);
                int duration = 34;

                context.Courses.Add(new Course
                {
                    Title = "Финансов анализ и оценка на компании",
                    ShortDescription = "Научи как професионалистите оценяват стойността на бизнеса.",
                    Description = "Курсът включва анализ на финансови отчети, ключови показатели, оценка на активи и методи за определяне на стойност. Подходящ е за бъдещи инвеститори, предприемачи и финансисти. Включва реални казуси и практически упражнения.",
                    DurationHours = 28,
                    DurationDays = duration,
                    Price = 220,
                    MaxParticipants = 20,
                    CurrentParticipants = 0,
                    HasCertificate = true,
                    StartDate = start,
                    EndDate = start.AddDays(duration),
                    CategoryId = finance.Id,
                    OrganizerId = organizer8.Id
                });
            }
            context.SaveChanges();
        }


    }
}