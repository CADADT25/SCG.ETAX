using DocumentFormat.OpenXml.Drawing;
using DocumentFormat.OpenXml.Drawing.Charts;
using DocumentFormat.OpenXml.ExtendedProperties;
using Microsoft.AspNetCore.Mvc;
using SCG.CAD.ETAX.MODEL.etaxModel;
using System;
using System.Net.Http;

namespace SCG.CAD.ETAX.WEB.Controllers
{
    public class RequestActionController : Controller
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
            if (model.StatusCode == Variable.RequestStatusCode_WaitManager)
            {
                if (model.TempUser == model.ManagerEmail)
                {
                    model.IsAuth = true;
                }
            }
            else if (model.StatusCode == Variable.RequestStatusCode_WaitOfficer)
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

        public async Task<JsonResult> Action(string jsonString, string action, string status, string reason)
        {
            Response ret = new Response();
            string requestNo = jsonString;
            string userEmail = HttpContext.Session.GetString("userMail") ?? "";
            try
            {
                // permission
                var permissionModel = new RequestPermissionDataModel();
                var permisRes = Task.Run(() => ApiHelper.GetURI("api/RequestPermission/GetRolesCompanys?user=" + userEmail)).Result;
                if (permisRes.STATUS)
                {
                    permissionModel = JsonConvert.DeserializeObject<RequestPermissionDataModel>(permisRes.OUTPUT_DATA.ToString());
                }
                // request
                var requestModel = new RequestRelateDataModel();
                var res = Task.Run(() => ApiHelper.GetURI("api/Request/GetRequest?requestNo=" + requestNo)).Result;

                if (res.STATUS)
                {
                    requestModel = JsonConvert.DeserializeObject<RequestRelateDataModel>(res.OUTPUT_DATA.ToString());
                }
                // check permission
                if (userEmail == requestModel.ManagerEmail)
                {
                    requestModel.IsManager = true;
                }
                if (permissionModel.Level == 5)
                {
                    requestModel.IsOfficer = true;
                }
                if (requestModel.StatusCode == Variable.RequestStatusCode_WaitManager)
                {
                    if (userEmail == requestModel.ManagerEmail)
                    {
                        requestModel.IsAuth = true;
                    }
                }
                else if (requestModel.StatusCode == Variable.RequestStatusCode_WaitOfficer)
                {
                    if (permissionModel.CompanyCodeList.Count > 0)
                    {
                        if (permissionModel.CompanyCodeList.Where(t => t.Contains(requestModel.CompanyCode)).Count() > 0)
                        {
                            requestModel.IsAuth = true;
                        }
                    }
                }

                string errorMsg = "";
                // check permission
                if (!requestModel.IsAuth)
                {
                    errorMsg = UtilityHelper.SetError(errorMsg, "You are not authorized.");
                }
                if (action == "manager_approve" || action == "manager_reject")
                {
                    if (requestModel.StatusCode == status && status == Variable.RequestStatusCode_WaitManager)
                    {

                    }
                    else
                    {
                        errorMsg = UtilityHelper.SetError(errorMsg, "Request status invalid.");
                    }
                }
                else if (action == "officer_approve" || action == "officer_reject")
                {
                    if (requestModel.StatusCode == status && status == Variable.RequestStatusCode_WaitOfficer)
                    {

                    }
                    else
                    {
                        errorMsg = UtilityHelper.SetError(errorMsg, "Request status invalid.");
                    }
                }
                else
                {
                    errorMsg = UtilityHelper.SetError(errorMsg, "Unknown action state.");
                }

                if (string.IsNullOrEmpty(errorMsg))
                {
                    var requestData = new RequestActionDataModel() { RequestId = requestModel.RequestId, Action = action, User = userEmail, Reason = reason };
                    if (action == "manager_reject" || action == "officer_reject")
                    {
                        // Action here
                        var httpActionRequest = new StringContent(JsonConvert.SerializeObject(requestData), Encoding.UTF8, "application/json");
                        var actRes = await Task.Run(() => ApiHelper.PostURI("api/Request/Action", httpActionRequest));
                        if (actRes.STATUS)
                        {
                            ret.STATUS = true;
                            //Send mail
                            Task.Run(() => ApiHelper.GetURI("api/SendEmail/SendEmailRequestByAction?requestNo=" + requestModel.RequestNo + "&action=reject"));
                        }
                        else
                        {
                            ret.STATUS = false;
                            ret.MESSAGE = actRes.MESSAGE ?? actRes.INNER_EXCEPTION;
                        }
                    }
                    else
                    {
                        //item
                        var data = new List<TransactionDescription>();
                        var task = await Task.Run(() => ApiHelper.GetURI("api/Request/GetRequestItemTransaction?requestNo=" + requestNo));
                        if (task.STATUS)
                        {
                            data = JsonConvert.DeserializeObject<List<TransactionDescription>>(task.OUTPUT_DATA.ToString());
                        }

                        //Verify
                        errorMsg = ValidateBeforSubmitRequest(requestModel.RequestAction, data);
                        if (string.IsNullOrEmpty(errorMsg))
                        {
                            ret.STATUS = true;
                            //Action here
                            var httpActionRequest = new StringContent(JsonConvert.SerializeObject(requestData), Encoding.UTF8, "application/json");
                            var actRes = await Task.Run(() => ApiHelper.PostURI("api/Request/Action", httpActionRequest));
                            if (actRes.STATUS)
                            {
                                ret.STATUS = true;
                            }
                            else
                            {
                                ret.STATUS = false;
                                ret.MESSAGE = actRes.MESSAGE ?? actRes.INNER_EXCEPTION;
                            }

                        }
                        else
                        {
                            ret.STATUS = false;
                            ret.MESSAGE = errorMsg;
                        }
                    }
                }
                else
                {
                    ret.STATUS = false;
                    ret.MESSAGE = errorMsg;
                }



            }
            catch (Exception ex)
            {
                ret.STATUS = false;
                ret.MESSAGE = ex.Message;
            }


            return Json(ret);
        }
        private string ValidateBeforSubmitRequest(string requestAction, List<TransactionDescription> dataTrans)
        {
            string errorMsg = "";
            if (requestAction != Variable.RequestActionCode_Delete && requestAction != Variable.RequestActionCode_Undelete && requestAction != Variable.RequestActionCode_ReSignNewTrans && requestAction != Variable.RequestActionCode_ReSignNewCert)
            {
                errorMsg = UtilityHelper.SetError(errorMsg, "Unknown action.");
            }

            if (dataTrans.Count() == 0)
            {
                errorMsg = UtilityHelper.SetError(errorMsg, "Transaction not found.");
            }

            foreach (var item in dataTrans)
            {
                // delete
                if (requestAction == Variable.RequestActionCode_Delete)
                {
                    //if (item.XmlCompressStatus == "Successful")
                    //{
                    //    errorMsg = UtilityHelper.SetError(errorMsg, "Billing No. " + item.BillingNumber + " compressed file.");
                    //}
                    if (item.Isactive != 1)
                    {
                        errorMsg = UtilityHelper.SetError(errorMsg, "Billing No. " + item.BillingNumber + " deleted.");
                    }
                }
                // undelete
                else if (requestAction == Variable.RequestActionCode_Undelete)
                {
                    if (item.Isactive == 1)
                    {
                        errorMsg = UtilityHelper.SetError(errorMsg, "Billing No. " + item.BillingNumber + " undeleted.");
                    }
                }
                else if (requestAction == Variable.RequestActionCode_ReSignNewTrans)
                {

                }
                else if (requestAction == Variable.RequestActionCode_ReSignNewCert)
                {

                }
                // unzip
                //else if (requestAction == "unzip")
                //{
                //    if (item.XmlCompressStatus != "Successful")
                //    {
                //        errorMsg = UtilityHelper.SetError(errorMsg, "Billing No. " + item.BillingNumber + " uncompressed file.");
                //    }
                //    if (item.Isactive != 1)
                //    {
                //        errorMsg = UtilityHelper.SetError(errorMsg, "Billing No. " + item.BillingNumber + " deleted.");
                //    }
                //    if (item.SentRevenueDepartment == 1)
                //    {
                //        errorMsg = UtilityHelper.SetError(errorMsg, "Billing No. " + item.BillingNumber + " sent to the Revenue Department.");
                //    }
                //}
            }


            return errorMsg;
        }

