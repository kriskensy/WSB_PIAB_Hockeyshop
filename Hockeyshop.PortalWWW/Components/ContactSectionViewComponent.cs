using Hockeyshop.Interfaces.CMS;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Hockeyshop.PortalWWW.Components
{
    public class ContactSectionViewComponent : ViewComponent
    {
        private readonly IContactSectionService _service;

        public ContactSectionViewComponent(IContactSectionService service)
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
