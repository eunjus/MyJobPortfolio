namespace SmartViceDev
{
    partial class GetIMGFilePopup
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnRemoveImg = new System.Windows.Forms.Button();
            this.btnRegImg = new System.Windows.Forms.Button();
            this.LabelImg = new System.Windows.Forms.Label();
            this.btnSaveNClose = new System.Windows.Forms.Button();
            this.openStaffFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.panel3 = new System.Windows.Forms.Panel();
            this.Picturebox = new System.Windows.Forms.PictureBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Picturebox)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.panel1.Controls.Add(this.btnRemoveImg);
            this.panel1.Controls.Add(this.btnRegImg);
            this.panel1.Controls.Add(this.LabelImg);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(546, 45);
            this.panel1.TabIndex = 0;
            // 
            // btnRemoveImg
            // 
            this.btnRemoveImg.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnRemoveImg.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRemoveImg.Location = new System.Drawing.Point(437, 4);
            this.btnRemoveImg.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnRemoveImg.Name = "btnRemoveImg";
            this.btnRemoveImg.Size = new System.Drawing.Size(106, 38);
            this.btnRemoveImg.TabIndex = 96;
            this.btnRemoveImg.Text = "이미지 삭제";
            this.btnRemoveImg.UseVisualStyleBackColor = false;
            // 
            // btnRegImg
            // 
            this.btnRegImg.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnRegImg.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRegImg.Location = new System.Drawing.Point(328, 4);
            this.btnRegImg.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnRegImg.Name = "btnRegImg";
            this.btnRegImg.Size = new System.Drawing.Size(106, 38);
            this.btnRegImg.TabIndex = 95;
            this.btnRegImg.Text = "이미지 등록";
            this.btnRegImg.UseVisualStyleBackColor = false;
            // 
            // LabelImg
            // 
            this.LabelImg.AutoSize = true;
            this.LabelImg.Font = new System.Drawing.Font("나눔고딕 ExtraBold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.LabelImg.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.LabelImg.Location = new System.Drawing.Point(14, 11);
            this.LabelImg.Name = "LabelImg";
            this.LabelImg.Size = new System.Drawing.Size(105, 21);
            this.LabelImg.TabIndex = 0;
            this.LabelImg.Text = "이미지 추가";
            // 
            // btnSaveNClose
            // 
            this.btnSaveNClose.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnSaveNClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.btnSaveNClose.Location = new System.Drawing.Point(414, 4);
            this.btnSaveNClose.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnSaveNClose.Name = "btnSaveNClose";
            this.btnSaveNClose.Size = new System.Drawing.Size(122, 41);
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
            this.panel3.Controls.Add(this.Picturebox);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 45);
            this.panel3.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(546, 715);
            this.panel3.TabIndex = 75;
            // 
            // Picturebox
            // 
            this.Picturebox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Picturebox.Location = new System.Drawing.Point(10, 16);
            this.Picturebox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Picturebox.Name = "Picturebox";
            this.Picturebox.Size = new System.Drawing.Size(525, 681);
            this.Picturebox.TabIndex = 94;
            this.Picturebox.TabStop = false;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.panel2.Controls.Add(this.btnSaveNClose);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 760);
            this.panel2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(546, 49);
            this.panel2.TabIndex = 76;
            // 
            // GetIMGFilePopup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(546, 809);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "GetIMGFilePopup";
            this.Text = "이미지 추가";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.RegIMGFilePopup_FormClosing);
            this.Load += new System.EventHandler(this.RegIMGFilePopup_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Picturebox)).EndInit();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label LabelImg;
        private System.Windows.Forms.Button btnSaveNClose;
        private System.Windows.Forms.OpenFileDialog openStaffFileDialog;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button btnRemoveImg;
        private System.Windows.Forms.Button btnRegImg;
        private System.Windows.Forms.PictureBox Picturebox;
        private System.Windows.Forms.Panel panel2;
    }
}