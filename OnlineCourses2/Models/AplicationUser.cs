using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace OnlineCourses2.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [MinLength(2)]
        public string FirstName { get; set; } = null!;

        public string? MiddleName { get; set; }

        [Required]
        [MinLength(2)]
        public string LastName { get; set; } = null!;

        [Range(14, 100)]
        public int Age { get; set; }

        [Required]
        public string? City { get; set; }

        [Required]
        public string Country { get; set; } = "България";

        public string? ProfileImagePath { get; set; }

        public ICollection<UserCourse>? UserCourses { get; set; }
        public ICollection<Course>? CreatedCourses { get; set; }

    }
}

