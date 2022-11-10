using DocumentFormat.OpenXml.ExtendedProperties;
using SCG.CAD.ETAX.MODEL.etaxModel;
using SCG.CAD.ETAX.UTILITY;
using System.Reflection;

namespace SCG.CAD.ETAX.API.Services
{
    public class RequestService : DatabaseExecuteController
    {
        readonly DatabaseContext _dbContext = new();
        public DateTime dtNow = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd'" + "T" + "'HH:mm:ss.fff"));

        sqlRequest sqlFactory = new sqlRequest();
        public Response GET_LIST()
        {
            Response resp = new Response();
            try
            {
                var getList = _dbContext.request.ToList();

                if (getList.Count > 0)
                {
                    resp.STATUS = true;
                    resp.MESSAGE = "Get list count '" + getList.Count + "' records. ";
                    resp.OUTPUT_DATA = getList;
                }
                else
                {
                    resp.STATUS = false;
                    resp.MESSAGE = "Data not found";
                }

            }
            catch (Exception ex)
            {
                resp.STATUS = false;
                resp.MESSAGE = "Get data fail.";
                resp.INNER_EXCEPTION = ex.InnerException.ToString();
            }
            return resp;
        }
        public Response GET_REQUEST(string requestNo)
        {
            Response resp = new Response();
            var requestRelate = new RequestRelateDataModel();
            try
            {
                var requestData = _dbContext.request.Where(t => t.RequestNo == requestNo).FirstOrDefault();

                var requester = _dbContext.profileUserManagement.Where(t => t.UserEmail == requestData.CreateBy).FirstOrDefault();
                var manager = _dbContext.profileUserManagement.Where(t => t.UserEmail == requestData.Manager).FirstOrDefault();
                ProfileUserManagement officer = null;
                if (!string.IsNullOrEmpty(requestData.OfficerBy))
                {
                    officer = _dbContext.profileUserManagement.Where(t => t.UserEmail == requestData.OfficerBy).FirstOrDefault();
                }

                requestRelate = new RequestRelateDataModel()
                {
                    RequestId = requestData.Id,
                    RequestAction = requestData.RequestAction,
                    RequestDate = requestData.CreateDate,
                    RequesterEmail = requestData.CreateBy,
                    RequesterName = requester.FirstName + " " + requester.LastName,
                    RequestNo = requestData.RequestNo,
                    CompanyCode = requestData.CompanyCode,
                    StatusCode = requestData.StatusCode,
                    ManagerEmail = requestData.Manager,
                    ManagerName = manager.FirstName + " " + manager.LastName,
                    OfficerEmail = requestData.OfficerBy ?? "",
                    OfficerName = officer != null ? officer.FirstName + " " + officer.LastName : "",
                    RequestHistorys = _dbContext.requestHistory.Where(t => t.RequestId == requestData.Id).ToList()
                };

                resp.STATUS = true;
                resp.OUTPUT_DATA = requestRelate;

            }
            catch (Exception ex)
            {
                resp.STATUS = false;
                resp.MESSAGE = "Get data fail.";
                resp.INNER_EXCEPTION = ex.InnerException.ToString();
            }
            return resp;
        }
        public Response GET_REQUEST_ITEM_TRANSACTION(string requestNo)
        {
            Response resp = new Response();
            try
            {
                string sql = sqlFactory.GET_REQUEST_ITEM_TRANSACTION(requestNo);

                var resultData = base.SearchBySql(sql);
                if (resultData.StatusOnDb)
                {

                    if (resultData.ResultOnDb.Rows.Count > 0)
                    {
                        var dataList = new List<TransactionDescription>();
                        dataList = UtilityHelper.ConvertDataTableToModel<TransactionDescription>(resultData.ResultOnDb);
                        resp.STATUS = true;
                        resp.OUTPUT_DATA = dataList;
                    }
                    else
                    {
                        resp.STATUS = false;
                        resp.MESSAGE = "Data not found.";
                    }
                }
                else
                {
                    resp.STATUS = false;
                    resp.MESSAGE = resultData.MessageOnDb;
                }

            }
            catch (Exception ex)
            {
                resp.STATUS = false;
                resp.MESSAGE = "Get data fail.";
                resp.INNER_EXCEPTION = ex.InnerException.ToString();
            }
            return resp;
        }

