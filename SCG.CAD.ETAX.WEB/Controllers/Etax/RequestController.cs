using Microsoft.AspNetCore.Mvc;

namespace SCG.CAD.ETAX.WEB.Controllers
{
    public class RequestController : Controller
    {
        public async Task<JsonResult> AddToCart(string jsonString)
        {
            Response res = new Response();

            var httpContent = new StringContent(jsonString, Encoding.UTF8, "application/json");

            var task = await Task.Run(() => ApiHelper.PostURI("api/RequestCart/InsertList", httpContent));

            return Json(task);
        }
    }
}
