using Hockeyshop.Data.Data.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hockeyshop.Data.Data.CMS
{
    public class IconLibrary
    {
        [Key]
        public int IdIcon { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Class name is required.")]
        public string ClassName { get; set; }

        public ICollection<ShopRule> ShopRules { get; } = new List<ShopRule>();
    }
}
