using Microsoft.AspNetCore.Mvc;
using SCG.CAD.ETAX.UTILITY.Authentication;

namespace SCG.CAD.ETAX.WEB.Controllers.Etax
{
    [SessionExpire]
    public class ProfileCertificateController : Controller
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
                ViewData["showIMPORT"] = permission.CheckControlAction(configControl, 9, userLevel, menuindex);
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
            List<ProfileCertificate> tran = new List<ProfileCertificate>();
            Response resp = new Response();

            var result = "";

            try
            {
                var task = await Task.Run(() => ApiHelper.GetURI("api/ProfileCertificate/GetDetail?id= " + id + " "));

                if (task.STATUS)
                {

                    tran = JsonConvert.DeserializeObject<List<ProfileCertificate>>(task.OUTPUT_DATA.ToString());
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

        public async Task<JsonResult> List(string companyCode)
        {
            Response resp = new Response();

            List<ProfileCertificate> tran = new List<ProfileCertificate>();

            try
            {
                var task = await Task.Run(() => ApiHelper.GetURI("api/ProfileCertificate/GetListAll"));

                if (task.STATUS)
                {
                    tran = JsonConvert.DeserializeObject<List<ProfileCertificate>>(task.OUTPUT_DATA.ToString());

                    var getComCodes = await Task.Run(() => ApiHelper.GetURI("api/ProfileCompany/GetListAll"));

                    tran = tran.Where(x => x.CertificateCompanyCode == companyCode).ToList();

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

            var task = await Task.Run(() => ApiHelper.PostURI("api/ProfileCertificate/Insert", httpContent));

            return Json(task);
        }

        public async Task<JsonResult> Update(string jsonString)
        {
            Response res = new Response();

            var httpContent = new StringContent(jsonString, Encoding.UTF8, "application/json");

            var task = await Task.Run(() => ApiHelper.PostURI("api/ProfileCertificate/Update", httpContent));

            return Json(task);
        }

        public async Task<JsonResult> Delete(string jsonString)
        {
            Response res = new Response();

            var httpContent = new StringContent(jsonString, Encoding.UTF8, "application/json");

            var task = await Task.Run(() => ApiHelper.PostURI("api/ProfileCertificate/Delete", httpContent));

            return Json(task);
        }

        public async Task<ActionResult> ExportToCsv()
        {
            Response resp = new Response();

            List<ProfileCertificate> tran = new List<ProfileCertificate>();

            var strBuilder = new StringBuilder();

            try
            {
                var task = await Task.Run(() => ApiHelper.GetURI("api/ProfileCertificate/GetListAll"));

                if (task.STATUS)
                {
                    tran = JsonConvert.DeserializeObject<List<ProfileCertificate>>(task.OUTPUT_DATA.ToString());

                    if (tran.Count() > 0)
                    {
                        strBuilder.AppendLine("" +
                            "CertificateNo," +
                            "CertificateHsmname," +
                            "CertificateHsmserial," +
                            "CertificateCertSerial," +
                            "CertificateKeyAlias," +
                            "CompanyCertificateStartDate," +
                            "CompanyCertificateEndDate," +
                            "CreateBy," +
                            "CreateDate," +
                            "UpdateBy," +
                            "UpdateDate," +
                            "Isactive");



                        foreach (var item in tran)
                        {
                            strBuilder.AppendLine($"" +
                                $"{item.CertificateNo}," +
                                $"{item.CertificateHsmname}," +
                                $"{item.CertificateHsmserial}," +
                                $"{item.CertificateCertSerial}," +
                                $"{item.CertificateKeyAlias}," +
                                $"{item.CertificateStartDate}," +
                                $"{item.CertificateEndDate}," +
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

            return File(Encoding.UTF8.GetBytes(strBuilder.ToString()), "text/csv", "scg-etax-ProfileCertificate.csv");

        }

        public async Task<JsonResult> DropDownList(string companyCode)
        {
            Response resp = new Response();

            List<ProfileCertificate> tran = new List<ProfileCertificate>();

            List<ProfileCompany> listProfileCompany = new List<ProfileCompany>();


            try
            {
                var task = await Task.Run(() => ApiHelper.GetURI("api/ProfileCertificate/GetListAll"));

                if (task.STATUS)
                {
                    tran = JsonConvert.DeserializeObject<List<ProfileCertificate>>(task.OUTPUT_DATA.ToString());

                    var getComCodes = await Task.Run(() => ApiHelper.GetURI("api/ProfileCompany/GetListAll"));

                    listProfileCompany = JsonConvert.DeserializeObject<List<ProfileCompany>>(getComCodes.OUTPUT_DATA.ToString());

                    var getTaxNo = listProfileCompany.Where(x => x.CompanyCode == companyCode).Select(x => x.TaxNumber).FirstOrDefault();

                    tran = tran.Where(x => x.Isactive == 1).ToList();

                    if (tran.Count <= 0)
                    {
                        resp.STATUS = false;
                        resp.ERROR_MESSAGE = "Data not found !";
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
                resp.STATUS = false;
                resp.ERROR_MESSAGE = ex.InnerException.ToString();
            }


            return Json(tran);
        }

    }
}
