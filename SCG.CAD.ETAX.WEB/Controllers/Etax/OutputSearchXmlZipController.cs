using Microsoft.AspNetCore.Mvc;
using SCG.CAD.ETAX.UTILITY.Authentication;

namespace SCG.CAD.ETAX.WEB.Controllers
{
    public class OutputSearchXmlZipController : Controller
    {
        [SessionExpire]
        public IActionResult Index()
        {
            Permission permission = new Permission();
            string pageindex = "8";
            if (!permission.CheckPremissionPage(HttpContext.Session.GetString("premissionMenu"), pageindex))
            {
                HttpContext.Session.SetInt32("checkpermissionpage", 0);
                return View("~/Views/Home/index.cshtml");
            }
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
                    tran = tran.OrderBy(x=> x.OutputSearchXmlZipCompanyCode).ThenBy(x=> x.CreateDate).ToList();
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

        public async Task<ActionResult> ExportToCsv(string jsonSearchString)
        {
            Response resp = new Response();

            outputSearchXmlModel obj = new outputSearchXmlModel();
            List<OutputSearchXmlZip> tran = new List<OutputSearchXmlZip>();

            var strBuilder = new StringBuilder();

            try
            {
                var task = await Task.Run(() => ApiHelper.GetURI("api/OutputSearchXmlZip/GetListAll"));

                if (task.STATUS)
                {
                    tran = JsonConvert.DeserializeObject<List<OutputSearchXmlZip>>(task.OUTPUT_DATA.ToString());

                    obj = JsonConvert.DeserializeObject<outputSearchXmlModel>(jsonSearchString);

                    if (obj != null)
                    {
                        DateTime getMinDate = new DateTime();
                        DateTime getMaxDate = new DateTime();

                        var getDocType = obj.outPutSearchDocType.ToUpper();

                        var getStatus = obj.outPutSearchStatus;

                        int statusDownload = 99;

                        getStatus = getStatus == "All" ? getStatus = "" : getStatus = obj.outPutSearchStatus;

                        if (obj.outPutSearchCompanyCode != null)
                        {
                            if (obj.outPutSearchCompanyCode.Count > 0)
                            {
                                tran = tran.Where(x => obj.outPutSearchCompanyCode.Contains(x.OutputSearchXmlZipCompanyCode)).ToList();
                            }
                        }

                        if (!string.IsNullOrEmpty(getStatus))
                        {
                            statusDownload = Convert.ToInt32(getStatus);
                            tran = tran.Where(x => x.OutputSearchXmlZipDowloadStatus == statusDownload).ToList();
                        }

                        if (!string.IsNullOrEmpty(getDocType) && getDocType.ToUpper() != "ALL")
                        {
                            tran = tran.Where(x => obj.outPutSearchDocType.ToUpper() == x.OutputSearchXmlZipDocType.ToUpper()).ToList();
                        }

                        if (!string.IsNullOrEmpty(obj.outPutSearchDate))
                        {
                            var getArrayDate = obj.outPutSearchDate.Split("to");
                            getMinDate = Convert.ToDateTime(getArrayDate[0].Trim());
                            getMaxDate = Convert.ToDateTime(getArrayDate[1].Trim());

                            tran = tran.Where(x => x.CreateDate >= getMinDate.Date && x.CreateDate <= getMaxDate.Date).ToList();
                        }
                    }

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
                    tran = tran.OrderBy(x => x.OutputSearchXmlZipCompanyCode).ThenBy(x => x.CreateDate).ToList();
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
