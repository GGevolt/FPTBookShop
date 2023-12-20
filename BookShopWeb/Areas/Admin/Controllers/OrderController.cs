using FPTBookShop.DataAccess.Repository.IRepository;
using FPTBookShop.Models;
using FPTBookShop.Models.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FPTBookShopWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class OrderController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public OrderController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            OrderVM orderVM = new OrderVM()
            {
               OrderHeaderes = _unitOfWork.OrderHeaderRepository.GetAll().ToList(),
				OrderDetailes = _unitOfWork.OrderDetailRepository.GetAll(includeProperty: "book").ToList()
			};
            return View(orderVM);
        }
    }
}

