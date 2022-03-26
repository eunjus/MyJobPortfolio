namespace SmartViceDev
{
    partial class SendMailPopup
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
            this.btnCancle = new System.Windows.Forms.Button();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.panel3 = new System.Windows.Forms.Panel();
            this.lstAttachFiles = new System.Windows.Forms.ListBox();
            this.btnRemoveFile = new System.Windows.Forms.Button();
            this.btnGetFile = new System.Windows.Forms.Button();
            this.tbAttachment = new System.Windows.Forms.TextBox();
            this.lbattachment = new System.Windows.Forms.Label();
            this.tbContent = new System.Windows.Forms.TextBox();
            this.lbContent = new System.Windows.Forms.Label();
            this.tbtitle = new System.Windows.Forms.TextBox();
            this.lbTitle = new System.Windows.Forms.Label();
            this.btnAddrBook = new System.Windows.Forms.Button();
            this.tbRecipientAddr = new System.Windows.Forms.TextBox();
            this.lbRecipientAddr = new System.Windows.Forms.Label();
            this.tbSenderAddr = new System.Windows.Forms.TextBox();
            this.lbSenderAddr = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnSend = new System.Windows.Forms.Button();
            this.LabelImg = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCancle
            // 
            this.btnCancle.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnCancle.Location = new System.Drawing.Point(441, 3);
            this.btnCancle.Name = "btnCancle";
            this.btnCancle.Size = new System.Drawing.Size(132, 33);
            this.btnCancle.TabIndex = 7;
            this.btnCancle.Text = "닫기";
            this.btnCancle.UseVisualStyleBackColor = false;
            this.btnCancle.Click += new System.EventHandler(this.btnCancle_Click);
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "openFileDialog";
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.panel3.Controls.Add(this.lstAttachFiles);
            this.panel3.Controls.Add(this.btnRemoveFile);
            this.panel3.Controls.Add(this.btnGetFile);
            this.panel3.Controls.Add(this.tbAttachment);
            this.panel3.Controls.Add(this.lbattachment);
            this.panel3.Controls.Add(this.tbContent);
            this.panel3.Controls.Add(this.lbContent);
            this.panel3.Controls.Add(this.tbtitle);
            this.panel3.Controls.Add(this.lbTitle);
            this.panel3.Controls.Add(this.btnAddrBook);
            this.panel3.Controls.Add(this.tbRecipientAddr);
            this.panel3.Controls.Add(this.lbRecipientAddr);
            this.panel3.Controls.Add(this.tbSenderAddr);
            this.panel3.Controls.Add(this.lbSenderAddr);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(576, 475);
            this.panel3.TabIndex = 75;
            // 
            // lstAttachFiles
            // 
            this.lstAttachFiles.FormattingEnabled = true;
            this.lstAttachFiles.ItemHeight = 12;
            this.lstAttachFiles.Location = new System.Drawing.Point(82, 397);
            this.lstAttachFiles.Name = "lstAttachFiles";
            this.lstAttachFiles.Size = new System.Drawing.Size(401, 64);
            this.lstAttachFiles.TabIndex = 114;
            // 
            // btnRemoveFile
            // 
            this.btnRemoveFile.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRemoveFile.Location = new System.Drawing.Point(489, 398);
            this.btnRemoveFile.Name = "btnRemoveFile";
            this.btnRemoveFile.Size = new System.Drawing.Size(75, 30);
            this.btnRemoveFile.TabIndex = 112;
            this.btnRemoveFile.Text = "삭제";
            this.btnRemoveFile.UseVisualStyleBackColor = true;
            this.btnRemoveFile.Click += new System.EventHandler(this.btnRemoveFile_Click);
            // 
            // btnGetFile
            // 
            this.btnGetFile.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGetFile.Location = new System.Drawing.Point(489, 368);
            this.btnGetFile.Name = "btnGetFile";
            this.btnGetFile.Size = new System.Drawing.Size(75, 24);
            this.btnGetFile.TabIndex = 110;
            this.btnGetFile.Text = "파일 찾기";
            this.btnGetFile.UseVisualStyleBackColor = true;
            // 
            // tbAttachment
            // 
            this.tbAttachment.Font = new System.Drawing.Font("나눔고딕", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.tbAttachment.Location = new System.Drawing.Point(82, 369);
            this.tbAttachment.Name = "tbAttachment";
            this.tbAttachment.Size = new System.Drawing.Size(401, 22);
            this.tbAttachment.TabIndex = 109;
            // 
            // lbattachment
            // 
            this.lbattachment.AutoSize = true;
            this.lbattachment.Font = new System.Drawing.Font("나눔고딕 ExtraBold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbattachment.Location = new System.Drawing.Point(12, 371);
            this.lbattachment.Name = "lbattachment";
            this.lbattachment.Size = new System.Drawing.Size(55, 15);
            this.lbattachment.TabIndex = 108;
            this.lbattachment.Text = "첨부파일";
            // 
            // tbContent
            // 
            this.tbContent.Font = new System.Drawing.Font("나눔고딕", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.tbContent.Location = new System.Drawing.Point(82, 172);
            this.tbContent.Multiline = true;
            this.tbContent.Name = "tbContent";
            this.tbContent.Size = new System.Drawing.Size(482, 180);
            this.tbContent.TabIndex = 107;
            // 
            // lbContent
            // 
            this.lbContent.AutoSize = true;
            this.lbContent.Font = new System.Drawing.Font("나눔고딕 ExtraBold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbContent.Location = new System.Drawing.Point(23, 253);
            this.lbContent.Name = "lbContent";
            this.lbContent.Size = new System.Drawing.Size(31, 15);
            this.lbContent.TabIndex = 106;
            this.lbContent.Text = "내용";
            // 
            // tbtitle
            // 
            this.tbtitle.Font = new System.Drawing.Font("나눔고딕", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.tbtitle.Location = new System.Drawing.Point(82, 129);
            this.tbtitle.Name = "tbtitle";
            this.tbtitle.Size = new System.Drawing.Size(482, 22);
            this.tbtitle.TabIndex = 105;
            // 
            // lbTitle
            // 
            this.lbTitle.AutoSize = true;
            this.lbTitle.Font = new System.Drawing.Font("나눔고딕 ExtraBold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbTitle.Location = new System.Drawing.Point(23, 131);
            this.lbTitle.Name = "lbTitle";
            this.lbTitle.Size = new System.Drawing.Size(31, 15);
            this.lbTitle.TabIndex = 104;
            this.lbTitle.Text = "제목";
            // 
            // btnAddrBook
            // 
            this.btnAddrBook.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.999999F, System.Drawing.FontStyle.Bold);
            this.btnAddrBook.Location = new System.Drawing.Point(489, 82);
            this.btnAddrBook.Name = "btnAddrBook";
            this.btnAddrBook.Size = new System.Drawing.Size(75, 30);
            this.btnAddrBook.TabIndex = 103;
            this.btnAddrBook.Text = "주소록";
            this.btnAddrBook.UseVisualStyleBackColor = true;
            this.btnAddrBook.Click += new System.EventHandler(this.btnAddrBook_Click);
            // 
            // tbRecipientAddr
            // 
            this.tbRecipientAddr.Font = new System.Drawing.Font("나눔고딕", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.tbRecipientAddr.Location = new System.Drawing.Point(82, 87);
            this.tbRecipientAddr.Multiline = true;
            this.tbRecipientAddr.Name = "tbRecipientAddr";
            this.tbRecipientAddr.Size = new System.Drawing.Size(401, 21);
            this.tbRecipientAddr.TabIndex = 102;
            // 
            // lbRecipientAddr
            // 
            this.lbRecipientAddr.AutoSize = true;
            this.lbRecipientAddr.Font = new System.Drawing.Font("나눔고딕 ExtraBold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbRecipientAddr.Location = new System.Drawing.Point(12, 89);
            this.lbRecipientAddr.Name = "lbRecipientAddr";
            this.lbRecipientAddr.Size = new System.Drawing.Size(59, 15);
            this.lbRecipientAddr.TabIndex = 101;
            this.lbRecipientAddr.Text = "받는 사람";
            // 
            // tbSenderAddr
            // 
            this.tbSenderAddr.Font = new System.Drawing.Font("나눔고딕", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.tbSenderAddr.Location = new System.Drawing.Point(82, 50);
            this.tbSenderAddr.Name = "tbSenderAddr";
            this.tbSenderAddr.Size = new System.Drawing.Size(401, 22);
            this.tbSenderAddr.TabIndex = 100;
            this.tbSenderAddr.Text = "jetspurt@jetspurt.com";
            // 
            // lbSenderAddr
            // 
            this.lbSenderAddr.AutoSize = true;
            this.lbSenderAddr.Font = new System.Drawing.Font("나눔고딕 ExtraBold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbSenderAddr.Location = new System.Drawing.Point(12, 52);
            this.lbSenderAddr.Name = "lbSenderAddr";
            this.lbSenderAddr.Size = new System.Drawing.Size(59, 15);
            this.lbSenderAddr.TabIndex = 96;
            this.lbSenderAddr.Text = "보낸 사람";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.panel2.Controls.Add(this.btnSend);
            this.panel2.Controls.Add(this.btnCancle);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 475);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(576, 39);
            this.panel2.TabIndex = 76;
            // 
            // btnSend
            // 
            this.btnSend.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnSend.Location = new System.Drawing.Point(303, 3);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(132, 33);
            this.btnSend.TabIndex = 8;
            this.btnSend.Text = "보내기";
            this.btnSend.UseVisualStyleBackColor = false;
            // 
            // LabelImg
            // 
            this.LabelImg.AutoSize = true;
            this.LabelImg.Font = new System.Drawing.Font("나눔고딕 ExtraBold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.LabelImg.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.LabelImg.Location = new System.Drawing.Point(12, 9);
            this.LabelImg.Name = "LabelImg";
            this.LabelImg.Size = new System.Drawing.Size(68, 17);
            this.LabelImg.TabIndex = 0;
            this.LabelImg.Text = "메일 쓰기";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.panel1.Controls.Add(this.LabelImg);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(576, 36);
            this.panel1.TabIndex = 0;
            // 
            // SendMailPopup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(576, 514);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Name = "SendMailPopup";
            this.Text = "메일 전송";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.RegIMGFilePopup_FormClosing);
            this.Load += new System.EventHandler(this.SendMailPopup_Load);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnCancle;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label lbSenderAddr;
        private System.Windows.Forms.Label LabelImg;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox tbRecipientAddr;
        private System.Windows.Forms.Label lbRecipientAddr;
        private System.Windows.Forms.TextBox tbSenderAddr;
        private System.Windows.Forms.Button btnAddrBook;
        private System.Windows.Forms.TextBox tbContent;
        private System.Windows.Forms.Label lbContent;
        private System.Windows.Forms.TextBox tbtitle;
        private System.Windows.Forms.Label lbTitle;
        private System.Windows.Forms.TextBox tbAttachment;
        private System.Windows.Forms.Label lbattachment;
        private System.Windows.Forms.Button btnGetFile;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.Button btnRemoveFile;
        private System.Windows.Forms.ListBox lstAttachFiles;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
    }
}