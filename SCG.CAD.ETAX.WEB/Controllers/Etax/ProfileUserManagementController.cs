using Microsoft.AspNetCore.Mvc;

namespace SCG.CAD.ETAX.WEB.Controllers
{
    public class ProfileUserManagementController : Controller
    {
        [SessionExpire]
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult _Content()
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
            List<ProfileUserManagement> tran = new List<ProfileUserManagement>();

            var task = await Task.Run(() => ApiHelper.GetURI("api/ProfileUserManagement/GetDetail?id= " + id + " "));

            Response resp = new Response();

            var result = "";

            if (task.STATUS)
            {

                tran = JsonConvert.DeserializeObject<List<ProfileUserManagement>>(task.OUTPUT_DATA.ToString());

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

            List<ProfileUserManagement> tran = new List<ProfileUserManagement>();

            try
            {
                var task = await Task.Run(() => ApiHelper.GetURI("api/ProfileUserManagement/GetListAll"));

                if (task.STATUS)
                {
                    tran = JsonConvert.DeserializeObject<List<ProfileUserManagement>>(task.OUTPUT_DATA.ToString());
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

            var task = await Task.Run(() => ApiHelper.PostURI("api/ProfileUserManagement/Insert", httpContent));

            return Json(task);
        }

        public async Task<JsonResult> Update(string jsonString)
        {
            Response res = new Response();

            var httpContent = new StringContent(jsonString, Encoding.UTF8, "application/json");

            var task = await Task.Run(() => ApiHelper.PostURI("api/ProfileUserManagement/Update", httpContent));

            return Json(task);
        }

        public async Task<JsonResult> Delete(string jsonString)
        {
            Response res = new Response();

            var httpContent = new StringContent(jsonString, Encoding.UTF8, "application/json");

            var task = await Task.Run(() => ApiHelper.PostURI("api/ProfileUserManagement/Delete", httpContent));

            return Json(task);
        }

        public async Task<JsonResult> DropDownList()
        {
            Response resp = new Response();

            List<ProfileUserManagement> tran = new List<ProfileUserManagement>();

            try
            {
                var task = await Task.Run(() => ApiHelper.GetURI("api/ProfileUserManagement/GetListAll"));

                if (task.STATUS)
                {
                    tran = JsonConvert.DeserializeObject<List<ProfileUserManagement>>(task.OUTPUT_DATA.ToString());

                    tran = tran.Where(x => x.AccountStatus == 1).OrderBy(x => x.UserNo).ToList();
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
