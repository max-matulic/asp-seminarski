using Microsoft.AspNetCore.Mvc;
using Projekt.Data;
using Projekt.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace Projekt.Controllers
{
    public class CartController : Controller
    {
        const string _sessionKey = "_cart";
        ApplicationDbContext _dbContext;

        public CartController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            List<CartItem> cart = HttpContext.Session.GetObjectsFromJson<List<CartItem>>(_sessionKey) ?? new List<CartItem>();
            decimal sum = default(decimal);
            ViewBag.TotalPrice = cart.Sum(item => sum + item.GetTotal());

            return View(cart);
        }

        [HttpPost]
        public IActionResult AddToCart(int productId)
        {
            List<CartItem> cartSession = HttpContext.Session.GetObjectsFromJson<List<CartItem>>(_sessionKey) ?? new List<CartItem>();
            if (cartSession.Count == 0)
            {
                var product = _dbContext.Product.Find(productId);
                CartItem cartItem = new CartItem()
                {
                    Product = product,
                    Quantity = 1
                };

                cartSession.Add(cartItem);
                HttpContext.Session.SetObjectAsJson(_sessionKey, cartSession);
            }
            else
            {
                var index = IsExistingInCart(productId);

                if (index != -1)
                {
                    cartSession[index].Quantity++;
                }
                else
                {
                    CartItem cartItem = new CartItem()
                    {
                        Product = _dbContext.Product.Find(productId),
                        Quantity = 1
                    };
                    cartSession.Add(cartItem);
                }

                HttpContext.Session.SetObjectAsJson(_sessionKey, cartSession);
            }

            return RedirectToAction(nameof(Index));

        }

        public IActionResult RemoveFromCart(int productId)
        {
            List<CartItem> cartItems = HttpContext.Session.GetObjectsFromJson<List<CartItem>>(_sessionKey);
            int index = IsExistingInCart(productId);
            cartItems.RemoveAt(index);

            HttpContext.Session.SetObjectAsJson(_sessionKey, cartItems);

            return RedirectToAction(nameof(Index));
        }

        private int IsExistingInCart(int productId)
        {
            List<CartItem> cart = HttpContext.Session.GetObjectsFromJson<List<CartItem>>(_sessionKey);

            for (int i = 0; i < cart.Count; i++)
            {
                if (cart[i].Product.Id == productId)
                {
                    return i;
                }
            }

            return -1;
        }
    }
}
