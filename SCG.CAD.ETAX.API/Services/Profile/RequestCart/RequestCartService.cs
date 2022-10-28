using Microsoft.EntityFrameworkCore;

namespace SCG.CAD.ETAX.API.Services
{
    public class RequestCartService
    {
        readonly DatabaseContext _dbContext = new();
        public DateTime dtNow = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd'" + "T" + "'HH:mm:ss.fff"));
        public Response SEARCH(RequestCartSearchModel req)
        {
            Response resp = new Response();


            List<RequestCart> cart = new List<RequestCart>();

            DateTime getMinDate = new DateTime();
            DateTime getMaxDate = new DateTime();

            try
            {
                if (req != null)
                {
                    if (!string.IsNullOrEmpty(req.CreateBy))
                    {
                        cart = _dbContext.requestCart.Where(x => x.CreateBy.ToLower() == req.CreateBy.ToLower()).ToList();
                    }
                }

                resp.STATUS = true;
                resp.MESSAGE = "Get data success. ";
                resp.OUTPUT_DATA = cart;


            }
            catch (Exception ex)
            {
                resp.STATUS = false;
                resp.MESSAGE = "Get data fail.";
                resp.INNER_EXCEPTION = ex.InnerException.ToString();
            }
            return resp;
        }

        public Response GET_LIST()
        {
            Response resp = new Response();
            try
            {
                var getList = _dbContext.requestCart.ToList();

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

        public Response INSERT(List<RequestCart> paramList)
        {
            Response resp = new Response();
            try
            {
                using (_dbContext)
                {
                    var allDatas = _dbContext.requestCart.Where(x => x.CreateBy == paramList[0].CreateBy).ToList();
                    foreach (var param in paramList)
                    {
                        var update = allDatas.Where(x => x.TransactionNo == param.TransactionNo && x.CompanyCode == param.CompanyCode && x.CreateBy == param.CreateBy).FirstOrDefault();
                        if(update == null)
                        {
                            param.CreateDate = dtNow;
                            param.UpdateDate = dtNow;
                            _dbContext.requestCart.Add(param);
                        }
                    }

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

        public Response INSERT(RequestCart param)
        {
            Response resp = new Response();
            try
            {
                using (_dbContext)
                {
                    param.CreateDate = dtNow;
                    param.UpdateDate = dtNow;

                    _dbContext.requestCart.Add(param);
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

        public Response UPDATE(RequestCart param)
        {
            Response resp = new Response();
            try
            {
                using (_dbContext)
                {
                    var update = _dbContext.requestCart.Where(x => x.Id == param.Id).FirstOrDefault();

                    if (update != null)
                    {
                        update.TransactionNo = param.TransactionNo;
                        update.BillingNumber = param.BillingNumber;
                        update.CompanyCode = param.CompanyCode;

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

        public Response DELETE(RequestCart param)
        {
            Response resp = new Response();
            try
            {
                using (_dbContext)
                {
                    var delete = _dbContext.requestCart.Find(param.Id);

                    if (delete != null)
                    {
                        _dbContext.requestCart.Remove(delete);
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
