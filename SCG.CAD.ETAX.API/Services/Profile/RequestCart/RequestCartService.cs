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
                resp.INNER_EXCEPTION = ex.Message.ToString();
            }
            return resp;
        }
        public Response SEARCH_FULL_DATA(RequestCartSearchModel req)
        {
            Response resp = new Response();


            List<RequestCart> cart = new List<RequestCart>();
            List<RequestCartDataModel> resData = new List<RequestCartDataModel>();

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
                // get trans
                if (cart.Count > 0)
                {
                    var transNos = cart.Select(t => t.TransactionNo).ToList();
                    var transDatas = _dbContext.transactionDescription.Where(t => transNos.Contains(t.TransactionNo)).ToList();
                    foreach (var item in cart)
                    {
                        var obj = transDatas.Where(t => t.TransactionNo == item.TransactionNo && t.BillingNumber == item.BillingNumber).FirstOrDefault();
                        if (obj != null)
                        {
                            resData.Add(new RequestCartDataModel
                            {
                                Id = item.Id,
                                TransactionNo = obj.TransactionNo,
                                BillingNumber = obj.BillingNumber,
                                BillingDate = obj.BillingDate,
                                BillingYear = obj.BillingYear,
                                ProcessingDate = obj.ProcessingDate,
                                CompanyCode = obj.CompanyCode,
                                CompanyName = obj.CompanyName,
                                CustomerId = obj.CustomerId,
                                CancelBilling = obj.CancelBilling,
                                CustomerName = obj.CustomerName,
                                SellOrg = obj.SellOrg,
                                SentRevenueDepartment = obj.SentRevenueDepartment,
                                ShipTo = obj.ShipTo,
                                SoldTo = obj.SoldTo,
                                SourceName = obj.SourceName,
                                EmailSendDateTime = obj.EmailSendDateTime,
                                EmailSendDetail = obj.EmailSendDetail,
                                EmailSendStatus = obj.EmailSendStatus,
                                OneTimeEmail = obj.OneTimeEmail,
                                GenerateStatus = obj.EmailSendStatus,
                                FiDoc = obj.FiDoc,
                                BillTo = obj.BillTo,
                                DmsAttachmentFileName = obj.DmsAttachmentFileName,
                                DmsAttachmentFilePath = obj.DmsAttachmentFilePath,
                                DocType = obj.DocType,
                                GenerateDateTime = obj.GenerateDateTime,
                                GenerateDetail = obj.GenerateDetail,
                                Ic = obj.Ic,
                                Foc = obj.Foc,
                                ImageDocType = obj.ImageDocType,
                                Isactive = obj.Isactive,
                                PdfIndexingDateTime = obj.PdfIndexingDateTime,
                                PdfIndexingDetail = obj.PdfIndexingDetail,
                                PdfIndexingStatus = obj.PdfIndexingStatus,
                                PdfSignDateTime = obj.PdfSignDateTime,
                                PdfSignDetail = obj.PdfSignDetail,
                                PdfSignLocation = obj.PdfSignLocation,
                                PdfSignStatus = obj.PdfSignStatus,
                                PoNumber = obj.PoNumber,
                                Payer = obj.Payer,
                                PostingYear = obj.PostingYear,
                                PrintDateTime = obj.PrintDateTime,
                                PrintDetail = obj.PrintDetail,
                                PrintStatus = obj.PrintStatus,
                                OutputPdfTransactionNo = obj.OutputPdfTransactionNo,
                                OutputMailTransactionNo = obj.OutputMailTransactionNo,
                                OutputXmlTransactionNo = obj.OutputXmlTransactionNo,
                                TypeInput = obj.TypeInput,
                                XmlCompressDateTime = obj.XmlCompressDateTime,
                                XmlCompressDetail = obj.XmlCompressDetail,
                                XmlCompressStatus = obj.XmlCompressStatus,
                                XmlSignDateTime = obj.XmlSignDateTime,
                                XmlSignDetail = obj.XmlSignDetail,
                                XmlSignLocation = obj.XmlSignLocation,
                                XmlSignStatus = obj.XmlSignStatus,
                                CreateBy = obj.CreateBy,
                                CreateDate = obj.CreateDate,
                                UpdateBy = obj.UpdateBy,
                                UpdateDate = obj.UpdateDate
                            });
                        }
                    }
                }
                resp.STATUS = true;
                resp.MESSAGE = "Get data success. ";
                resp.OUTPUT_DATA = resData;


            }
            catch (Exception ex)
            {
                resp.STATUS = false;
                resp.MESSAGE = "Get data fail.";
                resp.INNER_EXCEPTION = ex.Message.ToString();
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
                resp.INNER_EXCEPTION = ex.Message.ToString();
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
                        if (update == null)
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
                resp.INNER_EXCEPTION = ex.Message.ToString();
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
                resp.INNER_EXCEPTION = ex.Message.ToString();
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
                resp.INNER_EXCEPTION = ex.Message.ToString();
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
                resp.INNER_EXCEPTION = ex.Message.ToString();
            }
            return resp;
        }
        public Response DELETE(List<RequestCart> param)
        {
            Response resp = new Response();
            try
            {
                using (_dbContext)
                {
                    if (param.Count > 0)
                    {
                        foreach (var item in param)
                        {
                            var delete = _dbContext.requestCart.Find(item.Id);

                            if (delete != null)
                            {
                                _dbContext.requestCart.Remove(delete);

                            }
                        }
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
