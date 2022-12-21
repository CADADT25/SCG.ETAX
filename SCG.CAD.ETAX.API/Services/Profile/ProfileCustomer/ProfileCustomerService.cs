﻿namespace SCG.CAD.ETAX.API.Services
{
    public class ProfileCustomerService
    {

        readonly DatabaseContext _dbContext = new();

        public DateTime dtNow = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd'" + "T" + "'HH:mm:ss.fff"));

        public Response GET_LIST()
        {
            Response resp = new Response();
            try
            {
                var getList = _dbContext.profileCustomer.ToList();

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
                var getList = _dbContext.profileCustomer.Where(x => x.CustomerProfileNo == id).ToList();

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
                resp.INNER_EXCEPTION = ex.Message.ToString();
            }
            return resp;
        }

        public Response INSERT(ProfileCustomer param)
        {
            Response resp = new Response();
            try
            {
                using (_dbContext)
                {
                    param.CreateDate = dtNow;
                    param.UpdateDate = dtNow;

                    _dbContext.profileCustomer.Add(param);
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

        public Response UPDATE(ProfileCustomer param)
        {
            Response resp = new Response();
            try
            {
                using (_dbContext)
                {
                    var update = _dbContext.profileCustomer.Where(x => x.CustomerProfileNo == param.CustomerProfileNo).FirstOrDefault();

                    if (update != null)
                    {
                        
                        update.CustomerId = param.CustomerId;
                        update.CompanyCode = param.CompanyCode;
                        update.OutputType = param.OutputType;
                        update.NumberOfCopies = param.NumberOfCopies;
                        update.CustomerEmail = param.CustomerEmail;
                        update.CustomerCcemail = param.CustomerCcemail;
                        update.EmailType = param.EmailType;
                        update.UpdateBy = param.UpdateBy;
                        update.UpdateDate = dtNow;
                        update.Isactive = param.Isactive;
                        update.EmailTemplateNo = param.EmailTemplateNo;
                        update.StatusPrint = param.StatusPrint;
                        update.StatusEmail = param.StatusEmail;
                        update.StatusSignPdf = param.StatusSignPdf;
                        update.StatusSignXml = param.StatusSignXml;

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
        public Response IMPORT(List<ProfileCustomer> paramList)
        {
            Response resp = new Response();
            try
            {
                using (_dbContext)
                {
                    foreach(var param in paramList)
                    {
                        var data = _dbContext.profileCustomer.Where(x => x.CustomerProfileNo == param.CustomerProfileNo).FirstOrDefault();
                        if (data != null)
                        {
                            data.CustomerId = param.CustomerId;
                            data.CompanyCode = param.CompanyCode;
                            data.OutputType = param.OutputType;
                            data.NumberOfCopies = param.NumberOfCopies;
                            data.CustomerEmail = param.CustomerEmail;
                            data.CustomerCcemail = param.CustomerCcemail;
                            data.EmailType = param.EmailType;
                            data.Isactive = param.Isactive;
                            data.EmailTemplateNo = param.EmailTemplateNo;
                            data.StatusPrint = param.StatusPrint;
                            data.StatusEmail = param.StatusEmail;
                            data.StatusSignPdf = param.StatusSignPdf;
                            data.StatusSignXml = param.StatusSignXml;

                            data.UpdateBy = param.UpdateBy;
                            data.UpdateDate = dtNow;
                            _dbContext.profileCustomer.Update(data);
                        }
                        else
                        {
                            data = new ProfileCustomer();
                            data.CustomerId = param.CustomerId;
                            data.CompanyCode = param.CompanyCode;
                            data.OutputType = param.OutputType;
                            data.NumberOfCopies = param.NumberOfCopies;
                            data.CustomerEmail = param.CustomerEmail;
                            data.CustomerCcemail = param.CustomerCcemail;
                            data.EmailType = param.EmailType;
                            data.Isactive = param.Isactive;
                            data.EmailTemplateNo = param.EmailTemplateNo;
                            data.StatusPrint = param.StatusPrint;
                            data.StatusEmail = param.StatusEmail;
                            data.StatusSignPdf = param.StatusSignPdf;
                            data.StatusSignXml = param.StatusSignXml;

                            data.CreateBy = param.UpdateBy;
                            data.CreateDate = dtNow;
                            data.UpdateBy = param.UpdateBy;
                            data.UpdateDate = dtNow;
                            _dbContext.profileCustomer.Add(data);
                        }
                    }
                    _dbContext.SaveChanges();
                    resp.STATUS = true;
                }
            }
            catch (Exception ex)
            {
                resp.STATUS = false;
                resp.MESSAGE = "Import faild.";
                resp.ERROR_MESSAGE = ex.ToString();
            }
            return resp;
        }

        public Response DELETE(ProfileCustomer param)
        {
            Response resp = new Response();
            try
            {
                using (_dbContext)
                {
                    var delete = _dbContext.profileCustomer.Find(param.CustomerProfileNo);

                    if (delete != null)
                    {
                        _dbContext.profileCustomer.Remove(delete);
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
