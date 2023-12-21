using FPTBookShop.DataAccess.Repository.IRepository;
using FPTBookShop.Models;
using FPTBookShop.Models.ViewModel;
using FPTBookShop.Utitlity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FPTBookShopWeb.Areas.Customer.Controllers
{
	[Area("Customer")]
	[Authorize(Roles = "Customer")]
	public class CartController : Controller
	{

		private readonly IUnitOfWork _unitOfWork;
		[BindProperty]
		public ShoppingcartVM ShoppingcartVM { get; set; }
		public CartController(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;

		}
		public IActionResult Index()
		{
			var claimsIdentity = (ClaimsIdentity)User.Identity;
			var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
			ShoppingcartVM = new()
			{
				ShoppingcartList = _unitOfWork.ShoppingcartRepository.GetAll(filter: u => u.UserID == userId, includeProperty: "book"),
				OrderHeader = new()
			};
			foreach (var cart in ShoppingcartVM.ShoppingcartList)
			{
				cart.Price = PriceofBook(cart);
				ShoppingcartVM.OrderHeader.OrderTotal += (cart.Price * cart.Count);
			}
			return View(ShoppingcartVM);
		}
		public IActionResult Summary()
		{
			var claimsIdentity = (ClaimsIdentity)User.Identity;
			var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
			ShoppingcartVM = new()
			{
				ShoppingcartList = _unitOfWork.ShoppingcartRepository.GetAll(filter: u => u.UserID == userId, includeProperty: "book"),
				OrderHeader = new()
			};
			ShoppingcartVM.OrderHeader.User = _unitOfWork.ApplicationUserRepository.Get(u => u.Id == userId);

			ShoppingcartVM.OrderHeader.Name = ShoppingcartVM.OrderHeader.User.Full_Name;
			ShoppingcartVM.OrderHeader.PhoneNumber = ShoppingcartVM.OrderHeader.User.PhoneNumber;
			ShoppingcartVM.OrderHeader.Address = ShoppingcartVM.OrderHeader.User.Address;
			ShoppingcartVM.OrderHeader.City = ShoppingcartVM.OrderHeader.User.City;
			foreach (var cart in ShoppingcartVM.ShoppingcartList)
			{
				cart.Price = PriceofBook(cart);
				ShoppingcartVM.OrderHeader.OrderTotal += (cart.Price * cart.Count);
			}
			return View(ShoppingcartVM);
		}
		[HttpPost]
		[ActionName("Summary")]
		public IActionResult SummaryPost()
		{
			var claimsIdentity = (ClaimsIdentity)User.Identity;
			var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;

			ShoppingcartVM.ShoppingcartList = _unitOfWork.ShoppingcartRepository.GetAll(filter: u => u.UserID == userId, includeProperty: "book");
			ShoppingcartVM.OrderHeader.OrderDate = System.DateTime.Now;
			ShoppingcartVM.OrderHeader.UserId = userId;

			ApplicationUser applicationUser = _unitOfWork.ApplicationUserRepository.Get(u => u.Id == userId);

			foreach (var cart in ShoppingcartVM.ShoppingcartList)
			{
				cart.Price = PriceofBook(cart);
				ShoppingcartVM.OrderHeader.OrderTotal += (cart.Price * cart.Count);
            }

			ShoppingcartVM.OrderHeader.PaymentStatus = SD.PaymentStatusApprove;
			ShoppingcartVM.OrderHeader.OrderStatus = SD.StatusApprove;

			_unitOfWork.OrderHeaderRepository.Add(ShoppingcartVM.OrderHeader);
			_unitOfWork.Save();

			foreach (var cart in ShoppingcartVM.ShoppingcartList)
			{
				if (cart.Count > cart.book.Quantity) {
                    _unitOfWork.ShoppingcartRepository.Remove(cart);
                    TempData["error"] = $"{cart.book.Title} is out of stock, Sombody already order the last stock";
                    _unitOfWork.Save();
                    return RedirectToAction(nameof(Index));
                }
				OrderDetail orderDetail = new()
				{
					ProductID = cart.BookID,
					OrderHeaderID = ShoppingcartVM.OrderHeader.Id,
					Price = cart.Price,
					Count = cart.Count
				};
                cart.book.Quantity -= cart.Count;
                _unitOfWork.BookRepository.Update(cart.book);
                _unitOfWork.OrderDetailRepository.Add(orderDetail);
				_unitOfWork.Save();
			}
            foreach (var cart in ShoppingcartVM.ShoppingcartList)
			{
                _unitOfWork.ShoppingcartRepository.Remove(cart);
            }
            _unitOfWork.Save();
            return RedirectToAction(nameof(OrderConfirm), new {id = ShoppingcartVM.OrderHeader.Id});
		}
		public IActionResult OrderConfirm(int id)
		{
			return View(id);
		}
		public IActionResult Plus(int cartID)
		{
			var cartFromdDb = _unitOfWork.ShoppingcartRepository.Get(u => u.Id == cartID, includeProperty: "book");
			if (cartFromdDb.Count < cartFromdDb.book.Quantity)
			{
				cartFromdDb.Count += 1;
				_unitOfWork.ShoppingcartRepository.Update(cartFromdDb);
				_unitOfWork.Save();
			}
            else
            {
                TempData["error"] = "Out of stock can't add more";
            }
            return RedirectToAction(nameof(Index));
		}
		public IActionResult Minus(int cartID)
		{
			var cartFromdDb = _unitOfWork.ShoppingcartRepository.Get(u => u.Id == cartID);
			if (cartFromdDb.Count <= 1)
			{
				_unitOfWork.ShoppingcartRepository.Remove(cartFromdDb);
			}
			else
			{
				cartFromdDb.Count -= 1;
				_unitOfWork.ShoppingcartRepository.Update(cartFromdDb);
			}
			_unitOfWork.Save();
			return RedirectToAction(nameof(Index));
		}
		public IActionResult Remove(int cartID)
		{
			var cartFromdDb = _unitOfWork.ShoppingcartRepository.Get(u => u.Id == cartID);
			_unitOfWork.ShoppingcartRepository.Remove(cartFromdDb);
			_unitOfWork.Save();
			return RedirectToAction(nameof(Index));
		}


		private double PriceofBook(ShoppingCart shoppingcart)
		{
			return shoppingcart.book.Price;
		}
	}
}
