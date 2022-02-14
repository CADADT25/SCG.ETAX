namespace SCG.CAD.ETAX.API.Repositories
{
    public interface IZipFileConfigRepository
    {
        Task<Response> GET_LIST();
        Task<Response> GET_DETAIL(int id);
        Task<Response> INSERT(ZipFileConfig param);
        Task<Response> UPDATE(ZipFileConfig param);
        Task<Response> DELETE(ZipFileConfig param);
    }
}
