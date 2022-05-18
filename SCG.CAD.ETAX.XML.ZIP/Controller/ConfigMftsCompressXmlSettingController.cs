using Newtonsoft.Json;
using SCG.CAD.ETAX.MODEL;
using SCG.CAD.ETAX.MODEL.etaxModel;
using SCG.CAD.ETAX.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCG.CAD.ETAX.XML.ZIP.Controller
{
    public class ConfigMftsCompressXmlSettingController
    {
        public async Task<List<ConfigMftsCompressXmlSetting>> List()
        {
            Response resp = new Response();

            List<ConfigMftsCompressXmlSetting> tran = new List<ConfigMftsCompressXmlSetting>();

            try
            {
                var task = await Task.Run(() => ApiHelper.GetURI("api/ConfigMftsCompressXmlSetting/GetListAll"));

                if (task.STATUS)
                {
                    tran = JsonConvert.DeserializeObject<List<ConfigMftsCompressXmlSetting>>(task.OUTPUT_DATA.ToString());
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
