using System.Collections;
using System.Globalization;

namespace SCG.CAD.ETAX.API.Services
{
    public class ConfigPdfSignService
    {
        readonly DatabaseContext _dbContext = new();

        public DateTime dtNow = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd'" + "T" + "'HH:mm:ss.fff"));
        public Response GET_LIST()
        {
            Response resp = new Response();
            try
            {
                var getList = _dbContext.configPdfSign.ToList();

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
                var getList = _dbContext.configPdfSign.Where(x => x.ConfigPdfsignNo == id).ToList();

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

        public Response INSERT(ConfigPdfSign param)
        {
            Response resp = new Response();
            ProfileCertificate dataCertificate = new ProfileCertificate();
            try
            {
                using (_dbContext)
                {
                    var profilecompany = _dbContext.profileCompany.FirstOrDefault(x => x.CompanyCode == param.ConfigPdfsignCompanyCode);
                    if (profilecompany != null)
                    {
                        dataCertificate = _dbContext.profileCertificate.FirstOrDefault(x => x.CertificateNo == profilecompany.CertificateProfileNo);
                    }
                    param.ConfigPdfsignHsmModule = dataCertificate.CertificateHsmname;
                    param.ConfigPdfsignHsmSerial = dataCertificate.CertificateHsmserial;
                    param.ConfigPdfsignHsmPassword = dataCertificate.CertificateSlotPassword;
                    param.ConfigPdfsignKeyAlias = dataCertificate.CertificateKeyAlias;
                    param.ConfigPdfsignCertificateSerial = dataCertificate.CertificateCertSerial;
                    param.CreateDate = dtNow;
                    param.UpdateDate = dtNow;

                    _dbContext.configPdfSign.Add(param);
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

        public Response UPDATE(ConfigPdfSign param)
        {
            Response resp = new Response();
            try
            {
                using (_dbContext)
                {
                    var update = _dbContext.configPdfSign.Where(x => x.ConfigPdfsignNo == param.ConfigPdfsignNo).FirstOrDefault();

                    if (update != null)
                    {
                        update.ConfigPdfsignUlx = param.ConfigPdfsignUlx;
                        update.ConfigPdfsignUly = param.ConfigPdfsignUly;
                        update.ConfigPdfsignPage = param.ConfigPdfsignPage;
                        update.ConfigPdfsignOnlineRecordNumber = param.ConfigPdfsignOnlineRecordNumber;
                        update.ConfigPdfsignInputSource = param.ConfigPdfsignInputSource;
                        update.ConfigPdfsignInputType = param.ConfigPdfsignInputType;
                        update.ConfigPdfsignInputPath = param.ConfigPdfsignInputPath;
                        update.ConfigPdfsignOutputSource = param.ConfigPdfsignOutputSource;
                        update.ConfigPdfsignOutputType = param.ConfigPdfsignOutputType;
                        update.ConfigPdfsignOutputPath = param.ConfigPdfsignOutputPath;
                        update.ConfigPdfsignHsmSerial = param.ConfigPdfsignHsmSerial;
                        update.ConfigPdfsignKeyAlias = param.ConfigPdfsignKeyAlias;
                        update.ConfigPdfsignOneTime = param.ConfigPdfsignOneTime;
                        update.ConfigPdfsignAnyTime = param.ConfigPdfsignAnyTime;
                        update.ConfigPdfsignNextTime = param.ConfigPdfsignNextTime;
                      

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

        public Response DELETE(ConfigPdfSign param)
        {
            Response resp = new Response();
            try
            {
                using (_dbContext)
                {
                    var delete = _dbContext.configPdfSign.Find(param.ConfigPdfsignNo);

                    if (delete != null)
                    {
                        _dbContext.configPdfSign.Remove(delete);
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


        public Response UPDATE_ONETIME(ConfigPdfSign param)
        {
            Response resp = new Response();
            try
            {
                using (_dbContext)
                {
                    var update = _dbContext.configPdfSign.Where(x => x.ConfigPdfsignNo == param.ConfigPdfsignNo).FirstOrDefault();

                    if (update != null)
                    {
                        var setNewOnetime = "";

                        setNewOnetime += "|" + param.ConfigPdfsignOneTime;

                        var findFirstIndex = setNewOnetime.Substring(0, 1);

                        if (findFirstIndex == "|")
                        {
                            setNewOnetime = setNewOnetime.Substring(1);
                        }

                        var GetOldValue = update.ConfigPdfsignOneTime;

                        GetOldValue = GetOldValue += "|" + setNewOnetime;

                        var SetArrayOldValue = GetOldValue.Split("|");

                        ArrayList ArrayDateSortOld = new ArrayList();

                        ArrayList ArrayDateSortNew = new ArrayList();

                        foreach (var item in SetArrayOldValue)
                        {
                            if (!string.IsNullOrEmpty(item))
                            {
                                DateTime dt = DateTime.ParseExact(item.ToString(), "dd-MM-yyyy hh:mm", CultureInfo.InvariantCulture);

                                string s = dt.ToString("yyyy-MM-dd hh:mm", CultureInfo.InvariantCulture);

                                ArrayDateSortOld.Add(s);
                            }
                        }

                        ArrayDateSortOld.Sort();

                        foreach (var item in ArrayDateSortOld)
                        {
                            DateTime dt = DateTime.ParseExact(item.ToString(), "yyyy-MM-dd hh:mm", CultureInfo.InvariantCulture);

                            string s = dt.ToString("dd-MM-yyyy hh:mm", CultureInfo.InvariantCulture);

                            ArrayDateSortNew.Add(s);
                        }

                        var setSort = "";

                        int count = ArrayDateSortNew.Count;

                        int idx = 1;

                        foreach (var item in ArrayDateSortNew)
                        {
                            if (idx == count)
                            {
                                setSort += item;
                            }
                            else
                            {
                                setSort += item + "|";
                            }

                            idx++;
                        }

                        update.ConfigPdfsignOneTime = setSort;


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

        public Response UPDATE_ANYTIME(ConfigPdfSign param)
        {
            Response resp = new Response();
            try
            {
                using (_dbContext)
                {
                    var update = _dbContext.configPdfSign.Where(x => x.ConfigPdfsignNo == param.ConfigPdfsignNo).FirstOrDefault();

                    if (update != null)
                    {

                        var setNewAnytime = "";

                        setNewAnytime += "|" + param.ConfigPdfsignAnyTime;

                        var findFirstIndex = setNewAnytime.Substring(0, 1);

                        if (findFirstIndex == "|")
                        {
                            setNewAnytime = setNewAnytime.Substring(1, 5);
                        }


                        var GetOldValue = update.ConfigPdfsignAnyTime;

                        GetOldValue = GetOldValue += "|" + setNewAnytime;

                        var SetArrayOldValue = GetOldValue.Split("|");

                        ArrayList ArrayDateSort = new ArrayList();

                        foreach (var item in SetArrayOldValue)
                        {
                            if (!string.IsNullOrEmpty(item))
                            {
                                ArrayDateSort.Add(item);
                            }
                        }

                        ArrayDateSort.Sort();

                        var setSort = "";

                        int count = ArrayDateSort.Count;

                        int idx = 1;

                        foreach (var item in ArrayDateSort)
                        {
                            if (idx == count)
                            {
                                setSort += item;
                            }
                            else
                            {
                                setSort += item + "|";
                            }

                            idx++;
                        }

                        update.ConfigPdfsignAnyTime = setSort;


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


        public Response DELETE_ONETIME(DeleteOnetime param)
        {
            Response resp = new Response();
            try
            {
                using (_dbContext)
                {
                    var update = _dbContext.configPdfSign.Where(x => x.ConfigPdfsignNo == param.pk).FirstOrDefault();

                    if (update != null)
                    {

                        var getOnetime = update.ConfigPdfsignOneTime;

                        var splitOneTime = getOnetime.Split("|");

                        var setNewOneTime = "";

                        for (int i = 0; i < splitOneTime.Length; i++)
                        {
                            if (i != param.position)
                            {
                                setNewOneTime += "|" + splitOneTime[i];
                            }
                        }

                        update.ConfigPdfsignOneTime = setNewOneTime.Substring(1);

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

        public Response DELETE_ANYTIME(DeleteAnytime param)
        {
            Response resp = new Response();
            try
            {
                using (_dbContext)
                {
                    var update = _dbContext.configPdfSign.Where(x => x.ConfigPdfsignNo == param.pk).FirstOrDefault();

                    if (update != null)
                    {

                        var getAnyTime = update.ConfigPdfsignAnyTime;

                        var splitAnyTime = getAnyTime.Split("|");

                        var setNewAnyTime = "";

                        for (int i = 0; i < splitAnyTime.Length; i++)
                        {
                            if (i != param.position)
                            {
                                setNewAnyTime += "|" + splitAnyTime[i];
                            }
                        }

                        update.ConfigPdfsignAnyTime = setNewAnyTime.Substring(1);

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


        public Response UPDATE_NEXTTIME(ConfigNextTime param)
        {
            Response resp = new Response();
            try
            {
                using (_dbContext)
                {
                    var update = _dbContext.configPdfSign.Where(x => x.ConfigPdfsignNo == param.Id).FirstOrDefault();

                    if (update != null)
                    {

                        var getOnetime = update.ConfigPdfsignOneTime;

                        var splitOneTime = getOnetime.Split("|");

                        var setNewOneTime = "";

                        for (int i = 0; i < splitOneTime.Length; i++)
                        {
                            if (i != param.OneTimePosition)
                            {
                                setNewOneTime += "|" + splitOneTime[i];
                            }
                        }

                        update.ConfigPdfsignOneTime = setNewOneTime.Substring(1);
                        update.ConfigPdfsignNextTime = param.NextTime;

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

    }
}
