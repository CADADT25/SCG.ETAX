using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SCG.CAD.ETAX.WEB.Models;
using System.Diagnostics;

namespace SCG.CAD.ETAX.WEB.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index(string Username, bool CurrentLogin, int LogOut)
        {

            AuthenticationModel authenticationModel = new AuthenticationModel();

            authenticationModel.username = Username;
            authenticationModel.authenticated = CurrentLogin;

            AuthGuard authGuard = new AuthGuard();

            if (LogOut == 0)
            {
                if (authGuard.OnAuthentication(authenticationModel) == 1)
                {
                    if (CurrentLogin == true)
                    {
                        return View();
                    }
                    else
                    {
                        return new RedirectResult("~/AuthSinIn/Index");
                    }
                }
                else
                {
                    return new RedirectResult("~/AuthSinIn/Index");
                }
            }
            else
            {
                return new RedirectResult("~/AuthSinIn/Index");
            }



            //return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}