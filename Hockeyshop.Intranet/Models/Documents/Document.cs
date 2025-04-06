using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hockeyshop.Intranet.Models.Documents
{
    public class Document
    {
        [Key]
        public int IdDocument { get; set; }

        [Required(ErrorMessage = "The Title field is required.")]
        [MaxLength(100, ErrorMessage = "The Title must not exceed 100 characters.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "The Content field is required.")]
        public string Content { get; set; }

        [Required(ErrorMessage = "The Creation Date field is required.")]
        [Display(Name = "Creation Date")]
        public DateTime CreatedAt { get; set; }

        [Required(ErrorMessage = "The Update Date field is required.")]
        [Display(Name = "Update Date")]
        public DateTime UpdatedAt { get; set; }

        [ForeignKey("DocumentCategory")]
        [Required(ErrorMessage = "The Category ID field is required.")]
        [Display(Name = "Category ID")]
        public int IdCategory { get; set; }
        public DocumentCategory DocumentCategory { get; set; }
    }

}
