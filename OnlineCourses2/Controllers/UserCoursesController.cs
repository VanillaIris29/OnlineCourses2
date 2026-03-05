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

            // 1) Намираме курса
            var course = await _context.Courses
                .FirstOrDefaultAsync(c => c.Id == id);

            if (course == null)
            {
                TempData["Error"] = "Курсът не беше намерен.";
                return RedirectToAction("All");
            }

            // 2) Проверка дали курсът е пълен
            if (course.CurrentParticipants >= course.MaxParticipants)
            {
                TempData["Error"] = "Курсът е пълен.";
                return RedirectToAction("Details", new { id });
            }

            // 3) Проверка дали потребителят вече е записан
            bool already = await _context.Enrollments
                .AnyAsync(e => e.CourseId == id && e.UserId == userId);

            if (already)
            {
                TempData["Error"] = "Вече сте записани за този курс.";
                return RedirectToAction("Details", new { id });
            }

            // 4) Записване
            var enroll = new Enrollment
            {
                CourseId = id,
                UserId = userId
            };

            _context.Enrollments.Add(enroll);

            // 5) Увеличаваме броя на записаните
            course.CurrentParticipants++;
            _context.Courses.Update(course);

            await _context.SaveChangesAsync();

            TempData["Success"] = "Успешно се записахте за курса!";
            return RedirectToAction("Details", new { id });
        }
        
        [HttpPost]
        [Authorize(Roles = "Organizer,Admin")]
        public async Task<IActionResult> Remove(string courseId, string userId)
        {
            var enrollment = await _context.Enrollments
                .FirstOrDefaultAsync(e => e.CourseId == courseId && e.UserId == userId);

            if (enrollment == null)
                return NotFound();

            var course = await _context.Courses.FirstOrDefaultAsync(c => c.Id == courseId);

            if (course == null)
                return NotFound();

            if (course.CurrentParticipants > 0)
                course.CurrentParticipants--;

            _context.Enrollments.Remove(enrollment);
            _context.Courses.Update(course);

            await _context.SaveChangesAsync();

            TempData["Success"] = "Потребителят беше премахнат от курса.";

            return RedirectToAction("Participants", "Course", new { id = courseId });
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
