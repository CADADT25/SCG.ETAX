using SCG.CAD.ETAX.MODEL.etaxModel;
using Newtonsoft.Json;
using SCG.CAD.ETAX.MODEL;

namespace SCG.CAD.ETAX.UTILITY.Controllers
{
    public class UtilityProfileEmailTemplateController
    {
        public async Task<List<ProfileEmailTemplate>> List()
        {
            Response resp = new Response();

            List<ProfileEmailTemplate> tran = new List<ProfileEmailTemplate>();

            try
            {
                var task = await Task.Run(() => ApiHelper.GetURI("api/ProfileEmailTemplate/GetListAll"));

                if (task.STATUS)
                {
                    tran = JsonConvert.DeserializeObject<List<ProfileEmailTemplate>>(task.OUTPUT_DATA.ToString());
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
    }
}
