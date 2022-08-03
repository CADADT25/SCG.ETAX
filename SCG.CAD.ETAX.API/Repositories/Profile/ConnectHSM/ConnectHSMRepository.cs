using SCG.CAD.ETAX.MODEL.CustomModel;
using SCG.CAD.ETAX.UTILITY.Controllers;

namespace SCG.CAD.ETAX.API.Repositories.Profile.ConnectHSM
{
    public class ConnectHSMRepository : IConnectHSMRepository
    {
        UtilityAPISignController utilityAPISignController = new UtilityAPISignController();
        public async Task<APIGetHSMSerialModel> GetHSMSerial(string jsonString)
        {
            APIGetHSMSerialModel resp = new APIGetHSMSerialModel();
            try
            {
                resp = utilityAPISignController.GetHSMSerial(jsonString).Result;
            }
            catch (Exception ex)
            {
                resp.resultCode = "9999";
                resp.resultDes = ex.InnerException.Message.ToString();
            }
            return await Task.FromResult(resp);
        }

        public async Task<APIGetKeyAliasModel> GetKeyAlias(string jsonString)
        {
            APIGetKeyAliasModel resp = new APIGetKeyAliasModel();
            try
            {
                resp = utilityAPISignController.GetKeyAlias(jsonString).Result;
            }
            catch (Exception ex)
            {
                resp.resultCode = "9999";
                resp.resultDes = ex.InnerException.Message.ToString();
            }
            return await Task.FromResult(resp);
        }
    }
}
