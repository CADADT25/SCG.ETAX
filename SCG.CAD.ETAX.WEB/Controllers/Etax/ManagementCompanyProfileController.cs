using Microsoft.AspNetCore.Mvc;

namespace SCG.CAD.ETAX.WEB.Controllers.Etax
{
    public class ManagementCompanyProfileController : Controller
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


        public IActionResult _Create()
        {
            return View();
        }


        public IActionResult _Update()
        {
            return View();
        }

        public IActionResult _View()
        {
            return View();
        }
    }
}
