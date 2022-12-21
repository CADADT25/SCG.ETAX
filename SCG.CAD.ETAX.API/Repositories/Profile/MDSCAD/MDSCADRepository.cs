namespace SCG.CAD.ETAX.API.Repositories
{
    public class MDSCADRepository : IMDSCADRepository
    {
        MDSCADService service = new MDSCADService();
        public async Task<Response> GET_MANAGER_BY_USER(string user)
        {
            Response resp = new Response();
            try
            {
                var result = service.GET_MANAGER_BY_USER(user);

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
