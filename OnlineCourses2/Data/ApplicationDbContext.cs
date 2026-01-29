using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using OnlineCourses2.Models;

namespace OnlineCourses2.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<UserCourse> UserCourses { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<UserCourse>()
                .HasKey(uc => new { uc.UserId, uc.CourseId });
            builder.Entity<Course>()
      .HasOne(c => c.Organizer)
      .WithMany(u => u.CreatedCourses)
      .HasForeignKey(c => c.OrganizerId)
      .OnDelete(DeleteBehavior.Restrict);
            builder.Entity<Course>()
                    .Property(c => c.Price)
                    .HasPrecision(10, 2);

        }
        

    }
    
}

