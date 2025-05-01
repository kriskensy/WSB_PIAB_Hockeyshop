using System.ComponentModel.DataAnnotations;

namespace Hockeyshop.Data.Data.Payments
{
    public class PaymentMethod
    {
        [Key]
        public int IdPaymentMethod { get; set; }

        [Required(ErrorMessage = "The Payment Method Name field is required.")]
        public string Name { get; set; }

        public ICollection<Payment> Payments { get; } = new List<Payment>();
    }
}
