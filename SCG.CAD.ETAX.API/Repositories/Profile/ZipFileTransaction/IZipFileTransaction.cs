namespace SCG.CAD.ETAX.API.Repositories
{
    public interface IZipFileTransaction
    {
        Task<Response> GET_LIST();
        Task<Response> GET_DETAIL(int id);
        Task<Response> INSERT(ZipFileTransaction param);
        Task<Response> UPDATE(ZipFileTransaction param);
        Task<Response> DELETE(ZipFileTransaction param);
    }
}
