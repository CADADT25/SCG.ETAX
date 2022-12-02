using SCG.CAD.ETAX.MODEL.CustomModel;

namespace SCG.CAD.ETAX.API.Repositories
{
    public interface IAPISignRepository
    {
        Task<APIResponseSignModel> SendPDFSign(string json);
        Task<APIResponseSignModel> SendXMLSign(string json);
    }
}
