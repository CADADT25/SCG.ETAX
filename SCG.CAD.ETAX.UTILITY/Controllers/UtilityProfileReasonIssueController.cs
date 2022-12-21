using Newtonsoft.Json;
using SCG.CAD.ETAX.MODEL;
using SCG.CAD.ETAX.MODEL.etaxModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCG.CAD.ETAX.UTILITY.Controllers
{
    public class UtilityProfileReasonIssueController
    {
        public async Task<List<ProfileReasonIssue>> List()
        {
            Response resp = new Response();

            List<ProfileReasonIssue> tran = new List<ProfileReasonIssue>();

            try
            {
                var task = await Task.Run(() => ApiHelper.GetURI("api/ProfileReasonIssue/GetListAll"));

                if (task.STATUS)
                {
                    tran = JsonConvert.DeserializeObject<List<ProfileReasonIssue>>(task.OUTPUT_DATA.ToString());
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
