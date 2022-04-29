
using SCG.CAD.ETAX.MODEL.etaxModel;
using SCG.CAD.ETAX.UTILITY;
using Newtonsoft.Json;

namespace SCG.CAD.ETAX.XML.GENERATOR.Controller
{
    public class ProductUnitController
    {
        public async Task<List<ProductUnit>> List()
        {
            Response resp = new Response();

            List<ProductUnit> tran = new List<ProductUnit>();

            try
            {
                var task = await Task.Run(() => ApiHelper.GetURI("api/ProductUnit/GetListAll"));

                if (task.STATUS)
                {
                    tran = JsonConvert.DeserializeObject<List<ProductUnit>>(task.OUTPUT_DATA.ToString());
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
