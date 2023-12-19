using FPTBookShop.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using FPTBookShop.Models.ViewModel;


namespace FPTBookShopWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class AccountController : Controller
	{
        private readonly UserManager<ApplicationUser> _userManager;
		private readonly RoleManager<IdentityRole> _roleManager;

		public AccountController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
		{
			_userManager = userManager;
			_roleManager = roleManager;
		}
        [HttpGet]
        public IActionResult Index(string? sort)
		{
			var userRoles = new List<AccountVM>();
			if (!string.IsNullOrEmpty(sort))
			{
				foreach (var user in _userManager.GetUsersInRoleAsync(sort).GetAwaiter().GetResult())
				{
					List<string> roles = (List<string>)_userManager.GetRolesAsync(user).GetAwaiter().GetResult();
					userRoles.Add(new AccountVM { User = user, Roles = roles });
				}
				return View(userRoles);
			}
			var users = _userManager.Users.ToList();
			foreach (var user in users)
			{
				List<string> roles = (List<string>)_userManager.GetRolesAsync(user).GetAwaiter().GetResult();
				userRoles.Add(new AccountVM { User = user, Roles = roles });
			}

			return View(userRoles);
		}

		public async Task<IActionResult> ChangePasswordAsync(string? id)
		{
			if (id == null || id == "")
			{
				return NotFound();
			}
            var user = await _userManager.FindByIdAsync(id);
			if (user != null)
			{
				ChangePassVM changePassVM = new ChangePassVM()
				{
					userID = user.Id,
					Email = user.Email
				};
                return View(changePassVM);
            }
			return View();
		}

		[HttpPost]
        public async Task<IActionResult> ChangePasswordAsync(ChangePassVM changePassVM)
        {
			if (ModelState.IsValid)
			{
				var user = await _userManager.FindByIdAsync(changePassVM.userID);
				if (user != null)
				{
					string code = await _userManager.GeneratePasswordResetTokenAsync(user);
                    var result = await _userManager.ResetPasswordAsync(user, code, changePassVM.NewPassword.Trim());
					if (result.Succeeded)
					{
						user.SecurityStamp = Guid.NewGuid().ToString();
						await _userManager.UpdateSecurityStampAsync(user);
						TempData["success"] = "Reset Password successfull";
					}
					else
					{
                        foreach (var error in result.Errors)
                        {
                            TempData["error"] += $" {error.Description}";
                        }
                        return View(changePassVM);
                    }
					return RedirectToAction("Index");
				}
			}
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> AssignRole(string userId, string newrole)
		{
			var user = await _userManager.FindByIdAsync(userId);
			if (user == null)
			{
				return NotFound("User not found");
			}
			var currentRoles = await _userManager.GetRolesAsync(user);
			foreach (var role in currentRoles)
			{
				await _userManager.RemoveFromRoleAsync(user, role);
			}
			var result = await _userManager.AddToRoleAsync(user, newrole);
			if (result.Succeeded)
			{
				TempData["success"] = "Role assigned successfully";
			}
			else
			{
				TempData["error"] = "Failed to assign role";
			}
			return RedirectToAction("Index");
		}
	}
}
