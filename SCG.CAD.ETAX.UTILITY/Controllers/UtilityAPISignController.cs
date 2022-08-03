using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using SCG.CAD.ETAX.MODEL.CustomModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCG.CAD.ETAX.UTILITY.Controllers
{
    public class UtilityAPISignController
    {
        public async Task<APIResponseSignModel> SendFilePDFSign(string jsonString)
        {
            var httpContent = new StringContent(jsonString, Encoding.UTF8, "application/json");

            var task = await Task.Run(() => PostURI("v1/sign/pdf", httpContent));

            //JsonResult Json = new JsonResult(task);
            return task;
        }

        public async Task<APIResponseSignModel> SendFileXMLSign(string jsonString)
        {
            var httpContent = new StringContent(jsonString, Encoding.UTF8, "application/json");

            var task = await Task.Run(() => PostURI("v1/sign/xml", httpContent));

            //JsonResult Json = new JsonResult(task);
            return task;
        }

        public async Task<APIGetHSMSerialModel> GetHSMSerial(string jsonString)
        {
            var httpContent = new StringContent(jsonString, Encoding.UTF8, "application/json");
            APIGetHSMSerialModel response = new APIGetHSMSerialModel();

            var task = await Task.Run(() => PutURI("v1/hsmSerial", httpContent));
            if (task.IsSuccessStatusCode)
            {
                var x = task.Content.ReadAsStringAsync().Result;
                response = JsonConvert.DeserializeObject<APIGetHSMSerialModel>(x.ToString());
            }
            else
            {
                var getException = task.Content.ReadAsStringAsync();
                response.resultDes = getException.ToString();
            }
            return response;
        }

        public async Task<APIGetKeyAliasModel> GetKeyAlias(string jsonString)
        {
            var httpContent = new StringContent(jsonString, Encoding.UTF8, "application/json");
            APIGetKeyAliasModel response = new APIGetKeyAliasModel();

            var task = await Task.Run(() => PutURI("v1/hsmSerial", httpContent));
            if (task.IsSuccessStatusCode)
            {
                var x = task.Content.ReadAsStringAsync().Result;
                response = JsonConvert.DeserializeObject<APIGetKeyAliasModel>(x.ToString());
            }
            else
            {
                var getException = task.Content.ReadAsStringAsync();
                response.resultDes = getException.ToString();
            }
            return response;
        }

        public static async Task<APIResponseSignModel> PostURI(string url, HttpContent c)
        {
            APIResponseSignModel response = new APIResponseSignModel();
            try
            {
                using (var client = new HttpClient())
                {
                    var baseAdress = new ConfigurationBuilder().AddNewtonsoftJsonFile("appsettings.json").Build().GetSection("ApiConfig")["ApiPDFSign"];

                    string apiUrl = baseAdress + url;

                    client.DefaultRequestHeaders.Accept.Clear();

                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                    HttpResponseMessage result = await client.PostAsync(new Uri(apiUrl), c);

                    if (result.IsSuccessStatusCode)
                    {
                        var x = result.Content.ReadAsStringAsync().Result;
                        response = JsonConvert.DeserializeObject<APIResponseSignModel>(x.ToString());
                    }
                    else
                    {
                        var getException = result.Content.ReadAsStringAsync();
                        response.resultDes = getException.ToString();
                    }
                }
            }
            catch (Exception ex)
            {

            }

            return response;
        }

        public static async Task<HttpResponseMessage> PutURI(string url, HttpContent c)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            try
            {
                using (var client = new HttpClient())
                {
                    var baseAdress = new ConfigurationBuilder().AddNewtonsoftJsonFile("appsettings.json").Build().GetSection("ApiConfig")["ApiPDFSign"];

                    string apiUrl = baseAdress + url;

                    client.DefaultRequestHeaders.Accept.Clear();

                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                    HttpResponseMessage result = await client.PostAsync(new Uri(apiUrl), c);

                    response = result;
                }
            }
            catch (Exception ex)
            {

            }

            return response;
        }
    }
}
