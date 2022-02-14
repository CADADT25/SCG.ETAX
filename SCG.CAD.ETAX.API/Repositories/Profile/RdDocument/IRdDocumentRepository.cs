namespace SCG.CAD.ETAX.API.Repositories
{
    public interface IRdDocumentRepository
    {
        Task<Response> GET_LIST();
        Task<Response> GET_DETAIL(int id);
        Task<Response> INSERT(RdDocument param);
        Task<Response> UPDATE(RdDocument param);
        Task<Response> DELETE(RdDocument param);
    }
}
