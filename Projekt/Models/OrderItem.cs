using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Projekt.Models
{
    public class OrderItem
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        [Column(TypeName = "decimal(9,2)")]
        [Required(ErrorMessage = "Quantity is required!")]
        public decimal Quantity { get; set; }
        [Column(TypeName = "decimal(9,2")]
        [Required(ErrorMessage = "Total is required!")]
        public decimal Total { get; set; }

        [NotMapped]
        public string ProductName { get; set; }
    }
}
