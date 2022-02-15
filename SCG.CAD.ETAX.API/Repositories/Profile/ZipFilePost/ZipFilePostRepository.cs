namespace SCG.CAD.ETAX.API.Repositories
{
    public class ZipFilePostRepository : IZipFilePostRepository
    {
        ZipFilePostService service = new ZipFilePostService();

        public Task<Response> DELETE(MODEL.ZipFilePost param)
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

        public Task<Response> INSERT(MODEL.ZipFilePost param)
        {
            throw new NotImplementedException();
        }

        public Task<Response> UPDATE(MODEL.ZipFilePost param)
        {
            throw new NotImplementedException();
        }
    }
}
