using ManagementAsset.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace ManagementAsset.Areas.Identity.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public RegisterModel(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [BindProperty]
        public InputModel Input { get; set; } = new();

        public string? ReturnUrl { get; set; }

        public class InputModel
        {
            [Required(ErrorMessage = "Nama lengkap wajib diisi")]
            [Display(Name = "Nama Lengkap")]
            public string FullName { get; set; } = string.Empty;

            [Required(ErrorMessage = "Departemen wajib diisi")]
            [Display(Name = "Departemen")]
            public string Department { get; set; } = string.Empty;

            [Required(ErrorMessage = "Email wajib diisi")]
            [EmailAddress(ErrorMessage = "Format email tidak valid")]
            public string Email { get; set; } = string.Empty;

            [Required(ErrorMessage = "Password wajib diisi")]
            [StringLength(100, MinimumLength = 12, ErrorMessage = "Password minimal 12 karakter")]
            [DataType(DataType.Password)]
            public string Password { get; set; } = string.Empty;

            [Required(ErrorMessage = "Konfirmasi password wajib diisi")]
            [Compare("Password", ErrorMessage = "Password tidak cocok")]
            [DataType(DataType.Password)]
            [Display(Name = "Konfirmasi Password")]
            public string ConfirmPassword { get; set; } = string.Empty;
        }

        public void OnGet(string? returnUrl = null)
        {
            ReturnUrl = returnUrl ?? Url.Content("~/");
        }

        public async Task<IActionResult> OnPostAsync(string? returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");

            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = Input.Email,
                    Email = Input.Email,
                    FullName = Input.FullName,
                    Department = Input.Department,
                    EmailConfirmed = true
                };

                var result = await _userManager.CreateAsync(user, Input.Password);

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "User");
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return LocalRedirect(returnUrl);
                }

                foreach (var error in result.Errors)
                    ModelState.AddModelError(string.Empty, error.Description);
            }

            return Page();
        }
    }
}
