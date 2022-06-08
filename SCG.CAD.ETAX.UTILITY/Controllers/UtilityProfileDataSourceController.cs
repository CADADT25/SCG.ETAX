using SCG.CAD.ETAX.MODEL.etaxModel;
using Newtonsoft.Json;
using SCG.CAD.ETAX.MODEL;

namespace SCG.CAD.ETAX.UTILITY.Controllers
{
    public class UtilityProfileDataSourceController
    {
        public async Task<List<ProfileDataSource>> List()
        {
            Response resp = new Response();

            List<ProfileDataSource> tran = new List<ProfileDataSource>();

            try
            {
                var task = await Task.Run(() => ApiHelper.GetURI("api/ProfileDataSource/GetListAll"));

                if (task.STATUS)
                {
                    tran = JsonConvert.DeserializeObject<List<ProfileDataSource>>(task.OUTPUT_DATA.ToString());
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
