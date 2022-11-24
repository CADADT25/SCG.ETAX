using DocumentFormat.OpenXml.Drawing;
using Microsoft.AspNetCore.Mvc;

namespace SCG.CAD.ETAX.WEB.Controllers.Authentication
{
    public class AuthSinInController : Controller
    {
        public IActionResult Index()
        {
            var configTask = Task.Run(() => ApiHelper.GetURI("api/ConfigGlobal/GetDetailByName?cate=WEBAPI&name=AUTHENTICATIONLOGINURL")).Result;
            var config = new ConfigGlobal();
            if (configTask.STATUS)
            {
                config = JsonConvert.DeserializeObject<ConfigGlobal>(configTask.OUTPUT_DATA.ToString());
                ViewData["autoLoginDomain"] = config.ConfigGlobalValue;
            }
            return View();
        }

        public async Task<IActionResult> GetLoginxxx(string LoginEmail, string LoginPassword)
        {
            List<ProfileUserManagement> tran = new List<ProfileUserManagement>();

            AuthGuard authGuard = new AuthGuard();

            ProfileUserManagement profile = new ProfileUserManagement();

            AuthenticationModel _authenticationModel = new AuthenticationModel();

            Response resp = new Response();

            var result = "";

            try
            {
                var task = await Task.Run(() => ApiHelper.GetURI("api/Authentication/GetDetail/?Username=" + LoginEmail + "&Password=" + LoginPassword + ""));

                if (task.STATUS)
                {
                    tran = JsonConvert.DeserializeObject<List<ProfileUserManagement>>(task.OUTPUT_DATA.ToString());

                    result = JsonConvert.SerializeObject(tran[0]);

                    resp.STATUS = true;

                    _authenticationModel.username = LoginEmail;
                    _authenticationModel.password = LoginPassword;
                    _authenticationModel.authenticated = true;

                    authGuard.OnAuthentication(_authenticationModel);
                }
                else
                {
                    ViewBag.Error = task.ERROR_MESSAGE;
                }

            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException);

                resp.STATUS = false;
                resp.ERROR_MESSAGE = ex.InnerException.Message.ToString();
            }


            if (resp.STATUS == true)
            {
                //HomeController homeController = new HomeController();

                //homeController.currentLogin = true;

                string pathredirect = Url.Action("IndexCheckLogin", "Home", new { Username = _authenticationModel.username, CurrentLogin = _authenticationModel.authenticated });


                return new RedirectResult(pathredirect);
                //return RedirectToAction(pathredirect);


            }
            else
            {
                return new RedirectResult("~/AuthSinIn/Index");
            }


        }


        public async Task<IActionResult> GetLogin(string jsonString)
        {
            List<ProfileUserManagement> tran = new List<ProfileUserManagement>();

            AuthGuard authGuard = new AuthGuard();

            ProfileUserManagement profile = new ProfileUserManagement();

            AuthenticationModel authenticationModel = new AuthenticationModel();

            Response resp = new Response();

            var oMycustomclassname = Newtonsoft.Json.JsonConvert.DeserializeObject<AuthenticationModel>(jsonString);


            var result = "";

            try
            {
                var task = await Task.Run(() => ApiHelper.GetURI("api/Authentication/GetDetail/?Username=" + oMycustomclassname.username + "&Password=" + oMycustomclassname.password + ""));

                if (task.STATUS)
                {
                    tran = JsonConvert.DeserializeObject<List<ProfileUserManagement>>(task.OUTPUT_DATA.ToString());

                    result = JsonConvert.SerializeObject(tran[0]);

                    resp.STATUS = true;

                    oMycustomclassname.authenticated = true;

                    authGuard.OnAuthentication(oMycustomclassname);
                }
                else
                {
                    resp.ERROR_MESSAGE = task.ERROR_MESSAGE;
                }

            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException);

                resp.STATUS = false;
                resp.ERROR_MESSAGE = ex.InnerException.Message.ToString();
            }


