using Microsoft.AspNetCore.Mvc;

namespace SCG.CAD.ETAX.WEB.Controllers
{
    public class ConfigPdfSignController : Controller
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
            List<ConfigPdfSign> tran = new List<ConfigPdfSign>();

            Response resp = new Response();

            var result = "";

            try
            {
                var task = await Task.Run(() => ApiHelper.GetURI("api/ConfigPdfSign/GetDetail?id= " + id + " "));

                if (task.STATUS)
                {

                    tran = JsonConvert.DeserializeObject<List<ConfigPdfSign>>(task.OUTPUT_DATA.ToString());

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

            List<ConfigPdfSign> tran = new List<ConfigPdfSign>();

            try
            {
                var task = await Task.Run(() => ApiHelper.GetURI("api/ConfigPdfSign/GetListAll"));

                if (task.STATUS)
                {
                    tran = JsonConvert.DeserializeObject<List<ConfigPdfSign>>(task.OUTPUT_DATA.ToString());
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

            var task = await Task.Run(() => ApiHelper.PostURI("api/ConfigPdfSign/Insert", httpContent));

            return Json(task);
        }

        public async Task<JsonResult> Update(string jsonString)
        {
            Response res = new Response();

            var httpContent = new StringContent(jsonString, Encoding.UTF8, "application/json");

            var task = await Task.Run(() => ApiHelper.PostURI("api/ConfigPdfSign/Update", httpContent));

            return Json(task);
        }

        public async Task<JsonResult> Delete(string jsonString)
        {
            Response res = new Response();

            var httpContent = new StringContent(jsonString, Encoding.UTF8, "application/json");

            var task = await Task.Run(() => ApiHelper.PostURI("api/ConfigPdfSign/Delete", httpContent));

            return Json(task);
        }

        public async Task<ActionResult> ExportToCsv()
        {
            Response resp = new Response();

            List<ConfigPdfSign> tran = new List<ConfigPdfSign>();

            var strBuilder = new StringBuilder();

            try
            {
                var task = await Task.Run(() => ApiHelper.GetURI("api/ConfigPdfSign/GetListAll"));

                if (task.STATUS)
                {
                    tran = JsonConvert.DeserializeObject<List<ConfigPdfSign>>(task.OUTPUT_DATA.ToString());

                    if (tran.Count() > 0)
                    {
                        strBuilder.AppendLine("" +
                            "ConfigPdfsignNo," +
                            "ConfigPdfsignCompanyCode," +
                            "ConfigPdfsignCompanyTax," +
                            "ConfigPdfsignUlx," +
                            "ConfigPdfsignUly," +
                            "ConfigPdfsignPage," +
                            "ConfigPdfsignOnlineRecordNumber," +
                            "ConfigPdfsignInputSource," +
                            "ConfigPdfsignInputType," +
                            "ConfigPdfsignInputPath," +
                            "ConfigPdfsignOutputSource," +
                            "ConfigPdfsignOutputType," +
                            "ConfigPdfsignOutputPath," +
                            "ConfigPdfsignHsmSerial," +
                            "ConfigPdfsignKeyAlias," +
                            "CreateBy," +
                            "CreateDate," +
                            "UpdateBy," +
                            "UpdateDate," +
                            "Isactive");



                        foreach (var item in tran)
                        {
                            strBuilder.AppendLine($"" +
                                $"{item.ConfigPdfsignNo}," +
                                $"{item.ConfigPdfsignCompanyCode}," +
                                $"{item.ConfigPdfsignCompanyTax}," +
                                $"{item.ConfigPdfsignUlx}," +
                                $"{item.ConfigPdfsignUly}," +
                                $"{item.ConfigPdfsignPage}," +
                                $"{item.ConfigPdfsignOnlineRecordNumber}," +
                                $"{item.ConfigPdfsignInputSource}," +
                                $"{item.ConfigPdfsignInputType}," +
                                $"{item.ConfigPdfsignInputPath}," +
                                $"{item.ConfigPdfsignOutputSource}," +
                                $"{item.ConfigPdfsignOutputType}," +
                                $"{item.ConfigPdfsignOutputPath}," +
                                $"{item.ConfigPdfsignHsmSerial}," +
                                $"{item.ConfigPdfsignKeyAlias}," +
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

            return File(Encoding.UTF8.GetBytes(strBuilder.ToString()), "text/csv", "scg-etax-ConfigPdfSign.csv");

        }


    }
}
