using System.ComponentModel.DataAnnotations;

namespace OnlineCourses2.Models
{
    public class Category
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Required]
        [MinLength(3)]
        public string Name { get; set; } = null!;

        public ICollection<Course>? Courses { get; set; }

    }
}

