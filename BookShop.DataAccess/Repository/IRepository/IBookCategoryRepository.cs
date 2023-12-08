using FPTBookShopWeb.Models;

namespace FPTBookShopWeb.Repository.IRepository
{
    public interface IBookCategoryRepository : IRepository<BookCategory>
    {
        void Update(BookCategory bookCategory);
	}
}
