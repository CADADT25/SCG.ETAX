namespace SCG.CAD.ETAX.API.Services
{
    public class ConfigMftsCompressXmlSettingService
    {
        readonly DatabaseContext _dbContext = new();

        public DateTime dtNow = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd'" + "T" + "'HH:mm:ss.fff"));

        public Response GET_LIST()
        {
            Response resp = new Response();
            try
            {
                var getList = _dbContext.configMftsCompressXmlSetting.ToList();

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
                var getList = _dbContext.configMftsCompressXmlSetting.Where(x => x.ConfigMftsCompressXmlSettingNo == id).ToList();

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

        public Response INSERT(ConfigMftsCompressXmlSetting param)
        {
            Response resp = new Response();
            try
            {
                using (_dbContext)
                {
                    param.CreateDate = dtNow;
                    param.UpdateDate = dtNow;

                    _dbContext.configMftsCompressXmlSetting.Add(param);
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

        public Response UPDATE(ConfigMftsCompressXmlSetting param)
        {
            Response resp = new Response();
            try
            {
                using (_dbContext)
                {
                    var update = _dbContext.configMftsCompressXmlSetting.Where(x => x.ConfigMftsCompressXmlSettingNo == param.ConfigMftsCompressXmlSettingNo).FirstOrDefault();

                    if (update != null)
                    {
                        update.ConfigMftsCompressXmlSettingCompanyCode = param.ConfigMftsCompressXmlSettingCompanyCode;
                        update.ConfigMftsCompressXmlSettingSourceName = param.ConfigMftsCompressXmlSettingSourceName;
                        update.ConfigMftsCompressXmlSettingCompressType = param.ConfigMftsCompressXmlSettingCompressType;
                        update.ConfigMftsCompressXmlSettingOutputFolder = param.ConfigMftsCompressXmlSettingOutputFolder;
                        update.ConfigMftsCompressXmlSettingHost = param.ConfigMftsCompressXmlSettingHost;
                        update.ConfigMftsCompressXmlSettingPort = param.ConfigMftsCompressXmlSettingPort;
                        update.ConfigMftsCompressXmlSettingUsername = param.ConfigMftsCompressXmlSettingUsername;
                        update.ConfigMftsCompressXmlSettingPassword = param.ConfigMftsCompressXmlSettingPassword;
                       
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

        public Response DELETE(ConfigMftsCompressXmlSetting param)
        {
            Response resp = new Response();
            try
            {
                using (_dbContext)
                {
                    var delete = _dbContext.configMftsCompressXmlSetting.Find(param.ConfigMftsCompressXmlSettingNo);

                    if (delete != null)
                    {
                        _dbContext.configMftsCompressXmlSetting.Remove(delete);
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
