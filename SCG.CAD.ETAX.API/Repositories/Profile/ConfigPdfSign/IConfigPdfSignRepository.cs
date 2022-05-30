namespace SCG.CAD.ETAX.API.Repositories
{
    public interface IConfigPdfSignRepository
    {
        Task<Response> GET_LIST();
        Task<Response> GET_DETAIL(int id);
        Task<Response> INSERT(ConfigPdfSign param);
        Task<Response> UPDATE(ConfigPdfSign param);
        Task<Response> DELETE(ConfigPdfSign param);
        Task<Response> UPDATE_ONETIME(ConfigPdfSign param);
        Task<Response> UPDATE_ANYTIME(ConfigPdfSign param);
        Task<Response> DELETE_ONETIME(DeleteOnetime param);
        Task<Response> DELETE_ANYTIME(DeleteAnytime param);
        Task<Response> UPDATE_NEXTTIME(ConfigNextTime param);
    }
}
