namespace VisionInspection
{
    partial class Form1
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
            pictureBoxResult = new PictureBox();
            pictureBoxSrc = new PictureBox();
            buttonLoad = new Button();
            buttonInspect = new Button();
            labelResult = new Label();
            trackThreshold = new TrackBar();
            trackRatio = new TrackBar();
            labelThreshold = new Label();
            labelRatio = new Label();
            checkAutoThreshold = new CheckBox();
            labelDefectCount = new Label();
            labelDefectArea = new Label();
            ((System.ComponentModel.ISupportInitialize)pictureBoxResult).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxSrc).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trackThreshold).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trackRatio).BeginInit();
            SuspendLayout();
            // 
            // pictureBoxResult
            // 
            pictureBoxResult.Location = new Point(443, 199);
            pictureBoxResult.Name = "pictureBoxResult";
            pictureBoxResult.Size = new Size(169, 103);
            pictureBoxResult.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBoxResult.TabIndex = 0;
            pictureBoxResult.TabStop = false;
            pictureBoxResult.Click += pictureBoxResult_Click;
            // 
            // pictureBoxSrc
            // 
            pictureBoxSrc.BorderStyle = BorderStyle.FixedSingle;
            pictureBoxSrc.Location = new Point(208, 199);
            pictureBoxSrc.Name = "pictureBoxSrc";
            pictureBoxSrc.Size = new Size(175, 103);
            pictureBoxSrc.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBoxSrc.TabIndex = 1;
            pictureBoxSrc.TabStop = false;
            pictureBoxSrc.Paint += pictureBoxSrc_Paint;
            pictureBoxSrc.MouseDown += pictureBoxSrc_MouseDown;
            pictureBoxSrc.MouseMove += pictureBoxSrc_MouseMove;
            pictureBoxSrc.MouseUp += pictureBoxSrc_MouseUp;
            // 
            // buttonLoad
            // 
            buttonLoad.Location = new Point(210, 160);
            buttonLoad.Name = "buttonLoad";
            buttonLoad.Size = new Size(112, 23);
            buttonLoad.TabIndex = 2;
            buttonLoad.Text = "이미지 불러오기\r\n";
            buttonLoad.UseVisualStyleBackColor = true;
            buttonLoad.Click += buttonLoad_Click;
            // 
            // buttonInspect
            // 
            buttonInspect.Location = new Point(443, 160);
            buttonInspect.Name = "buttonInspect";
            buttonInspect.Size = new Size(75, 23);
            buttonInspect.TabIndex = 3;
            buttonInspect.Text = "검사실행\r\n";
            buttonInspect.UseVisualStyleBackColor = true;
            buttonInspect.Click += buttonInspect_Click;
            // 
            // labelResult
            // 
            labelResult.AutoSize = true;
            labelResult.Font = new Font("맑은 고딕", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 129);
            labelResult.Location = new Point(341, 98);
            labelResult.Name = "labelResult";
            labelResult.Size = new Size(95, 25);
            labelResult.TabIndex = 4;
            labelResult.Text = "RESULT: -";
            labelResult.TextAlign = ContentAlignment.MiddleCenter;
            labelResult.Click += labelResult_Click;
            // 
            // trackThreshold
            // 
            trackThreshold.Location = new Point(231, 361);
            trackThreshold.Maximum = 255;
            trackThreshold.Name = "trackThreshold";
            trackThreshold.Size = new Size(104, 45);
            trackThreshold.TabIndex = 6;
            trackThreshold.TickFrequency = 10;
            trackThreshold.Value = 120;
            trackThreshold.Scroll += trackThreshold_Scroll;
            // 
            // trackRatio
            // 
            trackRatio.Location = new Point(468, 361);
            trackRatio.Maximum = 100;
            trackRatio.Name = "trackRatio";
            trackRatio.Size = new Size(104, 45);
            trackRatio.TabIndex = 7;
            trackRatio.TickFrequency = 5;
            trackRatio.Value = 30;
            trackRatio.Scroll += trackRatio_Scroll;
            // 
            // labelThreshold
            // 
            labelThreshold.AutoSize = true;
            labelThreshold.Location = new Point(235, 325);
            labelThreshold.Name = "labelThreshold";
            labelThreshold.Size = new Size(87, 15);
            labelThreshold.TabIndex = 8;
            labelThreshold.Text = "Threshold: 120";
            labelThreshold.Click += label1_Click;
            // 
            // labelRatio
            // 
            labelRatio.AutoSize = true;
            labelRatio.Location = new Point(468, 325);
            labelRatio.Name = "labelRatio";
            labelRatio.Size = new Size(95, 15);
            labelRatio.TabIndex = 9;
            labelRatio.Text = "Fail Ratio(%): 30";
            labelRatio.Click += labelRatio_Click;
            // 
            // checkAutoThreshold
            // 
            checkAutoThreshold.AutoSize = true;
            checkAutoThreshold.Location = new Point(342, 76);
            checkAutoThreshold.Name = "checkAutoThreshold";
            checkAutoThreshold.Size = new Size(94, 19);
            checkAutoThreshold.TabIndex = 10;
            checkAutoThreshold.Text = "Auto (OTSU)";
            checkAutoThreshold.UseVisualStyleBackColor = true;
            checkAutoThreshold.CheckedChanged += checkAutoThreshold_CheckedChanged;
            // 
            // labelDefectCount
            // 
            labelDefectCount.AutoSize = true;
            labelDefectCount.Location = new Point(210, 97);
            labelDefectCount.Name = "labelDefectCount";
            labelDefectCount.Size = new Size(61, 15);
            labelDefectCount.TabIndex = 11;
            labelDefectCount.Text = "Defects: 0";
            labelDefectCount.Click += label1_Click_1;
            // 
            // labelDefectArea
            // 
            labelDefectArea.AutoSize = true;
            labelDefectArea.Location = new Point(210, 121);
            labelDefectArea.Name = "labelDefectArea";
            labelDefectArea.Size = new Size(45, 15);
            labelDefectArea.TabIndex = 12;
            labelDefectArea.Text = "Area: 0";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(labelDefectArea);
            Controls.Add(labelDefectCount);
            Controls.Add(checkAutoThreshold);
            Controls.Add(labelRatio);
            Controls.Add(labelThreshold);
            Controls.Add(trackRatio);
            Controls.Add(trackThreshold);
            Controls.Add(labelResult);
            Controls.Add(buttonInspect);
            Controls.Add(buttonLoad);
            Controls.Add(pictureBoxSrc);
            Controls.Add(pictureBoxResult);
            Name = "Form1";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBoxResult).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxSrc).EndInit();
            ((System.ComponentModel.ISupportInitialize)trackThreshold).EndInit();
            ((System.ComponentModel.ISupportInitialize)trackRatio).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox pictureBoxResult;
        private PictureBox pictureBoxSrc;
        private Button buttonLoad;
        private Button buttonInspect;
        private Label labelResult;
        private TrackBar trackThreshold;
        private TrackBar trackRatio;
        private Label labelThreshold;
        private Label labelRatio;
        private CheckBox checkAutoThreshold;
        private Label labelDefectCount;
        private Label labelDefectArea;
    }
}
