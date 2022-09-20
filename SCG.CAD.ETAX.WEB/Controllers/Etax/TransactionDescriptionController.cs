using Microsoft.AspNetCore.Mvc;

namespace SCG.CAD.ETAX.WEB.Controllers
{
    public class TransactionDescriptionController : Controller
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
                var task = await Task.Run(() => ApiHelper.GetURI("api/TransactionDescription/GetListAll"));

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


        public async Task<JsonResult> Search(string transactionSearchJson)
        {

            List<TransactionDescription> tran = new List<TransactionDescription>();

            Response resp = new Response();

            var result = "";

            try
            {
                var task = await Task.Run(() => ApiHelper.GetURI("api/TransactionDescription/Search?JsonString= " + transactionSearchJson + " "));

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

        public async Task<JsonResult> ResetStatusIndexing(List<TransactionDescription> listData,string updateby)
        {
            UTILITY.AdminTool.ResetIndexing resetIndexing = new UTILITY.AdminTool.ResetIndexing();
            Response res = new Response();
            List<string> listbillno = new List<string>();
            try
            {
                if(listData.Count > 0)
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

    }
}
