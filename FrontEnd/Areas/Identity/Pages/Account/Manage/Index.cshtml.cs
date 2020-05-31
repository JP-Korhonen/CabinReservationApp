using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using FrontEnd.Areas.Identity.Data;
using FrontEnd.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FrontEnd.Areas.Identity.Pages.Account.Manage
{
    public partial class IndexModel : PageModel
    {
        private readonly UserManager<FrontEndUser> _userManager;
        private readonly SignInManager<FrontEndUser> _signInManager;
        private readonly ServiceRepository _service;

        [ViewData]
        public List<SelectListItem> PostalCodes { get; set; }

        public IndexModel(
            UserManager<FrontEndUser> userManager,
            SignInManager<FrontEndUser> signInManager,
            ServiceRepository service)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _service = service;
        }

        [Display(Name = "Käyttäjätunnus")]
        public string Username { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required]
            [Display(Name = "Etunimi")]
            public string FirstName { get; set; }

            [Required]
            [Display(Name = "Sukunimi")]
            public string LastName { get; set; }

            [Required]
            [Display(Name = "Puhelinnumero")]
            public string PhoneNumber { get; set; }

            [Required]
            [Display(Name = "Osoite")]
            public string Address { get; set; }

            [Required]
            [Display(Name = "Postinumero")]
            public string PostalCode { get; set; }
        }

        private async Task LoadAsync(FrontEndUser user)
        {
            var userName = await _userManager.GetUserNameAsync(user);
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);

            // Gets Person in Api by Identity-User
            var person = await _service.GetPerson(User);

            Username = userName;

            Input = new InputModel
            {
                FirstName = person.FirstName,
                LastName = person.LastName,
                PhoneNumber = phoneNumber,
                Address = person.Address,
                PostalCode = person.PostalCode
            };

            // Setting PostalCodes
            await SetPostalCodes();
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);

            // Gets Person in Api by Identity-User
            var person = await _service.GetPerson(User);
            if (user == null || person == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                // Setting PostalCodes
                await SetPostalCodes();
                return Page();
            }

            //var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            //if (Input.PhoneNumber != phoneNumber)
            //{
            //    var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, Input.PhoneNumber);
            //    if (!setPhoneResult.Succeeded)
            //    {
            //        var userId = await _userManager.GetUserIdAsync(user);
            //        throw new InvalidOperationException($"Unexpected error occurred setting phone number for user with ID '{userId}'.");
            //    }
            //}

            // Checks if user changes personal details
            if (person.FirstName != Input.FirstName ||
            person.LastName != Input.LastName ||
            person.PhoneNumber != Input.PhoneNumber ||
            person.Address != Input.Address ||
            person.PostalCode != Input.PostalCode)
            {
                person.FirstName = Input.FirstName;
                person.LastName = Input.LastName;
                person.PhoneNumber = Input.PhoneNumber;
                person.Address = Input.Address;
                person.PostalCode = Input.PostalCode;

                // Updates Person-details in Api by Identity-User
                bool PutPerson = await _service.PutPerson(User, person.PersonId, person);
                if (false == PutPerson)
                {
                    // Setting PostalCodes
                    await SetPostalCodes();

                    // TODO: if cant update Person in Api must add some Error Handling
                    throw new InvalidOperationException($"Unexpected error occurred setting personal details for user with Username '{Username}'.");
                }
            }

            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Your profile has been updated";
            return RedirectToPage();
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
