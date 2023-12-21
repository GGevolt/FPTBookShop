using FPTBookShop.DataAccess.Repository.IRepository;
using FPTBookShop.Models;
using FPTBookShop.Models.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FPTBookShopWeb.Areas.StoreOwner.Controllers
{
    [Area("StoreOwner")]
    [Authorize(Roles = "StoreOwner")]
    public class OrderController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public OrderController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

		public IActionResult Index(int currentPage = 1)
		{
			const int pageSize = 5;
			int totalRecords = _unitOfWork.OrderHeaderRepository.GetAll().Count();
			int totalPages = (totalRecords + pageSize - 1) / pageSize;

			// Ensure currentPage is within valid range
			currentPage = Math.Max(1, Math.Min(currentPage, totalPages));

			var orderHeaders = _unitOfWork.OrderHeaderRepository.GetAll()
								.Skip((currentPage - 1) * pageSize)
								.Take(pageSize)
								.ToList();

			var orderDetails = _unitOfWork.OrderDetailRepository.GetAll(includeProperty: "book")
								.Where(od => orderHeaders.Any(oh => oh.Id == od.OrderHeaderID))
								.ToList();

			OrderVM orderVM = new OrderVM()
			{
				OrderHeaderes = orderHeaders,
				OrderDetailes = orderDetails
			};

			ViewBag.CurrentPage = currentPage;
			ViewBag.TotalPages = totalPages;
			ViewBag.PageSize = pageSize;

			return View(orderVM);
		}
	}
}

