namespace SCG.CAD.ETAX.API.Repositories
{
    public class ProfilePartnerRepository : IProfilePartnerRepository
    {
        ProfilePartnerService service = new ProfilePartnerService();

        public Task<Response> DELETE(MODEL.ProfilePartner param)
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

        public Task<Response> INSERT(MODEL.ProfilePartner param)
        {
            throw new NotImplementedException();
        }

        public Task<Response> UPDATE(MODEL.ProfilePartner param)
        {
            throw new NotImplementedException();
        }
    }
}
