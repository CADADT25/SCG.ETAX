using Microsoft.AspNetCore.Mvc;
using SCG.CAD.ETAX.UTILITY.Authentication;

namespace SCG.CAD.ETAX.WEB.Controllers
{
    public class OutputSearchPrintingController : Controller
    {
        [SessionExpire]
        public IActionResult Index()
        {
            Permission permission = new Permission();
            string pageindex = "7";
            if (!permission.CheckPremissionPage(HttpContext.Session.GetString("premissionMenu"), pageindex))
            {
                HttpContext.Session.SetInt32("checkpermissionpage", 0);
                string pathredirect = Url.Action("Index", "Home");
                return new RedirectResult(pathredirect);
            }
            else
            {
                var menuindex = 7;
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
            List<OutputSearchPrinting> tran = new List<OutputSearchPrinting>();

            var task = await Task.Run(() => ApiHelper.GetURI("api/OutputSearchPrinting/GetDetail?id= " + id + " "));

            Response resp = new Response();

            var result = "";

            if (task.STATUS)
            {

                tran = JsonConvert.DeserializeObject<List<OutputSearchPrinting>>(task.OUTPUT_DATA.ToString());

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

            List<OutputSearchPrinting> tran = new List<OutputSearchPrinting>();

            try
            {
                var task = await Task.Run(() => ApiHelper.GetURI("api/OutputSearchPrinting/GetListAll"));

                if (task.STATUS)
                {
                    tran = JsonConvert.DeserializeObject<List<OutputSearchPrinting>>(task.OUTPUT_DATA.ToString());
                    tran = tran.OrderBy(x => x.OutputSearchPrintingCompanyCode).ThenBy(x => x.CreateDate).ToList();
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

            List<OutputSearchPrinting> tran = new List<OutputSearchPrinting>();

            var strBuilder = new StringBuilder();

            try
            {
                var task = await Task.Run(() => ApiHelper.GetURI("api/OutputSearchPrinting/GetListAll"));

                if (task.STATUS)
                {
                    tran = JsonConvert.DeserializeObject<List<OutputSearchPrinting>>(task.OUTPUT_DATA.ToString());

                    outputSearchPrintingModel obj = new outputSearchPrintingModel();
                    obj = JsonConvert.DeserializeObject<outputSearchPrintingModel>(jsonSearchString);
                    if (obj != null)
                    {
                        DateTime getMinDate = new DateTime();
                        DateTime getMaxDate = new DateTime();

                        var getStatus = obj.outPutSearchStatus;

                        int statusDownload = 99;

                        getStatus = getStatus == "All" ? getStatus = "" : getStatus = obj.outPutSearchStatus;

                        tran = JsonConvert.DeserializeObject<List<OutputSearchPrinting>>(task.OUTPUT_DATA.ToString());

                        if (obj.outPutSearchCompanyCode != null)
                        {
                            if (obj.outPutSearchCompanyCode.Count > 0)
                            {
                                tran = tran.Where(x => obj.outPutSearchCompanyCode.Contains(x.OutputSearchPrintingCompanyCode)).ToList();
                            }
                        }

                        if (!string.IsNullOrEmpty(getStatus))
                        {
                            statusDownload = Convert.ToInt32(getStatus);

                            tran = tran.Where(x => x.OutputSearchPrintingDowloadStatus == statusDownload).ToList();
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
                            "OutputSearchPrintingNo," +
                            "OutputSearchPrintingCompanyCode," +
                            "OutputSearchPrintingFileName," +
                            "OutputSearchPrintingFullPath," +
                            "OutputSearchPrintingDowloadStatus," +
                            "OutputSearchPrintingDowloadCount," +
                            "OutputSearchPrintingDowloadLastTime," +
                            "OutputSearchPrintingDowloadLastBy," +
                            "CreateBy," +
                            "CreateDate," +
                            "UpdateBy," +
                            "UpdateDate," +
                            "Isactive");



                        foreach (var item in tran)
                        {
                            strBuilder.AppendLine($"" +
                                $"{item.OutputSearchPrintingNo}," +
                                $"{item.OutputSearchPrintingCompanyCode}," +
                                $"{item.OutputSearchPrintingFileName}," +
                                $"{item.OutputSearchPrintingFullPath}," +
                                $"{item.OutputSearchPrintingDowloadStatus}," +
                                $"{item.OutputSearchPrintingDowloadCount}," +
                                $"{item.OutputSearchPrintingDowloadLastTime}," +
                                $"{item.OutputSearchPrintingDowloadLastBy}," +
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

            return File(Encoding.UTF8.GetBytes(strBuilder.ToString()), "text/csv", "scg-etax-OutputSearchPrinting.csv");

        }

        public async Task<JsonResult> Search(string jsonSearchString)
        {

            List<OutputSearchPrinting> tran = new List<OutputSearchPrinting>();

            Response resp = new Response();

            var result = "";

            try
            {
                var task = await Task.Run(() => ApiHelper.GetURI("api/OutputSearchPrinting/Search?JsonString= " + jsonSearchString + " "));

                if (task.STATUS)
                {

                    tran = JsonConvert.DeserializeObject<List<OutputSearchPrinting>>(task.OUTPUT_DATA.ToString());
                    tran = tran.OrderBy(x => x.OutputSearchPrintingCompanyCode).ThenBy(x => x.CreateDate).ToList();

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

            var task = await Task.Run(() => ApiHelper.PostURI("api/OutputSearchPrinting/DownloadZipFile", httpContent));

            return Json(task);
        }

        public async Task<JsonResult> DetailHistory(int id)
        {
            List<OutputSearchPrintingDowloadHistory> tran = new List<OutputSearchPrintingDowloadHistory>();

            var task = await Task.Run(() => ApiHelper.GetURI("api/OutputSearchPrintingDowloadHistory/GetListAll"));

            if (task.STATUS)
            {

                tran = JsonConvert.DeserializeObject<List<OutputSearchPrintingDowloadHistory>>(task.OUTPUT_DATA.ToString());
                tran = tran.Where(x => x.OutputSearchPrintingNo == id).ToList();
            }
            else
            {
                ViewBag.Error = task.MESSAGE;
            }
            return Json(new { data = tran });
        }
    }
}