        public IActionResult ByEmail(string token)
        {
            try
            {
                var decode = UtilityHelper.DecodeSpecialChar(token);
                var strData = UtilityHelper.DecryptString(decode);
                var dataArr = strData.Split(";");
                ViewBag.RequestNo = dataArr[0];
                ViewBag.Action = dataArr[1] == "approve" ? "Approve" : "Reject";
                string action = dataArr[1] == "approve" ? "manager_approve" : "manager_reject";
                ViewBag.UserEmail = dataArr[2];
                ViewBag.User = dataArr[3];
                ViewBag.Status = "Success";
                ViewBag.Message = "The request has been " + ViewBag.Action;


                // permission
                var permissionModel = new RequestPermissionDataModel();
                var permisRes = Task.Run(() => ApiHelper.GetURI("api/RequestPermission/GetRolesCompanys?user=" + dataArr[2])).Result;
                if (permisRes.STATUS)
                {
                    permissionModel = JsonConvert.DeserializeObject<RequestPermissionDataModel>(permisRes.OUTPUT_DATA.ToString());
                }
                // request
                var requestModel = new RequestRelateDataModel();
                var res = Task.Run(() => ApiHelper.GetURI("api/Request/GetRequest?requestNo=" + dataArr[0])).Result;

                if (res.STATUS)
                {
                    requestModel = JsonConvert.DeserializeObject<RequestRelateDataModel>(res.OUTPUT_DATA.ToString());
                }
                // check permission
                if (dataArr[2] == requestModel.ManagerEmail)
                {
                    requestModel.IsManager = true;
                }
                if (permissionModel.Level == 5)
                {
                    requestModel.IsOfficer = true;
                }
                if (requestModel.StatusCode == Variable.RequestStatusCode_WaitManager)
                {
                    if (dataArr[2] == requestModel.ManagerEmail)
                    {
                        requestModel.IsAuth = true;
                    }
                }


                string errorMsg = "";
                // check permission
                // check permission
                if (!requestModel.IsAuth)
                {
                    errorMsg = UtilityHelper.SetError(errorMsg, "You are not authorized.");
                }
                if (requestModel.StatusCode != Variable.RequestStatusCode_WaitManager)
                {
                    errorMsg = UtilityHelper.SetError(errorMsg, "Request status invalid.");
                }


                if (string.IsNullOrEmpty(errorMsg))
                {
                    var requestData = new RequestActionDataModel() { RequestId = requestModel.RequestId, Action = action, User = dataArr[2], Reason = "Action by email" };
                    if (action == "manager_reject" || action == "officer_reject")
                    {
                        // Action here
                        var httpActionRequest = new StringContent(JsonConvert.SerializeObject(requestData), Encoding.UTF8, "application/json");
                        var actRes = Task.Run(() => ApiHelper.PostURI("api/Request/Action", httpActionRequest)).Result;
                        if (actRes.STATUS)
                        {

                            //Send mail
                            Task.Run(() => ApiHelper.GetURI("api/SendEmail/SendEmailRequestByAction?requestNo=" + requestModel.RequestNo + "&action=reject"));
                        }
                        else
                        {
                            ViewBag.Status = "Failed";
                            ViewBag.Message = actRes.MESSAGE ?? actRes.INNER_EXCEPTION;
                        }
                    }
                    else
                    {
                        //item
                        var data = new List<TransactionDescription>();
                        var task = Task.Run(() => ApiHelper.GetURI("api/Request/GetRequestItemTransaction?requestNo=" + requestModel.RequestNo)).Result;
                        if (task.STATUS)
                        {
                            data = JsonConvert.DeserializeObject<List<TransactionDescription>>(task.OUTPUT_DATA.ToString());
                        }

                        //Verify
                        errorMsg = ValidateBeforSubmitRequest(requestModel.RequestAction, data);
                        if (string.IsNullOrEmpty(errorMsg))
                        {
                            //Action here
                            var httpActionRequest = new StringContent(JsonConvert.SerializeObject(requestData), Encoding.UTF8, "application/json");
                            var actRes = Task.Run(() => ApiHelper.PostURI("api/Request/Action", httpActionRequest)).Result;
                            if (actRes.STATUS)
                            {
                                ViewBag.Status = "Success";
                                ViewBag.Message = "The request has been " + ViewBag.Action;
                            }
                            else
                            {
                                ViewBag.Status = "Failed";
                                ViewBag.Message = actRes.MESSAGE ?? actRes.INNER_EXCEPTION;
                            }

                        }
                        else
                        {
                            ViewBag.Status = "Failed";
                            ViewBag.Message = errorMsg;
                        }
                    }
                }
                else
                {
                    ViewBag.Status = "Failed";
                    ViewBag.Message = errorMsg;
                }
            }
            catch (Exception ex)
            {
                ViewBag.Status = "Failed";
                ViewBag.Message = ex.Message.ToString();
            }

            return View("Index");
        }
    }
}
