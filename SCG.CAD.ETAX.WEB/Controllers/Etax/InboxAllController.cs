using Microsoft.AspNetCore.Mvc;

namespace SCG.CAD.ETAX.WEB.Controllers.Etax
{
    public class InboxAllController : Controller
    {
        [SessionExpire]
        public IActionResult Index()
        {
            return View();
        }
    }
}
