using FPTBookShop.Models;

namespace FPTBookShop.DataAccess.Repository.IRepository
{
    public interface IBookCategoryRepository : IRepository<BookCategory>
    {
        void Update(BookCategory bookCategory);
	}
}
