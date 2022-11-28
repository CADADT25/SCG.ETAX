using OfficeOpenXml;

namespace SCG.CAD.ETAX.API.Services
{
    public class OutputSearchXmlZipService
    {
        readonly DatabaseContext _dbContext = new();

        public DateTime dtNow = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd'" + "T" + "'HH:mm:ss.fff"));
        public Response GET_LIST()
        {
            Response resp = new Response();
            try
            {
                var getList = _dbContext.outputSearchXmlZip.ToList();

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
                var getList = _dbContext.outputSearchXmlZip.Where(x => x.OutputSearchXmlZipNo == id).ToList();

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

        public Response INSERT(OutputSearchXmlZip param)
        {
            Response resp = new Response();
            try
            {
                using (_dbContext)
                {
                    param.CreateDate = dtNow;
                    param.UpdateDate = dtNow;

                    _dbContext.outputSearchXmlZip.Add(param);
                    _dbContext.SaveChanges();

                    int identityNo = param.OutputSearchXmlZipNo;

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

        public Response UPDATE(OutputSearchXmlZip param)
        {
            Response resp = new Response();
            try
            {
                using (_dbContext)
                {
                    var update = _dbContext.outputSearchXmlZip.Where(x => x.OutputSearchXmlZipNo == param.OutputSearchXmlZipNo).FirstOrDefault();

                    if (update != null)
                    {
                        update.OutputSearchXmlZipCompanyCode = param.OutputSearchXmlZipCompanyCode;
                        update.OutputSearchXmlZipFileName = param.OutputSearchXmlZipFileName;
                        update.OutputSearchXmlZipFullPath = param.OutputSearchXmlZipFullPath;
                        update.OutputSearchXmlZipDowloadStatus = param.OutputSearchXmlZipDowloadStatus;
                        update.OutputSearchXmlZipDowloadCount = param.OutputSearchXmlZipDowloadCount;
                        update.OutputSearchXmlZipDowloadLastTime = param.OutputSearchXmlZipDowloadLastTime;
                        update.OutputSearchXmlZipDowloadLastBy = param.OutputSearchXmlZipDowloadLastBy;

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

        public Response DELETE(OutputSearchXmlZip param)
        {
            Response resp = new Response();
            try
            {
                using (_dbContext)
                {
                    var delete = _dbContext.outputSearchXmlZip.Find(param.OutputSearchXmlZipNo);

                    if (delete != null)
                    {
                        _dbContext.outputSearchXmlZip.Remove(delete);
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

            outputSearchXmlModel obj = new outputSearchXmlModel();

            List<OutputSearchXmlZip> tran = new List<OutputSearchXmlZip>();

            try
            {
                obj = JsonConvert.DeserializeObject<outputSearchXmlModel>(JsonString);

                DateTime getMinDate = new DateTime();
                DateTime getMaxDate = new DateTime();

                var getDocType = obj.outPutSearchDocType.ToUpper();

                var getStatus = obj.outPutSearchStatus;

                int statusDownload = 99;

                getStatus = getStatus == "All" ? getStatus = "" : getStatus = obj.outPutSearchStatus;

                //if (!string.IsNullOrEmpty(getStatus))
                //{
                //    statusDownload = Convert.ToInt32(getStatus);
                //}
                //else
                //{
                //    statusDownload = 99;
                //}


                //if (!string.IsNullOrEmpty(obj.outPutSearchDate))
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
                    tran = _dbContext.outputSearchXmlZip.ToList();

                    if (obj.outPutSearchCompanyCode != null)
                    {
                        if (obj.outPutSearchCompanyCode.Count > 0)
                        {
                            tran = tran.Where(x => obj.outPutSearchCompanyCode.Contains(x.OutputSearchXmlZipCompanyCode)).ToList();
                        }
                    }

                    if (!string.IsNullOrEmpty(getStatus))
                    {
                        statusDownload = Convert.ToInt32(getStatus);
                        tran = tran.Where(x => x.OutputSearchXmlZipDowloadStatus == statusDownload).ToList();
                    }

                    if (!string.IsNullOrEmpty(getDocType) && getDocType.ToUpper() != "ALL")
                    {
                        tran = tran.Where(x => obj.outPutSearchDocType.ToUpper() == x.OutputSearchXmlZipDocType.ToUpper()).ToList();
                    }

                    if (!string.IsNullOrEmpty(obj.outPutSearchDate))
                    {
                        var getArrayDate = obj.outPutSearchDate.Split("to");
                        getMinDate = Convert.ToDateTime(getArrayDate[0].Trim());
                        getMaxDate = Convert.ToDateTime(getArrayDate[1].Trim());

                        tran = tran.Where(x => x.CreateDate >= getMinDate.Date && x.CreateDate <= getMaxDate.Date).ToList();
                    }

                    //tran = _dbContext.outputSearchXmlZip.Where(

                    //        x => x.CreateDate >= getMinDate.Date && x.CreateDate <= getMaxDate.Date &&

                    //        obj.outPutSearchCompanyCode.Count > 0 ? ( obj.outPutSearchCompanyCode.Contains(x.OutputSearchXmlZipCompanyCode) && x.CreateDate >= getMinDate.Date && x.CreateDate <= getMaxDate.Date) : (x.OutputSearchXmlZipCompanyCode != "" && x.CreateDate >= getMinDate.Date && x.CreateDate <= getMaxDate.Date) &&

                    //        statusDownload == 99 ? ( x.OutputSearchXmlZipDowloadStatus != 1 && x.OutputSearchXmlZipDowloadStatus != 0  && x.CreateDate >= getMinDate.Date && x.CreateDate <= getMaxDate.Date) : (x.OutputSearchXmlZipDowloadStatus == statusDownload && x.CreateDate >= getMinDate.Date && x.CreateDate <= getMaxDate.Date) &&

                    //        getDocType != "ALL" ? (obj.outPutSearchDocType.ToUpper() == x.OutputSearchXmlZipDocType.ToUpper() && x.CreateDate >= getMinDate.Date && x.CreateDate <= getMaxDate.Date) : (x.OutputSearchXmlZipDocType != "" && x.CreateDate >= getMinDate.Date && x.CreateDate <= getMaxDate.Date)

                    //        ).ToList();

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

                    var getList = _dbContext.outputSearchXmlZip.ToList();

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

        public Response DOWNLOADZIPFILE(OutputSearchXmlZip param)
        {
            Response resp = new Response();
            try
            {
                using (_dbContext)
                {
                    var data = _dbContext.outputSearchXmlZip.Where(x => x.OutputSearchXmlZipNo == param.OutputSearchXmlZipNo).FirstOrDefault();

                    if (data != null)
                    {
                        if (!String.IsNullOrEmpty(data.OutputSearchXmlZipFullPath))
                        {
                            string zipPath = data.OutputSearchXmlZipFullPath;
                            //string zipPath = "D:\\sign.7z";

                            //Read the File as Byte Array.
                            byte[] bytes = File.ReadAllBytes(zipPath);

                            //Convert File to Base64 string and send to Client.
                            resp.OUTPUT_DATA = Convert.ToBase64String(bytes, 0, bytes.Length);
                            resp.MESSAGE = Path.GetFileName(data.OutputSearchXmlZipFullPath);

                            if (data.OutputSearchXmlZipDowloadCount != null)
                            {
                                data.OutputSearchXmlZipDowloadCount = data.OutputSearchXmlZipDowloadCount + 1;
                            }
                            else
                            {
                                data.OutputSearchXmlZipDowloadCount = 1;
                            }
                            data.OutputSearchXmlZipDowloadStatus = 1;
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

        public void SAVEHISTORY(OutputSearchXmlZip param)
        {
            try
            {
                OutputSearchXmlZipDowloadHistory insert = new OutputSearchXmlZipDowloadHistory();
                insert.Isactive = 1;
                insert.OutputSearchXmlZipDowloadHistoryTime = DateTime.Now;
                insert.OutputSearchXmlZipDowloadHistoryBy = param.UpdateBy;
                insert.OutputSearchXmlZipDowloadHistoryNo = param.OutputSearchXmlZipNo;
                insert.CreateBy = param.CreateBy;
                insert.UpdateBy = param.UpdateBy;
                insert.UpdateDate = DateTime.Now;
                insert.CreateDate = DateTime.Now;

                _dbContext.outputSearchXmlZipDowloadHistory.Add(insert);
                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Response ExportData(string JsonString)
        {
            Response resp = new Response();
            string path = "C:\\FileExport\\";
            string filename = "scg-etax-OutputSearchXmlZip.xlsx";
            List<OutputSearchXmlZip> tran = new List<OutputSearchXmlZip>();
            try
            {
                resp = SEARCH(JsonString);
                tran = JsonConvert.DeserializeObject<List<OutputSearchXmlZip>>(resp.OUTPUT_DATA.ToString());
                if (tran.Count > 0)
                {
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    if (File.Exists(path + filename))
                    {
                        File.Delete(path + filename);
                    }
                    ExcelPackage ExcelPkg = new ExcelPackage();
                    ExcelWorksheet wsSheet1 = ExcelPkg.Workbook.Worksheets.Add("Sheet1");

                    wsSheet1.Cells["A1"].Value = "OutputSearchXmlZipNo";
                    wsSheet1.Cells["B1"].Value = "OutputSearchXmlZipCompanyCode";
                    wsSheet1.Cells["C1"].Value = "OutputSearchXmlZipFileName";
                    wsSheet1.Cells["D1"].Value = "OutputSearchXmlZipFullPath";
                    wsSheet1.Cells["E1"].Value = "OutputSearchXmlZipDowloadStatus";
                    wsSheet1.Cells["F1"].Value = "OutputSearchXmlZipDowloadCount";
                    wsSheet1.Cells["G1"].Value = "OutputSearchXmlZipDowloadLastTime";
                    wsSheet1.Cells["H1"].Value = "OutputSearchXmlZipDowloadLastBy";
                    wsSheet1.Cells["I1"].Value = "CreateBy";
                    wsSheet1.Cells["J1"].Value = "CreateDate";
                    wsSheet1.Cells["K1"].Value = "UpdateBy";
                    wsSheet1.Cells["L1"].Value = "UpdateDate";
                    wsSheet1.Cells["M1"].Value = "Isactive";

                    DateTime createdate;
                    DateTime updatedate;
                    for (int x = 1; x <= tran.Count; x++)
                    {
                        wsSheet1.Cells[x + 1, 1].Value = tran[x - 1].OutputSearchXmlZipNo;
                        wsSheet1.Cells[x + 1, 2].Value = tran[x - 1].OutputSearchXmlZipCompanyCode;
                        wsSheet1.Cells[x + 1, 3].Value = tran[x - 1].OutputSearchXmlZipFileName;
                        wsSheet1.Cells[x + 1, 4].Value = tran[x - 1].OutputSearchXmlZipFullPath;
                        wsSheet1.Cells[x + 1, 5].Value = tran[x - 1].OutputSearchXmlZipDowloadStatus;
                        wsSheet1.Cells[x + 1, 6].Value = tran[x - 1].OutputSearchXmlZipDowloadCount;
                        wsSheet1.Cells[x + 1, 7].Value = tran[x - 1].OutputSearchXmlZipDowloadLastTime;
                        wsSheet1.Cells[x + 1, 8].Value = tran[x - 1].OutputSearchXmlZipDowloadLastBy;
                        wsSheet1.Cells[x + 1, 9].Value = tran[x - 1].CreateBy;
                        wsSheet1.Cells[x + 1, 10].Value = tran[x - 1].CreateDate?.ToString("yyyy-MM-dd hh:mm:ss");
                        wsSheet1.Cells[x + 1, 11].Value = tran[x - 1].UpdateBy;
                        wsSheet1.Cells[x + 1, 12].Value = tran[x - 1].UpdateDate?.ToString("yyyy-MM-dd hh:mm:ss");
                        wsSheet1.Cells[x + 1, 13].Value = tran[x - 1].Isactive;
                        wsSheet1.Cells[x + 1, 13].Value = tran[x - 1].Isactive;
                    }

                    wsSheet1.Protection.IsProtected = false;
                    wsSheet1.Protection.AllowSelectLockedCells = false;
                    ExcelPkg.SaveAs(new FileInfo(path + filename));

                    resp = new Response();
                    byte[] bytes = File.ReadAllBytes(path + filename);
                    resp.OUTPUT_DATA = Convert.ToBase64String(bytes, 0, bytes.Length);
                    resp.STATUS = true;
                    resp.MESSAGE = filename;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return resp;
        }


    }
}
