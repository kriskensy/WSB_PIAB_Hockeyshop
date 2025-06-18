using Hockeyshop.Data.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hockeyshop.Services.Abstraction
{
    public abstract class BaseService
    {
        protected readonly HockeyshopContext _context;

        public BaseService(HockeyshopContext context)
        {
            _context = context;
        }
    }
}
