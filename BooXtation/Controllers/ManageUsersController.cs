using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using BookXtation.DAL.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using BookXtation.DAL.Models.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.WebUtilities;
using System.Text.Encodings.Web;
using System.Text;

namespace BooXtation.Controllers
{
    [Authorize(Roles = "Admin,Editor")]
    public class ManageUsersController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;


        public ManageUsersController(
            RoleManager<IdentityRole> roleManager, 
            UserManager<ApplicationUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
      
        }

        public async Task<IActionResult> Index()
        {
            var users = _userManager.Users.ToList();
            var roles = _roleManager.Roles
                .Where(r => r.Name != "Customer") // Exclude the "Customer" role
                .ToList();

            var userRoles = new Dictionary<IdentityUser, IList<string>>();

            foreach (var user in users)
            {
                var rolesForUser = await _userManager.GetRolesAsync(user);

                if (rolesForUser.Contains("Customer"))
                {
                    continue;
                }

                userRoles[user] = rolesForUser ?? new List<string>();
            }

            var viewModel = new UserRoleViewModel
            {
                Users = userRoles.Keys.ToList(), // Only include filtered users
                Roles = roles,
                UserRoles = userRoles
            };

            return View(viewModel);
        }



        [HttpGet]
        public ActionResult AddNewRole()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddNewRole(RoleViewModel roleVM)
        {
            if (ModelState.IsValid)
            {
                IdentityRole identityRole = new IdentityRole()
                {
                    Name = roleVM.RoleName
                };

                var result = await _roleManager.CreateAsync(identityRole);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }

                foreach(var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
            }
            return View(roleVM);
        }


        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteRole(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);

            if (role == null)
            {
                TempData["Error"] = "Role not found.";
                return RedirectToAction("Index");
            }

            //Vaildate that user is admin to delete
            var currentUser = await _userManager.GetUserAsync(User);
            if (!await _userManager.IsInRoleAsync(currentUser, "Admin"))
            {
                TempData["Error"] = "You are not allowed to delete Roles.";
                return RedirectToAction("Index");
            }
            //////


            if (role.Name == "Admin")
            {
                TempData["Error"] = "Cannot delete the Admin role.";
                return RedirectToAction("Index");
            }
            if (role.Name == "Editor")
            {
                TempData["Error"] = "Cannot delete the Editor role.";
                return RedirectToAction("Index");
            }

            var usersInRole = await _userManager.GetUsersInRoleAsync(role.Name);

            if (usersInRole.Any())
            {
                TempData["Error"] = $"Cannot delete role '{role.Name}' because there are users assigned to this role.";
                return RedirectToAction("Index");
            }

            var result = await _roleManager.DeleteAsync(role);

