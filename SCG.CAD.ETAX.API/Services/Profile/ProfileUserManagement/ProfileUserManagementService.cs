using System.Text;

namespace SCG.CAD.ETAX.API.Services
{
    public class ProfileUserManagementService
    {
        readonly DatabaseContext _dbContext = new();

        public DateTime dtNow = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd'" + "T" + "'HH:mm:ss.fff"));

        public Response GET_LIST()
        {
            Response resp = new Response();
            try
            {
                var getList = _dbContext.profileUserManagement.ToList();

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
                var getList = _dbContext.profileUserManagement.Where(x => x.UserNo == id).ToList();

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
        public Response GET_DETAIL_BY_EXTERNALID(string id)
        {
            Response resp = new Response();

            try
            {
                var data = _dbContext.profileUserManagement.Where(x => x.ExternalId == id).FirstOrDefault();

                if (data != null)
                {
                    resp.STATUS = true;
                    resp.MESSAGE = "Get data success. ";
                    resp.OUTPUT_DATA = data;
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
        public Response GET_DETAIL_BY_EMAIL_EXTERNALID2(string email)
        {
            Response resp = new Response();

            try
            {
                var data = _dbContext.profileUserManagement.Where(x => x.ExternalId2 == email.ToLower() || x.UserEmail == email.ToLower()).FirstOrDefault();

                if (data != null)
                {
                    resp.STATUS = true;
                    resp.MESSAGE = "Get data success. ";
                    resp.OUTPUT_DATA = data;
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

        public Response INSERT(ProfileUserManagement param)
        {
            Response resp = new Response();
            try
            {
                using (_dbContext)
                {
                    param.DomainName = "@" + param.UserEmail.Substring(param.UserEmail.IndexOf('@') + 1);
                    param.PasswordRegister = dtNow;
                    param.PasswordExpire = dtNow.AddYears(1);
                    param.UserPassword = Convert.ToBase64String(Encoding.UTF8.GetBytes(param.UserPassword));
                    param.CreateDate = dtNow;
                    param.UpdateDate = dtNow;

                    _dbContext.profileUserManagement.Add(param);
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

        public Response UPDATE(ProfileUserManagement param)
        {
            Response resp = new Response();
            try
            {
                using (_dbContext)
                {
                    var update = _dbContext.profileUserManagement.Where(x => x.UserNo == param.UserNo).FirstOrDefault();

                    if (update != null)
                    {
                        //update.UserEmail = param.UserEmail;
                        //update.UserPassword = param.UserPassword;
                        //update.DomainName = param.DomainName;
                        update.FirstName = param.FirstName;
                        update.LastName = param.LastName;
                        update.GroupId = param.GroupId;
                        update.LevelId = param.LevelId;
                        //update.PasswordRegister = param.PasswordRegister;
                        //update.PasswordExpire = param.PasswordExpire;
                        //update.AttempLogin = param.AttempLogin;
                        //update.AttempLast = param.AttempLast;
                        update.AccountStatus = param.AccountStatus;
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
        public Response UPDATE_EXTERNALID(ProfileUserManagement param)
        {
            Response resp = new Response();
            try
            {
                using (_dbContext)
                {
                    var update = _dbContext.profileUserManagement.Where(x => x.UserNo == param.UserNo).FirstOrDefault();

                    if (update != null)
                    {
                        update.ExternalId = param.ExternalId;
                        update.ExternalId2 = param.ExternalId2;

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

        public Response DELETE(ProfileUserManagement param)
        {
            Response resp = new Response();
            try
            {
                using (_dbContext)
                {
                    var delete = _dbContext.profileUserManagement.Find(param.UserNo);

                    if (delete != null)
                    {
                        _dbContext.profileUserManagement.Remove(delete);
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
