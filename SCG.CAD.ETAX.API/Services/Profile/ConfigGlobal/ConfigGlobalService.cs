namespace SCG.CAD.ETAX.API.Services
{
    public class ConfigGlobalService
    {
        readonly DatabaseContext _dbContext = new();

        public DateTime dtNow = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd'" + "T" + "'HH:mm:ss.fff"));
        public Response GET_LIST()
        {
            Response resp = new Response();
            try
            {
                var getList = _dbContext.configGlobal.OrderBy(x => x.ConfigGlobalCategoryName).ToList();

                if (getList.Count > 0)
                {

                    resp.STATUS = true;
                    resp.MESSAGE = "Get list count '" + getList.Count + "' records. ";
                    resp.OUTPUT_DATA = getList;
                }
                else
                {
                    resp.STATUS = false;
                    resp.ERROR_MESSAGE = "Data not found";
                }

            }
            catch (Exception ex)
            {
                resp.STATUS = false;
                resp.ERROR_MESSAGE = "Get data fail.";
                resp.INNER_EXCEPTION = ex.Message.ToString();
            }
            return resp;
        }

        public Response GET_DETAIL(int id)
        {
            Response resp = new Response();

            try
            {
                var getList = _dbContext.configGlobal.Where(x => x.ConfigGlobalNo == id).ToList();

                if (getList.Count > 0)
                {
                    resp.STATUS = true;
                    resp.MESSAGE = "Get data from ID '" + id + "' success. ";
                    resp.OUTPUT_DATA = getList;
                }
                else
                {
                    resp.STATUS = false;
                    resp.ERROR_MESSAGE = "Data not found";
                }

            }
            catch (Exception ex)
            {
                resp.STATUS = false;
                resp.ERROR_MESSAGE = "Get data fail.";
                resp.INNER_EXCEPTION = ex.Message.ToString();
            }
            return resp;
        }
        public Response GET_DETAIL_BY_NAME(string cate, string name)
        {
            Response resp = new Response();

            try
            {
                var data = _dbContext.configGlobal.Where(x => x.ConfigGlobalName == name && x.ConfigGlobalCategoryName == cate).FirstOrDefault();

                if (data != null)
                {
                    resp.STATUS = true;
                    resp.MESSAGE = "Get data success. ";
                    resp.OUTPUT_DATA = data;
                }
                else
                {
                    resp.STATUS = false;
                    resp.ERROR_MESSAGE = "Data not found";
                }

            }
            catch (Exception ex)
            {
                resp.STATUS = false;
                resp.ERROR_MESSAGE = "Get data fail.";
                resp.INNER_EXCEPTION = ex.Message.ToString();
            }
            return resp;
        }

        public Response INSERT(ConfigGlobal param)
        {
            Response resp = new Response();
            try
            {
                using (_dbContext)
                {
                    var getDuplicate = _dbContext.configGlobal.Where(x => x.ConfigGlobalName == param.ConfigGlobalName && x.ConfigGlobalCategoryName == param.ConfigGlobalCategoryName).ToList();

                    if (getDuplicate.Count > 0)
                    {
                        resp.STATUS = false;
                        resp.ERROR_MESSAGE = "Can't insert because data is duplicate.";
                    }
                    else
                    {

                        param.ConfigGlobalCategoryName = param.ConfigGlobalCategoryName.ToUpper();
                        param.ConfigGlobalName = param.ConfigGlobalName.ToUpper();

                        param.CreateDate = dtNow;
                        param.UpdateDate = dtNow;

                        _dbContext.configGlobal.Add(param);
                        _dbContext.SaveChanges();


                        resp.STATUS = true;
                        resp.MESSAGE = "Insert success.";
                    }
                }
            }
            catch (Exception ex)
            {
                resp.STATUS = false;
                resp.ERROR_MESSAGE = "Insert faild.";
                resp.INNER_EXCEPTION = ex.Message.ToString();
            }
            return resp;
        }

        public Response UPDATE(ConfigGlobal param)
        {
            Response resp = new Response();
            try
            {
                using (_dbContext)
                {
                    var update = _dbContext.configGlobal.Where(x => x.ConfigGlobalNo == param.ConfigGlobalNo).FirstOrDefault();

                    if (update != null)
                    {
                        //update.ConfigGlobalCategoryName = param.ConfigGlobalCategoryName;
                        //update.ConfigGlobalName = param.ConfigGlobalName;
                        update.ConfigGlobalValue = param.ConfigGlobalValue;
                        update.ConfigGlobalDescription = param.ConfigGlobalDescription;

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
                        resp.ERROR_MESSAGE = "Can't update because data not found.";
                    }
                }
            }
            catch (Exception ex)
            {
                resp.STATUS = false;
                resp.ERROR_MESSAGE = "Update faild.";
                resp.INNER_EXCEPTION = ex.Message.ToString();
            }
            return resp;
        }

        public Response DELETE(ConfigGlobal param)
        {
            Response resp = new Response();
            try
            {
                using (_dbContext)
                {
                    var delete = _dbContext.configGlobal.Find(param.ConfigGlobalNo);

                    if (delete != null)
                    {
                        _dbContext.configGlobal.Remove(delete);
                        _dbContext.SaveChanges();

                        resp.STATUS = true;
                        resp.MESSAGE = "Delete success.";
                    }
                    else
                    {
                        resp.STATUS = false;
                        resp.ERROR_MESSAGE = "Can't delete because data not found.";
                    }
                }
            }
            catch (Exception ex)
            {
                resp.STATUS = false;
                resp.ERROR_MESSAGE = "Delete faild.";
                resp.INNER_EXCEPTION = ex.Message.ToString();
            }
            return resp;
        }

    }
}
