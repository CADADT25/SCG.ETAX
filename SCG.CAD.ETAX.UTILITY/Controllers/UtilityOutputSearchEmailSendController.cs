using Newtonsoft.Json;
using SCG.CAD.ETAX.MODEL;
using SCG.CAD.ETAX.MODEL.etaxModel;
using System.Text;

namespace SCG.CAD.ETAX.UTILITY.Controllers
{
    public class UtilityOutputSearchEmailSendController
    {
        public async Task<List<OutputSearchEmailSend>> List()
        {
            Response resp = new Response();

            List<OutputSearchEmailSend> tran = new List<OutputSearchEmailSend>();

            try
            {
                var task = await Task.Run(() => ApiHelper.GetURI("api/OutputSearchEmailSend/GetListAll"));

                if (task.STATUS)
                {
                    tran = JsonConvert.DeserializeObject<List<OutputSearchEmailSend>>(task.OUTPUT_DATA.ToString());
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

        public async Task<Response> Insert(string jsonString)
        {
            var httpContent = new StringContent(jsonString, Encoding.UTF8, "application/json");

            var task = await Task.Run(() => ApiHelper.PostURI("api/OutputSearchEmailSend/Insert", httpContent));

            //JsonResult Json = new JsonResult(task);
            return task;
        }
    }
}
