using Microsoft.AspNetCore.Mvc;

namespace SCG.CAD.ETAX.WEB.Controllers.Etax
{
    public class CertificateMasterController : Controller
    {
        [SessionExpire]
        public IActionResult Index()
        {
            return View();
        }

        public async Task<JsonResult> List()
        {
            Response resp = new Response();

            List<CertificateMaster> tran = new List<CertificateMaster>();

            try
            {
                var task = await Task.Run(() => ApiHelper.GetURI("api/CertificateMaster/GetListAll"));

                if (task.STATUS)
                {
                    tran = JsonConvert.DeserializeObject<List<CertificateMaster>>(task.OUTPUT_DATA.ToString());
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
    }
}
