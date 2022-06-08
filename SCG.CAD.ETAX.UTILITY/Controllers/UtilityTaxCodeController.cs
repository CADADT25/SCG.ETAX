using SCG.CAD.ETAX.MODEL.etaxModel;
using Newtonsoft.Json;
using SCG.CAD.ETAX.MODEL;

namespace SCG.CAD.ETAX.UTILITY.Controllers
{
    public class UtilityTaxCodeController
    {
        public async Task<List<TaxCode>> List()
        {
            Response resp = new Response();

            List<TaxCode> tran = new List<TaxCode>();

            try
            {
                var task = await Task.Run(() => ApiHelper.GetURI("api/TaxCode/GetListAll"));

                if (task.STATUS)
                {
                    tran = JsonConvert.DeserializeObject<List<TaxCode>>(task.OUTPUT_DATA.ToString());
                }
                else
                {
                    
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException);
            }


            return tran;
        }
    }
}
