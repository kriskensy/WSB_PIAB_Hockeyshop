using Hockeyshop.Intranet.Models.Orders;
using Hockeyshop.Intranet.Models.Support;
using System.ComponentModel.DataAnnotations;

namespace Hockeyshop.Intranet.Models.Management
{
    public class User
    {
        [Key]
        public int IdUser { get; set; }

        [Required(ErrorMessage = "The Username field is required.")]
        [MaxLength(50, ErrorMessage = "The Username must not exceed 50 characters.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "The Password field is required.")]
        [Display(Name = "Password")]
        public string PasswordHash { get; set; }

        [Required(ErrorMessage = "The Email field is required.")]
        [EmailAddress(ErrorMessage = "The Email format is invalid.")]
        [MaxLength(100, ErrorMessage = "The Email must not exceed 100 characters.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "The Role field is required.")]
        [MaxLength(20, ErrorMessage = "The Role must not exceed 20 characters.")]
        public string Role { get; set; }

        public ICollection<Order> Users { get; } = new List<Order>();
        public ICollection<Ticket> Tickets { get; } = new List<Ticket>();
    }
}
