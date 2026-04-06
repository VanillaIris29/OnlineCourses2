using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using OnlineCourses2.Models;

namespace OnlineCourses2.Areas.Identity.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserStore<ApplicationUser> _userStore;
        private readonly IUserEmailStore<ApplicationUser> _emailStore;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;

        public RegisterModel(
            UserManager<ApplicationUser> userManager,
            IUserStore<ApplicationUser> userStore,
            SignInManager<ApplicationUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender)
        {
            _userManager = userManager;
            _userStore = userStore;
            _emailStore = GetEmailStore();
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public class InputModel
{
    [Required(ErrorMessage = "Полето „Име“ е задължително.")]
    [Display(Name = "Име")]
    public string FirstName { get; set; } = null!;

    [Display(Name = "Презиме")]
    public string? MiddleName { get; set; }

    [Required(ErrorMessage = "Полето „Фамилия“ е задължително.")]
    [Display(Name = "Фамилия")]
    public string LastName { get; set; } = null!;

    [Required(ErrorMessage = "Полето „Възраст“ е задължително.")]
    [Range(14, 100, ErrorMessage = "Моля, въведете валидна възраст (14).")]
    [Display(Name = "Възраст")]
    public int Age { get; set; }

    [Required(ErrorMessage = "Полето „Град“ е задължително.")]
    [Display(Name = "Град")]
    public string City { get; set; } = null!;

    [Required(ErrorMessage = "Полето „Държава“ е задължително.")]
    [Display(Name = "Държава")]
    public string Country { get; set; } = null!;

    [Required(ErrorMessage = "Полето „Email“ е задължително.")]
    [EmailAddress(ErrorMessage = "Моля, въведете валиден Email адрес.")]
    [Display(Name = "Email")]
    public string Email { get; set; } = null!;

    [Required(ErrorMessage = "Полето „Парола“ е задължително.")]
    [StringLength(100, ErrorMessage = "Паролата трябва да е поне {2} символа.", MinimumLength = 6)]
    [DataType(DataType.Password)]
    [Display(Name = "Парола")]
    public string Password { get; set; } = null!;

    [Required(ErrorMessage = "Моля, потвърдете паролата.")]
    [DataType(DataType.Password)]
    [Display(Name = "Потвърди паролата")]
    [Compare("Password", ErrorMessage = "Паролите не съвпадат.")]
    public string ConfirmPassword { get; set; } = null!;
}

        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            if (ModelState.IsValid)
            {
                var user = CreateUser();

                await _userStore.SetUserNameAsync(user, Input.Email, CancellationToken.None);
                await _emailStore.SetEmailAsync(user, Input.Email, CancellationToken.None);

              
                user.FirstName = Input.FirstName;
                user.MiddleName = Input.MiddleName;
                user.LastName = Input.LastName;
                user.Age = Input.Age;
                user.City = Input.City;
                user.Country = Input.Country;

                var result = await _userManager.CreateAsync(user, Input.Password);

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "User");

                    _logger.LogInformation("User created a new account with password.");

                    var userId = await _userManager.GetUserIdAsync(user);
                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { area = "Identity", userId = userId, code = code, returnUrl = returnUrl },
                        protocol: Request.Scheme);

                    await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                        $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        return RedirectToPage("RegisterConfirmation", new { email = Input.Email, returnUrl = returnUrl });
                    }
                    else
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        return LocalRedirect(returnUrl);
                    }
                }

                foreach (var error in result.Errors)
                    ModelState.AddModelError(string.Empty, error.Description);
            }

            return Page();
        }

        private ApplicationUser CreateUser()
        {
            try
            {
                return Activator.CreateInstance<ApplicationUser>();
            }
            catch
            {
                throw new InvalidOperationException($"Cannot create an instance of '{nameof(ApplicationUser)}'.");
            }
        }

        private IUserEmailStore<ApplicationUser> GetEmailStore()
        {
            if (!_userManager.SupportsUserEmail)
                throw new NotSupportedException("The default UI requires a user store with email support.");

            return (IUserEmailStore<ApplicationUser>)_userStore;
        }
    }
}