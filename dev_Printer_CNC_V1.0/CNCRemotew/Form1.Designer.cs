namespace CNCRemotew
{
    partial class BarcodePrinter
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.label6 = new System.Windows.Forms.Label();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.button3 = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.printer1 = new System.Windows.Forms.TextBox();
            this.printer2 = new System.Windows.Forms.TextBox();
            this.printer3 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.CNC1 = new System.Windows.Forms.TextBox();
            this.CNC2 = new System.Windows.Forms.TextBox();
            this.CNC3 = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton3 = new System.Windows.Forms.RadioButton();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(0, 0);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 18;
            this.button1.UseWaitCursor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(0, 0);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 17;
            this.button2.UseWaitCursor = true;
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1,
            this.toolStripButton2});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(495, 25);
            this.toolStrip1.TabIndex = 9;
            this.toolStrip1.Text = "toolStrip1";
            this.toolStrip1.UseWaitCursor = true;
            this.toolStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.toolStrip1_ItemClicked);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(60, 22);
            this.toolStripButton1.Text = "工位选择";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(72, 22);
            this.toolStripButton2.Text = "数据库设置";
            this.toolStripButton2.Click += new System.EventHandler(this.toolStripButton2_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(25, 38);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(47, 12);
            this.label6.TabIndex = 12;
            this.label6.Text = "IP 地址";
            this.label6.UseWaitCursor = true;
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(128, 31);
            this.textBox3.Margin = new System.Windows.Forms.Padding(4);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(199, 21);
            this.textBox3.TabIndex = 11;
            this.textBox3.UseWaitCursor = true;
            this.textBox3.TextChanged += new System.EventHandler(this.textBox3_TextChanged);
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Location = new System.Drawing.Point(13, 196);
            this.radioButton1.Margin = new System.Windows.Forms.Padding(4);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(71, 16);
            this.radioButton1.TabIndex = 13;
            this.radioButton1.Text = "加工中心";
            this.radioButton1.UseVisualStyleBackColor = true;
            this.radioButton1.UseWaitCursor = true;
            this.radioButton1.Visible = false;
            this.radioButton1.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(359, 31);
            this.button3.Margin = new System.Windows.Forms.Padding(4);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(100, 26);
            this.button3.TabIndex = 15;
            this.button3.Text = "监听";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.UseWaitCursor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(376, 224);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(113, 12);
            this.label7.TabIndex = 16;
            this.label7.Text = "Version: 20181026A";
            this.label7.UseWaitCursor = true;
            this.label7.Click += new System.EventHandler(this.label7_Click);
            // 
            // printer1
            // 
            this.printer1.Location = new System.Drawing.Point(43, 102);
            this.printer1.Name = "printer1";
            this.printer1.Size = new System.Drawing.Size(211, 21);
            this.printer1.TabIndex = 19;
            this.printer1.UseWaitCursor = true;
            // 
            // printer2
            // 
            this.printer2.Location = new System.Drawing.Point(43, 133);
            this.printer2.Name = "printer2";
            this.printer2.Size = new System.Drawing.Size(211, 21);
            this.printer2.TabIndex = 20;
            this.printer2.UseWaitCursor = true;
            // 
            // printer3
            // 
            this.printer3.Location = new System.Drawing.Point(43, 164);
            this.printer3.Name = "printer3";
            this.printer3.Size = new System.Drawing.Size(211, 21);
            this.printer3.TabIndex = 21;
            this.printer3.UseWaitCursor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 105);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(23, 12);
            this.label1.TabIndex = 22;
            this.label1.Text = "1：";
            this.label1.UseWaitCursor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 136);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(23, 12);
            this.label2.TabIndex = 23;
            this.label2.Text = "2：";
            this.label2.UseWaitCursor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 167);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(23, 12);
            this.label3.TabIndex = 24;
            this.label3.Text = "3：";
            this.label3.UseWaitCursor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(43, 81);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 25;
            this.label4.Text = "打印机名称";
            this.label4.UseWaitCursor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(269, 82);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 12);
            this.label5.TabIndex = 26;
            this.label5.Text = "监控机台号";
            this.label5.UseWaitCursor = true;
            // 
            // CNC1
            // 
            this.CNC1.Location = new System.Drawing.Point(272, 100);
            this.CNC1.Name = "CNC1";
            this.CNC1.Size = new System.Drawing.Size(100, 21);
            this.CNC1.TabIndex = 27;
            this.CNC1.UseWaitCursor = true;
            this.CNC1.TextChanged += new System.EventHandler(this.CNC1_TextChanged);
            // 
            // CNC2
            // 
            this.CNC2.Location = new System.Drawing.Point(272, 133);
            this.CNC2.Name = "CNC2";
            this.CNC2.Size = new System.Drawing.Size(100, 21);
            this.CNC2.TabIndex = 28;
            this.CNC2.UseWaitCursor = true;
            // 
            // CNC3
            // 
            this.CNC3.Location = new System.Drawing.Point(272, 164);
            this.CNC3.Name = "CNC3";
            this.CNC3.Size = new System.Drawing.Size(100, 21);
            this.CNC3.TabIndex = 29;
            this.CNC3.UseWaitCursor = true;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(128, 64);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(173, 12);
            this.label8.TabIndex = 30;
            this.label8.Text = "打印机设置区，仅加工中心有效";
            this.label8.UseWaitCursor = true;
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Checked = true;
            this.radioButton2.Location = new System.Drawing.Point(92, 196);
            this.radioButton2.Margin = new System.Windows.Forms.Padding(4);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(71, 16);
            this.radioButton2.TabIndex = 31;
            this.radioButton2.TabStop = true;
            this.radioButton2.Text = "补打工位";
            this.radioButton2.UseVisualStyleBackColor = true;
            this.radioButton2.UseWaitCursor = true;
            this.radioButton2.Visible = false;
            this.radioButton2.CheckedChanged += new System.EventHandler(this.radioButton2_CheckedChanged_1);
            // 
            // radioButton3
            // 
            this.radioButton3.AutoSize = true;
            this.radioButton3.Location = new System.Drawing.Point(171, 196);
            this.radioButton3.Margin = new System.Windows.Forms.Padding(4);
            this.radioButton3.Name = "radioButton3";
            this.radioButton3.Size = new System.Drawing.Size(95, 16);
            this.radioButton3.TabIndex = 32;
            this.radioButton3.Text = "质检补打工位";
            this.radioButton3.UseVisualStyleBackColor = true;
            this.radioButton3.UseWaitCursor = true;
            this.radioButton3.Visible = false;
            // 
            // BarcodePrinter
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(495, 245);
            this.Controls.Add(this.radioButton3);
            this.Controls.Add(this.radioButton2);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.CNC3);
            this.Controls.Add(this.CNC2);
            this.Controls.Add(this.CNC1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.printer3);
            this.Controls.Add(this.printer2);
            this.Controls.Add(this.printer1);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.radioButton1);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "BarcodePrinter";
            this.Text = "条码打印程序";
            this.UseWaitCursor = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.BarcodePrinter_FormClosed);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox printer1;
        private System.Windows.Forms.TextBox printer2;
        private System.Windows.Forms.TextBox printer3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox CNC1;
        private System.Windows.Forms.TextBox CNC2;
        private System.Windows.Forms.TextBox CNC3;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.RadioButton radioButton3;
    }
}

