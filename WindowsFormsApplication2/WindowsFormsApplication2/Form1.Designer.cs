namespace WindowsFormsApplication2
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.accLabel = new System.Windows.Forms.Label();
            this.loadFileButton = new System.Windows.Forms.Button();
            this.nextInstructionButton = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.CCLabel = new System.Windows.Forms.Label();
            this.IRLabel = new System.Windows.Forms.Label();
            this.TEMPLabel = new System.Windows.Forms.Label();
            this.MDRLabel = new System.Windows.Forms.Label();
            this.MARLabel = new System.Windows.Forms.Label();
            this.PCLabel = new System.Windows.Forms.Label();
            this.OneLabel = new System.Windows.Forms.Label();
            this.ZeroLabel = new System.Windows.Forms.Label();
            this.BLabel = new System.Windows.Forms.Label();
            this.ALabel = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.registerLabel = new System.Windows.Forms.Label();
            this.valueLabel = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.BackgroundPicBox = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.runAllButton = new System.Windows.Forms.Button();
            this.nextInstructionTable = new System.Windows.Forms.TableLayoutPanel();
            this.nextInstructionDisplayLabel = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.LoadLabel = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.BackgroundPicBox)).BeginInit();
            this.nextInstructionTable.SuspendLayout();
            this.SuspendLayout();
            // 
            // accLabel
            // 
            this.accLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold);
            this.accLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.accLabel.Location = new System.Drawing.Point(78, 21);
            this.accLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.accLabel.Name = "accLabel";
            this.accLabel.Size = new System.Drawing.Size(58, 25);
            this.accLabel.TabIndex = 1;
            this.accLabel.Text = "0";
            this.accLabel.Click += new System.EventHandler(this.accLabel_Click);
            // 
            // loadFileButton
            // 
            this.loadFileButton.Location = new System.Drawing.Point(473, 48);
            this.loadFileButton.Margin = new System.Windows.Forms.Padding(2);
            this.loadFileButton.Name = "loadFileButton";
            this.loadFileButton.Size = new System.Drawing.Size(56, 20);
            this.loadFileButton.TabIndex = 2;
            this.loadFileButton.Text = "File";
            this.loadFileButton.UseVisualStyleBackColor = true;
            this.loadFileButton.Click += new System.EventHandler(this.loadFileButton_Click);
            // 
            // nextInstructionButton
            // 
            this.nextInstructionButton.Location = new System.Drawing.Point(473, 242);
            this.nextInstructionButton.Margin = new System.Windows.Forms.Padding(2);
            this.nextInstructionButton.Name = "nextInstructionButton";
            this.nextInstructionButton.Size = new System.Drawing.Size(56, 19);
            this.nextInstructionButton.TabIndex = 3;
            this.nextInstructionButton.Text = "Next";
            this.nextInstructionButton.UseVisualStyleBackColor = true;
            this.nextInstructionButton.Click += new System.EventHandler(this.nextInstructionButton_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tableLayoutPanel1.BackgroundImage")));
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 43.97906F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 56.02094F));
            this.tableLayoutPanel1.Controls.Add(this.CCLabel, 1, 10);
            this.tableLayoutPanel1.Controls.Add(this.IRLabel, 1, 9);
            this.tableLayoutPanel1.Controls.Add(this.TEMPLabel, 1, 8);
            this.tableLayoutPanel1.Controls.Add(this.MDRLabel, 1, 7);
            this.tableLayoutPanel1.Controls.Add(this.MARLabel, 1, 6);
            this.tableLayoutPanel1.Controls.Add(this.PCLabel, 1, 5);
            this.tableLayoutPanel1.Controls.Add(this.OneLabel, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.ZeroLabel, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.BLabel, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.ALabel, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.registerLabel, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.valueLabel, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.label4, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.label5, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.label6, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.label7, 0, 6);
            this.tableLayoutPanel1.Controls.Add(this.label8, 0, 7);
            this.tableLayoutPanel1.Controls.Add(this.label9, 0, 8);
            this.tableLayoutPanel1.Controls.Add(this.label10, 0, 9);
            this.tableLayoutPanel1.Controls.Add(this.label11, 0, 10);
            this.tableLayoutPanel1.ForeColor = System.Drawing.SystemColors.AppWorkspace;
            this.tableLayoutPanel1.GrowStyle = System.Windows.Forms.TableLayoutPanelGrowStyle.FixedSize;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(238, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 12;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 23F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 23F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 23F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 33F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 16F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(208, 284);
            this.tableLayoutPanel1.TabIndex = 4;
            this.tableLayoutPanel1.Paint += new System.Windows.Forms.PaintEventHandler(this.tableLayoutPanel1_Paint_1);
            // 
            // CCLabel
            // 
            this.CCLabel.AutoSize = true;
            this.CCLabel.BackColor = System.Drawing.Color.Transparent;
            this.CCLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.CCLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.CCLabel.Location = new System.Drawing.Point(94, 235);
            this.CCLabel.Name = "CCLabel";
            this.CCLabel.Size = new System.Drawing.Size(98, 20);
            this.CCLabel.TabIndex = 23;
            this.CCLabel.Text = "0x00000000";
            this.CCLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // IRLabel
            // 
            this.IRLabel.AutoSize = true;
            this.IRLabel.BackColor = System.Drawing.Color.Transparent;
            this.IRLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.IRLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.IRLabel.Location = new System.Drawing.Point(94, 210);
            this.IRLabel.Name = "IRLabel";
            this.IRLabel.Size = new System.Drawing.Size(103, 25);
            this.IRLabel.TabIndex = 22;
            this.IRLabel.Text = "- - - - - - - - - -";
            this.IRLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // TEMPLabel
            // 
            this.TEMPLabel.AutoSize = true;
            this.TEMPLabel.BackColor = System.Drawing.Color.Transparent;
            this.TEMPLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.TEMPLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.TEMPLabel.Location = new System.Drawing.Point(94, 186);
            this.TEMPLabel.Name = "TEMPLabel";
            this.TEMPLabel.Size = new System.Drawing.Size(98, 20);
            this.TEMPLabel.TabIndex = 21;
            this.TEMPLabel.Text = "0x00000000";
            this.TEMPLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // MDRLabel
            // 
            this.MDRLabel.AutoSize = true;
            this.MDRLabel.BackColor = System.Drawing.Color.Transparent;
            this.MDRLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.MDRLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.MDRLabel.Location = new System.Drawing.Point(94, 163);
            this.MDRLabel.Name = "MDRLabel";
            this.MDRLabel.Size = new System.Drawing.Size(98, 20);
            this.MDRLabel.TabIndex = 20;
            this.MDRLabel.Text = "0x00000000";
            this.MDRLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // MARLabel
            // 
            this.MARLabel.AutoSize = true;
            this.MARLabel.BackColor = System.Drawing.Color.Transparent;
            this.MARLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.MARLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.MARLabel.Location = new System.Drawing.Point(94, 140);
            this.MARLabel.Name = "MARLabel";
            this.MARLabel.Size = new System.Drawing.Size(98, 20);
            this.MARLabel.TabIndex = 19;
            this.MARLabel.Text = "0x00000000";
            this.MARLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // PCLabel
            // 
            this.PCLabel.AutoSize = true;
            this.PCLabel.BackColor = System.Drawing.Color.Transparent;
            this.PCLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.PCLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.PCLabel.Location = new System.Drawing.Point(94, 117);
            this.PCLabel.Name = "PCLabel";
            this.PCLabel.Size = new System.Drawing.Size(98, 20);
            this.PCLabel.TabIndex = 18;
            this.PCLabel.Text = "0x00000000";
            this.PCLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // OneLabel
            // 
            this.OneLabel.AutoSize = true;
            this.OneLabel.BackColor = System.Drawing.Color.Transparent;
            this.OneLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.OneLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.OneLabel.Location = new System.Drawing.Point(94, 93);
            this.OneLabel.Name = "OneLabel";
            this.OneLabel.Size = new System.Drawing.Size(98, 20);
            this.OneLabel.TabIndex = 17;
            this.OneLabel.Text = "0x00000001";
            this.OneLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.OneLabel.Click += new System.EventHandler(this.label15_Click);
            // 
            // ZeroLabel
            // 
            this.ZeroLabel.AutoSize = true;
            this.ZeroLabel.BackColor = System.Drawing.Color.Transparent;
            this.ZeroLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.ZeroLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.ZeroLabel.Location = new System.Drawing.Point(94, 68);
            this.ZeroLabel.Name = "ZeroLabel";
            this.ZeroLabel.Size = new System.Drawing.Size(98, 20);
            this.ZeroLabel.TabIndex = 16;
            this.ZeroLabel.Text = "0x00000000";
            this.ZeroLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // BLabel
            // 
            this.BLabel.AutoSize = true;
            this.BLabel.BackColor = System.Drawing.Color.Transparent;
            this.BLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.BLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.BLabel.Location = new System.Drawing.Point(94, 44);
            this.BLabel.Name = "BLabel";
            this.BLabel.Size = new System.Drawing.Size(98, 20);
            this.BLabel.TabIndex = 15;
            this.BLabel.Text = "0x00000000";
            this.BLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // ALabel
            // 
            this.ALabel.AutoSize = true;
            this.ALabel.BackColor = System.Drawing.Color.Transparent;
            this.ALabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.ALabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.ALabel.Location = new System.Drawing.Point(94, 22);
            this.ALabel.Name = "ALabel";
            this.ALabel.Size = new System.Drawing.Size(98, 20);
            this.ALabel.TabIndex = 14;
            this.ALabel.Text = "0x00000000";
            this.ALabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.ALabel.Click += new System.EventHandler(this.label12_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.label3.ForeColor = System.Drawing.Color.Moccasin;
            this.label3.Location = new System.Drawing.Point(3, 44);
            this.label3.Name = "label3";
            this.label3.Padding = new System.Windows.Forms.Padding(9, 0, 0, 0);
            this.label3.Size = new System.Drawing.Size(30, 20);
            this.label3.TabIndex = 5;
            this.label3.Text = "B";
            this.label3.Click += new System.EventHandler(this.label3_Click_1);
            // 
            // registerLabel
            // 
            this.registerLabel.AutoSize = true;
            this.registerLabel.BackColor = System.Drawing.Color.Transparent;
            this.registerLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.registerLabel.ForeColor = System.Drawing.Color.Transparent;
            this.registerLabel.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.registerLabel.Location = new System.Drawing.Point(3, 0);
            this.registerLabel.Name = "registerLabel";
            this.registerLabel.Size = new System.Drawing.Size(77, 20);
            this.registerLabel.TabIndex = 0;
            this.registerLabel.Text = "Register";
            this.registerLabel.Click += new System.EventHandler(this.label2_Click_1);
            // 
            // valueLabel
            // 
            this.valueLabel.AutoSize = true;
            this.valueLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.valueLabel.ForeColor = System.Drawing.Color.Transparent;
            this.valueLabel.Location = new System.Drawing.Point(94, 0);
            this.valueLabel.Name = "valueLabel";
            this.valueLabel.Padding = new System.Windows.Forms.Padding(20, 0, 0, 0);
            this.valueLabel.Size = new System.Drawing.Size(75, 20);
            this.valueLabel.TabIndex = 1;
            this.valueLabel.Text = "Value";
            this.valueLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.valueLabel.Click += new System.EventHandler(this.label3_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.label2.ForeColor = System.Drawing.Color.Moccasin;
            this.label2.Location = new System.Drawing.Point(3, 22);
            this.label2.Name = "label2";
            this.label2.Padding = new System.Windows.Forms.Padding(9, 0, 0, 0);
            this.label2.Size = new System.Drawing.Size(29, 20);
            this.label2.TabIndex = 5;
            this.label2.Text = "A";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.label2.Click += new System.EventHandler(this.label2_Click_2);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.label4.ForeColor = System.Drawing.Color.Moccasin;
            this.label4.Location = new System.Drawing.Point(3, 68);
            this.label4.Name = "label4";
            this.label4.Padding = new System.Windows.Forms.Padding(9, 0, 0, 0);
            this.label4.Size = new System.Drawing.Size(51, 20);
            this.label4.TabIndex = 6;
            this.label4.Text = "Zero";
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.label5.ForeColor = System.Drawing.Color.Moccasin;
            this.label5.Location = new System.Drawing.Point(3, 93);
            this.label5.Name = "label5";
            this.label5.Padding = new System.Windows.Forms.Padding(9, 0, 0, 0);
            this.label5.Size = new System.Drawing.Size(49, 20);
            this.label5.TabIndex = 7;
            this.label5.Text = "One";
            this.label5.Click += new System.EventHandler(this.label5_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.label6.ForeColor = System.Drawing.Color.Moccasin;
            this.label6.Location = new System.Drawing.Point(3, 117);
            this.label6.Name = "label6";
            this.label6.Padding = new System.Windows.Forms.Padding(9, 0, 0, 0);
            this.label6.Size = new System.Drawing.Size(41, 20);
            this.label6.TabIndex = 8;
            this.label6.Text = "PC";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.label7.ForeColor = System.Drawing.Color.Moccasin;
            this.label7.Location = new System.Drawing.Point(3, 140);
            this.label7.Name = "label7";
            this.label7.Padding = new System.Windows.Forms.Padding(9, 0, 0, 0);
            this.label7.Size = new System.Drawing.Size(55, 20);
            this.label7.TabIndex = 9;
            this.label7.Text = "MAR";
            this.label7.Click += new System.EventHandler(this.label7_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.label8.ForeColor = System.Drawing.Color.Moccasin;
            this.label8.Location = new System.Drawing.Point(3, 163);
            this.label8.Name = "label8";
            this.label8.Padding = new System.Windows.Forms.Padding(9, 0, 0, 0);
            this.label8.Size = new System.Drawing.Size(57, 20);
            this.label8.TabIndex = 10;
            this.label8.Text = "MDR";
            this.label8.Click += new System.EventHandler(this.label8_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.label9.ForeColor = System.Drawing.Color.Moccasin;
            this.label9.Location = new System.Drawing.Point(3, 186);
            this.label9.Name = "label9";
            this.label9.Padding = new System.Windows.Forms.Padding(9, 0, 0, 0);
            this.label9.Size = new System.Drawing.Size(64, 20);
            this.label9.TabIndex = 11;
            this.label9.Text = "TEMP";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.label10.ForeColor = System.Drawing.Color.Moccasin;
            this.label10.Location = new System.Drawing.Point(3, 210);
            this.label10.Name = "label10";
            this.label10.Padding = new System.Windows.Forms.Padding(9, 0, 0, 0);
            this.label10.Size = new System.Drawing.Size(34, 20);
            this.label10.TabIndex = 12;
            this.label10.Text = "IR";
            this.label10.Click += new System.EventHandler(this.label10_Click);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.label11.ForeColor = System.Drawing.Color.Moccasin;
            this.label11.Location = new System.Drawing.Point(3, 235);
            this.label11.Name = "label11";
            this.label11.Padding = new System.Windows.Forms.Padding(9, 0, 0, 0);
            this.label11.Size = new System.Drawing.Size(42, 20);
            this.label11.TabIndex = 13;
            this.label11.Text = "CC";
            // 
            // BackgroundPicBox
            // 
            this.BackgroundPicBox.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.BackgroundPicBox.Image = ((System.Drawing.Image)(resources.GetObject("BackgroundPicBox.Image")));
            this.BackgroundPicBox.Location = new System.Drawing.Point(-192, -54);
            this.BackgroundPicBox.Name = "BackgroundPicBox";
            this.BackgroundPicBox.Size = new System.Drawing.Size(777, 390);
            this.BackgroundPicBox.TabIndex = 5;
            this.BackgroundPicBox.TabStop = false;
            this.BackgroundPicBox.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Verdana", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label1.Location = new System.Drawing.Point(4, 20);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "ACC:";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // runAllButton
            // 
            this.runAllButton.Location = new System.Drawing.Point(473, 265);
            this.runAllButton.Margin = new System.Windows.Forms.Padding(2);
            this.runAllButton.Name = "runAllButton";
            this.runAllButton.Size = new System.Drawing.Size(56, 19);
            this.runAllButton.TabIndex = 6;
            this.runAllButton.Text = "Run All";
            this.runAllButton.UseVisualStyleBackColor = true;
            // 
            // nextInstructionTable
            // 
            this.nextInstructionTable.BackColor = System.Drawing.Color.Transparent;
            this.nextInstructionTable.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("nextInstructionTable.BackgroundImage")));
            this.nextInstructionTable.ColumnCount = 1;
            this.nextInstructionTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.nextInstructionTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.nextInstructionTable.Controls.Add(this.nextInstructionDisplayLabel, 0, 1);
            this.nextInstructionTable.Controls.Add(this.label12, 0, 0);
            this.nextInstructionTable.Location = new System.Drawing.Point(6, 59);
            this.nextInstructionTable.Name = "nextInstructionTable";
            this.nextInstructionTable.RowCount = 2;
            this.nextInstructionTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.nextInstructionTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.nextInstructionTable.Size = new System.Drawing.Size(200, 66);
            this.nextInstructionTable.TabIndex = 7;
            // 
            // nextInstructionDisplayLabel
            // 
            this.nextInstructionDisplayLabel.AutoSize = true;
            this.nextInstructionDisplayLabel.BackColor = System.Drawing.Color.Transparent;
            this.nextInstructionDisplayLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.nextInstructionDisplayLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.nextInstructionDisplayLabel.Location = new System.Drawing.Point(3, 33);
            this.nextInstructionDisplayLabel.Name = "nextInstructionDisplayLabel";
            this.nextInstructionDisplayLabel.Size = new System.Drawing.Size(119, 20);
            this.nextInstructionDisplayLabel.TabIndex = 25;
            this.nextInstructionDisplayLabel.Text = "- - - - - - - - - - ";
            this.nextInstructionDisplayLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.BackColor = System.Drawing.Color.Transparent;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.ForeColor = System.Drawing.Color.Moccasin;
            this.label12.Location = new System.Drawing.Point(3, 0);
            this.label12.Name = "label12";
            this.label12.Padding = new System.Windows.Forms.Padding(1, 0, 0, 0);
            this.label12.Size = new System.Drawing.Size(184, 29);
            this.label12.TabIndex = 24;
            this.label12.Text = "Next Instruction:";
            this.label12.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.label12.Click += new System.EventHandler(this.label12_Click_1);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(473, 71);
            this.button1.Margin = new System.Windows.Forms.Padding(2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(56, 20);
            this.button1.TabIndex = 8;
            this.button1.Text = "Binary";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // LoadLabel
            // 
            this.LoadLabel.AutoSize = true;
            this.LoadLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LoadLabel.ForeColor = System.Drawing.Color.Moccasin;
            this.LoadLabel.Location = new System.Drawing.Point(472, 17);
            this.LoadLabel.Name = "LoadLabel";
            this.LoadLabel.Size = new System.Drawing.Size(62, 24);
            this.LoadLabel.TabIndex = 9;
            this.LoadLabel.Text = "Load:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(544, 296);
            this.Controls.Add(this.LoadLabel);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.nextInstructionTable);
            this.Controls.Add(this.runAllButton);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.nextInstructionButton);
            this.Controls.Add(this.loadFileButton);
            this.Controls.Add(this.accLabel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.BackgroundPicBox);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Form1";
            this.Text = "Best Gemini Microprocessor Ever";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.BackgroundPicBox)).EndInit();
            this.nextInstructionTable.ResumeLayout(false);
            this.nextInstructionTable.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label accLabel;
        private System.Windows.Forms.Button loadFileButton;
        private System.Windows.Forms.Button nextInstructionButton;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label registerLabel;
        private System.Windows.Forms.Label valueLabel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.PictureBox BackgroundPicBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label ALabel;
        private System.Windows.Forms.Label OneLabel;
        private System.Windows.Forms.Label ZeroLabel;
        private System.Windows.Forms.Label BLabel;
        private System.Windows.Forms.Label CCLabel;
        private System.Windows.Forms.Label IRLabel;
        private System.Windows.Forms.Label TEMPLabel;
        private System.Windows.Forms.Label MDRLabel;
        private System.Windows.Forms.Label MARLabel;
        private System.Windows.Forms.Label PCLabel;
        private System.Windows.Forms.Button runAllButton;
        private System.Windows.Forms.TableLayoutPanel nextInstructionTable;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label nextInstructionDisplayLabel;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label LoadLabel;
    }
}

