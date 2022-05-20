namespace SCG.CAD.ETAX.API.Services
{
    public class configGlobalCategoryService
    {
        readonly DatabaseContext _dbContext = new();

        public DateTime dtNow = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd'" + "T" + "'HH:mm:ss.fff"));
        public Response GET_LIST()
        {
            Response resp = new Response();
            try
            {
                var getList = _dbContext.configGlobalCategory.OrderBy(x => x.ConfigGlobalCategoryName).ToList();

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
                var getList = _dbContext.configGlobalCategory.Where(x => x.ConfigGlobalCategoryNo == id).ToList();

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

        public Response INSERT(ConfigGlobalCategory param)
        {
            Response resp = new Response();
            try
            {
                using (_dbContext)
                {
                    var getDuplicate = _dbContext.configGlobalCategory.Where(x => x.ConfigGlobalCategoryName == param.ConfigGlobalCategoryName).ToList();

                    if (getDuplicate.Count > 0)
                    {
                        resp.STATUS = false;
                        resp.ERROR_MESSAGE = "Can't insert because category name duplicate.";
                    }
                    else
                    {

                        param.ConfigGlobalCategoryName = param.ConfigGlobalCategoryName.ToUpper();

                        param.CreateDate = dtNow;
                        param.UpdateDate = dtNow;

                        _dbContext.configGlobalCategory.Add(param);
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

        public Response UPDATE(ConfigGlobalCategory param)
        {
            Response resp = new Response();
            try
            {
                using (_dbContext)
                {
                    var update = _dbContext.configGlobalCategory.Where(x => x.ConfigGlobalCategoryNo == param.ConfigGlobalCategoryNo).FirstOrDefault();

                    if (update != null)
                    {
                        //update.ConfigGlobalCategoryName = param.ConfigGlobalCategoryName.ToUpper();
                        update.ConfigGlobalCategoryDescription = param.ConfigGlobalCategoryDescription;

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

        public Response DELETE(ConfigGlobalCategory param)
        {
            Response resp = new Response();
            try
            {
                using (_dbContext)
                {
                    var delete = _dbContext.configGlobalCategory.Find(param.ConfigGlobalCategoryNo);

                    if (delete != null)
                    {
                        _dbContext.configGlobalCategory.Remove(delete);
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
