using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using SCG.CAD.ETAX.MODEL;
using SCG.CAD.ETAX.MODEL.etaxModel;
using SCG.CAD.ETAX.MONITOR.BussinessLayer;
using SCG.CAD.ETAX.MONITOR.Models;
using System.Net.Http.Headers;

namespace SCG.CAD.ETAX.MONITOR
{
    internal static class Program
    {
        static HttpClient client = new HttpClient();
        static List<ConfigGlobal> configGlobal = new List<ConfigGlobal>();
        static Service service = new Service();
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            MonitorProgram runMonitor = MonitorProgram.MainMenu;
            RunAsyncGetConfig().GetAwaiter().GetResult();
            switch (runMonitor)
            {
                case MonitorProgram.Monitor_EMAIL:
                    Application.Run(new Monitor_EMAIL(configGlobal));
                    break;
                case MonitorProgram.Monitor_PDFSign:
                    Application.Run(new Monitor_PDFSign(configGlobal));
                    break;
                case MonitorProgram.Monitor_PRINTZip:
                    Application.Run(new Monitor_PRINTZip(configGlobal));
                    break;
                case MonitorProgram.Monitor_XMLGenerator:
                    Application.Run(new Monitor_XMLGenerator(configGlobal));
                    break;
                case MonitorProgram.Monitor_XMLSign:
                    Application.Run(new Monitor_XMLSign(configGlobal));
                    break;
                case MonitorProgram.Monitor_XMLZip:
                    Application.Run(new Monitor_XMLZip(configGlobal));
                    break;
                case MonitorProgram.Monitor_INPUTINDEXING:
                    Application.Run(new Monitor_INPUTINDEXING(configGlobal));
                    break;
                case MonitorProgram.Monitor_OUTPUTINDEXING:
                    Application.Run(new Monitor_OUTPUTINDEXING(configGlobal));
                    break;
                case MonitorProgram.MainMenu:
                    Application.Run(new MainMenu(configGlobal));
                    break;
                default:
                    service.ShowMessageBox("Not Choose Program Monitor");
                    break;
            }

        }

        static async Task RunAsyncGetConfig()
        {
            // Update port # in the following line.
            //client.BaseAddress = new Uri("https://localhost:7274/");

            string token = GetToken().Result.Token;
            var baseAdress = new ConfigurationBuilder().AddNewtonsoftJsonFile("appsettings.json").Build().GetSection("ApiConfig")["ApiBaseAddress"];
            client.BaseAddress = new Uri(baseAdress);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            Service service = new Service();

            try
            {

                // Get the product
                configGlobal = await GetListConfigGlobalAsync();

            }
            catch (Exception e)
            {
                service.ShowMessageBox(e.Message);
            }
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


        static async Task<List<ConfigGlobal>> GetListConfigGlobalAsync()
        {
            List<ConfigGlobal> result = new List<ConfigGlobal>();
            Response res = new Response();

            HttpResponseMessage response = await client.GetAsync("api/ConfigGlobal/GetListAll");
            if (response.IsSuccessStatusCode)
            {
                var x = response.Content.ReadAsStringAsync().Result;
                res = JsonConvert.DeserializeObject<Response>(x.ToString());
                if (res.STATUS)
                {
                    result = JsonConvert.DeserializeObject<List<ConfigGlobal>>(res.OUTPUT_DATA.ToString());
                }
            }
            return result;
        }
    }
}