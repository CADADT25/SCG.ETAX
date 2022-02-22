using Microsoft.AspNetCore.Mvc;

namespace SCG.CAD.ETAX.WEB.Controllers
{
    public class ProfileEmailTypeController : Controller
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



        public async Task<JsonResult> Detail(int id)
        {
            List<ProfileEmailType> tran = new List<ProfileEmailType>();

            Response resp = new Response();

            var result = "";

            try
            {
                var task = await Task.Run(() => ApiHelper.GetURI("api/ProfileEmailType/GetDetail?id= " + id + " "));

                if (task.STATUS)
                {

                    tran = JsonConvert.DeserializeObject<List<ProfileEmailType>>(task.OUTPUT_DATA.ToString());

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

        public async Task<JsonResult> List()
        {
            Response resp = new Response();

            List<ProfileEmailType> tran = new List<ProfileEmailType>();

            try
            {
                var task = await Task.Run(() => ApiHelper.GetURI("api/ProfileEmailType/GetListAll"));

                if (task.STATUS)
                {
                    tran = JsonConvert.DeserializeObject<List<ProfileEmailType>>(task.OUTPUT_DATA.ToString());
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

            var task = await Task.Run(() => ApiHelper.PostURI("api/ProfileEmailType/Insert", httpContent));

            return Json(task);
        }

        public async Task<JsonResult> Update(string jsonString)
        {
            Response res = new Response();

            var httpContent = new StringContent(jsonString, Encoding.UTF8, "application/json");

            var task = await Task.Run(() => ApiHelper.PostURI("api/ProfileEmailType/Update", httpContent));

            return Json(task);
        }

        public async Task<JsonResult> Delete(string jsonString)
        {
            Response res = new Response();

            var httpContent = new StringContent(jsonString, Encoding.UTF8, "application/json");

            var task = await Task.Run(() => ApiHelper.PostURI("api/ProfileEmailType/Delete", httpContent));

            return Json(task);
        }

        public async Task<ActionResult> ExportToCsv()
        {
            Response resp = new Response();

            List<ProfileEmailType> tran = new List<ProfileEmailType>();

            var strBuilder = new StringBuilder();

            try
            {
                var task = await Task.Run(() => ApiHelper.GetURI("api/ProfileEmailType/GetListAll"));

                if (task.STATUS)
                {
                    tran = JsonConvert.DeserializeObject<List<ProfileEmailType>>(task.OUTPUT_DATA.ToString());

                    if (tran.Count() > 0)
                    {
                        strBuilder.AppendLine("" +
                            "EmailTypeNo," +
                            "EmailTypeName," +
                            "CreateBy," +
                            "CreateDate," +
                            "UpdateBy," +
                            "UpdateDate," +
                            "Isactive");


                        foreach (var item in tran)
                        {
                            strBuilder.AppendLine($"" +
                                $"{item.EmailTypeNo}," +
                                $"{item.EmailTypeName}," +
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

            return File(Encoding.UTF8.GetBytes(strBuilder.ToString()), "text/csv", "scg-etax-ProfileEmailType.csv");

        }



    }
}
