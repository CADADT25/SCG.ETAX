namespace SCG.CAD.ETAX.API.Services
{
    public class ProfileEmailTypeService
    {

        readonly DatabaseContext _dbContext = new();
        public List<ProfileEmailType> GET_LIST()
        {
            List<ProfileEmailType> resp = new List<ProfileEmailType>();
            try
            {

            }
            catch
            {
                throw;
            }
            return resp;
        }

        public List<ProfileEmailType> GET_DETAIL(int id)
        {
            List<ProfileEmailType> resp = new List<ProfileEmailType>();
            try
            {

            }
            catch
            {
                throw;
            }
            return resp;
        }

        public List<ProfileEmailType> INSERT(ProfileEmailType param)
        {
            List<ProfileEmailType> resp = new List<ProfileEmailType>();
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

        public List<ProfileEmailType> UPDATE(ProfileEmailType param)
        {
            List<ProfileEmailType> resp = new List<ProfileEmailType>();
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

        public List<ProfileEmailType> DELETE(ProfileEmailType param)
        {
            List<ProfileEmailType> resp = new List<ProfileEmailType>();
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
