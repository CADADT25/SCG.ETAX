using SCG.CAD.ETAX.INPUT.INDEXING.TO.DMS.BussinessLayer;
using SCG.CAD.ETAX.UTILITY;

namespace SCG.CAD.ETAX.INPUT.INDEXING.TO.DMS
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        InputIndexing inputIndexing = new InputIndexing();
        LogicToolHelper logicToolHelper = new LogicToolHelper();

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                if (logicToolHelper.CheckBatchRunningTime("RUNNINGTIMEINDEXING"))
                {
                    inputIndexing.ProcessIndexing();
                }
                await Task.Delay(100000, stoppingToken);
            }
        }
    }
}