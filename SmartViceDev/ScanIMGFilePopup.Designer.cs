namespace SmartViceDev
{
    partial class ScanIMGFilePopup
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
            this.btnSaveNClose = new System.Windows.Forms.Button();
            this.openStaffFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btnRegImg = new System.Windows.Forms.Button();
            this.pic_scan = new System.Windows.Forms.PictureBox();
            this.lbDevices = new System.Windows.Forms.Label();
            this.btnScanImg = new System.Windows.Forms.Button();
            this.tbDevices = new System.Windows.Forms.ListBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnDownloadImg = new System.Windows.Forms.Button();
            this.btnRotateImage = new System.Windows.Forms.Button();
            this.LabelImg = new System.Windows.Forms.Label();
            this.btnRemoveImg = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pic_scan)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnSaveNClose
            // 
            this.btnSaveNClose.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnSaveNClose.Location = new System.Drawing.Point(509, 546);
            this.btnSaveNClose.Name = "btnSaveNClose";
            this.btnSaveNClose.Size = new System.Drawing.Size(224, 40);
            this.btnSaveNClose.TabIndex = 7;
            this.btnSaveNClose.Text = "저장 및 닫기";
            this.btnSaveNClose.UseVisualStyleBackColor = false;
            // 
            // openStaffFileDialog
            // 
            this.openStaffFileDialog.FileName = "openStaffFileDialog";
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.panel3.Controls.Add(this.btnSaveNClose);
            this.panel3.Controls.Add(this.btnDownloadImg);
            this.panel3.Controls.Add(this.btnRemoveImg);
            this.panel3.Controls.Add(this.btnRotateImage);
            this.panel3.Controls.Add(this.btnRegImg);
            this.panel3.Controls.Add(this.pic_scan);
            this.panel3.Controls.Add(this.lbDevices);
            this.panel3.Controls.Add(this.btnScanImg);
            this.panel3.Controls.Add(this.tbDevices);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(755, 609);
            this.panel3.TabIndex = 75;
            // 
            // btnRegImg
            // 
            this.btnRegImg.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnRegImg.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRegImg.Location = new System.Drawing.Point(509, 327);
            this.btnRegImg.Name = "btnRegImg";
            this.btnRegImg.Size = new System.Drawing.Size(224, 40);
            this.btnRegImg.TabIndex = 99;
            this.btnRegImg.Text = "이미지 찾기";
            this.btnRegImg.UseVisualStyleBackColor = false;
            // 
            // pic_scan
            // 
            this.pic_scan.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pic_scan.Location = new System.Drawing.Point(10, 42);
            this.pic_scan.Name = "pic_scan";
            this.pic_scan.Size = new System.Drawing.Size(480, 554);
            this.pic_scan.TabIndex = 94;
            this.pic_scan.TabStop = false;
            // 
            // lbDevices
            // 
            this.lbDevices.AutoSize = true;
            this.lbDevices.Font = new System.Drawing.Font("나눔고딕", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbDevices.Location = new System.Drawing.Point(509, 200);
            this.lbDevices.Name = "lbDevices";
            this.lbDevices.Size = new System.Drawing.Size(117, 16);
            this.lbDevices.TabIndex = 96;
            this.lbDevices.Text = "Scan Devices List";
            // 
            // btnScanImg
            // 
            this.btnScanImg.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnScanImg.Location = new System.Drawing.Point(509, 270);
            this.btnScanImg.Name = "btnScanImg";
            this.btnScanImg.Size = new System.Drawing.Size(224, 40);
            this.btnScanImg.TabIndex = 95;
            this.btnScanImg.Text = "이미지 스캔하기";
            this.btnScanImg.UseVisualStyleBackColor = false;
            // 
            // tbDevices
            // 
            this.tbDevices.FormattingEnabled = true;
            this.tbDevices.ItemHeight = 12;
            this.tbDevices.Location = new System.Drawing.Point(509, 226);
            this.tbDevices.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tbDevices.Name = "tbDevices";
            this.tbDevices.Size = new System.Drawing.Size(224, 28);
            this.tbDevices.TabIndex = 95;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 609);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(755, 39);
            this.panel2.TabIndex = 76;
            // 
            // btnDownloadImg
            // 
            this.btnDownloadImg.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnDownloadImg.Location = new System.Drawing.Point(509, 492);
            this.btnDownloadImg.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnDownloadImg.Name = "btnDownloadImg";
            this.btnDownloadImg.Size = new System.Drawing.Size(224, 40);
            this.btnDownloadImg.TabIndex = 98;
            this.btnDownloadImg.Text = "이미지 내려받기";
            this.btnDownloadImg.UseVisualStyleBackColor = false;
            // 
            // btnRotateImage
            // 
            this.btnRotateImage.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnRotateImage.Location = new System.Drawing.Point(509, 382);
            this.btnRotateImage.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnRotateImage.Name = "btnRotateImage";
            this.btnRotateImage.Size = new System.Drawing.Size(224, 40);
            this.btnRotateImage.TabIndex = 97;
            this.btnRotateImage.Text = "이미지 회전하기";
            this.btnRotateImage.UseVisualStyleBackColor = false;
            // 
            // LabelImg
            // 
            this.LabelImg.AutoSize = true;
            this.LabelImg.Font = new System.Drawing.Font("나눔고딕 ExtraBold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.LabelImg.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.LabelImg.Location = new System.Drawing.Point(12, 9);
            this.LabelImg.Name = "LabelImg";
            this.LabelImg.Size = new System.Drawing.Size(82, 17);
            this.LabelImg.TabIndex = 0;
            this.LabelImg.Text = "이미지 추가";
            // 
            // btnRemoveImg
            // 
            this.btnRemoveImg.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnRemoveImg.Location = new System.Drawing.Point(509, 437);
            this.btnRemoveImg.Name = "btnRemoveImg";
            this.btnRemoveImg.Size = new System.Drawing.Size(224, 40);
            this.btnRemoveImg.TabIndex = 96;
            this.btnRemoveImg.Text = "이미지 삭제하기";
            this.btnRemoveImg.UseVisualStyleBackColor = false;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.panel1.Controls.Add(this.LabelImg);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(755, 36);
            this.panel1.TabIndex = 0;
            // 
            // ScanIMGFilePopup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(755, 648);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Name = "ScanIMGFilePopup";
            this.Text = "이미지 추가";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.RegIMGFilePopup_FormClosing);
            this.Load += new System.EventHandler(this.ScanIMGFilePopup_Load);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pic_scan)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnSaveNClose;
        private System.Windows.Forms.OpenFileDialog openStaffFileDialog;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.PictureBox pic_scan;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label lbDevices;
        private System.Windows.Forms.ListBox tbDevices;
        private System.Windows.Forms.Label LabelImg;
        private System.Windows.Forms.Button btnScanImg;
        private System.Windows.Forms.Button btnRemoveImg;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnRotateImage;
        private System.Windows.Forms.Button btnDownloadImg;
        private System.Windows.Forms.Button btnRegImg;
    }
}