using Hockeyshop.Intranet.Models.Management;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hockeyshop.Intranet.Models.Orders
{
    public class Order
    {
        [Key]
        public int IdOrder { get; set; }

        [Required(ErrorMessage = "The Order Date field is required.")]
        [Display(Name = "Order Date")]
        public DateTime OrderDate { get; set; }

        [Required(ErrorMessage = "The Status field is required.")]
        [MaxLength(50, ErrorMessage = "The Status must not exceed 50 characters.")]
        public string Status { get; set; }

        [Required(ErrorMessage = "The Total Amount field is required.")]
        //[Precision(18, 2)]
        [Range(0.01, double.MaxValue, ErrorMessage = "The Total Amount must be greater than 0.")]
        [Display(Name = "Total Amount")]
        public decimal TotalAmount { get; set; }

        [Required(ErrorMessage = "The Updated Date field is required.")]
        [Display(Name = "Update Date")]
        public DateTime UpdatedAt { get; set; }

        [ForeignKey("User")]
        [Required(ErrorMessage = "The User ID field is required.")]
        [Display(Name = "User ID")]
        public int IdUser { get; set; }
        public User Users { get; set; }
    }

}
