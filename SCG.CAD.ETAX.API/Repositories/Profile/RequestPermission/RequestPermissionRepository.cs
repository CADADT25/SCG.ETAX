namespace SCG.CAD.ETAX.API.Repositories
{
    public class RequestPermissionRepository : IRequestPermissionRepository
    {
        RequestPermissionService service = new RequestPermissionService();
        public async Task<Response> GET_ROLES_AND_COMPANYS(string user)
        {
            Response resp = new Response();
            try
            {
                var result = service.GET_ROLES_AND_COMPANYS(user);

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
