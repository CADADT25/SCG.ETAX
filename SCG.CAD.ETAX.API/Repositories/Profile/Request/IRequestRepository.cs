namespace SCG.CAD.ETAX.API.Repositories
{
    public interface IRequestRepository
    {
        Task<Response> GET_LIST();
        Task<Response> INSERT(Request param);
        Task<Response> UPDATE(Request param);
        Task<Response> DELETE(Request param);
    }
}
