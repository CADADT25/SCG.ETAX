using SCG.CAD.ETAX.MODEL.CustomModel;
using SCG.CAD.ETAX.UTILITY.Controllers;

namespace SCG.CAD.ETAX.API.Repositories
{
    public class APISignRepository : IAPISignRepository
    {
        UtilityAPISignController signController = new UtilityAPISignController();
        APISignService aPISignService = new APISignService();
        public async Task<Response> SendPDFSign(APISendFilePDFSignModel data)
        {
            Response responseSignModel = new Response();
            try
            {
                responseSignModel = signController.SendFilePDFSign(data).Result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return responseSignModel;
        }

        public async Task<Response> SendXMLSign(APISendFileXMLSignModel data)
        {
            Response responseSignModel = new Response();
            try
            {
                responseSignModel = signController.SendFileXMLSign(data).Result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return responseSignModel;
        }
        public async Task<Response> SyncCertificate()
        {
            Response responseSignModel = new Response();
            try
            {
                responseSignModel = aPISignService.SyncCertificate().Result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return responseSignModel;
        }
    }
}
