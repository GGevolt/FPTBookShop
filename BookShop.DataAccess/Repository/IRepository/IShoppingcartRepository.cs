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
        Task<int> AddItem(int bookID, int Qty);
        Task<int> RemoveItem(int bookID);
        Task<ShoppingCart> GetUserCart();
        Task<int> GetCartItemCount(string UserID = "");
        Task<ShoppingCart> GetCart(string UserID);

    }
}
