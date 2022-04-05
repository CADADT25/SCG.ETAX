
using SCG.CAD.ETAX.MODEL.etaxModel;
using SCG.CAD.ETAX.UTILITY;
using Newtonsoft.Json;

namespace SCG.CAD.ETAX.XML.GENERATOR.Controller
{
    public class RdDocumentController
    {
        public async Task<List<RdDocument>> List()
        {
            Response resp = new Response();

            List<RdDocument> tran = new List<RdDocument>();

            try
            {
                var task = await Task.Run(() => ApiHelper.GetURI("api/DocumentCode/GetListAll"));

                if (task.STATUS)
                {
                    tran = JsonConvert.DeserializeObject<List<RdDocument>>(task.OUTPUT_DATA.ToString());
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
