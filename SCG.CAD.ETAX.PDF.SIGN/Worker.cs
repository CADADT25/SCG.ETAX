using SCG.CAD.ETAX.MODEL.etaxModel;
using SCG.CAD.ETAX.PDF.SIGN.BussinessLayer;
using SCG.CAD.ETAX.UTILITY;

namespace SCG.CAD.ETAX.PDF.SIGN
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        PDFSign pDFSign = new PDFSign();
        LogicToolHelper logicToolHelper = new LogicToolHelper(); 
        LogHelper log = new LogHelper();
        string pathlog = @"D:\log\";

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            log.InsertLog(pathlog, "Test Service");
            while (!stoppingToken.IsCancellationRequested)
            {
                log.InsertLog(pathlog, "Loop Service");
                //_logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await Task.Delay(10000, stoppingToken);
            }
            //if (logicToolHelper.CheckBatchRunningTime("RUNNINGTIMEPDFSIGN"))
            //{
            //    pDFSign.ProcessPdfSign();
            //    //while (!stoppingToken.IsCancellationRequested)
            //    //{
            //    //    _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
            //    //    await Task.Delay(1000, stoppingToken);
            //    //}
            //}
        }
    }
}