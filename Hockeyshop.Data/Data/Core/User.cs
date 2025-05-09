using Hockeyshop.Data.Data.Cart;
using Hockeyshop.Data.Data.CMS;
using Hockeyshop.Data.Data.Orders;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hockeyshop.Data.Data.Core
{
    public class User
    {
        [Key]
        public int IdUser { get; set; }

        [Required(ErrorMessage = "The First Name field is required.")]
        [Display(Name = "First name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "The Last Name field is required.")]
        [Display(Name = "Last name")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "The Email field is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "The Password Hash field is required.")]
        public string PasswordHash { get; set; }

        [Required(ErrorMessage = "The Post Code field is required.")]
        [Display(Name = "Post code")]
        public string PostCode { get; set; }

        [Required(ErrorMessage = "The City field is required.")]
        public string City { get; set; }

        [Required(ErrorMessage = "The Street and Number field is required.")]
        [Display(Name = "Street and number")]
        public string StreetAndNumber { get; set; }

        [Required(ErrorMessage = "The User Role field is required.")]
        //[ForeignKey("UserRole")]
        [ForeignKey(nameof(UserRole))]
        [Display(Name = "User role")]
        public int IdUserRole { get; set; }
        public UserRole UserRole { get; set; }

        public ICollection<Order> Orders { get; } = new List<Order>();
        public ICollection<Invoice> Invoices { get; } = new List<Invoice>();
        public ICollection<HockeyNews> AuthoredNews { get; } = new List<HockeyNews>();
        public ICollection<UserCart> UserCarts { get; } = new List<UserCart>();
    }
}
