namespace SCG.CAD.ETAX.API.Repositories
{
    public interface IConfigPdfSignRepository
    {
        Task<Response> GET_LIST();
        Task<Response> GET_DETAIL(int id);
        Task<Response> INSERT(ConfigPdfSign param);
        Task<Response> UPDATE(ConfigPdfSign param);
        Task<Response> DELETE(ConfigPdfSign param);
    }
}
