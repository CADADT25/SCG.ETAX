using SCG.CAD.ETAX.MODEL.etaxModel;
using SCG.CAD.ETAX.UTILITY;
using Newtonsoft.Json;
using SCG.CAD.ETAX.MODEL;

namespace SCG.CAD.ETAX.XML.ZIP.Controller
{
    public class ConfigGlobalController
    {
        public async Task<List<ConfigGlobal>> List()
        {
            Response resp = new Response();

            List<ConfigGlobal> tran = new List<ConfigGlobal>();

            try
            {
                var task = await Task.Run(() => ApiHelper.GetURI("api/ConfigGlobal/GetListAll"));

                if (task.STATUS)
                {
                    tran = JsonConvert.DeserializeObject<List<ConfigGlobal>>(task.OUTPUT_DATA.ToString());
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
