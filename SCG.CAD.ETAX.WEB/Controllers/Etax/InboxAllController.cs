using Microsoft.AspNetCore.Mvc;

namespace SCG.CAD.ETAX.WEB.Controllers.Etax
{
    [SessionExpire]
    public class InboxAllController : Controller
    {
        [SessionExpire]
        public IActionResult Index()
        {
            return View();
        }
        public async Task<JsonResult> SearchAll(string jsonString)
        {
            var data = new List<InboxModelData>();
            try
            {
                var httpContent = new StringContent(jsonString, Encoding.UTF8, "application/json");

                var task = await Task.Run(() => ApiHelper.PostURI("api/InboxManagement/SearchAll", httpContent));
                if (task.STATUS)
                {
                    data = JsonConvert.DeserializeObject<List<InboxModelData>>(task.OUTPUT_DATA.ToString());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return Json(new { data = data });
        }
    }
}
