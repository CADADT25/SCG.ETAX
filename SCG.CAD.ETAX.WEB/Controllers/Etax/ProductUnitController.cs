using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SCG.CAD.ETAX.MODEL;
using SCG.CAD.ETAX.UTILITY;

namespace SCG.CAD.ETAX.WEB.Controllers
{
    public class ProductUnitController : Controller
    {
        public IActionResult Index()
        {
            List();
            return View();
        }

        public IActionResult _Content()
        {
            return View();
        }

        public async Task<JsonResult> List()
        {
            List<ProductUnit> tran = new List<ProductUnit>();
            var task = await Task.Run(() => ApiHelper.GetURI("api/ProductUnit/GetListAll?"));
            if (task.STATUS)
            {
                tran = JsonConvert.DeserializeObject<List<ProductUnit>>(task.OUTPUT_DATA.ToString());
            }
            else
            {
                ViewBag.Error = task.MESSAGE;
            }
            return Json(tran);
        }
    }
}
