using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineShopApp.Data;
using OnlineShopApp.Models.ViewModels;
using System.Security.Claims;

namespace OnlineShopApp.Areas.Customer.Controllers
{
    [Area("Customer")]
    [Authorize]
    public class CartController : Controller
    {
        private readonly ApplicationDbContext _db;
        public ShoppingCartVM shoppingCartVM { get; set; }
        public CartController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            shoppingCartVM = new ShoppingCartVM()
            {
                ListCart = _db.ShoppingCarts.Include(c => c.Course).Where(u => u.ApplicationUserId == claim.Value)
            };
            foreach(var cart in shoppingCartVM.ListCart)
            {
                cart.Price = cart.Course.Price;
                shoppingCartVM.CartTotal+=(cart.Price * cart.Count);
            }
            
            return View(shoppingCartVM);

        }

        public IActionResult Plus(int cartId)
        {
            var cart = _db.ShoppingCarts.FirstOrDefault(u=>u.Id==cartId);
            cart.Count++;
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Minus(int cartId)
        {
            var cart = _db.ShoppingCarts.FirstOrDefault(u => u.Id == cartId);
            if (cart.Count <= 1)
            {
                _db.ShoppingCarts.Remove(cart);
            }
            else
            {
                cart.Count--;

            }
            _db.SaveChanges();
            return RedirectToAction("Index");
        }


        public IActionResult Remove(int cartId)
        {
            var cart = _db.ShoppingCarts.FirstOrDefault(u => u.Id == cartId);
            _db.ShoppingCarts.Remove(cart);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}
