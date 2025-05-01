using System.ComponentModel.DataAnnotations;

namespace Hockeyshop.Data.Data.Payments
{
    public class PaymentStatus
    {
        [Key]
        public int IdPaymentStatus { get; set; }

        [Required(ErrorMessage = "The Payment Status Name field is required.")]
        public string Name { get; set; }

        public ICollection<Payment> Payments { get; } = new List<Payment>();
    }
}
