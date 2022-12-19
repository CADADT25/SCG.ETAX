using SCG.CAD.ETAX.MODEL.CustomModel;

namespace SCG.CAD.ETAX.API.Repositories
{
    public class PDFSignRepository : IPDFSignRepository
    {
        PDFSignService service = new PDFSignService();

        public async Task<Response> ProcessPDFSign(ConfigPdfSign configPdfSign, FilePDF filePDF)
        {
            Response resp = new Response();

            try
            {
                resp = service.ProcessPDFSign(configPdfSign, filePDF);
            }
            catch (Exception ex)
            {
                resp.STATUS = false;
                resp.ERROR_MESSAGE = ex.InnerException.Message.ToString();
            }

            return await Task.FromResult(resp);
        }
    }
}
