namespace SCG.CAD.ETAX.API.Repositories
{
    public class ProfileSellerRepository : IProfileSellerRepository
    {
        ProfileSellerService service = new ProfileSellerService();

        public Task<Response> DELETE(MODEL.ProfileSeller param)
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

        public Task<Response> INSERT(MODEL.ProfileSeller param)
        {
            throw new NotImplementedException();
        }

        public Task<Response> UPDATE(MODEL.ProfileSeller param)
        {
            throw new NotImplementedException();
        }
    }
}
