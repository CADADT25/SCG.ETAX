using Microsoft.AspNetCore.Mvc;

namespace SCG.CAD.ETAX.WEB.Controllers.Etax
{
    public class InboxInprogressController : Controller
    {
        [SessionExpire]
        public IActionResult Index()
        {
            return View();
        }
    }
}
