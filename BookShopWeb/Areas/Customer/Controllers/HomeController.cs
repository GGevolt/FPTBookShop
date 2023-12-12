using FPTBookShop.DataAccess.Repository.IRepository;
using FPTBookShop.Models;
using FPTBookShop.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
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
        public IActionResult Index(string? search, int? cateID)
        {
            HomeVM homeVM = new HomeVM() {
                Categories = _unitOfWork.CategoryRepository.GetAll().ToList()
            };
            if (!string.IsNullOrEmpty(search))
            {
                search = search.ToLower();
                homeVM.Books = _unitOfWork.BookRepository.GetAll("BookCategories.Category").Where(b => b.Title.ToLower().Contains(search) || b.Author.ToLower().Contains(search)).ToList();
                return View(homeVM);
            }
            if (cateID != 0 && cateID != null)
            {
                List<Book> books = _unitOfWork.BookRepository.GetAll("BookCategories.Category").ToList();
                List<int> bookIDs = _unitOfWork.BookCategoryRepository.GetAll().Where(bc => bc.CategoryId == cateID).Select(bc => bc.BookId).ToList();
                homeVM.Books = books.Where(b => bookIDs.Contains(b.ID)).ToList();
                return View(homeVM);
            }
            homeVM.Books = _unitOfWork.BookRepository.GetAll("BookCategories.Category").ToList();
            return View(homeVM);
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