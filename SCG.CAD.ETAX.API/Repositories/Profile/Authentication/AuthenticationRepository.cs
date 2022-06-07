namespace SCG.CAD.ETAX.API.Repositories
{
    public class AuthenticationRepository : IAuthenticationRepository
    {
        AuthenticationService service = new AuthenticationService();

        public async Task<Response> GET_DETAIL(string Username, string Password)
        {
            Response resp = new Response();

            try
            {
                var result = service.GET_DETAIL(Username,Password);

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
