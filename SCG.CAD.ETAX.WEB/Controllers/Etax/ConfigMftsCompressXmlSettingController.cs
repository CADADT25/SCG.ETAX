using Microsoft.AspNetCore.Mvc;

namespace SCG.CAD.ETAX.WEB.Controllers
{
    public class ConfigMftsCompressXmlSettingController : Controller
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
            List<ConfigMftsCompressXmlSetting> tran = new List<ConfigMftsCompressXmlSetting>();

            Response resp = new Response();

            var result = "";

            try
            {
                var task = await Task.Run(() => ApiHelper.GetURI("api/ConfigMftsCompressXmlSetting/GetDetail?id= " + id + " "));

                if (task.STATUS)
                {

                    tran = JsonConvert.DeserializeObject<List<ConfigMftsCompressXmlSetting>>(task.OUTPUT_DATA.ToString());

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

            List<ConfigMftsCompressXmlSetting> tran = new List<ConfigMftsCompressXmlSetting>();

            try
            {
                var task = await Task.Run(() => ApiHelper.GetURI("api/ConfigMftsCompressXmlSetting/GetListAll"));

                if (task.STATUS)
                {
                    tran = JsonConvert.DeserializeObject<List<ConfigMftsCompressXmlSetting>>(task.OUTPUT_DATA.ToString());

                    tran = tran.Where(x => x.ConfigMftsCompressXmlSettingCompanyCode == companyCode).ToList();
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

            var task = await Task.Run(() => ApiHelper.PostURI("api/ConfigMftsCompressXmlSetting/Insert", httpContent));

            return Json(task);
        }

        public async Task<JsonResult> Update(string jsonString)
        {
            Response res = new Response();

            var httpContent = new StringContent(jsonString, Encoding.UTF8, "application/json");

            var task = await Task.Run(() => ApiHelper.PostURI("api/ConfigMftsCompressXmlSetting/Update", httpContent));

            return Json(task);
        }

        public async Task<JsonResult> Delete(string jsonString)
        {
            Response res = new Response();

            var httpContent = new StringContent(jsonString, Encoding.UTF8, "application/json");

            var task = await Task.Run(() => ApiHelper.PostURI("api/ConfigMftsCompressXmlSetting/Delete", httpContent));

            return Json(task);
        }

    }
}
