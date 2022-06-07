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
                default:
                    Console.WriteLine("Nothing");
                    break;
            }

        }

        public enum MonitorProgram
        {
            Monitor_EMAIL = 1,
            Monitor_PDFSign = 2,
            Monitor_PRINTZip = 3,
            Monitor_XMLGenerator = 4,
            Monitor_XMLSign = 5,
            Monitor_XMLZip = 6,
            NotMonitor = 99
        }
    }
}