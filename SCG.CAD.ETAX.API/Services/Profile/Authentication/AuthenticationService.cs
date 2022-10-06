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
                string encodedStr = Convert.ToBase64String(Encoding.UTF8.GetBytes(Password));
                //string inputStr = Encoding.UTF8.GetString(Convert.FromBase64String(encodedStr));


                var getAccount = _dbContext.profileUserManagement.Where(x => x.UserEmail == Username).ToList();

                if (getAccount.Count > 0)
                {
                    var getList = _dbContext.profileUserManagement.Where(x => x.UserEmail == Username && x.UserPassword == encodedStr).ToList();

                    if (getList.Count() > 0)
                    {
                        resp.STATUS = true;
                        resp.MESSAGE = "Get data from email '" + Username + "' success. ";
                        resp.OUTPUT_DATA = getList;
                    }
                    else
                    {
                        resp.STATUS = false;
                        resp.ERROR_MESSAGE = "Invalid password";
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
            try
            {
                var test = _dbContext.profileUserManagement.ToList();
                var profileuser = _dbContext.profileUserManagement.FirstOrDefault(x => x.UserEmail == Username.Trim());
                if (profileuser != null)
                {
                    var menuid = _dbContext.profileUserGroup
                        .Where(x => profileuser.GroupId.Contains(x.ProfileUserGroupNo.ToString()))
                        .Select(x => x.ProfileControlMenu)
                        .ToList();
                    if (menuid.Count > 0)
                    {
                        foreach(var item in menuid)
                        {
                            if (!string.IsNullOrEmpty(item))
                            {
                                menu = _dbContext.configControlMenu
                                    .Where(x => item.Contains(x.ConfigControlMenuNo.ToString()))
                                    .ToList();
                                if (menu.Count > 0)
                                {
                                    controlMenu.AddRange(menu);
                                }
                            }
                        }

                        if(controlMenu.Count > 0)
                        {
                            controlMenu = controlMenu.Distinct()
                                .OrderBy(x=> x.ConfigControlMenuNo).ToList();

                            resp.STATUS = true;
                            resp.OUTPUT_DATA = controlMenu;
                        }
                        else
                        {
                            resp.STATUS = false;
                            resp.ERROR_MESSAGE = "User not found menu";
                        }

                    }
                    else
                    {
                        resp.STATUS = false;
                        resp.ERROR_MESSAGE = "User not found group";
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
    }
}
