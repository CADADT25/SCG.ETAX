using Microsoft.AspNetCore.Mvc;

namespace SCG.CAD.ETAX.WEB.Controllers.Authentication
{
    public class AuthResetPasswordController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

    }
}
