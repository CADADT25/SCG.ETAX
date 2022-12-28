using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using SCG.CAD.ETAX.MODEL;
using SCG.CAD.ETAX.MODEL.CustomModel;
using SCG.CAD.ETAX.MODEL.etaxModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCG.CAD.ETAX.UTILITY.Controllers
{
    public class UtilityAPISignController
    {
        public async Task<Response> SendFilePDFSign(APISendFilePDFSignModel data)
        {
            var jsonString = System.Text.Json.JsonSerializer.Serialize(data);
            var httpContent = new StringContent(jsonString, Encoding.UTF8, "application/json");

            var task = await Task.Run(() => PostURI("v1/sign/pdf", httpContent));

            //JsonResult Json = new JsonResult(task);
            return task;
        }

        public async Task<Response> SendFileXMLSign(APISendFileXMLSignModel data)
        {
            var jsonString = System.Text.Json.JsonSerializer.Serialize(data);
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
                var getException = task.Content.ReadAsStringAsync().Result;
                response.resultDes = getException.ToString();
            }
            return response;
        }

        public async Task<APIGetKeyAliasModel> GetKeyAlias(string jsonString)
        {
            var httpContent = new StringContent(jsonString, Encoding.UTF8, "application/json");
            APIGetKeyAliasModel response = new APIGetKeyAliasModel();

            var task = await Task.Run(() => PutURI("v1/keyAlias", httpContent));
            if (task.IsSuccessStatusCode)
            {
                var x = task.Content.ReadAsStringAsync().Result;
                response = JsonConvert.DeserializeObject<APIGetKeyAliasModel>(x.ToString());
            }
            else
            {
                var getException = task.Content.ReadAsStringAsync().Result;
                response.resultDes = getException.ToString();
            }
            return response;
        }

        public static async Task<Response> PostURI(string url, HttpContent c)
        {
            Response response = new Response();
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
                        //response = JsonConvert.DeserializeObject<APIResponseSignModel>(x.ToString());
                        response.OUTPUT_DATA = x.ToString();
                    }
                    else
                    {
                        var getException = result.Content.ReadAsStringAsync();
                        response.ERROR_MESSAGE = getException.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
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
                    var baseAdress = new ConfigurationBuilder().AddNewtonsoftJsonFile("appsettings.json").Build().GetSection("ApiConfig")["ApiSign"];

                    string apiUrl = baseAdress + url;

                    client.DefaultRequestHeaders.Accept.Clear();

                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                    HttpResponseMessage result = await client.PutAsync(new Uri(apiUrl), c);

                    response = result;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return response;
        }

        public async Task<APIGetHSMSerialModel> PostHSMSerialwithAPI(APIPostHSMSerialModel data)
        {
            var jsonString = System.Text.Json.JsonSerializer.Serialize(data);
            var httpContent = new StringContent(jsonString, Encoding.UTF8, "application/json");
            APIGetHSMSerialModel response = new APIGetHSMSerialModel();

            var task = await Task.Run(() => PostURIwithAPI("api/ConnectHSM/GetHSMSerial", httpContent));
            if (task.IsSuccessStatusCode)
            {
                var x = task.Content.ReadAsStringAsync().Result;
                response = JsonConvert.DeserializeObject<APIGetHSMSerialModel>(x.ToString());
            }
            else
            {
                var getException = task.Content.ReadAsStringAsync().Result;
                response.resultDes = getException.ToString();
            }
            return response;
        }

        public async Task<APIGetKeyAliasModel> PostKeyAliaswithAPI(APIPostKeyAliasModel data)
        {
            var jsonString = System.Text.Json.JsonSerializer.Serialize(data);
            var httpContent = new StringContent(jsonString, Encoding.UTF8, "application/json");
            APIGetKeyAliasModel response = new APIGetKeyAliasModel();

            var task = await Task.Run(() => PostURIwithAPI("api/ConnectHSM/GetKeyAlias", httpContent));
            if (task.IsSuccessStatusCode)
            {
                var x = task.Content.ReadAsStringAsync().Result;
                response = JsonConvert.DeserializeObject<APIGetKeyAliasModel>(x.ToString());
            }
            else
            {
                var getException = task.Content.ReadAsStringAsync().Result;
                response.resultDes = getException.ToString();
            }
            return response;
        }

        public static async Task<HttpResponseMessage> PostURIwithAPI(string url, HttpContent c)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            try
            {
                string token = GetToken().Result.Token;
                using (var client = new HttpClient())
                {
                    var baseAdress = new ConfigurationBuilder().AddNewtonsoftJsonFile("appsettings.json").Build().GetSection("ApiConfig")["ApiBaseAddress"];

                    string apiUrl = baseAdress + url;

                    client.DefaultRequestHeaders.Accept.Clear();

                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                    HttpResponseMessage result = await client.PostAsync(new Uri(apiUrl), c);

                    response = result;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return response;
        }

        public async Task<APIGetHSMSerialModel> GetHSMSerialwithAPI(string hsmName)
        {
            APIGetHSMSerialModel response = new APIGetHSMSerialModel();

            var task = Task.Run(() => GetURIwithAPI("api/ConnectHSM/GetHSMSerial?hsmName=" + hsmName)).GetAwaiter().GetResult();
            if (!string.IsNullOrEmpty(task))
            {
                response = JsonConvert.DeserializeObject<APIGetHSMSerialModel>(task);
            }
            else
            {
                //var getException = task.Content.ReadAsStringAsync().Result;
                //response.resultDes = getException.ToString();
            }
            return response;
        }

        public async Task<APIGetKeyAliasModel> GetKeyAliaswithAPI(string hsmName, string hsmSerial)
        {
            APIGetKeyAliasModel response = new APIGetKeyAliasModel();

            var task = Task.Run(() => GetURIwithAPI("api/ConnectHSM/GetKeyAlias?hsmName=" + hsmName + "&hsmSerial=" + hsmSerial)).GetAwaiter().GetResult();
            if (!string.IsNullOrEmpty(task))
            {
                response = JsonConvert.DeserializeObject<APIGetKeyAliasModel>(task);
            }
            else
            {
                //var getException = task.Content.ReadAsStringAsync().Result;
                //response.resultDes = getException.ToString();
            }
            return response;
        }

        public async Task<string> GetURIwithAPI(string url)
        {
            string response = "";
            string token = GetToken().Result.Token;
            try
            {
                using (var client = new HttpClient())
                {
                    var baseAdress = new ConfigurationBuilder().AddNewtonsoftJsonFile("appsettings.json").Build().GetSection("ApiConfig")["ApiBaseAddress"];

                    string apiUrl = baseAdress + url;

                    client.DefaultRequestHeaders.Accept.Clear();

                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                    HttpResponseMessage result = await client.GetAsync(apiUrl);

                    if (result.IsSuccessStatusCode)
                    {
                        var x = result.Content.ReadAsStringAsync().Result;
                        response = x.ToString();
                    }
                    else
                    {
                        var getException = result.Content.ReadAsStringAsync().Result;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return response;
        }

        public async Task<Response> GetSyncCertificate()
        {
            Response response = new Response();

            var task = await Task.Run(() => ApiHelper.GetURI("api/APISign/SyncCertificate"));
            if (task.STATUS)
            {
                //response = JsonConvert.DeserializeObject<Response>(task.OUTPUT_DATA.ToString());
            }
            else
            {
                //var getException = task.Content.ReadAsStringAsync().Result;
                //response.resultDes = getException.ToString();
            }
            return response;
        }

        private static async Task<AuthModel> GetToken()
        {
            AuthModel res = new AuthModel();
            using (var client = new HttpClient())
            {
                var baseAdress = new ConfigurationBuilder().AddNewtonsoftJsonFile("appsettings.json").Build().GetSection("ApiConfig")["ApiBaseAddress"];

                string apiUrl = baseAdress + "api/Auth/GetToken";

                client.DefaultRequestHeaders.Accept.Clear();

                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue(new ConfigurationBuilder().AddNewtonsoftJsonFile("appsettings.json").Build().GetSection("Jwt")["AppKey"]);

                HttpResponseMessage response = await client.GetAsync(apiUrl);

                //var getException = await client.GetAsync(apiUrl).Result.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    var x = response.Content.ReadAsStringAsync().Result;

                    res = JsonConvert.DeserializeObject<AuthModel>(x.ToString());
                }
            }
            return res;
        }
    }
}
