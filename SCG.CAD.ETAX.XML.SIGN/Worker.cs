using SCG.CAD.ETAX.XML.SIGN.BussinessLayer;
using SCG.CAD.ETAX.UTILITY;

namespace SCG.CAD.ETAX.XML.SIGN
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        XMLSign xMLSign = new XMLSign();
        LogicToolHelper logicToolHelper = new LogicToolHelper();

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            int delaytime = 5 * 60 * 1000; // 5 minutes 
            while (!stoppingToken.IsCancellationRequested)
            {
                if (logicToolHelper.CheckBatchRunningTime("RUNNINGTIMEXMLSIGN"))
                {
                    xMLSign.ProcessXMLSign();
                }
                delaytime = logicToolHelper.GetDelayTimeProgram("DELAYRUNNINGTIMEXMLSIGN");
                await Task.Delay(delaytime, stoppingToken);
            }
        }
    }
}