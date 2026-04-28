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
    /// Controller for administrative operations such as managing users,
    /// organizers, and expired courses. Accessible only to Admin role.
    /// </summary>
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;

        public AdminController(UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            _context = context;

            _userManager = userManager;
        }
        /// <summary>
        /// Displays the admin dashboard home page.
        /// </summary>
        /// <returns>Returns the Index view.</returns>
        public IActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// Lists all organizers with optional search and sorting.
        /// </summary>
        /// <param name="search">Search term for filtering by email.</param>
        /// <param name="filter">Sorting option (name_asc, email_desc, city, etc.).</param>
        /// <returns>Returns a view with a filtered and sorted list of organizers.</returns>
        public async Task<IActionResult> Organizers(string search, string filter)
        {
            var organizers = await _userManager.GetUsersInRoleAsync("Organizer");

            if (!string.IsNullOrEmpty(search))
            {
                organizers = organizers
                    .Where(o => o.Email.Contains(search, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }

            organizers = filter switch
            {
                "name_asc" => organizers.OrderBy(o => o.FirstName).ToList(),
                "name_desc" => organizers.OrderByDescending(o => o.FirstName).ToList(),
                "email_asc" => organizers.OrderBy(o => o.Email).ToList(),
                "email_desc" => organizers.OrderByDescending(o => o.Email).ToList(),
                "city" => organizers.OrderBy(o => o.City).ToList(),
                _ => organizers
            };

            ViewBag.Search = search;
            ViewBag.Filter = filter;

            return View(organizers);
        }
        /// <summary>
        /// Lists all regular users with optional search and sorting.
        /// </summary>
        /// <param name="search">Search term for filtering by email.</param>
        /// <param name="filter">Sorting option (name_asc, age, city, etc.).</param>
        /// <returns>Returns a view with a filtered and sorted list of users.</returns>
        public async Task<IActionResult> Users(string search, string filter)
        {
            var users = await _userManager.GetUsersInRoleAsync("User");

            if (!string.IsNullOrEmpty(search))
            {
                users = users
                    .Where(u => u.Email.Contains(search, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }

            users = filter switch
            {
                "name_asc" => users.OrderBy(u => u.FirstName).ToList(),
                "name_desc" => users.OrderByDescending(u => u.FirstName).ToList(),
                "email_asc" => users.OrderBy(u => u.Email).ToList(),
                "email_desc" => users.OrderByDescending(u => u.Email).ToList(),
                "city" => users.OrderBy(u => u.City).ToList(),
                "age" => users.OrderBy(u => u.Age).ToList(),
                _ => users
            };

            ViewBag.Search = search;
            ViewBag.Filter = filter;

            return View(users);
        }

        /// <summary>
        /// Displays detailed information about a specific user.
        /// </summary>
        /// <param name="id">The ID of the user to display.</param>
        /// <returns>
        /// Returns a view with user details,
        /// or NotFound if the user does not exist.
        /// </returns>
        public async Task<IActionResult> UserDetails(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
                return NotFound();

            var roles = await _userManager.GetRolesAsync(user);

            var model = new AdminUserDetailsViewModel
            {
                Id = user.Id,
                FirstName = user.FirstName,
                MiddleName = user.MiddleName,
                LastName = user.LastName,
                City = user.City,
                Country = user.Country,
                Age = user.Age,
                Email = user.Email,
                Role = roles.FirstOrDefault() 
            };

            return View(model);
        }
        /// <summary>
        /// Loads the Edit User form with existing user data and available roles.
        /// </summary>
        /// <param name="id">The ID of the user to edit.</param>
        /// <returns>
        /// Returns a view with an AdminUserEditViewModel,
        /// or NotFound if the user does not exist.
        /// </returns>
        public async Task<IActionResult> EditUser(string id)
        {
          
            var user = await _userManager.FindByIdAsync(id);
            

            if (user == null)
                return NotFound();

            var roles = await _userManager.GetRolesAsync(user);

            var model = new AdminUserEditViewModel
            {
                Id = user.Id,
                FirstName = user.FirstName,
                MiddleName = user.MiddleName,
                LastName = user.LastName,
                City = user.City,
                Country = user.Country,
                Age = user.Age,
                Email = user.Email,
                SelectedRole = roles.FirstOrDefault(),
                AvailableRoles = _context.Roles.Where(r => r.Name != "Admin").Select(r => r.Name).ToList()

            };

            return View(model);
        }
        /// <summary>
        /// Updates user information and assigns a new role.
        /// </summary>
        /// <param name="model">The view model containing updated user data.</param>
        /// <returns>
        /// Redirects to UserDetails on success,
        /// or returns the EditUser view with validation errors.
        /// </returns>
        [HttpPost]
        public async Task<IActionResult> EditUser(AdminUserEditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.AvailableRoles = _context.Roles.Select(r => r.Name).ToList();
                return View(model);
            }

            var user = await _userManager.FindByIdAsync(model.Id);

            if (user == null)
                return NotFound();

            user.FirstName = model.FirstName;
            user.MiddleName = model.MiddleName;
            user.LastName = model.LastName;
            user.City = model.City;
            user.Country = model.Country;
            user.Age = model.Age;
            user.Email = model.Email;

            await _userManager.UpdateAsync(user);

            var currentRoles = await _userManager.GetRolesAsync(user);
            await _userManager.RemoveFromRolesAsync(user, currentRoles);
            await _userManager.AddToRoleAsync(user, model.SelectedRole);

            return RedirectToAction("UserDetails", new { id = user.Id });
        }
        /// <summary>
        /// Displays detailed information about a specific organizer.
        /// </summary>
        /// <param name="id">The ID of the organizer.</param>
        /// <returns>
        /// Returns a view with organizer details,
        /// or NotFound if the organizer does not exist.
        /// </returns>
        public async Task<IActionResult> OrganizerDetails(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
                return NotFound();

            var roles = await _userManager.GetRolesAsync(user);

            var model = new AdminUserDetailsViewModel
            {
                Id = user.Id,
                FirstName = user.FirstName,
                MiddleName = user.MiddleName,
                LastName = user.LastName,
                City = user.City,
                Country = user.Country,
                Age = user.Age,
                Email = user.Email,
                Role = roles.FirstOrDefault()
            };

            return View(model);
        }
        /// <summary>
        /// Lists all expired courses with search, filtering, and sorting options.
        /// </summary>
        /// <param name="search">Search term for title, category, or organizer name.</param>
        /// <param name="categoryId">Optional category filter.</param>
        /// <param name="sort">Sorting option (name_asc, price_low, hours_high, etc.).</param>
        /// <param name="certificate">Filter by certificate: "yes", "no", or null.</param>
        /// <returns>Returns a view with a filtered list of expired courses.</returns>
        public async Task<IActionResult> ManageAllExpired(
     string search,
     string categoryId,
     string sort,
     string certificate)
        {
            var courses = _context.Courses
                .Where(c => c.EndDate < DateTime.Now)
                .Include(c => c.Category)
                .Include(c => c.Organizer)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                courses = courses.Where(c =>
     c.Title.Contains(search) ||
     c.Category.Name.Contains(search) ||
     (c.Organizer.FirstName + " " + c.Organizer.LastName).Contains(search)
 );
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
        /// Redirects to ManageAllExpired after deletion,
        /// or NotFound if the course does not exist.
        /// </returns>
        [HttpPost]
        public async Task<IActionResult> DeleteExpiredCourseAdmin(string id)
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

            return RedirectToAction("ManageAllExpired");
        }
    }
}

