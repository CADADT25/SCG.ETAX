namespace SCG.CAD.ETAX.API.Repositories
{
    public interface IZipFileType
    {
        Task<Response> GET_LIST();
        Task<Response> GET_DETAIL(int id);
        Task<Response> INSERT(ZipFileType param);
        Task<Response> UPDATE(ZipFileType param);
        Task<Response> DELETE(ZipFileType param);
    }
}
