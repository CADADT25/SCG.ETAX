namespace SCG.CAD.ETAX.API.Repositories
{
    public class ZipFileTypeRepository : IZipFileTypeRepository
    {
        ZipFileTypeService service = new ZipFileTypeService();

        public Task<Response> DELETE(ZipFileType param)
        {
            throw new NotImplementedException();
        }

        public Task<Response> GET_DETAIL(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Response> GET_LIST()
        {
            throw new NotImplementedException();
        }

        public Task<Response> INSERT(ZipFileType param)
        {
            throw new NotImplementedException();
        }

        public Task<Response> UPDATE(ZipFileType param)
        {
            throw new NotImplementedException();
        }
    }
}
