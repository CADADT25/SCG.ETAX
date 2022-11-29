

using OfficeOpenXml.FormulaParsing.Excel.Functions.Text;
using SCG.CAD.ETAX.UTILITY.AdminTool;

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
        public Response GET_DETAIL_BY_GROUP(string param)
        {
            Response resp = new Response();
            try
            {
                var profileuser = _dbContext.profileUserManagement.FirstOrDefault(x => x.UserEmail == param);
                var companyGroupList = _dbContext.profileUserGroup
                       .Where(x => profileuser.GroupId.Contains(x.ProfileUserGroupNo.ToString()))
                       .Select(x => x.ProfileCompanyCode)
                       .ToList();
                var companyCodeList = new List<string>();
                foreach (var company in companyGroupList)
                {
                    if (!string.IsNullOrEmpty(company))
                    {
                        var comArr = company.Split(",").ToList();
                        foreach (var com in comArr)
                        {
                            if (!string.IsNullOrEmpty(com))
                            {
                                companyCodeList.Add(com);
                            }
                        }
                    }
                }
                var getList = _dbContext.transactionDescription.Where(t => companyCodeList.Contains(t.CompanyCode)).ToList();

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

        public Response GET_BILLING(string billingNo)
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
                        update.OutputXmlTransactionNo = param.OutputXmlTransactionNo;
                        update.OutputPdfTransactionNo = param.OutputPdfTransactionNo;
                        update.OutputMailTransactionNo = param.OutputMailTransactionNo;
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
                                update.OutputXmlTransactionNo = item.OutputXmlTransactionNo;
                                update.OutputPdfTransactionNo = item.OutputPdfTransactionNo;
                                update.OutputMailTransactionNo = item.OutputMailTransactionNo;
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

        public Response SEARCH(transactionSearchModel JsonString)
        {
            Response resp = new Response();

            transactionSearchModel obj = new transactionSearchModel();

            List<TransactionDescription> tran = new List<TransactionDescription>();

            DateTime getMinDate = new DateTime();
            DateTime getMaxDate = new DateTime();

            try
            {
                //obj = JsonConvert.DeserializeObject<transactionSearchModel>(JsonString);
                var profileuser = _dbContext.profileUserManagement.FirstOrDefault(x => x.UserEmail == JsonString.user);
                var companyGroupList = _dbContext.profileUserGroup
                       .Where(x => profileuser.GroupId.Contains(x.ProfileUserGroupNo.ToString()))
                       .Select(x => x.ProfileCompanyCode)
                       .ToList();
                var companyCodeList = new List<string>();
                foreach (var company in companyGroupList)
                {
                    if (!string.IsNullOrEmpty(company))
                    {
                        var comArr = company.Split(",").ToList();
                        foreach (var com in comArr)
                        {
                            if (!string.IsNullOrEmpty(com))
                            {
                                companyCodeList.Add(com);
                            }
                        }
                    }
                }
                obj = JsonString;

                if (obj != null)
                {
                    tran = _dbContext.transactionDescription.ToList();

                    if (!string.IsNullOrEmpty(obj.tranSearchBillingNo))
                    {
                        tran = tran.Where(x => x.BillingNumber.Contains(obj.tranSearchBillingNo)).ToList();
                    }

                    if (!string.IsNullOrEmpty(obj.tranSearchCustomerCode))
                    {
                        tran = tran.Where(x => x.CustomerId.Contains(obj.tranSearchCustomerCode)).ToList();
                    }

                    if (!string.IsNullOrEmpty(obj.tranSearchDataSource))
                    {
                        tran = tran.Where(x => x.SourceName.Contains(obj.tranSearchDataSource)).ToList();
                    }

                    if (obj.tranSearchCompanyCode.Count > 0)
                    {
                        tran = tran.Where(x => obj.tranSearchCompanyCode.Contains(x.CompanyCode)).ToList();
                    }

                    if (obj.tranSearchStatus.Count > 0)
                    {
                        tran = tran.Where(x => obj.tranSearchStatus.Contains(x.EmailSendStatus) ||
                                    obj.tranSearchStatus.Contains(x.GenerateStatus) ||
                                    obj.tranSearchStatus.Contains(x.PdfIndexingStatus) ||
                                    obj.tranSearchStatus.Contains(x.PdfSignStatus) ||
                                    obj.tranSearchStatus.Contains(x.PrintStatus) ||
                                    obj.tranSearchStatus.Contains(x.XmlCompressStatus) ||
                                    obj.tranSearchStatus.Contains(x.XmlSignStatus)
                                    ).ToList();
                    }

                    if (obj.tranSearchDocumentType.Count > 0)
                    {
                        tran = tran.Where(x => obj.tranSearchDocumentType.Contains(x.DocType)).ToList();
                    }

                    if (!string.IsNullOrEmpty(obj.tranSearchDataSource))
                    {
                        tran = tran.Where(x => obj.tranSearchDataSource.Contains(x.SourceName)).ToList();
                    }

                    if (!string.IsNullOrEmpty(obj.tranSearchDateBetween))
                    {
                        var getArrayDate = obj.tranSearchDateBetween.Split("to");
                        getMinDate = Convert.ToDateTime(getArrayDate[0].Trim());
                        getMaxDate = Convert.ToDateTime(getArrayDate[1].Trim());

                        if (obj.tranSearchDateType.Equals("billingDate"))
                        {
                            tran = tran.Where(x => x.BillingDate >= getMinDate.Date && x.BillingDate <= getMaxDate.Date).ToList();
                        }
                        else if (obj.tranSearchDateType.Equals("createDate"))
                        {
                            tran = tran.Where(x => x.CreateDate >= getMinDate.Date && x.CreateDate <= getMaxDate.Date).ToList();
                        }
                        else if (obj.tranSearchDateType.Equals("processingDate"))
                        {
                            tran = tran.Where(x => x.ProcessingDate >= getMinDate.Date && x.ProcessingDate <= getMaxDate.Date).ToList();
                        }
                    }

                    //tran = _dbContext.transactionDescription.Where(
                    //        x => !string.IsNullOrEmpty(x.BillingNumber) ? x.BillingNumber.Contains(obj.tranSearchBillingNo) : x.BillingNumber != "" &&

                    //!string.IsNullOrEmpty(x.CustomerId) ? x.CustomerId.Contains(obj.tranSearchCustomerCode) : x.CustomerId != "" &&

                    //!string.IsNullOrEmpty(x.SourceName) ? x.SourceName.Contains(obj.tranSearchDataSource) : x.SourceName != "" &&

                    //obj.tranSearchCompanyCode.Count > 0 ? obj.tranSearchCompanyCode.Contains(x.CompanyCode) : x.CompanyCode != "" &&

                    //( 
                    //obj.tranSearchStatus.Count > 0 ? obj.tranSearchStatus.Contains(x.EmailSendStatus) : x.EmailSendStatus != "" ||

                    //    obj.tranSearchStatus.Count > 0 ? obj.tranSearchStatus.Contains(x.GenerateStatus) : x.GenerateStatus != "" ||

                    //    obj.tranSearchStatus.Count > 0 ? obj.tranSearchStatus.Contains(x.PdfIndexingStatus) : x.PdfIndexingStatus != "" ||

                    //    obj.tranSearchStatus.Count > 0 ? obj.tranSearchStatus.Contains(x.PdfSignStatus) : x.PdfSignStatus != "" ||

                    //    obj.tranSearchStatus.Count > 0 ? obj.tranSearchStatus.Contains(x.PrintStatus) : x.PrintStatus != "" ||

                    //    obj.tranSearchStatus.Count > 0 ? obj.tranSearchStatus.Contains(x.XmlCompressStatus) : x.XmlCompressStatus != "" ||

                    //    obj.tranSearchStatus.Count > 0 ? obj.tranSearchStatus.Contains(x.XmlSignStatus) : x.XmlSignStatus != "" 
                    //) && 
                    // obj.tranSearchDocumentType.Count > 0 ? obj.tranSearchDocumentType.Contains(x.DocType) : x.DocType != ""  && 

                    //    !string.IsNullOrEmpty(obj.tranSearchDataSource) ? obj.tranSearchDataSource.Contains(x.SourceName) : x.SourceName != ""
                    //)


                    //.ToList();
                    tran = tran.Where(t => companyCodeList.Contains(t.CompanyCode)).ToList();

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

                    var getList = _dbContext.transactionDescription.ToList();

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

        public Response SYNCSTATUSPDFSIGN(string listbillno, string updateby)
        {
            Response resp = new Response();
            UpdatePDFSign updatePDFSign = new UpdatePDFSign();
            try
            {
                List<string> billno = new List<string>();
                billno = listbillno.Split("|").ToList();
                if (billno.Count > 0)
                {
                    resp.STATUS = updatePDFSign.UpdatePDFSignStatusByMutipleRecords(billno, updateby);
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

        public Response SYNCSTATUSXMLSIGN(string listbillno, string updateby)
        {
            Response resp = new Response();
            UpdateXMLSign updateXMLSign = new UpdateXMLSign();
            try
            {
                List<string> billno = new List<string>();
                billno = listbillno.Split("|").ToList();
                if (billno.Count > 0)
                {
                    resp.STATUS = updateXMLSign.UpdateXMLSignStatusByMutipleRecords(billno, updateby);
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


        public Response DOWNLOADFILE(string path)
        {
            Response resp = new Response();
            try
            {
                if (!String.IsNullOrEmpty(path))
                {
                    if (File.Exists(path))
                    {
                        string zipPath = path;
                        //string zipPath = "D:\\sign.7z";

                        //Read the File as Byte Array.
                        byte[] bytes = File.ReadAllBytes(zipPath);

                        //Convert File to Base64 string and send to Client.
                        resp.OUTPUT_DATA = Convert.ToBase64String(bytes, 0, bytes.Length);
                        resp.MESSAGE = Path.GetFileName(path);
                        resp.STATUS = true;

                    }
                    else
                    {
                        resp.STATUS = false;
                        resp.ERROR_MESSAGE = "File Not Exists";

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


    }
}
