namespace SCG.CAD.ETAX.API.Repositories
{
    public interface ICancelZipLine
    {
        Task<Response> GET_LIST();
        Task<Response> GET_DETAIL(int id);
        Task<Response> INSERT(CancelZipLine param);
        Task<Response> UPDATE(CancelZipLine param);
        Task<Response> DELETE(CancelZipLine param);
    }
}
