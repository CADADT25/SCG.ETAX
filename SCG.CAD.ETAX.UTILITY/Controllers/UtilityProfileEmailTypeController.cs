using SCG.CAD.ETAX.MODEL.etaxModel;
using Newtonsoft.Json;
using SCG.CAD.ETAX.MODEL;

namespace SCG.CAD.ETAX.UTILITY.Controllers
{
    public class UtilityProfileEmailTypeController
    {
        public async Task<List<ProfileEmailType>> List()
        {
            Response resp = new Response();

            List<ProfileEmailType> tran = new List<ProfileEmailType>();

            try
            {
                var task = await Task.Run(() => ApiHelper.GetURI("api/ProfileEmailType/GetListAll"));

                if (task.STATUS)
                {
                    tran = JsonConvert.DeserializeObject<List<ProfileEmailType>>(task.OUTPUT_DATA.ToString());
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
