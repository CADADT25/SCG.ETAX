using SCG.CAD.ETAX.MODEL.CustomModel;

namespace SCG.CAD.ETAX.API.Repositories.Profile.ConnectHSM
{
    public interface IConnectHSMRepository
    {
        Task<APIGetHSMSerialModel> GetHSMSerial(string hsmName);
        Task<APIGetKeyAliasModel> GetKeyAlias(string hsmName, string hsmSerial);
    }
}
