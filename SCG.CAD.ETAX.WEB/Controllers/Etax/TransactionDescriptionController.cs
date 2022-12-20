using Microsoft.AspNetCore.Mvc;
using SCG.CAD.ETAX.UTILITY.Authentication;

namespace SCG.CAD.ETAX.WEB.Controllers
{
    [SessionExpire]
    public class TransactionDescriptionController : Controller
    {
        [SessionExpire]
        public IActionResult Index()
        {
            Permission permission = new Permission();
            string pageindex = "5";
            if (!permission.CheckPremissionPage(HttpContext.Session.GetString("premissionMenu"), pageindex))
            {
                HttpContext.Session.SetInt32("checkpermissionpage", 0);
                string pathredirect = Url.Action("Index", "Home");
                return new RedirectResult(pathredirect);
            }
            else
            {
                var menuindex = 5;
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

        public IActionResult _Search()
        {
            return View();
        }

        public IActionResult _Modal()
        {
            return View();
        }

        public IActionResult _View()
        {
            return View();
        }


        public async Task<JsonResult> Detail(int id)
        {
            List<TransactionDescription> tran = new List<TransactionDescription>();

            Response resp = new Response();

            var result = "";

            try
            {
                var task = await Task.Run(() => ApiHelper.GetURI("api/TransactionDescription/GetDetail?id= " + id + " "));

                if (task.STATUS)
                {

                    tran = JsonConvert.DeserializeObject<List<TransactionDescription>>(task.OUTPUT_DATA.ToString());

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

        public async Task<JsonResult> List(string transactionSearchJson)
        {
            if (!string.IsNullOrEmpty(transactionSearchJson))
            {
                var transactionSearchModel = JsonConvert.DeserializeObject<transactionSearchModel>(transactionSearchJson);

            }

            Response resp = new Response();

            List<TransactionDescription> tran = new List<TransactionDescription>();

            try
            {
                var task = await Task.Run(() => ApiHelper.GetURI("api/TransactionDescription/GetListByGroup?param=" + HttpContext.Session.GetString("userMail")));

                if (task.STATUS)
                {
                    tran = JsonConvert.DeserializeObject<List<TransactionDescription>>(task.OUTPUT_DATA.ToString());
                    if (string.IsNullOrEmpty(transactionSearchJson))
                    {
                        var comcode = JsonConvert.DeserializeObject<List<string>>(HttpContext.Session.GetString("premissionComCode"));
                        tran = tran.Where(x => comcode.Contains(x.CompanyCode)).ToList();
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


            return Json(new { data = tran });
        }

        public async Task<JsonResult> Search(string transactionSearchJson)
        {

            List<TransactionDescription> tran = new List<TransactionDescription>();

            Response resp = new Response();

            var result = "";

            try
            {
                var request = JsonConvert.DeserializeObject<transactionSearchModel>(transactionSearchJson);
                request.user = HttpContext.Session.GetString("userMail");
                //var task = await Task.Run(() => ApiHelper.GetURI("api/TransactionDescription/Search?JsonString= " + transactionSearchJson + " "));
                var httpContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
                var task = await Task.Run(() => ApiHelper.PostURI("api/TransactionDescription/Search", httpContent));

                if (task.STATUS)
                {

                    tran = JsonConvert.DeserializeObject<List<TransactionDescription>>(task.OUTPUT_DATA.ToString());

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

            var task = await Task.Run(() => ApiHelper.PostURI("api/TransactionDescription/Insert", httpContent));

            return Json(task);
        }

        public async Task<JsonResult> Update(string jsonString)
        {
            Response res = new Response();

            var httpContent = new StringContent(jsonString, Encoding.UTF8, "application/json");

            var task = await Task.Run(() => ApiHelper.PostURI("api/TransactionDescription/Update", httpContent));

            return Json(task);
        }

        public async Task<JsonResult> Delete(string jsonString)
        {
            Response res = new Response();

            var httpContent = new StringContent(jsonString, Encoding.UTF8, "application/json");

            var task = await Task.Run(() => ApiHelper.PostURI("api/TransactionDescription/Delete", httpContent));

            return Json(task);
        }

        public async Task<ActionResult> ExportToCsv()
        {
            Response resp = new Response();

            List<TransactionDescription> tran = new List<TransactionDescription>();

            var strBuilder = new StringBuilder();

            try
            {
                var task = await Task.Run(() => ApiHelper.GetURI("api/TransactionDescription/GetListAll"));

                if (task.STATUS)
                {
                    tran = JsonConvert.DeserializeObject<List<TransactionDescription>>(task.OUTPUT_DATA.ToString());

                    if (tran.Count() > 0)
                    {
                        strBuilder.AppendLine("" +
                            "TransactionNo," + "BillingNumber," + "BillingDate," + "BillingYear," + "ProcessingDate," +
                            "CompanyCode," + "CompanyName," + "CustomerId," + "CustomerName," + "SoldTo," +
                            "ShipTo," + "BillTo," + "Payer," + "SourceName," + "Foc," +
                            "Ic," + "PostingYear," + "FiDoc," + "ImageDocType," + "DocType," +
                            "SellOrg," + "PoNumber," + "TypeInput," + "GenerateStatus," + "GenerateDetail," +
                            "XmlSignStatus," + "XmlSignDetail," + "XmlSignDateTime," + "PdfSignStatus," +
                            "PdfSignDetail," + "PdfSignDateTime," + "PrintStatus," + "PrintDetail," + "PrintDateTime," +
                            "EmailSendStatus," + "EmailSendDetail," + "EmailSendDateTime," + "XmlCompressStatus," + "XmlCompressDetail," +
                            "XmlCompressDateTime," + "PdfIndexingStatus," + "PdfIndexingDetail," + "PdfIndexingDateTime," + "PdfSignLocation," +
                            "XmlSignLocation," + "OutputXmlTransactionNo," + "OutputPdfTransactionNo," + "OutputMailTransactionNo," + "DmsAttachmentFileName," + "DmsAttachmentFilePath," +
                            "CreateBy," + "CreateDate," + "UpdateBy," + "UpdateDate," + "Isactive,"
                            );



                        foreach (var item in tran)
                        {
                            strBuilder.AppendLine($"" +
                                $"{item.TransactionNo}," + $"{item.BillingNumber}," + $"{item.BillingDate}," + $"{item.BillingYear}," + $"{item.ProcessingDate}," +
                                $"{item.CompanyCode}," + $"{item.CompanyName}," + $"{item.CustomerId}," + $"{item.CustomerName}," + $"{item.SoldTo}," +
                                $"{item.ShipTo}," + $"{item.BillTo}," + $"{item.Payer}," + $"{item.SourceName}," + $"{item.Foc}," +
                                $"{item.Ic}," + $"{item.PostingYear}," + $"{item.FiDoc}," + $"{item.ImageDocType}," + $"{item.DocType}," +
                                $"{item.SellOrg}," + $"{item.PoNumber}," + $"{item.TypeInput}," + $"{item.GenerateStatus}," + $"{item.GenerateDetail}," +
                                $"{item.XmlSignStatus}," + $"{item.XmlSignDetail}," + $"{item.XmlSignDateTime}," + $"{item.PdfSignStatus}," +
                                $"{item.PdfSignDetail}," + $"{item.PdfSignDateTime}," + $"{item.PrintStatus}," + $"{item.PrintDetail}," + $"{item.PrintDateTime}," +
                                $"{item.EmailSendStatus}," + $"{item.EmailSendDetail}," + $"{item.EmailSendDateTime}," + $"{item.XmlCompressStatus}," + $"{item.XmlCompressDetail}," +
                                $"{item.XmlCompressDateTime}," + $"{item.PdfIndexingStatus}," + $"{item.PdfIndexingDetail}," + $"{item.PdfIndexingDateTime}," + $"{item.PdfSignLocation}," +
                                $"{item.XmlSignLocation}," + $"{item.OutputXmlTransactionNo}," + $"{item.OutputPdfTransactionNo}," + $"{item.OutputMailTransactionNo}," + $"{item.DmsAttachmentFileName}," + $"{item.DmsAttachmentFilePath}," +
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

            return File(Encoding.UTF8.GetBytes(strBuilder.ToString()), "text/csv", "scg-etax-TransactionDescription.csv");

        }

        public async Task<JsonResult> ResetStatusIndexing(List<TransactionDescription> listData, string updateby)
        {
            UTILITY.AdminTool.ResetIndexing resetIndexing = new UTILITY.AdminTool.ResetIndexing();
            Response res = new Response();
            List<string> listbillno = new List<string>();
            try
            {
                if (listData.Count > 0)
                {
                    listbillno = listData.Select(x => x.BillingNumber).ToList();
                    var result = resetIndexing.ResetStatusIndexingByMutipleRecords(listbillno, updateby);
                    res.STATUS = result;
                    res.ERROR_MESSAGE = "Failed";
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException.ToString());
            }


            return Json(res);
        }
        public async Task<JsonResult> ResetStatusXMLZip(List<TransactionDescription> listData, string updateby)
        {
            UTILITY.AdminTool.ResetXMLZip resetIndexing = new UTILITY.AdminTool.ResetXMLZip();
            Response res = new Response();
            List<string> listbillno = new List<string>();
            try
            {
                if (listData.Count > 0)
                {
                    listbillno = listData.Select(x => x.BillingNumber).ToList();
                    var result = resetIndexing.ResetStatusXMLZipByMutipleRecords(listbillno, updateby);
                    res.STATUS = result;
                    res.ERROR_MESSAGE = "Failed";
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException.ToString());
            }


            return Json(res);
        }
        public async Task<JsonResult> ResetStatusPrintZip(List<TransactionDescription> listData, string updateby)
        {
            UTILITY.AdminTool.ResetPrintZip resetIndexing = new UTILITY.AdminTool.ResetPrintZip();
            Response res = new Response();
            List<string> listbillno = new List<string>();
            try
            {
                if (listData.Count > 0)
                {
                    listbillno = listData.Select(x => x.BillingNumber).ToList();
                    var result = resetIndexing.ResetStatusPrintZipByMutipleRecords(listbillno, updateby);
                    res.STATUS = result;
                    res.ERROR_MESSAGE = "Failed";
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException.ToString());
            }


            return Json(res);
        }
        public async Task<JsonResult> SyncStatusPDFSign(List<TransactionDescription> listData, string updateby)
        {
            Response res = new Response();
            string listbill = "";
            try
            {
                if (listData.Count > 0)
                {
                    foreach (var item in listData)
                    {
                        listbill = listbill + "|" + item.BillingNumber;
                    }

                    var task = await Task.Run(() => ApiHelper.GetURI("api/TransactionDescription/SyncStatusPDFSign?listbillno=" + listbill + "&updateby=" + updateby));

                    if (task.STATUS)
                    {
                        res.STATUS = true;
                        //tran = JsonConvert.DeserializeObject<List<TransactionDescription>>(task.OUTPUT_DATA.ToString());

                    }
                    else
                    {
                        res.ERROR_MESSAGE = "Failed";
                        if (!string.IsNullOrEmpty(task.MESSAGE))
                        {
                            res.ERROR_MESSAGE = task.MESSAGE;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException.ToString());
            }


            return Json(res);
        }
        public async Task<JsonResult> ResendEmail(List<TransactionDescription> listData, string updateby)
        {
            UTILITY.AdminTool.UpdateXMLSign resetIndexing = new UTILITY.AdminTool.UpdateXMLSign();
            Response res = new Response();
            List<string> listbillno = new List<string>();
            try
            {
                if (listData.Count > 0)
                {
                    foreach (var item in listData)
                    {
                        res = await Task.Run(() => ApiHelper.GetURI("api/SendEmail/SendEmail?billno=" + item.BillingNumber + "&updateby=" + updateby));

                    }

                    //res.STATUS = true;

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException.ToString());
            }


            return Json(res);
        }
        public async Task<JsonResult> SyncStatusXMLSign(List<TransactionDescription> listData, string updateby)
        {
            Response res = new Response();
            string listbill = "";
            try
            {
                if (listData.Count > 0)
                {
                    foreach (var item in listData)
                    {
                        listbill = listbill + "|" + item.BillingNumber;
                    }

                    var task = await Task.Run(() => ApiHelper.GetURI("api/TransactionDescription/SyncStatusXMLSign?listbillno=" + listbill + "&updateby=" + updateby));

                    if (task.STATUS)
                    {
                        res.STATUS = true;
                        //tran = JsonConvert.DeserializeObject<List<TransactionDescription>>(task.OUTPUT_DATA.ToString());

                    }
                    else
                    {
                        res.ERROR_MESSAGE = "Failed";
                        if (!string.IsNullOrEmpty(task.MESSAGE))
                        {
                            res.ERROR_MESSAGE = task.MESSAGE;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException.ToString());
            }


            return Json(res);
        }
        public async Task<JsonResult> EditPostingYear(List<string> listData, string updateby, string year)
        {
            Response res = new Response();
            try
            {
                string listbill = "";
                if (listData.Count > 0)
                {
                    foreach (var item in listData)
                    {
                        listbill = listbill + "|" + item;
                    }
                    var task = await Task.Run(() => ApiHelper.GetURI("api/TransactionDescription/UpdatePostingYear?listbillno=" + listbill + "&updateby=" + updateby + "&postingYear=" + year));

                    if (task.STATUS)
                    {
                        res.STATUS = true;
                    }
                    else
                    {
                        res.ERROR_MESSAGE = "Failed";
                        if (!string.IsNullOrEmpty(task.MESSAGE))
                        {
                            res.ERROR_MESSAGE = task.MESSAGE;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }


            return Json(res);
        }
        public async Task<JsonResult> DownloadFile(string TransactionNo, string Type)
        {
            string pathFile = "";

            List<TransactionDescription> tran = new List<TransactionDescription>();

            Response resp = new Response();

            var result = "";
            var task = await Task.Run(() => ApiHelper.GetURI("api/TransactionDescription/GetDetail?id= " + TransactionNo + " "));

            if (task.STATUS)
            {

                tran = JsonConvert.DeserializeObject<List<TransactionDescription>>(task.OUTPUT_DATA.ToString());
                if (tran.Count > 0)
                {
                    if (Type.Equals("PDF", StringComparison.CurrentCultureIgnoreCase))
                    {
                        pathFile = tran[0].PdfSignLocation;
                    }
                    else if(Type.Equals("XML", StringComparison.CurrentCultureIgnoreCase))
                    {
                        pathFile = tran[0].XmlSignLocation;
                    }
                    else
                    {
                        pathFile = tran[0].XmlBeforeSignLocation;
                    }

                    task = await Task.Run(() => ApiHelper.GetURI("api/TransactionDescription/DownloadFile?pathfile=" + pathFile + ""));
                }
                else
                {
                    task.STATUS = false;
                }

            }
            else
            {
                task.STATUS = false;
                ViewBag.Error = task.MESSAGE;
            }

            return Json(task);
        }

        public async Task<JsonResult> ExportFile(string jsonSearchString)
        {
            Response task = new Response();

            outputSearchXmlModel obj = new outputSearchXmlModel();
            List<OutputSearchXmlZip> tran = new List<OutputSearchXmlZip>();

            var strBuilder = new StringBuilder();

            try
            {

                var request = JsonConvert.DeserializeObject<transactionSearchModel>(jsonSearchString);
                request.user = HttpContext.Session.GetString("userMail");
                //var task = await Task.Run(() => ApiHelper.GetURI("api/TransactionDescription/Search?JsonString= " + transactionSearchJson + " "));
                var httpContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
                task = await Task.Run(() => ApiHelper.PostURI("api/TransactionDescription/ExportData", httpContent));
                //task = await Task.Run(() => ApiHelper.GetURI("api/TransactionDescription/ExportData?JsonString= " + jsonSearchString + " "));

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException.ToString());
            }

            return Json(task);
        }

    }
}
