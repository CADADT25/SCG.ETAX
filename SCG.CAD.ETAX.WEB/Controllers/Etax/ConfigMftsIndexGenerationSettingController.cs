using Microsoft.AspNetCore.Mvc;

namespace SCG.CAD.ETAX.WEB.Controllers

{
    public class ConfigMftsIndexGenerationSettingController : Controller
    {
        [SessionExpire]
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult _Content()
        {
            return View();
        }

       

    }
}
