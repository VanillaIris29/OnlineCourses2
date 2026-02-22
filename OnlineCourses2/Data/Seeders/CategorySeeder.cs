using OnlineCourses2.Models;
namespace OnlineCourses2.Data.Seeders
{
    public static class CategorySeeder
    {
        public static void SeedCategories(ApplicationDbContext context)
        {
            var categories = new List<string>
        {
            "Психология",
            "Маркетинг",
            "Музика",
            "Личностно развитие",
            "Дизайн и креативност"
        };

            foreach (var name in categories)
            {
                if (!context.Categories.Any(c => c.Name == name))
                {
                    context.Categories.Add(new Category { Name = name });
                }
            }

            context.SaveChanges();
        }


    }
}
