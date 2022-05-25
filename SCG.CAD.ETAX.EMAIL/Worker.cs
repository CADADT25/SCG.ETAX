using SCG.CAD.ETAX.EMAIL.BussinessLayer;

namespace SCG.CAD.ETAX.EMAIL
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        Email email = new Email();
        TestEmail testemail = new TestEmail();
        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            email.ProcessSendEmail();
            //testemail.TestSendEmail();
            //testemail.ToEmlStream();
            //_lifetime.StopApplication();
            //while (!stoppingToken.IsCancellationRequested)
            //{
            //    _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
            //    await Task.Delay(1000, stoppingToken);
            //}
        }
    }
}