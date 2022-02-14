namespace SCG.CAD.ETAX.API.Services
{
    public class ProfileStatusService
    {

        readonly DatabaseContext _dbContext = new();
        public List<ProfileStatus> GET_LIST()
        {
            List<ProfileStatus> resp = new List<ProfileStatus>();
            try
            {

            }
            catch
            {
                throw;
            }
            return resp;
        }

        public List<ProfileStatus> GET_DETAIL(int id)
        {
            List<ProfileStatus> resp = new List<ProfileStatus>();
            try
            {

            }
            catch
            {
                throw;
            }
            return resp;
        }

        public List<ProfileStatus> INSERT(ProfileStatus param)
        {
            List<ProfileStatus> resp = new List<ProfileStatus>();
            try
            {
                using (_dbContext)
                {


                }
            }
            catch
            {
                throw;
            }
            return resp;
        }

        public List<ProfileStatus> UPDATE(ProfileStatus param)
        {
            List<ProfileStatus> resp = new List<ProfileStatus>();
            try
            {
                using (_dbContext)
                {

                }
            }
            catch
            {
                throw;
            }
            return resp;
        }

        public List<ProfileStatus> DELETE(ProfileStatus param)
        {
            List<ProfileStatus> resp = new List<ProfileStatus>();
            try
            {
                using (_dbContext)
                {

                }
            }
            catch
            {
                throw;
            }
            return resp;
        }

    }
}
