using Microsoft.AspNetCore.Mvc;

namespace SCG.CAD.ETAX.WEB.Controllers.Etax
{
    public class InboxCompleteController : Controller
    {
        [SessionExpire]
        public IActionResult Index()
        {
            return View();
        }
    }
}
