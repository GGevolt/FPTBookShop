using FPTBookShop.DataAccess;
using FPTBookShop.Models;
using FPTBookShop.Models.ViewModel;
using FPTBookShop.DataAccess.Repository;
using FPTBookShop.DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace FPTBookShopWeb.Areas.StoreOwner.Controllers
{
    [Area("StoreOwner")]
    [Authorize(Roles = "StoreOwner")]
    public class BookController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webhost;
        public BookController(IUnitOfWork unitOfWork, IWebHostEnvironment webhost)
        {
            _unitOfWork = unitOfWork;
            _webhost = webhost;
        }
		public IActionResult Index(int currentPage = 1) 
		{
			const int pageSize = 5;
			int totalRecords = _unitOfWork.BookRepository.GetAll().Count();
			int totalPages = (totalRecords + pageSize - 1) / pageSize;

			// Ensure currentPage is within valid range
			currentPage = Math.Max(1, Math.Min(currentPage, totalPages));

			var books = _unitOfWork.BookRepository.GetAll(includeProperty: "BookCategories.Category")
							.Skip((currentPage - 1) * pageSize)
							.Take(pageSize)
							.ToList();

			ViewBag.CurrentPage = currentPage;
			ViewBag.TotalPages = totalPages;
			ViewBag.PageSize = pageSize;

			return View(books);
		}
		public IActionResult CreateUpdate(int? id)
        {
            if (id == null || id == 0)
            {
                BookVM bookCreateVM = new BookVM
                {
                    Book = new Book(),
                    Categories = _unitOfWork.CategoryRepository.GetAll().ToList()
                };
                //Create new Book
                return View(bookCreateVM);
            }
            else
            {
                BookVM bookUpdateVM = new BookVM
                {
                    Book = _unitOfWork.BookRepository.Get(book => book.ID == id),
                    Categories = _unitOfWork.CategoryRepository.GetAll().ToList(),
                    SelectedCategories = _unitOfWork.BookCategoryRepository.GetAll().Where(bc => bc.BookId == id).Select(bc => bc.CategoryId).ToList()
                };
                //Update a Book
                bookUpdateVM.Book = _unitOfWork.BookRepository.Get(book => book.ID == id);

                return View(bookUpdateVM);
            }

        }
        [HttpPost]

        public IActionResult CreateUpdate(BookVM bookVM, IFormFile? file)
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
                    var PastBookCategories = _unitOfWork.BookCategoryRepository.GetAll().Where(bc => bc.BookId == bookVM.Book.ID).ToList();
                    foreach (var bookCategory in PastBookCategories)
                    {
                        _unitOfWork.BookCategoryRepository.Remove(bookCategory);
                    }

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
                    TempData["success"] = "Book updated succesfully";
                }
                return RedirectToAction("Index");
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
            var bookCate = _unitOfWork.BookCategoryRepository.GetAll().Where(bc => bc.BookId == book.ID).ToList();
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
            foreach (var bookCategory in bookCate)
            {
                _unitOfWork.BookCategoryRepository.Remove(bookCategory);
            }
            _unitOfWork.BookRepository.Remove(book);
            _unitOfWork.Save();
            TempData["success"] = "Book deleted succesfully";
            return RedirectToAction("Index");
        }
    }
}
