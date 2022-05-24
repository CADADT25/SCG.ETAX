using SCG.CAD.ETAX.XML.ZIP.BussinessLayer;

namespace SCG.CAD.ETAX.XML.ZIP
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        XmlZIP xMLZIP = new XmlZIP();
        IHostApplicationLifetime _lifetime;
        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            xMLZIP.Xml_ZIP();
            _lifetime.StopApplication();
            //while (!stoppingToken.IsCancellationRequested)
            //{
            //    _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
            //    await Task.Delay(1000, stoppingToken);
            //}
        }
    }
}