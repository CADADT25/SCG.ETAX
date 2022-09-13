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
            int delaytime = 5 * 60 * 1000; // 5 minutes 
            while (!stoppingToken.IsCancellationRequested)
            {
                if (logicToolHelper.CheckBatchRunningTime("RUNNINGTIMEXMLGENERATOR"))
                {
                    //// call business layer
                    xMLGenerate.ProcessGenXMLFile();
                }
                delaytime = logicToolHelper.GetDelayTimeProgram("DELAYRUNNINGTIMEXMLGENERATOR");
                await Task.Delay(delaytime, stoppingToken);
            }
        }
    }
}