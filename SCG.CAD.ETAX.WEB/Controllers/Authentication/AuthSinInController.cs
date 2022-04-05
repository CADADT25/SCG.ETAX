using Microsoft.AspNetCore.Mvc;

namespace SCG.CAD.ETAX.WEB.Controllers.Authentication
{
    public class AuthSinInController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> getLogin()
        {
            AuthenUserProfile tran = new AuthenUserProfile();

            Response resp = new Response();

            try
            {
                resp.STATUS = true;
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException);

                resp.STATUS = false;
                resp.ERROR_MESSAGE = ex.InnerException.Message.ToString();
            }

            return RedirectToAction("Index", "Home");
        }


    }
}
