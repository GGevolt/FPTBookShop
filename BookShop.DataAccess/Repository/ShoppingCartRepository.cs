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
    public class ShoppingCartRepository: Repository<ShoppingCart>, IShoppingCartRepository
    {
        private readonly ApplicationDBContext _dbContext;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IHttpContextAccessor _httpcontextAccessor;
        public ShoppingCartRepository(ApplicationDBContext dBContext, UserManager<IdentityUser> userManager, IHttpContextAccessor httpcontextAccessor) : base(dBContext)
        {
            _dbContext = dBContext;
            _userManager = userManager;
            _httpcontextAccessor = httpcontextAccessor;
        }
        public async Task<int> AddItem(int bookID, int Qty)
        {
            string UserID = GetUserId();
            using var transaction = _dbContext.Database.BeginTransaction();
            try
            {
                if (string.IsNullOrEmpty(UserID))
                    throw new Exception("User is not loggin");
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
            }
            catch (Exception ex)
            { 
            }
            var cartItemCount = await GetCartItemCount(UserID);
            return cartItemCount;
        }
        public async Task<int> RemoveItem(int bookID)
        {
            string UserID = GetUserId();
            //using var transaction = _dbContext.Database.BeginTransaction();
            try
            {
                if (string.IsNullOrEmpty(UserID))
                    throw new Exception("User is not loggin");
                var cart = await GetCart(UserID);
                if (cart is null)               
                    throw new Exception("Invalid Cart");
               var cartItem = _dbContext.CartDetails.FirstOrDefault(a => a.ShoppingCart_ID == cart.Id && a.BookID == bookID);
                if (cartItem is null)              
                    throw new Exception("There is no Item in cart");
                else if (cartItem.Quantity == 1)                
                    _dbContext.CartDetails.Remove(cartItem);               
                else               
                    cartItem.Quantity = cartItem.Quantity - 1;               
                _dbContext.SaveChanges();
                //transaction.Commit();
   
            }
            catch (Exception ex)
            {
   
            }
            var cartItemCount = await GetCartItemCount(UserID);
            return cartItemCount;
        }
        public async Task<ShoppingCart> GetUserCart()
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
                .FirstOrDefaultAsync();
            return shoppingCart;
        }
        public async Task<ShoppingCart> GetCart(string UserID)
        {
            var cart = await _dbContext.ShoppingCarts.FirstOrDefaultAsync(x => x.AccountID == UserID);
            return cart;
        }
        public async Task<int> GetCartItemCount(string UserID = "")
        {
            if(!string.IsNullOrEmpty(UserID))
            {
                UserID = GetUserId();
            }
            var data = await (from cart in _dbContext.ShoppingCarts
                              join CartDetail in _dbContext.CartDetails
                              on cart.Id equals CartDetail.ShoppingCart_ID
                              select new { CartDetail.Id }
                              ).ToListAsync();
            return data.Count;
        }
        private string GetUserId()
        {
            var principal = _httpcontextAccessor.HttpContext.User;
            var UserID = _userManager.GetUserId(principal);
            return UserID;
        }
    }
}
