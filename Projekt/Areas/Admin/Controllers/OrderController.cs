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
    [Authorize(Roles ="Admin")]
    public class OrderController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        public OrderController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IActionResult Index()
        {
            return View(_dbContext.Order.ToList());
        }

        public IActionResult Details(int id)
        {
            if (id == 0)
            {
                return RedirectToAction("PageNotFound", "HttpStatusCodes");
            }

            var order = _dbContext.Order.FirstOrDefault(o => o.Id == id);

            if (order == null)
            {
                return RedirectToAction("PageNotFound", "HttpStatusCodes");
            }

            return View(order);
        }

        public IActionResult Create()
        {
            ViewBag.Users = GetUsers();

            return View();
        }

        [HttpPost]
        public IActionResult Create(Order order)
        {
            if (ModelState.IsValid)
            {
                _dbContext.Order.Add(order);
                _dbContext.SaveChanges();

                return RedirectToAction(nameof(Index));
            }

            return View(order);
        }

        public IActionResult Edit(int id)
        {
            if (id == 0)
            {
                return RedirectToAction("PageNotFound", "HttpStatusCodes");
            }

            var order = _dbContext.Order.FirstOrDefault(o => o.Id == id);

            if (order == null)
            {
                return RedirectToAction("PageNotFound", "HttpStatusCodes");
            }

            ViewBag.Users = GetUsers();

            return View(order);
        }

        [HttpPost]
        public IActionResult Edit(int id, Order order)
        {
            if (id != order.Id)
            {
                return RedirectToAction("PageNotFound", "HttpStatusCodes");
            }

            if (ModelState.IsValid)
            {
                _dbContext.Order.Update(order);
                _dbContext.SaveChanges();

                return RedirectToAction(nameof(Index));
            }

            ViewBag.Users = GetUsers();

            return View(order);
        }

        public IActionResult Delete(int id)
        {
            if (id == 0)
            {
                return RedirectToAction("PageNotFound", "HttpStatusCodes");
            }

            var order = _dbContext.Order.Find(id);

            if (order == null)
            {
                return RedirectToAction("PageNotFound", "HttpStatusCodes");
            }

            return View(order);
        }

        [HttpPost]
        public IActionResult DeleteOrder(int id)
        {
            if (id == 0)
            {
                return RedirectToAction("PageNotFound", "HttpStatusCodes");
            }

            var order = _dbContext.Order.Find(id);

            if (order == null)
            {
                return RedirectToAction("PageNotFound", "HttpStatusCodes");
            }

            _dbContext.Order.Remove(order);
            _dbContext.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        

        private List<SelectListItem> GetUsers()
        {
            return _dbContext.Users.Select(u => new SelectListItem()
            {
                Value = u.Id.ToString(),
                Text = u.UserName
            }).ToList();
        }
    }
}
