namespace SCG.CAD.ETAX.API.Repositories
{
    public interface IRdDocument
    {
        Task<Response> GET_LIST();
        Task<Response> GET_DETAIL(int id);
        Task<Response> INSERT(RdDocument param);
        Task<Response> UPDATE(RdDocument param);
        Task<Response> DELETE(RdDocument param);
    }
}
