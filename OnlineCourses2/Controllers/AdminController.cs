using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineCourses2.Data;
using OnlineCourses2.Models;
using OnlineCourses2.ViewModels;

namespace OnlineCourses2.Controllers
{
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

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Organizers(string search, string filter)
        {
            // Взимаме всички потребители, които са в ролята "Organizer"
            var organizers = await _userManager.GetUsersInRoleAsync("Organizer");

            // Търсене
            if (!string.IsNullOrEmpty(search))
            {
                organizers = organizers
                    .Where(o => o.Email.Contains(search, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }

            // Филтриране
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
                Role = roles.FirstOrDefault() // ако има една роля
            };

            return View(model);
        }
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

            // Update fields
            user.FirstName = model.FirstName;
            user.MiddleName = model.MiddleName;
            user.LastName = model.LastName;
            user.City = model.City;
            user.Country = model.Country;
            user.Age = model.Age;
            user.Email = model.Email;

            await _userManager.UpdateAsync(user);

            // Update role
            var currentRoles = await _userManager.GetRolesAsync(user);
            await _userManager.RemoveFromRolesAsync(user, currentRoles);
            await _userManager.AddToRoleAsync(user, model.SelectedRole);

            return RedirectToAction("UserDetails", new { id = user.Id });
        }
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


    }
}

