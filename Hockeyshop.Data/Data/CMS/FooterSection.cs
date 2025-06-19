using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hockeyshop.Data.Data.CMS
{
    public class FooterSection
    {
        public int Id { get; set; }
        public string FooterText { get; set; }
        public string FooterLogoText { get; set; }
        public bool IsActive { get; set; }
    }
}
