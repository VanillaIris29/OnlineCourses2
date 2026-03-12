using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using OnlineCourses2.Models;
using System.Security.Claims;


namespace OnlineCourses2.Controllers
{
    public class ProfileController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public ProfileController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
                return NotFound();

            return View(user);
        }
        [Authorize]
        public async Task<IActionResult> Edit()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByIdAsync(userId);

            return View(user);
        }

        [HttpPost]
        [Authorize]
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
            user.Email = model.Email;
            user.UserName = model.Email;

            await _userManager.UpdateAsync(user);

            return RedirectToAction("Index");
        }
    }
}
