using Hockeyshop.Data.Data.Core;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hockeyshop.Data.Data.CMS
{
    public class HockeyNews
    {
        [Key]
        public int IdHockeyNews { get; set; }

        [Required(ErrorMessage = "The Title field is required.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "The Content field is required.")]
        public string Content { get; set; }

        [Required(ErrorMessage = "The Author field is required.")]
        [ForeignKey("Author")]
        [Display(Name = "Author ID")]
        public int IdAuthor { get; set; }
        public User Author { get; set; }

        [Required(ErrorMessage = "The Created At field is required.")]
        [Display(Name = "Created at")]
        public DateTime CreatedAt { get; set; }

        [Required(ErrorMessage = "The Published field is required.")]
        [Display(Name = "Is it published?")]
        public bool Published { get; set; }

        [Required(ErrorMessage = "The Image URL field is required.")]
        [Display(Name = "Image url")]
        public string ImageUrl { get; set; }
    }
}
