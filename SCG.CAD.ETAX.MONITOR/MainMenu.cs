using Newtonsoft.Json;
using SCG.CAD.ETAX.MODEL;
using SCG.CAD.ETAX.MODEL.etaxModel;
using SCG.CAD.ETAX.MONITOR.BussinessLayer;
using SCG.CAD.ETAX.MONITOR.Models;
using SCG.CAD.ETAX.UTILITY.Controllers;
using System.Net.Http.Headers;

namespace SCG.CAD.ETAX.MONITOR
{
    public partial class MainMenu : Form
    {
        UtilityConfigGlobalController configGlobalController = new UtilityConfigGlobalController();

        static HttpClient client = new HttpClient();
        static List<ConfigGlobal> configGlobal = new List<ConfigGlobal>();
        static Service service = new Service();
        public MonitorProgram runMonitor = MonitorProgram.NotMonitor;
        Thread threadMonitorMonitorEMAIL;
        Thread threadMonitorMonitorPDFSign;
        Thread threadMonitorMonitorPRINTZip;
        Thread threadMonitorMonitorXMLGenerator;
        Thread threadMonitorMonitorXMLSign;
        Thread threadMonitorMonitorXMLZip;
        Thread threadMonitorMonitorINPUTINDEXING;
        Thread threadMonitorMonitorOUTPUTINDEXING;

        public Monitor_XMLGenerator monitorXMLGenerator;
        public Monitor_XMLSign monitorXMLSign;
        public Monitor_PDFSign monitorPDFSign;
        public Monitor_PRINTZip monitorPRINTZip;
        public Monitor_XMLZip monitorXMLZip;
        public Monitor_EMAIL monitorEMAIL;
        public Monitor_OUTPUTINDEXING monitorOUTPUTINDEXING;
        public Monitor_INPUTINDEXING monitorINPUTINDEXING;

        public MainMenu(List<ConfigGlobal> config)
        {
            InitializeComponent();
            configGlobal = config;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (CheckMonitorRunning("Monitor_XMLGenerator"))
            {
                monitorXMLGenerator.Close();
            }
            else
            {
                monitorXMLGenerator = new Monitor_XMLGenerator(configGlobal);
                monitorXMLGenerator.Show();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (CheckMonitorRunning("Monitor_XMLSign"))
            {
                monitorXMLSign.Close();
            }
            else
            {
                monitorXMLSign = new Monitor_XMLSign(configGlobal);
                monitorXMLSign.Show();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (CheckMonitorRunning("Monitor_PDFSign"))
            {
                monitorPDFSign.Close();
            }
            else
            {
                monitorPDFSign = new Monitor_PDFSign(configGlobal);
                monitorPDFSign.Show();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (CheckMonitorRunning("Monitor_PRINTZip"))
            {
                monitorPRINTZip.Close();
            }
            else
            {
                monitorPRINTZip = new Monitor_PRINTZip(configGlobal);
                monitorPRINTZip.Show();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (CheckMonitorRunning("Monitor_XMLZip"))
            {
                monitorXMLZip.Close();
            }
            else
            {
                monitorXMLZip = new Monitor_XMLZip(configGlobal);
                monitorXMLZip.Show();
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (CheckMonitorRunning("Monitor_EMAIL"))
            {
                monitorEMAIL.Close();
            }
            else
            {
                monitorEMAIL = new Monitor_EMAIL(configGlobal);
                monitorEMAIL.Show();
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (CheckMonitorRunning("Monitor_OUTPUTINDEXING"))
            {
                monitorOUTPUTINDEXING.Close();
            }
            else
            {
                monitorOUTPUTINDEXING = new Monitor_OUTPUTINDEXING(configGlobal);
                monitorOUTPUTINDEXING.Show();
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (CheckMonitorRunning("Monitor_INPUTINDEXING"))
            {
                monitorINPUTINDEXING.Close();
            }
            else
            {
                monitorINPUTINDEXING = new Monitor_INPUTINDEXING(configGlobal);
                monitorINPUTINDEXING.Show();
            }
        }

        public bool CheckMonitorRunning(string monitorname)
        {
            bool checkrunning = false;
            FormCollection fc = Application.OpenForms;
            foreach (Form frm in fc)
            {
                if (frm.Name == monitorname)
                {
                    checkrunning = true;
                }
            }
            return checkrunning;
        }
    }
}
