using Microsoft.AspNetCore.Mvc;

namespace SCG.CAD.ETAX.WEB.Controllers
{
    public class ConfigXmlSignController : Controller
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
            List<ConfigXmlSign> tran = new List<ConfigXmlSign>();

            Response resp = new Response();

            var result = "";

            try
            {
                var task = await Task.Run(() => ApiHelper.GetURI("api/ConfigXmlSign/GetDetail?id= " + id + " "));

                if (task.STATUS)
                {

                    tran = JsonConvert.DeserializeObject<List<ConfigXmlSign>>(task.OUTPUT_DATA.ToString());

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

            List<ConfigXmlSign> tran = new List<ConfigXmlSign>();

            try
            {
                var task = await Task.Run(() => ApiHelper.GetURI("api/ConfigXmlSign/GetListAll"));

                if (task.STATUS)
                {
                    tran = JsonConvert.DeserializeObject<List<ConfigXmlSign>>(task.OUTPUT_DATA.ToString());

                    tran = tran.Where(x => x.ConfigXmlsignCompanycode == companyCode).ToList();
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

            var task = await Task.Run(() => ApiHelper.PostURI("api/ConfigXmlSign/Insert", httpContent));

            return Json(task);
        }

        public async Task<JsonResult> Update(string jsonString)
        {
            Response res = new Response();

            var httpContent = new StringContent(jsonString, Encoding.UTF8, "application/json");

            var task = await Task.Run(() => ApiHelper.PostURI("api/ConfigXmlSign/Update", httpContent));

            return Json(task);
        }

        public async Task<JsonResult> Delete(string jsonString)
        {
            Response res = new Response();

            var httpContent = new StringContent(jsonString, Encoding.UTF8, "application/json");

            var task = await Task.Run(() => ApiHelper.PostURI("api/ConfigXmlSign/Delete", httpContent));

            return Json(task);
        }

        public async Task<ActionResult> ExportToCsv()
        {
            Response resp = new Response();

            List<ConfigXmlSign> tran = new List<ConfigXmlSign>();

            var strBuilder = new StringBuilder();

            try
            {
                var task = await Task.Run(() => ApiHelper.GetURI("api/ConfigXmlSign/GetListAll"));

                if (task.STATUS)
                {
                    tran = JsonConvert.DeserializeObject<List<ConfigXmlSign>>(task.OUTPUT_DATA.ToString());

                    if (tran.Count() > 0)
                    {
                        strBuilder.AppendLine("" +
                            "ConfigXmlsignNo," +
                            "ConfigXmlsignCompanycode," +
                            "ConfigXmlsignCompanyTax," +
                            "ConfigXmlsignOnlineRecordNumber," +
                            "ConfigXmlsignInputSource," +
                            "ConfigXmlsignInputPath," +
                            "ConfigXmlsignOutputSource," +
                            "ConfigXmlsignOutputPath," +
                            "ConfigXmlsignOutputConvertPath," +
                            "ConfigXmlsignHsmSerial," +
                            "ConfigXmlsignCertificateSerial," +
                            "CreateBy," +
                            "CreateDate," +
                            "UpdateBy," +
                            "UpdateDate," +
                            "Isactive");



                        foreach (var item in tran)
                        {
                            strBuilder.AppendLine($"" +
                                $"{item.ConfigXmlsignNo}," +
                                $"{item.ConfigXmlsignCompanycode}," +
                                $"{item.ConfigXmlsignCompanyTax}," +
                                $"{item.ConfigXmlsignOnlineRecordNumber}," +
                                $"{item.ConfigXmlsignInputSource}," +
                                $"{item.ConfigXmlsignInputPath}," +
                                $"{item.ConfigXmlsignOutputSource}," +
                                $"{item.ConfigXmlsignOutputPath}," +
                                $"{item.ConfigXmlsignOutputConvertPath}," +
                                $"{item.ConfigXmlsignHsmSerial}," +
                                $"{item.ConfigXmlsignCertificateSerial}," +
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

            return File(Encoding.UTF8.GetBytes(strBuilder.ToString()), "text/csv", "scg-etax-ConfigXmlSign.csv");

        }


    }
}
