namespace SCG.CAD.ETAX.API.Repositories
{
    public interface IConfigControlMenuRepository
    {
        Task<Response> GET_LIST();
        Task<Response> GET_DETAIL(int id);
        Task<Response> INSERT(ConfigControlMenu param);
        Task<Response> UPDATE(ConfigControlMenu param);
        Task<Response> DELETE(ConfigControlMenu param);
    }
}
