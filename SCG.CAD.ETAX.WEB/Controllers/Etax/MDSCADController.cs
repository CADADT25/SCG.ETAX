using DocumentFormat.OpenXml.Drawing.Charts;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;

namespace SCG.CAD.ETAX.WEB.Controllers
{
    public class MDSCADController : Controller
    {
        public async Task<JsonResult> GetManagerByUser()
        {
            var data = new EhrUserModel();
            try
            {
                string user = HttpContext.Session.GetString("userMail") ?? "";
                var task = await Task.Run(() => ApiHelper.GetURI("api/MDSCAD/GetManagerByUser?user=" + user));
                if (task.STATUS)
                {
                    data = JsonConvert.DeserializeObject<EhrUserModel>(task.OUTPUT_DATA.ToString());
                }
            }
            catch (Exception ex)
            {
                
            }

            return Json(new { data = data });
        }
    }
}
