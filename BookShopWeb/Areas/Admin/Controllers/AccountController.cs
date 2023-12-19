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

        public AccountController(UserManager<ApplicationUser> userManager)
		{
			_userManager = userManager;
		}
        [HttpGet]
        public IActionResult Index(string? sort)
		{
			if (!string.IsNullOrEmpty(sort))
			{
                return View(_userManager.GetUsersInRoleAsync(sort).GetAwaiter().GetResult());
            }
            var users = _userManager.Users;
            return View(users);
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
    }
}
