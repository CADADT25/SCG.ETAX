namespace SCG.CAD.ETAX.API.Repositories
{
    public interface IOutputSearchEmailSendRepository
    {
        Task<Response> GET_LIST();
        Task<Response> GET_DETAIL(int id);
        Task<Response> INSERT(OutputSearchEmailSend param);
        Task<Response> UPDATE(OutputSearchEmailSend param);
        Task<Response> DELETE(OutputSearchEmailSend param);
        Task<Response> SEARCH(string JsonString);
        Task<Response> DOWNLOADZIPFILE(OutputSearchEmailSend param);

    }
}
