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

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                if (logicToolHelper.CheckBatchRunningTime("RUNNINGTIMEPDFSIGN"))
                {
                    pDFSign.ProcessPdfSign();
                }
                await Task.Delay(100000, stoppingToken);
            }
        }
    }
}