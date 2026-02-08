using System.ComponentModel.DataAnnotations;

namespace OnlineCourses2.ViewModels
{
    public class CreateCategoryViewModel
    {
        [Required]
        [MinLength(3)]
        public string Name { get; set; } = "";

    }
}
