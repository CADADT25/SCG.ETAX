namespace SCG.CAD.ETAX.API.Repositories
{
    public interface IConfigControlFunctionRepository
    {
        Task<Response> GET_LIST();
        Task<Response> GET_DETAIL(int id);
        Task<Response> INSERT(ConfigControlFunction param);
        Task<Response> UPDATE(ConfigControlFunction param);
        Task<Response> DELETE(ConfigControlFunction param);
    }
}
