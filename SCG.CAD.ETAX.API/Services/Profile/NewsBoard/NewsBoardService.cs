namespace SCG.CAD.ETAX.API.Services
{
    public class NewsBoardService : DatabaseExecuteController
    {

        readonly DatabaseContext _dbContext = new();

        public DateTime dtNow = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd'" + "T" + "'HH:mm:ss.fff"));
        public Response GET_LIST()
        {
            Response resp = new Response();
            try
            {
                var getList = _dbContext.newsBoard.ToList();

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
                resp.INNER_EXCEPTION = ex.Message.ToString();
            }
            return resp;
        }

        public Response GET_DETAIL(int NewsBoardNo)
        {
            Response resp = new Response();
            try
            {
                var getList = _dbContext.newsBoard.Where(x => x.NewsBoardNo == NewsBoardNo).ToList();

                if (getList.Count > 0)
                {
                    resp.STATUS = true;
                    resp.MESSAGE = "Get data from ID '" + NewsBoardNo + "' success. ";
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
                resp.INNER_EXCEPTION = ex.Message.ToString();

                DateTime.Now.ToString("ddMMyyyy");
            }
            return resp;
        }

        public Response INSERT(NewsBoard param)
        {
            Response resp = new Response();

            try
            {
                using (_dbContext)
                {
                    param.CreateDate = dtNow;
                    param.UpdateDate = dtNow;

                    _dbContext.newsBoard.Add(param);
                    _dbContext.SaveChanges();

                    resp.STATUS = true;
                    resp.MESSAGE = "Insert success.";
                }
            }
            catch (Exception ex)
            {
                resp.STATUS = false;
                resp.MESSAGE = "Insert faild.";
                resp.INNER_EXCEPTION = ex.Message.ToString();
            }
            return resp;
        }

        public Response UPDATE(NewsBoard param)
        {
            Response resp = new Response();

            try
            {
                using (_dbContext)
                {
                    var update = _dbContext.newsBoard.Where(x => x.NewsBoardNo == param.NewsBoardNo).FirstOrDefault();

                    if (update != null)
                    {
                        update.NewsBoardHeader = param.NewsBoardHeader;
                        update.NewsBoardBody = param.NewsBoardBody;
                        update.NewsBoardFooter = param.NewsBoardFooter;
                        update.NewsBoardSeq = param.NewsBoardSeq;
                        update.NewsBoardStart = param.NewsBoardStart;
                        update.NewsBoardEnd = param.NewsBoardEnd;
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
                resp.INNER_EXCEPTION = ex.Message.ToString();
            }
            return resp;
        }

        public Response DELETE(NewsBoard param)
        {
            Response resp = new Response();

            try
            {
                using (_dbContext)
                {
                    var delete = _dbContext.newsBoard.Find(param.NewsBoardNo);

                    if (delete != null)
                    {
                        _dbContext.newsBoard.Remove(delete);
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
                resp.INNER_EXCEPTION = ex.Message.ToString();
            }
            return resp;
        }

    }
}
