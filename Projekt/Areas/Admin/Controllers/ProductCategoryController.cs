using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Projekt.Data;
using Projekt.Models;
using System.Collections.Generic;
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

        public IActionResult Create(int productId)
        {
            ViewBag.ProductId = productId;
            ViewBag.Categories = GetCategories();

            return View();
        }

        [HttpPost]
        public IActionResult Create(ProductCategory productCategory)
        {
            if (ModelState.IsValid)
            {
                _dbContext.ProductCategory.Add(productCategory);
                _dbContext.SaveChanges();

                return RedirectToAction(nameof(Index), new { productId = productCategory.ProductId });
            }

            return View(productCategory);
        }

        public IActionResult Edit(int id)
        {
            var productCategory = _dbContext.ProductCategory.FirstOrDefault(p => p.Id == id);
            ViewBag.Categories = GetCategories();

            return View(productCategory);
        }

        [HttpPost]
        public IActionResult Edit(ProductCategory productCategory)
        {
            if (ModelState.IsValid)
            {
                _dbContext.ProductCategory.Update(productCategory);
                _dbContext.SaveChanges();

                return RedirectToAction(nameof(Index), new { productId = productCategory.ProductId });
            }

            return View(productCategory);
        }

        public IActionResult Delete(int id)
        {
            var productCategories = (from prodCat in _dbContext.ProductCategory
                                     where prodCat.Id == id
                                     select new ProductCategory
                                     {
                                         Id = prodCat.Id,
                                         ProductId = prodCat.ProductId,
                                         ProductTitle = (from p in _dbContext.Product where p.Id == prodCat.ProductId select p.Title).FirstOrDefault(),
                                         CategoryId = prodCat.CategoryId,
                                         CategoryTitle = (from c in _dbContext.Category where c.Id == prodCat.CategoryId select c.Title).FirstOrDefault()
                                     }).SingleOrDefault();

            return View(productCategories);
        }

        [HttpPost]
        public IActionResult DeleteProductCategory(int id)
        {
            var prodCat = _dbContext.ProductCategory.Find(id);
            _dbContext.ProductCategory.Remove(prodCat);
            _dbContext.SaveChanges();

            return RedirectToAction(nameof(Index), new { productId = prodCat.ProductId });
        }



        private List<SelectListItem> GetCategories()
        {
            return _dbContext.Category.Select(c => new SelectListItem()
            {
                Value = c.Id.ToString(),
                Text = c.Title
            }).ToList();
        }
    }
}
