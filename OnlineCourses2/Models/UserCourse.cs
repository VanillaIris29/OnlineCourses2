namespace OnlineCourses2.Models
{
    public class UserCourse
    {
        public string UserId { get; set; } = null!;
        public ApplicationUser User { get; set; } = null!;

        public string CourseId { get; set; } = null!;
        public Course Course { get; set; } = null!;

        public DateTime EnrolledOn { get; set; } = DateTime.UtcNow;

    }
}
