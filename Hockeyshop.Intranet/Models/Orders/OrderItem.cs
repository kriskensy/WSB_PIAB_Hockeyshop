using Hockeyshop.Intranet.Models.Inventory;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hockeyshop.Intranet.Models.Orders
{
    public class OrderItem
    {
        [Key]
        public int IdOrderItem { get; set; }

        [Required(ErrorMessage = "The Quantity field is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "The Quantity must be at least 1.")]
        public int Quantity { get; set; }

        [Required(ErrorMessage = "The Unit Price field is required.")]
        //[Precision(18, 2)]
        [Range(0.01, double.MaxValue, ErrorMessage = "The Unit Price must be greater than 0.")]
        [Display(Name = "Unit Price")]
        public decimal UnitPrice { get; set; }

        [ForeignKey("Order")]
        [Required(ErrorMessage = "The Order ID field is required.")]
        [Display(Name = "Order ID")]
        public int IdOrder { get; set; }
        public Order Orders { get; set; }

        [ForeignKey("Product")]
        [Required(ErrorMessage = "The Product ID field is required.")]
        [Display(Name = "Product ID")]
        public int IdProduct { get; set; }
        public Product Products { get; set; }
    }

}
