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
            if (logicToolHelper.CheckBatchRunningTime("RUNNINGTIMEPRINTZIP"))
            {
                printZIP.ProcessPrintzip();
                //while (!stoppingToken.IsCancellationRequested)
                //{
                //    _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                //    await Task.Delay(1000, stoppingToken);
                //}
            }
            //while (!stoppingToken.IsCancellationRequested)
            //{
            //    _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
            //    await Task.Delay(1000, stoppingToken);
            //}
        }
    }
}