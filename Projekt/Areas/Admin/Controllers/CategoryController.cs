using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Projekt.Data;
using Projekt.Models;
using System.Linq;

namespace Projekt.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles ="Admin")]
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        public CategoryController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IActionResult Index()
        {
            return View(_dbContext.Category.ToList());
        }

        public IActionResult Details(int id)
        {
            var category = _dbContext.Category.FirstOrDefault(c => c.Id == id);
            
            return View(category);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create([Bind("Id, Title")]Category category)
        {
            if (ModelState.IsValid)
            {
                _dbContext.Add(category);
                _dbContext.SaveChanges();

                return RedirectToAction(nameof(Index));
            }

            return View(category);
        }

        public IActionResult Edit(int id)
        {
            var category = _dbContext.Category.Find(id);

            return View(category);
        }

        [HttpPost]
        public IActionResult Edit(int id, Category category)
        {
            if (ModelState.IsValid)
            {
                _dbContext.Update(category);
                _dbContext.SaveChanges();

                return RedirectToAction(nameof(Index));
            }

            return View(category);
        }

        public IActionResult Delete(int id)
        {
            var category = _dbContext.Category.FirstOrDefault(c => c.Id == id);

            return View(category);
        }

        [HttpPost]
        [ActionName("Delete")]
        public IActionResult DeleteCategory(int id)
        {
            var category = _dbContext.Category.Find(id);
            _dbContext.Category.Remove(category);
            _dbContext.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
    }
}
