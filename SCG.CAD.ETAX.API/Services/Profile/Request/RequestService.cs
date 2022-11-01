namespace SCG.CAD.ETAX.API.Services
{
    public class RequestService
    {
        readonly DatabaseContext _dbContext = new();
        public DateTime dtNow = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd'" + "T" + "'HH:mm:ss.fff"));
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
                using (_dbContext)
                {
                    var companys = param.RequestCartList.Select(t => t.CompanyCode).Distinct().ToList();
                    foreach(var comCode in companys)
                    {
                        var dataList = param.RequestCartList.Where(t => t.CompanyCode == comCode).ToList();
                        var request = new Request();
                        request.Id = Guid.NewGuid();
                        request.RequestNo = GetRunningNo();
                        request.RequestAction = param.Action;
                        request.StatusCode = "wait_manager";
                        request.CompanyCode = comCode;
                        request.Manager = param.Manager;
                        request.CreateDate = dtNow;
                        request.CreateBy = param.UserBy;
                        request.UpdateDate = dtNow;
                        request.UpdateBy = param.UserBy;
                        _dbContext.request.Add(request);
                        var history = new RequestHistory();
                        history.Id = Guid.NewGuid();
                        history.RequestId = request.Id;
                        history.Action = "submit";
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

        private string GetRunningNo()
        {
            return DateTime.Now.ToString("yyyyMMddHHmmss");
        }

    }
}
