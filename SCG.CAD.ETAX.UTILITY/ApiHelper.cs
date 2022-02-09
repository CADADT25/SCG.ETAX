using Newtonsoft.Json;
using SCG.CAD.ETAX.MODEL;
using System.Configuration;
using System.Net;

namespace SCG.CAD.ETAX.UTILITY
{
    public static class ApiHelper
    {

        #region CommonReport
        public static async Task<HttpResponseMessage> Call(string url)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            var baseAdress = ConfigurationManager.AppSettings["ApiBaseAddress"];
            string apiUrl = baseAdress + url;

            using (HttpClient client = new HttpClient())
            {
                Response respService = new Response();
                client.BaseAddress = new Uri(apiUrl);

                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync(apiUrl);

                //var responseAUTH = await client.PutAsync(apiUrl, new { employee_number = en_no, application_code = app_code }).Result;

                return response;
            }
        }

        public static async Task<Response> PostURI(string url, HttpContent c)
        {
            Response response = new Response();
            try
            {
                using (var client = new HttpClient())
                {
                    var baseAdress = System.Configuration.ConfigurationManager.AppSettings["ApiBaseAddress"];
                    string apiUrl = baseAdress + url;

                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage result = await client.PostAsync(new Uri(apiUrl), c);
                    if (result.IsSuccessStatusCode)
                    {
                        var x = result.Content.ReadAsStringAsync().Result;
                        response = JsonConvert.DeserializeObject<Response>(x.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                response.STATUS = false;
                response.MESSAGE = ex.Message;
                response.ERROR_MESSAGE = ex.StackTrace;
            }

            return response;
        }

        public static async Task<Response> GetURI(string url)
        {
            Response res = new Response();
            using (var client = new HttpClient())
            {
                var baseAdress = System.Configuration.ConfigurationManager.AppSettings["ApiBaseAddress"];
                string apiUrl = baseAdress + url;

                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await client.GetAsync(new Uri(apiUrl));
                if (response.IsSuccessStatusCode)
                {
                    var x = response.Content.ReadAsStringAsync().Result;
                    res = JsonConvert.DeserializeObject<Response>(x.ToString());
                }
            }
            return res;
        }
        #endregion

        #region AdvanceReport

        #endregion

    }
}
