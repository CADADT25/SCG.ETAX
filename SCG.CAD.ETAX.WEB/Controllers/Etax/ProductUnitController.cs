using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SCG.CAD.ETAX.MODEL;
using SCG.CAD.ETAX.UTILITY;
using System.Text;

namespace SCG.CAD.ETAX.WEB.Controllers
{
    public class ProductUnitController : Controller
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

        public async Task<JsonResult> List()
        {
            List<ProductUnit> tran = new List<ProductUnit>();
            var task = await Task.Run(() => ApiHelper.GetURI("api/ProductUnit/GetListAll"));
            var json = JsonConvert.SerializeObject(tran);

            Response resp = new Response();



            if (task.STATUS)
            {
                tran = JsonConvert.DeserializeObject<List<ProductUnit>>(task.OUTPUT_DATA.ToString());
            }
            else
            {
                ViewBag.Error = task.MESSAGE;
            }
            return Json(new { data = tran });
        }

        public async Task<JsonResult> Insert(string jsonString)
        {
            Response res = new Response();

            //string json = JsonConvert.SerializeObject(jsonString, Formatting.Indented);

            var httpContent = new StringContent(jsonString, Encoding.UTF8, "application/json");

            var task = await Task.Run(() => ApiHelper.PostURI("api/ProductUnit/Insert", httpContent));

            return Json(task);
        }
    }
}
