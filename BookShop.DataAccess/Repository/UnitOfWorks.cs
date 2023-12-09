using FPTBookShop.DataAccess;
using FPTBookShop.DataAccess.Repository.IRepository;

namespace FPTBookShop.DataAccess.Repository
{
    public class UnitOfWorks : IUnitOfWork
    {
        private readonly ApplicationDBContext _dbContext;
        public ICategoryRepository CategoryRepository {  get;private set; }

        public IBookRepository BookRepository { get; private set; }
        public IBookCategoryRepository BookCategoryRepository { get; private set; }
        public IRequestRepository RequestRepository { get; private set; }

        public UnitOfWorks(ApplicationDBContext dBContext)
        {
            _dbContext = dBContext;
            CategoryRepository = new CategoryRepository(dBContext);
            BookRepository = new BookRepository(dBContext);
            BookCategoryRepository = new BookCategoryRepository(dBContext);
            RequestRepository = new RequestRepository(dBContext);
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }
    }
}
