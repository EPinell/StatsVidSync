using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static StatsVidSync.StatSyncBrain;

namespace StatsVidSync
{
    public partial class Form1 : Form
    {
        public OpenFileDialog Ofd { get; } = new OpenFileDialog();
        public Form1()
        {
            InitializeComponent();
            comboBox_duration.Text = "10";
            KeywordPlaybackDuration = "10s";
        }
        

        

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Button_browseFcpXml_Click(object sender, EventArgs e)
        {
            Ofd.Filter = ("XML|*.xml");
            if (Ofd.ShowDialog() == DialogResult.OK)
            {
                TextBox_fcpXml.Text = Ofd.FileName;
                FcpXmlFilePath = Ofd.FileName;
            }
        }

        private void Button_processXml_Click(object sender, EventArgs e)
        {
            if (FcpXmlFilePath != null && SoloStatsXmlFilePath != null)
            {
                SetKeywordDurationValues(comboBox_duration.Text);

                StatSyncBrain.ProcessXmlFiles(FcpXmlFilePath, SoloStatsXmlFilePath);
                textBox_results.Text = FinishedXml;
            }
        }


        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void btn_browseSoloStats_Click(object sender, EventArgs e)
        {

            Ofd.Filter = ("XML|*.xml");
            if (Ofd.ShowDialog() == DialogResult.OK)
            {
                textBox_soloStatsXml.Text = Ofd.FileName;
                SoloStatsXmlFilePath = Ofd.FileName;
            }
        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter_1(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
