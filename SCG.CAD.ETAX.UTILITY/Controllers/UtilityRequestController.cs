using SCG.CAD.ETAX.MODEL.etaxModel;
using Newtonsoft.Json;
using SCG.CAD.ETAX.MODEL;
using System.Text;

namespace SCG.CAD.ETAX.UTILITY.Controllers
{
    public class UtilityRequestController
    {
        public async Task<RequestRelateDataModel> GetRequest(string requestNo)
        {
            Response resp = new Response();

            RequestRelateDataModel req = new RequestRelateDataModel();

            try
            {
                var task = await Task.Run(() => ApiHelper.GetURI("api/Request/GetRequest?requestNo=" + requestNo));

                if (task.STATUS)
                {
                    req = JsonConvert.DeserializeObject<RequestRelateDataModel>(task.OUTPUT_DATA.ToString());
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

        public async Task<Response> UpdateRequestHistory(string jsonString)
        {
            var httpContent = new StringContent(jsonString, Encoding.UTF8, "application/json");

            var task = await Task.Run(() => ApiHelper.PostURI("api/RequestHistory/Update", httpContent));

            return task;
        }

    }
}
