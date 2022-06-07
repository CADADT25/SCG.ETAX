using SCG.CAD.ETAX.MODEL.etaxModel;
using SCG.CAD.ETAX.MONITOR.BussinessLayer;
using System.Data;
using System.ServiceProcess;

namespace SCG.CAD.ETAX.MONITOR
{
    public partial class Monitor_XMLGenerator : Form
    {
        Service service = new Service();
        ConfigGlobal configGlobal = new ConfigGlobal();
        bool stopcheckservicestatus = true;
        bool stopreadlogfile = true;
        string status = "";
        string servicename = "SCG.CAD.ETAX.EMAIL";
        string namepathlog = "PATHLOGFILE_XMLGENERATOR";
        List<string>  pathfilelog = new List<string>();
        public Monitor_XMLGenerator()
        {
            InitializeComponent();
            //GetConfig();
            InfiniteLoopCheckServiceStatus();
            //pathfilelog = service.ReadAllLogFile(configGlobal.ConfigGlobalValue);
            pathfilelog = service.ReadAllLogFile(@"C:\Code_Dev\sign\");
            SetValueComboBox();
        }

        public async void InfiniteLoopCheckServiceStatus()
        {
            while (stopcheckservicestatus)
            {
                await Task.Delay(100);
                status = service.GetStatusService(servicename);
                lblstatus.Text = status;
                if (status.Equals("Running", StringComparison.InvariantCultureIgnoreCase))
                {
                    btnprocess.Text = "Stop Service";
                }
                else
                {
                    btnprocess.Text = "Start Service";
                }
            }
        }

        public async void InfiniteLoopReadLogFile()
        {
            while (stopreadlogfile)
            {
                await Task.Delay(100);
                string content = File.ReadAllText(pathfilelog.First());
                richTextBox1.Text = content;
            }
        }

        public void btnprocess_Click(object sender, EventArgs e)
        {
            if(status.Equals("Running",StringComparison.InvariantCultureIgnoreCase))
            {
                service.StartService(servicename,1);
                //pathfilelog = service.ReadAllLogFile(configGlobal.ConfigGlobalValue);
                pathfilelog = service.ReadAllLogFile(@"C:\Code_Dev\sign\");
                stopreadlogfile = true;
                InfiniteLoopReadLogFile();
            }
            else
            {
                service.StopService(servicename,1);
                stopreadlogfile = false;
            }
        }

        public void GetConfig()
        {
            try
            {
                configGlobal = service.GetConfigGlobal().First(x=> x.ConfigGlobalName == namepathlog);
                label4.Text = servicename;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void SetValueComboBox()
        {
            ComboBox comboBox;
            List<ComboBox> comboBoxs = new List<ComboBox>();

            DataTable dtblDataSource = new DataTable();
            dtblDataSource.Columns.Add("DisplayMember");
            dtblDataSource.Columns.Add("ValueMember");
            foreach (var item in pathfilelog)
            {
                comboBox = new ComboBox();
                comboBox.DisplayMember = Path.GetFileName(item);
                comboBox.ValueMember = item;
                comboBoxs.Add(comboBox);
                dtblDataSource.Rows.Add(Path.GetFileName(item), item);
            }

            cbbpath.Items.Clear();
            cbbpath.DataSource = dtblDataSource;
            cbbpath.DisplayMember = "DisplayMember";
            cbbpath.ValueMember = "ValueMember";


        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string content = File.ReadAllText(cbbpath.SelectedValue.ToString());
                richTextBox1.Text = content;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //private void tabPage1_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (tabControl1.SelectedTab == tabControl1.TabPages["tabPage1"])//your specific tabname
        //    {
        //        SetValueComboBox();
        //    }
        //    else if (tabControl1.SelectedTab == tabControl1.TabPages["tabPage2"])//your specific tabname
        //    {
        //        InfiniteLoopReadLogFile();
        //    }
        //}
        //private void tabPage2_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (tabControl1.SelectedTab == tabControl1.TabPages["tabPage1"])//your specific tabname
        //    {
        //        SetValueComboBox();
        //    }
        //    else if (tabControl1.SelectedTab == tabControl1.TabPages["tabPage2"])//your specific tabname
        //    {
        //        InfiniteLoopReadLogFile();
        //    }
        //}

    }
}