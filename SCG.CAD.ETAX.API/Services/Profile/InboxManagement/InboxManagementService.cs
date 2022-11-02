using SCG.CAD.ETAX.MODEL.etaxModel;

namespace SCG.CAD.ETAX.API.Services
{
    public class InboxManagementService
    {
        readonly DatabaseContext _dbContext = new();
        public DateTime dtNow = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd'" + "T" + "'HH:mm:ss.fff"));

        public Response SEARCH_TODO(InboxSearchModel search)
        {
            Response resp = new Response();
            var inboxs = new List<InboxModelData>();
            var users = new List<ProfileUserManagement>();
            try
            {
                // get userGroup com
                var companyGroupList = new List<string>();
                var requestList = _dbContext.request.Where(t =>
                                                        (t.Manager == search.EmailUser && t.ManagerAction == false && t.StatusCode == "wait_manager")
                                                        ||
                                                        (companyGroupList.Contains(t.CompanyCode) && t.ManagerAction == true && t.StatusCode == "wait_officer")
                                                    ).ToList();
                if (!string.IsNullOrEmpty(search.RequestNo))
                {
                    requestList = requestList.Where(t => t.RequestNo.Contains(search.RequestNo)).ToList();
                }
                if (search.RequestStatus != null)
                {
                    if (search.RequestStatus.Count() > 0)
                    {
                        requestList = requestList.Where(t => search.RequestStatus.Contains(t.StatusCode)).ToList();
                    }
                }
                if (search.RequestAction != null)
                {
                    if (search.RequestAction.Count() > 1)
                    {
                        requestList = requestList.Where(t => search.RequestAction.Contains(t.RequestAction)).ToList();
                    }
                }
                var managers = requestList.Where(t=>t.Manager != null).Select(t => t.Manager).Distinct().ToList();
                var requesters = requestList.Where(t=>t.CreateBy != null).Select(t => t.CreateBy).Distinct().ToList();
                if (managers.Count() > 0)
                {
                    users.AddRange(_dbContext.profileUserManagement.Where(t => managers.Contains(t.UserEmail)).ToList());
                }
                if (requesters.Count() > 0)
                {
                    users.AddRange(_dbContext.profileUserManagement.Where(t => requesters.Contains(t.UserEmail)).ToList());
                }

                foreach (var item in requestList)
                {
                    var requester = users.Where(t => t.UserEmail == item.CreateBy).FirstOrDefault();
                    var manager = users.Where(t => t.UserEmail == item.Manager).FirstOrDefault();
                    inboxs.Add(new InboxModelData()
                    {
                        RequestId = item.Id,
                        RequestAction = item.RequestAction,
                        RequestDate = item.CreateDate,
                        RequesterEmail = item.CreateBy,
                        RequesterName = requester.FirstName + " " + requester.LastName,
                        RequestNo = item.RequestNo,
                        CompanyCode = item.CompanyCode,
                        StatusCode = item.StatusCode,
                        ManagerEmail = item.Manager,
                        ManagerName = manager.FirstName + " " + manager.LastName
                    });
                }
                //if (getList.Count > 0)
                //{
                resp.STATUS = true;
                resp.MESSAGE = "Get list count '" + inboxs.Count + "' records. ";
                resp.OUTPUT_DATA = inboxs;
                //}
                //else
                //{
                //    resp.STATUS = false;
                //    resp.MESSAGE = "Data not found";
                //}

            }
            catch (Exception ex)
            {
                resp.STATUS = false;
                resp.MESSAGE = "Get data fail.";
                resp.INNER_EXCEPTION = ex.InnerException.ToString();
            }
            return resp;
        }
        
