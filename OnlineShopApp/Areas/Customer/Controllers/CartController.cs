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
            
            return View(shoppingCartVM);

        }
    }
}
