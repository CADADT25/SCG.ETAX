using SCG.CAD.ETAX.XML.ZIP.BussinessLayer;
using SCG.CAD.ETAX.UTILITY;

namespace SCG.CAD.ETAX.XML.ZIP
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        XmlZIP xMLZIP = new XmlZIP();
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
                if (logicToolHelper.CheckBatchRunningTime("RUNNINGTIMEXMLZIP"))
                {
                    xMLZIP.Xml_ZIP();
                }
                await Task.Delay(delaytime, stoppingToken);
            }
        }
    }
}