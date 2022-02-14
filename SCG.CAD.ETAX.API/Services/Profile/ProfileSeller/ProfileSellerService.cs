namespace SCG.CAD.ETAX.API.Services
{
    public class ProfileSellerService
    {

        readonly DatabaseContext _dbContext = new();
        public List<ProfileSeller> GET_LIST()
        {
            List<ProfileSeller> resp = new List<ProfileSeller>();
            try
            {

            }
            catch
            {
                throw;
            }
            return resp;
        }

        public List<ProfileSeller> GET_DETAIL(int id)
        {
            List<ProfileSeller> resp = new List<ProfileSeller>();
            try
            {

            }
            catch
            {
                throw;
            }
            return resp;
        }

        public List<ProfileSeller> INSERT(ProfileSeller param)
        {
            List<ProfileSeller> resp = new List<ProfileSeller>();
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

        public List<ProfileSeller> UPDATE(ProfileSeller param)
        {
            List<ProfileSeller> resp = new List<ProfileSeller>();
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

        public List<ProfileSeller> DELETE(ProfileSeller param)
        {
            List<ProfileSeller> resp = new List<ProfileSeller>();
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
