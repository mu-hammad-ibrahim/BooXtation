using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BookXtation.DAL.Models.Data;
using BooXtation.BLL.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace BooXtation.Areas.Identity.Pages.Account.Manage
{
    [Authorize(Roles = "Customer")]
    public class IndexModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ICustomerRepository _customerRepository;


        public IndexModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ICustomerRepository customerRepository) // Inject CustomerRepository
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _customerRepository = customerRepository; // Assign repository
        }

        public string Username { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Phone]
            [Display(Name = "Phone number")]
            public string PhoneNumber { get; set; }

            // Add Customer details
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public DateTime? DateOfBirth { get; set; }
        }

        private async Task LoadAsync(ApplicationUser user)
        {
            var userName = await _userManager.GetUserNameAsync(user);
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);

            // Fetch customer using repository
            var customer = await _customerRepository.GetByUserIdAsync(user.Id);

            Username = userName;

            if (customer != null)
            {
                Input = new InputModel
                {
                    PhoneNumber = phoneNumber,
                    FirstName = customer.FirstName,
                    LastName = customer.LastName,
                    DateOfBirth = customer.DateOfBirth,
                };
            }
            else
            {
                Input = new InputModel
                {
                    PhoneNumber = phoneNumber
                };
            }
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

            // Fetch customer by User_ID and update the customer data using repository
            var customer = await _customerRepository.GetByUserIdAsync(user.Id);
            if (customer != null)
            {
                customer.FirstName = Input.FirstName;
                customer.LastName = Input.LastName;
                customer.Phone = Input.PhoneNumber;

                customer.DateOfBirth = Input.DateOfBirth;

                await _customerRepository.UpdateAsync(customer); // Update customer data
            }

            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Your profile has been updated";
            return RedirectToPage();
        }
    }
}
