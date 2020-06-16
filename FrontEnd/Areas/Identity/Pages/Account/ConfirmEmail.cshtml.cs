﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FrontEnd.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class ConfirmEmailModel : PageModel
    {
        // Dont allow User navigate in this page
        public IActionResult OnGet() => RedirectToPage("/Account/AccessDenied");
        public IActionResult OnPostAsync() => RedirectToPage("/Account/AccessDenied");

        //private readonly UserManager<FrontEndUser> _userManager;

        //public ConfirmEmailModel(UserManager<FrontEndUser> userManager)
        //{
        //    _userManager = userManager;
        //}

        //[TempData]
        //public string StatusMessage { get; set; }

        //public async Task<IActionResult> OnGetAsync(string userId, string code)
        //{
        //    if (userId == null || code == null)
        //    {
        //        return RedirectToPage("/Index");
        //    }

        //    var user = await _userManager.FindByIdAsync(userId);
        //    if (user == null)
        //    {
        //        return NotFound($"Unable to load user with ID '{userId}'.");
        //    }

        //    code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));
        //    var result = await _userManager.ConfirmEmailAsync(user, code);
        //    StatusMessage = result.Succeeded ? "Thank you for confirming your email." : "Error confirming your email.";
        //    return Page();
        //}
    }
}
