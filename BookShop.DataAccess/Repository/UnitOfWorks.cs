using FPTBookShop.DataAccess;
using FPTBookShop.DataAccess.Repository.IRepository;
using FPTBookShop.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace FPTBookShop.DataAccess.Repository
{
    public class UnitOfWorks : IUnitOfWork
    {
       
           
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ApplicationDBContext _dbContext;
        public ICategoryRepository CategoryRepository {  get;private set; }

        public IBookRepository BookRepository { get; private set; }
        public IBookCategoryRepository BookCategoryRepository { get; private set; }
        public IRequestRepository RequestRepository { get; private set; }
        public IShoppingCartRepository ShoppingCartRepository { get; private set; }

        public UnitOfWorks(ApplicationDBContext dBContext, UserManager<ApplicationUser> userManager, IHttpContextAccessor httpcontextAccessor)
        {
            _dbContext = dBContext;
            CategoryRepository = new CategoryRepository(dBContext);
            BookRepository = new BookRepository(dBContext);
            BookCategoryRepository = new BookCategoryRepository(dBContext);
            RequestRepository = new RequestRepository(dBContext);
            ShoppingCartRepository = new ShoppingCartRepository(dBContext, userManager, httpcontextAccessor);
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }
    }
}
