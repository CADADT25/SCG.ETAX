using SCG.CAD.ETAX.MODEL.etaxModel;
using Newtonsoft.Json;
using SCG.CAD.ETAX.MODEL;
using System.Text;

namespace SCG.CAD.ETAX.UTILITY.Controllers
{
    public class UtilityRequestController
    {
        public async Task<List<Request>> GetRequest(string requestNo)
        {
            Response resp = new Response();

            List<Request> req = new List<Request>();

            try
            {
                var task = await Task.Run(() => ApiHelper.GetURI("api/Request/GetRequest?requestNo=" + requestNo));

                if (task.STATUS)
                {
                    req = JsonConvert.DeserializeObject<List<Request>>(task.OUTPUT_DATA.ToString());
                }
                else
                {

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException);
            }

            return req;
        }

        public async Task<List<TransactionDescription>> GetRequestItemTransaction(string requestNo)
        {
            Response resp = new Response();

            List<TransactionDescription> req = new List<TransactionDescription>();

            try
            {
                var task = await Task.Run(() => ApiHelper.GetURI("api/Request/GetRequestItemTransaction?requestNo=" + requestNo));

                if (task.STATUS)
                {
                    req = JsonConvert.DeserializeObject<List<TransactionDescription>>(task.OUTPUT_DATA.ToString());
                }
                else
                {

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException);
            }

            return req;
        }

    }
}
