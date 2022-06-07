using SCG.CAD.ETAX.MODEL;
using System.Text;

namespace SCG.CAD.ETAX.UTILITY.Controllers
{
    public class OutputSearchPrintingController
    {
        public async Task<Response> Insert(string jsonString)
        {
            var httpContent = new StringContent(jsonString, Encoding.UTF8, "application/json");

            var task = await Task.Run(() => ApiHelper.PostURI("api/OutputSearchPrinting/Insert", httpContent));

            //JsonResult Json = new JsonResult(task);
            return task;
        }
    }
}
