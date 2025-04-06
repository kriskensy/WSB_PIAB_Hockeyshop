using Hockeyshop.Intranet.Models.Management;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hockeyshop.Intranet.Models.Support
{
    public class Ticket
    {
        [Key]
        public int IdTicket { get; set; }

        [Required(ErrorMessage = "The Title field is required.")]
        [MaxLength(100, ErrorMessage = "The Title must not exceed 100 characters.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "The Description field is required.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "The Status field is required.")]
        [MaxLength(50, ErrorMessage = "The Status must not exceed 50 characters.")]
        public string Status { get; set; }

        [Required(ErrorMessage = "The Created At field is required.")]
        public DateTime CreatedAt { get; set; }

        [Required(ErrorMessage = "The Closed At field is required.")]
        public DateTime ClosedAt { get; set; }

        [ForeignKey("User")]
        [Required(ErrorMessage = "The User ID field is required.")]
        [Display(Name = "User ID")]
        public int IdUser { get; set; }
        public User User { get; set; }

    }

}
