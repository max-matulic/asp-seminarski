using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Projekt.Data;
using Projekt.Models;
using System.Linq;

namespace Projekt.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class ProductCategoryController : Controller
    {
        private readonly ApplicationDbContext _dbContext;

        public ProductCategoryController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IActionResult Index(int productId)
        {
            var productCategories = (from prodCat in _dbContext.ProductCategory
                                     where prodCat.ProductId == productId
                                     select new ProductCategory
                                     {
                                         Id = prodCat.Id,
                                         ProductId = prodCat.ProductId,
                                         ProductTitle = (from p in _dbContext.Product where p.Id == prodCat.ProductId select p.Title).FirstOrDefault(),
                                         CategoryId = prodCat.CategoryId,
                                         CategoryTitle = (from c in _dbContext.Category where c.Id == prodCat.CategoryId select c.Title).FirstOrDefault()
                                     }).ToList();

            ViewBag.ProductId = productId;

            return View(productCategories);
        }
    }
}
