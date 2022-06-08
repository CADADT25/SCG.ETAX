using SCG.CAD.ETAX.MODEL.etaxModel;
using Newtonsoft.Json;
using SCG.CAD.ETAX.MODEL;

namespace SCG.CAD.ETAX.UTILITY.Controllers
{
    public class UtilityProfileCompanyController
    {
        public async Task<List<ProfileCompany>> List()
        {
            Response resp = new Response();

            List<ProfileCompany> tran = new List<ProfileCompany>();

            try
            {
                var task = await Task.Run(() => ApiHelper.GetURI("api/ProfileCompany/GetListAll"));

                if (task.STATUS)
                {
                    tran = JsonConvert.DeserializeObject<List<ProfileCompany>>(task.OUTPUT_DATA.ToString());
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
