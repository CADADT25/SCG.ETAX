namespace SCG.CAD.ETAX.API.Services
{
    public class ProfileIsActiveService
    {

        readonly DatabaseContext _dbContext = new();
        public List<ProfileIsActive> GET_LIST()
        {
            List<ProfileIsActive> resp = new List<ProfileIsActive>();
            try
            {

            }
            catch
            {
                throw;
            }
            return resp;
        }

        public List<ProfileIsActive> GET_DETAIL(int id)
        {
            List<ProfileIsActive> resp = new List<ProfileIsActive>();
            try
            {

            }
            catch
            {
                throw;
            }
            return resp;
        }

        public List<ProfileIsActive> INSERT(ProfileIsActive param)
        {
            List<ProfileIsActive> resp = new List<ProfileIsActive>();
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

        public List<ProfileIsActive> UPDATE(ProfileIsActive param)
        {
            List<ProfileIsActive> resp = new List<ProfileIsActive>();
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

        public List<ProfileIsActive> DELETE(ProfileIsActive param)
        {
            List<ProfileIsActive> resp = new List<ProfileIsActive>();
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
