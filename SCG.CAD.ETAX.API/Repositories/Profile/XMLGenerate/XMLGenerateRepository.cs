namespace SCG.CAD.ETAX.API.Repositories
{
    public class XMLGenerateRepository : IXMLGenerateRepository
    {
        XMLGenerateService service = new XMLGenerateService();

        public async Task<Response> ProcessXMLGenerate(string parttextfile)
        {
            Response resp = new Response();

            try
            {
                var result = service.ProcessXMLGenerate(parttextfile);

                resp = result;
            }
            catch (Exception ex)
            {
                resp.STATUS = false;
                resp.ERROR_MESSAGE = ex.Message.ToString();
            }

            return await Task.FromResult(resp);
        }
    }
}
