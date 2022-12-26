using SCG.CAD.ETAX.MODEL.CustomModel;

namespace SCG.CAD.ETAX.API.Repositories
{
    public interface IAPISignRepository
    {
        Task<Response> SendPDFSign(APISendFilePDFSignModel data);
        Task<Response> SendXMLSign(APISendFileXMLSignModel data);
        Task<Response> SyncCertificate();
    }
}
