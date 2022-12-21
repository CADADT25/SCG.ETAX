namespace SCG.CAD.ETAX.API.Services
{
    public class ProfileBranchService
    {

        readonly DatabaseContext _dbContext = new();

        public DateTime dtNow = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd'" + "T" + "'HH:mm:ss.fff"));

        public Response GET_LIST()
        {
            Response resp = new Response();
            try
            {
                var getList = _dbContext.profileBranch.ToList();

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
                var getList = _dbContext.profileBranch.Where(x => x.ProfileBranchNo == id).ToList();

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

        public Response INSERT(ProfileBranch param)
        {
            Response resp = new Response();
            try
            {
                using (_dbContext)
                {
                    var getDuplicate = _dbContext.profileBranch.Where(x => x.ProfileBranchCode == param.ProfileBranchCode && x.ProfileCompanyCode == param.ProfileCompanyCode).ToList();

                    if (getDuplicate.Count <= 0)
                    {
                        param.CreateDate = dtNow;
                        param.UpdateDate = dtNow;

                        _dbContext.profileBranch.Add(param);
                        _dbContext.SaveChanges();


                        resp.STATUS = true;
                        resp.MESSAGE = "Insert success.";
                    }
                    else
                    {
                        resp.STATUS = false;
                        resp.ERROR_MESSAGE = "Can't insert new record becuase Branch Code is duplicate.";
                    }



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

        public Response UPDATE(ProfileBranch param)
        {
            Response resp = new Response();
            try
            {
                using (_dbContext)
                {
                    //var getDuplicate = _dbContext.profileBranch.Where(x => x.ProfileBranchCode == param.ProfileBranchCode && x.ProfileCompanyCode == param.ProfileCompanyCode).ToList();

                    //if (getDuplicate.Count <= 0)
                    //{

                    //}
                    //else
                    //{
                    //    resp.STATUS = false;
                    //    resp.ERROR_MESSAGE = "Can't insert new record becuase Branch Code is duplicate.";
                    //}

                    var update = _dbContext.profileBranch.Where(x => x.ProfileBranchNo == param.ProfileBranchNo).FirstOrDefault();

                    if (update != null)
                    {
                        update.ProfileBranchCode = param.ProfileBranchCode;
                        update.ProfileCompanyCode = param.ProfileCompanyCode;
                        update.ProfileBranchNameTh = param.ProfileBranchNameTh;
                        update.ProfileBranchNameEn = param.ProfileBranchNameEn;
                        update.ProfileBranchDescrition = param.ProfileBranchDescrition;
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

        public Response DELETE(ProfileBranch param)
        {
            Response resp = new Response();
            try
            {
                using (_dbContext)
                {
                    var delete = _dbContext.profileBranch.Find(param.ProfileBranchNo);

                    if (delete != null)
                    {
                        _dbContext.profileBranch.Remove(delete);
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

        public Response INSERTS(List<ProfileBranch> param)
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

                            _dbContext.profileBranch.Add(item);

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
                resp.INNER_EXCEPTION = ex.Message.ToString();
            }
            return resp;
        }

    }
}
