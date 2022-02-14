namespace SCG.CAD.ETAX.API.Services
{
    public class ProductUnitService 
    {

        readonly DatabaseContext _dbContext = new();
        public List<ProductUnit> GET_LIST()
        {
            List<ProductUnit> resp = new List<ProductUnit>();
            try
            {

            }
            catch
            {
                throw;
            }
            return resp;
        }

        public List<ProductUnit> GET_DETAIL(int id)
        {
            List<ProductUnit> resp = new List<ProductUnit>();
            try
            {

            }
            catch
            {
                throw;
            }
            return resp;
        }

        public List<ProductUnit> INSERT(ProductUnit param)
        {
            List<ProductUnit> resp = new List<ProductUnit>();
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

        public List<ProductUnit> UPDATE(ProductUnit param)
        {
            List<ProductUnit> resp = new List<ProductUnit>();
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

        public List<ProductUnit> DELETE(ProductUnit param)
        {
            List<ProductUnit> resp = new List<ProductUnit>();
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
