using SCG.CAD.ETAX.API.Services;
using SCG.CAD.ETAX.MODEL;
using static SCG.CAD.ETAX.MODEL.Revenue.ETDA.CodeList.Provice.ThaiISOCountrySubdivisionCodeModel;
using static SCG.CAD.ETAX.MODEL.Revenue.ETDA.CodeList.City.TISICityNameModel;
using static SCG.CAD.ETAX.MODEL.Revenue.ETDA.CodeList.SubDivision.TISICitySubDivisionNameModel;

namespace SCG.CAD.ETAX.API.Repositories
{
    public class ETDARepository : IETDARepository
    {
        ETDAService eTDAService = new ETDAService();

        public Task<Response> GetThaiDocumentNameCode()
        {
            throw new NotImplementedException();
        }

        public async Task<Response> GetThaiISOCountrySubdivisionCode()
        {
            List<ETDAProvice> result = new List<ETDAProvice>();

            Response resp = new Response();

            try
            {
                var getXml = eTDAService.ReadXmlThaiISOCountrySubdivisionCode();

                if (!string.IsNullOrEmpty(getXml))
                {
                    result = eTDAService.ProviceMappingToObject(getXml);

                    if (result.Count() > 0)
                    {
                        resp.STATUS = true;
                        resp.OUTPUT_DATA = result;
                    }
                }
            }

            catch (Exception ex)
            {
                resp.STATUS = false;
                resp.ERROR_MESSAGE = ex.InnerException.ToString();
            }

            return resp;
        }

        public async Task<Response> GetTISICityName(string ProviceCode)
        {
            List<ETDADistrict> result = new List<ETDADistrict>();

            Response resp = new Response();

            try
            {
                var getXml = eTDAService.ReadXmlTISICityName();

                if (!string.IsNullOrEmpty(getXml))
                {
                    result = eTDAService.DistrictMappingToObject(getXml, ProviceCode);

                    if (result.Count() > 0)
                    {
                        resp.STATUS = true;
                        resp.OUTPUT_DATA = result;
                    }
                }
            }

            catch (Exception ex)
            {
                resp.STATUS = false;
                resp.ERROR_MESSAGE = ex.InnerException.ToString();
            }

            return resp;
        }

        public async Task<Response> GetTISICitySubDivisionName(string DistrictCode)
        {
            List<ETDASubDistrict> result = new List<ETDASubDistrict>();

            Response resp = new Response();

            try
            {
                var getXml = eTDAService.ReadXmlTISICitySubDivisionName();

                if (!string.IsNullOrEmpty(getXml))
                {
                    result = eTDAService.SubDistrictMappingToObject(getXml, DistrictCode);

                    if (result.Count() > 0)
                    {
                        resp.STATUS = true;
                        resp.OUTPUT_DATA = result;
                    }
                }
            }

            catch (Exception ex)
            {
                resp.STATUS = false;
                resp.ERROR_MESSAGE = ex.InnerException.ToString();
            }

            return resp;
        }

        public Task<Response> GetThaiMessageFunctionCode()
        {
            throw new NotImplementedException();
        }

        public Task<Response> GetThaiPurposeCode_Invoice()
        {
            throw new NotImplementedException();
        }

        public Task<Response> GetThai_MessageFunctionCode()
        {
            throw new NotImplementedException();
        }


    }
}
