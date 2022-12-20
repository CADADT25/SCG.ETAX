using SCG.CAD.ETAX.MODEL.CustomModel;

namespace SCG.CAD.ETAX.API.Repositories
{
    public class XMLSignRepository : IXMLSignRepository
    {
        XMLSignService service = new XMLSignService();

        public async Task<Response> ProcessXMLFileSign(XMLSignModel xMLSignModel)
        {
            Response resp = new Response();

            try
            {
                resp = service.ProcessXMLFileSign(xMLSignModel);
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
