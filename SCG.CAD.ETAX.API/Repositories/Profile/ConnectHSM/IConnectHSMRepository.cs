using SCG.CAD.ETAX.MODEL.CustomModel;

namespace SCG.CAD.ETAX.API.Repositories.Profile.ConnectHSM
{
    public interface IConnectHSMRepository
    {
        Task<APIGetHSMSerialModel> GetHSMSerial(string jsonString);
        Task<APIGetKeyAliasModel> GetKeyAlias(string jsonString);
    }
}
