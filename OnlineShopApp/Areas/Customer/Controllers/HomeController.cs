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

        public async Task<IActionResult> Details(int courseId)
        {
            var course = await _db.Courses.Include(c => c.Category).FirstOrDefaultAsync(m => m.Id == courseId);
            if (course == null)
            {
                return NotFound();
            }
            ShoppingCart cartObj = new()
            {
                Count = 1,
                CourseId = courseId,
                Course = course
            };

            return View(cartObj);

		}

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}