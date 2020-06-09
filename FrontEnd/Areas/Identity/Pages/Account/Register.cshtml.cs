using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using FrontEnd.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using CommonModels;
using FrontEnd.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FrontEnd.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<FrontEndUser> _signInManager;
        private readonly UserManager<FrontEndUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;
        private readonly ServiceRepository _service;

        [ViewData]
        public List<SelectListItem> PostalCodes { get; set; }

        public RegisterModel(
            UserManager<FrontEndUser> userManager,
            SignInManager<FrontEndUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender,
            ServiceRepository service)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
            _service = service;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            [Display(Name = "Sähköposti")]
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "{0} täytyy olla min. {2} merkkiä ja max. {1} merkkiä pitkä.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Salasana")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Salasana uudelleen")]
            [Compare("Password", ErrorMessage = "Salasanat eivät täsmää.")]
            public string ConfirmPassword { get; set; }

            [CustomRequired]
            [Display(Name = "Henkilötunnus")]
            public string SocialSecurityNumber { get; set; }

            [CustomRequired]
            [Display(Name = "Etunimi")]
            public string FirstName { get; set; }

            [CustomRequired]
            [Display(Name = "Sukunimi")]
            public string LastName { get; set; }

            [CustomRequired]
            [Display(Name = "Puhelinnumero")]
            public string PhoneNumber { get; set; }

            [CustomRequired]
            [Display(Name = "Osoite")]
            public string Address { get; set; }

            [CustomRequired]
            [Display(Name = "Postinumero")]
            public string PostalCode { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {            
            // Setting PostalCodes
            await SetPostalCodes();
            
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            if (ModelState.IsValid)
            {
                var user = new FrontEndUser { UserName = Input.Email, Email = Input.Email };

                var person = new Person { Email = Input.Email, SocialSecurityNumber = Input.SocialSecurityNumber, FirstName = Input.FirstName, LastName = Input.LastName, PhoneNumber = Input.PhoneNumber, Address = Input.Address, PostalCode = Input.PostalCode };

                var result = await _userManager.CreateAsync(user, Input.Password);
                if (result.Succeeded)
                {
                    // Setting Userrole to Person
                    var newUser = await _userManager.FindByNameAsync(user.UserName);
                    var setRole = await _userManager.AddToRoleAsync(newUser, "Customer");

                    // Creating new Person in Api
                    bool PostPerson = await _service.PostPerson(person);
                    if (false == PostPerson || false == setRole.Succeeded)
                    {
                        // TODO: if cant create Person/or add Role, must add some Error Handling, maybe delete User in IdentityDB??

                        // Setting PostalCodes
                        await SetPostalCodes();

                        return Page();
                    }

                    _logger.LogInformation("User created a new account with password.");

                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { area = "Identity", userId = user.Id, code = code, returnUrl = returnUrl },
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
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }

        // Sets PostalCodes to DropDownMenu
        public async Task SetPostalCodes()
        {
            var postalCodes = await _service.GetPostalCodes();
            PostalCodes = new List<SelectListItem>();
            foreach (var item in postalCodes)
            {
                PostalCodes.Add(new SelectListItem { Value = item, Text = item });
            }
        }
    }
}
