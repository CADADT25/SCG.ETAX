
using SCG.CAD.ETAX.INDEXING.TO.DMS.BussinessLayer;
using SCG.CAD.ETAX.UTILITY;

namespace SCG.CAD.ETAX.INDEXING.TO.DMS
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        InputIndexing inputIndexing = new InputIndexing(); 
        OutputIndexing outputIndexing = new OutputIndexing();
        LogicToolHelper logicToolHelper = new LogicToolHelper();

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                if (logicToolHelper.CheckBatchRunningTime("RUNNINGTIMEPDFSIGN"))
                {
                    //inputIndexing.ProcessIndexing();
                    outputIndexing.ProcessIndexing();
                }
                await Task.Delay(100000, stoppingToken);
            }
        }
    }
}