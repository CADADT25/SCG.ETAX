namespace SCG.CAD.ETAX.API.Repositories
{
    public interface IAuthenticationRepository
    {
        Task<Response> GET_DETAIL(string Username, string Password);
        Task<Response> GET_MENU(string Username);
    }
}
