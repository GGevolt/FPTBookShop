using FPTBookShop.Models;
using FPTBookShop.DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FPTBookShopWeb.Areas.StoreOwner.Controllers
{
    [Area("StoreOwner")]
    [Authorize(Roles = "StoreOwner")]
    public class OwnerController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public OwnerController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index(IUnitOfWork unitOfWork)
        {
            return View();
        }

        public IActionResult MakeRequest()
        {
            return View();
        }
        [HttpPost]
        public IActionResult MakeRequest(Request request)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.RequestRepository.Add(request);
                _unitOfWork.Save();
                TempData["success"] = "Request created succesfully";
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}
