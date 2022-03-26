namespace SmartViceDev
{
    partial class ShowVCanlendar
    {
        /// <summary> 
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 구성 요소 디자이너에서 생성한 코드

        /// <summary> 
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ShowVCanlendar));
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnSaveNClose = new System.Windows.Forms.Button();
            this.btnReset = new System.Windows.Forms.Button();
            this.lstLeaveDays = new System.Windows.Forms.ListBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.jetCalendar = new JETCalender.JETCalendar();
            this.SuspendLayout();
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(418, 176);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(149, 33);
            this.btnDelete.TabIndex = 2;
            this.btnDelete.Text = "삭제";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btn_Click);
            // 
            // btnSaveNClose
            // 
            this.btnSaveNClose.Location = new System.Drawing.Point(418, 254);
            this.btnSaveNClose.Name = "btnSaveNClose";
            this.btnSaveNClose.Size = new System.Drawing.Size(149, 33);
            this.btnSaveNClose.TabIndex = 3;
            this.btnSaveNClose.Text = "저장 및 닫기";
            this.btnSaveNClose.UseVisualStyleBackColor = true;
            this.btnSaveNClose.Click += new System.EventHandler(this.btn_Click);
            // 
            // btnReset
            // 
            this.btnReset.Location = new System.Drawing.Point(418, 215);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(149, 33);
            this.btnReset.TabIndex = 4;
            this.btnReset.Text = "초기화";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btn_Click);
            // 
            // lstLeaveDays
            // 
            this.lstLeaveDays.FormattingEnabled = true;
            this.lstLeaveDays.ItemHeight = 12;
            this.lstLeaveDays.Location = new System.Drawing.Point(418, 7);
            this.lstLeaveDays.Name = "lstLeaveDays";
            this.lstLeaveDays.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lstLeaveDays.Size = new System.Drawing.Size(152, 124);
            this.lstLeaveDays.Sorted = true;
            this.lstLeaveDays.TabIndex = 5;
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(418, 137);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(149, 33);
            this.btnAdd.TabIndex = 1;
            this.btnAdd.Text = "추가";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btn_Click);
            // 
            // jetCalendar
            // 
            this.jetCalendar.BlackoutDates = ((System.Collections.Generic.List<System.DateTime>)(resources.GetObject("jetCalendar.BlackoutDates")));
            this.jetCalendar.Location = new System.Drawing.Point(12, 7);
            this.jetCalendar.Name = "jetCalendar";
            this.jetCalendar.SelectedDate = new System.DateTime(((long)(0)));
            this.jetCalendar.SelectedDates = ((System.Collections.Generic.List<System.DateTime>)(resources.GetObject("jetCalendar.SelectedDates")));
            this.jetCalendar.Size = new System.Drawing.Size(400, 280);
            this.jetCalendar.TabIndex = 6;
            // 
            // ShowVCanlendar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(576, 296);
            this.Controls.Add(this.jetCalendar);
            this.Controls.Add(this.lstLeaveDays);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.btnSaveNClose);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnAdd);
            this.Name = "ShowVCanlendar";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ShowVCanlendar_FormClosing);
            this.Load += new System.EventHandler(this.ShowVCanlendar_Load);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnSaveNClose;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.ListBox lstLeaveDays;
        private System.Windows.Forms.Button btnAdd;
        private JETCalender.JETCalendar jetCalendar;
    }
}
