namespace SCG.CAD.ETAX.API.Services
{
    public class RdDocumentService
    {

        readonly DatabaseContext _dbContext = new();
        public List<RdDocument> GET_LIST()
        {
            List<RdDocument> resp = new List<RdDocument>();
            try
            {

            }
            catch
            {
                throw;
            }
            return resp;
        }

        public List<RdDocument> GET_DETAIL(int id)
        {
            List<RdDocument> resp = new List<RdDocument>();
            try
            {

            }
            catch
            {
                throw;
            }
            return resp;
        }

        public List<RdDocument> INSERT(RdDocument param)
        {
            List<RdDocument> resp = new List<RdDocument>();
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

        public List<RdDocument> UPDATE(RdDocument param)
        {
            List<RdDocument> resp = new List<RdDocument>();
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

        public List<RdDocument> DELETE(RdDocument param)
        {
            List<RdDocument> resp = new List<RdDocument>();
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
