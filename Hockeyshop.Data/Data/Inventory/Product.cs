using Hockeyshop.Data.Data.Orders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Hockeyshop.Data.Data.Inventory
{
    public class Product
    {
        [Key]
        public int IdProduct { get; set; }

        [Required(ErrorMessage = "The Name field is required.")]
        [MaxLength(100, ErrorMessage = "The Name must not exceed 100 characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "The Description field is required.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "The Price field is required.")]
        [Precision(18, 2)]
        [Range(0.01, double.MaxValue, ErrorMessage = "The Price must be greater than 0.")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "The Creation Date field is required.")]
        [Display(Name = "Creation Date")]
        public DateTime CreatedAt { get; set; }

        [ForeignKey("Supplier")]
        [Required(ErrorMessage = "The Supplier ID field is required.")]
        [Display(Name = "Supplier ID")]
        public int IdSupplier { get; set; }

        [ForeignKey("ProductCategory")]
        [Required(ErrorMessage = "The Category ID field is required.")]
        [Display(Name = "Category ID")]
        public int IdCategory { get; set; }

        public ICollection<Stock> Stocks { get; } = new List<Stock>();
        public ICollection<Product> Products { get; } = new List<Product>();
        public ICollection<OrderItem> OrderItems { get; } = new List<OrderItem>();

    }
}
