using SCG.CAD.ETAX.MODEL.etaxModel;
using Newtonsoft.Json;
using SCG.CAD.ETAX.MODEL;

namespace SCG.CAD.ETAX.UTILITY.Controllers
{
    public class UtilityConfigMftsIndexGenerationSettingInputController
    {
        public async Task<List<ConfigMftsIndexGenerationSettingInput>> List()
        {
            Response resp = new Response();

            List<ConfigMftsIndexGenerationSettingInput> tran = new List<ConfigMftsIndexGenerationSettingInput>();

            try
            {
                var task = await Task.Run(() => ApiHelper.GetURI("api/ConfigMftsIndexGenerationSettingInput/GetListAll"));

                if (task.STATUS)
                {
                    tran = JsonConvert.DeserializeObject<List<ConfigMftsIndexGenerationSettingInput>>(task.OUTPUT_DATA.ToString());
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
