namespace SmartViceDev
{
    partial class ChargeFoodManagement
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.grdChgFood = new System.Windows.Forms.DataGridView();
            this.panel2 = new System.Windows.Forms.Panel();
            this.rbtnShowByMonth = new System.Windows.Forms.RadioButton();
            this.rbtnShowbyDay = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.EndDate = new System.Windows.Forms.DateTimePicker();
            this.LsearchDate = new System.Windows.Forms.Label();
            this.StartDate = new System.Windows.Forms.DateTimePicker();
            this.btnExcelExport = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnSearch = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdChgFood)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.grdChgFood);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 73);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(914, 540);
            this.panel1.TabIndex = 1;
            // 
            // grdChgFood
            // 
            this.grdChgFood.BackgroundColor = System.Drawing.SystemColors.Control;
            this.grdChgFood.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdChgFood.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdChgFood.Location = new System.Drawing.Point(0, 0);
            this.grdChgFood.Name = "grdChgFood";
            this.grdChgFood.RowHeadersWidth = 51;
            this.grdChgFood.RowTemplate.Height = 21;
            this.grdChgFood.Size = new System.Drawing.Size(914, 540);
            this.grdChgFood.TabIndex = 0;
            this.grdChgFood.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grdChgFood_CellDoubleClick);
            // 
            // panel2
            // 
            this.panel2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panel2.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.panel2.Controls.Add(this.rbtnShowByMonth);
            this.panel2.Controls.Add(this.rbtnShowbyDay);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.EndDate);
            this.panel2.Controls.Add(this.LsearchDate);
            this.panel2.Controls.Add(this.StartDate);
            this.panel2.Controls.Add(this.btnExcelExport);
            this.panel2.Controls.Add(this.btnSave);
            this.panel2.Controls.Add(this.btnDelete);
            this.panel2.Controls.Add(this.btnAdd);
            this.panel2.Controls.Add(this.btnSearch);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(914, 73);
            this.panel2.TabIndex = 2;
            // 
            // rbtnShowByMonth
            // 
            this.rbtnShowByMonth.AutoSize = true;
            this.rbtnShowByMonth.Location = new System.Drawing.Point(91, 44);
            this.rbtnShowByMonth.Name = "rbtnShowByMonth";
            this.rbtnShowByMonth.Size = new System.Drawing.Size(69, 17);
            this.rbtnShowByMonth.TabIndex = 18;
            this.rbtnShowByMonth.TabStop = true;
            this.rbtnShowByMonth.Text = "월별조회";
            this.rbtnShowByMonth.UseVisualStyleBackColor = true;
            // 
            // rbtnShowbyDay
            // 
            this.rbtnShowbyDay.AutoSize = true;
            this.rbtnShowbyDay.Checked = true;
            this.rbtnShowbyDay.Location = new System.Drawing.Point(166, 44);
            this.rbtnShowbyDay.Name = "rbtnShowbyDay";
            this.rbtnShowbyDay.Size = new System.Drawing.Size(69, 17);
            this.rbtnShowbyDay.TabIndex = 17;
            this.rbtnShowbyDay.TabStop = true;
            this.rbtnShowbyDay.Text = "일자조회";
            this.rbtnShowbyDay.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("나눔고딕", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label1.Location = new System.Drawing.Point(245, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(18, 17);
            this.label1.TabIndex = 16;
            this.label1.Text = "~";
            // 
            // EndDate
            // 
            this.EndDate.CalendarFont = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.EndDate.Location = new System.Drawing.Point(265, 16);
            this.EndDate.Name = "EndDate";
            this.EndDate.Size = new System.Drawing.Size(156, 20);
            this.EndDate.TabIndex = 15;
            // 
            // LsearchDate
            // 
            this.LsearchDate.AutoSize = true;
            this.LsearchDate.Font = new System.Drawing.Font("나눔고딕", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.LsearchDate.Location = new System.Drawing.Point(12, 16);
            this.LsearchDate.Name = "LsearchDate";
            this.LsearchDate.Size = new System.Drawing.Size(68, 17);
            this.LsearchDate.TabIndex = 14;
            this.LsearchDate.Text = "조회 기간";
            // 
            // StartDate
            // 
            this.StartDate.CalendarFont = new System.Drawing.Font("맑은 고딕", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.StartDate.Location = new System.Drawing.Point(90, 16);
            this.StartDate.Name = "StartDate";
            this.StartDate.Size = new System.Drawing.Size(154, 20);
            this.StartDate.TabIndex = 13;
            // 
            // btnExcelExport
            // 
            this.btnExcelExport.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnExcelExport.BackColor = System.Drawing.Color.MistyRose;
            this.btnExcelExport.Location = new System.Drawing.Point(427, 7);
            this.btnExcelExport.Name = "btnExcelExport";
            this.btnExcelExport.Size = new System.Drawing.Size(94, 30);
            this.btnExcelExport.TabIndex = 12;
            this.btnExcelExport.Text = "엑셀 Export";
            this.btnExcelExport.UseVisualStyleBackColor = false;
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnSave.Location = new System.Drawing.Point(808, 37);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(94, 30);
            this.btnSave.TabIndex = 3;
            this.btnSave.Text = "저장";
            this.btnSave.UseVisualStyleBackColor = false;
            // 
            // btnDelete
            // 
            this.btnDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDelete.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnDelete.Location = new System.Drawing.Point(708, 37);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(94, 30);
            this.btnDelete.TabIndex = 2;
            this.btnDelete.Text = "삭제";
            this.btnDelete.UseVisualStyleBackColor = false;
            // 
            // btnAdd
            // 
            this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAdd.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnAdd.Location = new System.Drawing.Point(808, 6);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(94, 30);
            this.btnAdd.TabIndex = 1;
            this.btnAdd.Text = "추가";
            this.btnAdd.UseVisualStyleBackColor = false;
            // 
            // btnSearch
            // 
            this.btnSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSearch.AutoEllipsis = true;
            this.btnSearch.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnSearch.Location = new System.Drawing.Point(708, 6);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(94, 30);
            this.btnSearch.TabIndex = 0;
            this.btnSearch.Text = "조회";
            this.btnSearch.UseVisualStyleBackColor = false;
            // 
            // ChargeFoodManagement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(914, 613);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Name = "ChargeFoodManagement";
            this.Text = "중식관리대장";
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdChgFood)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.DataGridView grdChgFood;
        private System.Windows.Forms.Button btnExcelExport;
        private System.Windows.Forms.Label LsearchDate;
        private System.Windows.Forms.DateTimePicker StartDate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker EndDate;
        private System.Windows.Forms.RadioButton rbtnShowByMonth;
        private System.Windows.Forms.RadioButton rbtnShowbyDay;
    }
}
