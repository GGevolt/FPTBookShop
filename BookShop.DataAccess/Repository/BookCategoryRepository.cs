using FPTBookShop.DataAccess;
using FPTBookShop.Models;
using FPTBookShop.DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace FPTBookShop.DataAccess.Repository
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
