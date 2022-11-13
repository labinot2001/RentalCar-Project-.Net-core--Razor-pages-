using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RentalCar.Data;
using RentalCar.Data.Cart;
using RentalCar.Data.Services;
using RentalCar.Data.ViewModels;
using RentalCar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace RentalCar.Controllers
{
    public class OrdersController : Controller
    {
        private readonly ApplicationUser _applicationUser;
        private readonly ICarsService _carsService;
        private readonly ShoppingCart _shoppingCart;
        private readonly IOrdersService _ordersService;
        private readonly UserManager<ApplicationUser> _userManager;

        private readonly AppDbContext _context;
        public OrdersController(AppDbContext context, UserManager<ApplicationUser> userManager, ICarsService carsService, ShoppingCart shoppingCart, IOrdersService ordersService)
        {
            _carsService = carsService;
            _shoppingCart = shoppingCart;
            _ordersService = ordersService;
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {

            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            string userRole = User.FindFirstValue(ClaimTypes.Role);

            var orders = await _ordersService.GetOrdersByUserIdAndRoleAsync(userId, userRole);
            return View(orders);
        }
        public async Task<IActionResult> IndexUser()
        {
            ViewBag.Current = "Offers";

            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            string userRole = User.FindFirstValue(ClaimTypes.Role);

            var orders = await _ordersService.GetOrdersByUserIdAndRoleAsync(userId, userRole);
            return View(orders);
        }
        
        public async Task<IActionResult> ShoppingCart()
        {
            ViewBag.Current = "ShoppingCart";
            //string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);


            var items = _shoppingCart.GetShoppingCartItems();
            _shoppingCart.ShoppingCartItems = items;
            var response = new ShoppingCartVM
            {

                //ApplicationUser = await _userManager.FindByIdAsync(userId),
                ShoppingCart = _shoppingCart,
                ShoppingCartTotal = _shoppingCart.GetShoppingCartTotal()
            };


            return View(response);
        }
        public async Task<IActionResult> AddItemToShoppingCart(int id)
        {
            var item = await _carsService.GetCarByIdAsync(id);

            if (item != null)
            {
                _shoppingCart.AddItemToCart(item);
            }
            return RedirectToAction(nameof(ShoppingCart));
        }

        public async Task<IActionResult> RemoveItemFromShoppingCart(int id)
        {
            var item = await _carsService.GetCarByIdAsync(id);

            if (item != null)
            {
                _shoppingCart.RemoveItemFromCart(item);
            }
            return RedirectToAction(nameof(ShoppingCart));
        }
        public IActionResult CompleteOrder()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> CompleteOrder(int id)
        {
            var items = _shoppingCart.GetShoppingCartItems();
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            string userEmailAddress = User.FindFirstValue(ClaimTypes.Email);

            //await _ordersService.StoreOrderAsync(items, userId, userEmailAddress);
            //await _shoppingCart.ClearShoppingCartAsync();
            //int scopeId = _context.Orders.Count();
            return RedirectToAction("EditUser", "Account", new { id = userId });
        }

        public async Task<IActionResult> Invoice()
        {
            ViewBag.Current = "Offers";

            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            string userRole = User.FindFirstValue(ClaimTypes.Role);

            var orders = await _ordersService.GetOrdersByUserIdAndRoleAsync(userId, userRole);
            return View(orders);
        }
        //ThenInclude(n => n.Car).Include(n => n.User).
        //                     ThenInclude(n => n.Address).Include(n => n.User).
        //                     ThenInclude(n => n.Payment)
    }
}
