using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineCourses2.Data;
using OnlineCourses2.Models;
using OnlineCourses2.ViewModels;



namespace OnlineCourses2.Controllers
{
    /// <summary>
    /// Controller for managing course categories. Accessible to Admin and Organizer roles.
    /// Supports listing, creating, filtering, sorting, pagination, and deleting categories.
    /// </summary>
    [Authorize(Roles = "Admin,Organizer")]
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CategoryController(ApplicationDbContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Displays a list of all categories without filtering or pagination.
        /// </summary>
        /// <returns>Returns a view containing a list of all categories.</returns>
        public async Task<IActionResult> Index()
        {
            var categories = await _context.Categories.ToListAsync();
            return View(categories);
        }
        /// <summary>
        /// Displays the Create Category page with a paginated, searchable,
        /// and sortable list of existing categories.
        /// </summary>
        /// <param name="search">Optional search term for filtering categories by name.</param>
        /// <param name="sort">Sorting option (name_asc, name_desc).</param>
        /// <param name="page">Current page number for pagination (1-based).</param>
        /// <returns>
        /// Returns a view with a CreateCategoryViewModel containing the filtered,
        /// sorted, and paginated list of categories.
        /// </returns>
        [HttpGet]
        public async Task<IActionResult> Create(string search, string sort, int page = 1)
        {
            int pageSize = 12;

            var query = _context.Categories.AsQueryable();

            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(c => c.Name.ToLower().Contains(search.ToLower()));
            }

            switch (sort)
            {
                case "name_asc":
                    query = query.OrderBy(c => c.Name);
                    break;

                case "name_desc":
                    query = query.OrderByDescending(c => c.Name);
                    break;

                default:
                    query = query.OrderBy(c => c.Name);
                    break;
            }

            int totalItems = await query.CountAsync();
            int totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

            var categories = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var vm = new CreateCategoryViewModel
            {
                Categories = categories
            };

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = totalPages;
            ViewBag.Search = search;
            ViewBag.Sort = sort;

            return View(vm);
        }
        /// <summary>
        /// Creates a new category after validating the input and ensuring
        /// that a category with the same name does not already exist.
        /// </summary>
        /// <param name="model">The view model containing the new category name and category list.</param>
        /// <returns>
        /// Redirects to Create on success.
        /// Returns the Create view with validation errors if the model is invalid
        /// or if a category with the same name already exists.
        /// </returns>
        [HttpPost]
        public async Task<IActionResult> Create(CreateCategoryViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Categories = await _context.Categories
                    .OrderBy(c => c.Name)
                    .ToListAsync();
                ViewBag.Search = "";
                ViewBag.Sort = "";
                return View(model);
            }

            bool exists = await _context.Categories
                .AnyAsync(c => c.Name.ToLower() == model.Name.ToLower());

            if (exists)
            {
                ModelState.AddModelError("Name", "Категория с това име вече съществува.");

                model.Categories = await _context.Categories
                    .OrderBy(c => c.Name)
                    .ToListAsync();

                return View(model);
            }

            var category = new Category { Name = model.Name };
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
            return RedirectToAction("Create");
        }

        /// <summary>
        /// Deletes a category if it has no associated courses.
        /// </summary>
        /// <param name="id">The ID of the category to delete.</param>
        /// <returns>
        /// Redirects to Create after deletion.
        /// Returns NotFound if the category does not exist.
        /// If the category contains courses, sets an error message and redirects to Create.
        /// </returns>
        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            var category = await _context.Categories
                .Include(c => c.Courses)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (category == null)
                return NotFound();

            if (category.Courses.Any())
            {
                TempData["Error"] = "Категорията не може да бъде изтрита, защото има курсове.";
                return RedirectToAction("Create");
            }

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();

            return RedirectToAction("Create");
        }
    }
}
