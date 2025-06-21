using System.ComponentModel.DataAnnotations;

namespace Hockeyshop.PortalWWW.Models
{
    public class ContactFormViewModel
    {
        [Required]
        public string Name { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Message { get; set; }
    }
}
