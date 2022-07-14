using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using Projekt.Data;
using Projekt.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Projekt.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _dbContext;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
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
            List<Product> products = _dbContext.Product.ToList();

            if (categoryId != null)
            {
                products =
                    (
                        from product in _dbContext.Product
                        join proCat in _dbContext.ProductCategory on product.Id equals proCat.ProductId
                        where proCat.CategoryId == categoryId
                        select new Product
                        {
                            Id = product.Id,
                            Title = product.Title,
                            Description = product.Description,
                            Quantity = product.Quantity,
                            Price = product.Price
                        }
                    ).ToList();
            }

            ViewBag.Categories = _dbContext.Category.Select
                (
                    c => new SelectListItem
                    {
                        Value = c.Id.ToString(),
                        Text = c.Title
                    }
                );

            return View(products);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
