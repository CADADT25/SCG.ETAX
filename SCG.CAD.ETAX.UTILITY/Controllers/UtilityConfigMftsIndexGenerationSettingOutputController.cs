﻿using SCG.CAD.ETAX.MODEL.etaxModel;
using Newtonsoft.Json;
using SCG.CAD.ETAX.MODEL;
using System.Text;

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
                var task = await Task.Run(() => ApiHelper.GetURI("api/ConfigMftsIndexGenerationSettingOutput/GetListAll"));

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


        public Response SendDeleteOneTime(int index)
        {
            DeleteOnetime deleteOnetime = new DeleteOnetime();
            deleteOnetime.pk = index;
            deleteOnetime.position = 0;
            string jsonString = JsonConvert.SerializeObject(deleteOnetime);
            return DeleteOneTime(jsonString).Result;
        }

        public async Task<Response> DeleteOneTime(string jsonString)
        {
            var httpContent = new StringContent(jsonString, Encoding.UTF8, "application/json");

            var task = await Task.Run(() => ApiHelper.PostURI("api/ConfigMftsIndexGenerationSettingOutput/DeleteOneTime", httpContent));

            //JsonResult Json = new JsonResult(task);
            return task;
        }

        public Response SendUpdateNextTime(int index, DateTime nexttime, bool clearonetime)
        {
            ConfigNextTime configNextTime = new ConfigNextTime();
            configNextTime.Id = index;
            configNextTime.NextTime = nexttime;
            if (clearonetime)
            {
                configNextTime.OneTimePosition = 0;
            }
            string jsonString = JsonConvert.SerializeObject(configNextTime);
            return UpdateNextTime(jsonString).Result;
        }

        public async Task<Response> UpdateNextTime(string jsonString)
        {
            var httpContent = new StringContent(jsonString, Encoding.UTF8, "application/json");

            var task = await Task.Run(() => ApiHelper.PostURI("api/ConfigMftsIndexGenerationSettingOutput/UpdateNextTime", httpContent));

            //JsonResult Json = new JsonResult(task);
            return task;
        }
    }
}
