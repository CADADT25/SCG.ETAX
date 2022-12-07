using SCG.CAD.ETAX.MODEL.CustomModel;

namespace SCG.CAD.ETAX.API.Repositories
{
    public interface IAPISignRepository
    {
        Task<APIResponseSignModel> SendPDFSign(APISendFilePDFSignModel data);
        Task<APIResponseSignModel> SendXMLSign(APISendFileXMLSignModel data);
    }
}
