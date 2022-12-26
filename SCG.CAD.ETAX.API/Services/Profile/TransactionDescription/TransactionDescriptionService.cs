

using DocumentFormat.OpenXml.Drawing;
using DocumentFormat.OpenXml.VariantTypes;
using OfficeOpenXml;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Math;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Text;
using SCG.CAD.ETAX.UTILITY.AdminTool;
using System.Linq;
using Path = System.IO.Path;

namespace SCG.CAD.ETAX.API.Services
{
    public class TransactionDescriptionService
    {

        //readonly DatabaseContext _dbContext = new();

        public DateTime dtNow = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd'" + "T" + "'HH:mm:ss.fff"));
        public Response GET_LIST()
        {
            Response resp = new Response();
            try
            {
                using (var _dbContext = new DatabaseContext())
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


            }
            catch (Exception ex)
            {
                resp.STATUS = false;
                resp.MESSAGE = "Get data fail.";
                resp.INNER_EXCEPTION = ex.Message.ToString();
            }
            return resp;
        }
        public Response GET_DETAIL_BY_GROUP(string param)
        {
            Response resp = new Response();
            try
            {
                using (var _dbContext = new DatabaseContext())
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
                using (var _dbContext = new DatabaseContext())
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

            }
            catch (Exception ex)
            {
                resp.STATUS = false;
                resp.MESSAGE = "Get data fail.";
                resp.INNER_EXCEPTION = ex.Message.ToString();
            }
            return resp;
        }

        public Response GET_BILLING(string billingNo)
        {
            Response resp = new Response();

            try
            {
                using (var _dbContext = new DatabaseContext())
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
            }
            catch (Exception ex)
            {
                resp.STATUS = false;
                resp.MESSAGE = "Get data fail.";
                resp.INNER_EXCEPTION = ex.Message.ToString();
            }
            return resp;
        }

        public Response INSERT(TransactionDescription param)
        {
            Response resp = new Response();
            try
            {
                using (var _dbContext = new DatabaseContext())
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
                resp.INNER_EXCEPTION = ex.Message.ToString();
            }
            return resp;
        }

        public Response UPDATE(TransactionDescription param)
        {
            Response resp = new Response();
            try
            {
                using (var _dbContext = new DatabaseContext())
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
                        update.PdfBeforeSignLocation = param.PdfBeforeSignLocation;
                        update.PdfSignLocation = param.PdfSignLocation;
                        update.XmlBeforeSignLocation = param.XmlBeforeSignLocation;
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
                resp.INNER_EXCEPTION = ex.Message.ToString();
            }
            return resp;
        }

        public Response UPDATE_LIST(List<TransactionDescription> param)
        {
            Response resp = new Response();

            List<transactionSearchErrorModel> transactionSearchError = new List<transactionSearchErrorModel>();

            try
            {
                using (var _dbContext = new DatabaseContext())
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
                resp.INNER_EXCEPTION = ex.Message.ToString();
            }
            return resp;
        }

        public Response DELETE(TransactionDescription param)
        {
            Response resp = new Response();
            try
            {
                using (var _dbContext = new DatabaseContext())
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
                resp.INNER_EXCEPTION = ex.Message.ToString();
            }
            return resp;
        }

