namespace Robovator2.Controls
{
    partial class UserControlOneCam
    {
        /// <summary> 
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором компонентов

        /// <summary> 
        /// Обязательный метод для поддержки конструктора - не изменяйте 
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnColorCollection = new System.Windows.Forms.Button();
            this.btnColorDialog = new System.Windows.Forms.Button();
            this.labelRadius = new System.Windows.Forms.Label();
            this.labelCountOfObjects = new System.Windows.Forms.Label();
            this.labelRadiusNum = new System.Windows.Forms.Label();
            this.btnToTakePicture = new System.Windows.Forms.Button();
            this.comboBoxDevices = new System.Windows.Forms.ComboBox();
            this.BtnConnetct = new System.Windows.Forms.Button();
            this.pictureBoxSource = new System.Windows.Forms.PictureBox();
            this.pictureBoxDest = new System.Windows.Forms.PictureBox();
            this.hScrollBar1 = new System.Windows.Forms.HScrollBar();
            this.hScrollBar2 = new System.Windows.Forms.HScrollBar();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.hScrollBar3 = new System.Windows.Forms.HScrollBar();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.btnStarStop = new System.Windows.Forms.Button();
            this.formName = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.hScrollBar4 = new System.Windows.Forms.HScrollBar();
            this.hScrollBar5 = new System.Windows.Forms.HScrollBar();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.hScrollBar6 = new System.Windows.Forms.HScrollBar();
            this.hScrollBar7 = new System.Windows.Forms.HScrollBar();
            this.hScrollBar8 = new System.Windows.Forms.HScrollBar();
            this.hScrollBar9 = new System.Windows.Forms.HScrollBar();
            this.hScrollBar10 = new System.Windows.Forms.HScrollBar();
            this.hScrollBar11 = new System.Windows.Forms.HScrollBar();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxDest)).BeginInit();
            this.SuspendLayout();
            // 
            // btnColorCollection
            // 
            this.btnColorCollection.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnColorCollection.Location = new System.Drawing.Point(901, 18);
            this.btnColorCollection.Name = "btnColorCollection";
            this.btnColorCollection.Size = new System.Drawing.Size(106, 85);
            this.btnColorCollection.TabIndex = 46;
            this.btnColorCollection.Text = "ColorColl";
            this.btnColorCollection.UseVisualStyleBackColor = true;
            this.btnColorCollection.Click += new System.EventHandler(this.btnColorCollection_Click);
            // 
            // btnColorDialog
            // 
            this.btnColorDialog.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnColorDialog.Location = new System.Drawing.Point(789, 18);
            this.btnColorDialog.Name = "btnColorDialog";
            this.btnColorDialog.Size = new System.Drawing.Size(106, 85);
            this.btnColorDialog.TabIndex = 45;
            this.btnColorDialog.Text = "Color";
            this.btnColorDialog.UseVisualStyleBackColor = true;
            this.btnColorDialog.Click += new System.EventHandler(this.btnColorDialog_Click);
            // 
            // labelRadius
            // 
            this.labelRadius.AutoSize = true;
            this.labelRadius.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelRadius.Location = new System.Drawing.Point(553, 608);
            this.labelRadius.Name = "labelRadius";
            this.labelRadius.Size = new System.Drawing.Size(57, 16);
            this.labelRadius.TabIndex = 40;
            this.labelRadius.Text = "Radius";
            // 
            // labelCountOfObjects
            // 
            this.labelCountOfObjects.AutoSize = true;
            this.labelCountOfObjects.Location = new System.Drawing.Point(31, 307);
            this.labelCountOfObjects.Name = "labelCountOfObjects";
            this.labelCountOfObjects.Size = new System.Drawing.Size(0, 13);
            this.labelCountOfObjects.TabIndex = 39;
            // 
            // labelRadiusNum
            // 
            this.labelRadiusNum.AutoSize = true;
            this.labelRadiusNum.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelRadiusNum.Location = new System.Drawing.Point(646, 608);
            this.labelRadiusNum.Name = "labelRadiusNum";
            this.labelRadiusNum.Size = new System.Drawing.Size(0, 16);
            this.labelRadiusNum.TabIndex = 38;
            // 
            // btnToTakePicture
            // 
            this.btnToTakePicture.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnToTakePicture.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnToTakePicture.Location = new System.Drawing.Point(677, 18);
            this.btnToTakePicture.Name = "btnToTakePicture";
            this.btnToTakePicture.Size = new System.Drawing.Size(106, 85);
            this.btnToTakePicture.TabIndex = 36;
            this.btnToTakePicture.Text = "To take a picture";
            this.btnToTakePicture.UseVisualStyleBackColor = true;
            this.btnToTakePicture.Click += new System.EventHandler(this.btnToTakePicture_Click);
            // 
            // comboBoxDevices
            // 
            this.comboBoxDevices.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.comboBoxDevices.FormattingEnabled = true;
            this.comboBoxDevices.Location = new System.Drawing.Point(26, 20);
            this.comboBoxDevices.Name = "comboBoxDevices";
            this.comboBoxDevices.Size = new System.Drawing.Size(258, 24);
            this.comboBoxDevices.TabIndex = 34;
            // 
            // BtnConnetct
            // 
            this.BtnConnetct.BackColor = System.Drawing.Color.Blue;
            this.BtnConnetct.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.BtnConnetct.Location = new System.Drawing.Point(290, 18);
            this.BtnConnetct.Name = "BtnConnetct";
            this.BtnConnetct.Size = new System.Drawing.Size(106, 85);
            this.BtnConnetct.TabIndex = 33;
            this.BtnConnetct.Text = "Connect / Disconnect";
            this.BtnConnetct.UseVisualStyleBackColor = false;
            this.BtnConnetct.Click += new System.EventHandler(this.BtnConnetct_Click);
            // 
            // pictureBoxSource
            // 
            this.pictureBoxSource.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pictureBoxSource.Location = new System.Drawing.Point(26, 109);
            this.pictureBoxSource.Name = "pictureBoxSource";
            this.pictureBoxSource.Size = new System.Drawing.Size(360, 308);
            this.pictureBoxSource.TabIndex = 47;
            this.pictureBoxSource.TabStop = false;
            // 
            // pictureBoxDest
            // 
            this.pictureBoxDest.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pictureBoxDest.Location = new System.Drawing.Point(677, 109);
            this.pictureBoxDest.Name = "pictureBoxDest";
            this.pictureBoxDest.Size = new System.Drawing.Size(387, 308);
            this.pictureBoxDest.TabIndex = 48;
            this.pictureBoxDest.TabStop = false;
            // 
            // hScrollBar1
            // 
            this.hScrollBar1.Location = new System.Drawing.Point(677, 595);
            this.hScrollBar1.Maximum = 450;
            this.hScrollBar1.Name = "hScrollBar1";
            this.hScrollBar1.Size = new System.Drawing.Size(640, 54);
            this.hScrollBar1.TabIndex = 49;
            this.hScrollBar1.Scroll += new System.Windows.Forms.ScrollEventHandler(this.hScrollBar1_Scroll);
            // 
            // hScrollBar2
            // 
            this.hScrollBar2.Location = new System.Drawing.Point(677, 666);
            this.hScrollBar2.Maximum = 450;
            this.hScrollBar2.Name = "hScrollBar2";
            this.hScrollBar2.Size = new System.Drawing.Size(640, 54);
            this.hScrollBar2.TabIndex = 50;
            this.hScrollBar2.Scroll += new System.Windows.Forms.ScrollEventHandler(this.hScrollBar2_Scroll);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(553, 685);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 16);
            this.label1.TabIndex = 51;
            this.label1.Text = "Min Width";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.label3.Location = new System.Drawing.Point(646, 687);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(0, 16);
            this.label3.TabIndex = 53;
            // 
            // hScrollBar3
            // 
            this.hScrollBar3.Location = new System.Drawing.Point(677, 737);
            this.hScrollBar3.Maximum = 450;
            this.hScrollBar3.Name = "hScrollBar3";
            this.hScrollBar3.Size = new System.Drawing.Size(640, 54);
            this.hScrollBar3.TabIndex = 54;
            this.hScrollBar3.Scroll += new System.Windows.Forms.ScrollEventHandler(this.hScrollBar3_Scroll);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.label4.Location = new System.Drawing.Point(556, 760);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(81, 16);
            this.label4.TabIndex = 55;
            this.label4.Text = "Min Height";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.label5.Location = new System.Drawing.Point(646, 763);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(0, 16);
            this.label5.TabIndex = 56;
            // 
            // btnStarStop
            // 
            this.btnStarStop.BackColor = System.Drawing.Color.Blue;
            this.btnStarStop.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnStarStop.Location = new System.Drawing.Point(1211, 18);
            this.btnStarStop.Name = "btnStarStop";
            this.btnStarStop.Size = new System.Drawing.Size(106, 85);
            this.btnStarStop.TabIndex = 57;
            this.btnStarStop.Text = "Start/Stop";
            this.btnStarStop.UseVisualStyleBackColor = false;
            this.btnStarStop.Click += new System.EventHandler(this.btnStarStop_Click);
            // 
            // formName
            // 
            this.formName.AutoSize = true;
            this.formName.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.formName.Location = new System.Drawing.Point(23, 71);
            this.formName.Name = "formName";
            this.formName.Size = new System.Drawing.Size(76, 25);
            this.formName.TabIndex = 58;
            this.formName.Text = "label2";
            this.formName.Click += new System.EventHandler(this.formName_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(26, 608);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(193, 16);
            this.label2.TabIndex = 59;
            this.label2.Text = "Distance to the mechanism";
            // 
            // hScrollBar4
            // 
            this.hScrollBar4.Location = new System.Drawing.Point(259, 592);
            this.hScrollBar4.Name = "hScrollBar4";
            this.hScrollBar4.Size = new System.Drawing.Size(291, 54);
            this.hScrollBar4.TabIndex = 60;
            this.hScrollBar4.Scroll += new System.Windows.Forms.ScrollEventHandler(this.hScrollBar4_Scroll);
            // 
            // hScrollBar5
            // 
            this.hScrollBar5.Location = new System.Drawing.Point(259, 669);
            this.hScrollBar5.Maximum = 320;
            this.hScrollBar5.Name = "hScrollBar5";
            this.hScrollBar5.Size = new System.Drawing.Size(291, 54);
            this.hScrollBar5.TabIndex = 61;
            this.hScrollBar5.Scroll += new System.Windows.Forms.ScrollEventHandler(this.hScrollBar5_Scroll);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.label6.Location = new System.Drawing.Point(26, 685);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(103, 16);
            this.label6.TabIndex = 62;
            this.label6.Text = "Safe distance";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Lime;
            this.label7.Location = new System.Drawing.Point(1057, 18);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(86, 13);
            this.label7.TabIndex = 63;
            this.label7.Text = "the left boundary";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.Lime;
            this.label8.Location = new System.Drawing.Point(1057, 54);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(92, 13);
            this.label8.TabIndex = 64;
            this.label8.Text = "the right boundary";
            // 
            // hScrollBar6
            // 
            this.hScrollBar6.Location = new System.Drawing.Point(26, 445);
            this.hScrollBar6.Maximum = 1000;
            this.hScrollBar6.Name = "hScrollBar6";
            this.hScrollBar6.Size = new System.Drawing.Size(342, 33);
            this.hScrollBar6.TabIndex = 65;
            this.hScrollBar6.Scroll += new System.Windows.Forms.ScrollEventHandler(this.hScrollBar6_Scroll);
            // 
            // hScrollBar7
            // 
            this.hScrollBar7.Location = new System.Drawing.Point(26, 489);
            this.hScrollBar7.Maximum = 500;
            this.hScrollBar7.Minimum = -500;
            this.hScrollBar7.Name = "hScrollBar7";
            this.hScrollBar7.Size = new System.Drawing.Size(342, 33);
            this.hScrollBar7.TabIndex = 66;
            this.hScrollBar7.Scroll += new System.Windows.Forms.ScrollEventHandler(this.hScrollBar7_Scroll);
            // 
            // hScrollBar8
            // 
            this.hScrollBar8.Location = new System.Drawing.Point(26, 533);
            this.hScrollBar8.Maximum = 500;
            this.hScrollBar8.Minimum = -500;
            this.hScrollBar8.Name = "hScrollBar8";
            this.hScrollBar8.Size = new System.Drawing.Size(342, 33);
            this.hScrollBar8.TabIndex = 67;
            this.hScrollBar8.Scroll += new System.Windows.Forms.ScrollEventHandler(this.hScrollBar8_Scroll);
            // 
            // hScrollBar9
            // 
            this.hScrollBar9.Location = new System.Drawing.Point(403, 533);
            this.hScrollBar9.Maximum = 500;
            this.hScrollBar9.Minimum = -500;
            this.hScrollBar9.Name = "hScrollBar9";
            this.hScrollBar9.Size = new System.Drawing.Size(364, 33);
            this.hScrollBar9.TabIndex = 70;
            this.hScrollBar9.Scroll += new System.Windows.Forms.ScrollEventHandler(this.hScrollBar9_Scroll);
            // 
            // hScrollBar10
            // 
            this.hScrollBar10.Location = new System.Drawing.Point(403, 489);
            this.hScrollBar10.Maximum = 500;
            this.hScrollBar10.Minimum = -500;
            this.hScrollBar10.Name = "hScrollBar10";
            this.hScrollBar10.Size = new System.Drawing.Size(364, 33);
            this.hScrollBar10.TabIndex = 69;
            this.hScrollBar10.Scroll += new System.Windows.Forms.ScrollEventHandler(this.hScrollBar10_Scroll);
            // 
            // hScrollBar11
            // 
            this.hScrollBar11.Location = new System.Drawing.Point(403, 445);
            this.hScrollBar11.Maximum = 1000;
            this.hScrollBar11.Name = "hScrollBar11";
            this.hScrollBar11.Size = new System.Drawing.Size(364, 33);
            this.hScrollBar11.TabIndex = 68;
            this.hScrollBar11.Scroll += new System.Windows.Forms.ScrollEventHandler(this.hScrollBar11_Scroll);
            // 
            // UserControlOneCam
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoSize = true;
            this.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.Controls.Add(this.hScrollBar9);
            this.Controls.Add(this.hScrollBar10);
            this.Controls.Add(this.hScrollBar11);
            this.Controls.Add(this.hScrollBar8);
            this.Controls.Add(this.hScrollBar7);
            this.Controls.Add(this.hScrollBar6);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.hScrollBar5);
            this.Controls.Add(this.hScrollBar4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.formName);
            this.Controls.Add(this.btnStarStop);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.hScrollBar3);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.hScrollBar2);
            this.Controls.Add(this.hScrollBar1);
            this.Controls.Add(this.pictureBoxDest);
            this.Controls.Add(this.pictureBoxSource);
            this.Controls.Add(this.btnColorCollection);
            this.Controls.Add(this.btnColorDialog);
            this.Controls.Add(this.labelRadius);
            this.Controls.Add(this.labelCountOfObjects);
            this.Controls.Add(this.labelRadiusNum);
            this.Controls.Add(this.btnToTakePicture);
            this.Controls.Add(this.comboBoxDevices);
            this.Controls.Add(this.BtnConnetct);
            this.Name = "UserControlOneCam";
            this.Size = new System.Drawing.Size(1337, 813);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxDest)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnColorCollection;
        private System.Windows.Forms.Button btnColorDialog;
        private System.Windows.Forms.Label labelRadius;
        private System.Windows.Forms.Label labelCountOfObjects;
        private System.Windows.Forms.Label labelRadiusNum;
        public System.Windows.Forms.Button btnToTakePicture;
        private System.Windows.Forms.ComboBox comboBoxDevices;
        private System.Windows.Forms.Button BtnConnetct;
        private System.Windows.Forms.PictureBox pictureBoxSource;
        private System.Windows.Forms.PictureBox pictureBoxDest;
        private System.Windows.Forms.HScrollBar hScrollBar1;
        private System.Windows.Forms.HScrollBar hScrollBar2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.HScrollBar hScrollBar3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnStarStop;
        private System.Windows.Forms.Label formName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.HScrollBar hScrollBar4;
        private System.Windows.Forms.HScrollBar hScrollBar5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.HScrollBar hScrollBar6;
        private System.Windows.Forms.HScrollBar hScrollBar7;
        private System.Windows.Forms.HScrollBar hScrollBar8;
        private System.Windows.Forms.HScrollBar hScrollBar9;
        private System.Windows.Forms.HScrollBar hScrollBar10;
        private System.Windows.Forms.HScrollBar hScrollBar11;
    }
}
