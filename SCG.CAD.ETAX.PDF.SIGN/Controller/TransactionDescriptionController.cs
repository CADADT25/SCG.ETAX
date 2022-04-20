using SCG.CAD.ETAX.MODEL.etaxModel;
using SCG.CAD.ETAX.UTILITY;
using Newtonsoft.Json;
using SCG.CAD.ETAX.MODEL;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace SCG.CAD.ETAX.PDF.SIGN.Controller
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
        public async Task<JsonResult> Insert(string jsonString)
        {
            Response res = new Response();

            var httpContent = new StringContent(jsonString, Encoding.UTF8, "application/json");

            var task = await Task.Run(() => ApiHelper.PostURI("api/TransactionDescription/Insert", httpContent));

            JsonResult Json = new JsonResult(task);
            return Json;
        }
        public async Task<JsonResult> Update(string jsonString)
        {
            Response res = new Response();

            var httpContent = new StringContent(jsonString, Encoding.UTF8, "application/json");

            var task = await Task.Run(() => ApiHelper.PostURI("api/TransactionDescription/Update", httpContent));

            JsonResult Json = new JsonResult(task);
            return Json;
        }
        public async Task<JsonResult> Delete(string jsonString)
        {
            Response res = new Response();

            //string json = JsonConvert.SerializeObject(jsonString, Formatting.Indented);

            var httpContent = new StringContent(jsonString, Encoding.UTF8, "application/json");

            var task = await Task.Run(() => ApiHelper.PostURI("api/TransactionDescription/Delete", httpContent));

            JsonResult Json = new JsonResult(task);
            return Json;
        }
    }
}
