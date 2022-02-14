namespace SCG.CAD.ETAX.API.Services
{
    public class DocumentCodeService : DatabaseExecuteController
    {
        readonly DatabaseContext _dbContext = new();
        public List<DocumentCode> GET_LIST()
        {
            List<DocumentCode> resp = new List<DocumentCode>();
            try
            {

            }
            catch
            {
                throw;
            }
            return resp;
        }

        public List<DocumentCode> GET_DETAIL(int id)
        {
            List<DocumentCode> resp = new List<DocumentCode>();
            try
            {

            }
            catch
            {
                throw;
            }
            return resp;
        }

        public List<DocumentCode> INSERT(DocumentCode param)
        {
            List<DocumentCode> resp = new List<DocumentCode>();
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

        public List<DocumentCode> UPDATE(DocumentCode param)
        {
            List<DocumentCode> resp = new List<DocumentCode>();
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

        public List<DocumentCode> DELETE(DocumentCode param)
        {
            List<DocumentCode> resp = new List<DocumentCode>();
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
