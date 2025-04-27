using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hockeyshop.Data.Data.Inventory
{
    public class Stock
    {
        [Key]
        public int IdStock { get; set; }

        [Required(ErrorMessage = "The Quantity field is required.")]
        [Range(0, int.MaxValue, ErrorMessage = "The Quantity must be a positive number.")]
        public int Quantity { get; set; }

        [Required(ErrorMessage = "The Last Update Date field is required.")]
        [Display(Name = "Last Update Date")]
        public DateTime LastUpdate { get; set; }

        [ForeignKey("Product")]
        [Required(ErrorMessage = "The Product ID field is required.")]
        [Display(Name = "Product ID")]
        public int IdProduct { get; set; }
        public Product Product { get; set; }
    }
}
