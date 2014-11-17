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
            this.BackgroundPicBox = new System.Windows.Forms.PictureBox();
            this.nextInstructionDisplayLabel = new System.Windows.Forms.Label();
            this.runAllButton = new System.Windows.Forms.Button();
            this.MissCountLabel = new System.Windows.Forms.Label();
            this.HitCountLabel = new System.Windows.Forms.Label();
            this.MissOrHitLabel = new System.Windows.Forms.Label();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.FetchDisplayLabel = new System.Windows.Forms.Label();
            this.DecodeDisplayLabel = new System.Windows.Forms.Label();
            this.ExecuteDisplayLabel = new System.Windows.Forms.Label();
            this.StoreDisplayLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.BackgroundPicBox)).BeginInit();
            this.tableLayoutPanel3.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // accLabel
            // 
            this.accLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold);
            this.accLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.accLabel.Location = new System.Drawing.Point(128, 37);
            this.accLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.accLabel.Name = "accLabel";
            this.accLabel.Size = new System.Drawing.Size(58, 25);
            this.accLabel.TabIndex = 1;
            this.accLabel.Text = "0";
            this.accLabel.Click += new System.EventHandler(this.accLabel_Click);
            // 
            // loadFileButton
            // 
            this.loadFileButton.BackColor = System.Drawing.Color.Transparent;
            this.loadFileButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("loadFileButton.BackgroundImage")));
            this.loadFileButton.FlatAppearance.BorderSize = 0;
            this.loadFileButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.loadFileButton.ForeColor = System.Drawing.Color.Transparent;
            this.loadFileButton.Location = new System.Drawing.Point(403, 10);
            this.loadFileButton.Margin = new System.Windows.Forms.Padding(2);
            this.loadFileButton.Name = "loadFileButton";
            this.loadFileButton.Size = new System.Drawing.Size(88, 46);
            this.loadFileButton.TabIndex = 2;
            this.loadFileButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.loadFileButton.UseVisualStyleBackColor = false;
            this.loadFileButton.Click += new System.EventHandler(this.loadFileButton_Click);
            // 
            // nextInstructionButton
            // 
            this.nextInstructionButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("nextInstructionButton.BackgroundImage")));
            this.nextInstructionButton.FlatAppearance.BorderSize = 0;
            this.nextInstructionButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.nextInstructionButton.ForeColor = System.Drawing.Color.Transparent;
            this.nextInstructionButton.Location = new System.Drawing.Point(411, 77);
            this.nextInstructionButton.Margin = new System.Windows.Forms.Padding(2);
            this.nextInstructionButton.Name = "nextInstructionButton";
            this.nextInstructionButton.Size = new System.Drawing.Size(86, 31);
            this.nextInstructionButton.TabIndex = 3;
            this.nextInstructionButton.UseVisualStyleBackColor = true;
            this.nextInstructionButton.Click += new System.EventHandler(this.nextInstructionButton_Click);
            // 
            // CCLabel
            // 
            this.CCLabel.AutoSize = true;
            this.CCLabel.BackColor = System.Drawing.Color.Transparent;
            this.CCLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.CCLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(74)))), ((int)(((byte)(119)))), ((int)(((byte)(74)))));
            this.CCLabel.Location = new System.Drawing.Point(3, 180);
            this.CCLabel.Name = "CCLabel";
            this.CCLabel.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.CCLabel.Size = new System.Drawing.Size(23, 20);
            this.CCLabel.TabIndex = 23;
            this.CCLabel.Text = "0";
            this.CCLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // IRLabel
            // 
            this.IRLabel.AutoSize = true;
            this.IRLabel.BackColor = System.Drawing.Color.Transparent;
            this.IRLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.IRLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(74)))), ((int)(((byte)(119)))), ((int)(((byte)(74)))));
            this.IRLabel.Location = new System.Drawing.Point(3, 160);
            this.IRLabel.Name = "IRLabel";
            this.IRLabel.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.IRLabel.Size = new System.Drawing.Size(23, 20);
            this.IRLabel.TabIndex = 22;
            this.IRLabel.Text = "0";
            this.IRLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // TEMPLabel
            // 
            this.TEMPLabel.AutoSize = true;
            this.TEMPLabel.BackColor = System.Drawing.Color.Transparent;
            this.TEMPLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.TEMPLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(74)))), ((int)(((byte)(119)))), ((int)(((byte)(74)))));
            this.TEMPLabel.Location = new System.Drawing.Point(3, 141);
            this.TEMPLabel.Name = "TEMPLabel";
            this.TEMPLabel.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.TEMPLabel.Size = new System.Drawing.Size(23, 19);
            this.TEMPLabel.TabIndex = 21;
            this.TEMPLabel.Text = "0";
            this.TEMPLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // MDRLabel
            // 
            this.MDRLabel.AutoSize = true;
            this.MDRLabel.BackColor = System.Drawing.Color.Transparent;
            this.MDRLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.MDRLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(74)))), ((int)(((byte)(119)))), ((int)(((byte)(74)))));
            this.MDRLabel.Location = new System.Drawing.Point(3, 122);
            this.MDRLabel.Name = "MDRLabel";
            this.MDRLabel.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.MDRLabel.Size = new System.Drawing.Size(23, 19);
            this.MDRLabel.TabIndex = 20;
            this.MDRLabel.Text = "0";
            this.MDRLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // MARLabel
            // 
            this.MARLabel.AutoSize = true;
            this.MARLabel.BackColor = System.Drawing.Color.Transparent;
            this.MARLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.MARLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(74)))), ((int)(((byte)(119)))), ((int)(((byte)(74)))));
            this.MARLabel.Location = new System.Drawing.Point(3, 105);
            this.MARLabel.Name = "MARLabel";
            this.MARLabel.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.MARLabel.Size = new System.Drawing.Size(23, 17);
            this.MARLabel.TabIndex = 19;
            this.MARLabel.Text = "0";
            this.MARLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // PCLabel
            // 
            this.PCLabel.AutoSize = true;
            this.PCLabel.BackColor = System.Drawing.Color.Transparent;
            this.PCLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.PCLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(74)))), ((int)(((byte)(119)))), ((int)(((byte)(74)))));
            this.PCLabel.Location = new System.Drawing.Point(3, 85);
            this.PCLabel.Name = "PCLabel";
            this.PCLabel.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.PCLabel.Size = new System.Drawing.Size(23, 20);
            this.PCLabel.TabIndex = 18;
            this.PCLabel.Text = "0";
            this.PCLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // OneLabel
            // 
            this.OneLabel.AutoSize = true;
            this.OneLabel.BackColor = System.Drawing.Color.Transparent;
            this.OneLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.OneLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(74)))), ((int)(((byte)(119)))), ((int)(((byte)(74)))));
            this.OneLabel.Location = new System.Drawing.Point(3, 68);
            this.OneLabel.Name = "OneLabel";
            this.OneLabel.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.OneLabel.Size = new System.Drawing.Size(23, 17);
            this.OneLabel.TabIndex = 17;
            this.OneLabel.Text = "1";
            this.OneLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.OneLabel.Click += new System.EventHandler(this.label15_Click);
            // 
            // ZeroLabel
            // 
            this.ZeroLabel.AutoSize = true;
            this.ZeroLabel.BackColor = System.Drawing.Color.Transparent;
            this.ZeroLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.ZeroLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(74)))), ((int)(((byte)(119)))), ((int)(((byte)(74)))));
            this.ZeroLabel.Location = new System.Drawing.Point(3, 50);
            this.ZeroLabel.Name = "ZeroLabel";
            this.ZeroLabel.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.ZeroLabel.Size = new System.Drawing.Size(23, 18);
            this.ZeroLabel.TabIndex = 16;
            this.ZeroLabel.Text = "0";
            this.ZeroLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // BLabel
            // 
            this.BLabel.AutoSize = true;
            this.BLabel.BackColor = System.Drawing.Color.Transparent;
            this.BLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.BLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(74)))), ((int)(((byte)(119)))), ((int)(((byte)(74)))));
            this.BLabel.Location = new System.Drawing.Point(3, 30);
            this.BLabel.Name = "BLabel";
            this.BLabel.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.BLabel.Size = new System.Drawing.Size(23, 20);
            this.BLabel.TabIndex = 15;
            this.BLabel.Text = "0";
            this.BLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // ALabel
            // 
            this.ALabel.AutoSize = true;
            this.ALabel.BackColor = System.Drawing.Color.Transparent;
            this.ALabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.ALabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(74)))), ((int)(((byte)(119)))), ((int)(((byte)(74)))));
            this.ALabel.Location = new System.Drawing.Point(3, 0);
            this.ALabel.Name = "ALabel";
            this.ALabel.Padding = new System.Windows.Forms.Padding(5, 8, 0, 0);
            this.ALabel.Size = new System.Drawing.Size(23, 28);
            this.ALabel.TabIndex = 14;
            this.ALabel.Text = "0";
            this.ALabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.ALabel.Click += new System.EventHandler(this.label12_Click);
            // 
            // BackgroundPicBox
            // 
            this.BackgroundPicBox.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.BackgroundPicBox.Image = ((System.Drawing.Image)(resources.GetObject("BackgroundPicBox.Image")));
            this.BackgroundPicBox.Location = new System.Drawing.Point(-12, -14);
            this.BackgroundPicBox.Name = "BackgroundPicBox";
            this.BackgroundPicBox.Size = new System.Drawing.Size(563, 647);
            this.BackgroundPicBox.TabIndex = 5;
            this.BackgroundPicBox.TabStop = false;
            this.BackgroundPicBox.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // nextInstructionDisplayLabel
            // 
            this.nextInstructionDisplayLabel.AutoSize = true;
            this.nextInstructionDisplayLabel.BackColor = System.Drawing.Color.Transparent;
            this.nextInstructionDisplayLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nextInstructionDisplayLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.nextInstructionDisplayLabel.Location = new System.Drawing.Point(3, 0);
            this.nextInstructionDisplayLabel.Name = "nextInstructionDisplayLabel";
            this.nextInstructionDisplayLabel.Size = new System.Drawing.Size(119, 20);
            this.nextInstructionDisplayLabel.TabIndex = 25;
            this.nextInstructionDisplayLabel.Text = "- - - - - - - - - - ";
            this.nextInstructionDisplayLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.nextInstructionDisplayLabel.Click += new System.EventHandler(this.nextInstructionDisplayLabel_Click);
            // 
            // runAllButton
            // 
            this.runAllButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("runAllButton.BackgroundImage")));
            this.runAllButton.FlatAppearance.BorderSize = 0;
            this.runAllButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.runAllButton.Location = new System.Drawing.Point(411, 112);
            this.runAllButton.Name = "runAllButton";
            this.runAllButton.Size = new System.Drawing.Size(86, 30);
            this.runAllButton.TabIndex = 10;
            this.runAllButton.UseVisualStyleBackColor = true;
            this.runAllButton.Click += new System.EventHandler(this.runAllButton_Click);
            // 
            // MissCountLabel
            // 
            this.MissCountLabel.AutoSize = true;
            this.MissCountLabel.BackColor = System.Drawing.Color.Transparent;
            this.MissCountLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.25F);
            this.MissCountLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.MissCountLabel.Location = new System.Drawing.Point(3, 45);
            this.MissCountLabel.Name = "MissCountLabel";
            this.MissCountLabel.Padding = new System.Windows.Forms.Padding(15, 5, 0, 0);
            this.MissCountLabel.Size = new System.Drawing.Size(39, 30);
            this.MissCountLabel.TabIndex = 26;
            this.MissCountLabel.Text = "0";
            this.MissCountLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // HitCountLabel
            // 
            this.HitCountLabel.AutoSize = true;
            this.HitCountLabel.BackColor = System.Drawing.Color.Transparent;
            this.HitCountLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.25F);
            this.HitCountLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.HitCountLabel.Location = new System.Drawing.Point(3, 0);
            this.HitCountLabel.Name = "HitCountLabel";
            this.HitCountLabel.Padding = new System.Windows.Forms.Padding(15, 11, 0, 0);
            this.HitCountLabel.Size = new System.Drawing.Size(39, 36);
            this.HitCountLabel.TabIndex = 17;
            this.HitCountLabel.Text = "0";
            this.HitCountLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // MissOrHitLabel
            // 
            this.MissOrHitLabel.AutoSize = true;
            this.MissOrHitLabel.BackColor = System.Drawing.Color.Transparent;
            this.MissOrHitLabel.Font = new System.Drawing.Font("Verdana", 15.75F, System.Drawing.FontStyle.Bold);
            this.MissOrHitLabel.ForeColor = System.Drawing.Color.Yellow;
            this.MissOrHitLabel.Location = new System.Drawing.Point(142, 188);
            this.MissOrHitLabel.Name = "MissOrHitLabel";
            this.MissOrHitLabel.Padding = new System.Windows.Forms.Padding(1, 0, 0, 0);
            this.MissOrHitLabel.Size = new System.Drawing.Size(64, 25);
            this.MissOrHitLabel.TabIndex = 25;
            this.MissOrHitLabel.Text = "  --- ";
            this.MissOrHitLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.MissOrHitLabel.Click += new System.EventHandler(this.label17_Click);
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tableLayoutPanel3.BackgroundImage")));
            this.tableLayoutPanel3.ColumnCount = 1;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 88F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel3.Controls.Add(this.CCLabel, 0, 9);
            this.tableLayoutPanel3.Controls.Add(this.ALabel, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.IRLabel, 0, 8);
            this.tableLayoutPanel3.Controls.Add(this.BLabel, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.TEMPLabel, 0, 7);
            this.tableLayoutPanel3.Controls.Add(this.ZeroLabel, 0, 2);
            this.tableLayoutPanel3.Controls.Add(this.MDRLabel, 0, 6);
            this.tableLayoutPanel3.Controls.Add(this.OneLabel, 0, 3);
            this.tableLayoutPanel3.Controls.Add(this.MARLabel, 0, 5);
            this.tableLayoutPanel3.Controls.Add(this.PCLabel, 0, 4);
            this.tableLayoutPanel3.Location = new System.Drawing.Point(379, 199);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 10;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 18F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 17F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 17F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 19F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 19F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(88, 198);
            this.tableLayoutPanel3.TabIndex = 26;
            this.tableLayoutPanel3.Paint += new System.Windows.Forms.PaintEventHandler(this.tableLayoutPanel3_Paint);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tableLayoutPanel1.BackgroundImage")));
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.MissCountLabel, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.HitCountLabel, 0, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(104, 227);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(109, 91);
            this.tableLayoutPanel1.TabIndex = 27;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tableLayoutPanel2.BackgroundImage")));
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.nextInstructionDisplayLabel, 0, 0);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(63, 132);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(129, 27);
            this.tableLayoutPanel2.TabIndex = 28;
            // 
            // FetchDisplayLabel
            // 
            this.FetchDisplayLabel.AutoSize = true;
            this.FetchDisplayLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FetchDisplayLabel.Location = new System.Drawing.Point(129, 405);
            this.FetchDisplayLabel.Name = "FetchDisplayLabel";
            this.FetchDisplayLabel.Size = new System.Drawing.Size(51, 20);
            this.FetchDisplayLabel.TabIndex = 29;
            this.FetchDisplayLabel.Text = "label1";
            // 
            // DecodeDisplayLabel
            // 
            this.DecodeDisplayLabel.AutoSize = true;
            this.DecodeDisplayLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DecodeDisplayLabel.Location = new System.Drawing.Point(162, 537);
            this.DecodeDisplayLabel.Name = "DecodeDisplayLabel";
            this.DecodeDisplayLabel.Size = new System.Drawing.Size(51, 20);
            this.DecodeDisplayLabel.TabIndex = 30;
            this.DecodeDisplayLabel.Text = "label1";
            this.DecodeDisplayLabel.Click += new System.EventHandler(this.label1_Click_1);
            // 
            // ExecuteDisplayLabel
            // 
            this.ExecuteDisplayLabel.AutoSize = true;
            this.ExecuteDisplayLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ExecuteDisplayLabel.Location = new System.Drawing.Point(342, 465);
            this.ExecuteDisplayLabel.Name = "ExecuteDisplayLabel";
            this.ExecuteDisplayLabel.Size = new System.Drawing.Size(51, 20);
            this.ExecuteDisplayLabel.TabIndex = 31;
            this.ExecuteDisplayLabel.Text = "label1";
            // 
            // StoreDisplayLabel
            // 
            this.StoreDisplayLabel.AutoSize = true;
            this.StoreDisplayLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StoreDisplayLabel.Location = new System.Drawing.Point(375, 580);
            this.StoreDisplayLabel.Name = "StoreDisplayLabel";
            this.StoreDisplayLabel.Size = new System.Drawing.Size(51, 20);
            this.StoreDisplayLabel.TabIndex = 32;
            this.StoreDisplayLabel.Text = "label1";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(540, 628);
            this.Controls.Add(this.StoreDisplayLabel);
            this.Controls.Add(this.ExecuteDisplayLabel);
            this.Controls.Add(this.DecodeDisplayLabel);
            this.Controls.Add(this.FetchDisplayLabel);
            this.Controls.Add(this.tableLayoutPanel2);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.tableLayoutPanel3);
            this.Controls.Add(this.MissOrHitLabel);
            this.Controls.Add(this.runAllButton);
            this.Controls.Add(this.nextInstructionButton);
            this.Controls.Add(this.loadFileButton);
            this.Controls.Add(this.accLabel);
            this.Controls.Add(this.BackgroundPicBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Form1";
            this.Text = "Best Gemini Microprocessor Ever    (╯°Д°）╯︵ ┻━┻";
            ((System.ComponentModel.ISupportInitialize)(this.BackgroundPicBox)).EndInit();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label accLabel;
        private System.Windows.Forms.Button loadFileButton;
        private System.Windows.Forms.Button nextInstructionButton;
        private System.Windows.Forms.PictureBox BackgroundPicBox;
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
        private System.Windows.Forms.Label nextInstructionDisplayLabel;
        private System.Windows.Forms.Button runAllButton;
        private System.Windows.Forms.Label MissCountLabel;
        private System.Windows.Forms.Label HitCountLabel;
        private System.Windows.Forms.Label MissOrHitLabel;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label FetchDisplayLabel;
        private System.Windows.Forms.Label DecodeDisplayLabel;
        private System.Windows.Forms.Label ExecuteDisplayLabel;
        private System.Windows.Forms.Label StoreDisplayLabel;
    }
}

