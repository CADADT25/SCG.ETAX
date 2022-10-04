namespace SCG.CAD.ETAX.API.Services
{
    public class ProfileUserGroupService
    {


        readonly DatabaseContext _dbContext = new();

        public DateTime dtNow = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd'" + "T" + "'HH:mm:ss.fff"));

        public Response GET_LIST()
        {
            Response resp = new Response();
            try
            {
                var getList = _dbContext.profileUserGroup.ToList();

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

        public Response GET_DETAIL(int id)
        {
            Response resp = new Response();

            try
            {
                var getList = _dbContext.profileUserGroup.Where(x => x.ProfileUserGroupNo == id).ToList();

                if (getList.Count > 0)
                {
                    resp.STATUS = true;
                    resp.MESSAGE = "Get data from ID '" + id + "' success. ";
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

        public Response INSERT(ProfileUserGroup param)
        {
            Response resp = new Response();
            try
            {
                using (_dbContext)
                {

                    var getDuplicate = _dbContext.profileUserGroup.Where(x => x.ProfileUserGroupName == param.ProfileUserGroupName).ToList();

                    if (getDuplicate.Count > 0)
                    {
                        resp.STATUS = false;
                        resp.ERROR_MESSAGE = "Can't insert because data is duplicate.";
                    }
                    else
                    {
                        param.ProfileControlMenu = CheckMainMenu(param.ProfileControlMenu);
                        param.CreateDate = dtNow;
                        param.UpdateDate = dtNow;

                        _dbContext.profileUserGroup.Add(param);
                        _dbContext.SaveChanges();


                        resp.STATUS = true;
                        resp.MESSAGE = "Insert success.";
                    }
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

        public Response UPDATE(ProfileUserGroup param)
        {
            Response resp = new Response();
            try
            {
                using (_dbContext)
                {
                    var update = _dbContext.profileUserGroup.Where(x => x.ProfileUserGroupNo == param.ProfileUserGroupNo).FirstOrDefault();

                    if (update != null)
                    {
                        update.ProfileUserGroupName = param.ProfileUserGroupName;
                        update.ProfileUserGroupDescription = param.ProfileUserGroupDescription;
                        update.ProfileControlMenu = CheckMainMenu(param.ProfileControlMenu);
                        update.UpdateBy = param.UpdateBy;
                        update.UpdateDate = dtNow;
                        update.Isactive = param.Isactive;

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

        public Response DELETE(ProfileUserGroup param)
        {
            Response resp = new Response();
            try
            {
                using (_dbContext)
                {
                    var delete = _dbContext.profileUserGroup.Find(param.ProfileUserGroupNo);

                    if (delete != null)
                    {
                        _dbContext.profileUserGroup.Remove(delete);
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

        public string CheckMainMenu(string profileControlMenu)
        {
            string result = "";
            var listmenu = profileControlMenu.Split(",");
            List<string> newlistmenu = new List<string>();
            var checkmainmenu = _dbContext.configControlMenu.ToList();
            foreach (var item in listmenu)
            {
                var datamenu = checkmainmenu.FirstOrDefault(x => x.ConfigControlMenuNo == Convert.ToInt32(item));
                if (datamenu != null)
                {
                    newlistmenu.Add(item);
                    if (datamenu.ConfigControlMenuValue.Length > 1)
                    {
                        var mainmenu = checkmainmenu.FirstOrDefault(x => x.ConfigControlMenuValue.Equals(datamenu.ConfigControlMenuValue.Substring(0, 1)));
                        if (mainmenu != null)
                        {
                            newlistmenu.Add(mainmenu.ConfigControlMenuNo.ToString());
                        }
                    }
                }
            }

            if(newlistmenu.Count > 0)
            {
                newlistmenu = newlistmenu.Distinct().ToList();
                foreach(var item in newlistmenu)
                {
                    result = result + "," + item;
                }
                result = result.Substring(1);
            }
            return result;
        }
    }
}