        public Response INSERT(Request param)
        {
            Response resp = new Response();
            try
            {
                using (_dbContext)
                {
                    param.CreateDate = dtNow;
                    param.UpdateDate = dtNow;

                    _dbContext.request.Add(param);
                    _dbContext.SaveChanges();


                    resp.STATUS = true;
                    resp.MESSAGE = "Insert success.";
                }
            }
            catch (Exception ex)
            {
                resp.STATUS = false;
                resp.MESSAGE = "Insert faild.";
                resp.INNER_EXCEPTION = ex.InnerException.ToString();
            }
            return resp;
        }

        public Response UPDATE(Request param)
        {
            Response resp = new Response();
            try
            {
                using (_dbContext)
                {
                    var update = _dbContext.request.Where(x => x.Id == param.Id).FirstOrDefault();

                    if (update != null)
                    {
                        update.RequestNo = param.RequestNo;
                        update.RequestAction = param.RequestAction;
                        update.StatusCode = param.StatusCode;
                        update.CompanyCode = param.CompanyCode;
                        update.Manager = param.Manager;
                        update.ManagerAction = param.ManagerAction;
                        update.OfficerBy = param.OfficerBy;

                        update.UpdateBy = param.UpdateBy;
                        update.UpdateDate = dtNow;

                        _dbContext.SaveChanges();

                        resp.STATUS = true;
                        resp.MESSAGE = "Updated Success.";
                    }
                    else
                    {
                        resp.STATUS = false;
                        resp.MESSAGE = "Can't update because data not found.";
                    }
                }
            }
            catch (Exception ex)
            {
                resp.STATUS = false;
                resp.MESSAGE = "Update faild.";
                resp.INNER_EXCEPTION = ex.InnerException.ToString();
            }
            return resp;
        }

