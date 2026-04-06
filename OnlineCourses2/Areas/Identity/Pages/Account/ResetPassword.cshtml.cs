using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OnlineCourses2.Models; // ако ApplicationUser е в този namespace

namespace OnlineCourses2.Areas.Identity.Pages.Account
{
    public class ResetPasswordModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public ResetPasswordModel(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        [BindProperty]
        public InputModel Input { get; set; } = null!;

        public class InputModel
        {
            [Required]
            [EmailAddress]
            public string Email { get; set; } = null!;

            [Required]
            [StringLength(100, ErrorMessage = "Паролата трябва да е поне {2} символа.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            public string Password { get; set; } = null!;

            [DataType(DataType.Password)]
            [Display(Name = "Потвърди паролата")]
            [Compare("Password", ErrorMessage = "Паролите не съвпадат.")]
            public string ConfirmPassword { get; set; } = null!;
        }

        // ТУК ВАЖНО: взимаме email от query string и го слагаме в модела
        public IActionResult OnGet(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return RedirectToPage("./ForgotPassword");
            }

            Input = new InputModel
            {
                Email = email
            };

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            var user = await _userManager.FindByEmailAsync(Input.Email);

            if (user == null)
            {
                // За сигурност – все едно е успешно
                return RedirectToPage("./ResetPasswordConfirmation");
            }

            // Премахваме старата парола
            await _userManager.RemovePasswordAsync(user);

            // Добавяме новата
            var result = await _userManager.AddPasswordAsync(user, Input.Password);

            if (result.Succeeded)
            {
                return RedirectToPage("./ResetPasswordConfirmation");
            }

            foreach (var error in result.Errors)
                ModelState.AddModelError(string.Empty, error.Description);

            return Page();
        }
    }
}