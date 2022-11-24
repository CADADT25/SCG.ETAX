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
        //[PermissionAttribute]
        public IActionResult Index()
        {
            return View();
        }

        [SessionExpire]
        //public IActionResult IndexCheckLogin(string Username, bool CurrentLogin, int LogOut)
        public IActionResult IndexCheckLogin(bool CurrentLogin, int LogOut)
        {
            AuthenticationModel authenticationModel = new AuthenticationModel();
            string username = HttpContext.Session.GetString("userMail") ?? "";
            if(username == "") return new RedirectResult("~/AuthSinIn/Index");
            authenticationModel.username = username;
            authenticationModel.authenticated = CurrentLogin;
            string fullname = HttpContext.Session.GetString("userName").ToUpper() + " " + HttpContext.Session.GetString("userLastname").Substring(0, 1).ToUpper() + ".";
            string initialsname = HttpContext.Session.GetString("userName").Substring(0, 1).ToUpper() + HttpContext.Session.GetString("userLastname").Substring(0, 1).ToUpper();
            AuthGuard authGuard = new AuthGuard();
            ViewData["userEmail"] = username;
            ViewData["userFullname"] = fullname;
            ViewData["userInitialsName"] = initialsname;

            if (LogOut == 0)
            {
                if (authGuard.OnAuthentication(authenticationModel) == 1)
                {
                    if (CurrentLogin == true)
                    {
                        return View("~/Views/Home/index.cshtml");
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
                    //var comcode = JsonConvert.DeserializeObject<List<string>>(task.CODE.ToString());

                    result = JsonConvert.SerializeObject(tran);
                    var menu = string.Join(",", tran.Select(x => x.ConfigControlMenuNo).ToList());
                    HttpContext.Session.SetString("premissionMenu", menu); 
                    HttpContext.Session.SetString("premissionComCode", task.CODE.ToString()); 
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

        public async Task<bool> CheckPermission()
        {
            bool result = false;
            if (HttpContext.Session.GetInt32("checkpermissionpage") == 1)
            {
                result = true;
            }

            return result;
        }

        public async Task<bool> GetControlPermission()
        {
            List<ConfigControlFunction> tran = new List<ConfigControlFunction>();

            Response resp = new Response();

            var result = "";

            try
            {
                var task = await Task.Run(() => ApiHelper.GetURI("api/ConfigControlFunction/GetListAll"));

                if (task.STATUS)
                {
                    var userlevel = HttpContext.Session.GetInt32("userLevel").ToString();

                    tran = JsonConvert.DeserializeObject<List<ConfigControlFunction>>(task.OUTPUT_DATA.ToString());

                    tran = tran.Where(x=> x.Isactive == 1).ToList();
                    //tran = tran.Where(x=> !string.IsNullOrEmpty(x.ConfigControlFunctionRole) && x.ConfigControlFunctionRole.Contains(userlevel)).ToList();

                    HttpContext.Session.SetString("controlPermission", JsonConvert.SerializeObject(tran));
                }
                else
                {
                    ViewBag.Error = task.MESSAGE;
                }
            }

            catch (Exception ex)
            {
                throw ex;
            }
            return true;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}