using Microsoft.AspNetCore.Mvc;
using Projekt.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace Projekt.Controllers
{
    public class CartController : Controller
    {
        const string _sessionKey = "_cart";
        public IActionResult Index()
        {
            List<CartItem> cart = HttpContext.Session.GetObjectsFromJson<List<CartItem>>(_sessionKey) ?? new List<CartItem>();
            decimal sum = default(decimal);
            ViewBag.TotalPrice = cart.Sum(item => sum + item.GetTotal());

            return View(cart);
        }

        [HttpPost]
        public IActionResult AddToCart()
        {
            return RedirectToAction(nameof(Index));

        }

        public IActionResult RemoveFromCart(int productId)
        {
            return RedirectToAction(nameof(Index));
        }
    }
}
