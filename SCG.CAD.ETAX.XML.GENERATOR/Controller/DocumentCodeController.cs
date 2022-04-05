
using SCG.CAD.ETAX.MODEL.etaxModel;
using SCG.CAD.ETAX.UTILITY;
using Newtonsoft.Json;

namespace SCG.CAD.ETAX.XML.GENERATOR.Controller
{
    public class DocumentCodeController
    {
        public async Task<List<DocumentCode>> List()
        {
            Response resp = new Response();

            List<DocumentCode> tran = new List<DocumentCode>();

            try
            {
                var task = await Task.Run(() => ApiHelper.GetURI("api/DocumentCode/GetListAll"));

                if (task.STATUS)
                {
                    tran = JsonConvert.DeserializeObject<List<DocumentCode>>(task.OUTPUT_DATA.ToString());
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
