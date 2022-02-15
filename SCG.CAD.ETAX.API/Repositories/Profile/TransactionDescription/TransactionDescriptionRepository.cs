namespace SCG.CAD.ETAX.API.Repositories
{
    public class TransactionDescriptionRepository : ITransactionDescriptionRepository
    {
        TransactionDescriptionService service = new TransactionDescriptionService();

        public Task<Response> DELETE(MODEL.TransactionDescription param)
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

        public Task<Response> INSERT(MODEL.TransactionDescription param)
        {
            throw new NotImplementedException();
        }

        public Task<Response> UPDATE(MODEL.TransactionDescription param)
        {
            throw new NotImplementedException();
        }
    }
}