            if (resp.STATUS == true)
            {
                var userlevel = tran[0].LevelId;

                if (userlevel == null)
                {
                    userlevel = 0;
                }
                HttpContext.Session.Clear();
                HttpContext.Session.SetInt32("checkpermissionpage", 1);
                HttpContext.Session.SetInt32("islogin", 1);
                HttpContext.Session.SetString("userName", tran[0].FirstName);
                HttpContext.Session.SetString("userLastname", tran[0].LastName);
                HttpContext.Session.SetString("userMail", oMycustomclassname.username);
                HttpContext.Session.SetInt32("userLevel", Convert.ToInt32(userlevel));
                //string pathredirect = Url.Action("IndexCheckLogin", "Home", new { Username = oMycustomclassname.username, CurrentLogin = oMycustomclassname.authenticated });
                string pathredirect = Url.Action("IndexCheckLogin", "Home", new { CurrentLogin = oMycustomclassname.authenticated });

                authenticationModel.username = oMycustomclassname.username;
                authenticationModel.authenticated = oMycustomclassname.authenticated;
                authenticationModel.UrlRedirect = pathredirect;
                authenticationModel.MessageRedirect = "";

                result = JsonConvert.SerializeObject(authenticationModel);

                return Json(result);

                //return new RedirectResult(pathredirect);
            }
            else
            {
                authenticationModel.username = "";
                authenticationModel.authenticated = false;
                authenticationModel.UrlRedirect = "";
                authenticationModel.MessageRedirect = resp.ERROR_MESSAGE;

                result = JsonConvert.SerializeObject(authenticationModel);

                return Json(result);

                //return new RedirectResult("~/AuthSinIn/Index");
            }


        }

        public async Task<IActionResult> AutoLogin(string jwt)
        {
            List<ProfileUserManagement> tran = new List<ProfileUserManagement>();

            AuthGuard authGuard = new AuthGuard();

            ProfileUserManagement profile = new ProfileUserManagement();

            AuthenticationModel authenticationModel = new AuthenticationModel();

            Response resp = new Response();

            var oMycustomclassname = new AuthenticationModel();


            var result = "";
            

            try
            {

                VerifyUserRequest request = new VerifyUserRequest() { JwtToken = jwt };
                var httpContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

                var verifyJwt = Task.Run(() => ApiHelper.PostURI("api/AutoLogin/VerifyTokenStepTwo", httpContent)).Result;
                if (verifyJwt.STATUS)
                {
                    VerifyUserResponse userVerify = JsonConvert.DeserializeObject<VerifyUserResponse>(verifyJwt.OUTPUT_DATA.ToString());
                    if (!userVerify.IsError)
                    {
                        oMycustomclassname.username = userVerify.UserId;
                        oMycustomclassname.password = "autologin888";

                    }
                }


                var task = await Task.Run(() => ApiHelper.GetURI("api/Authentication/GetDetail/?Username=" + oMycustomclassname.username + "&Password=" + oMycustomclassname.password + ""));

                if (task.STATUS)
                {
                    tran = JsonConvert.DeserializeObject<List<ProfileUserManagement>>(task.OUTPUT_DATA.ToString());

                    resp.STATUS = true;

                    oMycustomclassname.authenticated = true;

                    authGuard.OnAuthentication(oMycustomclassname);
                }
                else
                {
                    resp.ERROR_MESSAGE = task.ERROR_MESSAGE;
                }

            }

            catch (Exception ex)
            {
                resp.STATUS = false;
                resp.ERROR_MESSAGE = ex.Message.ToString();
            }


            if (resp.STATUS == true)
            {
                var userlevel = tran[0].LevelId;

                if (userlevel == null)
                {
                    userlevel = 0;
                }
                HttpContext.Session.Clear();
                HttpContext.Session.SetInt32("checkpermissionpage", 1);
                HttpContext.Session.SetInt32("islogin", 1);
                HttpContext.Session.SetString("userName", tran[0].FirstName);
                HttpContext.Session.SetString("userLastname", tran[0].LastName);
                HttpContext.Session.SetString("userMail", oMycustomclassname.username);
                HttpContext.Session.SetInt32("userLevel", Convert.ToInt32(userlevel));
                //string pathredirect = Url.Action("IndexCheckLogin", "Home", new { Username = oMycustomclassname.username, CurrentLogin = oMycustomclassname.authenticated });
                string pathredirect = Url.Action("IndexCheckLogin", "Home", new { CurrentLogin = oMycustomclassname.authenticated });

                authenticationModel.username = oMycustomclassname.username;
                authenticationModel.authenticated = oMycustomclassname.authenticated;
                authenticationModel.UrlRedirect = pathredirect;
                authenticationModel.MessageRedirect = "";

                result = JsonConvert.SerializeObject(authenticationModel);

                return new RedirectResult(Url.Action("IndexCheckLogin", "Home", new { CurrentLogin = oMycustomclassname.authenticated }));
            }
            else
            {
                authenticationModel.username = "";
                authenticationModel.authenticated = false;
                authenticationModel.UrlRedirect = "";
                authenticationModel.MessageRedirect = resp.ERROR_MESSAGE;

                result = JsonConvert.SerializeObject(authenticationModel);

                return new RedirectResult("~/AuthSinIn/Index");
            }


        }


    }
}
