using System.ComponentModel.DataAnnotations;

namespace Hockeyshop.Intranet.Models.Documents
{
    public class DocumentCategory
    {
        [Key]
        public int IdCategory { get; set; }

        [Required(ErrorMessage = "The Name field is required.")]
        [MaxLength(50, ErrorMessage = "The Name must not exceed 50 characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "The Description field is required.")]
        [MaxLength(500, ErrorMessage = "The Description must not exceed 500 characters.")]
        public string Description { get; set; }

        public ICollection<Document> Documents { get; } = new List<Document>();
    }

}
