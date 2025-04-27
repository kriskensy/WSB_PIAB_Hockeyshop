using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hockeyshop.Data.Data.Help
{
    public class FaqEntry
    {
        [Key]
        public int IdFaq { get; set; }

        [Required(ErrorMessage = "The Question field is required.")]
        [MaxLength(255, ErrorMessage = "The Question must not exceed 255 characters.")]
        public string Question { get; set; }

        [Required(ErrorMessage = "The Answer field is required.")]
        public string Answer { get; set; }

        [Required(ErrorMessage = "The Creation Date field is required.")]
        [Display(Name = "Creation Date")]
        public DateTime CreatedAt { get; set; }

        [Required(ErrorMessage = "The Update Date field is required.")]
        [Display(Name = "Update Date")]
        public DateTime UpdatedAt { get; set; }

        [Required(ErrorMessage = "The View Count field is required.")]
        [Range(0, int.MaxValue, ErrorMessage = "The View Count must be a positive number.")]
        [Display(Name = "View Count")]
        public int ViewCount { get; set; }

        [ForeignKey("FaqCategory")]
        [Required(ErrorMessage = "The Category ID field is required.")]
        [Display(Name = "Category ID")]
        public int IdCategory { get; set; }
        public FaqCategory FaqCategory { get; set; }

    }
}
