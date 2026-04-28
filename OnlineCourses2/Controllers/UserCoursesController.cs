using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineCourses2.Data;
using OnlineCourses2.Models;
using OnlineCourses2.ViewModels;

namespace OnlineCourses2.Controllers
{
    /// <summary>
    /// Controller responsible for displaying, filtering, and managing course
    /// interactions for regular users, including browsing, viewing details,
    /// enrolling, removing enrollments, and listing personal courses.
    /// </summary>
    public class UserCoursesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public UserCoursesController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        /// <summary>
        /// Displays the default UserCourses index page.
        /// </summary>
        /// <returns>Returns the Index view.</returns>
        public IActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// Displays detailed information about a specific course,
        /// including whether the current user is enrolled in it.
        /// </summary>
        /// <param name="id">The ID of the course to display.</param>
        /// <returns>
        /// Returns the course details view,
        /// NotFound if the course does not exist.
        /// </returns>
        public async Task<IActionResult> Details(string id)
        {
            if (id == null) return NotFound();

            var course = await _context.Courses
                .Include(c => c.Category)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (course == null) return NotFound();
            var userId = _userManager.GetUserId(User);

            bool isEnrolled = await _context.Enrollments
                .AnyAsync(e => e.CourseId == id && e.UserId == userId);

            ViewBag.IsEnrolled = isEnrolled;

            return View(course);
        }
        /// <summary>
        /// Displays all active courses with search, filtering, sorting,
        /// and pagination. Accessible to anonymous users.
        /// </summary>
        /// <param name="search">Optional search term for course title or category name.</param>
        /// <param name="categoryId">Optional category filter.</param>
        /// <param name="sort">Sorting option (name_asc, price_low, hours_high, etc.).</param>
        /// <param name="certificate">Filter by certificate: "yes", "no", or null.</param>
        /// <param name="page">Current page number for pagination (1-based).</param>
        /// <returns>Returns a paginated list of active courses.</returns>
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

            if (!string.IsNullOrWhiteSpace(search))
            {
                courses = courses.Where(c =>
                    c.Title.Contains(search) ||
                    c.Category.Name.Contains(search));
            }

            if (!string.IsNullOrWhiteSpace(categoryId))
            {
                courses = courses.Where(c => c.CategoryId == categoryId);
            }

            if (certificate == "yes")
                courses = courses.Where(c => c.HasCertificate);
            else if (certificate == "no")
                courses = courses.Where(c => !c.HasCertificate);

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

            int totalItems = await courses.CountAsync();
            int totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

            var pagedCourses = await courses
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = totalPages;

            ViewBag.Search = search;
            ViewBag.CategoryId = categoryId;
            ViewBag.Sort = sort;
            ViewBag.Certificate = certificate;

            ViewBag.PaginationAction = "All";
            ViewBag.PaginationController = "UserCourses";

            ViewBag.Categories = await _context.Categories.ToListAsync();

            return View(pagedCourses);
        }
        /// <summary>
        /// Enrolls the currently logged-in user into a course,
        /// if the course exists, has available seats, and the user is not already enrolled.
        /// </summary>
        /// <param name="id">The ID of the course to enroll in.</param>
        /// <returns>
        /// Redirects to the course details page with success or error messages.
        /// Returns Forbid if the user is not in the User role.
        /// </returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Enroll(string id)
        {
            if (!User.IsInRole("User"))
            {
                return Forbid();
            }

            var userId = _userManager.GetUserId(User);

            var course = await _context.Courses
                .FirstOrDefaultAsync(c => c.Id == id);

            if (course == null)
            {
                TempData["Error"] = "Курсът не беше намерен.";
                return RedirectToAction("All");
            }

            if (course.CurrentParticipants >= course.MaxParticipants)
            {
                TempData["Error"] = "Курсът е пълен.";
                return RedirectToAction("Details", new { id });
            }

            bool already = await _context.Enrollments
                .AnyAsync(e => e.CourseId == id && e.UserId == userId);

            if (already)
            {
                TempData["Error"] = "Вече сте записани.";
                return RedirectToAction("Details", new { id });
            }

            _context.Enrollments.Add(new Enrollment
            {
                CourseId = id,
                UserId = userId
            });

            course.CurrentParticipants++;
            await _context.SaveChangesAsync();

            TempData["Success"] = "Успешно записване!";
            return RedirectToAction("Details", new { id });
        }
        /// <summary>
        /// Removes a user from a course. Accessible to Organizers and Admins.
        /// </summary>
        /// <param name="courseId">The ID of the course.</param>
        /// <param name="userId">The ID of the user to remove from the course.</param>
        /// <returns>
        /// Redirects to the Course Participants page after removal,
        /// or NotFound if the enrollment or course does not exist.
        /// </returns>
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
        /// <summary>
        /// Displays all courses in which the current user is enrolled,
        /// with search, filtering, sorting, and pagination.
        /// </summary>
        /// <param name="search">Optional search term for course title or category name.</param>
        /// <param name="categoryId">Optional category filter.</param>
        /// <param name="sort">Sorting option (name_asc, price_high, days_low, etc.).</param>
        /// <param name="certificate">Filter by certificate: "yes", "no", or null.</param>
        /// <param name="page">Current page number for pagination (1-based).</param>
        /// <returns>
        /// Returns a paginated list of courses the user is enrolled in.
        /// </returns>
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

            if (!string.IsNullOrWhiteSpace(search))
            {
                courses = courses.Where(c =>
                    c.Title.Contains(search) ||
                    c.Category.Name.Contains(search));
            }

            if (!string.IsNullOrWhiteSpace(categoryId))
            {
                courses = courses.Where(c => c.CategoryId == categoryId);
            }

            if (certificate == "yes")
                courses = courses.Where(c => c.HasCertificate);
            else if (certificate == "no")
                courses = courses.Where(c => !c.HasCertificate);

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

            int totalItems = await courses.CountAsync();
            int totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

            var pagedCourses = await courses
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = totalPages;

            ViewBag.Search = search;
            ViewBag.CategoryId = categoryId;
            ViewBag.Sort = sort;
            ViewBag.Certificate = certificate;

            ViewBag.PaginationAction = "MyCourses";
            ViewBag.PaginationController = "UserCourses";
            
            ViewBag.Categories = await _context.Categories.ToListAsync();

            return View(pagedCourses);
        }



    }
}
