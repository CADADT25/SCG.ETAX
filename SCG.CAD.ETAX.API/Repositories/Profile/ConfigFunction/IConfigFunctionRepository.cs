namespace SCG.CAD.ETAX.API.Repositories
{
    public interface IConfigFunctionRepository
    {
        Task<Response> GET_LIST();
        Task<Response> GET_DETAIL(int id);
        Task<Response> INSERT(ConfigFunction param);
        Task<Response> UPDATE(ConfigFunction param);
        Task<Response> DELETE(ConfigFunction param);
    }
}
