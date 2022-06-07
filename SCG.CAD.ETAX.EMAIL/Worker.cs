using SCG.CAD.ETAX.EMAIL.BussinessLayer;
using SCG.CAD.ETAX.UTILITY;

namespace SCG.CAD.ETAX.EMAIL
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        Email email = new Email();
        TestEmail testemail = new TestEmail();
        LogicToolHelper logicToolHelper = new LogicToolHelper();
        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                if (logicToolHelper.CheckBatchRunningTime("RUNNINGTIMESENDEMAIL"))
                {
                    email.ProcessSendEmail();
                    //testemail.TestSendEmail();
                    //testemail.ToEmlStream();
                    //_lifetime.StopApplication();
                }
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await Task.Delay(1000 * 60, stoppingToken);
            }
        }
    }
}