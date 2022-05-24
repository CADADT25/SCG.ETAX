namespace SCG.CAD.ETAX.API.Services
{
    public class ConfigMftsEmailSettingService
    {
        readonly DatabaseContext _dbContext = new();

        public DateTime dtNow = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd'" + "T" + "'HH:mm:ss.fff"));

        public Response GET_LIST()
        {
            Response resp = new Response();
            try
            {
                var getList = _dbContext.configMftsEmailSetting.ToList();

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
                var getList = _dbContext.configMftsEmailSetting.Where(x => x.ConfigMftsEmailSettingNo == id).ToList();

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

        public Response INSERT(ConfigMftsEmailSetting param)
        {
            Response resp = new Response();
            try
            {
                using (_dbContext)
                {
                    param.CreateDate = dtNow;
                    param.UpdateDate = dtNow;

                    _dbContext.configMftsEmailSetting.Add(param);
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

        public Response UPDATE(ConfigMftsEmailSetting param)
        {
            Response resp = new Response();
            try
            {
                using (_dbContext)
                {
                    var update = _dbContext.configMftsEmailSetting.Where(x => x.ConfigMftsEmailSettingNo == param.ConfigMftsEmailSettingNo).FirstOrDefault();

                    if (update != null)
                    {
                        update.ConfigMftsEmailSettingCompanyCode = param.ConfigMftsEmailSettingCompanyCode;
                        update.ConfigMftsEmailSettingOperation = param.ConfigMftsEmailSettingOperation;
                        update.ConfigMftsEmailSettingEmail = param.ConfigMftsEmailSettingEmail;
                        update.ConfigMftsEmailSettingEmailTemplate = param.ConfigMftsEmailSettingEmailTemplate;
                        update.ConfigMftsEmailSettingProtocol = param.ConfigMftsEmailSettingProtocol;
                        update.ConfigMftsEmailSettingHost = param.ConfigMftsEmailSettingHost;
                        update.ConfigMftsEmailSettingPort = param.ConfigMftsEmailSettingPort;
                        update.ConfigMftsEmailSettingUsername = param.ConfigMftsEmailSettingUsername;
                        update.ConfigMftsEmailSettingPassword = param.ConfigMftsEmailSettingPassword;
                        update.ConfigMftsEmailSettingOneTime += param.ConfigMftsEmailSettingOneTime;
                        update.ConfigMftsEmailSettingAnyTime = param.ConfigMftsEmailSettingAnyTime;
                        update.ConfigMftsEmailSettingApiKey = param.ConfigMftsEmailSettingApiKey;
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

        public Response DELETE(ConfigMftsEmailSetting param)
        {
            Response resp = new Response();
            try
            {
                using (_dbContext)
                {
                    var delete = _dbContext.configMftsEmailSetting.Find(param.ConfigMftsEmailSettingNo);

                    if (delete != null)
                    {
                        _dbContext.configMftsEmailSetting.Remove(delete);
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

        public Response UPDATE_ONETIME(ConfigMftsEmailSetting param)
        {
            Response resp = new Response();
            try
            {
                using (_dbContext)
                {
                    var update = _dbContext.configMftsEmailSetting.Where(x => x.ConfigMftsEmailSettingNo == param.ConfigMftsEmailSettingNo).FirstOrDefault();

                    if (update != null)
                    {
                        var setNewOnetime = "";

                        setNewOnetime += "|" + param.ConfigMftsEmailSettingOneTime;

                        var findFirstIndex = setNewOnetime.Substring(0, 1);

                        if (findFirstIndex == "|")
                        {
                            setNewOnetime = setNewOnetime.Substring(1, 5);
                        }

                        update.ConfigMftsEmailSettingAnyTime = setNewOnetime;


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

        public Response UPDATE_ANYTIME(ConfigMftsEmailSetting param)
        {
            Response resp = new Response();
            try
            {
                using (_dbContext)
                {
                    var update = _dbContext.configMftsEmailSetting.Where(x => x.ConfigMftsEmailSettingNo == param.ConfigMftsEmailSettingNo).FirstOrDefault();

                    if (update != null)
                    {

                        var setNewAnytime = "";

                        setNewAnytime += "|" + param.ConfigMftsEmailSettingAnyTime;

                        var findFirstIndex = setNewAnytime.Substring(0, 1);

                        if (findFirstIndex == "|")
                        {
                            setNewAnytime = setNewAnytime.Substring(1, 5);
                        }

                        update.ConfigMftsEmailSettingAnyTime = setNewAnytime;


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


        public Response DELETE_ONETIME(DeleteOnetime param)
        {
            Response resp = new Response();
            try
            {
                using (_dbContext)
                {
                    var update = _dbContext.configMftsEmailSetting.Where(x => x.ConfigMftsEmailSettingNo == param.pk).FirstOrDefault();

                    if (update != null)
                    {

                        var getOnetime = update.ConfigMftsEmailSettingOneTime;

                        var splitOneTime = getOnetime.Split("|");

                        var setNewOneTime = "";

                        for (int i = 0; i < splitOneTime.Length; i++)
                        {
                            if (i != param.position)
                            {
                                setNewOneTime += "|" + splitOneTime[i];
                            }
                        }

                        update.ConfigMftsEmailSettingOneTime = setNewOneTime.Substring(1);

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


        public Response DELETE_ANYTIME(DeleteOnetime param)
        {
            Response resp = new Response();
            try
            {
                using (_dbContext)
                {
                    var update = _dbContext.configMftsEmailSetting.Where(x => x.ConfigMftsEmailSettingNo == param.pk).FirstOrDefault();

                    if (update != null)
                    {

                        var getOnetime = update.ConfigMftsEmailSettingAnyTime;

                        var splitAnyTime = getOnetime.Split("|");

                        for (int i = 0; i < splitAnyTime.Length; i++)
                        {
                            if (i != param.position)
                            {
                                update.ConfigMftsEmailSettingOneTime += splitAnyTime[i];
                            }
                        }

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
    }
}
