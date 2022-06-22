global using SCG.CAD.ETAX.MODEL;
using SCG.CAD.ETAX.UTILITY;
using SCG.CAD.ETAX.XML.GENERATOR.BussinessLayer;

namespace SCG.CAD.ETAX.XML.GENERATOR
{
    public class Worker : BackgroundService
    {
        XMLGenerate xMLGenerate = new XMLGenerate();
        LogicToolHelper logicToolHelper = new LogicToolHelper();


        private readonly ILogger<Worker> _logger;

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            if (logicToolHelper.CheckBatchRunningTime("RUNNINGTIMEXMLGENERATOR"))
            {
                //// call business layer
                xMLGenerate.ProcessGenXMLFile();
                //var layer1 = pDFSign.GetAllPDFFile();

                //var layer2 = layer1;

                //while (!stoppingToken.IsCancellationRequested)
                //{
                //    _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                //    await Task.Delay(1000, stoppingToken);
                //}
            }

            while (!stoppingToken.IsCancellationRequested)
            {
                if (logicToolHelper.CheckBatchRunningTime("RUNNINGTIMEXMLGENERATOR"))
                {
                    //// call business layer
                    xMLGenerate.ProcessGenXMLFile();
                    //var layer1 = pDFSign.GetAllPDFFile();

                    //var layer2 = layer1;

                    //while (!stoppingToken.IsCancellationRequested)
                    //{
                    //    _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                    //    await Task.Delay(1000, stoppingToken);
                    //}
                }
                await Task.Delay(10000, stoppingToken);
            }
        }
    }
}