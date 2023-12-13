using FPTBookShop.DataAccess.Repository.IRepository;
using FPTBookShop.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPTBookShop.DataAccess.Repository
{
    public class ShoppingCartRepository: IShoppingCartRepository
    {
        private readonly ApplicationDBContext _dbContext;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IHttpContextAccessor _httpcontextAccessor;
        public ShoppingCartRepository(ApplicationDBContext dBContext, UserManager<IdentityUser> userManager, IHttpContextAccessor httpcontextAccessor)
        {
            _dbContext = dBContext;
            _userManager = userManager;
            _httpcontextAccessor = httpcontextAccessor;
        }
        public async Task<bool> AddItem(int bookID, int Qty)
        {
            using var transaction = _dbContext.Database.BeginTransaction();
            try
            {
                string UserID = GetUserId();
                if (string.IsNullOrEmpty(UserID))
                    return false;
                var cart = await GetCart(UserID);
                if (cart is null)
                {
                    cart = new ShoppingCart
                    {
                        AccountID = UserID
                    };
                    _dbContext.ShoppingCarts.Add(cart);
                }
                _dbContext.SaveChanges();
                var cartItem = _dbContext.CartDetails.FirstOrDefault(a => a.ShoppingCart_ID == cart.Id && a.BookID == bookID);
                if (cartItem is not null)
                {
                    cartItem.Quantity += Qty;
                }
                else
                {
                    cartItem = new CartDetail
                    {
                        BookID = bookID,
                        ShoppingCart_ID = cart.Id,
                        Quantity = Qty
                    };
                    _dbContext.CartDetails.Add(cartItem);
                }
                _dbContext.SaveChanges();
                transaction.Commit();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public async Task<bool> RemoveItem(int bookID)
        {
            //using var transaction = _dbContext.Database.BeginTransaction();
            try
            {
                string UserID = GetUserId();
                if (string.IsNullOrEmpty(UserID))
                    return false;
                var cart = await GetCart(UserID);
                if (cart is null)
                {
                    return false;
                }
                var cartItem = _dbContext.CartDetails.FirstOrDefault(a => a.ShoppingCart_ID == cart.Id && a.BookID == bookID);
                if (cartItem is null)
                {
                    return false;
                }
                else if (cartItem.Quantity == 1)
                {
                    _dbContext.CartDetails.Remove(cartItem);
                }
                else
                {
                    cartItem.Quantity = cartItem.Quantity - 1;
                }
                _dbContext.SaveChanges();
                //transaction.Commit();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public async Task<IEnumerable<ShoppingCart>> GetUserCart()
        {
            var UserID = GetUserId();
            if(UserID == null)
            {
                throw new Exception("Invalid request");
            }
            var shoppingCart = await _dbContext.ShoppingCarts.Include(a=>a.CartsDetails)
                .ThenInclude(a=>a.Book)
                .ThenInclude(a => a.BookCategories)
                .Where(a => a.AccountID == UserID )
                .ToListAsync();
            return shoppingCart;
        }
 

        private async Task<ShoppingCart> GetCart(string UserID)
        {
            var cart = await _dbContext.ShoppingCarts.FirstOrDefaultAsync(x => x.AccountID == UserID);
            return cart;
        }
        private string GetUserId()
        {
            var principal = _httpcontextAccessor.HttpContext.User;
            var UserID = _userManager.GetUserId(principal);
            return UserID;
        }
    }
}
