using Microsoft.AspNetCore.Mvc;

namespace SCG.CAD.ETAX.WEB.Controllers.Etax
{
    public class ProfileCertificateController : Controller
    {

        public IActionResult Index()
        {
            return View();
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
