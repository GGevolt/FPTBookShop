using FPTBookShop.DataAccess.Repository.IRepository;
using FPTBookShop.Models;
using FPTBookShop.Models.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;
using static System.Reflection.Metadata.BlobBuilder;

namespace FPTBookShopWeb.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IActionResult Index(string? search, string? sort, int currentPage =1)
        {
            int pageSize = 12;
            HomeVM homeVM = new HomeVM() {
                Categories = _unitOfWork.CategoryRepository.GetAll().ToList()
            };
            homeVM.Books = _unitOfWork.BookRepository.GetAll(includeProperty: "BookCategories.Category").ToList();
            if (!string.IsNullOrEmpty(search))
            {
                search = search.ToLower();
                homeVM.Books = _unitOfWork.BookRepository.GetAll(includeProperty:"BookCategories.Category").Where(b => b.Title.ToLower().Contains(search) || b.Author.ToLower().Contains(search)).ToList();
                homeVM.Search = search;
            }
            if (!string.IsNullOrEmpty(sort))
            {
                if (sort == "ViewAll")
                {
                    return RedirectToAction(nameof(Index));
                }
                List<Book> books = _unitOfWork.BookRepository.GetAll(includeProperty: "BookCategories.Category").ToList();
                List<int> bookIDs = _unitOfWork.BookCategoryRepository.GetAll().Where(bc => bc.Category.Name == sort).Select(bc => bc.BookId).ToList();
                homeVM.Books = books.Where(b => bookIDs.Contains(b.ID)).ToList();
                homeVM.Sort = sort;
            }
            var totalRecords = homeVM.Books.Count;
            var totalPages = (totalRecords + pageSize - 1) / pageSize;
            homeVM.Books = homeVM.Books.Skip((currentPage-1)*pageSize).Take(pageSize).ToList();
            homeVM.CurrentPage = currentPage;
            homeVM.TotalPages = totalPages;
            homeVM.PageSize = pageSize;
            return View(homeVM);
        }


        public IActionResult Detail(int id)
        {
            ShoppingCart cart = new()
            {
                book = _unitOfWork.BookRepository.Get(b => b.ID == id, "BookCategories.Category"),
                Count = 1,
                BookID=id,
            };
        
            return View(cart);
        }
        [HttpPost]
        [Authorize(Roles = "Customer")]
        public IActionResult Detail(ShoppingCart shoppingCart)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            shoppingCart.UserID = userId;
            shoppingCart.Id = 0;
            ShoppingCart cartFromDb = _unitOfWork.ShoppingcartRepository.Get(u => u.UserID == userId && u.BookID == shoppingCart.BookID);
            if (cartFromDb != null) 
            {
                cartFromDb.Count += shoppingCart.Count;
                _unitOfWork.ShoppingcartRepository.Update(cartFromDb);
            }
            else
            {
                _unitOfWork.ShoppingcartRepository.Add(shoppingCart);
            }
            /*_unitOfWork.ShoppingcartRepository.Add(shoppingCart);*/
            _unitOfWork.Save();
            TempData["success"] = "Cart Update Sucessfully";
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}