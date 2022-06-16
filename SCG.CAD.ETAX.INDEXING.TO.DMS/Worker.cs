
using SCG.CAD.ETAX.INDEXING.TO.DMS.BussinessLayer;
using SCG.CAD.ETAX.UTILITY;

namespace SCG.CAD.ETAX.INDEXING.TO.DMS
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
            if (logicToolHelper.CheckBatchRunningTime("RUNNINGTIMEPDFSIGN"))
            {
                inputIndexing.ProcessIndexing();
                //while (!stoppingToken.IsCancellationRequested)
                //{
                //    _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                //    await Task.Delay(1000, stoppingToken);
                //}
            }
        }
    }
}