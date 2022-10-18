namespace SCG.CAD.ETAX.API.Services
{
    public class ConfigControlFunctionService
    {

        readonly DatabaseContext _dbContext = new();

        public DateTime dtNow = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd'" + "T" + "'HH:mm:ss.fff"));
        public Response GET_LIST()
        {
            Response resp = new Response();
            try
            {
                var getList = _dbContext.configControlFunction.ToList();

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
                var getList = _dbContext.configControlFunction.Where(x => x.ConfigControlFunctionNo == id).ToList();

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

        public Response INSERT(ConfigControlFunction param)
        {
            Response resp = new Response();
            try
            {
                //using (_dbContext)
                //{
                //    var getDuplicate = _dbContext.configControlFunction.ToList();

                //    if (getDuplicate.Count > 0)
                //    {
                //        resp.STATUS = false;
                //        resp.ERROR_MESSAGE = "Can't insert because data is duplicate.";
                //    }
                //    else
                //    {

                //        //param.ConfigControlFunctionName = param.ConfigControlFunctionName.ToUpper();

                //        param.CreateDate = dtNow;
                //        param.UpdateDate = dtNow;

                //        _dbContext.configControlFunction.Add(param);
                //        _dbContext.SaveChanges();


                //        resp.STATUS = true;
                //        resp.MESSAGE = "Insert success.";
                //    }
                //}
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

        public Response UPDATE(ConfigControlFunction param)
        {
            Response resp = new Response();
            try
            {
                using (_dbContext)
                {
                    var update = _dbContext.configControlFunction.Where(x => x.ConfigControlFunctionMenuNo == param.ConfigControlFunctionMenuNo && x.ConfigControlNo == param.ConfigControlNo).FirstOrDefault();

                    if (update != null)
                    {
                        update.ConfigControlFunctionRole = param.ConfigControlFunctionRole;
                        update.UpdateBy = param.UpdateBy;
                        update.UpdateDate = dtNow;
                        update.Isactive = param.Isactive;

                        resp.STATUS = true;
                        resp.MESSAGE = "Updated Success.";
                    }
                    else
                    {
                        _dbContext.configControlFunction.Add(param);
                        resp.STATUS = true;
                        resp.ERROR_MESSAGE = "Insert success.";
                    }
                    _dbContext.SaveChanges();
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

        public Response DELETE(ConfigControlFunction param)
        {
            Response resp = new Response();
            try
            {
                using (_dbContext)
                {
                    var delete = _dbContext.configControlFunction.Find(param.ConfigControlFunctionNo);

                    if (delete != null)
                    {
                        _dbContext.configControlFunction.Remove(delete);
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
