namespace SCG.CAD.ETAX.API.Repositories
{
    public class TaxCodeRepository : ITaxCodeRepository
    {
        TaxCodeService service = new TaxCodeService();

        //OutputOnDbModel resultData = new OutputOnDbModel();
        public async Task<Response> GET_LIST()
        {
            List<TaxCode> taxCodeList = new List<TaxCode>();

            Response resp = new Response();
            try
            {
                var result = service.GET_TAXCODE_LIST();

                if (result.Count > 0)
                {
                    resp.STATUS = true;
                    resp.OUTPUT_DATA = result;
                }
                else
                {
                    resp.STATUS = true;
                    resp.MESSAGE = "Data not found";
                }
            }
            catch (Exception ex)
            {
                resp.STATUS = false;
                resp.ERROR_MESSAGE = ex.InnerException.Message.ToString();
            }

            return resp;
        }
        public async Task<Response> GET_DETAIL(int taxCodeNo)
        {
            List<TaxCode> taxCodeList = new List<TaxCode>();

            Response resp = new Response();
            try
            {
                var result = service.GET_TAXCODE_DETAIL(taxCodeNo);

                if (result.Count > 0)
                {
                    resp.STATUS = true;
                    resp.OUTPUT_DATA = result;
                }
                else
                {
                    resp.STATUS = true;
                    resp.MESSAGE = "Data not found";
                }
            }
            catch (Exception ex)
            {
                resp.STATUS = false;
                resp.ERROR_MESSAGE = ex.InnerException.Message.ToString();
            }

            return resp;
        }
        public async Task<Response> INSERT(TaxCode param)
        {
            List<TaxCode> taxCodeList = new List<TaxCode>();

            Response resp = new Response();
            try
            {
                var result = service.INSERT_TAXCODE(param);

                resp.STATUS = true;
                resp.MESSAGE = "SaveChanges Success";

            }
            catch (Exception ex)
            {
                resp.STATUS = false;
                resp.ERROR_MESSAGE = ex.InnerException.Message.ToString();
            }

            return resp;
        }
        public async Task<Response> UPDATE()
        {
            throw new NotImplementedException();
        }
        public async Task<Response> DELETE()
        {
            throw new NotImplementedException();
        }

    }
}
