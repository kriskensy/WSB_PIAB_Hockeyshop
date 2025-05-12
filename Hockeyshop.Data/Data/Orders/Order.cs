using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Hockeyshop.Data.Data.Core;
using Hockeyshop.Data.Data.Payments;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Hockeyshop.Data.Data.Orders
{
    public class Order
    {
        [Key]
        public int IdOrder { get; set; }

        [Required(ErrorMessage = "The Order Date field is required.")]
        [Display(Name = "Order date")]
        public DateTime OrderDate { get; set; }

        [Required(ErrorMessage = "The User field is required.")]
        [ForeignKey("User")]
        [Display(Name = "User ID")]
        public int IdUser { get; set; }
        [ValidateNever]
        public User User { get; set; }

        [ForeignKey("OrderStatus")]
        [Display(Name = "Order status")]
        public int? IdOrderStatus { get; set; }
        [ValidateNever]
        public OrderStatus OrderStatus { get; set; }

        public ICollection<OrderItem> OrderItems { get; } = new List<OrderItem>();
        public ICollection<Invoice> Invoices { get; } = new List<Invoice>();
        public ICollection<Payment> Payments { get; } = new List<Payment>();
    }
}
