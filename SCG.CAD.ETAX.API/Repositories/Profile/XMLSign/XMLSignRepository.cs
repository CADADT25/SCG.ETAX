using SCG.CAD.ETAX.MODEL.CustomModel;

namespace SCG.CAD.ETAX.API.Repositories
{
    public class XMLSignRepository : IXMLSignRepository
    {
        XMLSignService service = new XMLSignService();

        public async Task<Response> ProcessXMLSign(ConfigXmlSign configXmlSign, FileXML filePDF)
        {
            Response resp = new Response();

            try
            {
                var result = service.ProcessXMLSign(configXmlSign, filePDF);

                resp = result;
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