        public Response SEARCH(transactionSearchModel JsonString)
        {
            List<TransactionDescription> tran = new List<TransactionDescription>();
            Response resp = new Response();
            try
            {
                tran = SearchTransaction(JsonString);

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
            catch (Exception ex)
            {
                resp.STATUS = false;
                resp.MESSAGE = "Get data fail.";
                resp.INNER_EXCEPTION = ex.Message.ToString();
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
                resp.INNER_EXCEPTION = ex.Message.ToString();
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
                resp.INNER_EXCEPTION = ex.Message.ToString();
            }
            return resp;
        }

        public Response UPDATEPOSTINGYEAR(string listbillno, string updateby, string postingYear)
        {
            Response resp = new Response();
            UpdateXMLSign updateXMLSign = new UpdateXMLSign();
            try
            {
                using (var _dbContext = new DatabaseContext())
                {
                    List<string> billno = new List<string>();
                    billno = listbillno.Split("|").ToList();
                    if (billno.Count > 0)
                    {
                        var trans = _dbContext.transactionDescription.Where(t => billno.Contains(t.BillingNumber)).ToList();
                        foreach (var tran in trans)
                        {
                            tran.PostingYear = postingYear;
                            tran.UpdateDate = dtNow;
                            tran.UpdateBy = updateby;

                            _dbContext.Entry(tran).State = EntityState.Modified;
                        }
                        _dbContext.SaveChanges();
                    }
                    resp.STATUS = true;
                }
            }
            catch (Exception ex)
            {
                resp.STATUS = false;
                resp.MESSAGE = "Update data fail.";
                resp.ERROR_MESSAGE = ex.ToString();
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
                resp.INNER_EXCEPTION = ex.Message.ToString();
            }
            return resp;
        }

        private List<TransactionDescription> SearchTransaction(transactionSearchModel JsonString)
        {
            List<TransactionDescription> resp = new List<TransactionDescription>();

            transactionSearchModel obj = new transactionSearchModel();

            List<TransactionDescription> tran = new List<TransactionDescription>();

            DateTime getMinDate = new DateTime();
            DateTime getMaxDate = new DateTime();

            try
            {
                using (var _dbContext = new DatabaseContext())
                {
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

                        if (obj.tranSearchIcO2c.Count > 0)
                        {
                            List<double> ics = new List<double>();
                            foreach (var str in obj.tranSearchIcO2c)
                            {
                                try
                                {
                                    if (str == "ic")
                                    {
                                        ics.Add(1);
                                    }
                                    else
                                    {
                                        ics.Add(0);
                                    }
                                }
                                catch
                                {

                                }
                            }
                            tran = tran.Where(x => x.Ic != null).ToList();
                            tran = tran.Where(x => ics.Contains(x.Ic.Value)).ToList();
                        }

                        if (!string.IsNullOrEmpty(obj.tranSearchDataSource))
                        {
                            tran = tran.Where(x => obj.tranSearchDataSource.Contains(x.SourceName)).ToList();
                        }

                        if (!string.IsNullOrEmpty(obj.tranSearchDateBetween))
                        {
                            var getArrayDate = obj.tranSearchDateBetween.Split("to");
                            getMinDate = DateTime.ParseExact(getArrayDate.FirstOrDefault().Trim(), "dd-MM-yyyy", CultureInfo.InvariantCulture);
                            getMaxDate = DateTime.ParseExact(getArrayDate.LastOrDefault().Trim(), "dd-MM-yyyy", CultureInfo.InvariantCulture);
                            //getMinDate = Convert.ToDateTime(getArrayDate[0].Trim());
                            //getMaxDate = Convert.ToDateTime(getArrayDate[1].Trim());

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

                        tran = tran.Where(t => companyCodeList.Contains(t.CompanyCode)).ToList();
                    }
                    else
                    {
                        tran = _dbContext.transactionDescription.ToList();
                    }

                    resp = tran;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return resp;
        }

        public Response ExportDataTransaction(transactionSearchModel JsonString)
        {
            Response resp = new Response();
            string path = "C:\\FileExport\\";
            string filename = "scg-etax-Transaction.xlsx";
            try
            {
                using (var _dbContext = new DatabaseContext())
                {
                    var getList = SearchTransaction(JsonString);

                    if (getList.Count > 0)
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

                        wsSheet1.Cells["A1"].Value = "BillingNumber";
                        wsSheet1.Cells["B1"].Value = "BillingDate";
                        wsSheet1.Cells["C1"].Value = "BillingYear";
                        wsSheet1.Cells["D1"].Value = "ProcessingDate";
                        wsSheet1.Cells["E1"].Value = "CompanyCode";
                        wsSheet1.Cells["F1"].Value = "CompanyName";
                        wsSheet1.Cells["G1"].Value = "CustomerId";
                        wsSheet1.Cells["H1"].Value = "CustomerName";
                        wsSheet1.Cells["I1"].Value = "SoldTo";
                        wsSheet1.Cells["J1"].Value = "ShipTo";
                        wsSheet1.Cells["K1"].Value = "BillTo";
                        wsSheet1.Cells["L1"].Value = "Payer";
                        wsSheet1.Cells["M1"].Value = "SourceName";
                        wsSheet1.Cells["N1"].Value = "Foc";
                        wsSheet1.Cells["O1"].Value = "Ic";
                        wsSheet1.Cells["P1"].Value = "PostingYear";
                        wsSheet1.Cells["Q1"].Value = "FiDoc";
                        wsSheet1.Cells["R1"].Value = "ImageDocType";
                        wsSheet1.Cells["S1"].Value = "DocType";
                        wsSheet1.Cells["T1"].Value = "SellOrg";
                        wsSheet1.Cells["U1"].Value = "PoNumber";
                        wsSheet1.Cells["V1"].Value = "GenerateStatus";
                        wsSheet1.Cells["W1"].Value = "GenerateDetail";
                        wsSheet1.Cells["X1"].Value = "GenerateDateTime";
                        wsSheet1.Cells["Y1"].Value = "XmlSignStatus";
                        wsSheet1.Cells["Z1"].Value = "XmlSignDetail";
                        wsSheet1.Cells["AA1"].Value = "XmlSignDateTime";
                        wsSheet1.Cells["AB1"].Value = "PdfSignStatus";
                        wsSheet1.Cells["AC1"].Value = "PdfSignDetail";
                        wsSheet1.Cells["AD1"].Value = "PdfSignDateTime";
                        wsSheet1.Cells["AE1"].Value = "PrintStatus";
                        wsSheet1.Cells["AF1"].Value = "PrintDetail";
                        wsSheet1.Cells["AG1"].Value = "PrintDateTime";
                        wsSheet1.Cells["AH1"].Value = "EmailSendStatus";
                        wsSheet1.Cells["AI1"].Value = "EmailSendDetail";
                        wsSheet1.Cells["AJ1"].Value = "EmailSendDateTime";
                        wsSheet1.Cells["AK1"].Value = "XmlCompressStatus";
                        wsSheet1.Cells["AL1"].Value = "XmlCompressDetail";
                        wsSheet1.Cells["AM1"].Value = "XmlCompressDateTime";
                        wsSheet1.Cells["AN1"].Value = "PdfIndexingStatus";
                        wsSheet1.Cells["AO1"].Value = "PdfIndexingDetail";
                        wsSheet1.Cells["AP1"].Value = "PdfIndexingDateTime";
                        wsSheet1.Cells["AQ1"].Value = "PdfSignLocation";
                        wsSheet1.Cells["AR1"].Value = "XmlSignLocation";
                        wsSheet1.Cells["AS1"].Value = "OutputXmlFilePath";
                        wsSheet1.Cells["AT1"].Value = "OutputPdfFilePath";
                        wsSheet1.Cells["AU1"].Value = "OutputMailFilePath";
                        wsSheet1.Cells["AV1"].Value = "OneTimeEmail";
                        wsSheet1.Cells["AW1"].Value = "SentRevenueDepartment";
                        wsSheet1.Cells["AX1"].Value = "CancelBilling";
                        wsSheet1.Cells["AY1"].Value = "CreateBy";
                        wsSheet1.Cells["AZ1"].Value = "CreateDate";
                        wsSheet1.Cells["BA1"].Value = "UpdateBy";
                        wsSheet1.Cells["BB1"].Value = "UpdateDate";
                        wsSheet1.Cells["BC1"].Value = "Isactive";

                        List<OutputSearchXmlZip> ListOutputXml = _dbContext.outputSearchXmlZip.ToList();
                        List<OutputSearchPrinting> ListOutputPdf = _dbContext.outputSearchPrinting.ToList();
                        List<OutputSearchEmailSend> ListOutputMail = _dbContext.outputSearchEmailSend.ToList();

                        int OutputXmlNo;
                        int OutputPdfNo;
                        int OutputMailNo;

                        string OutputXmlFilePath;
                        string OutputPdfFilePath;
                        string OutputMailFilePath;

                        OutputSearchXmlZip OutputXml;
                        OutputSearchPrinting OutputPdf;
                        OutputSearchEmailSend OutputMail;

                        for (int x = 1; x <= getList.Count; x++)
                        {
                            OutputXmlFilePath = "";
                            OutputPdfFilePath = "";
                            OutputMailFilePath = "";
                            if (!string.IsNullOrEmpty(getList[x - 1].OutputXmlTransactionNo))
                            {
                                OutputXmlNo = Convert.ToInt32(getList[x - 1].OutputXmlTransactionNo);
                                OutputXml = ListOutputXml.FirstOrDefault(x => x.OutputSearchXmlZipNo == OutputXmlNo);
                                OutputXmlFilePath = OutputXml.OutputSearchXmlZipFullPath;
                            }
                            if (!string.IsNullOrEmpty(getList[x - 1].OutputPdfTransactionNo))
                            {
                                OutputPdfNo = Convert.ToInt32(getList[x - 1].OutputPdfTransactionNo);
                                OutputPdf = ListOutputPdf.FirstOrDefault(x => x.OutputSearchPrintingNo == OutputPdfNo);
                                OutputPdfFilePath = OutputPdf.OutputSearchPrintingFullPath;
                            }
                            if (!string.IsNullOrEmpty(getList[x - 1].OutputMailTransactionNo))
                            {
                                OutputMailNo = Convert.ToInt32(getList[x - 1].OutputMailTransactionNo);
                                OutputMail = ListOutputMail.FirstOrDefault(x => x.OutputSearchEmailSendNo == OutputMailNo);
                                OutputMailFilePath = OutputMail.OutputSearchEmailSendFileName;
                            }

                            wsSheet1.Cells[x + 1, 1].Value = getList[x - 1].BillingNumber;
                            wsSheet1.Cells[x + 1, 2].Value = getList[x - 1].BillingDate?.ToString("yyyy-MM-dd");
                            wsSheet1.Cells[x + 1, 3].Value = getList[x - 1].BillingYear;
                            wsSheet1.Cells[x + 1, 4].Value = getList[x - 1].ProcessingDate?.ToString("yyyy-MM-dd");
                            wsSheet1.Cells[x + 1, 5].Value = getList[x - 1].CompanyCode;
                            wsSheet1.Cells[x + 1, 6].Value = getList[x - 1].CompanyName;
                            wsSheet1.Cells[x + 1, 7].Value = getList[x - 1].CustomerId;
                            wsSheet1.Cells[x + 1, 8].Value = getList[x - 1].CustomerName;
                            wsSheet1.Cells[x + 1, 9].Value = getList[x - 1].SoldTo;
                            wsSheet1.Cells[x + 1, 10].Value = getList[x - 1].ShipTo;
                            wsSheet1.Cells[x + 1, 11].Value = getList[x - 1].BillTo;
                            wsSheet1.Cells[x + 1, 12].Value = getList[x - 1].Payer;
                            wsSheet1.Cells[x + 1, 13].Value = getList[x - 1].SourceName;
                            wsSheet1.Cells[x + 1, 14].Value = getList[x - 1].Foc;
                            wsSheet1.Cells[x + 1, 15].Value = getList[x - 1].Ic;
                            wsSheet1.Cells[x + 1, 16].Value = getList[x - 1].PostingYear;
                            wsSheet1.Cells[x + 1, 17].Value = getList[x - 1].FiDoc;
                            wsSheet1.Cells[x + 1, 18].Value = getList[x - 1].ImageDocType;
                            wsSheet1.Cells[x + 1, 19].Value = getList[x - 1].DocType;
                            wsSheet1.Cells[x + 1, 20].Value = getList[x - 1].SellOrg;
                            wsSheet1.Cells[x + 1, 21].Value = getList[x - 1].PoNumber;
                            wsSheet1.Cells[x + 1, 22].Value = getList[x - 1].GenerateStatus;
                            wsSheet1.Cells[x + 1, 23].Value = getList[x - 1].GenerateDetail;
                            wsSheet1.Cells[x + 1, 24].Value = getList[x - 1].GenerateDateTime?.ToString("yyyy-MM-dd hh:mm:ss");
                            wsSheet1.Cells[x + 1, 25].Value = getList[x - 1].XmlSignStatus;
                            wsSheet1.Cells[x + 1, 26].Value = getList[x - 1].XmlSignDetail;
                            wsSheet1.Cells[x + 1, 27].Value = getList[x - 1].XmlSignDateTime?.ToString("yyyy-MM-dd hh:mm:ss");
                            wsSheet1.Cells[x + 1, 28].Value = getList[x - 1].PdfSignStatus;
                            wsSheet1.Cells[x + 1, 29].Value = getList[x - 1].PdfSignDetail;
                            wsSheet1.Cells[x + 1, 30].Value = getList[x - 1].PdfSignDateTime?.ToString("yyyy-MM-dd hh:mm:ss");
                            wsSheet1.Cells[x + 1, 31].Value = getList[x - 1].PrintStatus;
                            wsSheet1.Cells[x + 1, 32].Value = getList[x - 1].PrintDetail;
                            wsSheet1.Cells[x + 1, 33].Value = getList[x - 1].PrintDateTime?.ToString("yyyy-MM-dd hh:mm:ss");
                            wsSheet1.Cells[x + 1, 34].Value = getList[x - 1].EmailSendStatus;
                            wsSheet1.Cells[x + 1, 35].Value = getList[x - 1].EmailSendDetail;
                            wsSheet1.Cells[x + 1, 36].Value = getList[x - 1].EmailSendDateTime?.ToString("yyyy-MM-dd hh:mm:ss");
                            wsSheet1.Cells[x + 1, 37].Value = getList[x - 1].XmlCompressStatus;
                            wsSheet1.Cells[x + 1, 38].Value = getList[x - 1].XmlCompressDetail;
                            wsSheet1.Cells[x + 1, 39].Value = getList[x - 1].XmlCompressDateTime?.ToString("yyyy-MM-dd hh:mm:ss");
                            wsSheet1.Cells[x + 1, 40].Value = getList[x - 1].PdfIndexingStatus;
                            wsSheet1.Cells[x + 1, 41].Value = getList[x - 1].PdfIndexingDetail;
                            wsSheet1.Cells[x + 1, 42].Value = getList[x - 1].PdfIndexingDateTime?.ToString("yyyy-MM-dd hh:mm:ss");
                            wsSheet1.Cells[x + 1, 43].Value = getList[x - 1].PdfSignLocation;
                            wsSheet1.Cells[x + 1, 44].Value = getList[x - 1].XmlSignLocation;
                            wsSheet1.Cells[x + 1, 45].Value = OutputXmlFilePath;
                            wsSheet1.Cells[x + 1, 46].Value = OutputPdfFilePath;
                            wsSheet1.Cells[x + 1, 47].Value = OutputMailFilePath;
                            wsSheet1.Cells[x + 1, 48].Value = getList[x - 1].OneTimeEmail;
                            wsSheet1.Cells[x + 1, 49].Value = getList[x - 1].SentRevenueDepartment;
                            wsSheet1.Cells[x + 1, 50].Value = getList[x - 1].CancelBilling;
                            wsSheet1.Cells[x + 1, 51].Value = getList[x - 1].CreateBy;
                            wsSheet1.Cells[x + 1, 52].Value = getList[x - 1].CreateDate?.ToString("yyyy-MM-dd hh:mm:ss");
                            wsSheet1.Cells[x + 1, 53].Value = getList[x - 1].UpdateBy;
                            wsSheet1.Cells[x + 1, 54].Value = getList[x - 1].UpdateDate?.ToString("yyyy-MM-dd hh:mm:ss");
                            wsSheet1.Cells[x + 1, 55].Value = getList[x - 1].Isactive;
                        }

                        wsSheet1.Protection.IsProtected = false;
                        wsSheet1.Protection.AllowSelectLockedCells = false;
                        ExcelPkg.SaveAs(new FileInfo(path + filename));

                        byte[] bytes = File.ReadAllBytes(path + filename);
                        resp.OUTPUT_DATA = Convert.ToBase64String(bytes, 0, bytes.Length);
                        resp.STATUS = true;
                        resp.MESSAGE = filename;

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
                throw ex;
            }
            return resp;
        }

    }
}
