using SCG.CAD.ETAX.MONITOR.BussinessLayer;
using SCG.CAD.ETAX.MONITOR.Models;

namespace SCG.CAD.ETAX.MONITOR
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            MonitorProgram runMonitor = MonitorProgram.NotMonitor;
            runMonitor = MonitorProgram.Monitor_PDFSign;
            Service service = new Service();
            switch (runMonitor)
            {
                case MonitorProgram.Monitor_EMAIL:
                    Application.Run(new Monitor_EMAIL());
                    break;
                case MonitorProgram.Monitor_PDFSign:
                    Application.Run(new Monitor_PDFSign());
                    break;
                case MonitorProgram.Monitor_PRINTZip:
                    Application.Run(new Monitor_PRINTZip());
                    break;
                case MonitorProgram.Monitor_XMLGenerator:
                    Application.Run(new Monitor_XMLGenerator());
                    break;
                case MonitorProgram.Monitor_XMLSign:
                    Application.Run(new Monitor_XMLSign());
                    break;
                case MonitorProgram.Monitor_XMLZip:
                    Application.Run(new Monitor_XMLZip());
                    break;
                case MonitorProgram.Monitor_INPUTINDEXING:
                    Application.Run(new Monitor_INPUTINDEXING());
                    break;
                case MonitorProgram.Monitor_OUTPUTINDEXING:
                    Application.Run(new Monitor_OUTPUTINDEXING());
                    break;
                default:
                    service.ShowMessageBox("Not Choose Program Monitor");
                    break;
            }

        }
    }
}