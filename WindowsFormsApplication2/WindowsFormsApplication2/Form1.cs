/**
 * Teague Forren
 * Johanna Jan
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
using System.IO;

using GeminiCore;

namespace WindowsFormsApplication2
{
    public partial class Form1 : Form
    {
        public CPU myCPU;
        public Memory mainMemory;

        public Form1()
        {
            myCPU = new CPU();

            mainMemory = new Memory();

            InitializeComponent();
            #region GUI pretty stuff. Making transparencies
            //Making the Acc labels transparent with the picturebox background
            //Label1 = "ACC:"
            var pos = this.PointToScreen(label1.Location);
            pos = BackgroundPicBox.PointToClient(pos);
            label1.Parent = BackgroundPicBox;
            label1.Location = pos;
            label1.BackColor = Color.Transparent;
            //AccLabel
            var pos2 = this.PointToScreen(accLabel.Location);
            pos2 = BackgroundPicBox.PointToClient(pos2);
            accLabel.Parent = BackgroundPicBox;
            accLabel.Location = pos2;
            accLabel.BackColor = Color.Transparent;
            //Loadlabel
            var pos3 = this.PointToScreen(LoadLabel.Location);
            pos3 = BackgroundPicBox.PointToClient(pos3);
            LoadLabel.Parent = BackgroundPicBox;
            LoadLabel.Location = pos3;
            LoadLabel.BackColor = Color.Transparent;
            //hitormissLabel
            var pos4 = this.PointToScreen(MissOrHitLabel.Location);
            pos4 = BackgroundPicBox.PointToClient(pos4);
            MissOrHitLabel.Parent = BackgroundPicBox;
            MissOrHitLabel.Location = pos4;
            MissOrHitLabel.BackColor = Color.Transparent;
#endregion

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
                        List<string> binary = ipe.ParseFile();
                        List<Int16> finBinary = convert(binary);
                        //ipe.createBinaryTextFile(binary);
                        createBinaryFile(finBinary);
                        myCPU.setBinary(binary);
                        myCPU.setBinary16(finBinary);
                        myCPU.setLabelLocationMap(ipe.getLabelLocationMap());
                        mainMemory.setLabelLocationMap(ipe.getLabelLocationMap());
                        myCPU.getMainMemory().InitializeCache();

                        foreach( KeyValuePair<string, int> kvp in ipe.getLabelLocationMap() )
                        {
                            //Console.WriteLine("Key = {0}, Value = {1}", kvp.Key, kvp.Value);
                        }
 //                       for (int i = 0; i < ipe.getLabelLocationMap().Count; i++)
   //                     {
     //                       Console.WriteLine("map key: " + ipe.getLabelLocationMap().Keys[i])
       //                 }
         //                   Console.WriteLine("map.keys: " + ipe.getLabelLocationMap());
                        //Console.WriteLine("map.value: " + ipe.getLabelLocationMap().Values);
                        myCPU.PC = 0;
                        myCPU.finished = false;
                        ipe.broken = false;
                    }
                    catch (Exception err)
                    {
                       System.Windows.Forms.MessageBox.Show(err.Message);
                    }
                }
            }
        }

        public Memory getMainMemory()
        {
            return mainMemory;
        }

        public double convertToBase10(string x)
        {
            double num = 0;
            int exp = x.Length - 1;
            //Console.WriteLine("x: " + x);
            for (int i = 0; i < x.Length; i++)
            {
                if (x[i] == '1')
                {
                    //Console.WriteLine("x[" + i + "] = " + x[i]);
                    num = num + Math.Pow(2, exp);
                 //   Console.WriteLine("exp: " + exp);
                   // Console.WriteLine("num: " + num);
                }
                exp--;
            }
            return num;
        }

        public List<Int16> convert(List<string> list) // Stuff breaks here. Int16 is maxed at 32,767 so our 16 digit binary number does not convert.
        {
            List<Int16> final = new List<Int16>();
            Int16 num = 0;
            for (int i = 0; i < list.Count; i++)
            {
                num = (short)convertToBase10(list[i]);

                final.Add(Convert.ToInt16(num));
            }
            return final;
        }

        public void createBinaryFile(List<Int16> binary)
        {
           // Console.WriteLine(binary.ToString());
            SaveFileDialog save = new SaveFileDialog();
            save.FileName = "Binary.out";
            save.Filter = "Binary File Format | *.out";

            if (save.ShowDialog() == DialogResult.OK)
            { 
                BinaryWriter writer = new BinaryWriter(save.OpenFile());
                foreach (int i in binary)
                {
                    writer.Write(i);
                    //Console.WriteLine(binary[i].ToString());
                }
                writer.Dispose();
                writer.Close();
            }

        }
        #endregion

        private void nextInstructionButton_Click(object sender, EventArgs e)
        {
            if (!myCPU.finished)
            {
                this.myCPU.nextInstruction();
                this.setCPUValuesToView();
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("Finished Code");
                myCPU.resetFields();
                this.setCPUValuesToView();
            }
        }

        public void setCPUValuesToView()
        {
            this.accLabel.Text = this.myCPU.ACC.ToString();
            this.ALabel.Text = this.myCPU.A;
            this.BLabel.Text = this.myCPU.B;
            this.PCLabel.Text = this.myCPU.PC.ToString();
            this.MARLabel.Text = this.myCPU.MAR;
            this.MDRLabel.Text = this.myCPU.MDR;
            this.TEMPLabel.Text = this.myCPU.TEMP;
            this.IRLabel.Text = this.myCPU.IR;
            this.CCLabel.Text = this.myCPU.CC;
            this.nextInstructionDisplayLabel.Text = this.myCPU.nextInst;
            this.HitCountLabel.Text = Convert.ToString(this.myCPU.getMainMemory().HITCOUNT);
            this.MissCountLabel.Text = Convert.ToString(this.myCPU.getMainMemory().MISSCOUNT);
            this.MissOrHitLabel.Text = myCPU.getMainMemory().HitorMiss;
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

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel1_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {


        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void accLabel_Click(object sender, EventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void label15_Click(object sender, EventArgs e)
        {

        }

        private void label12_Click_1(object sender, EventArgs e)
        {

        }

        private void nextInstructionDisplayLabel_Click(object sender, EventArgs e)
        {

        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void runAllButton_Click(object sender, EventArgs e)
        {
            while (!myCPU.finished)
            {
                //myCPU.runAll();
                myCPU.nextInstruction();
                this.setCPUValuesToView();
            }
            System.Windows.Forms.MessageBox.Show("Finished Code");
            myCPU.resetFields();
            this.setCPUValuesToView();
        }

        private void label14_Click(object sender, EventArgs e)
        {

        }

        private void label17_Click(object sender, EventArgs e)
        {

        }
    }
}
