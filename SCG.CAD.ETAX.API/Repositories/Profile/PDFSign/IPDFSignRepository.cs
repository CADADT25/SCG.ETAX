using SCG.CAD.ETAX.MODEL.CustomModel;

namespace SCG.CAD.ETAX.API.Repositories
{
    public interface IPDFSignRepository
    {
        Task<Response> ProcessPDFSign(ConfigPdfSign configPdfSign, FilePDF filePDF);
    }
}
