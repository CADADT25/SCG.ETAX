using Newtonsoft.Json;
using SCG.CAD.ETAX.MODEL;
using SCG.CAD.ETAX.MODEL.etaxModel;

namespace SCG.CAD.ETAX.UTILITY.Controllers
{
    public class ConfigMftsCompressPrintSettingController
    {
        public async Task<List<ConfigMftsCompressPrintSetting>> List()
        {
            Response resp = new Response();

            List<ConfigMftsCompressPrintSetting> tran = new List<ConfigMftsCompressPrintSetting>();

            try
            {
                var task = await Task.Run(() => ApiHelper.GetURI("api/ConfigMftsCompressPrintSetting/GetListAll"));

                if (task.STATUS)
                {
                    tran = JsonConvert.DeserializeObject<List<ConfigMftsCompressPrintSetting>>(task.OUTPUT_DATA.ToString());
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
