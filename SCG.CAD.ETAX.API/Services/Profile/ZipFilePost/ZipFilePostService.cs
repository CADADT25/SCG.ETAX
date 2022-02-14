namespace SCG.CAD.ETAX.API.Services
{
    public class ZipFilePostService
    {

        readonly DatabaseContext _dbContext = new();
        public List<ZipFilePost> GET_LIST()
        {
            List<ZipFilePost> resp = new List<ZipFilePost>();
            try
            {

            }
            catch
            {
                throw;
            }
            return resp;
        }

        public List<ZipFilePost> GET_DETAIL(int id)
        {
            List<ZipFilePost> resp = new List<ZipFilePost>();
            try
            {

            }
            catch
            {
                throw;
            }
            return resp;
        }

        public List<ZipFilePost> INSERT(ZipFilePost param)
        {
            List<ZipFilePost> resp = new List<ZipFilePost>();
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

        public List<ZipFilePost> UPDATE(ZipFilePost param)
        {
            List<ZipFilePost> resp = new List<ZipFilePost>();
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

        public List<ZipFilePost> DELETE(ZipFilePost param)
        {
            List<ZipFilePost> resp = new List<ZipFilePost>();
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
