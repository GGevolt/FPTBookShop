using FPTBookShop.Models;
using FPTBookShop.DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;

namespace FPTBookShopWeb.Areas.StoreOwner.Controllers
{
    [Area("StoreOwner")]
    [Authorize(Roles = "StoreOwner")]
    public class CategoryRequestController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoryRequestController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            var userid = User.FindFirstValue(ClaimTypes.NameIdentifier);
			List<Request> requests = _unitOfWork.RequestRepository.GetAll().Where(r => r.UserId == userid).ToList();
            return View(requests);
        }

        public IActionResult MakeRequest()
        {
            return View();
        }
        [HttpPost]
        public IActionResult MakeRequest(Request request)
        {
			if (request.Request_Name == request.Request_Description)
			{
				ModelState.AddModelError("Name", "Name can not be equal to Description");
            }
            else
            {
				request.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
				request.Status = "Pending";
				_unitOfWork.RequestRepository.Add(request);
				_unitOfWork.Save();
				TempData["success"] = "Request created succesfully";
				return RedirectToAction("Index");
			}
			return View();
		}
    }
}
