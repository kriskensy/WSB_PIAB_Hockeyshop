using Hockeyshop.Data.Data.Orders;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Hockeyshop.Data.Data.Payments
{
    public class Payment
    {
        [Key]
        public int IdPayment { get; set; }

        [Required(ErrorMessage = "The Order field is required.")]
        [ForeignKey("Order")]
        [Display(Name = "Order ID")]
        public int IdOrder { get; set; }
        [ValidateNever]
        public Order Order { get; set; }

        [Required(ErrorMessage = "The Payment Date field is required.")]
        [Display(Name = "Payment date")]
        public DateTime PaymentDate { get; set; }

        [Required(ErrorMessage = "The Payment Method field is required.")]
        [ForeignKey("PaymentMethod")]
        [Display(Name = "Payment method")]
        public int IdPaymentMethod { get; set; }
        [ValidateNever]
        public PaymentMethod PaymentMethod { get; set; }

        [Required(ErrorMessage = "The Amount field is required.")]
        public decimal Amount { get; set; }

        [Required(ErrorMessage = "The Payment Status field is required.")]
        [ForeignKey("PaymentStatus")]
        [Display(Name = "Payment status ID")]
        public int IdPaymentStatus { get; set; }
        [ValidateNever]
        public PaymentStatus PaymentStatus { get; set; }
    }
}
