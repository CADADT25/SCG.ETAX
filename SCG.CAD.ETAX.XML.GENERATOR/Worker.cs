global using SCG.CAD.ETAX.MODEL;
using Newtonsoft.Json;
using SCG.CAD.ETAX.MODEL.etaxModel;
using SCG.CAD.ETAX.UTILITY;
using SCG.CAD.ETAX.XML.GENERATOR.BussinessLayer;
using SCG.CAD.ETAX.XML.GENERATOR.Models;

namespace SCG.CAD.ETAX.XML.GENERATOR
{
    public class Worker : BackgroundService
    {
        PDFSign pDFSign = new PDFSign();
        XMLGenerate xMLGenerate = new XMLGenerate();
        Template_TaxInvoice template = new Template_TaxInvoice();
        TaxCodeController taxCodeController = new TaxCodeController();
        List<TaxCode> tran = new List<TaxCode>();


        private readonly ILogger<Worker> _logger;

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            //CrossIndustryInvoice data = new CrossIndustryInvoice();
            //var xml = template.XMLtemplate(data);
            //tran = taxCodeController.List().Result;


            // call business layer
            xMLGenerate.ReadXMLFile();
            //var layer1 = pDFSign.GetAllPDFFile();

            //var layer2 = layer1;


            //Response resp = new Response();

            //List<TaxCode> tran = new List<TaxCode>();

            //try
            //{
            //    var task = await Task.Run(() => ApiHelper.GetURI("api/TaxCode/GetListAll"));

            //    if (task.STATUS)
            //    {
            //        tran = JsonConvert.DeserializeObject<List<TaxCode>>(task.OUTPUT_DATA.ToString());

            //        foreach (var item in tran)
            //        {
            //            _logger.LogInformation("Worker {item} running at: {time}", item.TaxCodeRd.ToString() , DateTimeOffset.Now);
            //            await Task.Delay(1000, stoppingToken);
            //        }
            //    }

            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine(ex.InnerException);
            //}


            //while (!stoppingToken.IsCancellationRequested)
            //{
            //    _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
            //    await Task.Delay(1000, stoppingToken);
            //}
        }
    }
}