        public Response SEARCH_INPROGRESS(InboxSearchModel search)
        {
            Response resp = new Response();
            var inboxs = new List<InboxModelData>();
            var users = new List<ProfileUserManagement>();
            try
            {
                var companyGroupList = new List<string>();
                var requestList = _dbContext.request.Where(t =>
                                                        (t.Manager == search.EmailUser && t.StatusCode != "wait_manager" && t.StatusCode != "reject" && t.StatusCode != "compleate" && t.StatusCode != "cancel")
                                                        ||
                                                        (t.CreateBy == search.EmailUser && t.StatusCode != "reject" && t.StatusCode != "compleate" && t.StatusCode != "cancel")
                                                    ).ToList();
                if (!string.IsNullOrEmpty(search.RequestNo))
                {
                    requestList = requestList.Where(t => t.RequestNo.Contains(search.RequestNo)).ToList();
                }
                if (search.RequestStatus != null)
                {
                    if (search.RequestStatus.Count() > 0)
                    {
                        requestList = requestList.Where(t => search.RequestStatus.Contains(t.StatusCode)).ToList();
                    }
                }
                if (search.RequestAction != null)
                {
                    if (search.RequestAction.Count() > 1)
                    {
                        requestList = requestList.Where(t => search.RequestAction.Contains(t.RequestAction)).ToList();
                    }
                }
                var managers = requestList.Where(t=>t.Manager != null).Select(t => t.Manager).Distinct().ToList();
                var requesters = requestList.Where(t=>t.CreateBy != null).Select(t => t.CreateBy).Distinct().ToList();
                if (managers.Count() > 0)
                {
                    users.AddRange(_dbContext.profileUserManagement.Where(t => managers.Contains(t.UserEmail)).ToList());
                }
                if (requesters.Count() > 0)
                {
                    users.AddRange(_dbContext.profileUserManagement.Where(t => requesters.Contains(t.UserEmail)).ToList());
                }

                foreach (var item in requestList)
                {
                    var requester = users.Where(t => t.UserEmail == item.CreateBy).FirstOrDefault();
                    var manager = users.Where(t => t.UserEmail == item.Manager).FirstOrDefault();
                    inboxs.Add(new InboxModelData()
                    {
                        RequestId = item.Id,
                        RequestAction = item.RequestAction,
                        RequestDate = item.CreateDate,
                        RequesterEmail = item.CreateBy,
                        RequesterName = requester.FirstName + " " + requester.LastName,
                        RequestNo = item.RequestNo,
                        CompanyCode = item.CompanyCode,
                        StatusCode = item.StatusCode,
                        ManagerEmail = item.Manager,
                        ManagerName = manager.FirstName + " " + manager.LastName
                    });
                }
                //if (getList.Count > 0)
                //{
                resp.STATUS = true;
                resp.MESSAGE = "Get list count '" + inboxs.Count + "' records. ";
                resp.OUTPUT_DATA = inboxs;
                //}
                //else
                //{
                //    resp.STATUS = false;
                //    resp.MESSAGE = "Data not found";
                //}

            }
            catch (Exception ex)
            {
                resp.STATUS = false;
                resp.MESSAGE = "Get data fail.";
                resp.INNER_EXCEPTION = ex.InnerException.ToString();
            }
            return resp;
        }
        
        public Response SEARCH_ALL(InboxSearchModel search)
        {
            Response resp = new Response();
            var inboxs = new List<InboxModelData>();
            var users = new List<ProfileUserManagement>();
            try
            {
                // get userGroup com
                var companyGroupList = new List<string>();
                var requestList = _dbContext.request.Where(t =>
                                                        (t.Manager == search.EmailUser)
                                                        ||
                                                        (t.CreateBy == search.EmailUser)
                                                        ||
                                                        (companyGroupList.Contains(t.CompanyCode))
                                                    ).ToList();
                if (!string.IsNullOrEmpty(search.RequestNo))
                {
                    requestList = requestList.Where(t => t.RequestNo.Contains(search.RequestNo)).ToList();
                }
                if (search.RequestStatus != null)
                {
                    if (search.RequestStatus.Count() > 0)
                    {
                        requestList = requestList.Where(t => search.RequestStatus.Contains(t.StatusCode)).ToList();
                    }
                }
                if (search.RequestAction != null)
                {
                    if (search.RequestAction.Count() > 1)
                    {
                        requestList = requestList.Where(t => search.RequestAction.Contains(t.RequestAction)).ToList();
                    }
                }
                var managers = requestList.Where(t=>t.Manager != null).Select(t => t.Manager).Distinct().ToList();
                var requesters = requestList.Where(t=>t.CreateBy != null).Select(t => t.CreateBy).Distinct().ToList();
                if (managers.Count() > 0)
                {
                    users.AddRange(_dbContext.profileUserManagement.Where(t => managers.Contains(t.UserEmail)).ToList());
                }
                if (requesters.Count() > 0)
                {
                    users.AddRange(_dbContext.profileUserManagement.Where(t => requesters.Contains(t.UserEmail)).ToList());
                }

                foreach (var item in requestList)
                {
                    var requester = users.Where(t => t.UserEmail == item.CreateBy).FirstOrDefault();
                    var manager = users.Where(t => t.UserEmail == item.Manager).FirstOrDefault();
                    inboxs.Add(new InboxModelData()
                    {
                        RequestId = item.Id,
                        RequestAction = item.RequestAction,
                        RequestDate = item.CreateDate,
                        RequesterEmail = item.CreateBy,
                        RequesterName = requester.FirstName + " " + requester.LastName,
                        RequestNo = item.RequestNo,
                        CompanyCode = item.CompanyCode,
                        StatusCode = item.StatusCode,
                        ManagerEmail = item.Manager,
                        ManagerName = manager.FirstName + " " + manager.LastName
                    });
                }
                //if (getList.Count > 0)
                //{
                resp.STATUS = true;
                resp.MESSAGE = "Get list count '" + inboxs.Count + "' records. ";
                resp.OUTPUT_DATA = inboxs;
                //}
                //else
                //{
                //    resp.STATUS = false;
                //    resp.MESSAGE = "Data not found";
                //}

            }
            catch (Exception ex)
            {
                resp.STATUS = false;
                resp.MESSAGE = "Get data fail.";
                resp.INNER_EXCEPTION = ex.InnerException.ToString();
            }
            return resp;
        }


    }
}
