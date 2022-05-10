namespace SCG.CAD.ETAX.API.Repositories
{
    public interface IOutputSearchPrintingDowloadHistoryRepository
    {
        Task<Response> GET_LIST();
        Task<Response> GET_DETAIL(int id);
        Task<Response> INSERT(OutputSearchPrintingDowloadHistory param);
        Task<Response> UPDATE(OutputSearchPrintingDowloadHistory param);
        Task<Response> DELETE(OutputSearchPrintingDowloadHistory param);
    }
}
