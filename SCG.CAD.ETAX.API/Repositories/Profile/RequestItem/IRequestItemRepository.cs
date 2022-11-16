namespace SCG.CAD.ETAX.API.Repositories
{
    public interface IRequestPathRepository
    {
        Task<Response> GET_LIST(Guid id);
        Task<Response> GET_LIST_BY_STATUS(List<string> param);
        Task<Response> INSERT(RequestPath param);
        Task<Response> UPDATE(RequestPath param);
        Task<Response> DELETE(RequestPath param);
    }
}
