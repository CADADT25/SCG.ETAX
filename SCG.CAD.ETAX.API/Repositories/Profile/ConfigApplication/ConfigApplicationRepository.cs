namespace SCG.CAD.ETAX.API.Repositories
{
    public class ConfigApplicationRepository : IConfigApplicationRepository
    {
        ConfigApplicationService service = new ConfigApplicationService();

        public async Task<Response> CHECK_KEY(string key)
        {
            Response resp = new Response();

            try
            {
                var result = service.CHECK_KEY(key);

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
