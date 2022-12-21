
using SCG.CAD.ETAX.MODEL.etaxModel;
using Newtonsoft.Json;
using SCG.CAD.ETAX.MODEL;

namespace SCG.CAD.ETAX.UTILITY.Controllers
{
    public class UtilityDocumentCodeController
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
                Console.WriteLine(ex.Message);
            }


            return tran;
        }
    }

}
