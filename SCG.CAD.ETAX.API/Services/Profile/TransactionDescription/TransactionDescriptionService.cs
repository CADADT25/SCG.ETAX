namespace SCG.CAD.ETAX.API.Services
{
    public class TransactionDescriptionService
    {

        readonly DatabaseContext _dbContext = new();

        public DateTime dtNow = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd'" + "T" + "'HH:mm:ss.fff"));
        public Response GET_LIST()
        {
            Response resp = new Response();
            try
            {
                var getList = _dbContext.transactionDescription.ToList();

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
                var getList = _dbContext.transactionDescription.Where(x => x.TransactionNo == id).ToList();

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

        public Response GET_BILLING(int billingNo)
        {
            Response resp = new Response();

            try
            {
                var getList = _dbContext.transactionDescription.Where(x => x.BillingNumber == Convert.ToString(billingNo)).ToList();

                if (getList.Count > 0)
                {
                    resp.STATUS = true;
                    resp.MESSAGE = "Get data from ID '" + billingNo + "' success. ";
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

        public Response INSERT(TransactionDescription param)
        {
            Response resp = new Response();
            try
            {
                using (_dbContext)
                {
                    param.CreateDate = dtNow;
                    param.UpdateDate = dtNow;

                    _dbContext.transactionDescription.Add(param);
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

        public Response UPDATE(TransactionDescription param)
        {
            Response resp = new Response();
            try
            {
                using (_dbContext)
                {
                    var update = _dbContext.transactionDescription.Where(x => x.TransactionNo == param.TransactionNo).FirstOrDefault();

                    if (update != null)
                    {
                        update.BillingNumber = param.BillingNumber;
                        update.BillingDate = param.BillingDate;
                        update.BillingYear = param.BillingYear;
                        update.ProcessingDate = param.ProcessingDate;
                        update.CompanyCode = param.CompanyCode;
                        update.CompanyName = param.CompanyName;
                        update.CustomerId = param.CustomerId;
                        update.CustomerName = param.CustomerName;
                        update.SoldTo = param.SoldTo;
                        update.ShipTo = param.ShipTo;
                        update.BillTo = param.BillTo;
                        update.Payer = param.Payer;
                        update.SourceName = param.SourceName;
                        update.Foc = param.Foc;
                        update.Ic = param.Ic;
                        update.PostingYear = param.PostingYear;
                        update.FiDoc = param.FiDoc;
                        update.ImageDocType = param.ImageDocType;
                        update.DocType = param.DocType;
                        update.SellOrg = param.SellOrg;
                        update.PoNumber = param.PoNumber;
                        update.TypeInput = param.TypeInput;
                        update.GenerateStatus = param.GenerateStatus;
                        update.GenerateDetail = param.GenerateDetail;
                        update.GenerateDateTime = param.GenerateDateTime;
                        update.XmlSignStatus = param.XmlSignStatus;
                        update.XmlSignDetail = param.XmlSignDetail;
                        update.XmlSignDateTime = param.XmlSignDateTime;
                        update.PdfSignStatus = param.PdfSignStatus;
                        update.PdfSignDetail = param.PdfSignDetail;
                        update.PdfSignDateTime = param.PdfSignDateTime;
                        update.PrintStatus = param.PrintStatus;
                        update.PrintDetail = param.PrintDetail;
                        update.PrintDateTime = param.PrintDateTime;
                        update.EmailSendStatus = param.EmailSendStatus;
                        update.EmailSendDetail = param.EmailSendDetail;
                        update.EmailSendDateTime = param.EmailSendDateTime;
                        update.XmlCompressStatus = param.XmlCompressStatus;
                        update.XmlCompressDetail = param.XmlCompressDetail;
                        update.XmlCompressDateTime = param.XmlCompressDateTime;
                        update.PdfIndexingStatus = param.PdfIndexingStatus;
                        update.PdfIndexingDetail = param.PdfIndexingDetail;
                        update.PdfIndexingDateTime = param.PdfIndexingDateTime;
                        update.PdfSignLocation = param.PdfSignLocation;
                        update.XmlSignLocation = param.XmlSignLocation;
                        update.ZipTransactionNo = param.ZipTransactionNo;
                        update.DmsAttachmentFileName = param.DmsAttachmentFileName;
                        update.DmsAttachmentFilePath = param.DmsAttachmentFilePath;
                        update.OneTimeEmail = param.OneTimeEmail;

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

        public Response UPDATE_LIST(List<TransactionDescription> param)
        {
            Response resp = new Response();

            List<transactionSearchErrorModel> transactionSearchError = new List<transactionSearchErrorModel>();

            try
            {
                using (_dbContext)
                {
                    if (param.Count > 0)
                    {
                        foreach (var item in param)
                        {
                            var update = _dbContext.transactionDescription.Where(x => x.TransactionNo == item.TransactionNo).FirstOrDefault();

                            if (update != null)
                            {
                                update.BillingNumber = item.BillingNumber;
                                update.BillingDate = item.BillingDate;
                                update.BillingYear = item.BillingYear;
                                update.ProcessingDate = item.ProcessingDate;
                                update.CompanyCode = item.CompanyCode;
                                update.CompanyName = item.CompanyName;
                                update.CustomerId = item.CustomerId;
                                update.CustomerName = item.CustomerName;
                                update.SoldTo = item.SoldTo;
                                update.ShipTo = item.ShipTo;
                                update.BillTo = item.BillTo;
                                update.Payer = item.Payer;
                                update.SourceName = item.SourceName;
                                update.Foc = item.Foc;
                                update.Ic = item.Ic;
                                update.PostingYear = item.PostingYear;
                                update.FiDoc = item.FiDoc;
                                update.ImageDocType = item.ImageDocType;
                                update.DocType = item.DocType;
                                update.SellOrg = item.SellOrg;
                                update.PoNumber = item.PoNumber;
                                update.TypeInput = item.TypeInput;
                                update.GenerateStatus = item.GenerateStatus;
                                update.GenerateDetail = item.GenerateDetail;
                                update.GenerateDateTime = item.GenerateDateTime;
                                update.XmlSignStatus = item.XmlSignStatus;
                                update.XmlSignDetail = item.XmlSignDetail;
                                update.XmlSignDateTime = item.XmlSignDateTime;
                                update.PdfSignStatus = item.PdfSignStatus;
                                update.PdfSignDetail = item.PdfSignDetail;
                                update.PdfSignDateTime = item.PdfSignDateTime;
                                update.PrintStatus = item.PrintStatus;
                                update.PrintDetail = item.PrintDetail;
                                update.PrintDateTime = item.PrintDateTime;
                                update.EmailSendStatus = item.EmailSendStatus;
                                update.EmailSendDetail = item.EmailSendDetail;
                                update.EmailSendDateTime = item.EmailSendDateTime;
                                update.XmlCompressStatus = item.XmlCompressStatus;
                                update.XmlCompressDetail = item.XmlCompressDetail;
                                update.XmlCompressDateTime = item.XmlCompressDateTime;
                                update.PdfIndexingStatus = item.PdfIndexingStatus;
                                update.PdfIndexingDetail = item.PdfIndexingDetail;
                                update.PdfIndexingDateTime = item.PdfIndexingDateTime;
                                update.PdfSignLocation = item.PdfSignLocation;
                                update.XmlSignLocation = item.XmlSignLocation;
                                update.ZipTransactionNo = item.ZipTransactionNo;
                                update.DmsAttachmentFileName = item.DmsAttachmentFileName;
                                update.DmsAttachmentFilePath = item.DmsAttachmentFilePath;
                                update.OneTimeEmail = item.OneTimeEmail;

                                update.UpdateBy = item.UpdateBy;
                                update.UpdateDate = dtNow;
                                update.Isactive = item.Isactive;

                                _dbContext.SaveChanges();

                                resp.STATUS = true;

                                resp.MESSAGE = "Updated Success.";
                            }
                            else
                            {
                                transactionSearchError.Add(new transactionSearchErrorModel 
                                { 
                                    tranSearchErrorBillingNo = "123", 
                                    tranSearchErrorDetail = "Can't update because data not found."
                                });
                            }
                        }
                    }
                }

                if (transactionSearchError.Count > 0)
                {
                    resp.ERROR_STACK = Convert.ToString(transactionSearchError.Count());
                    resp.OUTPUT_DATA = transactionSearchError;
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

        public Response DELETE(TransactionDescription param)
        {
            Response resp = new Response();
            try
            {
                using (_dbContext)
                {
                    var delete = _dbContext.transactionDescription.Find(param.TransactionNo);

                    if (delete != null)
                    {
                        _dbContext.transactionDescription.Remove(delete);
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
