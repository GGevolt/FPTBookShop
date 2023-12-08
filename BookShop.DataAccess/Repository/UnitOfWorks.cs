using FPTBookShopWeb.Data;
using FPTBookShopWeb.Repository.IRepository;

namespace FPTBookShopWeb.Repository
{
    public class UnitOfWorks : IUnitOfWork
    {
        private readonly ApplicationDBContext _dbContext;
        public ICategoryRepository CategoryRepository {  get;private set; }

        public IBookRepository BookRepository { get; private set; }
        public IBookCategoryRepository BookCategoryRepository { get; private set; }

        public UnitOfWorks(ApplicationDBContext dBContext)
        {
            _dbContext = dBContext;
            CategoryRepository = new CategoryRepository(dBContext);
            BookRepository = new BookRepository(dBContext);
            BookCategoryRepository = new BookCategoryRepository(dBContext);
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }
    }
}
