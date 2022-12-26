
using SCG.CAD.ETAX.OUTPUT.INDEXING.TO.DMS.BussinessLayer;
using SCG.CAD.ETAX.UTILITY;

namespace SCG.CAD.ETAX.OUTPUT.INDEXING.TO.DMS
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        OutputIndexing outputIndexing = new OutputIndexing();
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
                if (logicToolHelper.CheckBatchRunningTime("RUNNINGTIMEINDEXING"))
                {
                    //inputIndexing.ProcessIndexing();
                    outputIndexing.ProcessIndexing();
                }
                await Task.Delay(delaytime, stoppingToken);
            }
        }
    }
}