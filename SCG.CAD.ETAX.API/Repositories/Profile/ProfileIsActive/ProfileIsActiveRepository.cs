namespace SCG.CAD.ETAX.API.Repositories
{
    public class ProfileIsActiveRepository : IProfileIsActiveRepository
    {
        ProfileIsActiveService service = new ProfileIsActiveService();

        public Task<Response> DELETE(MODEL.ProfileIsActive param)
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

        public Task<Response> INSERT(MODEL.ProfileIsActive param)
        {
            throw new NotImplementedException();
        }

        public Task<Response> UPDATE(MODEL.ProfileIsActive param)
        {
            throw new NotImplementedException();
        }
    }
}
