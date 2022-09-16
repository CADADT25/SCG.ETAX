namespace SCG.CAD.ETAX.API.Services
{
    public class OutputSearchEmailSendService
    {
        readonly DatabaseContext _dbContext = new();

        public DateTime dtNow = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd'" + "T" + "'HH:mm:ss.fff"));
        public Response GET_LIST()
        {
            Response resp = new Response();
            try
            {
                var getList = _dbContext.outputSearchEmailSend.ToList();

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
                var getList = _dbContext.outputSearchEmailSend.Where(x => x.OutputSearchEmailSendNo == id).ToList();

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

        public Response INSERT(OutputSearchEmailSend param)
        {
            Response resp = new Response();
            try
            {
                using (_dbContext)
                {
                    param.CreateDate = dtNow;
                    param.UpdateDate = dtNow;

                    _dbContext.outputSearchEmailSend.Add(param);
                    _dbContext.SaveChanges();
                    int identityNo = param.OutputSearchEmailSendNo;

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

        public Response UPDATE(OutputSearchEmailSend param)
        {
            Response resp = new Response();
            try
            {
                using (_dbContext)
                {
                    var update = _dbContext.outputSearchEmailSend.Where(x => x.OutputSearchEmailSendNo == param.OutputSearchEmailSendNo).FirstOrDefault();

                    if (update != null)
                    {
                        update.OutputSearchEmailSendCompanyCode = param.OutputSearchEmailSendCompanyCode;
                        update.OutputSearchEmailSendSubject = param.OutputSearchEmailSendSubject;
                        update.OutputSearchEmailSendFrom = param.OutputSearchEmailSendFrom;
                        update.OutputSearchEmailSendTo = param.OutputSearchEmailSendTo;
                        update.OutputSearchEmailSendCc = param.OutputSearchEmailSendCc;
                        update.OutputSearchEmailSendFileName = param.OutputSearchEmailSendFileName;
                        update.OutputSearchEmailSendStatus = param.OutputSearchEmailSendStatus;

                        update.OutputSearchEmailSendLastTime = param.OutputSearchEmailSendLastTime;
                        update.OutputSearchEmailSendLastBy = param.OutputSearchEmailSendLastBy;

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

        public Response DELETE(OutputSearchEmailSend param)
        {
            Response resp = new Response();
            try
            {
                using (_dbContext)
                {
                    var delete = _dbContext.outputSearchEmailSend.Find(param.OutputSearchEmailSendNo);

                    if (delete != null)
                    {
                        _dbContext.outputSearchEmailSend.Remove(delete);
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

            outputSearchEmailModel obj = new outputSearchEmailModel();

            List<OutputSearchEmailSend> tran = new List<OutputSearchEmailSend>();

            try
            {
                obj = JsonConvert.DeserializeObject<outputSearchEmailModel>(JsonString);

                DateTime getMinDate = new DateTime();
                DateTime getMaxDate = new DateTime();

                var getStatus = obj.outPutSearchEmailStatus;

                int statusDownload = 99;

                getStatus = getStatus == "All" ? getStatus = "" : getStatus = obj.outPutSearchEmailStatus;

                //if (!string.IsNullOrEmpty(getStatus))
                //{
                //    statusDownload = Convert.ToInt32(getStatus);
                //}
                //else
                //{
                //    statusDownload = 99;
                //}


                //if (!string.IsNullOrEmpty(obj.outPutSearchEmailDate))
                //{
                //    getMinDate = Convert.ToDateTime(getArrayDate[0].Trim());
                //    getMaxDate = Convert.ToDateTime(getArrayDate[1].Trim());
                //}
                //else
                //{
                //    getMinDate = DateTime.Now.AddDays(-30);
                //    getMaxDate = DateTime.Now.AddDays(30);
                //}

                if (obj != null)
                {

                    //tran = _dbContext.outputSearchEmailSend.Where(

                    //        x => x.CreateDate >= getMinDate.Date && x.CreateDate <= getMaxDate.Date &&

                    //        obj.outPutSearchEmailCompanyCode.Count > 0 ? (obj.outPutSearchEmailCompanyCode.Contains(x.OutputSearchEmailSendCompanyCode) && x.CreateDate >= getMinDate.Date && x.CreateDate <= getMaxDate.Date) : (x.OutputSearchEmailSendCompanyCode != "" && x.CreateDate >= getMinDate.Date && x.CreateDate <= getMaxDate.Date) &&

                    //        statusDownload == 99 ? (x.OutputSearchEmailSendStatus != 1 && x.OutputSearchEmailSendStatus != 0 && x.CreateDate >= getMinDate.Date && x.CreateDate <= getMaxDate.Date) : (x.OutputSearchEmailSendStatus == statusDownload && x.CreateDate >= getMinDate.Date && x.CreateDate <= getMaxDate.Date)

                    //        ).ToList();

                    tran = _dbContext.outputSearchEmailSend.ToList();

                    if(obj.outPutSearchEmailCompanyCode != null)
                    {
                        if(obj.outPutSearchEmailCompanyCode.Count > 0)
                        {
                            tran = tran.Where(x=> obj.outPutSearchEmailCompanyCode.Contains(x.OutputSearchEmailSendCompanyCode)).ToList();
                        }
                    }

                    if (!string.IsNullOrEmpty(getStatus))
                    {
                        statusDownload = Convert.ToInt32(getStatus);
                        tran = tran.Where(x=> x.OutputSearchEmailSendStatus == statusDownload).ToList();
                    }

                    if (!string.IsNullOrEmpty(obj.outPutSearchEmailDate))
                    {
                        var getArrayDate = obj.outPutSearchEmailDate.Split("to");
                        getMinDate = Convert.ToDateTime(getArrayDate[0].Trim());
                        getMaxDate = Convert.ToDateTime(getArrayDate[1].Trim());

                        tran = tran.Where(x => x.CreateDate >= getMinDate.Date && x.CreateDate <= getMaxDate.Date).ToList();
                    }

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

                    var getList = _dbContext.outputSearchEmailSend.ToList();

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

        public Response DOWNLOADZIPFILE(OutputSearchEmailSend param)
        {
            Response resp = new Response();
            try
            {
                using (_dbContext)
                {
                    var data = _dbContext.outputSearchEmailSend.Where(x => x.OutputSearchEmailSendNo == param.OutputSearchEmailSendNo).FirstOrDefault();

                    if (data != null)
                    {
                        if (!String.IsNullOrEmpty(data.OutputSearchEmailSendFileName))
                        {
                            string zipPath = data.OutputSearchEmailSendFileName;
                            //string zipPath = "D:\\sign.7z";

                            //Read the File as Byte Array.
                            byte[] bytes = File.ReadAllBytes(zipPath);

                            //Convert File to Base64 string and send to Client.
                            resp.OUTPUT_DATA = Convert.ToBase64String(bytes, 0, bytes.Length);
                            resp.MESSAGE = Path.GetFileName(data.OutputSearchEmailSendFileName);

                            data.OutputSearchEmailSendStatus = 1;
                            _dbContext.SaveChanges();

                            SAVEHISTORY(param);
                        }
                        resp.STATUS = true;
                    }
                    else
                    {
                        resp.STATUS = false;
                    }
                }
            }
            catch (Exception ex)
            {
                resp.STATUS = false;
                resp.INNER_EXCEPTION = ex.InnerException.ToString();
            }
            return resp;
        }

        public void SAVEHISTORY(OutputSearchEmailSend param)
        {
            try
            {
                OutputSearchEmailSendHistory insert = new OutputSearchEmailSendHistory();
                insert.Isactive = 1;
                insert.OutputSearchEmailSendHistoryTime = DateTime.Now;
                insert.OutputSearchEmailSendHistoryBy = param.UpdateBy;
                insert.OutputSearchEmailSendNo = param.OutputSearchEmailSendNo;
                insert.CreateBy = param.CreateBy;
                insert.UpdateBy = param.UpdateBy;
                insert.UpdateDate = DateTime.Now;
                insert.CreateDate = DateTime.Now;

                _dbContext.outputSearchEmailSendHistory.Add(insert);
                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
