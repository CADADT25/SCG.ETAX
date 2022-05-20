using SCG.CAD.ETAX.MODEL.etaxModel;
using SCG.CAD.ETAX.UTILITY;
using Newtonsoft.Json;
using SCG.CAD.ETAX.MODEL;

namespace SCG.CAD.ETAX.EMAIL.Controller
{
    public class OutputSearchXmlZipController
    {
        public async Task<List<OutputSearchXmlZip>> List()
        {
            Response resp = new Response();

            List<OutputSearchXmlZip> tran = new List<OutputSearchXmlZip>();

            try
            {
                var task = await Task.Run(() => ApiHelper.GetURI("api/OutputSearchXmlZip/GetListAll"));

                if (task.STATUS)
                {
                    tran = JsonConvert.DeserializeObject<List<OutputSearchXmlZip>>(task.OUTPUT_DATA.ToString());
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
