
namespace SCG.CAD.ETAX.API.Services
{
    public class TaxCodeService : DatabaseExecuteController
    {
        readonly DatabaseContext _dbContext = new();


        OutputOnDbModel resultData = new OutputOnDbModel();
        sqlTaxCode sqlFactory = new sqlTaxCode();

        string sql = "";

        public OutputOnDbModel getGetTaxCodeAll()
        {
            sql = sqlFactory.GET_TAXTCODE_ALL();

            resultData = base.SearchBySql(sql);

            return resultData;
        }

        public OutputOnDbModel getGetTaxCodeDetail()
        {
            sql = sqlFactory.GET_TAXTCODE_ALL();

            resultData = base.SearchBySql(sql);

            return resultData;
        }

        public List<TaxCode> GET_TAXCODE_LIST()
        {
            List<TaxCode> resp = new List<TaxCode> ();
            try
            {
                resp = _dbContext.taxCode.ToList();
            }
            catch
            {
                throw;
            }
            return resp;
        }

        public List<TaxCode> GET_TAXCODE_DETAIL(int CodeNo)
        {
            List<TaxCode> resp = new List<TaxCode>();
            try
            {
                resp = _dbContext.taxCode.Where(x => x.TaxCodeNo ==CodeNo).ToList();
            }
            catch
            {
                throw;
            }
            return resp;
        }

        public List<TaxCode> INSERT_TAXCODE(TaxCode param)
        {
            List<TaxCode> resp = new List<TaxCode>();
            try
            {
                using (_dbContext)
                {
                    _dbContext.taxCode.Add(param);
                    _dbContext.SaveChanges();
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
