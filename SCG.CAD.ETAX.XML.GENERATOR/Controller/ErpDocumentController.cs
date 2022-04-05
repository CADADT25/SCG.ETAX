
using SCG.CAD.ETAX.MODEL.etaxModel;
using SCG.CAD.ETAX.UTILITY;
using Newtonsoft.Json;

namespace SCG.CAD.ETAX.XML.GENERATOR.Controller
{
    public class ErpDocumentController
    {
        public async Task<List<ErpDocument>> List()
        {
            Response resp = new Response();

            List<ErpDocument> tran = new List<ErpDocument>();

            try
            {
                var task = await Task.Run(() => ApiHelper.GetURI("api/DocumentCode/GetListAll"));

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
                Console.WriteLine(ex.InnerException);
            }


            return tran;
        }
    }
}
