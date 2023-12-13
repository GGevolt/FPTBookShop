using FPTBookShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPTBookShop.DataAccess.Repository.IRepository
{
    public interface IShoppingCartRepository 
    {
        Task<bool> AddItem(int bookID, int Qty);
        Task<bool> RemoveItem(int bookID);
        Task<IEnumerable<ShoppingCart>> GetUserCart();

    }
}
