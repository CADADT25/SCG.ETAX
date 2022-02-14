namespace SCG.CAD.ETAX.API.Services
{
    public class ProfileCustomerService
    {

        readonly DatabaseContext _dbContext = new();

        public List<ProfileCustomer> GET_LIST()
        {
            List<ProfileCustomer> resp = new List<ProfileCustomer>();
            try
            {

            }
            catch
            {
                throw;
            }
            return resp;
        }

        public List<ProfileCustomer> GET_DETAIL(int id)
        {
            List<ProfileCustomer> resp = new List<ProfileCustomer>();
            try
            {

            }
            catch
            {
                throw;
            }
            return resp;
        }

        public List<ProfileCustomer> INSERT(ProfileCustomer param)
        {
            List<ProfileCustomer> resp = new List<ProfileCustomer>();
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

        public List<ProfileCustomer> UPDATE(ProfileCustomer param)
        {
            List<ProfileCustomer> resp = new List<ProfileCustomer>();
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

        public List<ProfileCustomer> DELETE(ProfileCustomer param)
        {
            List<ProfileCustomer> resp = new List<ProfileCustomer>();
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
