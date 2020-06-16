using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FrontEnd.Areas.Identity.Pages.Account.Manage
{
    public class PersonalDataModel : PageModel
    {
        // Dont allow User navigate in this page
        public IActionResult OnGet() => RedirectToPage("/Account/AccessDenied");
        public IActionResult OnPostAsync() => RedirectToPage("/Account/AccessDenied");

        //private readonly UserManager<FrontEndUser> _userManager;
        //private readonly ILogger<PersonalDataModel> _logger;

        //public PersonalDataModel(
        //    UserManager<FrontEndUser> userManager,
        //    ILogger<PersonalDataModel> logger)
        //{
        //    _userManager = userManager;
        //    _logger = logger;
        //}

        //public async Task<IActionResult> OnGet()
        //{
        //    var user = await _userManager.GetUserAsync(User);
        //    if (user == null)
        //    {
        //        return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
        //    }

        //    return Page();
        //}
    }
}