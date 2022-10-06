using Microsoft.AspNetCore.Mvc;

namespace SCG.CAD.ETAX.WEB.Controllers
{
    public class ProfileCompanyCodeController : Controller
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

        public async Task<JsonResult> Detail(int id)
        {
            List<ProfileCompanyCode> tran = new List<ProfileCompanyCode>();

            var task = await Task.Run(() => ApiHelper.GetURI("api/ProfileCompanyCode/GetDetail?id= " + id + " "));

            Response resp = new Response();

            var result = "";

            if (task.STATUS)
            {

                tran = JsonConvert.DeserializeObject<List<ProfileCompanyCode>>(task.OUTPUT_DATA.ToString());

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

            List<ProfileCompanyCode> tran = new List<ProfileCompanyCode>();

            try
            {
                var task = await Task.Run(() => ApiHelper.GetURI("api/ProfileCompanyCode/GetListAll"));

                if (task.STATUS)
                {
                    tran = JsonConvert.DeserializeObject<List<ProfileCompanyCode>>(task.OUTPUT_DATA.ToString());
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

            //ProfileCompanyCode tran = new ProfileCompanyCode();

            //tran = JsonConvert.DeserializeObject<ProfileCompanyCode>(jsonString.ToString());

            //string json = JsonConvert.SerializeObject(tran, Formatting.Indented);

            var httpContent = new StringContent(jsonString, Encoding.UTF8, "application/json");

            var task = await Task.Run(() => ApiHelper.PostURI("api/ProfileCompanyCode/Insert", httpContent));

            return Json(task);
        }

        public async Task<JsonResult> Update(string jsonString)
        {
            Response res = new Response();

            //string json = JsonConvert.SerializeObject(jsonString, Formatting.Indented);

            var httpContent = new StringContent(jsonString, Encoding.UTF8, "application/json");

            var task = await Task.Run(() => ApiHelper.PostURI("api/ProfileCompanyCode/Update", httpContent));

            return Json(task);
        }

        public async Task<JsonResult> Delete(string jsonString)
        {
            Response res = new Response();

            //string json = JsonConvert.SerializeObject(jsonString, Formatting.Indented);

            var httpContent = new StringContent(jsonString, Encoding.UTF8, "application/json");

            var task = await Task.Run(() => ApiHelper.PostURI("api/ProfileCompanyCode/Delete", httpContent));

            return Json(task);
        }

        public async Task<ActionResult> ExportToCsv()
        {
            Response resp = new Response();

            List<ProfileCompanyCode> tran = new List<ProfileCompanyCode>();

            var strBuilder = new StringBuilder();

            try
            {
                var task = await Task.Run(() => ApiHelper.GetURI("api/ProfileCompanyCode/GetListAll"));

                if (task.STATUS)
                {
                    tran = JsonConvert.DeserializeObject<List<ProfileCompanyCode>>(task.OUTPUT_DATA.ToString());

                    if (tran.Count() > 0)
                    {
                        strBuilder.AppendLine("" +
                            "CompanyCodeNo," +
                            "CompanyCode," +
                            "CompanyCodeDescription," +
                            "CreateBy," +
                            "CreateDate," +
                            "UpdateBy," +
                            "UpdateDate," +
                            "Isactive");



                        foreach (var item in tran)
                        {
                            strBuilder.AppendLine($"" +
                                $"{item.CompanyCodeNo}," +
                                $"{item.CompanyCode}," +
                                $"{item.CompanyCodeDescription}," +
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

            return File(Encoding.UTF8.GetBytes(strBuilder.ToString()), "text/csv", "scg-etax-ProfileCompanyCode.csv");

        }


    }
}
