using SCG.CAD.ETAX.PDF.SIGN.BussinessLayer;

namespace SCG.CAD.ETAX.PDF.SIGN
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        PDFSign pDFSign = new PDFSign();

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            pDFSign.ProcessPdfSign();
            //while (!stoppingToken.IsCancellationRequested)
            //{
            //    _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
            //    await Task.Delay(1000, stoppingToken);
            //}
        }
    }
}