namespace SCG.CAD.ETAX.API.Services
{
    public class NewsBoardService : DatabaseExecuteController
    {

        readonly DatabaseContext _dbContext = new();
        public List<NewsBoard> GET_NEWSBOARD_LIST()
        {
            List<NewsBoard> resp = new List<NewsBoard>();
            try
            {

            }
            catch
            {
                throw;
            }
            return resp;
        }

        public List<NewsBoard> GET_NEWSBOARD_DETAIL(int NewsBoardNo)
        {
            List<NewsBoard> resp = new List<NewsBoard>();
            try
            {

            }
            catch
            {
                throw;
            }
            return resp;
        }

        public List<NewsBoard> INSERT_NEWSBOARD(NewsBoard param)
        {
            List<NewsBoard> resp = new List<NewsBoard>();
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

        public List<NewsBoard> UPDATE_NEWSBOARD(NewsBoard param)
        {
            List<NewsBoard> resp = new List<NewsBoard>();
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

        public List<NewsBoard> DELETE_NEWSBOARD(NewsBoard param)
        {
            List<NewsBoard> resp = new List<NewsBoard>();
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
