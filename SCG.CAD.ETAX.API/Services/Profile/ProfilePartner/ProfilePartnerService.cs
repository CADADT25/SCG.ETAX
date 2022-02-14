namespace SCG.CAD.ETAX.API.Services
{
    public class ProfilePartnerService
    {

        readonly DatabaseContext _dbContext = new();
        public List<ProfilePartner> GET_LIST()
        {
            List<ProfilePartner> resp = new List<ProfilePartner>();
            try
            {

            }
            catch
            {
                throw;
            }
            return resp;
        }

        public List<ProfilePartner> GET_DETAIL(int id)
        {
            List<ProfilePartner> resp = new List<ProfilePartner>();
            try
            {

            }
            catch
            {
                throw;
            }
            return resp;
        }

        public List<ProfilePartner> INSERT(ProfilePartner param)
        {
            List<ProfilePartner> resp = new List<ProfilePartner>();
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

        public List<ProfilePartner> UPDATE(ProfilePartner param)
        {
            List<ProfilePartner> resp = new List<ProfilePartner>();
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

        public List<ProfilePartner> DELETE(ProfilePartner param)
        {
            List<ProfilePartner> resp = new List<ProfilePartner>();
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
