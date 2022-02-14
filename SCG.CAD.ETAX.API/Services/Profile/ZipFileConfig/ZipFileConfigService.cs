namespace SCG.CAD.ETAX.API.Services
{
    public class ZipFileConfigService
    {

        readonly DatabaseContext _dbContext = new();
        public List<ZipFileConfig> GET_LIST()
        {
            List<ZipFileConfig> resp = new List<ZipFileConfig>();
            try
            {

            }
            catch
            {
                throw;
            }
            return resp;
        }

        public List<ZipFileConfig> GET_DETAIL(int id)
        {
            List<ZipFileConfig> resp = new List<ZipFileConfig>();
            try
            {

            }
            catch
            {
                throw;
            }
            return resp;
        }

        public List<ZipFileConfig> INSERT(ZipFileConfig param)
        {
            List<ZipFileConfig> resp = new List<ZipFileConfig>();
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

        public List<ZipFileConfig> UPDATE(ZipFileConfig param)
        {
            List<ZipFileConfig> resp = new List<ZipFileConfig>();
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

        public List<ZipFileConfig> DELETE(ZipFileConfig param)
        {
            List<ZipFileConfig> resp = new List<ZipFileConfig>();
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
