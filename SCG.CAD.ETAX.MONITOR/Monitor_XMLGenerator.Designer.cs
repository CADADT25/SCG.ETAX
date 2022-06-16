namespace SCG.CAD.ETAX.MONITOR
{
    partial class Monitor_XMLGenerator
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.ReadLogFile = new System.Windows.Forms.TabPage();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.cbbpath = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.StopStartService = new System.Windows.Forms.TabPage();
            this.label4 = new System.Windows.Forms.Label();
            this.lblstatus = new System.Windows.Forms.Label();
            this.btnprocess = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.richTextBox2 = new System.Windows.Forms.RichTextBox();
            this.tabControl1.SuspendLayout();
            this.ReadLogFile.SuspendLayout();
            this.StopStartService.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.ReadLogFile);
            this.tabControl1.Controls.Add(this.StopStartService);
            this.tabControl1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.tabControl1.Location = new System.Drawing.Point(15, 15);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1548, 864);
            this.tabControl1.TabIndex = 4;
            // 
            // ReadLogFile
            // 
            this.ReadLogFile.Controls.Add(this.richTextBox1);
            this.ReadLogFile.Controls.Add(this.button1);
            this.ReadLogFile.Controls.Add(this.cbbpath);
            this.ReadLogFile.Controls.Add(this.label1);
            this.ReadLogFile.Location = new System.Drawing.Point(4, 34);
            this.ReadLogFile.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.ReadLogFile.Name = "ReadLogFile";
            this.ReadLogFile.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.ReadLogFile.Size = new System.Drawing.Size(1540, 826);
            this.ReadLogFile.TabIndex = 0;
            this.ReadLogFile.Text = "ReadLogFile";
            this.ReadLogFile.UseVisualStyleBackColor = true;
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(38, 102);
            this.richTextBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(1453, 679);
            this.richTextBox1.TabIndex = 3;
            this.richTextBox1.Text = "";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(979, 35);
            this.button1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(118, 36);
            this.button1.TabIndex = 2;
            this.button1.Text = "Load";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // cbbpath
            // 
            this.cbbpath.FormattingEnabled = true;
            this.cbbpath.Location = new System.Drawing.Point(294, 35);
            this.cbbpath.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cbbpath.Name = "cbbpath";
            this.cbbpath.Size = new System.Drawing.Size(625, 33);
            this.cbbpath.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(119, 39);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(138, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "Choose File Log";
            // 
            // StopStartService
            // 
            this.StopStartService.Controls.Add(this.label4);
            this.StopStartService.Controls.Add(this.lblstatus);
            this.StopStartService.Controls.Add(this.btnprocess);
            this.StopStartService.Controls.Add(this.label3);
            this.StopStartService.Controls.Add(this.label2);
            this.StopStartService.Controls.Add(this.richTextBox2);
            this.StopStartService.Location = new System.Drawing.Point(4, 34);
            this.StopStartService.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.StopStartService.Name = "StopStartService";
            this.StopStartService.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.StopStartService.Size = new System.Drawing.Size(1540, 826);
            this.StopStartService.TabIndex = 1;
            this.StopStartService.Text = "Stop/Start Service";
            this.StopStartService.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(145, 31);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(0, 25);
            this.label4.TabIndex = 9;
            // 
            // lblstatus
            // 
            this.lblstatus.AutoSize = true;
            this.lblstatus.Location = new System.Drawing.Point(159, 71);
            this.lblstatus.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblstatus.Name = "lblstatus";
            this.lblstatus.Size = new System.Drawing.Size(0, 25);
            this.lblstatus.TabIndex = 8;
            // 
            // btnprocess
            // 
            this.btnprocess.Location = new System.Drawing.Point(66, 121);
            this.btnprocess.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnprocess.Name = "btnprocess";
            this.btnprocess.Size = new System.Drawing.Size(176, 36);
            this.btnprocess.TabIndex = 7;
            this.btnprocess.Text = "...";
            this.btnprocess.UseVisualStyleBackColor = true;
            this.btnprocess.Click += new System.EventHandler(this.btnprocess_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(66, 71);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(69, 25);
            this.label3.TabIndex = 6;
            this.label3.Text = "Status :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(66, 31);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(81, 25);
            this.label2.TabIndex = 5;
            this.label2.Text = "Service : ";
            // 
            // richTextBox2
            // 
            this.richTextBox2.Location = new System.Drawing.Point(42, 182);
            this.richTextBox2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.richTextBox2.Name = "richTextBox2";
            this.richTextBox2.Size = new System.Drawing.Size(1453, 613);
            this.richTextBox2.TabIndex = 4;
            this.richTextBox2.Text = "";
            // 
            // Monitor_XMLGenerator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1578, 894);
            this.Controls.Add(this.tabControl1);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "Monitor_XMLGenerator";
            this.Text = "Monitor_XMLGenerator";
            this.tabControl1.ResumeLayout(false);
            this.ReadLogFile.ResumeLayout(false);
            this.ReadLogFile.PerformLayout();
            this.StopStartService.ResumeLayout(false);
            this.StopStartService.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private TabControl tabControl1;
        private TabPage ReadLogFile;
        private TabPage StopStartService;
        private RichTextBox richTextBox1;
        private Button button1;
        private ComboBox cbbpath;
        private Label label1;
        private Button btnprocess;
        private Label label3;
        private Label label2;
        private RichTextBox richTextBox2;
        private Label lblstatus;
        private Label label4;
    }
}