namespace SCG.CAD.ETAX.API.Repositories
{
    public class RdDocumentRepository : IRdDocumentRepository
    {
        RdDocumentService service = new RdDocumentService();

        public Task<Response> DELETE(MODEL.RdDocument param)
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

        public Task<Response> INSERT(MODEL.RdDocument param)
        {
            throw new NotImplementedException();
        }

        public Task<Response> UPDATE(MODEL.RdDocument param)
        {
            throw new NotImplementedException();
        }
    }
}
