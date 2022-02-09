using Microsoft.AspNetCore.Mvc;

namespace SCG.CAD.ETAX.WEB.Controllers.Sidebar
{
    public class SidebarController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public ActionResult TopSidebar()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult LeftSidebar()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult BottomSidebar()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult RightSidebar()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }



    }
}
