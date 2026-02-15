using System.ComponentModel.DataAnnotations;

namespace OnlineCourses2.Models
{
    public class Course
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Required]
        [MinLength(3)]
        public string Title { get; set; } = null!;

        [Required]
        [MinLength(10)]
        public string ShortDescription { get; set; } = null!;

        public string? Description { get; set; }

        [Range(1, 1000)]
        public int DurationHours { get; set; }

        [Range(0, 9999)]
        public decimal Price { get; set; }

        [Range(10, 20)]
        public int MaxParticipants { get; set; }

        public int CurrentParticipants { get; set; }

        public string? ImagePath { get; set; }
        public bool HasCertificate { get; set; }

        // Category
        [Required]
        public string CategoryId { get; set; } = null!;
        public Category? Category { get; set; }

        // Organizer
        [Required]
        public string OrganizerId { get; set; } = null!;
        public ApplicationUser? Organizer { get; set; }

        public ICollection<UserCourse>? UserCourses { get; set; }

    }
}

