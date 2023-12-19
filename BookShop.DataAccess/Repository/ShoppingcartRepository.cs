using FPTBookShop.DataAccess;
using FPTBookShop.Models;
using FPTBookShop.DataAccess.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace FPTBookShop.DataAccess.Repository
{
    public class ShoppingcartRepository : Repository<ShoppingCart>,IShoppingcartRepository
    {
        private readonly ApplicationDBContext _dbContext;
        public ShoppingcartRepository(ApplicationDBContext dBContext):base(dBContext)
        {
            _dbContext = dBContext;       
        }

        public void Update(ShoppingCart shoppingCart)
        {
            _dbContext.Update(shoppingCart);
        }
	}
}
