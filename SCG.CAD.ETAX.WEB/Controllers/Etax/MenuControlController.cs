using Microsoft.AspNetCore.Mvc;

namespace SCG.CAD.ETAX.WEB.Controllers.Etax
{
    public class MenuControlController : Controller
    {
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
