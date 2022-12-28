using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using SCG.CAD.ETAX.MODEL.CustomModel;
using SCG.CAD.ETAX.UTILITY.Controllers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SCG.CAD.ETAX.MONITOR
{
    public partial class ConnectHSM : Form
    {
        UtilityAPISignController utilityAPISignController = new UtilityAPISignController();
        static HttpClient client = new HttpClient();
        APIGetHSMSerialModel hsmserial = new APIGetHSMSerialModel();
        APIGetKeyAliasModel keyalias = new APIGetKeyAliasModel();
        public ConnectHSM()
        {
            InitializeComponent();
            SetVisible();
        }

        public void SetVisible()
        {
            button2.Visible = false;
            label2.Visible = false;
            listBox1.Visible = false;
            label3.Visible = false;
            comboBox1.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                SetVisible();
                string module = textBox1.Text;
                if (string.IsNullOrEmpty(module))
                {
                    module = "pse";
                }

                var res = utilityAPISignController.GetHSMSerialwithAPI(module).GetAwaiter().GetResult();

                if (res.resultCode.Equals("000"))
                {
                    comboBox1.Visible = true;
                    button2.Visible = true;
                    label2.Visible = true;
                    List<string> listHSMSerial = res.hsmSerialList.Select(x => x.hsmSerial).ToList();
                    SetValueComboBox(comboBox1, listHSMSerial);
                    label2.Text = comboBox1.SelectedValue.ToString();
                }
                textBox1.Enabled = false;
                button1.Enabled = false;
            }
            catch (Exception ex)
            {
                SetVisible();
                label3.Visible = true;
                label3.Text = ex.Message;
            }
        }

        public void SetValueComboBox(ComboBox comboBox, List<string> value)
        {
            try
            {
                DataTable dtblDataSource = new DataTable();
                dtblDataSource.Columns.Add("DisplayMember");
                dtblDataSource.Columns.Add("ValueMember");
                foreach (var item in value)
                {
                    dtblDataSource.Rows.Add(item, item);
                }

                comboBox.Items.Clear();
                comboBox.DataSource = dtblDataSource;
                comboBox.DisplayMember = "DisplayMember";
                comboBox.ValueMember = "ValueMember";
            }
            catch (Exception ex)
            {
                SetVisible();
                label3.Visible = true;
                label3.Text = ex.Message;
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (comboBox1.SelectedValue != null)
                {
                    label2.Text = comboBox1.SelectedValue.ToString();
                }
            }
            catch (Exception ex)
            {
                SetVisible();
                label3.Visible = true;
                label3.Text = ex.Message;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (comboBox1.SelectedValue != null)
                {
                    string module = textBox1.Text;
                    if (string.IsNullOrEmpty(module))
                    {
                        module = "pse";
                    }

                    var res = utilityAPISignController.GetKeyAliaswithAPI(module, comboBox1.SelectedValue.ToString()).GetAwaiter().GetResult();

                    if (res.resultCode.Equals("000"))
                    {
                        listBox1.Visible = true;
                        foreach(var item in res.certInfoList)
                        {
                            listBox1.Items.Add("certSerial : " + item.certSerial);
                            listBox1.Items.Add("keyAlias : " + item.keyAlias);
                            listBox1.Items.Add("-----------------------------------");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                SetVisible();
                label3.Visible = true;
                label3.Text = ex.Message;
            }
        }

    }
}
