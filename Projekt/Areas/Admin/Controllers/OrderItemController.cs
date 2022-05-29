using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Projekt.Data;
using Projekt.Models;
using System.Collections.Generic;
using System.Linq;

namespace Projekt.Areas.Admin.Controllers
{
    public class OrderItemController : Controller
    {
        private ApplicationDbContext _dbContext;

        public OrderItemController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            var products = _dbContext.Product.ToList()
                .Select
                (
                    p => new SelectListItem() { Text = p.Title, Value = p.Id.ToString() }
                );

            ViewBag.Products = products;
            ViewBag.ProductsEntity = _dbContext.Product.ToList();

            var productsSlCustom = _dbContext.Product.ToList();
            List<SelectListItem> items = new List<SelectListItem>();

            foreach (var item in productsSlCustom)
            {
                items.Add(new SelectListItem() { Text = item.Title, Value = item.Id.ToString() });
            }

            return View();
        }

        [HttpPost]
        public IActionResult Create(OrderItem orderItem)
        {
            throw new System.NotImplementedException();
        }
    }
}
