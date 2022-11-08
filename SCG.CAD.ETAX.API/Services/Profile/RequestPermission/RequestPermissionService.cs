using DocumentFormat.OpenXml.Office2010.Excel;

namespace SCG.CAD.ETAX.API.Services
{
    public class RequestPermissionService
    {
        readonly DatabaseContext _dbContext = new();
        public DateTime dtNow = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd'" + "T" + "'HH:mm:ss.fff"));

        public Response GET_ROLES_AND_COMPANYS(string user)
        {
            Response resp = new Response();

            try
            {
                var profileuser = _dbContext.profileUserManagement.FirstOrDefault(x => x.UserEmail == user);
                var companyGroupList = _dbContext.profileUserGroup
                       .Where(x => profileuser.GroupId.Contains(x.ProfileUserGroupNo.ToString()))
                       .Select(x => x.profileCompanyCode)
                       .ToList();

                var data = new RequestPermissionDataModel();
                data.CompanyCodeList = companyGroupList;
                data.Level = profileuser.LevelId;
                resp.STATUS = true;
                resp.MESSAGE = "Get data success. ";
                resp.OUTPUT_DATA = data;
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
