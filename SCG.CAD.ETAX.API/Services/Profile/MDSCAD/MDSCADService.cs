using SCG.CAD.ETAX.UTILITY;

namespace SCG.CAD.ETAX.API.Services
{
    public class MDSCADService : DatabaseExecuteController
    {
        public MDSCADService() : base("MDS_CAD")
        {

        }
        readonly DatabaseContext _dbContext = new();
        sqlMDSCAD sqlFactory = new sqlMDSCAD();
        public Response GET_MANAGER_BY_USER(string requestNo)
        {
            Response resp = new Response();
            try
            {
                string sql = sqlFactory.GET_MANAGER_BY_USER(requestNo);

                var resultData = base.SearchBySql(sql);
                if (resultData.StatusOnDb)
                {

                    if (resultData.ResultOnDb.Rows.Count > 0)
                    {
                        var dataList = new List<EhrUserModel>();
                        dataList = UtilityHelper.ConvertDataTableToModel<EhrUserModel>(resultData.ResultOnDb);
                        var mdsUser = dataList.FirstOrDefault();
                        var localUser = _dbContext.profileUserManagement.Where(t => t.UserEmail.ToLower() == mdsUser.ManagerEmail).FirstOrDefault();
                        if (localUser != null)
                        {
                            mdsUser.IsUse = true;
                            mdsUser.ManagerEmail = mdsUser.ManagerEmail.ToLower();
                        }
                        else
                        {
                            mdsUser.IsUse = false;
                        }
                        resp.STATUS = true;
                        resp.OUTPUT_DATA = mdsUser;
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
                resp.INNER_EXCEPTION = ex.Message.ToString();
            }
            return resp;
        }


    }
}
