


using SCG.CAD.ETAX.UTILITY;

namespace SCG.CAD.ETAX.API.Services
{
    public class ConfigMftsIndexGenerationSettingOutputService
    {
        readonly DatabaseContext _dbContext = new();

        public DateTime dtNow = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd'" + "T" + "'HH:mm:ss.fff"));

        public Response GET_LIST()
        {
            Response resp = new Response();
            try
            {
                var getList = _dbContext.configMftsIndexGenerationSettingOutput.ToList();

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
                var getList = _dbContext.configMftsIndexGenerationSettingOutput.Where(x => x.ConfigMftsIndexGenerationSettingOutputNo == id).ToList();

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

        public Response INSERT(ConfigMftsIndexGenerationSettingOutput param)
        {
            Response resp = new Response();
            try
            {
                using (_dbContext)
                {
                    if (!string.IsNullOrEmpty(param.ConfigMftsIndexGenerationSettingOutputPassword))
                    {
                        EncodeHelper helper = new EncodeHelper();
                        param.ConfigMftsIndexGenerationSettingOutputPassword = helper.Base64Encode(param.ConfigMftsIndexGenerationSettingOutputPassword);
                    }
                    param.CreateDate = dtNow;
                    param.UpdateDate = dtNow;

                    _dbContext.configMftsIndexGenerationSettingOutput.Add(param);
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

        public Response UPDATE(ConfigMftsIndexGenerationSettingOutput param)
        {
            Response resp = new Response();
            try
            {
                using (_dbContext)
                {
                    var update = _dbContext.configMftsIndexGenerationSettingOutput.Where(x => x.ConfigMftsIndexGenerationSettingOutputNo == param.ConfigMftsIndexGenerationSettingOutputNo).FirstOrDefault();

                    if (update != null)
                    {
                        update.ConfigMftsIndexGenerationSettingOutputCompanyCode = param.ConfigMftsIndexGenerationSettingOutputCompanyCode;
                        update.ConfigMftsIndexGenerationSettingOutputSourceName = param.ConfigMftsIndexGenerationSettingOutputSourceName;
                        update.ConfigMftsIndexGenerationSettingOutputType = param.ConfigMftsIndexGenerationSettingOutputType;
                        update.ConfigMftsIndexGenerationSettingOutputFolder = param.ConfigMftsIndexGenerationSettingOutputFolder;
                        update.ConfigMftsIndexGenerationSettingOutputLogReceiveType = param.ConfigMftsIndexGenerationSettingOutputLogReceiveType;
                        update.ConfigMftsIndexGenerationSettingOutputLogReceiveFolder = param.ConfigMftsIndexGenerationSettingOutputLogReceiveFolder;
                        update.ConfigMftsIndexGenerationSettingOutputHost = param.ConfigMftsIndexGenerationSettingOutputHost;
                        update.ConfigMftsIndexGenerationSettingOutputPort = param.ConfigMftsIndexGenerationSettingOutputPort;
                        update.ConfigMftsIndexGenerationSettingOutputUsername = param.ConfigMftsIndexGenerationSettingOutputUsername;

                        if (!string.IsNullOrEmpty(param.ConfigMftsIndexGenerationSettingOutputPassword))
                        {
                            if (update.ConfigMftsIndexGenerationSettingOutputPassword != param.ConfigMftsIndexGenerationSettingOutputPassword)
                            {
                                EncodeHelper helper = new EncodeHelper();
                                update.ConfigMftsIndexGenerationSettingOutputPassword = helper.Base64Encode(param.ConfigMftsIndexGenerationSettingOutputPassword);
                            }
                        }

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

        public Response DELETE(ConfigMftsIndexGenerationSettingOutput param)
        {
            Response resp = new Response();
            try
            {
                using (_dbContext)
                {
                    var delete = _dbContext.configMftsIndexGenerationSettingOutput.Find(param.ConfigMftsIndexGenerationSettingOutputNo);

                    if (delete != null)
                    {
                        _dbContext.configMftsIndexGenerationSettingOutput.Remove(delete);
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



        public Response UPDATE_ONETIME(ConfigMftsIndexGenerationSettingOutput param)
        {
            Response resp = new Response();
            try
            {
                using (_dbContext)
                {
                    var update = _dbContext.configMftsIndexGenerationSettingOutput.Where(x => x.ConfigMftsIndexGenerationSettingOutputNo == param.ConfigMftsIndexGenerationSettingOutputNo).FirstOrDefault();

                    if (update != null)
                    {
                        var setNewOnetime = "";

                        setNewOnetime += "|" + param.ConfigMftsIndexGenerationSettingOutputOneTime;

                        var findFirstIndex = setNewOnetime.Substring(0, 1);

                        if (findFirstIndex == "|")
                        {
                            setNewOnetime = setNewOnetime.Substring(1);
                        }

                        var GetOldValue = update.ConfigMftsIndexGenerationSettingOutputOneTime;

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

                        update.ConfigMftsIndexGenerationSettingOutputOneTime = setSort;

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

        public Response UPDATE_ANYTIME(ConfigMftsIndexGenerationSettingOutput param)
        {
            Response resp = new Response();
            try
            {
                using (_dbContext)
                {
                    var update = _dbContext.configMftsIndexGenerationSettingOutput.Where(x => x.ConfigMftsIndexGenerationSettingOutputNo == param.ConfigMftsIndexGenerationSettingOutputNo).FirstOrDefault();

                    if (update != null)
                    {

                        var setNewAnytime = "";

                        setNewAnytime += "|" + param.ConfigMftsIndexGenerationSettingOutputAnyTime;

                        var findFirstIndex = setNewAnytime.Substring(0, 1);

                        if (findFirstIndex == "|")
                        {
                            setNewAnytime = setNewAnytime.Substring(1, 5);
                        }


                        var GetOldValue = update.ConfigMftsIndexGenerationSettingOutputAnyTime;

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

                        update.ConfigMftsIndexGenerationSettingOutputAnyTime = setSort;


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

        public Response DELETE_ONETIME(DeleteOnetime param)
        {
            Response resp = new Response();
            try
            {
                using (_dbContext)
                {
                    var update = _dbContext.configMftsIndexGenerationSettingOutput.Where(x => x.ConfigMftsIndexGenerationSettingOutputNo == param.pk).FirstOrDefault();

                    if (update != null)
                    {

                        var getOnetime = update.ConfigMftsIndexGenerationSettingOutputOneTime;

                        var splitOneTime = getOnetime.Split("|");

                        var setNewOneTime = "";

                        for (int i = 0; i < splitOneTime.Length; i++)
                        {
                            if (i != param.position)
                            {
                                setNewOneTime += "|" + splitOneTime[i];
                            }
                        }

                        update.ConfigMftsIndexGenerationSettingOutputOneTime = setNewOneTime.Substring(1);

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

        public Response DELETE_ANYTIME(DeleteAnytime param)
        {
            Response resp = new Response();
            try
            {
                using (_dbContext)
                {
                    var update = _dbContext.configMftsIndexGenerationSettingOutput.Where(x => x.ConfigMftsIndexGenerationSettingOutputNo == param.pk).FirstOrDefault();

                    if (update != null)
                    {

                        var getAnyTime = update.ConfigMftsIndexGenerationSettingOutputAnyTime;

                        var splitAnyTime = getAnyTime.Split("|");

                        var setNewAnyTime = "";

                        for (int i = 0; i < splitAnyTime.Length; i++)
                        {
                            if (i != param.position)
                            {
                                setNewAnyTime += "|" + splitAnyTime[i];
                            }
                        }

                        update.ConfigMftsIndexGenerationSettingOutputAnyTime = setNewAnyTime.Substring(1);

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

        public Response UPDATE_NEXTTIME(ConfigNextTime param)
        {
            var setNewOneTime = "";
            Response resp = new Response();
            try
            {
                using (_dbContext)
                {
                    var update = _dbContext.configMftsIndexGenerationSettingOutput.Where(x => x.ConfigMftsIndexGenerationSettingOutputNo == param.Id).FirstOrDefault();

                    if (update != null)
                    {
                        if (!string.IsNullOrEmpty(update.ConfigMftsIndexGenerationSettingOutputOneTime))
                        {
                            var getOnetime = update.ConfigMftsIndexGenerationSettingOutputOneTime;

                            var splitOneTime = getOnetime.Split("|");


                            for (int i = 0; i < splitOneTime.Length; i++)
                            {
                                if (i != param.OneTimePosition)
                                {
                                    setNewOneTime += "|" + splitOneTime[i];
                                }
                            }
                            setNewOneTime = setNewOneTime.Substring(1);
                        }

                        update.ConfigMftsIndexGenerationSettingOutputOneTime = setNewOneTime;
                        update.ConfigMftsIndexGenerationSettingOutputNextTime = param.NextTime;

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


    }
}
