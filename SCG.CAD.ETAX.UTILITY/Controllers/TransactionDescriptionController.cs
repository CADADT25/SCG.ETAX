using SCG.CAD.ETAX.MODEL.etaxModel;
using Newtonsoft.Json;
using SCG.CAD.ETAX.MODEL;
using System.Text;

namespace SCG.CAD.ETAX.UTILITY.Controllers
{
    public class TransactionDescriptionController
    {
        public async Task<List<TransactionDescription>> List()
        {
            Response resp = new Response();

            List<TransactionDescription> tran = new List<TransactionDescription>();

            try
            {
                var task = await Task.Run(() => ApiHelper.GetURI("api/TransactionDescription/GetListAll"));

                if (task.STATUS)
                {
                    tran = JsonConvert.DeserializeObject<List<TransactionDescription>>(task.OUTPUT_DATA.ToString());
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
        public async Task<List<TransactionDescription>> Detail()
        {
            Response resp = new Response();

            List<TransactionDescription> tran = new List<TransactionDescription>();

            try
            {
                var task = await Task.Run(() => ApiHelper.GetURI("api/TransactionDescription/GetDetail"));

                if (task.STATUS)
                {
                    tran = JsonConvert.DeserializeObject<List<TransactionDescription>>(task.OUTPUT_DATA.ToString());
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
        public async Task<Response> Insert(string jsonString)
        {
            var httpContent = new StringContent(jsonString, Encoding.UTF8, "application/json");

            var task = await Task.Run(() => ApiHelper.PostURI("api/TransactionDescription/Insert", httpContent));

            //JsonResult Json = new JsonResult(task);
            return task;
        }
        public async Task<Response> Update(string jsonString)
        {
            var httpContent = new StringContent(jsonString, Encoding.UTF8, "application/json");

            var task = await Task.Run(() => ApiHelper.PostURI("api/TransactionDescription/Update", httpContent));

            //JsonResult Json = new JsonResult(task);
            return task;
        }
        public async Task<Response> Delete(string jsonString)
        {
            var httpContent = new StringContent(jsonString, Encoding.UTF8, "application/json");

            var task = await Task.Run(() => ApiHelper.PostURI("api/TransactionDescription/Delete", httpContent));

            //JsonResult Json = new JsonResult(task);
            return task;
        }
        public async Task<List<TransactionDescription>> GetBilling(string billno)
        {
            Response resp = new Response();

            List<TransactionDescription> tran = new List<TransactionDescription>();

            try
            {
                var task = await Task.Run(() => ApiHelper.GetURI("api/TransactionDescription/GetBilling?billingNo=" + billno));

                if (task.STATUS)
                {
                    tran = JsonConvert.DeserializeObject<List<TransactionDescription>>(task.OUTPUT_DATA.ToString());
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

        public async Task<Response> UpdateList(string jsonString)
        {
            var httpContent = new StringContent(jsonString, Encoding.UTF8, "application/json");

            var task = await Task.Run(() => ApiHelper.PostURI("api/TransactionDescription/UpdateList", httpContent));

            //JsonResult Json = new JsonResult(task);
            return task;
        }
    }
}
