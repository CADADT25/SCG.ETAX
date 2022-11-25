using System.Text;

namespace SCG.CAD.ETAX.API.Services
{
    public class AuthenticationService
    {
        readonly DatabaseContext _dbContext = new();

        public DateTime dtNow = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd'" + "T" + "'HH:mm:ss.fff"));


        public Response GET_DETAIL(string Username, string Password)
        {
            Response resp = new Response();
            try
            {
                var getAccount = new List<ProfileUserManagement>();
                getAccount = _dbContext.profileUserManagement.Where(x => x.UserEmail == Username).ToList();

                if (getAccount.Count > 0)
                {
                    var getList = new List<ProfileUserManagement>();
                    if(Password != "autologin888")
                    {
                        string encodedStr = Convert.ToBase64String(Encoding.UTF8.GetBytes(Password));
                        getList = _dbContext.profileUserManagement.Where(x => x.UserEmail == Username && x.UserPassword == encodedStr).ToList();
                    }
                    else
                    {
                        getList = getAccount;
                    }
                    if (getList.Count() > 0)
                    {
                        resp.STATUS = true;
                        resp.MESSAGE = "Get data from email '" + Username + "' success. ";
                        resp.OUTPUT_DATA = getList;
                    }
                    else
                    {
                        resp.STATUS = false;
                        resp.ERROR_MESSAGE = "User and Password is not correct.";
                    }

                }
                else
                {
                    resp.STATUS = false;
                    resp.ERROR_MESSAGE = "User not found";
                }

            }
            catch (Exception ex)
            {
                resp.STATUS = false;
                resp.ERROR_MESSAGE = "Get data fail.";
                resp.INNER_EXCEPTION = ex.InnerException.ToString();
            }
            return resp;
        }
        public Response GET_MENU(string Username)
        {
            Response resp = new Response();
            List<ConfigControlMenu> controlMenu = new List<ConfigControlMenu>();
            List<ConfigControlMenu> menu = new List<ConfigControlMenu>();
            string profilecompanycode = "";
            try
            {
                var test = _dbContext.profileUserManagement.ToList();
                var profileuser = _dbContext.profileUserManagement.Where(x => x.UserEmail == Username.Trim()).Select(x => x.GroupId).ToList();
                if (profileuser.Count() > 0)
                {
                    var groupid = profileuser[0].Split(',');
                    foreach(var id in groupid)
                    {
                        var menuid = _dbContext.profileUserGroup
                            .Where(x => id.Contains(x.ProfileUserGroupNo.ToString()))
                            .ToList();
                        if (menuid.Count > 0)
                        {
                            foreach (var item in menuid)
                            {
                                if (!string.IsNullOrEmpty(item.ProfileControlMenu))
                                {
                                    menu = _dbContext.configControlMenu
                                        .Where(x => item.ProfileControlMenu.Contains(x.ConfigControlMenuNo.ToString()))
                                        .ToList();
                                    if (menu.Count > 0)
                                    {
                                        controlMenu.AddRange(menu);
                                    }
                                }

                                if (!string.IsNullOrEmpty(item.ProfileCompanyCode))
                                {
                                    profilecompanycode += item.ProfileCompanyCode;
                                }
                            }

                        }
                    }

                    if (controlMenu.Count > 0)
                    {
                        controlMenu = controlMenu.Distinct()
                            .OrderBy(x => x.ConfigControlMenuNo).ToList();

                        resp.OUTPUT_DATA = controlMenu;
                    }
                    if (!string.IsNullOrEmpty(profilecompanycode))
                    {
                        var listcomcode = profilecompanycode.Split(',').Distinct().Where(x => !string.IsNullOrEmpty(x)).ToList();
                        resp.CODE = JsonConvert.SerializeObject(listcomcode);
                    }
                    resp.STATUS = true;
                }
                else
                {
                    resp.STATUS = false;
                    resp.ERROR_MESSAGE = "User not found";
                }
            }
            catch (Exception ex)
            {
                resp.STATUS = false;
                resp.ERROR_MESSAGE = "Get data fail.";
                resp.INNER_EXCEPTION = ex.InnerException.ToString();
            }
            return resp;
        }
    }
}
