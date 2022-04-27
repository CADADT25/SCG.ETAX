namespace SCG.CAD.ETAX.API.Services
{
    public class ConfigMftsIndexGenerationSettingOutputService
    {
        readonly DatabaseContext _dbContext = new();

        public DateTime dtNow = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd'" + "T" + "'HH:mm:ss.fff"));

        public Response GET_LIST()
        {
            Response resp = new Response();
            try
            {
                var getList = _dbContext.configMftsIndexGenerationSettingOutput.ToList();

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
                var getList = _dbContext.configMftsIndexGenerationSettingOutput.Where(x => x.ConfigMftsIndexGenerationSettingOutputNo == id).ToList();

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

        public Response INSERT(ConfigMftsIndexGenerationSettingOutput param)
        {
            Response resp = new Response();
            try
            {
                using (_dbContext)
                {
                    param.CreateDate = dtNow;
                    param.UpdateDate = dtNow;

                    _dbContext.configMftsIndexGenerationSettingOutput.Add(param);
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

        public Response UPDATE(ConfigMftsIndexGenerationSettingOutput param)
        {
            Response resp = new Response();
            try
            {
                using (_dbContext)
                {
                    var update = _dbContext.configMftsIndexGenerationSettingOutput.Where(x => x.ConfigMftsIndexGenerationSettingOutputNo == param.ConfigMftsIndexGenerationSettingOutputNo).FirstOrDefault();

                    if (update != null)
                    {
                        update.ConfigMftsIndexGenerationSettingOutputCompanyCode = param.ConfigMftsIndexGenerationSettingOutputCompanyCode;
                        update.ConfigMftsIndexGenerationSettingOutputSourceName = param.ConfigMftsIndexGenerationSettingOutputSourceName;
                        update.ConfigMftsIndexGenerationSettingOutputType = param.ConfigMftsIndexGenerationSettingOutputType;
                        update.ConfigMftsIndexGenerationSettingOutputFolder = param.ConfigMftsIndexGenerationSettingOutputFolder;
                        update.ConfigMftsIndexGenerationSettingOutputLogReceiveType = param.ConfigMftsIndexGenerationSettingOutputLogReceiveType;
                        update.ConfigMftsIndexGenerationSettingOutputLogReceiveFolder = param.ConfigMftsIndexGenerationSettingOutputLogReceiveFolder;
                        update.ConfigMftsIndexGenerationSettingOutputHost = param.ConfigMftsIndexGenerationSettingOutputHost;
                        update.ConfigMftsIndexGenerationSettingOutputPort = param.ConfigMftsIndexGenerationSettingOutputPort;
                        update.ConfigMftsIndexGenerationSettingOutputUsername = param.ConfigMftsIndexGenerationSettingOutputUsername;
                        update.ConfigMftsIndexGenerationSettingOutputPassword = param.ConfigMftsIndexGenerationSettingOutputPassword;


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

        public Response DELETE(ConfigMftsIndexGenerationSettingOutput param)
        {
            Response resp = new Response();
            try
            {
                using (_dbContext)
                {
                    var delete = _dbContext.configMftsIndexGenerationSettingOutput.Find(param.ConfigMftsIndexGenerationSettingOutputNo);

                    if (delete != null)
                    {
                        _dbContext.configMftsIndexGenerationSettingOutput.Remove(delete);
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
