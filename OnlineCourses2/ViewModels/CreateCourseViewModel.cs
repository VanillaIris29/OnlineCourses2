using OnlineCourses2.Models;

namespace OnlineCourses2.ViewModels
{
    public class CreateCourseViewModel
    {
            public string Title { get; set; } = "";
        public string ShortDescription { get; set; } = "";
            public string? Description { get; set; }
            public int DurationHours { get; set; }
            public decimal Price { get; set; }
            public int MaxParticipants { get; set; }
        public bool HasCertificate { get; set; }
        public DateTime StartDate { get; set; } = DateTime.Today;
        public DateTime EndDate { get; set; } = DateTime.Today.AddDays(30);

        public string CategoryId { get; set; } = "";
        public List<Category> Categories { get; set; } = new();
        public IFormFile? ImageFile { get; set; }


    }


}
