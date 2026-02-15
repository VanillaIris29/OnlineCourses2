using OnlineCourses2.Models;

namespace OnlineCourses2.ViewModels
{
    public class EditCourseViewModel
    {
        public string Id { get; set; } = "";

        public string Title { get; set; } = "";
        public string ShortDescription { get; set; } = "";
        public string? Description { get; set; } = "";
        public int DurationHours { get; set; }
        public decimal Price { get; set; }
        public int MaxParticipants { get; set; }
        public bool HasCertificate { get; set; }
        public string CategoryId { get; set; } = "";
        public List<Category> Categories { get; set; } = new();

        public IFormFile? ImageFile { get; set; }
        public string? ExistingImagePath { get; set; }

    }
}
