using DocumentFormat.OpenXml.Drawing.Charts;
using DocumentFormat.OpenXml.EMMA;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.AspNetCore.Mvc;
using SCG.CAD.ETAX.MODEL.etaxModel;
using SCG.CAD.ETAX.UTILITY.Authentication;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace SCG.CAD.ETAX.WEB.Controllers
{
    public class RequestCartController : Controller
    {
        [SessionExpire]
        public IActionResult Index()
        {
            //ViewData["cartCount"] = 0;
            //ViewData["cartList"] = new List<RequestCart>();
            var models = new List<RequestCart>();
            try
            {
                string email = HttpContext.Session.GetString("userMail") ?? "";
                if (!string.IsNullOrEmpty(email))
                {
                    var req = new RequestCartSearchModel() { CreateBy = email };

                    var httpContent = new StringContent(JsonConvert.SerializeObject(req), Encoding.UTF8, "application/json");

                    var res = Task.Run(() => ApiHelper.PostURI("api/RequestCart/Search", httpContent)).Result;
                    if (res.STATUS)
                    {
                        models = JsonConvert.DeserializeObject<List<RequestCart>>(res.OUTPUT_DATA.ToString());
                        models = models.OrderBy(t => t.BillingNumber).ToList();
                        //ViewData["cartCount"] = output != null ? output.Count() : 0;
                        //ViewData["cartList"] = output;
                    }
                }
            }
            catch
            {

            }

            return View(models);
        }
        public IActionResult _Modal()
        {
            return View();
        }

        [SessionExpire]
        public IActionResult ManageCart()
        {
            var models = new ManageRequestCartModel();
            try
            {
                //string email = HttpContext.Session.GetString("userMail") ?? "";
                //if (!string.IsNullOrEmpty(email))
                //{
                //    var req = new RequestCartSearchModel() { CreateBy = email };

                //    var httpContent = new StringContent(JsonConvert.SerializeObject(req), Encoding.UTF8, "application/json");

                //    var res = Task.Run(() => ApiHelper.PostURI("api/RequestCart/Search", httpContent)).Result;
                //    if (res.STATUS)
                //    {
                //        models.RequestCarts = JsonConvert.DeserializeObject<List<RequestCart>>(res.OUTPUT_DATA.ToString()) ?? new List<RequestCart>();
                //        models.RequestCarts = models.RequestCarts.OrderBy(t => t.BillingNumber).ToList();
                //    }
                //}
            }
            catch
            {

            }

            return View(models);
        }

        public JsonResult GetPathXmlPdf()
        {
            var data = new PathXmlPdfModel();
            var configTask = Task.Run(() => ApiHelper.GetURI("api/ConfigGlobal/GetDetailByName?cate=REQEUST&name=RESIGN_NEWTRANS_ROOT_PATH")).Result;
            var config = new ConfigGlobal();
            if (configTask.STATUS)
            {
                config = JsonConvert.DeserializeObject<ConfigGlobal>(configTask.OUTPUT_DATA.ToString());
            }
            else
            {
                return Json(data);
            }
            //var path = @"C:\Work space\Document\Etax\Test\";
            var path = config.ConfigGlobalValue;
            path = Path.Combine(path);
            var filePaths = UtilityHelper.GetFileDirectories(path);

            if (filePaths != null)
            {
                filePaths.ForEach(t =>
                {
                    string fExtension = t.Split('.').Last();
                    //if (fExtension.ToLower() == "pdf")
                    //{
                    //    data.Pdfs.Add(t.Replace(path, ""));
                    //}
                    //else if (fExtension.ToLower() == "xml")
                    //{
                    //    data.Xmls.Add(t.Replace(path, ""));
                    //}
                });
            }

            return Json(data);
        }
        public async Task<JsonResult> List(string searchJson)
        {
            string email = HttpContext.Session.GetString("userMail") ?? "";

            Response resp = new Response();

            List<RequestCartDataModel> data = new List<RequestCartDataModel>();

            try
            {
                var req = new RequestCartSearchModel() { CreateBy = email };

                var httpContent = new StringContent(JsonConvert.SerializeObject(req), Encoding.UTF8, "application/json");
                var res = await Task.Run(() => ApiHelper.PostURI("api/RequestCart/SearchFull", httpContent));

                if (res.STATUS)
                {
                    data = JsonConvert.DeserializeObject<List<RequestCartDataModel>>(res.OUTPUT_DATA.ToString());
                }
                else
                {
                    ViewBag.Error = res.MESSAGE;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException);
            }


            return Json(new { data = data });
        }

        public async Task<JsonResult> AddToCart(string jsonString)
        {
            Response res = new Response();

            var httpContent = new StringContent(jsonString, Encoding.UTF8, "application/json");

            var task = await Task.Run(() => ApiHelper.PostURI("api/RequestCart/InsertList", httpContent));

            return Json(task);
        }
        public async Task<JsonResult> DeleteCart(string jsonString)
        {
            Response res = new Response();

            var httpContent = new StringContent(jsonString, Encoding.UTF8, "application/json");

            var task = await Task.Run(() => ApiHelper.PostURI("api/RequestCart/Delete", httpContent));

            return Json(task);
        }
        public async Task<JsonResult> MultiDeleteCart(string jsonString)
        {
            Response res = new Response();

            var httpContent = new StringContent(jsonString, Encoding.UTF8, "application/json");

            var task = await Task.Run(() => ApiHelper.PostURI("api/RequestCart/MultiDelete", httpContent));

            return Json(task);
        }

        public async Task<JsonResult> SubmitRequest(string action, string manager)
        {
            Response res = new Response();
            try
            {
                var req = new RequestCartSearchModel() { CreateBy = HttpContext.Session.GetString("userMail") };

                var httpContentRequestCart = new StringContent(JsonConvert.SerializeObject(req), Encoding.UTF8, "application/json");
                var resRequestCart = Task.Run(() => ApiHelper.PostURI("api/RequestCart/SearchFull", httpContentRequestCart)).Result;
                var dataRequestCart = new List<RequestCartDataModel>();
                if (resRequestCart.STATUS)
                {
                    dataRequestCart = JsonConvert.DeserializeObject<List<RequestCartDataModel>>(resRequestCart.OUTPUT_DATA.ToString());
                }
                string errorMsg = ValidateBeforSubmitRequest(action, manager, dataRequestCart);
                if (string.IsNullOrEmpty(errorMsg))
                {
                    res.STATUS = true;
                    // Submit request here
                    var submitRequest = new RequestDataModel();
                    submitRequest.RequestCartList = dataRequestCart;
                    submitRequest.Action = action;
                    submitRequest.Manager = manager;
                    submitRequest.UserBy = HttpContext.Session.GetString("userMail");
                    var httpContentSubmitRequest = new StringContent(JsonConvert.SerializeObject(submitRequest), Encoding.UTF8, "application/json");
                    res = await Task.Run(() => ApiHelper.PostURI("api/Request/SubmitRequest", httpContentSubmitRequest));
                    if (res.STATUS)
                    {
                        if (res.OUTPUT_DATA != null)
                        {
                            var reqNos = JsonConvert.DeserializeObject<List<string>>(res.OUTPUT_DATA.ToString());
                            // Send mail to Manager
                            foreach (var item in reqNos)
                            {
                                Task.Run(() => ApiHelper.GetURI("api/SendEmail/SendEmailRequestByAction?requestNo=" + item + "&action=submit"));

                            }
                        }

                    }

                }
                else
                {
                    res.STATUS = false;
                    res.MESSAGE = errorMsg;
                }

            }
            catch (Exception ex)
            {
                res.STATUS = false;
                res.MESSAGE = ex.Message;
            }


            return Json(res);
        }
        public async Task<JsonResult> SubmitRequestNewTrans(string action, string manager, string jsonString)
        {
            Response res = new Response();
            try
            {
                var dataPath = JsonConvert.DeserializeObject<List<PathXmlPdfModel>>(jsonString);

                string errorMsg = ValidateBeforSubmitRequestNewTrans(action, manager, dataPath);
                if (string.IsNullOrEmpty(errorMsg))
                {
                    res.STATUS = true;
                    // Submit request here
                    var submitRequest = new RequestDataModel();
                    submitRequest.PathList = dataPath;
                    submitRequest.Action = action;
                    submitRequest.Manager = manager;
                    submitRequest.UserBy = HttpContext.Session.GetString("userMail");
                    var httpContentSubmitRequest = new StringContent(JsonConvert.SerializeObject(submitRequest), Encoding.UTF8, "application/json");
                    res = await Task.Run(() => ApiHelper.PostURI("api/Request/SubmitRequestNewTrans", httpContentSubmitRequest));
                    if (res.STATUS)
                    {
                        if (res.OUTPUT_DATA != null)
                        {
                            var reqNos = JsonConvert.DeserializeObject<List<string>>(res.OUTPUT_DATA.ToString());
                            // Send mail to Manager
                            foreach (var item in reqNos)
                            {
                                Task.Run(() => ApiHelper.GetURI("api/SendEmail/SendEmailRequestByAction?requestNo=" + item + "&action=submit"));

                            }
                        }

                    }

                }
                else
                {
                    res.STATUS = false;
                    res.MESSAGE = errorMsg;
                }

            }
            catch (Exception ex)
            {
                res.STATUS = false;
                res.MESSAGE = ex.Message;
            }


            return Json(res);
        }
        private string ValidateBeforSubmitRequest(string action, string manager, List<RequestCartDataModel> dataRequestCart)
        {
            string errorMsg = "";
            if (string.IsNullOrEmpty(action))
            {
                errorMsg = UtilityHelper.SetError(errorMsg, "Please select action.");
            }
            if (string.IsNullOrEmpty(manager))
            {
                errorMsg = UtilityHelper.SetError(errorMsg, "Please select manager.");
            }
            if (action != Variable.RequestActionCode_Delete && action != Variable.RequestActionCode_Undelete && action != Variable.RequestActionCode_ReSignNewTrans && action != Variable.RequestActionCode_ReSignNewCert)
            {
                errorMsg = UtilityHelper.SetError(errorMsg, "Unknown action.");
            }

            if (action == Variable.RequestActionCode_ReSignNewTrans)
            {

            }
            else
            {
                if (dataRequestCart.Count() == 0)
                {
                    errorMsg = UtilityHelper.SetError(errorMsg, "Data not found in your cart.");
                }

                var requestStatus = new List<string>() { Variable.RequestStatusCode_WaitManager, Variable.RequestStatusCode_WaitOfficer, Variable.RequestStatusCode_WaitAdminCheck };
                var httpContentRequestItem = new StringContent(JsonConvert.SerializeObject(requestStatus), Encoding.UTF8, "application/json");
                var resRequestItem = Task.Run(() => ApiHelper.PostURI("api/RequestItem/GetListByStatus", httpContentRequestItem)).Result;
                var dataRequestItem = new List<RequestItem>();
                if (resRequestItem.STATUS)
                {
                    dataRequestItem = JsonConvert.DeserializeObject<List<RequestItem>>(resRequestItem.OUTPUT_DATA.ToString());
                }

                foreach (var item in dataRequestCart)
                {
                    if (dataRequestItem != null)
                    {
                        if (dataRequestItem.Count() > 0)
                        {
                            var obj = dataRequestItem.Where(t => t.TransactionNo == item.TransactionNo).FirstOrDefault();
                            if (obj != null)
                            {
                                errorMsg = UtilityHelper.SetError(errorMsg, "Billing No. " + obj.BillingNumber + " duplicates another request.");
                            }
                        }
                    }
                    // delete
                    if (action == Variable.RequestActionCode_Delete)
                    {
                        //if(item.XmlCompressStatus == "Successful")
                        //{
                        //    errorMsg = UtilityHelper.SetError(errorMsg, "Billing No. " + item.BillingNumber + " compressed file.");
                        //}
                        if (item.SentRevenueDepartment == 1)
                        {
                            errorMsg = UtilityHelper.SetError(errorMsg, "Billing No. " + item.BillingNumber + " sent to the Revenue Department.");
                        }
                        if (item.Isactive != 1)
                        {
                            errorMsg = UtilityHelper.SetError(errorMsg, "Billing No. " + item.BillingNumber + " has been deleted.");
                        }
                    }
                    // undelete
                    else if (action == Variable.RequestActionCode_Undelete)
                    {
                        if (item.Isactive == 1)
                        {
                            errorMsg = UtilityHelper.SetError(errorMsg, "Billing No. " + item.BillingNumber + " has been  undeleted.");
                        }
                    }
                    else if (action == Variable.RequestActionCode_ReSignNewTrans)
                    {

                    }
                    else if (action == Variable.RequestActionCode_ReSignNewCert)
                    {
                        if (item.SentRevenueDepartment == 1)
                        {
                            errorMsg = UtilityHelper.SetError(errorMsg, "Billing No. " + item.BillingNumber + " sent to the Revenue Department.");
                        }
                        //if(item.PdfSignStatus != "Successful" || item.XmlSignStatus != "Successful")
                        if(item.XmlSignStatus != "Successful")
                        {
                            //errorMsg = UtilityHelper.SetError(errorMsg, "Billing No. " + item.BillingNumber + " Xml or Pdf not signed.");
                            errorMsg = UtilityHelper.SetError(errorMsg, "Billing No. " + item.BillingNumber + " Xml still not signed.");
                        }
                    }
                    // unzip
                    //else if (action == "unzip")
                    //{
                    //    if (item.XmlCompressStatus != "Successful")
                    //    {
                    //        errorMsg = UtilityHelper.SetError(errorMsg, "Billing No. " + item.BillingNumber + " uncompressed file.");
                    //    }
                    //    if (item.Isactive != 1)
                    //    {
                    //        errorMsg = UtilityHelper.SetError(errorMsg, "Billing No. " + item.BillingNumber + " deleted.");
                    //    }
                    //    if (item.SentRevenueDepartment == 1)
                    //    {
                    //        errorMsg = UtilityHelper.SetError(errorMsg, "Billing No. " + item.BillingNumber + " sent to the Revenue Department.");
                    //    }
                    //}
                }
            }
            return errorMsg;
        }
        private string ValidateBeforSubmitRequestNewTrans(string action, string manager, List<PathXmlPdfModel> dataPath)
        {
            string errorMsg = "";
            if (string.IsNullOrEmpty(action))
            {
                errorMsg = UtilityHelper.SetError(errorMsg, "Please select action.");
            }
            if (string.IsNullOrEmpty(manager))
            {
                errorMsg = UtilityHelper.SetError(errorMsg, "Please select manager.");
            }
            if (action != Variable.RequestActionCode_Delete && action != Variable.RequestActionCode_Undelete && action != Variable.RequestActionCode_ReSignNewTrans && action != Variable.RequestActionCode_ReSignNewCert)
            {
                errorMsg = UtilityHelper.SetError(errorMsg, "Unknown action.");
            }

            if (dataPath.Count() == 0)
            {
                errorMsg = UtilityHelper.SetError(errorMsg, "Data path not found.");
            }

            if (action == Variable.RequestActionCode_ReSignNewTrans)
            {

            }

            return errorMsg;
        }

        [HttpPost]
        public async Task<JsonResult> UploadFile(IFormFile files)
        {
            Response res = new Response();
            try
            {
                if (files == null || files.Length == 0)
                {
                    res.STATUS = false;
                    res.MESSAGE = "File not found.";
                    return Json(res);
                }

                var data = new PathXmlPdfModel();
                var configTask = Task.Run(() => ApiHelper.GetURI("api/ConfigGlobal/GetDetailByName?cate=REQEUST&name=RESIGN_NEWTRANS_TEMP_PATH")).Result;
                var config = new ConfigGlobal();
                if (configTask.STATUS)
                {
                    config = JsonConvert.DeserializeObject<ConfigGlobal>(configTask.OUTPUT_DATA.ToString());
                }
                else
                {
                    return Json(data);
                }

                string rootPath = config.ConfigGlobalValue;//@"C:\Work space\Document\Etax\Test\Upload\Temp";

                string oldFileName = files.FileName;
                
                string newFileName = Guid.NewGuid().ToString() + "_" + files.FileName;

                string destinationPath = Path.Combine(rootPath, DateTime.Now.ToString("yyyy"), DateTime.Now.ToString("MM"), DateTime.Now.ToString("dd"), newFileName);

                //string path = Path.Combine(fullPath);
                string directoryPath = System.IO.Path.GetDirectoryName(destinationPath);

                if (!Directory.Exists(directoryPath))
                {
                    DirectoryInfo di = Directory.CreateDirectory(directoryPath);
                }

                using (var stream = new FileStream(destinationPath, FileMode.Create))
                {
                    await files.CopyToAsync(stream);
                    stream.Close();
                }
                res.STATUS = true;
                res.OUTPUT_DATA = new { newFileName = newFileName , oldFileName = oldFileName };
            }
            catch(Exception ex)
            {
                res.STATUS = false;
                res.MESSAGE = ex.Message;
            }
            return Json(res);
        }
    }
}
