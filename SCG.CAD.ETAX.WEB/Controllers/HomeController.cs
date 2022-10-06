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

        [SessionExpire]
        public IActionResult Index(string Username, bool CurrentLogin, int LogOut)
        {
            AuthenticationModel authenticationModel = new AuthenticationModel();

            authenticationModel.username = Username;
            authenticationModel.authenticated = CurrentLogin;

            AuthGuard authGuard = new AuthGuard();
            ViewData["userEmail"] = Username;
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

        public async Task<JsonResult> GetMenu(string user)
        {
            List<ConfigControlMenu> tran = new List<ConfigControlMenu>();

            Response resp = new Response();

            var result = "";

            try
            {
                var task = await Task.Run(() => ApiHelper.GetURI("api/Authentication/GetMenu?Username= " + user + " "));

                if (task.STATUS)
                {

                    tran = JsonConvert.DeserializeObject<List<ConfigControlMenu>>(task.OUTPUT_DATA.ToString());

                    result = JsonConvert.SerializeObject(tran);

                }
                else
                {
                    ViewBag.Error = task.MESSAGE;
                }
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException);
            }

            return Json(result);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}