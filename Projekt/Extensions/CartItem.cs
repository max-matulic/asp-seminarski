using Projekt.Models;

namespace Projekt.Extensions
{
    public class CartItem
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }

        public decimal GetTotal()
        {
            return Product.Price * Quantity;
        }
    }
}
