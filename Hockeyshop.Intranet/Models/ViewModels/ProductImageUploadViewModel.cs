using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Hockeyshop.Intranet.Models.ViewModels
{
    public class ProductImageUploadViewModel
    {
        public int IdProductImage { get; set; }
        public int IdProduct { get; set; }
        [Required(ErrorMessage = "You must upload an image.")]
        public IFormFile ImageFile { get; set; }
        public string ImageUrl { get; set; }
    }

}
