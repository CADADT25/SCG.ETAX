using System.Collections;
using System.Globalization;

namespace SCG.CAD.ETAX.API.Services
{
    public class ConfigMftsCompressPrintSettingService
    {
        readonly DatabaseContext _dbContext = new();

        public DateTime dtNow = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd'" + "T" + "'HH:mm:ss.fff"));

        public Response GET_LIST()
        {
            Response resp = new Response();
            try
            {
                var getList = _dbContext.configMftsCompressPrintSetting.ToList();

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
                var getList = _dbContext.configMftsCompressPrintSetting.Where(x => x.ConfigMftsCompressPrintSettingNo == id).ToList();

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

        public Response INSERT(ConfigMftsCompressPrintSetting param)
        {
            Response resp = new Response();
            try
            {
                using (_dbContext)
                {
                    param.CreateDate = dtNow;
                    param.UpdateDate = dtNow;

                    _dbContext.configMftsCompressPrintSetting.Add(param);
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

        public Response UPDATE(ConfigMftsCompressPrintSetting param)
        {
            Response resp = new Response();
            try
            {
                using (_dbContext)
                {
                    var update = _dbContext.configMftsCompressPrintSetting.Where(x => x.ConfigMftsCompressPrintSettingNo == param.ConfigMftsCompressPrintSettingNo).FirstOrDefault();

                    if (update != null)
                    {
                        update.ConfigMftsCompressPrintSettingCompanyCode = param.ConfigMftsCompressPrintSettingCompanyCode;
                        update.ConfigMftsCompressPrintSettingOneTime = param.ConfigMftsCompressPrintSettingOneTime;
                        update.ConfigMftsCompressPrintSettingAnyTime = param.ConfigMftsCompressPrintSettingAnyTime;
                        update.ConfigMftsCompressPrintSettingNextTime = param.ConfigMftsCompressPrintSettingNextTime;
                        update.ConfigMftsCompressPrintSettingOutputPdf = param.ConfigMftsCompressPrintSettingOutputPdf;
                        update.ConfigMftsCompressPrintSettingInputPdf = param.ConfigMftsCompressPrintSettingInputPdf;
                        update.ConfigMftsCompressPrintSettingDataSource = param.ConfigMftsCompressPrintSettingDataSource;
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

        public Response DELETE(ConfigMftsCompressPrintSetting param)
        {
            Response resp = new Response();
            try
            {
                using (_dbContext)
                {
                    var delete = _dbContext.configMftsCompressPrintSetting.Find(param.ConfigMftsCompressPrintSettingNo);

                    if (delete != null)
                    {
                        _dbContext.configMftsCompressPrintSetting.Remove(delete);
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


        public Response UPDATE_ONETIME(ConfigMftsCompressPrintSetting param)
        {
            Response resp = new Response();
            try
            {
                using (_dbContext)
                {
                    var update = _dbContext.configMftsCompressPrintSetting.Where(x => x.ConfigMftsCompressPrintSettingNo == param.ConfigMftsCompressPrintSettingNo).FirstOrDefault();

                    if (update != null)
                    {
                        var setNewOnetime = "";

                        setNewOnetime += "|" + param.ConfigMftsCompressPrintSettingOneTime;

                        var findFirstIndex = setNewOnetime.Substring(0, 1);

                        if (findFirstIndex == "|")
                        {
                            setNewOnetime = setNewOnetime.Substring(1);
                        }

                        var GetOldValue = update.ConfigMftsCompressPrintSettingOneTime;

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

                        update.ConfigMftsCompressPrintSettingOneTime = setSort;


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

        public Response UPDATE_ANYTIME(ConfigMftsCompressPrintSetting param)
        {
            Response resp = new Response();
            try
            {
                using (_dbContext)
                {
                    var update = _dbContext.configMftsCompressPrintSetting.Where(x => x.ConfigMftsCompressPrintSettingNo == param.ConfigMftsCompressPrintSettingNo).FirstOrDefault();

                    if (update != null)
                    {

                        var setNewAnytime = "";

                        setNewAnytime += "|" + param.ConfigMftsCompressPrintSettingAnyTime;

                        var findFirstIndex = setNewAnytime.Substring(0, 1);

                        if (findFirstIndex == "|")
                        {
                            setNewAnytime = setNewAnytime.Substring(1, 5);
                        }


                        var GetOldValue = update.ConfigMftsCompressPrintSettingAnyTime;

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

                        update.ConfigMftsCompressPrintSettingAnyTime = setSort;


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
                    var update = _dbContext.configMftsCompressPrintSetting.Where(x => x.ConfigMftsCompressPrintSettingNo == param.pk).FirstOrDefault();

                    if (update != null)
                    {

                        var getOnetime = update.ConfigMftsCompressPrintSettingOneTime;

                        var splitOneTime = getOnetime.Split("|");

                        var setNewOneTime = "";

                        for (int i = 0; i < splitOneTime.Length; i++)
                        {
                            if (i != param.position)
                            {
                                setNewOneTime += "|" + splitOneTime[i];
                            }
                        }

                        if(setNewOneTime.Length > 0)
                        {
                            update.ConfigMftsCompressPrintSettingOneTime = setNewOneTime.Substring(1);
                        }
                        else
                        {
                            update.ConfigMftsCompressPrintSettingOneTime = setNewOneTime;
                        }

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
                    var update = _dbContext.configMftsCompressPrintSetting.Where(x => x.ConfigMftsCompressPrintSettingNo == param.pk).FirstOrDefault();

                    if (update != null)
                    {

                        var getAnyTime = update.ConfigMftsCompressPrintSettingAnyTime;

                        var splitAnyTime = getAnyTime.Split("|");

                        var setNewAnyTime = "";

                        for (int i = 0; i < splitAnyTime.Length; i++)
                        {
                            if (i != param.position)
                            {
                                setNewAnyTime += "|" + splitAnyTime[i];
                            }
                        }

                        if (setNewAnyTime.Length > 0)
                        {
                            update.ConfigMftsCompressPrintSettingAnyTime = setNewAnyTime.Substring(1);
                        }
                        else
                        {
                            update.ConfigMftsCompressPrintSettingAnyTime = setNewAnyTime;
                        }

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
                    var update = _dbContext.configMftsCompressPrintSetting.Where(x => x.ConfigMftsCompressPrintSettingNo == param.Id).FirstOrDefault();

                    if (update != null)
                    {

                        var getOnetime = update.ConfigMftsCompressPrintSettingOneTime;

                        var splitOneTime = getOnetime.Split("|");

                        var setNewOneTime = "";

                        for (int i = 0; i < splitOneTime.Length; i++)
                        {
                            if (i != param.OneTimePosition)
                            {
                                setNewOneTime += "|" + splitOneTime[i];
                            }
                        }

                        update.ConfigMftsCompressPrintSettingOneTime = setNewOneTime.Substring(1);
                        update.ConfigMftsCompressPrintSettingNextTime = param.NextTime;

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
