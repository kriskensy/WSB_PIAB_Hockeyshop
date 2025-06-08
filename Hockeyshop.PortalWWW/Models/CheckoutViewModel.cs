using Hockeyshop.Data.Data.Payments;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace Hockeyshop.PortalWWW.Models
{
    public class CheckoutViewModel
    {
        [Required(ErrorMessage = "Please select a payment method")]
        [Display(Name = "Payment Method")]
        public int SelectedPaymentMethodId { get; set; }

        [ValidateNever]
        public  UserCartViewModel? Cart { get; set; }
        public List<PaymentMethodOption> PaymentMethods { get; set; } = new List<PaymentMethodOption>();
    }

    public class PaymentMethodOption
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
