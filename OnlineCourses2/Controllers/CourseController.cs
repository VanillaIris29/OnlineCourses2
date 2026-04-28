using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineCourses2.Data;
using OnlineCourses2.Models;
using OnlineCourses2.ViewModels;
using System.Security.Claims;

namespace OnlineCourses2.Controllers
{
    /// <summary>
    /// Controller responsible for managing courses in the OnlineCourses2 platform.
    /// Provides functionality for creating, editing, listing, filtering, sorting,
    /// and deleting courses, as well as managing participants.
    /// </summary>
    public class CourseController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public CourseController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        /// <summary>
        /// Displays all active courses for Admin users with search, filtering,
        /// sorting, and pagination support.
        /// </summary>
        /// <param name="search">Optional search term for course title or category name.</param>
        /// <param name="categoryId">Optional category ID to filter courses by category.</param>
        /// <param name="sort">Sorting option (e.g. name_asc, price_low, days_high).</param>
        /// <param name="certificate">Filter by certificate: "yes", "no" or null for all.</param>
        /// <param name="page">Current page number for pagination (1-based).</param>
        /// <returns>Returns a view with a paged list of active courses.</returns>
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ManageAll(
    string search,
    string categoryId,
    string sort,
    string certificate,
    int page = 1)
        {
            int pageSize = 12;

            var courses = _context.Courses
                .Include(c => c.Category)
                .Include(c => c.Organizer)
                .Where(c => c.EndDate >= DateTime.Now)
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

            ViewBag.PaginationAction = "ManageAll";
            ViewBag.PaginationController = "Course";

            ViewBag.Categories = await _context.Categories.ToListAsync();

            return View(pagedCourses);
        }
        /// <summary>
        /// Displays the Create Course form and loads all available categories.
        /// </summary>
        /// <returns>Returns a view with an empty CreateCourseViewModel.</returns>
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var vm = new CreateCourseViewModel
            {
                Categories = await _context.Categories.ToListAsync()

            };


