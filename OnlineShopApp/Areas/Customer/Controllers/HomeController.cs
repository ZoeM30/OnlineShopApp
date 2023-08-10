using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineShopApp.Data;
using OnlineShopApp.Models;
using System.Diagnostics;

namespace OnlineShopApp.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _db;
        public HomeController(ApplicationDbContext db, ILogger<HomeController> logger)
        {
            _db = db;
            _logger = logger;
        }


        public async Task<IActionResult> Index()
		{
            var applicationDbContext = _db.Courses.Include(c => c.Category);
            return View(await applicationDbContext.ToListAsync());
        }

		public IActionResult Details(int courseId)
		{
			ShoppingCart cartObj = new()
			{
				Count = 1,
				CourseId = courseId,
				Course = _db.Courses.Include(c=>c.Category).FirstOrDefault(u => u.Id == courseId),
			};

			return View(cartObj);
		}
		//public async Task<IActionResult> Details(int courseId)
		//{
		//ShoppingCart cartObj = new ShoppingCart();
		//cartObj.Count = 1;
		//cartObj.CourseId = courseId;
		//cartObj.Course = await _db.Courses.Include(c => c.Category).FirstOrDefaultAsync(m => m.Id == courseId);

		//return View(cartObj);
		//}
		[HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
		public async Task<IActionResult> eeee()
		{
			var applicationDbContext = _db.Courses.Include(c => c.Category);
			return View(await applicationDbContext.ToListAsync());
		}
		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}