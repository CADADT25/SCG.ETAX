using SCG.CAD.ETAX.MODEL.CustomModel;
using SCG.CAD.ETAX.UTILITY.Controllers;

namespace SCG.CAD.ETAX.API.Repositories.Profile.ConnectHSM
{
    public class ConnectHSMRepository : IConnectHSMRepository
    {
        UtilityAPISignController utilityAPISignController = new UtilityAPISignController();
        public async Task<APIGetHSMSerialModel> GetHSMSerial(string hsmName)
        {
            APIGetHSMSerialModel resp = new APIGetHSMSerialModel();
            APIPutHSMSerialModel put = new APIPutHSMSerialModel();
            try
            {
                put.hsmName = hsmName;
                var jsonString = System.Text.Json.JsonSerializer.Serialize(put);
                resp = utilityAPISignController.GetHSMSerial(jsonString).Result;
            }
            catch (Exception ex)
            {
                resp.resultCode = "9999";
                resp.resultDes = ex.InnerException.Message.ToString();
            }
            return await Task.FromResult(resp);
        }

        public async Task<APIGetKeyAliasModel> GetKeyAlias(string hsmName, string hsmSerial)
        {
            APIGetKeyAliasModel resp = new APIGetKeyAliasModel();
            APIPutKeyAliasModel put = new APIPutKeyAliasModel();
            try
            {
                put.hsmName = hsmName;
                put.hsmSerial = hsmSerial;
                var jsonString = System.Text.Json.JsonSerializer.Serialize(put);
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
