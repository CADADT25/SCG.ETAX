using Microsoft.AspNetCore.Mvc;

namespace SCG.CAD.ETAX.WEB.Controllers
{
    [SessionExpire]
    public class ProfilePartnerController : Controller
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
            List<ProfilePartner> tran = new List<ProfilePartner>();

            Response resp = new Response();

            var result = "";

            try
            {
                var task = await Task.Run(() => ApiHelper.GetURI("api/ProfilePartner/GetDetail?id= " + id + " "));

                if (task.STATUS)
                {

                    tran = JsonConvert.DeserializeObject<List<ProfilePartner>>(task.OUTPUT_DATA.ToString());

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

            List<ProfilePartner> tran = new List<ProfilePartner>();

            try
            {
                var task = await Task.Run(() => ApiHelper.GetURI("api/ProfilePartner/GetListAll"));

                if (task.STATUS)
                {
                    var comcode = JsonConvert.DeserializeObject<List<string>>(HttpContext.Session.GetString("premissionComCode"));

                    tran = JsonConvert.DeserializeObject<List<ProfilePartner>>(task.OUTPUT_DATA.ToString());
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
            Response res = new Response();

            var httpContent = new StringContent(jsonString, Encoding.UTF8, "application/json");

            var task = await Task.Run(() => ApiHelper.PostURI("api/ProfilePartner/Insert", httpContent));

            return Json(task);
        }

        public async Task<JsonResult> Update(string jsonString)
        {
            Response res = new Response();

            var httpContent = new StringContent(jsonString, Encoding.UTF8, "application/json");

            var task = await Task.Run(() => ApiHelper.PostURI("api/ProfilePartner/Update", httpContent));

            return Json(task);
        }

        public async Task<JsonResult> Delete(string jsonString)
        {
            Response res = new Response();

            var httpContent = new StringContent(jsonString, Encoding.UTF8, "application/json");

            var task = await Task.Run(() => ApiHelper.PostURI("api/ProfilePartner/Delete", httpContent));

            return Json(task);
        }

        public async Task<ActionResult> ExportToCsv()
        {
            Response resp = new Response();

            List<ProfilePartner> tran = new List<ProfilePartner>();

            var strBuilder = new StringBuilder();

            try
            {
                var task = await Task.Run(() => ApiHelper.GetURI("api/ProfilePartner/GetListAll"));

                if (task.STATUS)
                {
                    tran = JsonConvert.DeserializeObject<List<ProfilePartner>>(task.OUTPUT_DATA.ToString());

                    if (tran.Count() > 0)
                    {
                        strBuilder.AppendLine("" +
                            "PartnerProfileNo," +
                            "CustomerId," +
                            "CompanyCode," +
                            "SellOrg," +
                            "PartnerOutputType," +
                            "NumberOfCopies," +
                            "SoldToCode," +
                            "SoldToEmail," +
                            "SoldToCcemail," +
                            "ShipToCode," +
                            "ShipToEmail," +
                            "ShipToCcemail," +
                            "PartnerEmailType," +
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
                            string SoldToEmail = item.SoldToEmail != null ? item.SoldToEmail.Replace(",", "|") : "";
                            string SoldToCcemail = item.SoldToCcemail != null ? item.SoldToCcemail.Replace(",", "|") : "";
                            string ShipToEmail = item.ShipToEmail != null ? item.ShipToEmail.Replace(",", "|") : "";
                            string ShipToCcemail = item.ShipToCcemail != null ? item.ShipToCcemail.Replace(",", "|") : "";
                            strBuilder.AppendLine($"" +
                                $"{item.PartnerProfileNo}," +
                                $"{item.CustomerId}," +
                                $"{item.CompanyCode}," +
                                $"{item.SellOrg}," +
                                $"{item.PartnerOutputType}," +
                                $"{item.NumberOfCopies}," +
                                $"{item.SoldToCode}," +
                                $"{SoldToEmail}," +
                                $"{SoldToCcemail}," +
                                $"{item.ShipToCode}," +
                                $"{ShipToEmail}," +
                                $"{ShipToCcemail}," +
                                $"{item.PartnerEmailType}," +
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

            return File(Encoding.UTF8.GetBytes(strBuilder.ToString()), "text/csv", "scg-etax-ProfilePartner.csv");

        }

        public async Task<JsonResult> Import(string jsonString)
        {
            // replace | to ,
            Response resp = new Response();
            try
            {
                var request = JsonConvert.DeserializeObject<List<ProfilePartner>>(jsonString);
                var data = new List<ProfilePartner>();
                foreach (var item in request)
                {
                    item.UpdateBy = HttpContext.Session.GetString("userMail");
                    item.SoldToEmail = item.SoldToEmail != null ? item.SoldToEmail.Replace("|", ",") : "";
                    item.SoldToCcemail = item.SoldToCcemail != null ? item.SoldToCcemail.Replace("|", ",") : "";
                    item.ShipToEmail = item.ShipToEmail != null ? item.ShipToEmail.Replace("|", ",") : "";
                    item.ShipToCcemail = item.ShipToCcemail != null ? item.ShipToCcemail.Replace("|", ",") : "";
                    data.Add(item);
                }
                string jsonData = JsonConvert.SerializeObject(data);
                var httpContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
                resp = await Task.Run(() => ApiHelper.PostURI("api/ProfilePartner/Import", httpContent));
            }
            catch (Exception ex)
            {
                resp.STATUS = false;
                resp.ERROR_MESSAGE = ex.Message.ToString();
            }
            return Json(resp);
        }


    }
}
