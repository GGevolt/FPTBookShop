using FPTBookShop.Models;
                                                
namespace FPTBookShop.DataAccess.Repository.IRepository
{
    public interface IShoppingcartRepository:IRepository<ShoppingCart>
    {
        void Update(ShoppingCart shoppingCart);
    }
}
