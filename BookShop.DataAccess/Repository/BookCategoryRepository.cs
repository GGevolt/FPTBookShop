using FPTBookShopWeb.Data;
using FPTBookShopWeb.Models;
using FPTBookShopWeb.Repository.IRepository;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace FPTBookShopWeb.Repository
{
    internal class BookCategoryRepository : Repository<BookCategory>, IBookCategoryRepository
    {
        private readonly ApplicationDBContext _dbContext;
        public BookCategoryRepository(ApplicationDBContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public void Update(BookCategory bookCategory)
        {
            _dbContext.Update(bookCategory);
        }
	}
}
