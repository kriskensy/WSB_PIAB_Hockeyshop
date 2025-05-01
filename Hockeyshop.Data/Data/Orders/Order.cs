using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Hockeyshop.Data.Data.Core;
using Hockeyshop.Data.Data.Payments;

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
        public User User { get; set; }

        [ForeignKey("OrderStatus")]
        [Display(Name = "Order status")]
        public int? IdOrderStatus { get; set; }
        public OrderStatus OrderStatus { get; set; }

        public ICollection<OrderItem> OrderItems { get; } = new List<OrderItem>();
        public Invoice Invoice { get; set; }
        public Payment Payment { get; set; }
    }
}
