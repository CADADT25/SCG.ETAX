using SCG.CAD.ETAX.MODEL.CustomModel;
using SCG.CAD.ETAX.UTILITY.Controllers;

namespace SCG.CAD.ETAX.API.Repositories
{
    public class APISignRepository : IAPISignRepository
    {
        UtilityAPISignController signController = new UtilityAPISignController();
        public async Task<APIResponseSignModel> SendPDFSign(APISendFilePDFSignModel data)
        {
            APIResponseSignModel responseSignModel = new APIResponseSignModel();
            try
            {
                var json = System.Text.Json.JsonSerializer.Serialize(data);
                responseSignModel = signController.SendFileXMLSign(json).Result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return responseSignModel;
        }

        public async Task<APIResponseSignModel> SendXMLSign(APISendFileXMLSignModel data)
        {
            APIResponseSignModel responseSignModel = new APIResponseSignModel();
            try
            {
                var json = System.Text.Json.JsonSerializer.Serialize(data);
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
