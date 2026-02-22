using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OnlineCourses2.ViewModels
{
    public class AdminUserEditViewModel
    {
        public string Id { get; set; } = "";
        [Required]
        public string FirstName { get; set; } = "";
        public string MiddleName { get; set; } = "";
        [Required]
        public string LastName { get; set; } = "";

        public string City { get; set; } = "";
        public string Country { get; set; } = "";
        [Required]
        [Range(14, 100)]
        public int Age { get; set; }
        [Required]
        public string Email { get; set; } = "";
        [Required]
        public string SelectedRole { get; set; } = "";
        public List<string> AvailableRoles { get; set; } = new();

    }
}
