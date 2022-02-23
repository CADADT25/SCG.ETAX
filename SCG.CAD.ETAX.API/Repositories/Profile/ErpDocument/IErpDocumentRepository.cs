namespace SCG.CAD.ETAX.API.Repositories
{
    public interface IErpDocumentRepository
    {
        Task<Response> GET_LIST();
        Task<Response> GET_DETAIL(int id);
        Task<Response> INSERT(ErpDocument param);
        Task<Response> UPDATE(ErpDocument param);
        Task<Response> DELETE(ErpDocument param);
    }
}
