using FPTBookShop.DataAccess;
using FPTBookShop.DataAccess.Repository.IRepository;
using FPTBookShop.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace FPTBookShop.DataAccess.Repository
{
    public class UnitOfWorks : IUnitOfWork
    {
        private readonly ApplicationDBContext _dbContext;
        public ICategoryRepository CategoryRepository {  get;private set; }

        public IBookRepository BookRepository { get; private set; }
        public IBookCategoryRepository BookCategoryRepository { get; private set; }
        public IRequestRepository RequestRepository { get; private set; }
        public IShoppingcartRepository ShoppingcartRepository { get; private set; }
        public IApplicationUserRepository ApplicationUserRepository { get; private set; }
        public UnitOfWorks(ApplicationDBContext dBContext)
        {
            _dbContext = dBContext;
            CategoryRepository = new CategoryRepository(dBContext);
            BookRepository = new BookRepository(dBContext);
            BookCategoryRepository = new BookCategoryRepository(dBContext);
            RequestRepository = new RequestRepository(dBContext);
            ShoppingcartRepository = new ShoppingcartRepository(dBContext);
            ApplicationUserRepository = new ApplicationUserRepository(dBContext);
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }
    }
}
