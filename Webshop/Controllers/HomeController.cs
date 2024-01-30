using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Webshop.Data;
using Webshop.Models;

namespace Webshop.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Product(int? categoryId)
        {
            List<Product> products = _context.Product.ToList();

            foreach (var product in products)
            {
                product.ProductImages = _context.ProductImage.Where(pi => pi.ProductId == product.Id).ToList();
                product.ProductCategories = _context.ProductCategory.Where(pc => pc.ProductId == product.Id).ToList();
            }

            if (categoryId != null)
            {
                products = products.Where(p => p.ProductCategories.Any(p => p.CategoryId == categoryId)).ToList();
            }

            ViewBag.Categories = _context.Category.ToList();
            return View(products);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
