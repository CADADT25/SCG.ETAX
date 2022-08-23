namespace SCG.CAD.ETAX.API.Services
{
    public class ProfileCertificateService
    {

        readonly DatabaseContext _dbContext = new();

        public DateTime dtNow = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd'" + "T" + "'HH:mm:ss.fff"));
        public Response GET_LIST()
        {
            Response resp = new Response();
            try
            {
                var getList = _dbContext.profileCertificate.ToList();

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
                var getList = _dbContext.profileCertificate.Where(x => x.CertificateNo == id).ToList();

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

        public Response INSERT(ProfileCertificate param)
        {
            Response resp = new Response();
            try
            {
                using (_dbContext)
                {
                    var datacer = _dbContext.profileCertificate.Where(x => x.CertificateCompanyCode == param.CertificateCompanyCode).ToList();

                    if (datacer.FirstOrDefault(x => x.CertificateCertSerial == param.CertificateCertSerial) == null)
                    {
                        var datainsert = _dbContext.certificateMaster.FirstOrDefault(x => x.CertificateCertSerial == param.CertificateCertSerial);

                        if (datainsert != null)
                        {
                            param.CertificateName = datainsert.CertificateName;
                            param.CertificateHsmname = datainsert.CertificateHsmname;
                            param.CertificateHsmserial = datainsert.CertificateHsmserial;
                            param.CertificateCertSerial = datainsert.CertificateCertSerial;
                            param.CertificateKeyAlias = datainsert.CertificateKeyAlias;
                            param.CertificateSlotPassword = datainsert.CertificateSlotPassword;
                            param.CertificateStartDate = datainsert.CertificateStartDate;
                            param.CertificateEndDate = datainsert.CertificateEndDate;

                            param.CreateDate = dtNow;
                            param.UpdateDate = dtNow;

                            _dbContext.profileCertificate.Add(param);
                            _dbContext.SaveChanges();

                            if (param.Isactive == 1)
                            {
                                foreach(var item in datacer)
                                {
                                    item.Isactive = 0;
                                    //_dbContext.profileCertificate.Add(item);
                                    _dbContext.SaveChanges();
                                }

                                var updateprofileCompany = _dbContext.profileCompany.FirstOrDefault(x => x.CompanyCode == param.CertificateCompanyCode);
                                updateprofileCompany.CertificateProfileNo = param.CertificateNo;
                                _dbContext.SaveChanges();

                                UpdateConfigXMLSign(param.CertificateCompanyCode, datainsert);
                                UpdateConfigPDFSign(param.CertificateCompanyCode, datainsert);
                            }

                        }
                    }


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

        public Response INSERTS(List<ProfileCertificate> param)
        {
            Response resp = new Response();

            try
            {
                using (_dbContext)
                {
                    if (param.Count() > 0)
                    {
                        foreach (var item in param)
                        {
                            item.CreateDate = dtNow;

                            item.UpdateDate = dtNow;

                            _dbContext.profileCertificate.Add(item);

                            _dbContext.SaveChanges();
                        }
                    }

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

        public Response UPDATE(ProfileCertificate param)
        {
            Response resp = new Response();
            try
            {
                using (_dbContext)
                {
                    var update = _dbContext.profileCertificate.Where(x => x.CertificateNo == param.CertificateNo).FirstOrDefault();

                    if (update != null)
                    {
                        update.CertificateCompanyCode = param.CertificateCompanyCode;
                        update.CertificateHsmname = param.CertificateHsmname;
                        update.CertificateHsmserial = param.CertificateHsmserial;
                        update.CertificateCertSerial = param.CertificateCertSerial;
                        update.CertificateKeyAlias = param.CertificateKeyAlias;
                        update.CertificateSlotPassword = param.CertificateSlotPassword;
                        update.CertificateStartDate = param.CertificateStartDate;
                        update.CertificateEndDate = param.CertificateEndDate;
                        update.UpdateBy = param.UpdateBy;
                        update.UpdateDate = dtNow;
                        update.Isactive = param.Isactive;

                        _dbContext.SaveChanges();

                        if (param.Isactive == 1)
                        {
                            var datacer = _dbContext.profileCertificate.Where(x => x.CertificateCompanyCode == param.CertificateCompanyCode).ToList();
                            datacer = datacer.Where(x => x.CertificateNo != param.CertificateNo).ToList();
                            if(datacer.Count > 0)
                            {
                                foreach (var item in datacer)
                                {
                                    item.Isactive = 0;
                                    _dbContext.SaveChanges();
                                }
                            }

                            var updateprofileCompany = _dbContext.profileCompany.FirstOrDefault(x => x.CompanyCode == param.CertificateCompanyCode);
                            updateprofileCompany.CertificateProfileNo = param.CertificateNo;
                            _dbContext.SaveChanges();

                            var datainsert = _dbContext.certificateMaster.FirstOrDefault(x=> x.CertificateCertSerial == update.CertificateCertSerial);
                            UpdateConfigXMLSign(param.CertificateCompanyCode, datainsert);
                            UpdateConfigPDFSign(param.CertificateCompanyCode, datainsert);
                        }


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

        public Response DELETE(ProfileCertificate param)
        {
            Response resp = new Response();
            try
            {
                using (_dbContext)
                {
                    var delete = _dbContext.profileCertificate.Find(param.CertificateNo);

                    if (delete != null)
                    {
                        _dbContext.profileCertificate.Remove(delete);
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

        public Response UpdateConfigXMLSign(string companycode, CertificateMaster param)
        {
            Response resp = new Response();
            ConfigXmlSign dataupdate = new ConfigXmlSign();
            try
            {
                using (_dbContext)
                {
                    var dataconfig = _dbContext.configXmlSign.Where(x => x.ConfigXmlsignCompanycode == companycode).ToList();
                    if(dataconfig.Count > 0)
                    {
                        foreach (var item in dataconfig)
                        {
                            dataupdate = item;
                            dataupdate.ConfigXmlsignHsmModule = param.CertificateHsmname;
                            dataupdate.ConfigXmlsignHsmSerial = param.CertificateHsmserial;
                            dataupdate.ConfigXmlsignHsmPassword = param.CertificateSlotPassword;
                            dataupdate.ConfigXmlsignKeyAlias = param.CertificateKeyAlias;
                            dataupdate.ConfigXmlsignCertificateSerial = param.CertificateCertSerial;
                            _dbContext.SaveChanges();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                resp.STATUS = false;
                resp.MESSAGE = "Update data fail.";
                resp.INNER_EXCEPTION = ex.InnerException.ToString();
            }
            return resp;
        }

        private Response UpdateConfigPDFSign(string companycode, CertificateMaster param)
        {
            Response resp = new Response();
            ConfigPdfSign dataupdate = new ConfigPdfSign();
            try
            {
                using (_dbContext)
                {
                    var dataconfig = _dbContext.configPdfSign.Where(x => x.ConfigPdfsignCompanyCode == companycode).ToList();
                    if (dataconfig.Count > 0)
                    {
                        foreach (var item in dataconfig)
                        {
                            dataupdate = item;
                            dataupdate.ConfigPdfsignHsmModule = param.CertificateHsmname;
                            dataupdate.ConfigPdfsignHsmSerial = param.CertificateHsmserial;
                            dataupdate.ConfigPdfsignHsmPassword = param.CertificateSlotPassword;
                            dataupdate.ConfigPdfsignKeyAlias = param.CertificateKeyAlias;
                            dataupdate.ConfigPdfsignCertificateSerial = param.CertificateCertSerial;
                            _dbContext.SaveChanges();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                resp.STATUS = false;
                resp.MESSAGE = "Update data fail.";
                resp.INNER_EXCEPTION = ex.InnerException.ToString();
            }
            return resp;
        }

    }
}
