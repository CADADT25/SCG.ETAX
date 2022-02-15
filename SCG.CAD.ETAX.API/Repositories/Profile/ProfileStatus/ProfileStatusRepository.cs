namespace SCG.CAD.ETAX.API.Repositories
{
    public class ProfileStatusRepository : IProfileStatusRepository
    {
        ProfileStatusService service = new ProfileStatusService();

        public Task<Response> DELETE(MODEL.ProfileStatus param)
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

        public Task<Response> INSERT(MODEL.ProfileStatus param)
        {
            throw new NotImplementedException();
        }

        public Task<Response> UPDATE(MODEL.ProfileStatus param)
        {
            throw new NotImplementedException();
        }
    }
}