            return View(vm);
        }
        /// <summary>
        /// Creates a new course with validation, image upload,
        /// and automatic EndDate calculation based on StartDate and DurationDays.
        /// </summary>
        /// <param name="model">The view model containing course data and uploaded image.</param>
        /// <returns>
        /// Redirects to ManageAll (Admin) or Manage (Organizer) on success,
        /// or returns the Create view with validation errors.
        /// </returns>
        [HttpPost]
        public async Task<IActionResult> Create(CreateCourseViewModel model)
        {

            if (model.DurationHours < 8 || model.DurationHours > 30)
                ModelState.AddModelError("DurationHours", "Продължителността трябва да е между 8 и 30 часа.");

            if (model.Price <= 0)
                ModelState.AddModelError("Price", "Цената трябва да е по-голяма от 0.");

            if (model.MaxParticipants < 10 || model.MaxParticipants > 20)
                ModelState.AddModelError("MaxParticipants", "Броят участници трябва да е между 10 и 20.");

            if (model.DurationDays <= 0)
                ModelState.AddModelError("DurationDays", "Продължителността трябва да е поне 1 ден.");

            if (!ModelState.IsValid)
            {
                model.Categories = await _context.Categories.ToListAsync();
                return View(model);
            }
            var organizer = await _userManager.GetUserAsync(User);

            string? imagePath = null;

            if (model.ImageFile != null)
            {
                string folder = Path.Combine("wwwroot", "images", "courses");

                if (!Directory.Exists(folder))
                    Directory.CreateDirectory(folder);

                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(model.ImageFile.FileName);
                string filePath = Path.Combine(folder, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await model.ImageFile.CopyToAsync(stream);
                }

                imagePath = "/images/courses/" + fileName;
            }


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
                CurrentParticipants = 0,
                ImagePath = imagePath,
                HasCertificate = model.HasCertificate,
                DurationDays = model.DurationDays,
                StartDate = model.StartDate,
                EndDate = model.StartDate.AddDays(model.DurationDays)


            };

            _context.Courses.Add(course);
            await _context.SaveChangesAsync();
            TempData["Success"] = "Курсът беше създаден успешно!";
            if (User.IsInRole("Admin"))
            {
                return RedirectToAction("ManageAll");
            }
            return RedirectToAction("Manage");
        }
        /// <summary>
        /// Public listing of all active courses with search, filtering,
        /// sorting, and pagination. Accessible anonymously.
        /// </summary>
        /// <param name="search">Optional search term for course title or category name.</param>
        /// <param name="categoryId">Optional category ID to filter courses.</param>
        /// <param name="sort">Sorting option (e.g. name_asc, price_low, hours_high).</param>
        /// <param name="certificate">Filter by certificate: "yes", "no" or null for all.</param>
        /// <param name="page">Current page number for pagination (1-based).</param>
        /// <returns>Returns a view with a paged list of active courses.</returns>
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
                .Include(c => c.Enrollments)
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
            ViewBag.PaginationController = "Course";

            ViewBag.Categories = await _context.Categories.ToListAsync();

            return View(pagedCourses);
        } /// <summary>
          /// Displays only expired courses for Admin and Organizer roles.
          /// </summary>
          /// <returns>Returns a view with a list of expired courses.</returns>
        [Authorize(Roles = "Admin,Organizer")]
        public async Task<IActionResult> Expired()
        {
            var courses = await _context.Courses
                .Include(c => c.Category)
                .Where(c => c.EndDate < DateTime.Today)
                .ToListAsync();

            return View(courses);
        }
        /// <summary>
        /// Loads the Edit Course form with existing course data.
        /// </summary>
        /// <param name="id">The ID of the course to edit.</param>
        /// <returns>
        /// Returns a view with an EditCourseViewModel if found,
        /// or NotFound if the course does not exist.
        /// </returns>
        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            var course = await _context.Courses
                .Include(c => c.Category)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (course == null)
                return NotFound();

            var vm = new EditCourseViewModel
            {
                Id = course.Id,
                Title = course.Title,
                ShortDescription = course.ShortDescription,
                Description = course.Description,
                DurationHours = course.DurationHours,
                DurationDays = course.DurationDays,
                Price = course.Price,
                MaxParticipants = course.MaxParticipants,
                CategoryId = course.CategoryId,
                ExistingImagePath = course.ImagePath,
                HasCertificate = course.HasCertificate,
                StartDate = course.StartDate,
                EndDate = course.EndDate,
                Categories = await _context.Categories.ToListAsync()
            };

            return View(vm);
        }
        /// <summary>
        /// Updates course information, recalculates EndDate,
        /// and optionally replaces the course image.
        /// </summary>
        /// <param name="model">The view model containing updated course data.</param>
        /// <returns>
        /// Redirects to ManageAll (Admin) or Manage (Organizer) on success,
        /// or returns the Edit view with validation errors.
        /// </returns>
        [HttpPost]
        public async Task<IActionResult> Edit(EditCourseViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Categories = await _context.Categories.ToListAsync();
                return View(model);
            }

            var course = await _context.Courses.FindAsync(model.Id);

            if (course == null)
                return NotFound();

            course.Title = model.Title;
            course.ShortDescription = model.ShortDescription;
            course.Description = model.Description;
            course.DurationHours = model.DurationHours;
            course.DurationDays = model.DurationDays;
            course.Price = model.Price;
            course.MaxParticipants = model.MaxParticipants;
            course.CategoryId = model.CategoryId;
            course.HasCertificate = model.HasCertificate;
            course.StartDate = model.StartDate;

            course.EndDate = model.StartDate.AddDays(model.DurationDays);

            if (model.ImageFile != null)
            {
                string folder = Path.Combine("wwwroot", "images", "courses");

                if (!Directory.Exists(folder))
                    Directory.CreateDirectory(folder);

                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(model.ImageFile.FileName);
                string filePath = Path.Combine(folder, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await model.ImageFile.CopyToAsync(stream);
                }

                course.ImagePath = "/images/courses/" + fileName;
            }

            await _context.SaveChangesAsync();

            TempData["Success"] = "Курсът беше редактиран успешно!";

            if (User.IsInRole("Admin"))
                return RedirectToAction("ManageAll");

            return RedirectToAction("Manage");
        }
        /// <summary>
        /// Displays detailed information about a single course.
        /// </summary>
        /// <param name="id">The ID of the course to display.</param>
        /// <returns>
        /// Returns a view with the course details,
        /// or NotFound if the course does not exist.
        /// </returns>
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
                return NotFound();

            var course = await _context.Courses
                .Include(c => c.Category)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (course == null)
                return NotFound();

            return View(course);
        }
        /// <summary>
        /// Organizer view: lists only the organizer’s active courses
        /// with search, filtering, sorting, and pagination.
        /// </summary>
        /// <param name="search">Optional search term for course title or category name.</param>
        /// <param name="categoryId">Optional category ID to filter courses.</param>
        /// <param name="sort">Sorting option (e.g. name_asc, price_high).</param>
        /// <param name="certificate">Filter by certificate: "yes", "no" or null for all.</param>
        /// <param name="page">Current page number for pagination (1-based).</param>
        /// <returns>Returns a view with a paged list of the organizer’s active courses.</returns>
        [HttpGet]
        [Authorize(Roles = "Admin,Organizer")]
        public async Task<IActionResult> Manage(
    string search,
    string categoryId,
    string sort,
    string certificate,
    int page = 1)
        {
            int pageSize = 12;

            var user = await _userManager.GetUserAsync(User);

            var courses = _context.Courses
                .Where(c => c.OrganizerId == user.Id)
                .Where(c => c.EndDate >= DateTime.Today)
                .Include(c => c.Category)
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

            ViewBag.PaginationAction = "Manage";
            ViewBag.PaginationController = "Course";

            ViewBag.Categories = await _context.Categories.ToListAsync();

            return View(pagedCourses);
        }

        /// <summary>
        /// Deletes a course by ID.
        /// </summary>
        /// <param name="id">The ID of the course to delete.</param>
        /// <returns>
        /// Redirects to ManageAll for Admin or Manage for Organizer,
        /// or returns NotFound if the course does not exist.
        /// </returns>
        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            var course = await _context.Courses.FindAsync(id);

            if (course == null)
                return NotFound();

            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();
            if (User.IsInRole("Admin"))
            {
                return RedirectToAction("ManageAll");
            }

            return RedirectToAction("Manage");
        }
        /// <summary>
        /// Displays participants enrolled in a specific course.
        /// Accessible to Admin or the course Organizer only.
        /// </summary>
        /// <param name="id">The ID of the course whose participants are listed.</param>
        /// <returns>
        /// Returns a view with the course and its enrollments,
        /// NotFound if the course does not exist,
        /// or Forbid if the organizer is not the owner.
        /// </returns>
        [HttpGet("Course/Participants/{id}")]
        [Authorize(Roles = "Organizer,Admin")]
        public async Task<IActionResult> Participants(string id)
        {
            var course = await _context.Courses
                .Include(c => c.Enrollments)
                .ThenInclude(e => e.User)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (course == null)
                return NotFound();

            if (User.IsInRole("Organizer") && course.OrganizerId != _userManager.GetUserId(User))
                return Forbid();

            return View(course);
        }
        /// <summary>
        /// Organizer view: lists expired courses with search, filtering, and sorting.
        /// </summary>
        /// <param name="search">Optional search term for course title or category name.</param>
        /// <param name="categoryId">Optional category ID to filter courses.</param>
        /// <param name="sort">Sorting option (e.g. name_asc, days_low).</param>
        /// <param name="certificate">Filter by certificate: "yes", "no" or null for all.</param>
        /// <returns>Returns a view with a list of the organizer’s expired courses.</returns>
        [Authorize(Roles = "Organizer")]
        public async Task<IActionResult> ManageExpired(
      string search,
      string categoryId,
      string sort,
      string certificate)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var courses = _context.Courses
                .Where(c => c.OrganizerId == userId)
                .Where(c => c.EndDate < DateTime.Now)
                .Include(c => c.Category)
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

            ViewBag.Search = search;
            ViewBag.CategoryId = categoryId;
            ViewBag.Sort = sort;
            ViewBag.Certificate = certificate;
            ViewBag.Categories = await _context.Categories.ToListAsync();

            return View(await courses.ToListAsync());
        }
        /// <summary>
        /// Deletes an expired course and all its enrollments.
        /// </summary>
        /// <param name="id">The ID of the expired course to delete.</param>
        /// <returns>
        /// Redirects to ManageExpired after successful deletion,
        /// or returns NotFound if the course does not exist.
        /// </returns>
        [Authorize(Roles = "Organizer,Admin")]
        [HttpPost]
        public async Task<IActionResult> DeleteExpiredCourse(string id)
        {
            var course = await _context.Courses
                .Include(c => c.Enrollments)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (course == null)
                return NotFound();

            if (course.Enrollments.Any())
                _context.Enrollments.RemoveRange(course.Enrollments);

            _context.Courses.Remove(course);

            await _context.SaveChangesAsync();

            TempData["Success"] = "Курсът беше изтрит успешно.";

            return RedirectToAction("ManageExpired");
        }
        /// <summary>
        /// Shows detailed information about a specific participant in a course.
        /// Accessible to Admin or the course Organizer only.
        /// </summary>
        /// <param name="courseId">The ID of the course.</param>
        /// <param name="userId">The ID of the participant (user).</param>
        /// <returns>
        /// Returns a view with participant details,
        /// NotFound if the course or participant is not found,
        /// or Forbid if the current user has no access.
        /// </returns>
        public async Task<IActionResult> ParticipantDetails(string courseId, string userId)
        {
            if (courseId == null || userId == null)
                return NotFound();

            var currentUserId = _userManager.GetUserId(User);

            var course = await _context.Courses
                .Where(c => c.Id == courseId)
                .Select(c => new
                {
                    c.Id,
                    c.OrganizerId
                })
                .FirstOrDefaultAsync();

            if (course == null)
                return NotFound();

            if (!User.IsInRole("Admin") && course.OrganizerId != currentUserId)
                return Forbid();

            var participant = await _context.Enrollments
                .Where(e => e.CourseId == courseId && e.UserId == userId)
                .Select(e => new AdminUserDetailsViewModel
                {
                    Id = e.User.Id,
                    FirstName = e.User.FirstName,
                    MiddleName = e.User.MiddleName,
                    LastName = e.User.LastName,
                    City = e.User.City,
                    Country = e.User.Country,
                    Age = e.User.Age,
                    Email = e.User.Email
                })
                .FirstOrDefaultAsync();

            if (participant == null)
                return NotFound();
            ViewBag.CourseId = courseId;
            return View("ParticipantDetails", participant);
        }

    }

}

