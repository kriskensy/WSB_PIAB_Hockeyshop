using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Hockeyshop.Data.Data.Products;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Hockeyshop.Data.Data.Orders
{
    public class OrderItem
    {
        [Key]
        public int IdOrderItem { get; set; }

        [Required(ErrorMessage = "The Order field is required.")]
        [ForeignKey("Order")]
        [Display(Name = "Order ID")]
        public int IdOrder { get; set; }
        [ValidateNever]
        public Order Order { get; set; }

        [Required(ErrorMessage = "The Product field is required.")]
        [ForeignKey("Product")]
        [Display(Name = "Product ID")]
        public int IdProduct { get; set; }
        [ValidateNever]
        public Product Product { get; set; }

        [Required(ErrorMessage = "The Quantity field is required.")]
        public int Quantity { get; set; }

        [Required(ErrorMessage = "The Unit Price field is required.")]
        [Display(Name = "Unit price")]
        public decimal UnitPrice { get; set; }
    }
}
