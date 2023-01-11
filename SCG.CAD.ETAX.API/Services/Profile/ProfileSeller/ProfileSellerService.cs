using static SCG.CAD.ETAX.MODEL.Revenue.ETDA.CodeList.City.TISICityNameModel;
using static SCG.CAD.ETAX.MODEL.Revenue.ETDA.CodeList.Provice.ThaiISOCountrySubdivisionCodeModel;
using static SCG.CAD.ETAX.MODEL.Revenue.ETDA.CodeList.SubDivision.TISICitySubDivisionNameModel;

namespace SCG.CAD.ETAX.API.Services
{
    public class ProfileSellerService
    {

        ETDAService eTDAService = new ETDAService();
        readonly DatabaseContext _dbContext = new();

        public DateTime dtNow = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd'" + "T" + "'HH:mm:ss.fff"));

        public Response GET_LIST()
        {
            Response resp = new Response();
            try
            {
                var getList = _dbContext.profileSeller.ToList();

                if (getList.Count > 0)
                {
                    //List<ProfileSeller> list = new List<ProfileSeller>();

                    //var Provice = GetProvice();
                    //var District = GetDistrict();
                    //var SubDivision = GetSubDivision();


                    //list = getList.Select(x => new ProfileSeller
                    //{
                    //    SellerNo = x.SellerNo,
                    //    CompanyCode = x.CompanyCode,
                    //    BranchCode = x.BranchCode,
                    //    Province = Provice.FirstOrDefault(y=> x.Province == y.ProviceCode).ProviceName,
                    //    District = District.FirstOrDefault(y => x.District == y.districtCode).districtName,
                    //    SubDistrict = SubDivision.FirstOrDefault(y => x.SubDistrict == y.subDistrictCode).subDistrictName,
                    //    Road = x.Road,
                    //    Building = x.Building,
                    //    Addressnumber = x.Addressnumber,
                    //    SellerEmail = x.SellerEmail,
                    //    CreateBy = x.CreateBy,
                    //    CreateDate = x.CreateDate,
                    //    UpdateBy = x.UpdateBy,
                    //    UpdateDate = x.UpdateDate,
                    //    Isactive = x.Isactive,
                    //    EmailTemplateNo = x.EmailTemplateNo
                    //}).ToList();

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
                var getList = _dbContext.profileSeller.Where(x => x.SellerNo == id).ToList();

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

        public Response INSERT(ProfileSeller param)
        {
            Response resp = new Response();
            try
            {
                using (_dbContext)
                {
                    var getDuplicate = _dbContext.profileSeller.Where(x => x.CompanyCode == param.CompanyCode && x.BranchCode == param.BranchCode).ToList();

                    if (getDuplicate.Count <= 0)
                    {
                        param.CreateDate = dtNow;
                        param.UpdateDate = dtNow;

                        param.SellerEmail = param.SellerEmail.Replace("[", "").Replace("]", "").Replace("\"", "");

                        _dbContext.profileSeller.Add(param);
                        _dbContext.SaveChanges();


                        resp.STATUS = true;
                        resp.MESSAGE = "Insert success.";
                    }
                    else
                    {
                        resp.STATUS = false;
                        resp.ERROR_MESSAGE = "Can't insert new record becuase Company Code is duplicate.";
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

        public Response UPDATE(ProfileSeller param)
        {
            Response resp = new Response();
            try
            {
                using (_dbContext)
                {
                    var update = _dbContext.profileSeller.Where(x => x.SellerNo == param.SellerNo).FirstOrDefault();

                    if (update != null)
                    {
                        update.CompanyCode = param.CompanyCode;
                        update.BranchCode = param.BranchCode;
                        update.Province = param.Province;
                        update.District = param.District;
                        update.SubDistrict = param.SubDistrict;
                        update.Road = param.Road;
                        update.Building = param.Building;
                        update.Addressnumber = param.Addressnumber;
                        update.SellerEmail = param.SellerEmail;
                        update.EmailTemplateNo = param.EmailTemplateNo;
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

        public Response DELETE(ProfileSeller param)
        {
            Response resp = new Response();
            try
            {
                using (_dbContext)
                {
                    var delete = _dbContext.profileSeller.Find(param.SellerNo);

                    if (delete != null)
                    {
                        _dbContext.profileSeller.Remove(delete);
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

        public Response GET_LIST_Detail()
        {
            Response resp = new Response();
            try
            {
                var getList = _dbContext.profileSeller.ToList();

                if (getList.Count > 0)
                {
                    List<ProfileSeller> list = new List<ProfileSeller>();

                    var Provice = GetProvice();
                    var District = GetDistrict();
                    var SubDivision = GetSubDivision();


                    list = getList.Select(x => new ProfileSeller
                    {
                        SellerNo = x.SellerNo,
                        CompanyCode = x.CompanyCode,
                        BranchCode = x.BranchCode,
                        Province = Provice.FirstOrDefault(y => x.Province == y.ProviceCode) != null ? Provice.FirstOrDefault(y => x.Province == y.ProviceCode).ProviceName : "",
                        District = District.FirstOrDefault(y => x.District == y.districtCode) != null ? District.FirstOrDefault(y => x.District == y.districtCode).districtName : "",
                        SubDistrict = SubDivision.FirstOrDefault(y => x.SubDistrict == y.subDistrictCode) != null ? SubDivision.FirstOrDefault(y => x.SubDistrict == y.subDistrictCode).subDistrictName : "",
                        Road = x.Road,
                        Building = x.Building,
                        Addressnumber = x.Addressnumber,
                        SellerEmail = x.SellerEmail,
                        CreateBy = x.CreateBy,
                        CreateDate = x.CreateDate,
                        UpdateBy = x.UpdateBy,
                        UpdateDate = x.UpdateDate,
                        Isactive = x.Isactive,
                        EmailTemplateNo = x.EmailTemplateNo
                    }).ToList();

                    resp.STATUS = true;
                    resp.MESSAGE = "Get list count '" + list.Count + "' records. ";
                    resp.OUTPUT_DATA = list;
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

        public List<ETDAProvice> GetProvice()
        {
            List<ETDAProvice> result = new List<ETDAProvice>();

            try
            {
                var getXml = eTDAService.ReadXmlThaiISOCountrySubdivisionCode();

                if (!string.IsNullOrEmpty(getXml))
                {
                    result = eTDAService.ProviceMappingToObject(getXml);
                }
            }

            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        public List<ETDADistrict> GetDistrict()
        {
            List<ETDADistrict> result = new List<ETDADistrict>();

            try
            {
                var getXml = eTDAService.ReadXmlTISICityName();

                if (!string.IsNullOrEmpty(getXml))
                {
                    result = eTDAService.DistrictMappingToObject(getXml);
                }
            }

            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        public List<ETDASubDistrict> GetSubDivision()
        {
            List<ETDASubDistrict> result = new List<ETDASubDistrict>();

            Response resp = new Response();

            try
            {
                var getXml = eTDAService.ReadXmlTISICitySubDivisionName();

                if (!string.IsNullOrEmpty(getXml))
                {
                    result = eTDAService.SubDistrictMappingToObject(getXml);
                }
            }

            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }
    }
}
