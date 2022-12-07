using SCG.CAD.ETAX.MODEL.CustomModel;

namespace SCG.CAD.ETAX.API.Repositories
{
    public interface ISignDocumentRepository
    {
        Task<Response> Sign(SignDocumentRequest req);
    }
}
