using SCG.CAD.ETAX.MODEL.CustomModel;

namespace SCG.CAD.ETAX.API.Repositories
{
    public interface IXMLSignRepository
    {
        Task<Response> ProcessXMLFileSign(XMLSignModel xMLSignModel);
    }
}
