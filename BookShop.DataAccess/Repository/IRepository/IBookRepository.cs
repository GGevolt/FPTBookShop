using FPTBookShopWeb.Models;

namespace FPTBookShopWeb.Repository.IRepository
{
    public interface IBookRepository:IRepository<Book>
    {
        void Update(Book book);

	}
}
