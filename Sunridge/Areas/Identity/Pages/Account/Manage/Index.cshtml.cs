using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sunridge.Models;

namespace Sunridge.Areas.Identity.Pages.Account.Manage
{
    [Authorize(Roles = "Administrator,Owner")]
    public partial class IndexModel : PageModel
    {
        private readonly UserManager<Owner> _userManager;
        private readonly SignInManager<Owner> _signInManager;

        public IndexModel(UserManager<Owner> userManager, SignInManager<Owner> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            public string OwnerId { get; set; }

            [EmailAddress]
            [Display(Name = "Email Address")]
            public string Email { get; set; }

            [Phone]
            [Display(Name = "Phone Number")]
            public string PhoneNumber { get; set; }

            [Display(Name = "First Name")]
            public string FirstName { get; set; }

            [Display(Name = "Last Name")]
            public string LastName { get; set; }

            public string Occupation { get; set; }

            public DateTime Birthday { get; set; }

            [Display(Name = "Street Address")]
            public string StreetAddress { get; set; }

            public string Apartment { get; set; }

            public string City { get; set; }

            public string Zip { get; set; }

            public string State { get; set; }

            [Display(Name = "Emergency Contact")]
            public string EmergencyContact { get; set; }

            [Display(Name = "Emergency Contact Number")]
            public string EmergencyContactNumber { get; set; }
        }

        private async Task LoadAsync(Owner user)
        {
            Input = new InputModel
            {
                OwnerId = user.Id,
                Email = user.Email,
                PhoneNumber = await _userManager.GetPhoneNumberAsync(user),
                FirstName = user.FirstName,
                LastName = user.LastName,
                Occupation = user.Occupation,
                Birthday = user.Birthday,
                StreetAddress = user.StreetAddress,
                Apartment = user.Apartment,
                City = user.City,
                Zip = user.Zip,
                State = user.State,
                EmergencyContact = user.EmergencyContact,
                EmergencyContactNumber = user.EmergencyContactNumber
            };
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
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }

            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            if (Input.PhoneNumber != phoneNumber)
            {
                var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, Input.PhoneNumber);
                if (!setPhoneResult.Succeeded)
                {
                    StatusMessage = "Unexpected error when trying to set phone number.";
                    return RedirectToPage();
                }
            }

            user.FirstName = Input.FirstName;
            user.LastName = Input.LastName;
            user.Occupation = Input.Occupation;
            user.Birthday = Input.Birthday;

            user.Email = Input.Email;
            user.StreetAddress = Input.StreetAddress;
            user.Apartment = Input.Apartment;
            user.City = Input.City;
            user.Zip = Input.Zip;
            user.State = Input.State;
            user.EmergencyContact = Input.EmergencyContact;
            user.EmergencyContactNumber = Input.EmergencyContactNumber;

            await _userManager.UpdateAsync(user);
            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Your profile has been updated";
            return RedirectToPage();
        }
    }
}
