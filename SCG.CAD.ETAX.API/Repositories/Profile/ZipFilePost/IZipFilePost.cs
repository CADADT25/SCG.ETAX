namespace SCG.CAD.ETAX.API.Repositories
{
    public interface IZipFilePost
    {
        Task<Response> GET_LIST();
        Task<Response> GET_DETAIL(int id);
        Task<Response> INSERT(ZipFilePost param);
        Task<Response> UPDATE(ZipFilePost param);
        Task<Response> DELETE(ZipFilePost param);
    }
}
