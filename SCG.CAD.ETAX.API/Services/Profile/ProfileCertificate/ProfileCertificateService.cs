namespace SCG.CAD.ETAX.API.Services
{
    public class ProfileCertificateService 
    {

        readonly DatabaseContext _dbContext = new();
        public List<ProfileCertificate> GET_LIST()
        {
            List<ProfileCertificate> resp = new List<ProfileCertificate>();
            try
            {

            }
            catch
            {
                throw;
            }
            return resp;
        }

        public List<ProfileCertificate> GET_DETAIL(int id)
        {
            List<ProfileCertificate> resp = new List<ProfileCertificate>();
            try
            {

            }
            catch
            {
                throw;
            }
            return resp;
        }

        public List<ProfileCertificate> INSERT(ProfileCertificate param)
        {
            List<ProfileCertificate> resp = new List<ProfileCertificate>();
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

        public List<ProfileCertificate> UPDATE(ProfileCertificate param)
        {
            List<ProfileCertificate> resp = new List<ProfileCertificate>();
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

        public List<ProfileCertificate> DELETE(ProfileCertificate param)
        {
            List<ProfileCertificate> resp = new List<ProfileCertificate>();
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
