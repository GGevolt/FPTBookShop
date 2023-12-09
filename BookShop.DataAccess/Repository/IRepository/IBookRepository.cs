using FPTBookShop.Models;

namespace FPTBookShop.DataAccess.Repository.IRepository
{
    public interface IBookRepository:IRepository<Book>
    {
        void Update(Book book);

	}
}
