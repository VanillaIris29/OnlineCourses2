using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineCourses2.Data;
using OnlineCourses2.Models;
using OnlineCourses2.ViewModels;

namespace OnlineCourses2.Controllers
{
    public class UserCoursesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public UserCoursesController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> Details(string id)
        {
            if (id == null) return NotFound();

            var course = await _context.Courses
                .Include(c => c.Category)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (course == null) return NotFound();

            // Проверка дали потребителят е записан
            var userId = _userManager.GetUserId(User);

            bool isEnrolled = await _context.Enrollments
                .AnyAsync(e => e.CourseId == id && e.UserId == userId);

            ViewBag.IsEnrolled = isEnrolled;

            return View(course);
        }

        public async Task<IActionResult> All()
        {
            var courses = await _context.Courses
                .Include(c => c.Category)
                .ToListAsync();

            return View(courses);
        }
        [HttpPost]
        public async Task<IActionResult> Enroll(string id)
        {
            var userId = _userManager.GetUserId(User);

            bool already = await _context.Enrollments
                .AnyAsync(e => e.CourseId == id && e.UserId == userId);

            if (!already)
            {
                var enroll = new Enrollment
                {
                    CourseId = id,
                    UserId = userId
                };

                _context.Enrollments.Add(enroll);
                await _context.SaveChangesAsync();
            }

            TempData["Success"] = "Успешно се записахте за курса!";
            return RedirectToAction("Details", new { id });
        }
        public async Task<IActionResult> MyCourses()
        {
            var userId = _userManager.GetUserId(User);

            var courses = await _context.Enrollments
                .Where(e => e.UserId == userId)
                .Include(e => e.Course)
                .ThenInclude(c => c.Category)
                .Select(e => e.Course)
                .ToListAsync();

            return View(courses);
        }


    }
}
