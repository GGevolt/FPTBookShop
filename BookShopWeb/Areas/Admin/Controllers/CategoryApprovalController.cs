using FPTBookShop.DataAccess.Repository.IRepository;
using FPTBookShop.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FPTBookShopWeb.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Authorize(Roles = "Admin")]
	public class CategoryApprovalController : Controller
	{
		private readonly IUnitOfWork _unitOfWork;
		public CategoryApprovalController(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}
		[HttpGet]
		public IActionResult Index()
		{
			List<Request> requests = _unitOfWork.RequestRepository.GetAll().Where(r=>r.Status == "Pending").ToList();
			return View(requests);
		}
		[HttpPost]
		public IActionResult ApproveRequest(int? id)
		{
			if (id == null || id == 0)
			{
				return NotFound();
			}
			var request = _unitOfWork.RequestRepository.Get(r => r.ID == id);
            if (request != null)
            {
				Category categoryNew = new()
				{
					Name = request.Request_Name,
					Description = request.Request_Description,
				};
				request.Status = "Approved";
				_unitOfWork.CategoryRepository.Add(categoryNew);
				_unitOfWork.RequestRepository.Update(request);
				_unitOfWork.Save();
				TempData["success"] = "Request Approved";
            }
            else
            {
                TempData["error"] = "Request not found";
            }
            return RedirectToAction("Index");
		}

		[HttpPost]
		public IActionResult RejectRequest(int? id)
		{
			if (id == null || id == 0)
			{
				return NotFound();
			}
			var request = _unitOfWork.RequestRepository.Get(r => r.ID == id);
            if (request != null)
            {
                request.Status = "Rejected";
                _unitOfWork.RequestRepository.Update(request);
                _unitOfWork.Save();
                TempData["success"] = "Request Rejected";
            }
            else
            {
                TempData["error"] = "Request not found";
            }
            return RedirectToAction("Index");
		}
	}
}
