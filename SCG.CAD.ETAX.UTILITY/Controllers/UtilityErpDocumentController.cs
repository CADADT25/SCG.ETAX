
using SCG.CAD.ETAX.MODEL.etaxModel;
using Newtonsoft.Json;
using SCG.CAD.ETAX.MODEL;

namespace SCG.CAD.ETAX.UTILITY.Controllers
{
    public class UtilityErpDocumentController
    {
        public async Task<List<ErpDocument>> List()
        {
            Response resp = new Response();

            List<ErpDocument> tran = new List<ErpDocument>();

            try
            {
                var task = await Task.Run(() => ApiHelper.GetURI("api/ErpDocument/GetListAll"));

                if (task.STATUS)
                {
                    tran = JsonConvert.DeserializeObject<List<ErpDocument>>(task.OUTPUT_DATA.ToString());
                }
                else
                {

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }


            return tran;
        }
    }
}
