namespace SCG.CAD.ETAX.API.Repositories
{
    public interface IConfigGlobalCategoryRepository
    {
        Task<Response> GET_LIST();
        Task<Response> GET_DETAIL(int id);
        Task<Response> INSERT(ConfigGlobalCategory param);
        Task<Response> UPDATE(ConfigGlobalCategory param);
        Task<Response> DELETE(ConfigGlobalCategory param);
    }
}
