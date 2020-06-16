using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FrontEnd.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public abstract class ResendEmailConfirmationModel : PageModel
    {
        // Dont allow User navigate in this page
        public IActionResult OnGet() => RedirectToPage("/Account/AccessDenied");
        public IActionResult OnPostAsync() => RedirectToPage("/Account/AccessDenied");

        //private readonly UserManager<FrontEndUser> _userManager;
        //private readonly IEmailSender _emailSender;

        //public ResendEmailConfirmationModel(UserManager<FrontEndUser> userManager, IEmailSender emailSender)
        //{
        //    _userManager = userManager;
        //    _emailSender = emailSender;
        //}

        //[BindProperty]
        //public InputModel Input { get; set; }

        //public class InputModel
        //{
        //    [Required]
        //    [EmailAddress]
        //    public string Email { get; set; }
        //}

        //public void OnGet()
        //{
        //}

        //public async Task<IActionResult> OnPostAsync()
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return Page();
        //    }

        //    var user = await _userManager.FindByEmailAsync(Input.Email);
        //    if (user == null)
        //    {
        //        ModelState.AddModelError(string.Empty, "Verification email sent. Please check your email.");
        //        return Page();
        //    }

        //    var userId = await _userManager.GetUserIdAsync(user);
        //    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
        //    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
        //    var callbackUrl = Url.Page(
        //        "/Account/ConfirmEmail",
        //        pageHandler: null,
        //        values: new { userId = userId, code = code },
        //        protocol: Request.Scheme);
        //    await _emailSender.SendEmailAsync(
        //        Input.Email,
        //        "Confirm your email",
        //        $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

        //    ModelState.AddModelError(string.Empty, "Verification email sent. Please check your email.");
        //    return Page();
        //}
    }
}
