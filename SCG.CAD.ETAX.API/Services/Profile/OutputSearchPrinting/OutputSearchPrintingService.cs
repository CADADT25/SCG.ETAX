namespace SCG.CAD.ETAX.API.Services
{
    public class OutputSearchPrintingService
    {
        readonly DatabaseContext _dbContext = new();

        public DateTime dtNow = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd'" + "T" + "'HH:mm:ss.fff"));
        public Response GET_LIST()
        {
            Response resp = new Response();
            try
            {
                var getList = _dbContext.outputSearchPrinting.ToList();

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
                var getList = _dbContext.outputSearchPrinting.Where(x => x.OutputSearchPrintingNo == id).ToList();

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

        public Response INSERT(OutputSearchPrinting param)
        {
            Response resp = new Response();
            try
            {
                using (_dbContext)
                {
                    param.CreateDate = dtNow;
                    param.UpdateDate = dtNow;

                    _dbContext.outputSearchPrinting.Add(param);
                    _dbContext.SaveChanges();

                    int identityNo = param.OutputSearchPrintingNo;



                    resp.STATUS = true;
                    resp.MESSAGE = "Insert success.";
                    resp.OUTPUT_DATA = identityNo;
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

        public Response UPDATE(OutputSearchPrinting param)
        {
            Response resp = new Response();
            try
            {
                using (_dbContext)
                {
                    var update = _dbContext.outputSearchPrinting.Where(x => x.OutputSearchPrintingNo == param.OutputSearchPrintingNo).FirstOrDefault();

                    if (update != null)
                    {
                        update.OutputSearchPrintingCompanyCode = param.OutputSearchPrintingCompanyCode;
                        update.OutputSearchPrintingFileName = param.OutputSearchPrintingFileName;
                        update.OutputSearchPrintingFullPath = param.OutputSearchPrintingFullPath;
                        update.OutputSearchPrintingDowloadStatus = param.OutputSearchPrintingDowloadStatus;
                        update.OutputSearchPrintingDowloadCount = param.OutputSearchPrintingDowloadCount;
                        update.OutputSearchPrintingDowloadLastTime = param.OutputSearchPrintingDowloadLastTime;
                        update.OutputSearchPrintingDowloadLastBy = param.OutputSearchPrintingDowloadLastBy;

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

        public Response DELETE(OutputSearchPrinting param)
        {
            Response resp = new Response();
            try
            {
                using (_dbContext)
                {
                    var delete = _dbContext.outputSearchPrinting.Find(param.OutputSearchPrintingNo);

                    if (delete != null)
                    {
                        _dbContext.outputSearchPrinting.Remove(delete);
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

        public Response SEARCH(string JsonString)
        {
            Response resp = new Response();

            outputSearchPrintingModel obj = new outputSearchPrintingModel();

            List<OutputSearchPrinting> tran = new List<OutputSearchPrinting>();

            try
            {

                obj = JsonConvert.DeserializeObject<outputSearchPrintingModel>(JsonString);

                DateTime getMinDate = new DateTime();
                DateTime getMaxDate = new DateTime();

                var getStatus = obj.outPutSearchStatus;

                int statusDownload = 99;

                getStatus = getStatus == "All" ? getStatus = "" : getStatus = obj.outPutSearchStatus;

                if (!string.IsNullOrEmpty(getStatus))
                {
                    statusDownload = Convert.ToInt32(getStatus);
                }
                else
                {
                    statusDownload = 99;
                }

                var getArrayDate = obj.outPutSearchDate.Split("to");

                if (!string.IsNullOrEmpty(obj.outPutSearchDate))
                {
                    getMinDate = Convert.ToDateTime(getArrayDate[0].Trim());
                    getMaxDate = Convert.ToDateTime(getArrayDate[1].Trim());
                }
                else
                {
                    getMinDate = DateTime.Now.AddDays(-30);
                    getMaxDate = DateTime.Now.AddDays(30);
                }


                if (obj != null)
                {

                    tran = _dbContext.outputSearchPrinting.Where(

                            x =>  x.CreateDate >= getMinDate.Date &&  x.CreateDate <= getMaxDate.Date && 

                            obj.outPutSearchCompanyCode.Count > 0 ? obj.outPutSearchCompanyCode.Contains(x.OutputSearchPrintingCompanyCode) : ( x.OutputSearchPrintingCompanyCode != "" && x.CreateDate >= getMinDate.Date && x.CreateDate <= getMaxDate.Date ) &&

                            statusDownload == 99 ? x.OutputSearchPrintingDowloadStatus != 99 :  ( x.OutputSearchPrintingDowloadStatus == statusDownload && x.CreateDate >= getMinDate.Date && x.CreateDate <= getMaxDate.Date)

                            ).ToList();


                    if (tran.Count > 0)
                    {
                        resp.STATUS = true;
                        resp.MESSAGE = "Get data success. ";
                        resp.OUTPUT_DATA = tran;
                    }
                    else
                    {
                        resp.STATUS = false;
                        resp.MESSAGE = "Data not found";
                    }
                }
                else
                {

                    var getList = _dbContext.outputSearchPrinting.ToList();

                    if (getList.Count > 0)
                    {
                        resp.STATUS = true;
                        resp.MESSAGE = "Get data success. ";
                        resp.OUTPUT_DATA = getList;
                    }
                    else
                    {
                        resp.STATUS = false;
                        resp.MESSAGE = "Data not found";
                    }
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


    }
}
