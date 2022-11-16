namespace SCG.CAD.ETAX.API.Repositories
{
    public interface IConfigGlobalRepository
    {
        Task<Response> GET_LIST();
        Task<Response> GET_DETAIL(int id);
        Task<Response> GET_DETAIL_BY_NAME(string cate, string name);
        Task<Response> INSERT(ConfigGlobal param);
        Task<Response> UPDATE(ConfigGlobal param);
        Task<Response> DELETE(ConfigGlobal param);
    }
}
