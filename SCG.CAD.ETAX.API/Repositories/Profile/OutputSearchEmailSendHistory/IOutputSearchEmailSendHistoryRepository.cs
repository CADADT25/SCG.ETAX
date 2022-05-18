namespace SCG.CAD.ETAX.API.Repositories
{
    public interface IOutputSearchEmailSendHistoryRepository
    {
        Task<Response> GET_LIST();
        Task<Response> GET_DETAIL(int id);
        Task<Response> INSERT(OutputSearchEmailSendHistory param);
        Task<Response> UPDATE(OutputSearchEmailSendHistory param);
        Task<Response> DELETE(OutputSearchEmailSendHistory param);
    }
}
