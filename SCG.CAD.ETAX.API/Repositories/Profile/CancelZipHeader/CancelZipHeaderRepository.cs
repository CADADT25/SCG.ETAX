namespace SCG.CAD.ETAX.API.Repositories
{
    public class CancelZipHeaderRepository : ICancelZipHeaderRepository
    {
        CancelZipHeaderService service = new CancelZipHeaderService();

        public Task<Response> DELETE(MODEL.CancelZipHeader param)
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

        public Task<Response> INSERT(MODEL.CancelZipHeader param)
        {
            throw new NotImplementedException();
        }

        public Task<Response> UPDATE(MODEL.CancelZipHeader param)
        {
            throw new NotImplementedException();
        }
    }
}
