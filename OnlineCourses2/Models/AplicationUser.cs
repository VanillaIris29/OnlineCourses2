using Microsoft.AspNetCore.Identity;

namespace OnlineCourses2.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }

        public ICollection<UserCourse>? UserCourses { get; set; }
    }
}

