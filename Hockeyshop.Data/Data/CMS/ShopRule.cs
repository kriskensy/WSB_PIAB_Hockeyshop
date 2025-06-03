using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hockeyshop.Data.Data.CMS
{
    public class ShopRule
    {
        [Key]
        public int IdShopRule {  get; set; }

        [Required(ErrorMessage = "Icon is required.")]
        [ForeignKey(nameof(IconLibrary))]
        public int IdIcon { get; set; }

        [ValidateNever]
        public IconLibrary IconLibrary { get; set; }

        [Required(ErrorMessage = "The Title field is required.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Content is required.")]
        public string Content { get; set; }

        public int? DisplayOrder {  get; set; } //1,2,3 lub null (nie wyswietla)
    }
}
