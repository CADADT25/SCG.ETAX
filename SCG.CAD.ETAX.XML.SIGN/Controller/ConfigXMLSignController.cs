using SCG.CAD.ETAX.MODEL.etaxModel;
using SCG.CAD.ETAX.UTILITY;
using Newtonsoft.Json;
using SCG.CAD.ETAX.MODEL;

namespace SCG.CAD.ETAX.XML.SIGN.Controller
{
    public class ConfigXMLSignController
    {
        public async Task<List<ConfigXmlSign>> List()
        {
            Response resp = new Response();

            List<ConfigXmlSign> tran = new List<ConfigXmlSign>();

            try
            {
                var task = await Task.Run(() => ApiHelper.GetURI("api/ConfigXmlSign/GetListAll"));

                if (task.STATUS)
                {
                    tran = JsonConvert.DeserializeObject<List<ConfigXmlSign>>(task.OUTPUT_DATA.ToString());
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
