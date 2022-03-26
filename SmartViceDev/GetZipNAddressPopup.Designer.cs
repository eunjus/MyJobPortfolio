namespace SmartViceDev
{
    partial class GetZipNAddressPopup
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel1 = new System.Windows.Forms.Panel();
            this.LabelImg = new System.Windows.Forms.Label();
            this.btnSearch = new System.Windows.Forms.Button();
            this.btnSaveNClose = new System.Windows.Forms.Button();
            this.openStaffFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.grdPageInfos = new System.Windows.Forms.DataGridView();
            this.grdAddress = new System.Windows.Forms.DataGridView();
            this.panel4 = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lbCntDt = new System.Windows.Forms.Label();
            this.tbRoadNm = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdPageInfos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdAddress)).BeginInit();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.panel1.Controls.Add(this.LabelImg);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(749, 36);
            this.panel1.TabIndex = 0;
            // 
            // LabelImg
            // 
            this.LabelImg.AutoSize = true;
            this.LabelImg.Font = new System.Drawing.Font("나눔고딕 ExtraBold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.LabelImg.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.LabelImg.Location = new System.Drawing.Point(12, 9);
            this.LabelImg.Name = "LabelImg";
            this.LabelImg.Size = new System.Drawing.Size(92, 17);
            this.LabelImg.TabIndex = 0;
            this.LabelImg.Text = "우편번호검색";
            // 
            // btnSearch
            // 
            this.btnSearch.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.btnSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSearch.Location = new System.Drawing.Point(565, 9);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(173, 35);
            this.btnSearch.TabIndex = 95;
            this.btnSearch.Text = "검색";
            this.btnSearch.UseVisualStyleBackColor = false;
            // 
            // btnSaveNClose
            // 
            this.btnSaveNClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSaveNClose.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnSaveNClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSaveNClose.Location = new System.Drawing.Point(565, 3);
            this.btnSaveNClose.Name = "btnSaveNClose";
            this.btnSaveNClose.Size = new System.Drawing.Size(173, 33);
            this.btnSaveNClose.TabIndex = 7;
            this.btnSaveNClose.Text = "선택 및 닫기";
            this.btnSaveNClose.UseVisualStyleBackColor = false;
            this.btnSaveNClose.Visible = false;
            // 
            // openStaffFileDialog
            // 
            this.openStaffFileDialog.FileName = "openStaffFileDialog";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.panel2.Controls.Add(this.btnSaveNClose);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 608);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(749, 39);
            this.panel2.TabIndex = 76;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.panel5);
            this.panel3.Controls.Add(this.panel4);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 36);
            this.panel3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(749, 572);
            this.panel3.TabIndex = 77;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.grdPageInfos);
            this.panel5.Controls.Add(this.grdAddress);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel5.Location = new System.Drawing.Point(0, 113);
            this.panel5.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(749, 459);
            this.panel5.TabIndex = 1;
            // 
            // grdPageInfos
            // 
            this.grdPageInfos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdPageInfos.Location = new System.Drawing.Point(257, 66);
            this.grdPageInfos.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.grdPageInfos.Name = "grdPageInfos";
            this.grdPageInfos.RowHeadersWidth = 51;
            this.grdPageInfos.RowTemplate.Height = 27;
            this.grdPageInfos.Size = new System.Drawing.Size(210, 120);
            this.grdPageInfos.TabIndex = 1;
            this.grdPageInfos.Visible = false;
            // 
            // grdAddress
            // 
            this.grdAddress.AllowUserToAddRows = false;
            this.grdAddress.AllowUserToDeleteRows = false;
            this.grdAddress.BackgroundColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grdAddress.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.grdAddress.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdAddress.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdAddress.Location = new System.Drawing.Point(0, 0);
            this.grdAddress.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.grdAddress.Name = "grdAddress";
            this.grdAddress.ReadOnly = true;
            this.grdAddress.RowHeadersWidth = 51;
            this.grdAddress.RowTemplate.Height = 27;
            this.grdAddress.Size = new System.Drawing.Size(749, 459);
            this.grdAddress.TabIndex = 0;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.panel4.Controls.Add(this.label5);
            this.panel4.Controls.Add(this.label4);
            this.panel4.Controls.Add(this.label3);
            this.panel4.Controls.Add(this.label2);
            this.panel4.Controls.Add(this.lbCntDt);
            this.panel4.Controls.Add(this.btnSearch);
            this.panel4.Controls.Add(this.tbRoadNm);
            this.panel4.Controls.Add(this.label1);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(749, 113);
            this.panel4.TabIndex = 0;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("나눔고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label5.Location = new System.Drawing.Point(139, 89);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(207, 14);
            this.label5.TabIndex = 100;
            this.label5.Text = "* 건물명, 아파트명 (예: 반포자이아파트)";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("나눔고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label4.Location = new System.Drawing.Point(139, 66);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(199, 14);
            this.label4.TabIndex = 99;
            this.label4.Text = "* 동/읍/면/리 + 번지 (예: 신천동7-30)";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("나눔고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label3.Location = new System.Drawing.Point(139, 44);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(202, 14);
            this.label3.TabIndex = 98;
            this.label3.Text = "* 도로명 + 건물번호 (예: 송파대로570)";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("나눔고딕", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label2.Location = new System.Drawing.Point(10, 44);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(118, 14);
            this.label2.TabIndex = 97;
            this.label2.Text = "우편번호 통합검색 Tip";
            // 
            // lbCntDt
            // 
            this.lbCntDt.AutoSize = true;
            this.lbCntDt.Font = new System.Drawing.Font("나눔고딕", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbCntDt.Location = new System.Drawing.Point(417, 113);
            this.lbCntDt.Name = "lbCntDt";
            this.lbCntDt.Size = new System.Drawing.Size(0, 16);
            this.lbCntDt.TabIndex = 96;
            // 
            // tbRoadNm
            // 
            this.tbRoadNm.Location = new System.Drawing.Point(67, 14);
            this.tbRoadNm.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tbRoadNm.Name = "tbRoadNm";
            this.tbRoadNm.Size = new System.Drawing.Size(492, 21);
            this.tbRoadNm.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("나눔고딕", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 16);
            this.label1.TabIndex = 5;
            this.label1.Text = "도로명";
            // 
            // GetZipNAddressPopup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(749, 647);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "GetZipNAddressPopup";
            this.Text = "우편번호검색";
            this.Load += new System.EventHandler(this.GetZipNAddressPopup_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdPageInfos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdAddress)).EndInit();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label LabelImg;
        private System.Windows.Forms.Button btnSaveNClose;
        private System.Windows.Forms.OpenFileDialog openStaffFileDialog;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.DataGridView grdAddress;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.TextBox tbRoadNm;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbCntDt;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView grdPageInfos;
    }
}