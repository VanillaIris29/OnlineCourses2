using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineCourses2.Data;
using OnlineCourses2.Models;
using OnlineCourses2.ViewModels;
using Microsoft.EntityFrameworkCore;

[Authorize(Roles = "Organizer,Admin")]
public class CourseController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;

    public CourseController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    [HttpGet]
    public async Task<IActionResult> Create()
    {
        var vm = new CreateCourseViewModel
        {
            Categories = await _context.Categories.ToListAsync()
        };

        return View(vm);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateCourseViewModel model)
    {
        if (!ModelState.IsValid)
        {
            model.Categories = await _context.Categories.ToListAsync();
            return View(model);
        }

        var organizer = await _userManager.GetUserAsync(User);

        var course = new Course
        {
            Title = model.Title,
            ShortDescription = model.ShortDescription,
            Description = model.Description,
            DurationHours = model.DurationHours,
            Price = model.Price,
            MaxParticipants = model.MaxParticipants,
            CategoryId = model.CategoryId,
            OrganizerId = organizer.Id,
            CurrentParticipants = 0
        };

        _context.Courses.Add(course);
        await _context.SaveChangesAsync();

        return RedirectToAction("MyCourses");
    }
    [HttpGet]
    public async Task<IActionResult> MyCourses()
    {
        var user = await _userManager.GetUserAsync(User);

        var courses = await _context.Courses
            .Where(c => c.OrganizerId == user.Id)
            .Include(c => c.Category)
            .ToListAsync();

        return View(courses);
    }


}

