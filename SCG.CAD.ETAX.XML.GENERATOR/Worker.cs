global using SCG.CAD.ETAX.MODEL;
using Newtonsoft.Json;
using SCG.CAD.ETAX.MODEL.etaxModel;
using SCG.CAD.ETAX.UTILITY;
using SCG.CAD.ETAX.XML.GENERATOR.BussinessLayer;
using SCG.CAD.ETAX.XML.GENERATOR.Models;
using SCG.CAD.ETAX.UTILITY.Controllers;

namespace SCG.CAD.ETAX.XML.GENERATOR
{
    public class Worker : BackgroundService
    {
        XMLGenerate xMLGenerate = new XMLGenerate();
        Template_TaxInvoice template = new Template_TaxInvoice();
        TaxCodeController taxCodeController = new TaxCodeController();
        List<TaxCode> tran = new List<TaxCode>();
        ConfigGlobalController configGlobalController = new ConfigGlobalController();
        List<ConfigGlobal> configGlobals = new List<ConfigGlobal>();


        private readonly ILogger<Worker> _logger;

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            if (CheckRunningTime())
            {
                // call business layer
                xMLGenerate.ProcessGenXMLFile();
                //var layer1 = pDFSign.GetAllPDFFile();

                //var layer2 = layer1;

                //while (!stoppingToken.IsCancellationRequested)
                //{
                //    _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                //    await Task.Delay(1000, stoppingToken);
                //}
            }
        }

        public void GetGlobalConfig()
        {
            try
            {
                configGlobals = configGlobalController.List().Result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool CheckRunningTime()
        {
            bool result = false;
            try
            {
                GetGlobalConfig();
                var config = configGlobals.FirstOrDefault(x => x.ConfigGlobalName == "RUNNINGTIMEXMLGENERATOR");
                if (config != null)
                {
                    if (config.ConfigGlobalValue != null && !String.IsNullOrEmpty(config.ConfigGlobalValue))
                    {
                        if (config.ConfigGlobalValue.IndexOf(DateTime.Now.ToString("HH:mm")) >= 0)
                        {
                            result = true;
                        }
                    }
                    else
                    {
                        result = true;
                    }
                }
                result = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }
    }
}