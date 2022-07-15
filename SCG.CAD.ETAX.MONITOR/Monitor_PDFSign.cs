using SCG.CAD.ETAX.MODEL.etaxModel;
using SCG.CAD.ETAX.MONITOR.BussinessLayer;
using System.Data;

namespace SCG.CAD.ETAX.MONITOR
{
    public partial class Monitor_PDFSign : Form
    {
        Service service = new Service();
        ConfigGlobal configGlobal = new ConfigGlobal();
        bool stopcheckservicestatus = true;
        bool stopreadlogfile = true;
        string status = "";
        string servicename = "SCG.CAD.ETAX.PDF.SIGN";
        string namepathlog = "PATHLOGFILE_PDFSIGN";
        string pathlogcurrentdate = @"D:\";
        int length = 0;
        List<string> pathfilelog = new List<string>();
        public Monitor_PDFSign(List<ConfigGlobal> config)
        {
            try
            {
                InitializeComponent();
                GetConfig(config);
                InfiniteLoopCheckServiceStatus();
                pathfilelog = service.ReadAllLogFile(configGlobal.ConfigGlobalValue);
                //pathfilelog = service.ReadAllLogFile(@"D:\log\");
                SetValueComboBox();

            }
            catch (Exception ex)
            {
                service.ShowMessageBox(ex.Message);
            }
        }

        public async void InfiniteLoopCheckServiceStatus()
        {
            try
            {
                while (stopcheckservicestatus)
                {
                    status = service.GetStatusService(servicename);
                    lblstatus.Text = status;
                    if (!status.Equals("Running", StringComparison.InvariantCultureIgnoreCase))
                    {
                        btnprocess.Text = "Start Service";
                    }
                    else
                    {
                        btnprocess.Text = "Stop Service";
                        InfiniteLoopReadLogFile();
                    }
                    await service.SetDelay();
                    btnprocess.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                service.ShowMessageBox(ex.Message);
            }
        }

        public async void InfiniteLoopReadLogFile()
        {
            try
            {
                while (stopreadlogfile)
                {
                    string content = service.ReadFileOnly(pathlogcurrentdate);
                    if (length == content.Length)
                    {
                        richTextBox2.Clear();
                        richTextBox2.Focus();
                        richTextBox2.AppendText(content);
                    }
                    else
                    {
                        length = content.Length;
                    }
                    await service.SetDelay();
                }
            }
            catch (Exception ex)
            {
                service.ShowMessageBox(ex.Message);
            }
        }

        public void btnprocess_Click(object sender, EventArgs e)
        {
            try
            {
                btnprocess.Enabled = false;
                if (!status.Equals("Running", StringComparison.InvariantCultureIgnoreCase))
                {
                    service.StartService(servicename, 1);
                    pathfilelog = service.ReadAllLogFile(configGlobal.ConfigGlobalValue);
                    //pathfilelog = service.ReadAllLogFile(@"D:\log\");
                    stopreadlogfile = true;
                }
                else
                {
                    service.StopService(servicename, 1);
                    stopreadlogfile = false;
                }
            }
            catch (Exception ex)
            {
                service.ShowMessageBox(ex.Message);
            }
        }

        public void GetConfig(List<ConfigGlobal> config)
        {
            try
            {
                configGlobal = config.First(x => x.ConfigGlobalName == namepathlog);
                label4.Text = servicename;
                pathlogcurrentdate = configGlobal.ConfigGlobalValue + "\\" + DateTime.Now.ToString("yyyyMMdd") + ".txt";
            }
            catch (Exception ex)
            {
                service.ShowMessageBox(ex.Message);
            }
        }

        public void SetValueComboBox()
        {
            try
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
            catch (Exception ex)
            {
                service.ShowMessageBox(ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (cbbpath.SelectedValue != null)
                {
                    string content = service.ReadFileOnly(cbbpath.SelectedValue.ToString());
                    richTextBox1.Text = content;
                }
            }
            catch (Exception ex)
            {
                service.ShowMessageBox(ex.Message);
            }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (tabControl1.SelectedTab.Name == "ReadLogFile")
                {
                    stopcheckservicestatus = false;
                    stopreadlogfile = false;
                }
                else
                {
                    stopcheckservicestatus = true;
                    InfiniteLoopCheckServiceStatus();
                }
            }
            catch (Exception ex)
            {
                service.ShowMessageBox(ex.Message);
            }
        }


    }
}