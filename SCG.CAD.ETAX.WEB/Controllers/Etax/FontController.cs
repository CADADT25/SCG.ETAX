﻿using Microsoft.AspNetCore.Mvc;

namespace SCG.CAD.ETAX.WEB.Controllers.Etax
{
    public class FontController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public async Task<JsonResult> List()
        {
            Response resp = new Response();

            List<Font> tran = new List<Font>();

            try
            {
                var task = await Task.Run(() => ApiHelper.GetURI("api/Font/GetListAll"));

                if (task.STATUS)
                {
                    tran = JsonConvert.DeserializeObject<List<Font>>(task.OUTPUT_DATA.ToString());

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


            return Json(tran);
        }
    }
}
