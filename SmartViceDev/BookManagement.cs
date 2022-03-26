using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using SmartViceDev.CubridUtils;
using System.Drawing;


namespace SmartViceDev
{
    public partial class BookManagement : Form
    {
        RULE_BOOKMANAGEMENT smartViceData = new RULE_BOOKMANAGEMENT();
        //조회조건 리스트
        Dictionary<string, string> MessageSet = new Dictionary<string, string>();
        DataTable dtForchk = new DataTable();
        bool chkSearched = true;
        CheckBox cb = new CheckBox();

        DateTimePicker Purchase_dtp = new DateTimePicker();
        Rectangle _Purchase_dtpRectangle = new Rectangle();

        DateTimePicker Return_dtp = new DateTimePicker();
        Rectangle _Return_dtpRectangle = new Rectangle();

        public BookManagement()
        {
            InitializeComponent();

            eventslist(); //이벤트 처리 함수
            grdColumnAdd(); // 그리드 컬럼 형성
            comboboxListAdd(); // 콤보박스 초기화
        }

        private void BookManagement_Load(object sender, EventArgs e)
        {                      
            //구매일자 컬럼에 캘린더 기능 추가 
            grdBook.Controls.Add(Purchase_dtp);
            Purchase_dtp.Visible = false;
            Purchase_dtp.Format = DateTimePickerFormat.Custom;
            Purchase_dtp.TextChanged += new EventHandler(Purchase_dtp_TextChange);
            //반납예정일 컬럼에 캘린더 기능 추가 
            grdBook.Controls.Add(Return_dtp);
            Return_dtp.Visible = false;
            Return_dtp.Format = DateTimePickerFormat.Custom;
            Return_dtp.TextChanged += new EventHandler(Return_dtp_TextChange);

            btnSearch_Click(null, null);

            this.UseWaitCursor = false;
        }

