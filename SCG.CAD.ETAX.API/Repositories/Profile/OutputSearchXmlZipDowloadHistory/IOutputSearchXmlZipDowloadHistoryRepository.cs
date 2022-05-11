namespace SCG.CAD.ETAX.API.Repositories
{
    public interface IOutputSearchXmlZipDowloadHistoryRepository
    {
        Task<Response> GET_LIST();
        Task<Response> GET_DETAIL(int id);
        Task<Response> INSERT(OutputSearchXmlZipDowloadHistory param);
        Task<Response> UPDATE(OutputSearchXmlZipDowloadHistory param);
        Task<Response> DELETE(OutputSearchXmlZipDowloadHistory param);
    }
}
