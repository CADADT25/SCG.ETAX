using SCG.CAD.ETAX.EMAIL.BussinessLayer;
using SCG.CAD.ETAX.MODEL.etaxModel;
using SCG.CAD.ETAX.UTILITY.Controllers;

namespace SCG.CAD.ETAX.EMAIL
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        Email email = new Email();
        TestEmail testemail = new TestEmail();
        ConfigGlobalController configGlobalController = new ConfigGlobalController();
        List<ConfigGlobal> configGlobals = new List<ConfigGlobal>();
        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                if (CheckRunningTime())
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

        public void GetGlobalConfig()
        {
            try
            {
                configGlobals = configGlobalController.List().Result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool CheckRunningTime()
        {
            bool result = false;
            try
            {
                GetGlobalConfig();
                var config = configGlobals.FirstOrDefault(x => x.ConfigGlobalName == "RUNNINGTIMESENDEMAIL");
                if (config != null)
                {
                    if (config.ConfigGlobalValue != null && !String.IsNullOrEmpty(config.ConfigGlobalValue))
                    {
                        if (config.ConfigGlobalValue.IndexOf(DateTime.Now.ToString("HH:mm")) >= 0)
                        {
                            result = true;
                        }
                    }
                    else
                    {
                        result = true;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }
    }
}