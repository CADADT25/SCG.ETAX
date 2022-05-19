using SCG.CAD.ETAX.MODEL.etaxModel;
using SCG.CAD.ETAX.UTILITY;
using Newtonsoft.Json;

namespace SCG.CAD.ETAX.XML.GENERATOR.Controller
{
    public class ProfileFiDocController
    {
        public async Task<List<ProfileFiDoc>> List()
        {
            Response resp = new Response();

            List<ProfileFiDoc> tran = new List<ProfileFiDoc>();

            try
            {
                var task = await Task.Run(() => ApiHelper.GetURI("api/ProfileFiDoc/GetListAll"));

                if (task.STATUS)
                {
                    tran = JsonConvert.DeserializeObject<List<ProfileFiDoc>>(task.OUTPUT_DATA.ToString());
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
