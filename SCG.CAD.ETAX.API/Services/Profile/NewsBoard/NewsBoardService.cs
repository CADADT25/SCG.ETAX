namespace SCG.CAD.ETAX.API.Services
{
    public class NewsBoardService : DatabaseExecuteController
    {

        readonly DatabaseContext _dbContext = new();
        public List<NewsBoard> GET_LIST()
        {
            List<NewsBoard> resp = new List<NewsBoard>();
            try
            {
                resp = _dbContext.newsBoard.ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return resp;
        }

        public List<NewsBoard> GET_DETAIL(int NewsBoardNo)
        {
            List<NewsBoard> resp = new List<NewsBoard>();
            try
            {
                resp = _dbContext.newsBoard.Where(x => x.NewsBoardNo == NewsBoardNo).ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
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
                resp.INNER_EXCEPTION = ex.InnerException.ToString();
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
                        update.UpdateDate = param.UpdateDate;
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
                resp.MESSAGE = "Update faild.";
                resp.INNER_EXCEPTION = ex.InnerException.ToString();
            }
            return resp;
        }

    }
}
