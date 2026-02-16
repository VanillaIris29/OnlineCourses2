using OnlineCourses2.Models;
using System.ComponentModel.DataAnnotations;
namespace OnlineCourses2.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [MinLength(2)]
        public string FirstName { get; set; } = "";

        public string? MiddleName { get; set; }

        [Required]
        [MinLength(2)]
        public string LastName { get; set; } = "";

        [Range(14, 100)]
        public int Age { get; set; }

        [Required]
        public string City { get; set; } = "";

        [Required]
        public string Country { get; set; } = "България";

    }
}
