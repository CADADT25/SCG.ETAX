using SCG.CAD.ETAX.MODEL.etaxModel;
using SCG.CAD.ETAX.UTILITY;
using Newtonsoft.Json;
using SCG.CAD.ETAX.MODEL;

namespace SCG.CAD.ETAX.PDF.SIGN.Controller
{
    public class ConfigPDFSignController
    {
        public async Task<List<ConfigPdfSign>> List()
        {
            Response resp = new Response();

            List<ConfigPdfSign> tran = new List<ConfigPdfSign>();

            try
            {
                var task = await Task.Run(() => ApiHelper.GetURI("api/ConfigPdfSign/GetListAll"));

                if (task.STATUS)
                {
                    tran = JsonConvert.DeserializeObject<List<ConfigPdfSign>>(task.OUTPUT_DATA.ToString());
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
