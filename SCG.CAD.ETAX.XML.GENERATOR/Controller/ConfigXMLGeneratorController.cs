using SCG.CAD.ETAX.MODEL.etaxModel;
using SCG.CAD.ETAX.UTILITY;
using Newtonsoft.Json;

namespace SCG.CAD.ETAX.XML.GENERATOR.Controller
{
    public class ConfigXMLGeneratorController
    {
        public async Task<List<ConfigXmlGenerator>> List()
        {
            Response resp = new Response();

            List<ConfigXmlGenerator> tran = new List<ConfigXmlGenerator>();

            try
            {
                var task = await Task.Run(() => ApiHelper.GetURI("api/ConfigXmlGenerator/GetListAll"));

                if (task.STATUS)
                {
                    tran = JsonConvert.DeserializeObject<List<ConfigXmlGenerator>>(task.OUTPUT_DATA.ToString());
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
