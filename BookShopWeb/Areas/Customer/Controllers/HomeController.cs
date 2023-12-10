using FPTBookShop.DataAccess.Repository.IRepository;
using FPTBookShop.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

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
        public IActionResult Index(string? search)
        {
            if (!string.IsNullOrEmpty(search))
            {
                search = search.ToLower();
                return View(_unitOfWork.BookRepository.GetAll("BookCategories.Category").Where(b => b.Title.ToLower().Contains(search) || b.Author.ToLower().Contains(search)).ToList());
            }
            List<Book> books = _unitOfWork.BookRepository.GetAll("BookCategories.Category").ToList();
            return View(books);
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