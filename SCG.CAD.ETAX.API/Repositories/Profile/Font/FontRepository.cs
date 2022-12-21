namespace SCG.CAD.ETAX.API.Repositories
{
    public class FontRepository : IFontRepository
    {
        FontService service = new FontService();
        public async Task<Response> GET_LIST()
        {
            Response resp = new Response();

            try
            {
                var result = service.GET_LIST();

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
