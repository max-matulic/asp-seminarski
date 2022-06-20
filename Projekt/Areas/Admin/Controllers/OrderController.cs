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

        #region Order
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

            order.OrderItems =
                (
                    from oi in _dbContext.OrderItem
                    where oi.OrderId == id
                    select new OrderItem
                    {
                        Id = oi.Id,
                        OrderId = oi.OrderId,
                        Quantity = oi.Quantity,
                        Total = oi.Total,
                        ProductId = oi.ProductId,
                        ProductName = (from pr in _dbContext.Product where pr.Id == oi.ProductId select pr.Title).FirstOrDefault()
                    }
                ).ToList();

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

        #endregion

        #region Order Items

        public IActionResult AddOrderItem(int id)
        {
            ViewBag.OrderId = id;
            ViewBag.Products = _dbContext.Product.Select(p => new SelectListItem()
            {
                Value = p.Id.ToString(),
                Text = p.Title
            }).ToList();

            return View();
        }

        [HttpPost]
        public IActionResult AddOrderItem([Bind("OrderId, ProductId, Total, Quantity")] OrderItem orderItem)
        {
            if (ModelState.IsValid)
            {
                _dbContext.OrderItem.Add(orderItem);
                _dbContext.SaveChanges();

                return RedirectToAction("Details", new { id = orderItem.OrderId });
            }

            return View(orderItem);
        }

        public IActionResult DeleteOrderItem(int id)
        {
            var orderItem = _dbContext.OrderItem.Find(id);
            _dbContext.OrderItem.Remove(orderItem);
            _dbContext.SaveChanges();

            return RedirectToAction("Details", new { id = orderItem.OrderId });
        }

        #endregion

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
