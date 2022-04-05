namespace SCG.CAD.ETAX.WEB.Controllers
{
    public class TaxCodeController : Controller
    {
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
            List<TaxCode> tran = new List<TaxCode>();

            var task = await Task.Run(() => ApiHelper.GetURI("api/TaxCode/GetDetail?id= " + id + " "));

            Response resp = new Response();

            var result = "";

            if (task.STATUS)
            {

                tran = JsonConvert.DeserializeObject<List<TaxCode>>(task.OUTPUT_DATA.ToString());

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

            List<TaxCode> tran = new List<TaxCode>();

            try
            {
                var task = await Task.Run(() => ApiHelper.GetURI("api/TaxCode/GetListAll"));

                if (task.STATUS)
                {
                    tran = JsonConvert.DeserializeObject<List<TaxCode>>(task.OUTPUT_DATA.ToString());
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

            var task = await Task.Run(() => ApiHelper.PostURI("api/TaxCode/Insert", httpContent));

            return Json(task);
        }

        public async Task<JsonResult> Update(string jsonString)
        {
            Response res = new Response();

            var httpContent = new StringContent(jsonString, Encoding.UTF8, "application/json");

            var task = await Task.Run(() => ApiHelper.PostURI("api/TaxCode/Update", httpContent));

            return Json(task);
        }

        public async Task<JsonResult> Delete(string jsonString)
        {
            Response res = new Response();

            var httpContent = new StringContent(jsonString, Encoding.UTF8, "application/json");

            var task = await Task.Run(() => ApiHelper.PostURI("api/TaxCode/Delete", httpContent));

            return Json(task);
        }

        public async Task<ActionResult> ExportToCsv()
        {
            Response resp = new Response();

            List<TaxCode> tran = new List<TaxCode>();

            var strBuilder = new StringBuilder();

            try
            {
                var task = await Task.Run(() => ApiHelper.GetURI("api/TaxCode/GetListAll"));

                if (task.STATUS)
                {
                    tran = JsonConvert.DeserializeObject<List<TaxCode>>(task.OUTPUT_DATA.ToString());

                    if (tran.Count() > 0)
                    {
                        strBuilder.AppendLine("" +
                            "TaxCodeNo," +
                            "TaxCodeErp," +
                            "TaxCodeRd," +
                            "TaxCodeDescription," +
                            "CreateBy," +
                            "CreateDate," +
                            "UpdateBy," +
                            "UpdateDate," +
                            "Isactive");



                        foreach (var item in tran)
                        {
                            strBuilder.AppendLine($"" +
                                $"{item.TaxCodeNo}," +
                                $"{item.TaxCodeErp}," +
                                $"{item.TaxCodeRd}," +
                                $"{item.TaxCodeDescription}," +
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

            return File(Encoding.UTF8.GetBytes(strBuilder.ToString()), "text/csv", "scg-etax-ProductUnit.csv");

        }


    }
}