        public Response DELETE(Request param)
        {
            Response resp = new Response();
            try
            {
                using (_dbContext)
                {
                    var delete = _dbContext.request.Find(param.Id);

                    if (delete != null)
                    {
                        _dbContext.request.Remove(delete);
                        _dbContext.SaveChanges();

                        resp.STATUS = true;
                        resp.MESSAGE = "Delete success.";
                    }
                    else
                    {
                        resp.STATUS = false;
                        resp.MESSAGE = "Can't delete because data not found.";
                    }
                }
            }
            catch (Exception ex)
            {
                resp.STATUS = false;
                resp.MESSAGE = "Delete faild.";
                resp.INNER_EXCEPTION = ex.InnerException.ToString();
            }
            return resp;
        }
        public Response SUBMIT_REQUEST(RequestDataModel param)
        {
            Response resp = new Response();
            try
            {
                var reqNos = new List<string>();
                using (_dbContext)
                {
                    var companys = param.RequestCartList.Select(t => t.CompanyCode).Distinct().ToList();
                    foreach (var comCode in companys)
                    {
                        var dataList = param.RequestCartList.Where(t => t.CompanyCode == comCode).ToList();
                        var request = new Request();
                        request.Id = Guid.NewGuid();
                        request.RequestNo = GetRunningNo(comCode);
                        request.RequestAction = param.Action;
                        request.StatusCode = Variable.RequestStatusCode_WaitManager;
                        request.CompanyCode = comCode;
                        request.Manager = param.Manager;
                        request.CreateDate = dtNow;
                        request.CreateBy = param.UserBy;
                        request.UpdateDate = dtNow;
                        request.UpdateBy = param.UserBy;
                        reqNos.Add(request.RequestNo);
                        _dbContext.request.Add(request);
                        var history = new RequestHistory();
                        history.Id = Guid.NewGuid();
                        history.RequestId = request.Id;
                        history.Action = "Submit";
                        history.CreateDate = dtNow;
                        history.CreateBy = param.UserBy;
                        history.UpdateDate = dtNow;
                        history.UpdateBy = param.UserBy;
                        _dbContext.requestHistory.Add(history);
                        foreach (var item in dataList)
                        {
                            var requestItem = new RequestItem();
                            requestItem.Id = Guid.NewGuid();
                            requestItem.RequestId = request.Id;
                            requestItem.TransactionNo = item.TransactionNo;
                            requestItem.BillingNumber = item.BillingNumber;
                            requestItem.CreateDate = dtNow;
                            requestItem.CreateBy = param.UserBy;
                            requestItem.UpdateDate = dtNow;
                            requestItem.UpdateBy = param.UserBy;
                            _dbContext.requestItem.Add(requestItem);

                            var delete = _dbContext.requestCart.Find(item.Id);
                            if (delete != null)
                            {
                                _dbContext.requestCart.Remove(delete);
                            }
                        }
                    }
                    _dbContext.SaveChanges();
                    resp.STATUS = true;
                    resp.MESSAGE = "Submit request success.";
                    resp.OUTPUT_DATA = reqNos;
                }
            }
            catch (Exception ex)
            {
                resp.STATUS = false;
                resp.MESSAGE = "Submit faild.";
                resp.INNER_EXCEPTION = ex.Message;
            }
            return resp;
        }
        public Response Action(RequestActionDataModel param)
        {
            Response resp = new Response();
            try
            {
                using (_dbContext)
                {
                    List<string> outputXmlTransactionNos = new List<string>();
                    var request = _dbContext.request.Where(t => t.Id == param.RequestId).FirstOrDefault();
                    if (param.Action == "manager_approve")
                    {
                        request.StatusCode = Variable.RequestStatusCode_WaitOfficer;
                        request.ManagerAction = true;
                    }
                    else if (param.Action == "manager_reject")
                    {
                        request.StatusCode = Variable.RequestStatusCode_Reject;
                        request.ManagerAction = true;
                    }
                    else if (param.Action == "officer_approve")
                    {
                        request.StatusCode = Variable.RequestStatusCode_Complete;
                        request.OfficerBy = param.User;
                        // Clear Data
                        if (request.RequestAction == Variable.RequestActionCode_Delete)
                        {
                            var requestItem = _dbContext.requestItem.Where(t => t.RequestId == request.Id).ToList();
                            var tranNos = requestItem.Select(t => t.TransactionNo).Distinct().ToList();
                            var transactions = _dbContext.transactionDescription.Where(t => tranNos.Contains(t.TransactionNo)).ToList();
                            foreach (var tran in transactions)
                            {
                                if (tran.XmlCompressStatus == "Successful")
                                {
                                    tran.XmlCompressStatus = "Waiting";
                                    if (tran.OutputXmlTransactionNo != null)
                                    {
                                        var transactions2 = _dbContext.transactionDescription.Where(t => t.OutputXmlTransactionNo == tran.OutputXmlTransactionNo && !tranNos.Contains(t.TransactionNo)).ToList();
                                        foreach (var tran2 in transactions2)
                                        {
                                            tran2.XmlCompressStatus = "Waiting";
                                            tran2.OutputXmlTransactionNo = null;
                                            tran2.UpdateBy = param.User;
                                            tran2.UpdateDate = dtNow;
                                            _dbContext.Entry(tran2).State = EntityState.Modified;
                                        }
                                        var outputxml = _dbContext.outputSearchXmlZip.Where(t => t.OutputSearchXmlZipNo == int.Parse(tran.OutputXmlTransactionNo)).FirstOrDefault();
                                        if (outputxml != null)
                                        {
                                            outputxml.Isactive = 0;
                                            outputxml.UpdateBy = param.User;
                                            outputxml.UpdateDate = dtNow;
                                            _dbContext.Entry(outputxml).State = EntityState.Modified;
                                            outputXmlTransactionNos.Add(tran.OutputXmlTransactionNo);
                                        }

                                        tran.OutputXmlTransactionNo = null;
                                    }
                                }
                                tran.Isactive = 0;
                                tran.CancelBilling = 0;
                                tran.UpdateBy = param.User;
                                tran.UpdateDate = dtNow;
                                _dbContext.Entry(tran).State = EntityState.Modified;
                            }
                        }
                        else if (request.RequestAction == Variable.RequestActionCode_Undelete)
                        {
                            var requestItem = _dbContext.requestItem.Where(t => t.RequestId == request.Id).ToList();
                            var tranNos = requestItem.Select(t => t.TransactionNo).Distinct().ToList();
                            var transactions = _dbContext.transactionDescription.Where(t => tranNos.Contains(t.TransactionNo)).ToList();
                            foreach (var tran in transactions)
                            {
                                tran.Isactive = 1;
                                tran.CancelBilling = null;
                                tran.UpdateBy = param.User;
                                tran.UpdateDate = dtNow;
                                _dbContext.Entry(tran).State = EntityState.Modified;
                            }


                        }
                        else if (request.RequestAction == Variable.RequestActionCode_ReSignNewTrans)
                        {
                            
                        }
                        else if (request.RequestAction == Variable.RequestActionCode_ReSignNewCert)
                        {

                        }
                        //else if (request.RequestAction == "unzip")
                        //{
                        //    var requestItem = _dbContext.requestItem.Where(t => t.RequestId == request.Id).ToList();
                        //    var tranNos = requestItem.Select(t => t.TransactionNo).Distinct().ToList();
                        //    var transactions = _dbContext.transactionDescription.Where(t => tranNos.Contains(t.TransactionNo)).ToList();
                        //    foreach (var tran in transactions)
                        //    {
                        //        tran.XmlCompressStatus = "Waiting";
                        //        if(tran.OutputXmlTransactionNo != null)
                        //        {
                        //            var transactions2 = _dbContext.transactionDescription.Where(t => t.OutputXmlTransactionNo == tran.OutputXmlTransactionNo && t.TransactionNo != tran.TransactionNo).ToList();
                        //            foreach(var tran2 in transactions2)
                        //            {
                        //                tran2.XmlCompressStatus = "Waiting";
                        //                tran2.OutputXmlTransactionNo = null;
                        //                tran2.UpdateBy = param.User;
                        //                tran2.UpdateDate = dtNow;
                        //                _dbContext.Entry(tran2).State = EntityState.Modified;
                        //            }
                        //            var outputxml = _dbContext.outputSearchXmlZip.Where(t => t.OutputSearchXmlZipNo == int.Parse(tran.OutputXmlTransactionNo)).FirstOrDefault();
                        //            if(outputxml!= null)
                        //            {
                        //                outputxml.Isactive = 0;
                        //                outputxml.UpdateBy = param.User;
                        //                outputxml.UpdateDate = dtNow;
                        //                _dbContext.Entry(outputxml).State = EntityState.Modified;
                        //                outputXmlTransactionNos.Add(tran.OutputXmlTransactionNo);
                        //            }

                        //            tran.OutputXmlTransactionNo = null;
                        //        }
                        //        tran.UpdateBy = param.User;
                        //        tran.UpdateDate = dtNow;
                        //        _dbContext.Entry(tran).State = EntityState.Modified;
                        //    }
                        //}
                    }
                    else
                    {
                        request.StatusCode = Variable.RequestStatusCode_Reject;
                        request.OfficerBy = param.User;
                    }
                    request.UpdateDate = dtNow;
                    request.UpdateBy = param.User;

                    var history = new RequestHistory();
                    history.Id = Guid.NewGuid();
                    history.RequestId = param.RequestId;
                    history.Action = param.Action.Contains("approve") ? "Approve" : "Reject";
                    history.Reason = param.Reason;
                    history.CreateDate = dtNow;
                    history.CreateBy = param.User;
                    history.UpdateDate = dtNow;
                    history.UpdateBy = param.User;
                    _dbContext.requestHistory.Add(history);

                    _dbContext.SaveChanges();
                    resp.STATUS = true;
                    resp.MESSAGE = "success.";
                    resp.OUTPUT_DATA = outputXmlTransactionNos;
                }
            }
            catch (Exception ex)
            {
                resp.STATUS = false;
                resp.MESSAGE = "Action faild.";
                resp.INNER_EXCEPTION = ex.Message;
            }
            return resp;
        }

        public string GetRunningNo(string company)
        {
            try
            {
                string sql = sqlFactory.GET_REQUEST_RUNNING();

                var resultData = base.SearchBySql(sql);
                if (resultData.StatusOnDb)
                {
                    string ret = "REQ" + DateTime.Now.ToString("yyyy", new System.Globalization.CultureInfo("en-US")) + company;
                    string running = resultData.ResultOnDb.Rows[0]["number"].ToString();
                    while (running.Length < 4)
                    {
                        running = "0" + running;
                    }
                    return ret + running;
                }
                else
                {
                    throw new Exception(resultData.MessageOnDb);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

    }
}
