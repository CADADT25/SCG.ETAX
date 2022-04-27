using Microsoft.AspNetCore.Mvc;

namespace SCG.CAD.ETAX.WEB.Controllers
{
    public class ConfigXmlGeneratorController : Controller
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
            List<ConfigXmlGenerator> tran = new List<ConfigXmlGenerator>();

            Response resp = new Response();

            var result = "";

            try
            {
                var task = await Task.Run(() => ApiHelper.GetURI("api/ConfigXmlGenerator/GetDetail?id= " + id + " "));

                if (task.STATUS)
                {

                    tran = JsonConvert.DeserializeObject<List<ConfigXmlGenerator>>(task.OUTPUT_DATA.ToString());

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

        public async Task<JsonResult> List(string companyCode)
        {
            Response resp = new Response();

            List<ConfigXmlGenerator> tran = new List<ConfigXmlGenerator>();

            try
            {
                var task = await Task.Run(() => ApiHelper.GetURI("api/ConfigXmlGenerator/GetListAll"));

                if (task.STATUS)
                {
                    tran = JsonConvert.DeserializeObject<List<ConfigXmlGenerator>>(task.OUTPUT_DATA.ToString());

                    tran = tran.Where(x => x.ConfigXmlGeneratorCompanyCode == companyCode).ToList();

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

            var task = await Task.Run(() => ApiHelper.PostURI("api/ConfigXmlGenerator/Insert", httpContent));

            return Json(task);
        }

        public async Task<JsonResult> Update(string jsonString)
        {
            Response res = new Response();

            var httpContent = new StringContent(jsonString, Encoding.UTF8, "application/json");

            var task = await Task.Run(() => ApiHelper.PostURI("api/ConfigXmlGenerator/Update", httpContent));

            return Json(task);
        }

        public async Task<JsonResult> Delete(string jsonString)
        {
            Response res = new Response();

            var httpContent = new StringContent(jsonString, Encoding.UTF8, "application/json");

            var task = await Task.Run(() => ApiHelper.PostURI("api/ConfigXmlGenerator/Delete", httpContent));

            return Json(task);
        }

        public async Task<ActionResult> ExportToCsv()
        {
            Response resp = new Response();

            List<ConfigXmlGenerator> tran = new List<ConfigXmlGenerator>();

            var strBuilder = new StringBuilder();

            try
            {
                var task = await Task.Run(() => ApiHelper.GetURI("api/ConfigXmlGenerator/GetListAll"));

                if (task.STATUS)
                {
                    tran = JsonConvert.DeserializeObject<List<ConfigXmlGenerator>>(task.OUTPUT_DATA.ToString());

                    if (tran.Count() > 0)
                    {
                        strBuilder.AppendLine("" +
                            "ConfigXmlGeneratorNo," +
                            "ConfigXmlGeneratorInputSource," +
                            "ConfigXmlGeneratorInputType," +
                            "ConfigXmlGeneratorInputPath," +
                            "ConfigXmlGeneratorOutputSource," +
                            "ConfigXmlGeneratorOutputType," +
                            "ConfigXmlGeneratorOutputPath," +
                            "CreateBy," +
                            "CreateDate," +
                            "UpdateBy," +
                            "UpdateDate," +
                            "Isactive");



                        foreach (var item in tran)
                        {
                            strBuilder.AppendLine($"" +
                                $"{item.ConfigXmlGeneratorNo}," +
                                $"{item.ConfigXmlGeneratorInputSource}," +
                                $"{item.ConfigXmlGeneratorInputType}," +
                                $"{item.ConfigXmlGeneratorInputPath}," +
                                $"{item.ConfigXmlGeneratorOutputSource}," +
                                $"{item.ConfigXmlGeneratorOutputType}," +
                                $"{item.ConfigXmlGeneratorOutputPath}," +
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

            return File(Encoding.UTF8.GetBytes(strBuilder.ToString()), "text/csv", "scg-etax-ConfigXmlGenerator.csv");

        }


    }
}
