namespace StatsVidSync
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.TextBox_fcpXml = new System.Windows.Forms.TextBox();
            this.Button_browseFcpXml = new System.Windows.Forms.Button();
            this.groupBox_finalCutXml = new System.Windows.Forms.GroupBox();
            this.groupBox_soloStatsXml = new System.Windows.Forms.GroupBox();
            this.textBox_soloStatsXml = new System.Windows.Forms.TextBox();
            this.btn_browseSoloStats = new System.Windows.Forms.Button();
            this.groupBox_results = new System.Windows.Forms.GroupBox();
            this.textBox_results = new System.Windows.Forms.TextBox();
            this.button_processXml = new System.Windows.Forms.Button();
            this.groupBox_Duration = new System.Windows.Forms.GroupBox();
            this.label_durationInfo = new System.Windows.Forms.Label();
            this.comboBox_duration = new System.Windows.Forms.ComboBox();
            this.groupBox_finalCutXml.SuspendLayout();
            this.groupBox_soloStatsXml.SuspendLayout();
            this.groupBox_results.SuspendLayout();
            this.groupBox_Duration.SuspendLayout();
            this.SuspendLayout();
            // 
            // TextBox_fcpXml
            // 
            this.TextBox_fcpXml.Location = new System.Drawing.Point(125, 25);
            this.TextBox_fcpXml.Name = "TextBox_fcpXml";
            this.TextBox_fcpXml.Size = new System.Drawing.Size(296, 20);
            this.TextBox_fcpXml.TabIndex = 0;
            this.TextBox_fcpXml.Text = "Select the FCP xml file for this match / set";
            this.TextBox_fcpXml.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // Button_browseFcpXml
            // 
            this.Button_browseFcpXml.Location = new System.Drawing.Point(15, 25);
            this.Button_browseFcpXml.Name = "Button_browseFcpXml";
            this.Button_browseFcpXml.Size = new System.Drawing.Size(104, 20);
            this.Button_browseFcpXml.TabIndex = 2;
            this.Button_browseFcpXml.Text = "Browse";
            this.Button_browseFcpXml.UseVisualStyleBackColor = true;
            this.Button_browseFcpXml.Click += new System.EventHandler(this.Button_browseFcpXml_Click);
            // 
            // groupBox_finalCutXml
            // 
            this.groupBox_finalCutXml.Controls.Add(this.TextBox_fcpXml);
            this.groupBox_finalCutXml.Controls.Add(this.Button_browseFcpXml);
            this.groupBox_finalCutXml.Location = new System.Drawing.Point(23, 23);
            this.groupBox_finalCutXml.Name = "groupBox_finalCutXml";
            this.groupBox_finalCutXml.Size = new System.Drawing.Size(439, 60);
            this.groupBox_finalCutXml.TabIndex = 3;
            this.groupBox_finalCutXml.TabStop = false;
            this.groupBox_finalCutXml.Text = "Final Cut XML";
            this.groupBox_finalCutXml.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // groupBox_soloStatsXml
            // 
            this.groupBox_soloStatsXml.Controls.Add(this.textBox_soloStatsXml);
            this.groupBox_soloStatsXml.Controls.Add(this.btn_browseSoloStats);
            this.groupBox_soloStatsXml.Location = new System.Drawing.Point(23, 89);
            this.groupBox_soloStatsXml.Name = "groupBox_soloStatsXml";
            this.groupBox_soloStatsXml.Size = new System.Drawing.Size(439, 60);
            this.groupBox_soloStatsXml.TabIndex = 4;
            this.groupBox_soloStatsXml.TabStop = false;
            this.groupBox_soloStatsXml.Text = "SoloStats XML";
            // 
            // textBox_soloStatsXml
            // 
            this.textBox_soloStatsXml.Location = new System.Drawing.Point(125, 25);
            this.textBox_soloStatsXml.Name = "textBox_soloStatsXml";
            this.textBox_soloStatsXml.Size = new System.Drawing.Size(296, 20);
            this.textBox_soloStatsXml.TabIndex = 0;
            this.textBox_soloStatsXml.Text = "Select the SoloStats xml file for this match / set";
            // 
            // btn_browseSoloStats
            // 
            this.btn_browseSoloStats.Location = new System.Drawing.Point(15, 25);
            this.btn_browseSoloStats.Name = "btn_browseSoloStats";
            this.btn_browseSoloStats.Size = new System.Drawing.Size(104, 20);
            this.btn_browseSoloStats.TabIndex = 2;
            this.btn_browseSoloStats.Text = "Browse";
            this.btn_browseSoloStats.UseVisualStyleBackColor = true;
            this.btn_browseSoloStats.Click += new System.EventHandler(this.btn_browseSoloStats_Click);
            // 
            // groupBox_results
            // 
            this.groupBox_results.Controls.Add(this.textBox_results);
            this.groupBox_results.Location = new System.Drawing.Point(23, 172);
            this.groupBox_results.Name = "groupBox_results";
            this.groupBox_results.Size = new System.Drawing.Size(720, 388);
            this.groupBox_results.TabIndex = 5;
            this.groupBox_results.TabStop = false;
            this.groupBox_results.Text = "Results";
            this.groupBox_results.Enter += new System.EventHandler(this.groupBox3_Enter);
            // 
            // textBox_results
            // 
            this.textBox_results.Location = new System.Drawing.Point(15, 25);
            this.textBox_results.Multiline = true;
            this.textBox_results.Name = "textBox_results";
            this.textBox_results.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox_results.Size = new System.Drawing.Size(685, 344);
            this.textBox_results.TabIndex = 0;
            // 
            // button_processXml
            // 
            this.button_processXml.Location = new System.Drawing.Point(551, 111);
            this.button_processXml.Name = "button_processXml";
            this.button_processXml.Size = new System.Drawing.Size(75, 23);
            this.button_processXml.TabIndex = 6;
            this.button_processXml.Text = "Process";
            this.button_processXml.UseVisualStyleBackColor = true;
            this.button_processXml.Click += new System.EventHandler(this.Button_processXml_Click);
            // 
            // groupBox_Duration
            // 
            this.groupBox_Duration.Controls.Add(this.comboBox_duration);
            this.groupBox_Duration.Controls.Add(this.label_durationInfo);
            this.groupBox_Duration.Location = new System.Drawing.Point(486, 23);
            this.groupBox_Duration.Name = "groupBox_Duration";
            this.groupBox_Duration.Size = new System.Drawing.Size(257, 60);
            this.groupBox_Duration.TabIndex = 4;
            this.groupBox_Duration.TabStop = false;
            this.groupBox_Duration.Text = "Clip Duration";
            this.groupBox_Duration.Enter += new System.EventHandler(this.groupBox1_Enter_1);
            // 
            // label_durationInfo
            // 
            this.label_durationInfo.AutoSize = true;
            this.label_durationInfo.Location = new System.Drawing.Point(81, 28);
            this.label_durationInfo.Name = "label_durationInfo";
            this.label_durationInfo.Size = new System.Drawing.Size(160, 13);
            this.label_durationInfo.TabIndex = 1;
            this.label_durationInfo.Text = "Choose clip duration in seconds.\r\n";
            this.label_durationInfo.Click += new System.EventHandler(this.label1_Click);
            // 
            // comboBox_duration
            // 
            this.comboBox_duration.FormattingEnabled = true;
            this.comboBox_duration.Items.AddRange(new object[] {
            "7",
            "8",
            "9",
            "10",
            "11",
            "12"});
            this.comboBox_duration.Location = new System.Drawing.Point(6, 25);
            this.comboBox_duration.Name = "comboBox_duration";
            this.comboBox_duration.Size = new System.Drawing.Size(63, 21);
            this.comboBox_duration.TabIndex = 7;
            this.comboBox_duration.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(755, 572);
            this.Controls.Add(this.groupBox_Duration);
            this.Controls.Add(this.button_processXml);
            this.Controls.Add(this.groupBox_results);
            this.Controls.Add(this.groupBox_soloStatsXml);
            this.Controls.Add(this.groupBox_finalCutXml);
            this.Name = "Form1";
            this.Text = "Form1";
            this.groupBox_finalCutXml.ResumeLayout(false);
            this.groupBox_finalCutXml.PerformLayout();
            this.groupBox_soloStatsXml.ResumeLayout(false);
            this.groupBox_soloStatsXml.PerformLayout();
            this.groupBox_results.ResumeLayout(false);
            this.groupBox_results.PerformLayout();
            this.groupBox_Duration.ResumeLayout(false);
            this.groupBox_Duration.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox TextBox_fcpXml;
        private System.Windows.Forms.Button Button_browseFcpXml;
        private System.Windows.Forms.GroupBox groupBox_finalCutXml;
        private System.Windows.Forms.GroupBox groupBox_soloStatsXml;
        private System.Windows.Forms.TextBox textBox_soloStatsXml;
        private System.Windows.Forms.Button btn_browseSoloStats;
        private System.Windows.Forms.GroupBox groupBox_results;
        private System.Windows.Forms.TextBox textBox_results;
        private System.Windows.Forms.Button button_processXml;
        private System.Windows.Forms.GroupBox groupBox_Duration;
        private System.Windows.Forms.Label label_durationInfo;
        private System.Windows.Forms.ComboBox comboBox_duration;
    }
}

