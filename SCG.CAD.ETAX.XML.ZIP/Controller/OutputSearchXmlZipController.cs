using SCG.CAD.ETAX.MODEL;
using SCG.CAD.ETAX.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCG.CAD.ETAX.XML.ZIP.Controller
{
    public class OutputSearchXmlZipController
    {
        public async Task<Response> Insert(string jsonString)
        {
            var httpContent = new StringContent(jsonString, Encoding.UTF8, "application/json");

            var task = await Task.Run(() => ApiHelper.PostURI("api/OutputSearchXmlZip/Insert", httpContent));

            //JsonResult Json = new JsonResult(task);
            return task;
        }
    }
}
