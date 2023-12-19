﻿using FPTBookShop.DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FPTBookShopWeb.Areas.Customer.Controllers
{
    [Area("Customer")]
    [Authorize(Roles = "Customer")]
    public class CartController : Controller
    {
        private readonly IShoppingCartRepository _cartRepo;
        public CartController(IShoppingCartRepository cartRepo)
        {
            _cartRepo = cartRepo;
        }
        public async Task<IActionResult> AddItem(int bookID, int Qty = 1, int redirect = 0)
        {
            var cartCount = await _cartRepo.AddItem(bookID, Qty);
            if (redirect == 0)
            {
                return Ok(cartCount);
            }
            return RedirectToAction("GetUserCart");
        }
        public async Task<IActionResult> RemoveItem(int bookID)
        {
            var cartCount = await _cartRepo.RemoveItem(bookID);
            return RedirectToAction("GetUserCart");
        }
        public async Task<IActionResult> GetUserCart()
        {
            var cart = await _cartRepo.GetUserCart();
            return View(cart);
        }
        public async Task<IActionResult> GetTotalIteminCart()
        {
            int cartItem = await _cartRepo.GetCartItemCount();
            return Ok(cartItem);
        }
    }
}
