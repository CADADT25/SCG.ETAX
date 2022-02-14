namespace SCG.CAD.ETAX.API.Repositories
{
    public interface IDocumentCode
    {
        Task<Response> GET_LIST();
        Task<Response> GET_DETAIL(int id);
        Task<Response> INSERT(DocumentCode param);
        Task<Response> UPDATE(DocumentCode param);
        Task<Response> DELETE(DocumentCode param);
    }
}
