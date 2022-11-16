using Microsoft.AspNetCore.Mvc;
using SCG.CAD.ETAX.UTILITY.Authentication;

namespace SCG.CAD.ETAX.WEB.Controllers
{
    public class ConfigControlFunctionController : Controller
    {
        [SessionExpire]
        public IActionResult Index()
        {
            Permission permission = new Permission();
            string pageindex = "26";
            if (!permission.CheckPremissionPage(HttpContext.Session.GetString("premissionMenu"), pageindex))
            {
                HttpContext.Session.SetInt32("checkpermissionpage", 0);
                string pathredirect = Url.Action("Index", "Home");
                return new RedirectResult(pathredirect);
            }
            else
            {
                var menuindex = 26;
                var userLevel = HttpContext.Session.GetInt32("userLevel").ToString();
                var configControl = JsonConvert.DeserializeObject<List<ConfigControlFunction>>(HttpContext.Session.GetString("controlPermission"));

                ViewData["showCREATE"] = permission.CheckControlAction(configControl, 1, userLevel, menuindex);
                ViewData["showUPDATE"] = permission.CheckControlAction(configControl, 2, userLevel, menuindex);
                ViewData["showDELETE"] = permission.CheckControlAction(configControl, 3, userLevel, menuindex);
                ViewData["showEXPORT"] = permission.CheckControlAction(configControl, 4, userLevel, menuindex);
                ViewData["showDOWNLOAD"] = permission.CheckControlAction(configControl, 5, userLevel, menuindex);
                ViewData["showVIEW"] = permission.CheckControlAction(configControl, 6, userLevel, menuindex);
                ViewData["showSEARCH"] = permission.CheckControlAction(configControl, 7, userLevel, menuindex);
                ViewData["showADMINTOOL"] = permission.CheckControlAction(configControl, 8, userLevel, menuindex);

                return View();
            }
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
            List<ConfigControlFunction> tranConfigControlFunction = new List<ConfigControlFunction>();
            List<ConfigFunction> tranConfigFunction = new List<ConfigFunction>();
            List<ProfileUserRole> tranProfileUserRole = new List<ProfileUserRole>();
            ProfileUserRole profileRole = new ProfileUserRole();
            List<ProfileUserRole> profileUserRole = new List<ProfileUserRole>();
            List<PermissionFunction> listPermissionFunction = new List<PermissionFunction>();
            PermissionFunction permissionFunction = new PermissionFunction();
            ConfigControlFunction configControlFunction = new ConfigControlFunction();

            Response resp = new Response();

            var result = "";

            try
            {
                var task = await Task.Run(() => ApiHelper.GetURI("api/ConfigControlFunction/GetListAll"));

                if (task.STATUS)
                {
                    tranConfigControlFunction = JsonConvert.DeserializeObject<List<ConfigControlFunction>>(task.OUTPUT_DATA.ToString());
                    if(tranConfigControlFunction.Count > 0)
                    {
                        tranConfigControlFunction = tranConfigControlFunction.Where(x => x.ConfigControlFunctionMenuNo == id).ToList(); ;
                    }
                }

                task = await Task.Run(() => ApiHelper.GetURI("api/ConfigFunction/GetListAll"));

                if (task.STATUS)
                {
                    tranConfigFunction = JsonConvert.DeserializeObject<List<ConfigFunction>>(task.OUTPUT_DATA.ToString());
                }

                task = await Task.Run(() => ApiHelper.GetURI("api/ProfileUserRole/GetListAll"));

                if (task.STATUS)
                {
                    tranProfileUserRole = JsonConvert.DeserializeObject<List<ProfileUserRole>>(task.OUTPUT_DATA.ToString());
                }

                if(tranConfigFunction.Count > 0)
                {
                    tranConfigFunction = tranConfigFunction.OrderBy(x => x.ConfigFunctionNo).ToList();
                    foreach (var item in tranConfigFunction)
                    {
                        permissionFunction = new PermissionFunction();
                        permissionFunction.ConfigFunctionNo = item.ConfigFunctionNo.ToString();
                        permissionFunction.ConfigFunctionName = item.ConfigFunctionName;

                        permissionFunction.ConfigFunctionActive = false;
                        configControlFunction = tranConfigControlFunction.FirstOrDefault(x => x.ConfigControlNo == item.ConfigFunctionNo);
                        if(configControlFunction != null)
                        {
                            if(configControlFunction.ConfigControlFunctionRole != null)
                            {
                                profileUserRole = new List<ProfileUserRole>();
                                profileRole = new ProfileUserRole();
                                var listrole = configControlFunction.ConfigControlFunctionRole.Split(',').ToList();
                                foreach (var itemrole in listrole)
                                {
                                    profileRole = tranProfileUserRole.FirstOrDefault(x => x.ProfileUserRoleNo.ToString() == itemrole);
                                    if(profileRole != null)
                                    {
                                        profileUserRole.Add(profileRole);
                                    }
                                }
                                permissionFunction.ConfigFunctionGroupRole = string.Join(",", profileUserRole.Select(x=> x.ProfileUserRoleName).ToList());
                                permissionFunction.ConfigFunctionGroupRoleNo = configControlFunction.ConfigControlFunctionRole;
                            }
                            if (configControlFunction.Isactive == 1)
                            {
                                permissionFunction.ConfigFunctionActive = true;
                            }
                        }

                        listPermissionFunction.Add(permissionFunction);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException);
            }

            return Json(new { data = listPermissionFunction });
        }

        public async Task<JsonResult> List()
        {
            Response resp = new Response();

            List<ConfigControlFunction> tran = new List<ConfigControlFunction>();

            try
            {
                var task = await Task.Run(() => ApiHelper.GetURI("api/ConfigControlFunction/GetListAll"));

                if (task.STATUS)
                {
                    tran = JsonConvert.DeserializeObject<List<ConfigControlFunction>>(task.OUTPUT_DATA.ToString());
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

            var task = await Task.Run(() => ApiHelper.PostURI("api/ConfigControlFunction/Insert", httpContent));

            return Json(task);
        }

        public async Task<JsonResult> Update(string jsonString)
        {
            Response res = new Response();

            var httpContent = new StringContent(jsonString, Encoding.UTF8, "application/json");

            var task = await Task.Run(() => ApiHelper.PostURI("api/ConfigControlFunction/Update", httpContent));

            return Json(task);
        }

        public async Task<JsonResult> Delete(string jsonString)
        {
            Response res = new Response();

            var httpContent = new StringContent(jsonString, Encoding.UTF8, "application/json");

            var task = await Task.Run(() => ApiHelper.PostURI("api/ConfigControlFunction/Delete", httpContent));

            return Json(task);
        }

        public class PermissionFunction
        {
            public string ConfigFunctionNo { get; set; }
            public string ConfigFunctionName { get; set; }
            public string ConfigFunctionGroupRoleNo { get; set; }
            public string ConfigFunctionGroupRole { get; set; }
            public bool ConfigFunctionActive { get; set; }
        }
    }
}
