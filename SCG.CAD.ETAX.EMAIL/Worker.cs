using SCG.CAD.ETAX.EMAIL.BussinessLayer;

namespace SCG.CAD.ETAX.EMAIL
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        Email email = new Email();
        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            //email.ProcessSendEmail();
            email.TestSendEmail();
            //while (!stoppingToken.IsCancellationRequested)
            //{
            //    _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
            //    await Task.Delay(1000, stoppingToken);
            //}
        }
    }
}