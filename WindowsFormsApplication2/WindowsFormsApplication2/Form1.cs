/**
 * Seth Morecraft
 */
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using GeminiCore;

namespace WindowsFormsApplication2
{
    public partial class Form1 : Form
    {
        public CPU myCPU;

        public Form1()
        {
            myCPU = new CPU();

            InitializeComponent();

#if DEBUG
            loadFileButton.Text = "Load";
#endif
        }

        #region Events
        private void loadFileButton_Click(object sender, EventArgs e)
        {
            using (var ofd = new OpenFileDialog())
            {
                if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    try
                    {
                        var ipe = new IPE(ofd.FileName);
                        ipe.ParseFile();
                    }
                    catch (Exception err)
                    {
                        // show a dialog with error
                    }
                }
            }
        }
        #endregion

        private void nextInstructionButton_Click(object sender, EventArgs e)
        {
            this.myCPU.nextInstruction();
            this.setCPUValuesToView();
        }

        public void setCPUValuesToView()
        {
            this.accLabel.Text = this.myCPU.ACC.ToString();
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {
                    }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click_1(object sender, EventArgs e)
        {

        }

        private void label2_Click_2(object sender, EventArgs e)
        {

        }

        private void label3_Click_1(object sender, EventArgs e)
        {

        }
    }
}
