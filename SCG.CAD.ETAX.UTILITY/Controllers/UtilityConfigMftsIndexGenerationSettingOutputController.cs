using SCG.CAD.ETAX.MODEL.etaxModel;
using Newtonsoft.Json;
using SCG.CAD.ETAX.MODEL;

namespace SCG.CAD.ETAX.UTILITY.Controllers
{
    public class UtilityConfigMftsIndexGenerationSettingOutputController
    {
        public async Task<List<ConfigMftsIndexGenerationSettingOutput>> List()
        {
            Response resp = new Response();

            List<ConfigMftsIndexGenerationSettingOutput> tran = new List<ConfigMftsIndexGenerationSettingOutput>();

            try
            {
                var task = await Task.Run(() => ApiHelper.GetURI("api/ConfigMftsIndexGenerationSettingInput/GetListAll"));

                if (task.STATUS)
                {
                    tran = JsonConvert.DeserializeObject<List<ConfigMftsIndexGenerationSettingOutput>>(task.OUTPUT_DATA.ToString());
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
