namespace SCG.CAD.ETAX.API.Repositories
{
    public class ZipFileTransactionRepository : IZipFileTransactionRepository
    {
        ZipFileTransactionService service = new ZipFileTransactionService();

        public Task<Response> DELETE(MODEL.ZipFileTransaction param)
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

        public Task<Response> INSERT(MODEL.ZipFileTransaction param)
        {
            throw new NotImplementedException();
        }

        public Task<Response> UPDATE(MODEL.ZipFileTransaction param)
        {
            throw new NotImplementedException();
        }
    }
}
