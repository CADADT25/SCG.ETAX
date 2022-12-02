using SCG.CAD.ETAX.MODEL.CustomModel;
using SCG.CAD.ETAX.UTILITY.Controllers;

namespace SCG.CAD.ETAX.API.Repositories
{
    public class APISignRepository : IAPISignRepository
    {
        UtilityAPISignController signController = new UtilityAPISignController();
        public async Task<APIResponseSignModel> SendPDFSign(string json)
        {
            APIResponseSignModel responseSignModel = new APIResponseSignModel();
            try
            {
                responseSignModel = signController.SendFileXMLSign(json).Result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return responseSignModel;
        }

        public async Task<APIResponseSignModel> SendXMLSign(string json)
        {
            APIResponseSignModel responseSignModel = new APIResponseSignModel();
            try
            {
                responseSignModel = signController.SendFileXMLSign(json).Result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return responseSignModel;
        }
    }
}
