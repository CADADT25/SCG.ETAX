namespace SCG.CAD.ETAX.API.Services
{
    public class ProfileEmailTemplateService
    {

        readonly DatabaseContext _dbContext = new();
        public List<ProfileEmailTemplate> GET_LIST()
        {
            List<ProfileEmailTemplate> resp = new List<ProfileEmailTemplate>();
            try
            {

            }
            catch
            {
                throw;
            }
            return resp;
        }

        public List<ProfileEmailTemplate> GET_DETAIL(int id)
        {
            List<ProfileEmailTemplate> resp = new List<ProfileEmailTemplate>();
            try
            {

            }
            catch
            {
                throw;
            }
            return resp;
        }

        public List<ProfileEmailTemplate> INSERT(ProfileEmailTemplate param)
        {
            List<ProfileEmailTemplate> resp = new List<ProfileEmailTemplate>();
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

        public List<ProfileEmailTemplate> UPDATE(ProfileEmailTemplate param)
        {
            List<ProfileEmailTemplate> resp = new List<ProfileEmailTemplate>();
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

        public List<ProfileEmailTemplate> DELETE(ProfileEmailTemplate param)
        {
            List<ProfileEmailTemplate> resp = new List<ProfileEmailTemplate>();
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


