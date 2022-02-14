namespace SCG.CAD.ETAX.API.Repositories
{
    public interface IZipFileConfig
    {
        Task<Response> GET_LIST();
        Task<Response> GET_DETAIL(int id);
        Task<Response> INSERT(ZipFileConfig param);
        Task<Response> UPDATE(ZipFileConfig param);
        Task<Response> DELETE(ZipFileConfig param);
    }
}
