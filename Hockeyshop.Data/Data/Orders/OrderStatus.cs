using System.ComponentModel.DataAnnotations;


namespace Hockeyshop.Data.Data.Orders
{
    public class OrderStatus
    {
        [Key]
        public int IdOrderStatus { get; set; }

        [Required(ErrorMessage = "The Order Status Name field is required.")]
        public string Name { get; set; }

        public ICollection<Order> Orders { get; } = new List<Order>();
    }
}
