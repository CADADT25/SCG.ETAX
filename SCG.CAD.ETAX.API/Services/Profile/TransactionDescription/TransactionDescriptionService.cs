namespace SCG.CAD.ETAX.API.Services
{
    public class TransactionDescriptionService
    {

        readonly DatabaseContext _dbContext = new();
        public List<TransactionDescription> GET_LIST()
        {
            List<TransactionDescription> resp = new List<TransactionDescription>();
            try
            {

            }
            catch
            {
                throw;
            }
            return resp;
        }

        public List<TransactionDescription> GET_DETAIL(int id)
        {
            List<TransactionDescription> resp = new List<TransactionDescription>();
            try
            {

            }
            catch
            {
                throw;
            }
            return resp;
        }

        public List<TransactionDescription> INSERT(TransactionDescription param)
        {
            List<TransactionDescription> resp = new List<TransactionDescription>();
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

        public List<TransactionDescription> UPDATE(TransactionDescription param)
        {
            List<TransactionDescription> resp = new List<TransactionDescription>();
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

        public List<TransactionDescription> DELETE(TransactionDescription param)
        {
            List<TransactionDescription> resp = new List<TransactionDescription>();
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
