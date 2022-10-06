using Microsoft.AspNetCore.Mvc;

namespace SCG.CAD.ETAX.WEB.Controllers
{
    public class configMftsIndexGenerationSettingInputController : Controller
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
            List<ConfigMftsIndexGenerationSettingInput> tran = new List<ConfigMftsIndexGenerationSettingInput>();

            Response resp = new Response();

            var result = "";

            try
            {
                var task = await Task.Run(() => ApiHelper.GetURI("api/ConfigMftsIndexGenerationSettingInput/GetDetail?id= " + id + " "));

                if (task.STATUS)
                {

                    tran = JsonConvert.DeserializeObject<List<ConfigMftsIndexGenerationSettingInput>>(task.OUTPUT_DATA.ToString());

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

            List<ConfigMftsIndexGenerationSettingInput> tran = new List<ConfigMftsIndexGenerationSettingInput>();

            try
            {
                var task = await Task.Run(() => ApiHelper.GetURI("api/ConfigMftsIndexGenerationSettingInput/GetListAll"));

                if (task.STATUS)
                {
                    tran = JsonConvert.DeserializeObject<List<ConfigMftsIndexGenerationSettingInput>>(task.OUTPUT_DATA.ToString());

                    tran = tran.Where(x => x.ConfigMftsIndexGenerationSettingInputCompanyCode == companyCode).ToList();
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

            var task = await Task.Run(() => ApiHelper.PostURI("api/ConfigMftsIndexGenerationSettingInput/Insert", httpContent));

            return Json(task);
        }

        public async Task<JsonResult> Update(string jsonString)
        {
            Response res = new Response();

            var httpContent = new StringContent(jsonString, Encoding.UTF8, "application/json");

            var task = await Task.Run(() => ApiHelper.PostURI("api/ConfigMftsIndexGenerationSettingInput/Update", httpContent));

            return Json(task);
        }

        public async Task<JsonResult> Delete(string jsonString)
        {
            Response res = new Response();

            var httpContent = new StringContent(jsonString, Encoding.UTF8, "application/json");

            var task = await Task.Run(() => ApiHelper.PostURI("api/ConfigMftsIndexGenerationSettingInput/Delete", httpContent));

            return Json(task);
        }


        public async Task<JsonResult> UpdateOneTime(string jsonString)
        {
            Response res = new Response();

            var httpContent = new StringContent(jsonString, Encoding.UTF8, "application/json");

            var task = await Task.Run(() => ApiHelper.PostURI("api/ConfigMftsIndexGenerationSettingInput/UpdateOneTime", httpContent));

            return Json(task);
        }

        public async Task<JsonResult> UpdateAnyTime(string jsonString)
        {
            Response res = new Response();

            var httpContent = new StringContent(jsonString, Encoding.UTF8, "application/json");

            var task = await Task.Run(() => ApiHelper.PostURI("api/ConfigMftsIndexGenerationSettingInput/UpdateAnyTime", httpContent));

            return Json(task);
        }


        public async Task<JsonResult> DeleteOneTime(string jsonString)
        {
            Response res = new Response();

            var httpContent = new StringContent(jsonString, Encoding.UTF8, "application/json");

            var task = await Task.Run(() => ApiHelper.PostURI("api/ConfigMftsIndexGenerationSettingInput/DeleteOneTime", httpContent));

            return Json(task);
        }

        public async Task<JsonResult> DeleteAnyTime(string jsonString)
        {
            Response res = new Response();

            var httpContent = new StringContent(jsonString, Encoding.UTF8, "application/json");

            var task = await Task.Run(() => ApiHelper.PostURI("api/ConfigMftsIndexGenerationSettingInput/DeleteAnyTime", httpContent));

            return Json(task);
        }

    }
}
