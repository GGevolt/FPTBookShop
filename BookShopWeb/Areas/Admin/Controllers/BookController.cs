using FPTBookShopWeb.Data;
using FPTBookShopWeb.Models;
using FPTBookShopWeb.Models.ViewModel;
using FPTBookShopWeb.Repository;
using FPTBookShopWeb.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace FPTBookShopWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles ="Admin")]
    public class BookController : Controller
    {
        //private readonly ApplicationDBContext _dbContext;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webhost;
        public BookController(IUnitOfWork unitOfWork, IWebHostEnvironment webhost)
        {
            _unitOfWork = unitOfWork;
            _webhost = webhost;
        }
        public IActionResult Index()
        {
            List<Book> books = _unitOfWork.BookRepository.GetAll("BookCategories.Category").ToList();
            return View(books);
        }
        public IActionResult CreateUpdate(int? id)
        {
			BookVM bookVM= new BookVM
			{
				Book = new Book(),
				Categories = _unitOfWork.CategoryRepository.GetAll().ToList()
			};
			if (id == null || id == 0)
            {
                //Create new Book
                return View(bookVM);
            }
            else
            {
				//Update a Book
				bookVM.Book = _unitOfWork.BookRepository.Get(book => book.ID == id);
                return View(bookVM);
            }

        }
        [HttpPost]

        public IActionResult CreateUpdate(BookVM bookVM, IFormFile? file)
        {

            if (ModelState.IsValid)
            {
                string wwwRootPath = _webhost.WebRootPath;
                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string bookPath = Path.Combine(wwwRootPath, "img");
                    if (!string.IsNullOrEmpty(bookVM.Book.Book_Image))
                    {
                        //Delete old image
                        var oldImagePath = Path.Combine(wwwRootPath, bookVM.Book.Book_Image.TrimStart('\\'));
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }
                    using (var fileStream = new FileStream(Path.Combine(bookPath, fileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
					bookVM.Book.Book_Image = @"\img\" + fileName;
                }
                if (bookVM.Book.ID == 0)
                {
                    _unitOfWork.BookRepository.Add(bookVM.Book);
                    _unitOfWork.Save();
                    if (bookVM.SelectedCategories != null && bookVM.SelectedCategories.Count > 0)
                    {
                        foreach (int categoryId in bookVM.SelectedCategories)
                        {
                            var category = _unitOfWork.CategoryRepository.Get(c => c.ID == categoryId);
                            if (category != null)
                            {
                                var bookCategory = new BookCategory
                                {
                                    BookId = bookVM.Book.ID,
                                    CategoryId = category.ID
                                };
                                _unitOfWork.BookCategoryRepository.Add(bookCategory);
                            }
                        }
                        _unitOfWork.Save();
                    }
                    TempData["success"] = "Book created succesfully";
                }
                else
                {
                    _unitOfWork.BookRepository.Update(bookVM.Book);
                    _unitOfWork.Save();
                    var existingBook = _unitOfWork.BookRepository.Get(b => b.ID == bookVM.Book.ID, "BookCategories");
                    _unitOfWork.BookCategoryRepository.RemoveRange(existingBook.BookCategories);
                    if (bookVM.SelectedCategories != null && bookVM.SelectedCategories.Count > 0)
                    {
                        foreach (int categoryId in bookVM.SelectedCategories)
                        {
                            var category = _unitOfWork.CategoryRepository.Get(c => c.ID == categoryId);
                            if (category != null)
                            {
                                existingBook.BookCategories.Add(new BookCategory
                                {
                                    CategoryId = category.ID
                                });
                            }
                        }
                        _unitOfWork.Save();
                    }
                    TempData["success"] = "Book updated succesfully";
                }
                return RedirectToAction("Index");
            }
            else
            {
				BookVM bookNewVM = new BookVM
				{
					Book = new Book(),
					Categories = _unitOfWork.CategoryRepository.GetAll().ToList()
				};
				return View(bookNewVM);
            }



        }
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var book = _unitOfWork.BookRepository.Get(book => book.ID == id);
            if (book == null)
            {
                return NotFound();
            }
            return View(book);
        }
        [HttpPost]
        public IActionResult Delete(Book book)
        {
            var existingBook = _unitOfWork.BookRepository.Get(b => b.ID == book.ID, "BookCategories");
            string wwwRootPath = _webhost.WebRootPath;
            if (!string.IsNullOrEmpty(book.Book_Image))
            {
                //Delete old image
                var oldImagePath = Path.Combine(wwwRootPath, book.Book_Image.TrimStart('\\'));
                if (System.IO.File.Exists(oldImagePath))
                {
                    System.IO.File.Delete(oldImagePath);
                }
            }
            _unitOfWork.BookCategoryRepository.RemoveRange(existingBook.BookCategories);
            _unitOfWork.BookRepository.Remove(book);
            _unitOfWork.Save();
            TempData["success"] = "Book deleted succesfully";
            return RedirectToAction("Index");
        }

    }
}
