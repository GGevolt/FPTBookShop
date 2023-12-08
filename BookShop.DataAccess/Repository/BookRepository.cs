using FPTBookShopWeb.Data;
using FPTBookShopWeb.Models;
using FPTBookShopWeb.Repository.IRepository;

namespace FPTBookShopWeb.Repository
{
    public class BookRepository : Repository<Book>, IBookRepository
    {   
        private readonly ApplicationDBContext _dbContext;
        public BookRepository(ApplicationDBContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public void Update(Book book)
        {
            _dbContext.Update(book);
        }
    }
}
