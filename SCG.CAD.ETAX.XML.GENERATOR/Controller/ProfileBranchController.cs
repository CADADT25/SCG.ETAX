using SCG.CAD.ETAX.MODEL.etaxModel;
using SCG.CAD.ETAX.UTILITY;
using Newtonsoft.Json;

namespace SCG.CAD.ETAX.XML.GENERATOR.Controller
{
    public class ProfileBranchController
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
