using DocumentFormat.OpenXml.ExtendedProperties;

namespace SCG.CAD.ETAX.API.Services
{
    public class RequestHistoryService
    {
        readonly DatabaseContext _dbContext = new();
        public DateTime dtNow = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd'" + "T" + "'HH:mm:ss.fff"));
        public Response GET_LIST()
        {
            Response resp = new Response();
            try
            {
                var getList = _dbContext.requestHistory.ToList();

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
        public Response GET_LIST(Guid requestId)
        {
            Response resp = new Response();
            var dataList = new List<RequestHistoryDataModel>();
            try
            {
                var getList = _dbContext.requestHistory.Where(t => t.RequestId == requestId).ToList();
                
                if (getList.Count > 0)
                {
                    var userEmails = getList.Select(t => t.CreateBy).Distinct().ToList();

                    var users = _dbContext.profileUserManagement.Where(t => userEmails.Contains(t.UserEmail)).ToList();

                    foreach(var item in getList)
                    {
                        var user = users.Where(t => t.UserEmail == item.CreateBy).FirstOrDefault();
                        dataList.Add(new RequestHistoryDataModel()
                        {
                            Id = item.Id,
                            RequestId = item.RequestId,
                            UserEmail = user != null ? user.UserEmail : "",
                            UserName = user != null ? user.FirstName + " " + user.LastName : "",
                            Action = item.Action,
                            CreateDate = item.CreateDate,
                            Reason = item.Reason
                        });
                    }

                    resp.STATUS = true;
                    resp.MESSAGE = "Get list count '" + dataList.Count + "' records. ";
                    resp.OUTPUT_DATA = dataList.OrderBy(t => t.CreateDate).ToList();
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

        public Response INSERT(RequestHistory param)
        {
            Response resp = new Response();
            try
            {
                using (_dbContext)
                {
                    param.CreateDate = dtNow;
                    param.UpdateDate = dtNow;

                    _dbContext.requestHistory.Add(param);
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

        public Response UPDATE(RequestHistory param)
        {
            Response resp = new Response();
            try
            {
                using (_dbContext)
                {
                    var update = _dbContext.requestHistory.Where(x => x.Id == param.Id).FirstOrDefault();

                    if (update != null)
                    {
                        update.RequestId = param.RequestId;
                        update.Action = param.Action;
                        update.Reason = param.Reason;
                        update.SendEmail = param.SendEmail;

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

        public Response DELETE(RequestHistory param)
        {
            Response resp = new Response();
            try
            {
                using (_dbContext)
                {
                    var delete = _dbContext.requestHistory.Find(param.Id);

                    if (delete != null)
                    {
                        _dbContext.requestHistory.Remove(delete);
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

    }
}
