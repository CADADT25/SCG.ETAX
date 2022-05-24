using Microsoft.AspNetCore.Mvc;

namespace SCG.CAD.ETAX.WEB.Controllers
{
    public class ProfileUserRoleController : Controller
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


        public IActionResult _Create()
        {
            return View();
        }


        public IActionResult _Update()
        {
            return View();
        }


        public async Task<JsonResult> Detail(int id)
        {
            List<ProfileUserRole> tran = new List<ProfileUserRole>();

            var task = await Task.Run(() => ApiHelper.GetURI("api/ProfileUserRole/GetDetail?id= " + id + " "));

            Response resp = new Response();

            var result = "";

            if (task.STATUS)
            {

                tran = JsonConvert.DeserializeObject<List<ProfileUserRole>>(task.OUTPUT_DATA.ToString());

                result = JsonConvert.SerializeObject(tran[0]);

            }
            else
            {
                ViewBag.Error = task.MESSAGE;
            }
            return Json(result);
        }

        public async Task<JsonResult> List()
        {
            Response resp = new Response();

            List<ProfileUserRole> tran = new List<ProfileUserRole>();

            try
            {
                var task = await Task.Run(() => ApiHelper.GetURI("api/ProfileUserRole/GetListAll"));

                if (task.STATUS)
                {
                    tran = JsonConvert.DeserializeObject<List<ProfileUserRole>>(task.OUTPUT_DATA.ToString());
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

        public async Task<JsonResult> Insert(string jsonString)
        {
            Response res = new Response();

            var httpContent = new StringContent(jsonString, Encoding.UTF8, "application/json");

            var task = await Task.Run(() => ApiHelper.PostURI("api/ProfileUserRole/Insert", httpContent));

            return Json(task);
        }

        public async Task<JsonResult> Update(string jsonString)
        {
            Response res = new Response();

            var httpContent = new StringContent(jsonString, Encoding.UTF8, "application/json");

            var task = await Task.Run(() => ApiHelper.PostURI("api/ProfileUserRole/Update", httpContent));

            return Json(task);
        }

        public async Task<JsonResult> Delete(string jsonString)
        {
            Response res = new Response();

            var httpContent = new StringContent(jsonString, Encoding.UTF8, "application/json");

            var task = await Task.Run(() => ApiHelper.PostURI("api/ProfileUserRole/Delete", httpContent));

            return Json(task);
        }

        public async Task<JsonResult> DropDownList()
        {
            Response resp = new Response();

            List<ProfileUserRole> tran = new List<ProfileUserRole>();

            try
            {
                var task = await Task.Run(() => ApiHelper.GetURI("api/ProfileUserRole/GetListAll"));

                if (task.STATUS)
                {
                    tran = JsonConvert.DeserializeObject<List<ProfileUserRole>>(task.OUTPUT_DATA.ToString());

                    tran = tran.Where(x => x.Isactive == 1).OrderBy(x => x.ProfileUserRoleName).ToList();
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
