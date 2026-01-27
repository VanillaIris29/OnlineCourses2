using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using OnlineCourses2.Models;

namespace OnlineCourses2.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<ApplicationUser> ApplicationUsers => Set<ApplicationUser>();
        public DbSet<Category> Categories => Set<Category>();
        public DbSet<Course> Courses => Set<Course>();
        public DbSet<UserCourse> UserCourses => Set<UserCourse>();

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }
    }
}
