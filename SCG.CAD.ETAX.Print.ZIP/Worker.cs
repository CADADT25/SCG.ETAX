using SCG.CAD.ETAX.MODEL.etaxModel;
using SCG.CAD.ETAX.PRINT.ZIP.BussinessLayer;
using SCG.CAD.ETAX.UTILITY;

namespace SCG.CAD.ETAX.Print.ZIP
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        PrintZIP printZIP = new PrintZIP();
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
                if (logicToolHelper.CheckBatchRunningTime("RUNNINGTIMEPRINTZIP"))
                {
                    printZIP.ProcessPrintzip();
                }
                await Task.Delay(delaytime, stoppingToken);
            }
        }
    }
}