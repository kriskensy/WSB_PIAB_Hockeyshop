using Hockeyshop.Data.Data.Core;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Hockeyshop.Data.Data.Cart
{
    public class UserCart
    {
        [Key]
        public int IdUserCart { get; set; }

        [Required(ErrorMessage = "The User field is required.")]
        [ForeignKey("User")]
        [Display(Name = "User ID")]
        public int IdUser { get; set; }
        [ValidateNever]
        public User User { get; set; }

        [Required(ErrorMessage = "The Created At field is required.")]
        public DateTime CreatedAt { get; set; }

        public ICollection<CartItem> CartItems { get; } = new List<CartItem>();
    }
}
