using FPTBookShop.DataAccess;
using FPTBookShop.Models;
using FPTBookShop.DataAccess.Repository.IRepository;

namespace FPTBookShop.DataAccess.Repository
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
