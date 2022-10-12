
namespace SCG.CAD.ETAX.API.Services
{
    public class ConfigFunctionService
    {
        readonly DatabaseContext _dbContext = new();

        public DateTime dtNow = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd'" + "T" + "'HH:mm:ss.fff"));
        public Response GET_LIST()
        {
            Response resp = new Response();
            try
            {
                var getList = _dbContext.configFunction.ToList();

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
                resp.INNER_EXCEPTION = ex.InnerException.ToString();
            }
            return resp;
        }

        public Response GET_DETAIL(int id)
        {
            Response resp = new Response();

            try
            {
                var getList = _dbContext.configFunction.Where(x => x.ConfigFunctionNo == id).ToList();

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
                resp.INNER_EXCEPTION = ex.InnerException.ToString();
            }
            return resp;
        }

        public Response INSERT(ConfigFunction param)
        {
            Response resp = new Response();
            try
            {
                using (_dbContext)
                {
                    var getDuplicate = _dbContext.configFunction.ToList();

                    if (getDuplicate.Count > 0)
                    {
                        resp.STATUS = false;
                        resp.ERROR_MESSAGE = "Can't insert because data is duplicate.";
                    }
                    else
                    {

                        //param.ConfigControlFunctionName = param.ConfigControlFunctionName.ToUpper();

                        param.CreateDate = dtNow;
                        param.UpdateDate = dtNow;

                        _dbContext.configFunction.Add(param);
                        _dbContext.SaveChanges();


                        resp.STATUS = true;
                        resp.MESSAGE = "Insert success.";
                    }
                }
                resp.STATUS = true;
                resp.MESSAGE = "Insert success.";
            }
            catch (Exception ex)
            {
                resp.STATUS = false;
                resp.ERROR_MESSAGE = "Insert faild.";
                resp.INNER_EXCEPTION = ex.InnerException.ToString();
            }
            return resp;
        }

        public Response UPDATE(ConfigFunction param)
        {
            Response resp = new Response();
            try
            {
                using (_dbContext)
                {
                    var update = _dbContext.configFunction.Where(x => x.ConfigFunctionNo == param.ConfigFunctionNo).FirstOrDefault();

                    if (update != null)
                    {


                        update.UpdateBy = param.UpdateBy;
                        update.UpdateDate = dtNow;

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
                resp.INNER_EXCEPTION = ex.InnerException.ToString();
            }
            return resp;
        }

        public Response DELETE(ConfigFunction param)
        {
            Response resp = new Response();
            try
            {
                using (_dbContext)
                {
                    var delete = _dbContext.configFunction.Find(param.ConfigFunctionNo);

                    if (delete != null)
                    {
                        _dbContext.configFunction.Remove(delete);
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
                resp.INNER_EXCEPTION = ex.InnerException.ToString();
            }
            return resp;
        }
    }
}
