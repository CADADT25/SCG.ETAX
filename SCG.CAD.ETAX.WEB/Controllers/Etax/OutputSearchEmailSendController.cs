using Microsoft.AspNetCore.Mvc;
using SCG.CAD.ETAX.UTILITY.Authentication;

namespace SCG.CAD.ETAX.WEB.Controllers
{
    public class OutputSearchEmailSendController : Controller
    {
        [SessionExpire]
        public IActionResult Index()
        {
            Permission permission = new Permission();
            string pageindex = "9";
            if (!permission.CheckPremissionPage(HttpContext.Session.GetString("premissionMenu"), pageindex))
            {
                HttpContext.Session.SetInt32("checkpermissionpage", 0);
                string pathredirect = Url.Action("Index", "Home");
                return new RedirectResult(pathredirect);
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
            List<OutputSearchEmailSend> tran = new List<OutputSearchEmailSend>();

            var task = await Task.Run(() => ApiHelper.GetURI("api/OutputSearchEmailSend/GetDetail?id= " + id + " "));

            Response resp = new Response();

            var result = "";

            if (task.STATUS)
            {

                tran = JsonConvert.DeserializeObject<List<OutputSearchEmailSend>>(task.OUTPUT_DATA.ToString());

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

            List<OutputSearchEmailSend> tran = new List<OutputSearchEmailSend>();

            try
            {
                var task = await Task.Run(() => ApiHelper.GetURI("api/OutputSearchEmailSend/GetListAll"));

                if (task.STATUS)
                {
                    tran = JsonConvert.DeserializeObject<List<OutputSearchEmailSend>>(task.OUTPUT_DATA.ToString());
                    tran = tran.OrderBy(x => x.OutputSearchEmailSendCompanyCode).ThenBy(x => x.CreateDate).ToList();
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

            outputSearchEmailModel obj = new outputSearchEmailModel();
            List<OutputSearchEmailSend> tran = new List<OutputSearchEmailSend>();

            var strBuilder = new StringBuilder();

            try
            {
                var task = await Task.Run(() => ApiHelper.GetURI("api/OutputSearchEmailSend/GetListAll"));

                if (task.STATUS)
                {
                    tran = JsonConvert.DeserializeObject<List<OutputSearchEmailSend>>(task.OUTPUT_DATA.ToString());


                    obj = JsonConvert.DeserializeObject<outputSearchEmailModel>(jsonSearchString);
                    if (obj != null)
                    {
                        DateTime getMinDate = new DateTime();
                        DateTime getMaxDate = new DateTime();

                        var getStatus = obj.outPutSearchEmailStatus;

                        int statusDownload = 99;

                        getStatus = getStatus == "All" ? getStatus = "" : getStatus = obj.outPutSearchEmailStatus;

                        if (obj.outPutSearchEmailCompanyCode != null)
                        {
                            if (obj.outPutSearchEmailCompanyCode.Count > 0)
                            {
                                tran = tran.Where(x => obj.outPutSearchEmailCompanyCode.Contains(x.OutputSearchEmailSendCompanyCode)).ToList();
                            }
                        }

                        if (!string.IsNullOrEmpty(getStatus))
                        {
                            statusDownload = Convert.ToInt32(getStatus);
                            tran = tran.Where(x => x.OutputSearchEmailSendStatus == statusDownload).ToList();
                        }

                        if (!string.IsNullOrEmpty(obj.outPutSearchEmailDate))
                        {
                            var getArrayDate = obj.outPutSearchEmailDate.Split("to");
                            getMinDate = Convert.ToDateTime(getArrayDate[0].Trim());
                            getMaxDate = Convert.ToDateTime(getArrayDate[1].Trim());

                            tran = tran.Where(x => x.CreateDate >= getMinDate.Date && x.CreateDate <= getMaxDate.Date).ToList();
                        }
                    }

                    if (tran.Count() > 0)
                    {
                        strBuilder.AppendLine("" +
                            "OutputSearchEmailSendNo," +
                            "OutputSearchEmailSendCompanyCode," +
                            "OutputSearchEmailSendSubject," +
                            "OutputSearchEmailSendFrom," +
                            "OutputSearchEmailSendTo," +
                            "OutputSearchEmailSendCC," +
                            "OutputSearchEmailSendFileName," +
                            "OutputSearchEmailSendStatus," +
                            "OutputSearchEmailSendLastTime," +
                            "OutputSearchEmailSendLastBy," +
                            "CreateBy," +
                            "CreateDate," +
                            "UpdateBy," +
                            "UpdateDate," +
                            "Isactive");



                        foreach (var item in tran)
                        {
                            strBuilder.AppendLine($"" +
                                $"{item.OutputSearchEmailSendNo}," +
                                $"{item.OutputSearchEmailSendCompanyCode}," +
                                $"{item.OutputSearchEmailSendSubject}," +
                                $"{item.OutputSearchEmailSendFrom}," +
                                $"{item.OutputSearchEmailSendTo}," +
                                $"{item.OutputSearchEmailSendFileName}," +
                                $"{item.OutputSearchEmailSendStatus}," +
                                $"{item.OutputSearchEmailSendLastTime}," +
                                $"{item.OutputSearchEmailSendLastBy}," +
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

            return File(Encoding.UTF8.GetBytes(strBuilder.ToString()), "text/csv", "scg-etax-OutputSearchEmailSend.csv");

        }


        public async Task<JsonResult> Search(string jsonSearchString)
        {

            List<OutputSearchEmailSend> tran = new List<OutputSearchEmailSend>();

            Response resp = new Response();

            var result = "";

            try
            {
                var task = await Task.Run(() => ApiHelper.GetURI("api/OutputSearchEmailSend/Search?JsonString= " + jsonSearchString + " "));

                if (task.STATUS)
                {

                    tran = JsonConvert.DeserializeObject<List<OutputSearchEmailSend>>(task.OUTPUT_DATA.ToString());
                    tran = tran.OrderBy(x => x.OutputSearchEmailSendCompanyCode).ThenBy(x => x.CreateDate).ToList();

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

            var task = await Task.Run(() => ApiHelper.PostURI("api/OutputSearchEmailSend/DownloadZipFile", httpContent));

            return Json(task);
        }

        public async Task<JsonResult> DetailHistory(int id)
        {
            List<OutputSearchEmailSendHistory> tran = new List<OutputSearchEmailSendHistory>();

            var task = await Task.Run(() => ApiHelper.GetURI("api/OutputSearchEmailSendHistory/GetListAll"));

            if (task.STATUS)
            {

                tran = JsonConvert.DeserializeObject<List<OutputSearchEmailSendHistory>>(task.OUTPUT_DATA.ToString());
                tran = tran.Where(x=> x.OutputSearchEmailSendNo == id).ToList();
            }
            else
            {
                ViewBag.Error = task.MESSAGE;
            }
            return Json(new { data = tran });
        }
    }
}
