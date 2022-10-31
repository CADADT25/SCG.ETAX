using Microsoft.AspNetCore.Mvc;
using SCG.CAD.ETAX.UTILITY.Authentication;
using System.Threading.Tasks;

namespace SCG.CAD.ETAX.WEB.Controllers
{
    public class RequestCartController : Controller
    {
        public IActionResult Index()
        {
            //ViewData["cartCount"] = 0;
            //ViewData["cartList"] = new List<RequestCart>();
            var models = new List<RequestCart>();
            try
            {
                string email = HttpContext.Session.GetString("userMail") ?? "";
                if (!string.IsNullOrEmpty(email))
                {
                    var req = new RequestCartSearchModel() { CreateBy = email };

                    var httpContent = new StringContent(JsonConvert.SerializeObject(req), Encoding.UTF8, "application/json");

                    var res = Task.Run(() => ApiHelper.PostURI("api/RequestCart/Search", httpContent)).Result;
                    if (res.STATUS)
                    {
                        models = JsonConvert.DeserializeObject<List<RequestCart>>(res.OUTPUT_DATA.ToString());
                        models = models.OrderBy(t => t.BillingNumber).ToList();
                        //ViewData["cartCount"] = output != null ? output.Count() : 0;
                        //ViewData["cartList"] = output;
                    }
                }
            }
            catch
            {

            }

            return View(models);
        }

        public IActionResult ManageCart()
        {
            var models = new ManageRequestCartModel();
            try
            {
                //string email = HttpContext.Session.GetString("userMail") ?? "";
                //if (!string.IsNullOrEmpty(email))
                //{
                //    var req = new RequestCartSearchModel() { CreateBy = email };

                //    var httpContent = new StringContent(JsonConvert.SerializeObject(req), Encoding.UTF8, "application/json");

                //    var res = Task.Run(() => ApiHelper.PostURI("api/RequestCart/Search", httpContent)).Result;
                //    if (res.STATUS)
                //    {
                //        models.RequestCarts = JsonConvert.DeserializeObject<List<RequestCart>>(res.OUTPUT_DATA.ToString()) ?? new List<RequestCart>();
                //        models.RequestCarts = models.RequestCarts.OrderBy(t => t.BillingNumber).ToList();
                //    }
                //}
            }
            catch
            {

            }

            return View(models);
        }
        public async Task<JsonResult> List(string searchJson)
        {
            string email = HttpContext.Session.GetString("userMail") ?? "";

            Response resp = new Response();

            List<RequestCartDataModel> tran = new List<RequestCartDataModel>();

            try
            {
                var req = new RequestCartSearchModel() { CreateBy = email };

                var httpContent = new StringContent(JsonConvert.SerializeObject(req), Encoding.UTF8, "application/json");
                var res = await Task.Run(() => ApiHelper.PostURI("api/RequestCart/SearchFull", httpContent));

                if (res.STATUS)
                {
                    tran = JsonConvert.DeserializeObject<List<RequestCartDataModel>>(res.OUTPUT_DATA.ToString());
                }
                else
                {
                    ViewBag.Error = res.MESSAGE;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException);
            }


            return Json(new { data = tran });
        }

        public async Task<JsonResult> AddToCart(string jsonString)
        {
            Response res = new Response();

            var httpContent = new StringContent(jsonString, Encoding.UTF8, "application/json");

            var task = await Task.Run(() => ApiHelper.PostURI("api/RequestCart/InsertList", httpContent));

            return Json(task);
        }
        public async Task<JsonResult> DeleteCart(string jsonString)
        {
            Response res = new Response();

            var httpContent = new StringContent(jsonString, Encoding.UTF8, "application/json");

            var task = await Task.Run(() => ApiHelper.PostURI("api/RequestCart/Delete", httpContent));

            return Json(task);
        }
        public async Task<JsonResult> MultiDeleteCart(string jsonString)
        {
            Response res = new Response();

            var httpContent = new StringContent(jsonString, Encoding.UTF8, "application/json");

            var task = await Task.Run(() => ApiHelper.PostURI("api/RequestCart/MultiDelete", httpContent));

            return Json(task);
        }
    }
}
