namespace SCG.CAD.ETAX.API.Repositories
{
    public class ProfileCustomerRepository : IProfileCustomerRepository
    {
        ProfileCustomerService service = new ProfileCustomerService();

        public Task<Response> DELETE(MODEL.ProfileCustomer param)
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

        public Task<Response> INSERT(MODEL.ProfileCustomer param)
        {
            throw new NotImplementedException();
        }

        public Task<Response> UPDATE(MODEL.ProfileCustomer param)
        {
            throw new NotImplementedException();
        }
    }
}