            if (result.Succeeded)
            {
                TempData["Success"] = $"Role '{role.Name}' deleted successfully.";
            }
            else
            {
                TempData["Error"] = $"Error occurred while deleting role '{role.Name}'.";
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> AddRoleToUser()
        {
            var users = await _userManager.Users.ToListAsync();
            var roles = await _roleManager.Roles.ToListAsync();

            var model = new UserRoleViewModel
            {
                Users = users,
                Roles = roles
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AssignRole(string userId, string roleName)
        {
            if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(roleName))
            {
                TempData["Error"] = "Invalid user or role.";
                return RedirectToAction("Index");
            }

            var user = await _userManager.FindByIdAsync(userId);
            var role = await _roleManager.FindByNameAsync(roleName);

            if (user == null || role == null)
            {
                return NotFound();
            }

            if (await _userManager.IsInRoleAsync(user, roleName))
            {
                TempData["Error"] = $"User '{user.UserName}' already has the role '{roleName}'.";
                return RedirectToAction("Index");
            }
            var result = await _userManager.AddToRoleAsync(user, roleName);

            if (result.Succeeded)
            {
                TempData["Success"] = $"Role '{roleName}' assigned to user '{user.UserName}' successfully.";
            }
            else
            {
                TempData["Error"] = $"Error occurred while assigning role '{roleName}' to user '{user.UserName}'.";
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveRoleFromUser(string userId, string roleName)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                TempData["Error"] = "User not found.";
                return RedirectToAction("Index");
            }

            if (string.Equals(user.UserName, "Admin", StringComparison.OrdinalIgnoreCase) && roleName == "Admin")
            {
                TempData["Error"] = "Cannot remove Admin role from Admin user.";
                return RedirectToAction("Index");
            }

            var result = await _userManager.RemoveFromRoleAsync(user, roleName);
            if (result.Succeeded)
            {
                TempData["Success"] = $"Role '{roleName}' removed from user '{user.UserName}'.";
            }
            else
            {
                TempData["Error"] = "Failed to remove role from user.";
            }

            return RedirectToAction("Index");
        }


        [HttpGet]
        public IActionResult AddNewUser()
        {
            return View(new UserViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> AddNewUser(UserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var existingUserByUsername = await _userManager.FindByNameAsync(model.UserName);
                var existingUserByEmail = await _userManager.FindByEmailAsync(model.Email);

                if (existingUserByUsername != null)
                {
                    TempData["Error"] = "Username already exists. Please choose another one.";
                    return View(model);
                }

                if (existingUserByEmail != null)
                {
                    TempData["Error"] = "Email already exists. Please use a different email address.";
                    return View(model);
                }

                var user = new ApplicationUser
                {
                    UserName = model.UserName,
                    Email = model.Email
                };

                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    TempData["Success"] = "User created successfully!";
                    return RedirectToAction("Index"); 
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

            TempData["Error"] = "Failed to add new user.";
            return View(model); 
        }

        [HttpGet]
        public async Task<IActionResult> EditUser(string id)
        {
            //Vaildate that user is admin to delete
            var currentUser = await _userManager.GetUserAsync(User);
            if (!await _userManager.IsInRoleAsync(currentUser, "Admin"))
            {
                TempData["Error"] = "You are not allowed to Edit Users.";
                return RedirectToAction("Index");
            }
            //////
            if (id == null)
            {
                return NotFound();
            }

            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            // Check if the user is an admin
            var roles = await _userManager.GetRolesAsync(user);
            if (roles.Contains("Admin"))
            {
                TempData["Error"] = "Admins cannot be edited.";
                return RedirectToAction("Index");
            }

            var model = new EditUserViewModel
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditUser(EditUserViewModel model)
        {
            //Vaildate that user is admin to delete
            var currentUser = await _userManager.GetUserAsync(User);
            if (!await _userManager.IsInRoleAsync(currentUser, "Admin"))
            {
                TempData["Error"] = "You are not allowed to Edit Users.";
                return RedirectToAction("Index");
            }
            //////
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userManager.FindByIdAsync(model.Id);
            if (user == null)
            {
                return NotFound();
            }

            // Check if the user is an admin
            var roles = await _userManager.GetRolesAsync(user);
            if (roles.Contains("Admin"))
            {
                return Forbid(); // or return RedirectToAction("AccessDenied"); depending on your error handling setup
            }

            user.UserName = model.UserName;
            user.Email = model.Email;

            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                if (!string.IsNullOrEmpty(model.NewPassword))
                {
                    var resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);
                    var resetResult = await _userManager.ResetPasswordAsync(user, resetToken, model.NewPassword);

                    if (!resetResult.Succeeded)
                    {
                        foreach (var error in resetResult.Errors)
                        {
                            ModelState.AddModelError("", error.Description);
                        }
                        return View(model);
                    }
                }

                TempData["Success"] = "User updated successfully!";
                return RedirectToAction("Index");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteUser(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                TempData["Error"] = "User ID is required.";
                return RedirectToAction("Index");
            }

            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                TempData["Error"] = "User not found.";
                return RedirectToAction("Index");
            }

            //Vaildate that user is admin to delete
            var currentUser = await _userManager.GetUserAsync(User);
            if (!await _userManager.IsInRoleAsync(currentUser, "Admin"))
            {
                TempData["Error"] = "You are not allowed to delete users.";
                return RedirectToAction("Index");
            }
            ///////
            var isAdmin = await _userManager.IsInRoleAsync(user, "Admin");
            if (user.UserName.Equals("admin", StringComparison.OrdinalIgnoreCase))
            {
                TempData["Error"] = $"Cannot delete the user {user.UserName}";
                return RedirectToAction("Index");
            }

            if (isAdmin)
            {
                TempData["Error"] = "Cannot delete user with 'Admin' role.";
                return RedirectToAction("Index");
            }

            var result = await _userManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                TempData["Success"] = "User deleted successfully!";
            }
            else
            {
                TempData["Error"] = "Failed to delete the user.";
            }

            return RedirectToAction("Index");
        }

    }
}

