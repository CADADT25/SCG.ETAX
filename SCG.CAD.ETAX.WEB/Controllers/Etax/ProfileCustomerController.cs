using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace SCG.CAD.ETAX.WEB.Controllers.Etax
{
    [SessionExpire]
    public class ProfileCustomerController : Controller
    {
        [SessionExpire]
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
            List<ProfileCustomer> tran = new List<ProfileCustomer>();

            Response resp = new Response();

            var result = "";

            try
            {
                var task = await Task.Run(() => ApiHelper.GetURI("api/ProfileCustomer/GetDetail?id= " + id + " "));

                if (task.STATUS)
                {

                    tran = JsonConvert.DeserializeObject<List<ProfileCustomer>>(task.OUTPUT_DATA.ToString());

                    result = JsonConvert.SerializeObject(tran[0]);

                }
                else
                {
                    ViewBag.Error = task.MESSAGE;
                }
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return Json(result);
        }

        public async Task<JsonResult> List()
        {
            Response resp = new Response();

            List<ProfileCustomer> tran = new List<ProfileCustomer>();

            try
            {
                var task = await Task.Run(() => ApiHelper.GetURI("api/ProfileCustomer/GetListAll"));

                if (task.STATUS)
                {
                    var comcode = JsonConvert.DeserializeObject<List<string>>(HttpContext.Session.GetString("premissionComCode"));

                    tran = JsonConvert.DeserializeObject<List<ProfileCustomer>>(task.OUTPUT_DATA.ToString());
                    tran = tran.Where(x => comcode.Contains(x.CompanyCode)).ToList();
                }
                else
                {
                    ViewBag.Error = task.MESSAGE;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }


            return Json(new { data = tran });
        }

        public async Task<JsonResult> Insert(string jsonString)
        {
            Response resp = new Response();

            var httpContent = new StringContent(jsonString, Encoding.UTF8, "application/json");

            var task = await Task.Run(() => ApiHelper.PostURI("api/ProfileCustomer/Insert", httpContent));

            resp = task;

            return Json(resp);
        }

        public async Task<JsonResult> Update(string jsonString)
        {
            Response res = new Response();

            var httpContent = new StringContent(jsonString, Encoding.UTF8, "application/json");

            var task = await Task.Run(() => ApiHelper.PostURI("api/ProfileCustomer/Update", httpContent));

            return Json(task);
        }

        public async Task<JsonResult> Delete(string jsonString)
        {
            Response res = new Response();

            var httpContent = new StringContent(jsonString, Encoding.UTF8, "application/json");

            var task = await Task.Run(() => ApiHelper.PostURI("api/ProfileCustomer/Delete", httpContent));

            return Json(task);
        }

        public async Task<ActionResult> ExportToCsv()
        {
            Response resp = new Response();

            List<ProfileCustomer> tran = new List<ProfileCustomer>();

            var strBuilder = new StringBuilder();

            try
            {
                var task = await Task.Run(() => ApiHelper.GetURI("api/ProfileCustomer/GetListAll"));

                if (task.STATUS)
                {
                    tran = JsonConvert.DeserializeObject<List<ProfileCustomer>>(task.OUTPUT_DATA.ToString());

                    if (tran.Count() > 0)
                    {
                        strBuilder.AppendLine("" +
                            "CustomerProfileNo," +
                            "CustomerId," +
                            "CompanyCode," +
                            "OutputType," +
                            "NumberOfCopies," +
                            "CustomerEmail," +
                            "CustomerCcemail," +
                            "EmailType," +
                            "EmailTemplateNo," +
                            "StatusPrint," +
                            "StatusEmail," +
                            "StatusSignPdf," +
                            "StatusSignXml," +
                            "Isactive," +
                            "CreateBy," +
                            "CreateDate," +
                            "UpdateBy," +
                            "UpdateDate");


                        foreach (var item in tran)
                        {
                            string customerEmail = item.CustomerEmail != null ? item.CustomerEmail.Replace(",", "|") : "";
                            string customerCcemail = item.CustomerCcemail != null ? item.CustomerCcemail.Replace(",", "|") : "";
                            strBuilder.AppendLine($"" +
                                $"{item.CustomerProfileNo}," +
                                $"{item.CustomerId}," +
                                $"{item.CompanyCode}," +
                                $"{item.OutputType}," +
                                $"{item.NumberOfCopies}," +
                                $"{customerEmail}," +
                                $"{customerCcemail}," +
                                $"{item.EmailType}," +
                                $"{item.EmailTemplateNo}," +
                                $"{item.StatusPrint}," +
                                $"{item.StatusEmail}," +
                                $"{item.StatusSignPdf}," +
                                $"{item.StatusSignXml}," +
                                $"{item.Isactive}," +
                                $"{item.CreateBy}," +
                                $"{item.CreateDate}," +
                                $"{item.UpdateBy}," +
                                $"{item.UpdateDate}");
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
                Console.WriteLine(ex.Message.ToString());
            }

            return File(Encoding.UTF8.GetBytes(strBuilder.ToString()), "text/csv", "scg-etax-ProfileCustomer.csv");

        }

        //public async Task<JsonResult> Import(List<ProfileCustomer> request)
        public async Task<JsonResult> Import(string jsonString)
        {
            // replace | to ,
            Response resp = new Response();
            try
            {
                var request = JsonConvert.DeserializeObject<List<ProfileCustomer>>(jsonString);
                var data = new List<ProfileCustomer>();
                foreach (var item in request)
                {
                    item.UpdateBy = HttpContext.Session.GetString("userMail");
                    item.CustomerEmail = item.CustomerEmail != null ? item.CustomerEmail.Replace("|", ",") : "";
                    item.CustomerCcemail = item.CustomerCcemail != null ? item.CustomerCcemail.Replace("|", ",") : "";
                    data.Add(item);
                }
                string jsonData = JsonConvert.SerializeObject(data);
                var httpContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
                resp = await Task.Run(() => ApiHelper.PostURI("api/ProfileCustomer/Import", httpContent));
            }
            catch(Exception ex)
            {
                resp.STATUS = false;
                resp.ERROR_MESSAGE = ex.Message.ToString();
            }
            return Json(resp);
        }
        public async Task<JsonResult> DropDownList()
        {
            Response resp = new Response();
            List<string> comcode = new List<string>();
            //List<ProfileCustomer> tran = new List<ProfileCustomer>();

            try
            {
                //var task = await Task.Run(() => ApiHelper.GetURI("api/ProfileCustomer/GetListAll"));

                //if (task.STATUS)
                //{
                //    tran = JsonConvert.DeserializeObject<List<ProfileCustomer>>(task.OUTPUT_DATA.ToString());

                //    if (tran.Count > 0)
                //    {
                //        tran = tran.Where(x => x.Isactive == 1 && x.CompanyCode == companyCode).ToList();
                //    }
                //}

                //else
                //{
                //    ViewBag.Error = task.MESSAGE;
                //}

                comcode = JsonConvert.DeserializeObject<List<string>>(HttpContext.Session.GetString("premissionComCode"));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }


            return Json(comcode);
        }

        public async Task<JsonResult> GetEmailType()
        {
            Response resp = new Response();

            List<ProfileEmailType> tran = new List<ProfileEmailType>();

            var result = "";

            try
            {
                var task = await Task.Run(() => ApiHelper.GetURI("api/ProfileEmailType/GetListAll"));

                if (task.STATUS)
                {
                    tran = JsonConvert.DeserializeObject<List<ProfileEmailType>>(task.OUTPUT_DATA.ToString());

                    if (tran.Count > 0)
                    {
                        tran = tran.Where(x => x.Isactive == 1).ToList();
                    }
                }

                else
                {
                    ViewBag.Error = task.MESSAGE;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }


            return Json(tran);
        }


    }
}
