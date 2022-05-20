using SCG.CAD.ETAX.XML.SIGN.BussinessLayer;

namespace SCG.CAD.ETAX.XML.SIGN
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        XMLSign xMLSign = new XMLSign();

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            xMLSign.ProcessXMLSign();
            //while (!stoppingToken.IsCancellationRequested)
            //{
            //    _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
            //    await Task.Delay(1000, stoppingToken);
            //}
        }
    }
}