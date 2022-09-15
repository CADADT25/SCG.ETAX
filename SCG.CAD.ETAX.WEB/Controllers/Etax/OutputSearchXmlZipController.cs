using Microsoft.AspNetCore.Mvc;

namespace SCG.CAD.ETAX.WEB.Controllers
{
    public class OutputSearchXmlZipController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult _Content()
        {
            return View();
        }

        public IActionResult _Search()
        {
            return View();
        }

        public IActionResult _Modal()
        {
            return View();
        }


        public async Task<JsonResult> Detail(int id)
        {
            List<OutputSearchXmlZip> tran = new List<OutputSearchXmlZip>();

            var task = await Task.Run(() => ApiHelper.GetURI("api/OutputSearchXmlZip/GetDetail?id= " + id + " "));

            Response resp = new Response();

            var result = "";

            if (task.STATUS)
            {

                tran = JsonConvert.DeserializeObject<List<OutputSearchXmlZip>>(task.OUTPUT_DATA.ToString());

                result = JsonConvert.SerializeObject(tran[0]);

            }
            else
            {
                ViewBag.Error = task.MESSAGE;
            }
            return Json(result);
        }

        public async Task<JsonResult> List()
        {
            Response resp = new Response();

            List<OutputSearchXmlZip> tran = new List<OutputSearchXmlZip>();

            try
            {
                var task = await Task.Run(() => ApiHelper.GetURI("api/OutputSearchXmlZip/GetListAll"));

                if (task.STATUS)
                {
                    tran = JsonConvert.DeserializeObject<List<OutputSearchXmlZip>>(task.OUTPUT_DATA.ToString());
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

        public async Task<ActionResult> ExportToCsv()
        {
            Response resp = new Response();

            List<OutputSearchXmlZip> tran = new List<OutputSearchXmlZip>();

            var strBuilder = new StringBuilder();

            try
            {
                var task = await Task.Run(() => ApiHelper.GetURI("api/OutputSearchXmlZip/GetListAll"));

                if (task.STATUS)
                {
                    tran = JsonConvert.DeserializeObject<List<OutputSearchXmlZip>>(task.OUTPUT_DATA.ToString());

                    if (tran.Count() > 0)
                    {
                        strBuilder.AppendLine("" +
                            "OutputSearchXmlZipNo," +
                            "OutputSearchXmlZipCompanyCode," +
                            "OutputSearchXmlZipFileName," +
                            "OutputSearchXmlZipFullPath," +
                            "OutputSearchXmlZipDowloadStatus," +
                            "OutputSearchXmlZipDowloadCount," +
                            "OutputSearchXmlZipDowloadLastTime," +
                            "OutputSearchXmlZipDowloadLastBy," +
                            "CreateBy," +
                            "CreateDate," +
                            "UpdateBy," +
                            "UpdateDate," +
                            "Isactive");



                        foreach (var item in tran)
                        {
                            strBuilder.AppendLine($"" +
                                $"{item.OutputSearchXmlZipNo}," +
                                $"{item.OutputSearchXmlZipCompanyCode}," +
                                $"{item.OutputSearchXmlZipFileName}," +
                                $"{item.OutputSearchXmlZipFullPath}," +
                                $"{item.OutputSearchXmlZipDowloadStatus}," +
                                $"{item.OutputSearchXmlZipDowloadCount}," +
                                $"{item.OutputSearchXmlZipDowloadLastTime}," +
                                $"{item.OutputSearchXmlZipDowloadLastBy}," +
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

            return File(Encoding.UTF8.GetBytes(strBuilder.ToString()), "text/csv", "scg-etax-OutputSearchXmlZip.csv");

        }


        public async Task<JsonResult> Search(string jsonSearchString)
        {

            List<OutputSearchXmlZip> tran = new List<OutputSearchXmlZip>();

            Response resp = new Response();

            var result = "";

            try
            {
                var task = await Task.Run(() => ApiHelper.GetURI("api/OutputSearchXmlZip/Search?JsonString= " + jsonSearchString + " "));

                if (task.STATUS)
                {

                    tran = JsonConvert.DeserializeObject<List<OutputSearchXmlZip>>(task.OUTPUT_DATA.ToString());

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

        public async Task<JsonResult> DownloadZipFile(string jsonString)
        {
            var httpContent = new StringContent(jsonString, Encoding.UTF8, "application/json");

            var task = await Task.Run(() => ApiHelper.PostURI("api/OutputSearchXmlZip/DownloadZipFile", httpContent));

            return Json(task);
        }

        public async Task<JsonResult> DetailHistory(int id)
        {
            List<OutputSearchXmlZipDowloadHistory> tran = new List<OutputSearchXmlZipDowloadHistory>();

            var task = await Task.Run(() => ApiHelper.GetURI("api/OutputSearchXmlZipDowloadHistory/GetListAll"));

            if (task.STATUS)
            {

                tran = JsonConvert.DeserializeObject<List<OutputSearchXmlZipDowloadHistory>>(task.OUTPUT_DATA.ToString());
                tran = tran.Where(x => x.OutputSearchXmlZipNo == id).ToList();
            }
            else
            {
                ViewBag.Error = task.MESSAGE;
            }
            return Json(new { data = tran });
        }
    }
}
