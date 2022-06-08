using SCG.CAD.ETAX.MODEL.etaxModel;
using Newtonsoft.Json;
using SCG.CAD.ETAX.MODEL;

namespace SCG.CAD.ETAX.UTILITY.Controllers
{
    public class UtilityProfileBranchController
    {
        public async Task<List<ProfileBranch>> List()
        {
            Response resp = new Response();

            List<ProfileBranch> tran = new List<ProfileBranch>();

            try
            {
                var task = await Task.Run(() => ApiHelper.GetURI("api/ProfileBranch/GetListAll"));

                if (task.STATUS)
                {
                    tran = JsonConvert.DeserializeObject<List<ProfileBranch>>(task.OUTPUT_DATA.ToString());
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
