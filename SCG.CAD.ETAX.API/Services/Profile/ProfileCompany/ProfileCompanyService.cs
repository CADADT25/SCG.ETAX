namespace SCG.CAD.ETAX.API.Services
{
    public class ProfileCompanyService
    {

        readonly DatabaseContext _dbContext = new();

        public List<ProfileCompany> GET_LIST()
        {
            List<ProfileCompany> resp = new List<ProfileCompany>();
            try
            {

            }
            catch
            {
                throw;
            }
            return resp;
        }

        public List<ProfileCompany> GET_DETAIL(int id)
        {
            List<ProfileCompany> resp = new List<ProfileCompany>();
            try
            {

            }
            catch
            {
                throw;
            }
            return resp;
        }

        public List<ProfileCompany> INSERT(ProfileCompany param)
        {
            List<ProfileCompany> resp = new List<ProfileCompany>();
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

        public List<ProfileCompany> UPDATE(ProfileCompany param)
        {
            List<ProfileCompany> resp = new List<ProfileCompany>();
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

        public List<ProfileCompany> DELETE(ProfileCompany param)
        {
            List<ProfileCompany> resp = new List<ProfileCompany>();
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
