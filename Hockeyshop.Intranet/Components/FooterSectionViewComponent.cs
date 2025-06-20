using Hockeyshop.Interfaces.CMS;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Hockeyshop.Intranet.Components
{
    public class FooterSectionViewComponent : ViewComponent
    {
        private readonly IFooterSectionService _service;

        public FooterSectionViewComponent(IFooterSectionService service)
        {
            _service = service;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var model = await _service.GetActiveAsync();
            return View(model);
        }
    }
}
