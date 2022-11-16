using Microsoft.AspNetCore.Mvc;
using SCG.CAD.ETAX.UTILITY.Authentication;

namespace SCG.CAD.ETAX.WEB.Controllers
{
    public class ProfileEmailTemplateController : Controller
    {
        [SessionExpire]
        public IActionResult Index()
        {
            Permission permission = new Permission();
            string pageindex = "23";
            if (!permission.CheckPremissionPage(HttpContext.Session.GetString("premissionMenu"), pageindex))
            {
                HttpContext.Session.SetInt32("checkpermissionpage", 0);
                string pathredirect = Url.Action("Index", "Home");
                return new RedirectResult(pathredirect);
            }
            else
            {
                var menuindex = 23;
                var userLevel = HttpContext.Session.GetInt32("userLevel").ToString();
                var configControl = JsonConvert.DeserializeObject<List<ConfigControlFunction>>(HttpContext.Session.GetString("controlPermission"));

                ViewData["showCREATE"] = permission.CheckControlAction(configControl, 1, userLevel, menuindex);
                ViewData["showUPDATE"] = permission.CheckControlAction(configControl, 2, userLevel, menuindex);
                ViewData["showDELETE"] = permission.CheckControlAction(configControl, 3, userLevel, menuindex);
                ViewData["showEXPORT"] = permission.CheckControlAction(configControl, 4, userLevel, menuindex);
                ViewData["showDOWNLOAD"] = permission.CheckControlAction(configControl, 5, userLevel, menuindex);
                ViewData["showVIEW"] = permission.CheckControlAction(configControl, 6, userLevel, menuindex);
                ViewData["showSEARCH"] = permission.CheckControlAction(configControl, 7, userLevel, menuindex);
                ViewData["showADMINTOOL"] = permission.CheckControlAction(configControl, 8, userLevel, menuindex);

                return View();
            }
        }

        public IActionResult _Content()
        {
            return View();
        }

        public IActionResult _Modal()
        {
            return View();
        }

        public IActionResult _Create()
        {
            return View();
        }

        public IActionResult _Update()
        {
            return View();
        }


        public async Task<JsonResult> Detail(int id)
        {
            List<ProfileEmailTemplate> tran = new List<ProfileEmailTemplate>();

            Response resp = new Response();

            var result = "";

            try
            {
                var task = await Task.Run(() => ApiHelper.GetURI("api/ProfileEmailTemplate/GetDetail?id= " + id + " "));

                if (task.STATUS)
                {

                    tran = JsonConvert.DeserializeObject<List<ProfileEmailTemplate>>(task.OUTPUT_DATA.ToString());

                    result = JsonConvert.SerializeObject(tran[0]);

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

        public async Task<JsonResult> List()
        {
            Response resp = new Response();

            List<ProfileEmailTemplate> tran = new List<ProfileEmailTemplate>();

            try
            {
                var task = await Task.Run(() => ApiHelper.GetURI("api/ProfileEmailTemplate/GetListAll"));

                if (task.STATUS)
                {
                    tran = JsonConvert.DeserializeObject<List<ProfileEmailTemplate>>(task.OUTPUT_DATA.ToString());
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


            return Json(new { data = tran });
        }

        public async Task<JsonResult> Insert(string jsonString)
        {
            Response res = new Response();

            var httpContent = new StringContent(jsonString, Encoding.UTF8, "application/json");

            var task = await Task.Run(() => ApiHelper.PostURI("api/ProfileEmailTemplate/Insert", httpContent));

            return Json(task);
        }

        public async Task<JsonResult> Update(string jsonString)
        {
            Response res = new Response();

            var httpContent = new StringContent(jsonString, Encoding.UTF8, "application/json");

            var task = await Task.Run(() => ApiHelper.PostURI("api/ProfileEmailTemplate/Update", httpContent));

            return Json(task);
        }

        public async Task<JsonResult> Delete(string jsonString)
        {
            Response res = new Response();

            var httpContent = new StringContent(jsonString, Encoding.UTF8, "application/json");

            var task = await Task.Run(() => ApiHelper.PostURI("api/ProfileEmailTemplate/Delete", httpContent));

            return Json(task);
        }

        public async Task<ActionResult> ExportToCsv()
        {
            Response resp = new Response();

            List<ProfileEmailTemplate> tran = new List<ProfileEmailTemplate>();

            var strBuilder = new StringBuilder();

            try
            {
                var task = await Task.Run(() => ApiHelper.GetURI("api/ProfileEmailTemplate/GetListAll"));

                if (task.STATUS)
                {
                    tran = JsonConvert.DeserializeObject<List<ProfileEmailTemplate>>(task.OUTPUT_DATA.ToString());

                    if (tran.Count() > 0)
                    {
                        strBuilder.AppendLine("" +
                            "EmailTemplateNo," +
                            "EmailTypeNo," +
                            "EmailBody," +
                            "CreateBy," +
                            "CreateDate," +
                            "UpdateBy," +
                            "UpdateDate," +
                            "Isactive");


                        foreach (var item in tran)
                        {
                            strBuilder.AppendLine($"" +
                                $"{item.EmailTemplateNo}," +
                                $"{item.EmailTypeNo}," +
                                $"{item.EmailBody}," +
                                $"{item.CreateBy}," +
                                $"{item.CreateDate}," +
                                $"{item.UpdateBy}," +
                                $"{item.UpdateDate}," +
                                $"{item.Isactive}");
                        }

                        resp.STATUS = true;
                    }
                    else
                    {
                        resp.STATUS = false;
                    }
                }
                else
                {
                    ViewBag.Error = task.MESSAGE;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException.ToString());
            }

            return File(Encoding.UTF8.GetBytes(strBuilder.ToString()), "text/csv", "scg-etax-ProfileEmailTemplate.csv");

        }

        public async Task<JsonResult> DropDownList()
        {
            Response resp = new Response();

            List<ProfileEmailTemplate> tran = new List<ProfileEmailTemplate>();

            var result = "";

            try
            {
                var task = await Task.Run(() => ApiHelper.GetURI("api/ProfileEmailTemplate/GetListAll"));

                if (task.STATUS)
                {
                    tran = JsonConvert.DeserializeObject<List<ProfileEmailTemplate>>(task.OUTPUT_DATA.ToString());

                    if (tran.Count > 0)
                    {
                        tran = tran.Where(x => x.Isactive == 1).OrderBy(x => x.EmailTemplateName).ToList();
                    }
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


            return Json(tran);
        }


    }
}
