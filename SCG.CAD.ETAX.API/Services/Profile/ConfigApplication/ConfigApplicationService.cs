using System.Collections;
using System.Globalization;

namespace SCG.CAD.ETAX.API.Services
{
    public class ConfigApplicationService
    {
        readonly DatabaseContext _dbContext = new();
        public Response CHECK_KEY(string key)
        {
            Response resp = new Response();

            try
            {
                var getList = _dbContext.configApplication.Where(x => x.SecretKey == key && x.IsActive).ToList();

                if (getList.Count > 0)
                {
                    resp.STATUS = true;
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
    }
}
