using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Projekt.Data;
using Projekt.Models;
using System.Linq;

namespace Projekt.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles ="Admin")]
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _dbContext;

        public ProductController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IActionResult Index()
        {
            return View(_dbContext.Product.ToList());
        }

        public IActionResult Details(int id)
        {
            if (id == default(int))
            {
                return RedirectToAction("PageNotFound", "HttpStatusCodes", new { source = "Id is not valid!"});
            }

            var product = _dbContext.Product.FirstOrDefault(p => p.Id == id);

            if (product == null)
            {
                return RedirectToAction("PageNotFound", "HttpStatusCodes", new {source = "No product in database!"});
            }

            return View(product);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                _dbContext.Product.Add(product);
                _dbContext.SaveChanges();

                return RedirectToAction(nameof(Index));
            }

            return View(product);
        }

        //public IActionResult Edit(int id)
        //{
        //    var product = _dbContext.Product.Find(id);

        //    return View(product);
        //}

        //[HttpPost]
        //public IActionResult Edit(int id, Product product)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _dbContext.Update(product);
        //        _dbContext.SaveChanges();

        //        return RedirectToAction(nameof(Index));
        //    }

        //    return View(product);
        //}

        public IActionResult Delete(int id)
        {
            var product = _dbContext.Product.FirstOrDefault(p => p.Id == id);

            return View(product);
        }

        [HttpPost]
        [ActionName("Delete")]
        public IActionResult DeleteProduct(int id)
        {
            var product = _dbContext.Product.Find(id);
            _dbContext.Product.Remove(product);
            _dbContext.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
    }
}
