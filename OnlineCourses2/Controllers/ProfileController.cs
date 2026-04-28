using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using OnlineCourses2.Models;
using System.Security.Claims;

namespace OnlineCourses2.Controllers
{
    /// <summary>
    /// Controller responsible for displaying and editing profile information
    /// for Users, Organizers, and Admins. Requires authentication.
    /// </summary>
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public ProfileController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        /// <summary>
        /// Displays the profile page for a regular User.
        /// </summary>
        /// <returns>
        /// Returns the User profile view,
        /// or NotFound if the user cannot be located.
        /// </returns>
        public async Task<IActionResult> IndexUser()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
                return NotFound();

            return View(user);
        }
        /// <summary>
        /// Loads the Edit Profile page for a regular User.
        /// </summary>
        /// <returns>
        /// Returns the EditUser view populated with the current user's data.
        /// </returns>
        [Authorize(Roles = "User")]
        public async Task<IActionResult> EditUser()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByIdAsync(userId);

            return View(user);
        }
        /// <summary>
        /// Updates the profile information of a regular User.
        /// </summary>
        /// <param name="model">The ApplicationUser model containing updated profile data.</param>
        /// <returns>
        /// Redirects to Index on success,
        /// or returns the Edit view if validation fails.
        /// </returns>
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
        /// <summary>
        /// Displays the profile page for an Organizer.
        /// </summary>
        /// <returns>
        /// Returns the IndexOrganizer view with the organizer's profile data.
        /// </returns>
        [Authorize(Roles = "Organizer")]
        public async Task<IActionResult> IndexOrganizer()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByIdAsync(userId);

            return View("IndexOrganizer", user);
        }
        /// <summary>
        /// Loads the Edit Profile page for an Organizer.
        /// </summary>
        /// <returns>
        /// Returns the EditOrganizer view populated with the organizer's data.
        /// </returns>
        [Authorize(Roles = "Organizer")]
        public async Task<IActionResult> EditOrganizer()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByIdAsync(userId);

            return View("EditOrganizer", user);
        }
        /// <summary>
        /// Updates the profile information of an Organizer.
        /// </summary>
        /// <param name="model">The ApplicationUser model containing updated organizer data.</param>
        /// <returns>
        /// Redirects to IndexOrganizer on success,
        /// or returns the EditOrganizer view if validation fails.
        /// </returns>
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
        /// <summary>
        /// Displays the profile page for an Admin.
        /// </summary>
        /// <returns>
        /// Returns the IndexAdmin view with the admin's profile data.
        /// </returns>
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> IndexAdmin()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByIdAsync(userId);

            return View("IndexAdmin", user);
        }
        /// <summary>
        /// Loads the Edit Profile page for an Admin.
        /// </summary>
        /// <returns>
        /// Returns the EditAdmin view populated with the admin's data.
        /// </returns>
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> EditAdmin()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByIdAsync(userId);

            return View("EditAdmin", user);
        }
        /// <summary>
        /// Updates the profile information of an Admin.
        /// </summary>
        /// <param name="model">The ApplicationUser model containing updated admin data.</param>
        /// <returns>
        /// Redirects to IndexAdmin on success,
        /// or returns the EditAdmin view if validation fails.
        /// </returns>
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