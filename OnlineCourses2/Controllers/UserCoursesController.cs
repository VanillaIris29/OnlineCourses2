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

        [AllowAnonymous]
        public async Task<IActionResult> All(
     string search,
     string categoryId,
     string sort,
     string certificate,
     int page = 1)
        {
            int pageSize = 12;

            var courses = _context.Courses
                .Include(c => c.Category)
                .Where(c => c.EndDate >= DateTime.Today)
                .AsQueryable();

            // 🔍 Search
            if (!string.IsNullOrWhiteSpace(search))
            {
                courses = courses.Where(c =>
                    c.Title.Contains(search) ||
                    c.Category.Name.Contains(search));
            }

            // 📂 Category
            if (!string.IsNullOrWhiteSpace(categoryId))
            {
                courses = courses.Where(c => c.CategoryId == categoryId);
            }

            // 🎓 Certificate
            if (certificate == "yes")
                courses = courses.Where(c => c.HasCertificate);
            else if (certificate == "no")
                courses = courses.Where(c => !c.HasCertificate);

            // 🔽 Sorting
            courses = sort switch
            {
                "name_asc" => courses.OrderBy(c => c.Title),
                "name_desc" => courses.OrderByDescending(c => c.Title),

                "price_low" => courses.OrderBy(c => c.Price),
                "price_high" => courses.OrderByDescending(c => c.Price),

                "days_low" => courses.OrderBy(c => c.DurationDays),
                "days_high" => courses.OrderByDescending(c => c.DurationDays),

                "hours_low" => courses.OrderBy(c => c.DurationHours),
                "hours_high" => courses.OrderByDescending(c => c.DurationHours),

                _ => courses
            };

            // 📄 Pagination
            int totalItems = await courses.CountAsync();
            int totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

            var pagedCourses = await courses
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            // 📦 ViewBag за универсалния pagination
            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = totalPages;

            ViewBag.Search = search;
            ViewBag.CategoryId = categoryId;
            ViewBag.Sort = sort;
            ViewBag.Certificate = certificate;

            ViewBag.PaginationAction = "All";
            ViewBag.PaginationController = "Course";

            ViewBag.Categories = await _context.Categories.ToListAsync();

            return View(pagedCourses);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
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
            if (course.CurrentParticipants >= course.MaxParticipants)
            {
                TempData["Error"] = "Курсът е вече пълен.";
                return RedirectToAction("Details", new { id = course.Id });
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

        [HttpGet]
        public async Task<IActionResult> MyCourses(
    string search,
    string categoryId,
    string sort,
    string certificate,
    int page = 1)
        {
            int pageSize = 12;

            var userId = _userManager.GetUserId(User);

            var courses = _context.Enrollments
                .Where(e => e.UserId == userId)
                .Include(e => e.Course)
                .ThenInclude(c => c.Category)
                .Select(e => e.Course)
                .AsQueryable();

            // 🔍 Search
            if (!string.IsNullOrWhiteSpace(search))
            {
                courses = courses.Where(c =>
                    c.Title.Contains(search) ||
                    c.Category.Name.Contains(search));
            }

            // 📂 Category
            if (!string.IsNullOrWhiteSpace(categoryId))
            {
                courses = courses.Where(c => c.CategoryId == categoryId);
            }

            // 🎓 Certificate
            if (certificate == "yes")
                courses = courses.Where(c => c.HasCertificate);
            else if (certificate == "no")
                courses = courses.Where(c => !c.HasCertificate);

            // 🔽 Sorting
            courses = sort switch
            {
                "name_asc" => courses.OrderBy(c => c.Title),
                "name_desc" => courses.OrderByDescending(c => c.Title),

                "price_low" => courses.OrderBy(c => c.Price),
                "price_high" => courses.OrderByDescending(c => c.Price),

                "days_low" => courses.OrderBy(c => c.DurationDays),
                "days_high" => courses.OrderByDescending(c => c.DurationDays),

                "hours_low" => courses.OrderBy(c => c.DurationHours),
                "hours_high" => courses.OrderByDescending(c => c.DurationHours),

                _ => courses
            };

            // 📄 Pagination
            int totalItems = await courses.CountAsync();
            int totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

            var pagedCourses = await courses
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            // 📦 ViewBag за универсалния pagination
            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = totalPages;

            ViewBag.Search = search;
            ViewBag.CategoryId = categoryId;
            ViewBag.Sort = sort;
            ViewBag.Certificate = certificate;

            ViewBag.PaginationAction = "MyCourses";
            ViewBag.PaginationController = "Course";

            ViewBag.Categories = await _context.Categories.ToListAsync();

            return View(pagedCourses);
        }



    }
}
