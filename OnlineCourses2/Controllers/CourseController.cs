using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineCourses2.Data;
using OnlineCourses2.Models;
using OnlineCourses2.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace OnlineCourses2.Controllers
{
    [Authorize(Roles = "Organizer,Admin")]
    public class CourseController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public CourseController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var vm = new CreateCourseViewModel
            {
                Categories = await _context.Categories.ToListAsync(),
                StartDate = DateTime.Today,
                EndDate = DateTime.Today.AddDays(30)

            };


            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateCourseViewModel model)
        {

            if (model.DurationHours < 8 || model.DurationHours > 30)
                ModelState.AddModelError("DurationHours", "Продължителността трябва да е между 8 и 30 часа.");

            if (model.Price <= 0)
                ModelState.AddModelError("Price", "Цената трябва да е по-голяма от 0.");

            if (model.MaxParticipants < 10 || model.MaxParticipants > 20)
                ModelState.AddModelError("MaxParticipants", "Броят участници трябва да е между 10 и 20.");
            if (model.EndDate < model.StartDate)
            {
                ModelState.AddModelError("EndDate", "Крайната дата трябва да е след началната.");
            }

            if (!ModelState.IsValid)
            {
                model.Categories = await _context.Categories.ToListAsync();
                if (model.StartDate == DateTime.MinValue)
                    model.StartDate = DateTime.Today;

                if (model.EndDate == DateTime.MinValue)
                    model.EndDate = DateTime.Today.AddDays(30);
                return View(model);
            }
            var organizer = await _userManager.GetUserAsync(User);

            string? imagePath = null;

            // Handle image upload
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
                StartDate = model.StartDate,
                EndDate = model.EndDate


            };

            _context.Courses.Add(course);
            await _context.SaveChangesAsync();
            TempData["Success"] = "Курсът беше създаден успешно!";
            return RedirectToAction("Manage");
        }
        [AllowAnonymous]
        public async Task<IActionResult> All()
        {
            var courses = await _context.Courses
                .Include(c => c.Category)
                .ToListAsync();

            return View(courses);
        }

        [HttpGet]
        public async Task<IActionResult> MyCourses()
        {
            var user = await _userManager.GetUserAsync(User);

            var courses = await _context.Courses
                .Where(c => c.OrganizerId == user.Id)
                .Include(c => c.Category)
                .ToListAsync();

            return View(courses);
        }

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
        [HttpPost]
        public async Task<IActionResult> Edit(EditCourseViewModel model)
        {
            if (model.EndDate < model.StartDate)
            {
                ModelState.AddModelError("EndDate", "Крайната дата трябва да е след началната.");
            }

            if (!ModelState.IsValid)
            {
                model.Categories = await _context.Categories.ToListAsync();
                if (model.StartDate == DateTime.MinValue)
                    model.StartDate = DateTime.Today;

                if (model.EndDate == DateTime.MinValue)
                    model.EndDate = DateTime.Today.AddDays(30);

                return View(model);
            }

            var course = await _context.Courses.FindAsync(model.Id);

            if (course == null)
                return NotFound();

            // Update fields
            course.Title = model.Title;
            course.ShortDescription = model.ShortDescription;
            course.Description = model.Description;
            course.DurationHours = model.DurationHours;
            course.Price = model.Price;
            course.MaxParticipants = model.MaxParticipants;
            course.CategoryId = model.CategoryId;
            course.HasCertificate = model.HasCertificate;
            course.StartDate = model.StartDate;
            course.EndDate = model.EndDate;


            // Handle new image upload
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

            return RedirectToAction("Manage");
        }
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

        [HttpGet]
        public async Task<IActionResult> Manage()
        {
            var user = await _userManager.GetUserAsync(User);

            var courses = await _context.Courses
                .Where(c => c.OrganizerId == user.Id)
                .Include(c => c.Category)
                .ToListAsync();

            return View(courses);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            var course = await _context.Courses.FindAsync(id);

            if (course == null)
                return NotFound();

            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();

            return RedirectToAction("Manage");
        }

    }

}

