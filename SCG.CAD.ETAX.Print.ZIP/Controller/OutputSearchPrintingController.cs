using SCG.CAD.ETAX.MODEL;
using SCG.CAD.ETAX.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCG.CAD.ETAX.PRINT.ZIP.Controller
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
