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
            MonitorProgram runMonitor = MonitorProgram.NotMonitor;
            runMonitor = MonitorProgram.Monitor_INPUTINDEXING;
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
                default:
                    service.ShowMessageBox("Not Choose Program Monitor");
                    break;
            }

        }

        static async Task RunAsyncGetConfig()
        {
            // Update port # in the following line.
            client.BaseAddress = new Uri("https://localhost:7274/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

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