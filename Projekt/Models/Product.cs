using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Projekt.Models
{
    public class Product
    {
        public int Id { get; set; }
        [Required]
        [StringLength(200, MinimumLength = 2)]
        public string Title { get; set; }
        [Required]
        [Column(TypeName = "decimal(9,2)")]
        public decimal Quantity { get; set; }
        [Required]
        [Column(TypeName = "decimal(9,2)")]
        public decimal Price { get; set; }
        [ForeignKey("ProductId")]
        public List<OrderItem> OrderItems { get; set; }
        [ForeignKey("ProductId")]
        public List<ProductCategory> ProductCategories { get; set; }
    }
}
