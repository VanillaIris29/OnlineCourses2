using System.ComponentModel.DataAnnotations;

namespace OnlineCourses2.Models
{
    public class Course
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Required]
        [MinLength(3)]
        public string Title { get; set; } = null!;

        public string? Description { get; set; }

        [Range(1, int.MaxValue)]
        public int LessonsCount { get; set; }

        public bool IsFull { get; set; }

        // Category
        [Required]
        public string CategoryId { get; set; } = null!;
        public Category? Category { get; set; }

        // Organizer (User)
        public string? OrganizerId { get; set; }
        public ApplicationUser? Organizer { get; set; }

        public ICollection<UserCourse>? UserCourses { get; set; }
    }
}

