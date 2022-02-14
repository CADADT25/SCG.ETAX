namespace SCG.CAD.ETAX.API.Services
{
    public class ZipFileTransactionService
    {

        readonly DatabaseContext _dbContext = new();
        public List<ZipFileTransaction> GET_LIST()
        {
            List<ZipFileTransaction> resp = new List<ZipFileTransaction>();
            try
            {

            }
            catch
            {
                throw;
            }
            return resp;
        }

        public List<ZipFileTransaction> GET_DETAIL(int id)
        {
            List<ZipFileTransaction> resp = new List<ZipFileTransaction>();
            try
            {

            }
            catch
            {
                throw;
            }
            return resp;
        }

        public List<ZipFileTransaction> INSERT(ZipFileTransaction param)
        {
            List<ZipFileTransaction> resp = new List<ZipFileTransaction>();
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

        public List<ZipFileTransaction> UPDATE(ZipFileTransaction param)
        {
            List<ZipFileTransaction> resp = new List<ZipFileTransaction>();
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

        public List<ZipFileTransaction> DELETE(ZipFileTransaction param)
        {
            List<ZipFileTransaction> resp = new List<ZipFileTransaction>();
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