        private void eventslist()
        {            
            this.Shown += new System.EventHandler(this.BookManagement_Shown);

            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);

            
            this.grdBook.KeyUp += new System.Windows.Forms.KeyEventHandler(this.grdBook_KeyUp);
            this.grdBook.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.grdBook_KeyPress);
            this.grdBook.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.grdBook_EditingControlShowing);
            this.grdBook.Scroll += new System.Windows.Forms.ScrollEventHandler(this.grdBook_Scroll);
            // 그리드에 있는 콤보박스 형태의 열 값이 변경될 때 발생하는 이벤트
            this.grdBook.CurrentCellDirtyStateChanged += new System.EventHandler(this.grdBook_CurrentCellDirtyStateChanged);
            this.grdBook.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grdBook_CellClick);
            this.grdBook.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grdBook_CellDoubleClick);
            this.grdBook.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.grdBook_CellValueChanged);
            this.grdBook.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.grdBook_CellPainting);
            
        }
        private void comboboxListAdd()
        {
            smartViceData = new RULE_BOOKMANAGEMENT();
            DataTable ds = new DataTable();
            //사원명 콤보박스 리스트업 
            ds = smartViceData.SearchCommon("seachPublisher",MessageSet);

            this.cmbPublisher.Items.Add("전체");
            for (int row = 0; row < ds.Rows.Count; row++)
            {
                this.cmbPublisher.Items.Add(ds.Rows[row].Field<string>("publisher").ToString());
            }
            this.cmbPublisher.SelectedItem = "전체";

            ////직책명 콤보박스 리스트업 

            //ds = smartViceData.SearchCommon("seachPosition");

            //this.cmbPosition.Items.Add("전체");
            //for (int row = 0; row < ds.Tables[0].Rows.Count; row++)
            //{
            //    this.cmbPosition.Items.Add(ds.Tables[0].Rows[row].Field<string>("position_name").ToString());
            //}

            //this.cmbPosition.SelectedItem = "전체";
        }

        private void grdColumnAdd()
        {
            DataSet ds = new DataSet();
            RULE_BOOKMANAGEMENT smartViceData = new RULE_BOOKMANAGEMENT();

            var chkCol = new DataGridViewCheckBoxColumn
            {
                Name = "colCheck",
                HeaderText = ""
            };
            this.grdBook.Columns.Add(chkCol);
            this.grdBook.Columns["colCheck"].Width = 50;

            //유효 아이디
            this.grdBook.Columns.Add("valid_id", "유효 아이디");
            this.grdBook.Columns["valid_id"].Visible = false;
            //책 제목
            this.grdBook.Columns.Add("book_title", "책 제목");
            this.grdBook.Columns["book_title"].Width = 500;
            //출판사
            this.grdBook.Columns.Add("publisher", "출판사");
            //this.grdBook.Columns["publisher"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            //구매 일자
            this.grdBook.Columns.Add("purchase_dt", "구매 일자 (yyyy-mm-dd)");
            this.grdBook.Columns["purchase_dt"].Width = 200;
            //this.grdBook.Columns["publisher"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            //대출 현황
            DataGridViewComboBoxColumn cmb_borrow_situation = new DataGridViewComboBoxColumn();
            cmb_borrow_situation.Name = "cmb_borrow_situation";
            cmb_borrow_situation.HeaderText = "대출현황";
            cmb_borrow_situation.Items.Add("대출중");
            cmb_borrow_situation.Items.Add("대출가능");
            this.grdBook.Columns.Add(cmb_borrow_situation);
            //this.grdBook.Columns["publisher"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            ////대출자 ID
            //this.grdBook.Columns.Add("borrower_id", "대출자 ID");
            ////this.grdBook.Columns["publisher"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            DataTable dt = new DataTable();
            //대출자 ID
            dt = smartViceData.SearchCommon("searchStaffInfo", MessageSet);

            //대출자 명
            DataGridViewComboBoxColumn cmb_borrower_Nm = new DataGridViewComboBoxColumn();
            cmb_borrower_Nm.Name = "cmb_borrower_Nm";
            cmb_borrower_Nm.HeaderText = "대출자 명";
            for (int row = 0; row < dt.Rows.Count; row++)
            {
                cmb_borrower_Nm.Items.Add(dt.Rows[row]["staff_name"]);
            }
            //cmbStaffNm.Items.Add("cmbStaffNm");
            this.grdBook.Columns.Add(cmb_borrower_Nm);

            //대출자 ID
            DataGridViewComboBoxColumn cmb_borrower_Id = new DataGridViewComboBoxColumn();
            cmb_borrower_Id.Name = "cmb_borrower_Id";
            cmb_borrower_Id.HeaderText = "대출자 ID";
            cmb_borrower_Id.Width = 150;
            for (int row = 0; row < dt.Rows.Count; row++)
            {
                cmb_borrower_Id.Items.Add(dt.Rows[row]["staff_id"]);
            }
            //cmbStaffId.Items.Add("cmbStaffId");
            this.grdBook.Columns.Add(cmb_borrower_Id);


            //반납예정일자
            this.grdBook.Columns.Add("return_schedule_dt", "반납예정일자 (yyyy-mm-dd)");
            this.grdBook.Columns["return_schedule_dt"].Width = 200;
            //this.grdBook.Columns["publisher"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            //설명
            this.grdBook.Columns.Add("comment", "설명");
            this.grdBook.Columns["comment"].Width = 300;
            //등록 날짜
            this.grdBook.Columns.Add("reg_dt", "등록 날짜");
            this.grdBook.Columns["reg_dt"].ReadOnly = true;
            this.grdBook.Columns["reg_dt"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //수정 날짜
            this.grdBook.Columns.Add("update_dt", "수정 날짜");
            this.grdBook.Columns["update_dt"].ReadOnly = true;
            this.grdBook.Columns["update_dt"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //ROWSTATE
            this.grdBook.Columns.Add("ROWSTATE", "ROWSTATE");
            this.grdBook.Columns["ROWSTATE"].Visible = false;


        }
        private void Purchase_dtp_TextChange(object sender, EventArgs e)
        {            
            if(Purchase_dtp.Value != null)
                grdBook.CurrentCell.Value = Purchase_dtp.Value.ToShortDateString();           
        }
       
        private void Return_dtp_TextChange(object sender, EventArgs e)
        {
            if (Return_dtp.Text != null)
                grdBook.CurrentCell.Value = Return_dtp.Value.ToShortDateString();
        }       

        private void grdBook_Scroll(object sender, ScrollEventArgs e)
        {       
            Purchase_dtp.Visible = false;
            Return_dtp.Visible = false;
            cb.Visible = false;
        }
        private void grdBook_KeyUp(object sender, KeyEventArgs e)
        {
            //delete키 누르면 셀 내용 전체 삭제
            if (e.KeyCode == Keys.Delete)
            {
                this.grdBook.CurrentCell.Value = string.Empty;
            }
        }
        private void grdBook_KeyPress(object sender, KeyPressEventArgs e)
        {
            //숫자가 아닌 값이거나 백스페이스를 제외한 나머지를 바로 처리
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != '-' && e.KeyChar != '/' && e.KeyChar != Convert.ToChar(Keys.Back))
            {            
                e.Handled = true;
            }
        }
        private void grdBook_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
       {
            //구매일자 컬럼에 숫자와 '-' 또는 '/' 
            string name = grdBook.CurrentCell.OwningColumn.Name;

            if (name == "purchase_dt")
            {
                e.Control.KeyPress += new KeyPressEventHandler(grdBook_KeyPress);
            }
            else
            {
                e.Control.KeyPress -= new KeyPressEventHandler(grdBook_KeyPress);
            }
        }

        private void grdBook_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            switch (grdBook.Columns[e.ColumnIndex].Name)
            {
                case "colCheck":
                    if (this.grdBook.Rows[e.RowIndex].Cells["colCheck"].Value == null || this.grdBook.Rows[e.RowIndex].Cells["colCheck"].Value.ToString().ToUpper() == "false".ToUpper())
                    {
                        this.grdBook.CurrentRow.Selected = true;
                    }
                    break;  
                default:
                    Purchase_dtp.Visible = false;
                    Return_dtp.Visible = false;
                    break;
            }
        }

        private void grdBook_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            switch (grdBook.Columns[e.ColumnIndex].Name)
            {
                case "purchase_dt":

                    _Purchase_dtpRectangle = grdBook.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true);
                    Purchase_dtp.Size = new Size(_Purchase_dtpRectangle.Width, _Purchase_dtpRectangle.Height);
                    Purchase_dtp.Location = new Point(_Purchase_dtpRectangle.X, _Purchase_dtpRectangle.Y);
                    Purchase_dtp.Visible = true;
                    if (!this.grdBook.Rows[e.RowIndex].IsNewRow && this.grdBook.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null
                        && this.grdBook.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() != string.Empty)
                    {
                        Purchase_dtp.Value = Convert.ToDateTime(this.grdBook.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);
                    }
                    else
                        Purchase_dtp.Value = DateTime.Today;

                    break;
                case "return_schedule_dt":

                    _Return_dtpRectangle = grdBook.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true);
                    Return_dtp.Size = new Size(_Return_dtpRectangle.Width, _Return_dtpRectangle.Height);
                    Return_dtp.Location = new Point(_Return_dtpRectangle.X, _Return_dtpRectangle.Y);
                    Return_dtp.Visible = true;
                    if (!this.grdBook.Rows[e.RowIndex].IsNewRow && this.grdBook.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null
                        && this.grdBook.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() != string.Empty)
                    {
                        Return_dtp.Value = Convert.ToDateTime(this.grdBook.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);
                    }
                    else
                        Return_dtp.Value = DateTime.Today;

                    break;
                default:
                    Purchase_dtp.Visible = false;
                    Return_dtp.Visible = false;
                    break;
            }
        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
            chkSearched = true;

            int chkgrddt = Chksearch();

            if (chkgrddt == 1)
            {
                DialogResult dialogResult = MessageBox.Show("저장되지 않은 데이터가 존재합니다. 그래도 조회하시겠습니까?\n(수정한 데이터를 잃게됩니다.)", "Warning", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
                if (dialogResult == DialogResult.Yes)
                {
                    int resultchk = Search(MessageSet);

                    if (resultchk == -1)
                    {
                        MessageBox.Show("조회된 데이터가 없습니다.", "Inform", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.grdBook.Rows.Clear();
                        chkSearched = false;
                        return;
                    }
                }
                else if (dialogResult == DialogResult.Cancel || dialogResult == DialogResult.No)
                    return;
            }
         
            int resultchk2 = Search(MessageSet);

            if (resultchk2 == -1)
            {
                MessageBox.Show("조회된 데이터가 없습니다.", "Inform", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.grdBook.Rows.Clear();
                chkSearched = false;
                return;
            }
           
        }

        public int Chksearch()
        {
            if (chkSearched == false) // 조회한 이력이 있을때만 그리드 체크 
            {

                for (int grdrow = 0; grdrow < this.grdBook.Rows.Count; grdrow++)
                {
                    if (this.grdBook.Rows[grdrow].Cells["ROWSTATE"].Value != null)
                    {
                        if (this.grdBook.Rows[grdrow].Cells["ROWSTATE"].Value.ToString() == "Modified" || this.grdBook.Rows[grdrow].Cells["ROWSTATE"].Value.ToString() == "Added"
                            || this.grdBook.Rows[grdrow].Cells["ROWSTATE"].Value.ToString() == "Deleted")
                        {
                            return 1;
                        }
                    }
                }   
            }
            return 0;
        }

        public int Search(Dictionary<string, string> MessageSet)
        {
            //저장되지않은 변경된 데이터가 있는지 체크하는 함수 추가해줘!

            if (this.grdBook.Rows.Count > 1)
            {
                this.grdBook.Rows.Clear();
            }

            RULE_BOOKMANAGEMENT smartViceData = new RULE_BOOKMANAGEMENT();
            
            MessageSet.Clear();
            MessageSet.Add("book_title", this.tbBookNm.Text);
            MessageSet.Add("publisher", this.cmbPublisher.SelectedItem.ToString() == "전체" ? "" : this.cmbPublisher.SelectedItem.ToString());

            DataSet ds = new DataSet();
            ds = smartViceData.Search(MessageSet);

            DataTable dt = ds.Tables[0].Copy();

            if (dt.Rows.Count < 1)
                return -1;

            dtForchk = ds.Tables[0].Copy();

            if (this.grdBook.Rows.Count > 1)
                this.grdBook.Rows.Clear();

            for (int row = 0; row < dt.Rows.Count; row++)
            {
                this.grdBook.Rows.Add();
                this.grdBook["valid_id", row].Value = dt.Rows[row]["valid_id"].ToString();
                this.grdBook["book_title", row].Value = dt.Rows[row]["book_title"].ToString();
                this.grdBook["publisher", row].Value = dt.Rows[row]["publisher"].ToString();
                this.grdBook["purchase_dt", row].Value = dt.Rows[row]["purchase_dt"].ToString();
                this.grdBook["cmb_borrow_situation", row].Value = dt.Rows[row]["borrow_situation"].ToString();
                this.grdBook["cmb_borrower_Id", row].Value = dt.Rows[row]["borrower_id"].ToString();
                this.grdBook["cmb_borrower_Nm", row].Value = dt.Rows[row]["staff_name"].ToString();
                this.grdBook["return_schedule_dt", row].Value = dt.Rows[row]["return_schedule_dt"].ToString();
                this.grdBook["comment", row].Value = dt.Rows[row]["comment"].ToString();
                this.grdBook["reg_dt", row].Value = dt.Rows[row]["reg_dt"].ToString();
                this.grdBook["update_dt", row].Value = dt.Rows[row]["update_dt"].ToString();
                this.grdBook["ROWSTATE", row].Value = string.Empty;
            }

            // 데이터에 맞게 칼럼 사이즈 조정하기
            //for (int i = 0; i < grdBook.Columns.Count; i++)
            //{
            //    grdBook.AutoResizeColumn(i, DataGridViewAutoSizeColumnMode.AllCells);
            //}            

            grdBook.RowHeadersVisible = false;
            grdBook.AllowUserToAddRows = false;  // 빈레코드 표시 안하기
            grdBook.AlternatingRowsDefaultCellStyle.BackColor = Color.AliceBlue; // 그리드 행에 색 입히기
            grdBook.CurrentCell = null;

            cb.Checked = false;
            chkSearched = false;

            return 0;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Guid new_guid = Guid.NewGuid();

            int IdxNewRow = this.grdBook.Rows.Add();
            this.grdBook["valid_id", IdxNewRow].Value = new_guid.ToString();
            this.grdBook["purchase_dt", IdxNewRow].Value = string.Empty;
            this.grdBook["cmb_borrow_situation", IdxNewRow].Value = "대출가능";         
            this.grdBook["return_schedule_dt", IdxNewRow].Value = string.Empty;
            this.grdBook["update_dt", IdxNewRow].Value = DateTime.Now;
            this.grdBook["reg_dt", IdxNewRow].Value = DateTime.Now;
            this.grdBook["comment", IdxNewRow].Value = string.Empty;
            this.grdBook["ROWSTATE", IdxNewRow].Value = "Added";
            //this.grdBook["work_year", grdBook.CurrentCell.RowIndex].Value = Convert.ToInt32(this.grdBook["work_year", grdBook.CurrentCell.RowIndex - 1].Value) + 1;            
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int cnt_checkedrow = 0;

            foreach (DataGridViewRow dr in grdBook.Rows) 
            {
                if (dr.Cells["colCheck"].Value != null && dr.Cells["colCheck"].Value.ToString().ToUpper() != "false".ToUpper()) 
                {
                    if (dr.Cells["ROWSTATE"].Value.ToString() == "Added")// 새롭게 추가된 행은 그리드에서 바로 삭제
                    {
                        grdBook.Rows.Remove(dr);
                        continue;
                    }

                    dr.Visible = false;
                    this.grdBook["ROWSTATE", dr.Index].Value = "Deleted";// 기존 데이터는 그리드에서 내용만 삭제하고 저장하면 DB에서 삭제 진행.

                    cnt_checkedrow++;
                }
            }
            if(cnt_checkedrow != 0 )
                MessageBox.Show("삭제되었습니다.", "inform", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show("삭제할 행을 선택하여 주십시오.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            int resultdt = 1;

            int chkdt = chkSave();

            if (chkdt == -1 || chkdt == 1)
            {                
                return;
            }           
            resultdt = Save();

            if (resultdt == -1)
                MessageBox.Show("저장에 실패하였습니다.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else if (resultdt == 1)
            {
                MessageSet.Clear();
                grdBook.Rows.Clear();

                Search(MessageSet);
                chkSearched = false;

                MessageBox.Show("저장하였습니다.", "Inform", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (resultdt == 0)
                MessageBox.Show("저장할 데이터가 존재하지 않습니다.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
                MessageBox.Show("중복된 항목이 존재합니다.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //저장실패 - cellvaluechanged 이벤트에서 기존 work_year 값과 중복되는 값을 입력시 Warning창 띄워줘!!!!!!!!!!!!!!
        }

        private int Save()
        {
            RULE_BOOKMANAGEMENT smartViceData = new RULE_BOOKMANAGEMENT();

            bool chksave = true;
            DataSet ds = new DataSet();

            string QueryType = string.Empty;

            if (grdBook.Rows.Count > 0)
            {
                //현재 그리드와 조회한 후 그리드를 비교 후 insert 또는 delete 작업            
                for (int grdrow = 0; grdrow < grdBook.Rows.Count; grdrow++)
                {
                    if (this.grdBook.Rows[grdrow].Cells["ROWSTATE"].Value.ToString() == string.Empty)
                        continue;

                    if (this.grdBook.Rows[grdrow].Cells["ROWSTATE"].Value.ToString() == "Added" || this.grdBook.Rows[grdrow].Cells["ROWSTATE"].Value.ToString() == "Modified")
                    {

                        MessageSet.Clear();
                        MessageSet.Add("QueryType", (this.grdBook.Rows[grdrow].Cells["ROWSTATE"].Value.ToString() == "Added" ? "Insert" : "Update"));
                        MessageSet.Add("valid_id", this.grdBook.Rows[grdrow].Cells["valid_id"].Value.ToString());
                        MessageSet.Add("book_title", this.grdBook.Rows[grdrow].Cells["book_title"].Value.ToString());
                        MessageSet.Add("publisher", this.grdBook.Rows[grdrow].Cells["publisher"].Value.ToString());
                        MessageSet.Add("purchase_dt", this.grdBook.Rows[grdrow].Cells["purchase_dt"].Value.ToString());
                        MessageSet.Add("borrow_situation", this.grdBook.Rows[grdrow].Cells["cmb_borrow_situation"].Value.ToString());
                        if (this.grdBook.Rows[grdrow].Cells["cmb_borrow_situation"].Value.ToString() == "대출중")
                        {
                            MessageSet.Add("borrower_id", this.grdBook.Rows[grdrow].Cells["cmb_borrower_Id"].Value.ToString());
                            MessageSet.Add("return_schedule_dt", this.grdBook.Rows[grdrow].Cells["return_schedule_dt"].Value.ToString());
                        }         
                        if (this.grdBook.Rows[grdrow].Cells["comment"].Value.ToString() != string.Empty)      
                            MessageSet.Add("COMMENT", this.grdBook.Rows[grdrow].Cells["comment"].Value.ToString());

                        MessageSet.Add("Reg_dt", this.grdBook.Rows[grdrow].Cells["reg_dt"].Value.ToString());
                        MessageSet.Add("Update_dt", this.grdBook.Rows[grdrow].Cells["update_dt"].Value.ToString());

                        chksave = smartViceData.Save(MessageSet);
                        
                        if (!chksave)
                        {
                            return -1;
                        }
                    }
                    else if (this.grdBook.Rows[grdrow].Cells["ROWSTATE"].Value.ToString() == "Deleted")
                    {
                        QueryType = "Delete";
                        MessageSet.Clear();
                        MessageSet.Add("QueryType", QueryType);
                        MessageSet.Add("valid_id", this.grdBook.Rows[grdrow].Cells["valid_id"].Value.ToString());
                        chksave = smartViceData.Save(MessageSet);
                        
                        if (!chksave)
                        {
                            return -1;
                        }
                    }
                }
                return 1;
            }
            else
            {
                return 0;
            }
        }

        public int chkSave()
        {
            for (int row = 0; row < this.grdBook.Rows.Count; row++)
            {
               
                if (this.grdBook.Rows[row].Cells["book_title"].Value == null || this.grdBook.Rows[row].Cells["book_title"].Value.ToString() == string.Empty)
                {
                    MessageBox.Show(this.grdBook.Columns["book_title"].HeaderText + " 값이 누락되었습니다.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return -1;
                }

                if (this.grdBook.Rows[row].Cells["publisher"].Value == null || this.grdBook.Rows[row].Cells["publisher"].Value.ToString() == string.Empty)
                {
                    MessageBox.Show(this.grdBook.Columns["publisher"].HeaderText + " 값이 누락되었습니다.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return -1;
                }

                if (this.grdBook.Rows[row].Cells["return_schedule_dt"].Value == null || this.grdBook.Rows[row].Cells["return_schedule_dt"].Value.ToString() == string.Empty)
                {
                    if (this.grdBook.Rows[row].Cells["cmb_borrow_situation"].Value.ToString() == "대출중")
                    {
                        MessageBox.Show(this.grdBook.Columns["return_schedule_dt"].HeaderText + " 값이 누락되었습니다.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return -1;
                    }
                }
                if (this.grdBook.Rows[row].Cells["cmb_borrower_Nm"].Value == null || this.grdBook.Rows[row].Cells["cmb_borrower_Nm"].Value.ToString() == string.Empty)
                {
                    if (this.grdBook.Rows[row].Cells["cmb_borrow_situation"].Value.ToString() == "대출중")
                    {
                        MessageBox.Show(this.grdBook.Columns["cmb_borrower_Nm"].HeaderText + " 값이 누락되었습니다.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return -1;
                    }
                }
                if (this.grdBook.Rows[row].Cells["cmb_borrower_Id"].Value == null || this.grdBook.Rows[row].Cells["cmb_borrower_Id"].Value.ToString() == string.Empty)
                {
                    if (this.grdBook.Rows[row].Cells["cmb_borrow_situation"].Value.ToString() == "대출중")
                    {
                        MessageBox.Show(this.grdBook.Columns["cmb_borrower_Id"].HeaderText + " 값이 누락되었습니다.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return -1;
                    }
                }

                if (this.grdBook.Rows[row].Cells["cmb_borrower_Nm"].Value == null || this.grdBook.Rows[row].Cells["cmb_borrower_Nm"].Value.ToString() == string.Empty)
                {
                    if (this.grdBook.Rows[row].Cells["cmb_borrow_situation"].Value.ToString() == "대출중")
                    {
                        MessageBox.Show(this.grdBook.Columns["cmb_borrower_Nm"].HeaderText + " 값이 누락되었습니다.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return -1;
                    }
                }
                //새로 추가된 행 중에 그리드에 있는 work_year 값과 중복되는 데이터 입력시 Warning!
                if (this.grdBook.Rows[row].Cells["ROWSTATE"].Value.ToString() == "Added")
                {
                    int checkdr = dtForchk.AsEnumerable().Count(dr => dr.Field<string>("valid_id").ToString() == this.grdBook.Rows[row].Cells["valid_id"].Value.ToString());

                    if (checkdr > 0)
                    {
                        MessageBox.Show(this.grdBook.Columns["valid_id"].HeaderText + " 값은 중복될 수 없습니다.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); // 키 중복확인
                        return 1;
                    }
                }
            }
            return 0;
        }
        private void grdBook_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (chkSearched == true)
                return;

            MessageSet.Clear();
            DataTable dt = new DataTable();

            smartViceData = new RULE_BOOKMANAGEMENT();

            switch (grdBook.Columns[e.ColumnIndex].Name)
            {
                case "cmb_borrower_Nm":
                    DataGridViewComboBoxCell cmbStaffNm = (DataGridViewComboBoxCell)grdBook.Rows[e.RowIndex].Cells["cmb_borrower_Nm"];
                    if (cmbStaffNm.Value != null && cmbStaffNm.Value.ToString() != string.Empty)
                    {
                        // do stuff
                        MessageSet.Clear();
                        MessageSet.Add("Staff_Nm", this.grdBook.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString());
                        dt = smartViceData.SearchCommon("searchStaffInfo", MessageSet);

                        if (dt.Rows.Count > 0)
                            this.grdBook.Rows[e.RowIndex].Cells["cmb_borrower_Id"].Value = dt.Rows[0].Field<string>("staff_id");

                        if (dt.Rows.Count > 2)
                            MessageBox.Show("동명이인이 존재합니다. 사번을 확인하여 주십시오.", "Inform", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        grdBook.Invalidate();
                    }
                    break;
                case "cmb_borrower_Id":
                    DataGridViewComboBoxCell cmbStaffId = (DataGridViewComboBoxCell)grdBook.Rows[e.RowIndex].Cells["cmb_borrower_Id"];
                    if (cmbStaffId.Value != null && cmbStaffId.Value.ToString() != string.Empty)
                    {
                        // do stuff
                        MessageSet.Clear();
                        MessageSet.Add("Staff_Id", this.grdBook.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString());
                        dt = smartViceData.SearchCommon("searchStaffInfo", MessageSet);

                        if (dt.Rows.Count > 0)
                            this.grdBook.Rows[e.RowIndex].Cells["cmb_borrower_Nm"].Value = dt.Rows[0].Field<string>("staff_name");

                        grdBook.Invalidate();
                    }
                    break;
                case "cmb_borrow_situation":
                    DataGridViewComboBoxCell cmb_borrow_situation = (DataGridViewComboBoxCell)grdBook.Rows[e.RowIndex].Cells["cmb_borrow_situation"];
                    if (cmb_borrow_situation.Value != null && cmb_borrow_situation.Value.ToString() != string.Empty)
                    {
                        // do stuff
                        if (cmb_borrow_situation.Value.ToString() == "대출가능")
                        {
                            this.grdBook.Rows[e.RowIndex].Cells["cmb_borrower_Nm"].Value = string.Empty;
                            this.grdBook.Rows[e.RowIndex].Cells["cmb_borrower_Id"].Value = string.Empty;
                            this.grdBook.Rows[e.RowIndex].Cells["return_schedule_dt"].Value = string.Empty;
                        }

                        grdBook.Invalidate();
                    }
                    break;
            }

            //if (e.ColumnIndex == grdBook.Columns["valid_id"].Index || e.ColumnIndex == grdBook.Columns["book_title"].Index
            //    || e.ColumnIndex == grdBook.Columns["publisher"].Index || e.ColumnIndex == grdBook.Columns["COMMENT"].Index)
            //{
                //기존 데이터나 수정된 기존 데이터에만 modified 표시 
                if (this.grdBook.Rows[e.RowIndex].Cells["ROWSTATE"].Value == null || this.grdBook.Rows[e.RowIndex].Cells["ROWSTATE"].Value.ToString() == "Modified"
                    || this.grdBook.Rows[e.RowIndex].Cells["ROWSTATE"].Value.ToString() == "")
                    this.grdBook.Rows[e.RowIndex].Cells["ROWSTATE"].Value = "Modified";

                chkSearched = false;
            //}
        }
        // This event handler manually raises the CellValueChanged event 
        // by calling the CommitEdit method. 
        private void grdBook_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (grdBook.IsCurrentCellDirty)
            {
                // This fires the cell value changed handler below
                grdBook.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void grdBook_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.ColumnIndex == 0 && e.RowIndex == -1)
            {
                e.PaintBackground(e.ClipBounds, false);

                Point pt = e.CellBounds.Location;  // where you want the bitmap in the cell

                int nChkBoxWidth = 15;
                int nChkBoxHeight = 15;
                int offsetx = (e.CellBounds.Width - nChkBoxWidth) / 2;
                int offsety = (e.CellBounds.Height - nChkBoxHeight) / 2;

                pt.X += offsetx;
                pt.Y += offsety;

                //CheckBox cb = new CheckBox();
                cb.Size = new Size(nChkBoxWidth, nChkBoxHeight);
                cb.Location = pt;
                cb.CheckedChanged += new EventHandler(grdBook_CheckedChanged);

                this.grdBook.Controls.Add(cb);
                //((DataGridView)sender).Controls.Add(cb);

                e.Handled = true;
            }
        }
        private void grdBook_CheckedChanged(object sender, EventArgs e)
        {
            grdBook.CurrentCell = null;

            if (cb.CheckState == CheckState.Checked)
                this.grdBook.SelectAll();
            else
                grdBook.ClearSelection();

            foreach (DataGridViewRow r in grdBook.Rows)
            {
                if (cb.CheckState == CheckState.Checked)
                {
                    r.Cells["colCheck"].Value = true;
                }
                else
                {
                    r.Cells["colCheck"].Value = false;
                }

            }
        }

        private void BookManagement_Shown(object sender, EventArgs e)
        {
            //grdBook.ClearSelection();
            grdBook.CurrentCell = null;
        }        
    }
}
