namespace SCG.CAD.ETAX.API.Services
{
    public class ZipFileTypeService
    {

        readonly DatabaseContext _dbContext = new();
        public List<ZipFileType> GET_LIST()
        {
            List<ZipFileType> resp = new List<ZipFileType>();
            try
            {

            }
            catch
            {
                throw;
            }
            return resp;
        }

        public List<ZipFileType> GET_DETAIL(int id)
        {
            List<ZipFileType> resp = new List<ZipFileType>();
            try
            {

            }
            catch
            {
                throw;
            }
            return resp;
        }

        public List<ZipFileType> INSERT(ZipFileType param)
        {
            List<ZipFileType> resp = new List<ZipFileType>();
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

        public List<ZipFileType> UPDATE(ZipFileType param)
        {
            List<ZipFileType> resp = new List<ZipFileType>();
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

        public List<ZipFileType> DELETE(ZipFileType param)
        {
            List<ZipFileType> resp = new List<ZipFileType>();
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
