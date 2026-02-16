namespace OnlineCourses2.Models
{
    public class Enrollment
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public string UserId { get; set; } = null!;
        public ApplicationUser User { get; set; } = null!;

        public string CourseId { get; set; } = null!;
        public Course Course { get; set; } = null!;

        public DateTime EnrolledOn { get; set; } = DateTime.UtcNow;

    }
}
