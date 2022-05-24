using Microsoft.AspNetCore.Mvc;

namespace SCG.CAD.ETAX.WEB.Controllers
{
    public class ConfigMftsEmailSettingController : Controller
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

        public IActionResult _OneTime()
        {
            return View();
        }

        public IActionResult _AnyTime()
        {
            return View();
        }



        public async Task<JsonResult> Detail(int id)
        {
            List<ConfigMftsEmailSetting> tran = new List<ConfigMftsEmailSetting>();

            Response resp = new Response();

            var result = "";

            try
            {
                var task = await Task.Run(() => ApiHelper.GetURI("api/ConfigMftsEmailSetting/GetDetail?id= " + id + " "));

                if (task.STATUS)
                {

                    tran = JsonConvert.DeserializeObject<List<ConfigMftsEmailSetting>>(task.OUTPUT_DATA.ToString());

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

            List<ConfigMftsEmailSetting> tran = new List<ConfigMftsEmailSetting>();

            try
            {
                var task = await Task.Run(() => ApiHelper.GetURI("api/ConfigMftsEmailSetting/GetListAll"));

                if (task.STATUS)
                {
                    tran = JsonConvert.DeserializeObject<List<ConfigMftsEmailSetting>>(task.OUTPUT_DATA.ToString());

                    tran = tran.Where(x => x.ConfigMftsEmailSettingCompanyCode == companyCode).ToList();
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

            var task = await Task.Run(() => ApiHelper.PostURI("api/ConfigMftsEmailSetting/Insert", httpContent));

            return Json(task);
        }

        public async Task<JsonResult> Update(string jsonString)
        {
            Response res = new Response();

            var httpContent = new StringContent(jsonString, Encoding.UTF8, "application/json");

            var task = await Task.Run(() => ApiHelper.PostURI("api/ConfigMftsEmailSetting/Update", httpContent));

            return Json(task);
        }

        public async Task<JsonResult> Delete(string jsonString)
        {
            Response res = new Response();

            var httpContent = new StringContent(jsonString, Encoding.UTF8, "application/json");

            var task = await Task.Run(() => ApiHelper.PostURI("api/ConfigMftsEmailSetting/Delete", httpContent));

            return Json(task);
        }

        public async Task<JsonResult> UpdateOneTime(string jsonString)
        {
            Response res = new Response();

            var httpContent = new StringContent(jsonString, Encoding.UTF8, "application/json");

            var task = await Task.Run(() => ApiHelper.PostURI("api/ConfigMftsEmailSetting/UpdateOneTime", httpContent));

            return Json(task);
        }

        public async Task<JsonResult> UpdateAnyTime(string jsonString)
        {
            Response res = new Response();

            var httpContent = new StringContent(jsonString, Encoding.UTF8, "application/json");

            var task = await Task.Run(() => ApiHelper.PostURI("api/ConfigMftsEmailSetting/UpdateAnyTime", httpContent));

            return Json(task);
        }


        public async Task<JsonResult> DeleteOneTime(string jsonString)
        {
            Response res = new Response();

            var httpContent = new StringContent(jsonString, Encoding.UTF8, "application/json");

            var task = await Task.Run(() => ApiHelper.PostURI("api/ConfigMftsEmailSetting/DeleteOneTime", httpContent));

            return Json(task);
        }

        public async Task<JsonResult> DeleteAnyTime(string jsonString)
        {
            Response res = new Response();

            var httpContent = new StringContent(jsonString, Encoding.UTF8, "application/json");

            var task = await Task.Run(() => ApiHelper.PostURI("api/ConfigMftsEmailSetting/DeleteAnyTime", httpContent));

            return Json(task);
        }

    }
}
