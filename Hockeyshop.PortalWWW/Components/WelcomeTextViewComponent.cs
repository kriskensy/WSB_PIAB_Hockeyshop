using Hockeyshop.Interfaces.CMS;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Hockeyshop.PortalWWW.Components
{
    public class WelcomeTextViewComponent : ViewComponent
    {
        private readonly IWelcomeTextService _service;

        public WelcomeTextViewComponent(IWelcomeTextService service)
        {
            _service = service;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var model = await _service.GetActiveAsync();//pobiera tylko aktywny
            return View(model);
        }

    }
}

