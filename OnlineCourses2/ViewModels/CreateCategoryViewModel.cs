using OnlineCourses2.Models;
using System.ComponentModel.DataAnnotations;

namespace OnlineCourses2.ViewModels
{
    public class CreateCategoryViewModel
    {
        [Required]
        [MinLength(3)]
        public string Name { get; set; } = "";
        public List<Category> Categories { get; set; } = new();
    }


}

