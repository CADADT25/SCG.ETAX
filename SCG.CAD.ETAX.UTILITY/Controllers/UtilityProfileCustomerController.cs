using SCG.CAD.ETAX.MODEL.etaxModel;
using Newtonsoft.Json;
using SCG.CAD.ETAX.MODEL;

namespace SCG.CAD.ETAX.UTILITY.Controllers
{
    public class UtilityProfileCustomerController
    {
        public async Task<List<ProfileCustomer>> List()
        {
            Response resp = new Response();

            List<ProfileCustomer> tran = new List<ProfileCustomer>();

            try
            {
                var task = await Task.Run(() => ApiHelper.GetURI("api/ProfileCustomer/GetListAll"));

                if (task.STATUS)
                {
                    tran = JsonConvert.DeserializeObject<List<ProfileCustomer>>(task.OUTPUT_DATA.ToString());
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
