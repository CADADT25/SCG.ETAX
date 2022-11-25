using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.AspNetCore.Mvc;
using SCG.CAD.ETAX.UTILITY.Authentication;

namespace SCG.CAD.ETAX.WEB.Controllers
{
    public class ProfileCompanyController : Controller
    {
        [SessionExpire]
        public IActionResult Index()
        {
            Permission permission = new Permission();
            string pageindex = "16";
            if (!permission.CheckPremissionPage(HttpContext.Session.GetString("premissionMenu"), pageindex))
            {
                HttpContext.Session.SetInt32("checkpermissionpage", 0);
                string pathredirect = Url.Action("Index", "Home");
                return new RedirectResult(pathredirect);
            }
            else
            {
                var menuindex = 16;
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
                var comcode = JsonConvert.DeserializeObject<List<string>>(HttpContext.Session.GetString("premissionComCode"));
                ViewData["companycode"] = comcode;

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
            List<ProfileCompany> tran = new List<ProfileCompany>();

            Response resp = new Response();

            var result = "";

            try
            {
                var task = await Task.Run(() => ApiHelper.GetURI("api/ProfileCompany/GetDetail?id= " + id + " "));

                if (task.STATUS)
                {

                    tran = JsonConvert.DeserializeObject<List<ProfileCompany>>(task.OUTPUT_DATA.ToString());



                    result = JsonConvert.SerializeObject(tran[0]);

                    ViewBag.CompanyCode = tran[0].CompanyCode;

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

            List<ProfileCompany> tran = new List<ProfileCompany>();

            try
            {
                var task = await Task.Run(() => ApiHelper.GetURI("api/ProfileCompany/GetListAll"));

                if (task.STATUS)
                {
                    var comcode = JsonConvert.DeserializeObject<List<string>>(HttpContext.Session.GetString("premissionComCode"));

                    tran = JsonConvert.DeserializeObject<List<ProfileCompany>>(task.OUTPUT_DATA.ToString());

                    tran = tran.Where(x => comcode.Contains(x.CompanyCode)).OrderBy(x => x.CompanyCode).ToList();
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
            Response resp = new Response();

            var httpContent = new StringContent(jsonString, Encoding.UTF8, "application/json");

            var task = await Task.Run(() => ApiHelper.PostURI("api/ProfileCompany/Insert", httpContent));

            resp = task;

            return Json(resp);
        }

        public async Task<JsonResult> Update(string jsonString)
        {
            Response res = new Response();

            var httpContent = new StringContent(jsonString, Encoding.UTF8, "application/json");

            var task = await Task.Run(() => ApiHelper.PostURI("api/ProfileCompany/Update", httpContent));

            return Json(task);
        }

        public async Task<JsonResult> Delete(string jsonString)
        {
            Response res = new Response();

            var httpContent = new StringContent(jsonString, Encoding.UTF8, "application/json");

            var task = await Task.Run(() => ApiHelper.PostURI("api/ProfileCompany/Delete", httpContent));

            return Json(task);
        }

        public async Task<ActionResult> ExportToCsv()
        {
            Response resp = new Response();

            List<ProfileCompany> tran = new List<ProfileCompany>();

            var strBuilder = new StringBuilder();

            try
            {
                var task = await Task.Run(() => ApiHelper.GetURI("api/ProfileCompany/GetListAll"));
                //ExportFileCSVAsync();
                if (task.STATUS)
                {
                    tran = JsonConvert.DeserializeObject<List<ProfileCompany>>(task.OUTPUT_DATA.ToString());

                    if (tran.Count() > 0)
                    {
                        strBuilder.AppendLine("" +
                            "CompanyNo," +
                            "CompanyCode," +
                            "CompanyNameTh," +
                            "CompanyNameEn," +
                            "CertificateProfileNo," +
                            "TaxNumber," +
                            "IsEbill," +
                            "CreateBy," +
                            "CreateDate," +
                            "UpdateBy," +
                            "UpdateDate," +
                            "Isactive");


                        foreach (var item in tran)
                        {
                            strBuilder.AppendLine($"" +
                                $"{item.CompanyNo}," +
                                $"{item.CompanyCode}," +
                                $"{item.CompanyNameTh}," +
                                $"{item.CompanyNameEn}," +
                                $"{item.CertificateProfileNo}," +
                                $"{item.TaxNumber}," +
                                $"{item.IsEbill}," +
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
            //System.Text.Encoding.GetEncoding("windows-874");
            return File(Encoding.UTF8.GetBytes(strBuilder.ToString()), "text/csv", "scg-etax-ProfileCompany.csv");
            //return File(Encoding.UTF8.GetBytes(strBuilder.ToString()), "text/csv", "scg-etax-ProfileCompany.csv");

        }

        public async Task<JsonResult> DropDownList()
        {
            Response resp = new Response();

            List<ProfileCompany> tran = new List<ProfileCompany>();

            var result = "";

            try
            {
                var task = await Task.Run(() => ApiHelper.GetURI("api/ProfileCompany/GetListAll"));

                if (task.STATUS)
                {
                    tran = JsonConvert.DeserializeObject<List<ProfileCompany>>(task.OUTPUT_DATA.ToString());

                    if (tran.Count > 0)
                    {
                        tran = tran.Where(x => x.Isactive == 1).OrderBy(x => x.CompanyCode).ToList();
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

        public async Task<JsonResult> ExportFile()
        {
            var task = await Task.Run(() => ApiHelper.GetURI("api/ProfileCompany/ExportDataProfileCompany"));

            return Json(task);
        }


    }
}
