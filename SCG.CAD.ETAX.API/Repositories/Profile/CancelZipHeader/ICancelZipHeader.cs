namespace SCG.CAD.ETAX.API.Repositories
{
    public interface ICancelZipHeader
    {
        Task<Response> GET_LIST();
        Task<Response> GET_DETAIL(int id);
        Task<Response> INSERT(CancelZipHeader param);
        Task<Response> UPDATE(CancelZipHeader param);
        Task<Response> DELETE(CancelZipHeader param);
    }
}
