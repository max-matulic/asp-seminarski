using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Projekt.Models
{
    public class Order
    {
        public int Id { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}")]
        public DateTime DateCreated { get; set; }
        [Required(ErrorMessage = "Total price is required!")]
        [Column(TypeName = "decimal(7,2)")]
        public decimal Total { get; set; }
        [Required(ErrorMessage = "First name is required!")]
        [StringLength(50)]
        public string BillingFirstName { get; set; }
        [Required(ErrorMessage = "Last name is required!")]
        [StringLength(50)]
        public string BillingLastName { get; set; }
        [Required(ErrorMessage = "Email Address is required!")]
        [StringLength(100)]
        [DataType(DataType.EmailAddress, ErrorMessage = "E-mail is not valid!")]
        public string BillingEmail { get; set; }
        [Required(ErrorMessage = "Phone number is required!")]
        [StringLength(100)]
        public string BillingPhone { get; set; }
        [Required(ErrorMessage = "Address is required!")]
        [StringLength(100)]
        public string BillingAddress { get; set; }
        [Required(ErrorMessage = "City is required!")]
        [StringLength(100)]
        public string BillingCity { get; set; }
        [StringLength(100)]
        [Required(ErrorMessage = "Country is required!")]
        public string BillingCountry { get; set; }
        [Required(ErrorMessage = "Postal code is required!")]
        [StringLength(20)]
        public string BillingZip { get; set; }


        [ForeignKey("OrderId")]
        public List<OrderItem> OrderItems { get; set; }
        public string UserId { get; set; }
    }
}
