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
                var getList = _dbContext.transactionDescription.Where(x => x.BillingNumber == billingNo).ToList();

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
