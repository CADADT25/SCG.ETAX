using DocumentFormat.OpenXml.Drawing.Charts;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;

namespace SCG.CAD.ETAX.WEB.Controllers
{
    public class RequestController : Controller
    {
        public IActionResult Index(string requestNo)
        {
            var model = new RequestRelateDataModel();
            var res = Task.Run(() => ApiHelper.GetURI("api/Request/GetRequest?requestNo=" + requestNo)).Result;

            if (res.STATUS)
            {
                model = JsonConvert.DeserializeObject<RequestRelateDataModel>(res.OUTPUT_DATA.ToString());
            }
            model.TempUser = HttpContext.Session.GetString("userMail") ?? "";
            // permission
            var permissionModel = new RequestPermissionDataModel();
            var permisRes = Task.Run(() => ApiHelper.GetURI("api/RequestPermission/GetRolesCompanys?user=" + model.TempUser)).Result;
            if (permisRes.STATUS)
            {
                permissionModel = JsonConvert.DeserializeObject<RequestPermissionDataModel>(permisRes.OUTPUT_DATA.ToString());
            }

            // check permission
            if (model.TempUser == model.ManagerEmail)
            {
                model.IsManager = true;
            }
            if (permissionModel.Level == 5)
            {
                model.IsOfficer = true;
            }
            if (model.StatusCode == "wait_manager")
            {
                if (model.TempUser == model.ManagerEmail)
                {
                    model.IsAuth = true;
                }
            }
            else if (model.StatusCode == "wait_officer")
            {
                if (permissionModel.CompanyCodeList.Count > 0)
                {
                    if (permissionModel.CompanyCodeList.Where(t => t.Contains(model.CompanyCode)).Count() > 0)
                    {
                        model.IsAuth = true;
                    }
                }
            }
            
            return View(model);
        }
        public async Task<JsonResult> RequestItem(string jsonString)
        {
            var data = new List<TransactionDescription>();
            try
            {

                var task = await Task.Run(() => ApiHelper.GetURI("api/Request/GetRequestItemTransaction?requestNo=" + jsonString));
                if (task.STATUS)
                {
                    data = JsonConvert.DeserializeObject<List<TransactionDescription>>(task.OUTPUT_DATA.ToString());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return Json(new { data = data });
        }
        public async Task<JsonResult> RequestHistory(string jsonString)
        {
            var data = new List<RequestHistoryDataModel>();
            try
            {

                var task = await Task.Run(() => ApiHelper.GetURI("api/RequestHistory/GetList?requestId=" + jsonString));
                if (task.STATUS)
                {
                    data = JsonConvert.DeserializeObject<List<RequestHistoryDataModel>>(task.OUTPUT_DATA.ToString());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return Json(new { data = data });
        }
    }
}
