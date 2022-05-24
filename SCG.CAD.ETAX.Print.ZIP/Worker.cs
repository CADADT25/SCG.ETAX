using SCG.CAD.ETAX.PRINT.ZIP.BussinessLayer;

namespace SCG.CAD.ETAX.Print.ZIP
{
    public class Worker : BackgroundService
    {
        IHostApplicationLifetime _lifetime;
        private readonly ILogger<Worker> _logger;
        PrintZIP printZIP = new PrintZIP();
        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            printZIP.ProcessPrintzip();
            _lifetime.StopApplication();
            //while (!stoppingToken.IsCancellationRequested)
            //{
            //    _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
            //    await Task.Delay(1000, stoppingToken);
            //}
        }
    }
}