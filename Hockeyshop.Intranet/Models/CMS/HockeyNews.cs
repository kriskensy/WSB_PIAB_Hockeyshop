using System.ComponentModel.DataAnnotations;

namespace Hockeyshop.Intranet.Models.CMS
{
    public class HockeyNews
    {
        [Key]
        public int IdNews { get; set; }

        [Required(ErrorMessage = "The Name field is required.")]
        [MaxLength(100, ErrorMessage = "The Name must not exceed 100 characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "The Description field is required.")]
        public string Description { get; set; }
    }
}
