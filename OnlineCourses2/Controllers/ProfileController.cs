using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using OnlineCourses2.Models;
using System.Security.Claims;

namespace OnlineCourses2.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public ProfileController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        // ---------------- USER ----------------
        [Authorize(Roles = "User")]
        public async Task<IActionResult> IndexUser()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
                return NotFound();

            return View(user);
        }

        [Authorize(Roles = "User")]
        public async Task<IActionResult> EditUser()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByIdAsync(userId);

            return View(user);
        }

        [HttpPost]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> Edit(ApplicationUser model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = await _userManager.FindByIdAsync(model.Id);

            user.FirstName = model.FirstName;
            user.MiddleName = model.MiddleName;
            user.LastName = model.LastName;
            user.City = model.City;
            user.Country = model.Country;
            user.Age = model.Age;

            await _userManager.UpdateAsync(user);

            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Organizer")]
        public async Task<IActionResult> IndexOrganizer()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByIdAsync(userId);

            return View("IndexOrganizer", user);
        }

        [Authorize(Roles = "Organizer")]
        public async Task<IActionResult> EditOrganizer()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByIdAsync(userId);

            return View("EditOrganizer", user);
        }

        [HttpPost]
        [Authorize(Roles = "Organizer")]
        public async Task<IActionResult> EditOrganizer(ApplicationUser model)
        {
            if (!ModelState.IsValid)
                return View("EditOrganizer", model);

            var user = await _userManager.FindByIdAsync(model.Id);

            user.FirstName = model.FirstName;
            user.MiddleName = model.MiddleName;
            user.LastName = model.LastName;
            user.City = model.City;
            user.Country = model.Country;
            user.Age = model.Age;

            await _userManager.UpdateAsync(user);

            return RedirectToAction("IndexOrganizer");
        }

        // ---------------- ADMIN ----------------
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> IndexAdmin()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByIdAsync(userId);

            return View("IndexAdmin", user);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> EditAdmin()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByIdAsync(userId);

            return View("EditAdmin", user);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> EditAdmin(ApplicationUser model)
        {
            if (!ModelState.IsValid)
                return View("EditAdmin", model);

            var user = await _userManager.FindByIdAsync(model.Id);

            user.FirstName = model.FirstName;
            user.MiddleName = model.MiddleName;
            user.LastName = model.LastName;
            user.City = model.City;
            user.Country = model.Country;
            user.Age = model.Age;

            await _userManager.UpdateAsync(user);

            return RedirectToAction("IndexAdmin");
        }
    }
}