using Microsoft.AspNetCore.Mvc;

namespace SCG.CAD.ETAX.WEB.Controllers
{
    public class RequestController : Controller
    {
        public IActionResult Index(string requestNo)
        {
            return View();
        }
    }
}
