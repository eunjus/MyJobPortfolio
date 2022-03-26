using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using SmartViceDev.CubridUtils;
using System.Drawing;
using System.Drawing.Printing;
using WIA;

namespace SmartViceDev
{
    public partial class ChargeFoodManagement : Form
    {
        RULE_CHARGEFOODMANAGEMENT smartViceData = new RULE_CHARGEFOODMANAGEMENT();
       
        Dictionary<string, string> MessageSet = new Dictionary<string, string>();
        DataTable dtForchk = new DataTable();
        bool chkSearched = true;
        DateTimePicker dtp = new DateTimePicker();
        Rectangle _dtpRectangle = new Rectangle();

        CheckBox cb = new CheckBox();

        public ChargeFoodManagement()
        {
            InitializeComponent();

            eventslist(); //이벤트 처리 함수
            grdColumnAdd(); // 그리드 컬럼 형성

            this.StartDate.Value = Convert.ToDateTime((DateTime.Today.Year.ToString() + "-" + DateTime.Today.Month.ToString().PadLeft(2, '0') + "-" + "01"));
            this.EndDate.Value = Convert.ToDateTime((DateTime.Today.Year.ToString() + "-" + DateTime.Today.Month.ToString().PadLeft(2, '0') + "-" + DateTime.DaysInMonth(DateTime.Today.Year, DateTime.Today.Month).ToString().PadLeft(2, '0')));

        }

        private void ChargeFoodManagement_Load(object sender, EventArgs e)
        {
            
            //그리드내 특정 셀에 캘린더 추가 
            grdChgFood.Controls.Add(dtp);
            dtp.Visible = false;
            dtp.Format = DateTimePickerFormat.Custom;
            dtp.TextChanged += new EventHandler(dtp_TextChange);

            btnSearch_Click(null, null);
        }

        private void eventslist()
        {
            this.Load += new System.EventHandler(this.ChargeFoodManagement_Load);
            this.Shown += new System.EventHandler(this.ChargeFoodManagement_Shown);

            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            this.btnExcelExport.Click += new System.EventHandler(this.btnExcelExport_Click);

            this.grdChgFood.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.grdChgFood_CellPainting);
            this.grdChgFood.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grdChgFood_CellClick);
            this.grdChgFood.Scroll += new System.Windows.Forms.ScrollEventHandler(this.grdChgFood_Scroll);
            this.grdChgFood.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.grdChgFood_CellValueChanged);
            // 그리드에 있는 콤보박스 형태의 열 값이 변경될 때 발생하는 이벤트
            this.grdChgFood.CurrentCellDirtyStateChanged += new System.EventHandler(this.grdChgFood_CurrentCellDirtyStateChanged);

            this.rbtnShowbyDay.CheckedChanged += new System.EventHandler(this.rbtnShowbyDay_CheckedChanged);
            this.rbtnShowByMonth.CheckedChanged += new System.EventHandler(this.rbtnShowByMonth_CheckedChanged);

        }

        private void grdColumnAdd()
        {
            DataTable dt = new DataTable();
            //RULE_DEPARTMENTMANAGEMENT smartViceData = new RULE_DEPARTMENTMANAGEMENT();

            var chkCol = new DataGridViewCheckBoxColumn
            {
                Name = "colCheck",
                HeaderText = ""
            };
            this.grdChgFood.Columns.Add(chkCol);
            this.grdChgFood.Columns["colCheck"].Width = 50;

            //유효 아이디
            this.grdChgFood.Columns.Add("valid_id", "유효 아이디");
            this.grdChgFood.Columns["valid_id"].Visible = false;
            //월별 일자
            this.grdChgFood.Columns.Add("monthly_date", "월별 일자");
            this.grdChgFood.Columns["monthly_date"].Width = 100;            
            //지출자 아이디            
            dt = smartViceData.SearchCommon("seachStaffInfo",MessageSet);

            //직원 명
            DataGridViewComboBoxColumn cmbStaffNm = new DataGridViewComboBoxColumn();
            cmbStaffNm.Name = "cmbStaffNm";
            cmbStaffNm.HeaderText = "직원 명";            
            for (int row = 0; row < dt.Rows.Count; row++)
            {
                cmbStaffNm.Items.Add(dt.Rows[row]["staff_name"]);
            }
            this.grdChgFood.Columns.Add(cmbStaffNm);

            //직원 ID
            DataGridViewComboBoxColumn cmbStaffId = new DataGridViewComboBoxColumn();
            cmbStaffId.Name = "cmbStaffId";
            cmbStaffId.HeaderText = "직원 ID";
            cmbStaffId.Width = 150;
            for (int row = 0; row < dt.Rows.Count; row++)
            {
                cmbStaffId.Items.Add(dt.Rows[row]["staff_id"]);
            }        
            this.grdChgFood.Columns.Add(cmbStaffId);

            //this.grdChgFood.Columns["staff_name"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            //거래처
            this.grdChgFood.Columns.Add("food_place", "거래처");
            this.grdChgFood.Columns["food_place"].Width = 150;            
            //이용 횟수
            this.grdChgFood.Columns.Add("use_times", "이용 횟수");                                  
            this.grdChgFood.Columns["use_times"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            //지급여부          
            this.grdChgFood.Columns.Add("food_cost", "단가");
            this.grdChgFood.Columns["food_cost"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            //합계         
            this.grdChgFood.Columns.Add("Sum_cost", "합계");
            this.grdChgFood.Columns["Sum_cost"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            //ROWSTATE
            this.grdChgFood.Columns.Add("RowState", "RowState");
            this.grdChgFood.Columns["RowState"].Visible = false; // 행의 신규,기존,수정,삭제 등의 상태를 나타냄.

            this.grdChgFood.AllowUserToAddRows = false;
            
        }

        private void comboboxListAdd()
        {
            //DataSet ds = new DataSet();
            ////사원명 콤보박스 리스트업 
            //ds = smartViceData.SearchCommon("seachEmpNm");

            //this.cbempNm.Items.Add("전체");
            //for (int row = 0; row < ds.Tables[0].Rows.Count; row++)
            //{
            //    this.cbempNm.Items.Add(ds.Tables[0].Rows[row].Field<string>("staff_name").ToString());
            //}
            //this.cbempNm.SelectedItem = "전체";

            ////직책명 콤보박스 리스트업 

            //ds = smartViceData.SearchCommon("seachPosition");

            //this.cmbPosition.Items.Add("전체");
            //for (int row = 0; row < ds.Tables[0].Rows.Count; row++)
            //{
            //    this.cmbPosition.Items.Add(ds.Tables[0].Rows[row].Field<string>("position_name").ToString());
            //}

            //this.cmbPosition.SelectedItem = "전체";
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
                        this.grdChgFood.Rows.Clear();
                        chkSearched = false;                       
                        return;
                    }
                }
                else if (dialogResult == DialogResult.Cancel || dialogResult == DialogResult.No)
                {
                    return;
                }
            }
         
            int resultchk2 = Search(MessageSet);

            if (resultchk2 == -1)
            {
                MessageBox.Show("조회된 데이터가 없습니다.", "Inform", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.grdChgFood.Rows.Clear();
                chkSearched = false;               
                return;
            }
        }

        public int Chksearch()
        {
            if (chkSearched == false) // 조회한 이력이 있을때만 그리드 체크 
            {

                for (int grdrow = 0; grdrow < this.grdChgFood.Rows.Count; grdrow++)
                {
                    if (this.grdChgFood.Rows[grdrow].Cells["ROWSTATE"].Value != null)
                    {
                        if (this.grdChgFood.Rows[grdrow].Cells["ROWSTATE"].Value.ToString() == "Modified" || this.grdChgFood.Rows[grdrow].Cells["ROWSTATE"].Value.ToString() == "Added"
                            || this.grdChgFood.Rows[grdrow].Cells["ROWSTATE"].Value.ToString() == "Deleted")
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
            MessageSet.Clear();

            if (rbtnShowByMonth.Checked == true && rbtnShowbyDay.Checked == false)
                MessageSet.Add("QueryType", "GetDataByMonth");
            else if (rbtnShowByMonth.Checked == false && rbtnShowbyDay.Checked == true)
                MessageSet.Add("QueryType", "GetDataByDay");

            MessageSet.Add("StartDate", this.StartDate.Value.ToShortDateString());
            MessageSet.Add("EndDate", this.EndDate.Value.ToShortDateString());

            if (this.grdChgFood.Rows.Count > 0)
            {
                this.grdChgFood.Rows.Clear();
            }

            DataSet ds = new DataSet();
            ds = smartViceData.Search(MessageSet);

            DataTable dt = ds.Tables[0].Copy();

            if (dt.Rows.Count < 1)
                return -1;

            dtForchk = ds.Tables[0].Copy();

            if (this.grdChgFood.Rows.Count > 1)
                this.grdChgFood.Rows.Clear();

            for (int row = 0; row < dt.Rows.Count; row++)
            {           
                this.grdChgFood.Rows.Add();
                this.grdChgFood["valid_id", row].Value = dt.Rows[row]["valid_id"].ToString();
                this.grdChgFood["monthly_date", row].Value = dt.Rows[row]["monthly_date"].ToString();
                this.grdChgFood["cmbStaffId", row].Value = dt.Rows[row]["staff_id"].ToString();
                this.grdChgFood["cmbStaffNm", row].Value = dt.Rows[row]["staff_name"].ToString();
                this.grdChgFood["food_place", row].Value = dt.Rows[row]["food_place"].ToString();
                this.grdChgFood["use_times", row].Value = dt.Rows[row]["use_times"].ToString();                
                this.grdChgFood["food_cost", row].Value = dt.Rows[row]["food_cost"].ToString();
                this.grdChgFood["ROWSTATE", row].Value = "";
            }

            // 데이터에 맞게 칼럼 사이즈 조정하기
            //for (int i = 0; i < grdDepart.Columns.Count; i++)
            //{
            //    grdDepart.AutoResizeColumn(i, DataGridViewAutoSizeColumnMode.AllCells);
            //}
            grdChgFood.RowHeadersVisible = false;
            grdChgFood.AllowUserToAddRows = false;  // 빈레코드 표시 안하기
            grdChgFood.AlternatingRowsDefaultCellStyle.BackColor = Color.AliceBlue; // 그리드 행에 색 입히기
            grdChgFood.CurrentCell = null;

            cb.Checked = false;
            chkSearched = false;

            return 0;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Guid new_guid = Guid.NewGuid();

            int IdxNewRow = this.grdChgFood.Rows.Add();
            this.grdChgFood["valid_id", IdxNewRow].Value = new_guid.ToString();
            this.grdChgFood["monthly_date", IdxNewRow].Value = DateTime.Today.Year.ToString() + "-" + DateTime.Today.Month.ToString().PadLeft(2,'0');
            //this.grdChgFood["cmbStaffNm", IdxNewRow].Value = "이은주";
            //this.grdChgFood["cmbStaffId", IdxNewRow].Value = "JWP001_J";
            this.grdChgFood["Sum_cost", IdxNewRow].Value = "0"; 
            this.grdChgFood["ROWSTATE", IdxNewRow].Value = "Added";
            //this.grdDepart["work_year", grdDepart.CurrentCell.RowIndex].Value = Convert.ToInt32(this.grdDepart["work_year", grdDepart.CurrentCell.RowIndex - 1].Value) + 1;            
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow dr in grdChgFood.Rows) 
            {
                
                if (dr.Cells["colCheck"].Value != null && dr.Cells["colCheck"].Value.ToString().ToUpper() != "false".ToUpper()) 
                {
                    if (dr.Cells["ROWSTATE"].Value.ToString() == "Added")// 새롭게 추가된 행은 그리드에서 바로 삭제
                    {
                        grdChgFood.Rows.Remove(dr);
                        continue;
                    }

                    dr.Visible = false;
                    this.grdChgFood["ROWSTATE", dr.Index].Value = "Deleted";// 기존 데이터는 그리드에서 내용만 삭제하고 저장하면 DB에서 삭제 진행.
                }
            }
            MessageBox.Show("삭제되었습니다.", "inform", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            int resultdt = 1;

            int chkdt = chkSave();

            if (chkdt == -1)
            {               
                return;
            }

            resultdt = Save();

            if (resultdt == -1)
                MessageBox.Show("저장에 실패하였습니다.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else if (resultdt == 1)
            {
                //MessageSet.Clear();
                grdChgFood.Rows.Clear();

                //if (rbtnShowByMonth.Checked == true && rbtnShowbyDay.Checked == false)
                //    MessageSet.Add("QueryType", "GetDataByMonth");
                //else if (rbtnShowByMonth.Checked == false && rbtnShowbyDay.Checked == true)
                //    MessageSet.Add("QueryType", "GetDataByDay");

                //MessageSet.Add("StartDate", this.StartDate.Value.ToShortDateString());
                //MessageSet.Add("EndDate", this.EndDate.Value.ToShortDateString());

                Search(MessageSet);
                chkSearched = false;

                dtp.Visible = false;

                MessageBox.Show("저장하였습니다.", "Inform", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (resultdt == 0)
                MessageBox.Show("저장할 데이터가 존재하지 않습니다.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
                MessageBox.Show("중복된 항목이 존재합니다.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private int Save()
        {
            bool chksave = true;
            DataSet ds = new DataSet();

            string QueryType = string.Empty;

            if (grdChgFood.Rows.Count > 0)
            {
                //현재 그리드와 조회한 후 그리드를 비교 후 insert 또는 delete 작업            
                for (int grdrow = 0; grdrow < grdChgFood.Rows.Count; grdrow++)
                {
                    if (this.grdChgFood.Rows[grdrow].Cells["ROWSTATE"].Value.ToString() == string.Empty)
                        continue;

                    if (this.grdChgFood.Rows[grdrow].Cells["ROWSTATE"].Value.ToString() == "Added" || this.grdChgFood.Rows[grdrow].Cells["ROWSTATE"].Value.ToString() == "Modified")
                    {
                        MessageSet.Clear();
                        MessageSet.Add("QueryType", (this.grdChgFood.Rows[grdrow].Cells["ROWSTATE"].Value.ToString() == "Added" ? "Insert" : "Update"));
                        MessageSet.Add("valid_id", this.grdChgFood.Rows[grdrow].Cells["valid_id"].Value.ToString());
                        MessageSet.Add("monthly_date", this.grdChgFood.Rows[grdrow].Cells["monthly_date"].Value.ToString());
                        MessageSet.Add("staff_id", this.grdChgFood.Rows[grdrow].Cells["cmbStaffId"].Value.ToString());
                        MessageSet.Add("staff_name", this.grdChgFood.Rows[grdrow].Cells["cmbStaffNm"].Value.ToString());
                        MessageSet.Add("food_place", this.grdChgFood.Rows[grdrow].Cells["food_place"].Value.ToString());
                        MessageSet.Add("use_times", this.grdChgFood.Rows[grdrow].Cells["use_times"].Value.ToString());                        
                        MessageSet.Add("food_cost", this.grdChgFood.Rows[grdrow].Cells["food_cost"].Value.ToString());

                        chksave = smartViceData.Save(MessageSet);

                        if (!chksave)
                        {
                            return -1;
                        }
                    }
                    else if (this.grdChgFood.Rows[grdrow].Cells["ROWSTATE"].Value.ToString() == "Deleted")
                    {
                        MessageSet.Clear();
                        QueryType = "Delete";
                        MessageSet.Add("QueryType", QueryType);
                        MessageSet.Add("valid_id", this.grdChgFood.Rows[grdrow].Cells["valid_id"].Value.ToString());
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
            for (int row = 0; row < this.grdChgFood.Rows.Count; row++)
            {
                if (this.grdChgFood.Rows[row].Cells["monthly_date"].Value == null)
                {
                    MessageBox.Show(this.grdChgFood.Columns["monthly_date"].HeaderText + " 값이 누락되었습니다.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return -1;
                }

                if (this.grdChgFood.Rows[row].Cells["cmbStaffId"].Value == null)
                {
                    MessageBox.Show(this.grdChgFood.Columns["cmbStaffId"].HeaderText + " 값이 누락되었습니다.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return -1;
                }

                if (this.grdChgFood.Rows[row].Cells["cmbStaffNm"].Value == null)
                {
                    MessageBox.Show(this.grdChgFood.Columns["cmbStaffNm"].HeaderText + " 값이 누락되었습니다.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return -1;
                }

                if (this.grdChgFood.Rows[row].Cells["food_place"].Value == null)
                {
                    MessageBox.Show(this.grdChgFood.Columns["food_place"].HeaderText + " 값이 누락되었습니다.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return -1;
                }

                if (this.grdChgFood.Rows[row].Cells["use_times"].Value == null)
                {
                    MessageBox.Show(this.grdChgFood.Columns["use_times"].HeaderText + " 값이 누락되었습니다.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return -1;
                }

                if (this.grdChgFood.Rows[row].Cells["food_cost"].Value == null)
                {
                    MessageBox.Show(this.grdChgFood.Columns["food_cost"].HeaderText + " 값이 누락되었습니다.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return -1;
                }
            }
            return 0;
        }
        private void grdChgFood_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {            

            MessageSet.Clear();
            DataTable dt = new DataTable();

            smartViceData = new RULE_CHARGEFOODMANAGEMENT();

            switch (grdChgFood.Columns[e.ColumnIndex].Name)
            {
                case "cmbStaffNm":
                    DataGridViewComboBoxCell cmbStaffNm = (DataGridViewComboBoxCell)grdChgFood.Rows[e.RowIndex].Cells["cmbStaffNm"];
                    if (cmbStaffNm.Value != null && cmbStaffNm.Value.ToString() != string.Empty)
                    {
                        // do stuff
                        MessageSet.Clear();
                        MessageSet.Add("Staff_Nm", this.grdChgFood.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString());
                        dt = smartViceData.SearchCommon("seachStaffInfo", MessageSet);

                        if (dt.Rows.Count > 0)
                            this.grdChgFood.Rows[e.RowIndex].Cells["cmbStaffId"].Value = dt.Rows[0].Field<string>("staff_id");

                        if (dt.Rows.Count > 2)
                            MessageBox.Show("동명이인이 존재합니다. 사번을 확인하여 주십시오.", "Inform", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        grdChgFood.Invalidate();
                    }
                    break;
                case "cmbStaffId":
                    DataGridViewComboBoxCell cmbStaffId = (DataGridViewComboBoxCell)grdChgFood.Rows[e.RowIndex].Cells["cmbStaffId"];
                    if (cmbStaffId.Value != null && cmbStaffId.Value.ToString() != string.Empty)
                    {
                        // do stuff
                        MessageSet.Clear();
                        MessageSet.Add("Staff_Id", this.grdChgFood.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString());
                        dt = smartViceData.SearchCommon("seachStaffInfo", MessageSet);

                        if (dt.Rows.Count > 0)
                            this.grdChgFood.Rows[e.RowIndex].Cells["cmbStaffNm"].Value = dt.Rows[0].Field<string>("staff_name");

                        grdChgFood.Invalidate();
                    }
                    break;
                case "use_times":
                    if(this.grdChgFood.Rows[e.RowIndex].Cells["food_cost"].Value != null && this.grdChgFood.Rows[e.RowIndex].Cells["use_times"].Value != null)
                        this.grdChgFood.Rows[e.RowIndex].Cells["Sum_cost"].Value = Convert.ToInt32(this.grdChgFood.Rows[e.RowIndex].Cells["food_cost"].Value) *
                                                                                    Convert.ToInt32(this.grdChgFood.Rows[e.RowIndex].Cells["use_times"].Value);
                    break;
                case "food_cost":
                    if (this.grdChgFood.Rows[e.RowIndex].Cells["food_cost"].Value != null && this.grdChgFood.Rows[e.RowIndex].Cells["use_times"].Value != null)
                        this.grdChgFood.Rows[e.RowIndex].Cells["Sum_cost"].Value = Convert.ToInt32(this.grdChgFood.Rows[e.RowIndex].Cells["food_cost"].Value) *
                                                                                    Convert.ToInt32(this.grdChgFood.Rows[e.RowIndex].Cells["use_times"].Value);
                    break;
            }

            if (chkSearched == true)
                return;

            if (e.ColumnIndex == grdChgFood.Columns["monthly_date"].Index || e.ColumnIndex == grdChgFood.Columns["cmbStaffNm"].Index
                || e.ColumnIndex == grdChgFood.Columns["cmbStaffId"].Index || e.ColumnIndex == grdChgFood.Columns["food_place"].Index
                || e.ColumnIndex == grdChgFood.Columns["use_times"].Index  || e.ColumnIndex == grdChgFood.Columns["food_cost"].Index)
            {
                //기존 데이터나 수정된 기존 데이터에만 modified 표시 
                if (this.grdChgFood.Rows[e.RowIndex].Cells["ROWSTATE"].Value == null || this.grdChgFood.Rows[e.RowIndex].Cells["ROWSTATE"].Value.ToString() == "Modified"
                    || this.grdChgFood.Rows[e.RowIndex].Cells["ROWSTATE"].Value.ToString() == "")
                    this.grdChgFood.Rows[e.RowIndex].Cells["ROWSTATE"].Value = "Modified";

                chkSearched = false;
            }

        }

        private void grdChgFood_Scroll(object sender, ScrollEventArgs e)
        {
            DataGridView gv = (DataGridView)sender;

            Rectangle rtHeader = gv.DisplayRectangle;

            rtHeader.Height = gv.ColumnHeadersHeight / 2;

            gv.Invalidate(rtHeader);

            dtp.Visible = false;
        }

        private void grdChgFood_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)
                return;

            switch (grdChgFood.Columns[e.ColumnIndex].Name)
            {
                case "colCheck":                    
                        this.grdChgFood.CurrentRow.Selected = true;         
                    break;
                case "monthly_date":

                    if (rbtnShowByMonth.Checked == true && rbtnShowbyDay.Checked == false)
                        return;

                    _dtpRectangle = grdChgFood.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true);
                    dtp.Size = new Size(_dtpRectangle.Width, _dtpRectangle.Height);
                    dtp.Location = new Point(_dtpRectangle.X, _dtpRectangle.Y);
                    dtp.Visible = true;
                    if (!this.grdChgFood.Rows[e.RowIndex].IsNewRow && this.grdChgFood.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
                    {
                        dtp.Value = Convert.ToDateTime(this.grdChgFood.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);
                    }
                    //else
                        //dtp.Value = DateTime.Today;

                    break;
                default:
                    dtp.Visible = false;                    
                    break;
            }
        }

        private void dtp_TextChange(object sender, EventArgs e)
        {            
            grdChgFood.CurrentCell.Value = dtp.Value.ToShortDateString();                                  
        }
      
        // This event handler manually raises the CellValueChanged event 
        // by calling the CommitEdit method. 
        private void grdChgFood_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (grdChgFood.IsCurrentCellDirty)
            {
                // This fires the cell value changed handler below
                grdChgFood.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }        

        private void btnExcelExport_Click(object sender, EventArgs e)
        {
            if (this.grdChgFood.Rows.Count < 1)
            {
                MessageBox.Show("엑셀파일로 저장할 데이터가 없습니다.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            ExportToExcel();
        }

        private void ExportToExcel()
        {
            bool isExport = false;

            //Creating a Excel object
            Microsoft.Office.Interop.Excel._Application excel = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel._Workbook workbook = excel.Workbooks.Add(Type.Missing);
            Microsoft.Office.Interop.Excel._Worksheet worksheet = null;

            //DataGridView에 불러온 Data가 아무것도 없을 경우
            if (grdChgFood.Rows.Count == 1)
            {
                MessageBox.Show("엑셀 파일로 저장할 데이터가 없습니다.", "inform", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                worksheet = workbook.ActiveSheet;

                int cellRowIndex = 1;
                int cellColumsIndex = 1;

                //Add Column 
                for (int col = 1; col < grdChgFood.Columns.Count; col++)
                {
                    if (cellRowIndex == 1)
                    {
                        if (grdChgFood.Columns[col].Visible == true)
                        {
                            worksheet.Cells[cellRowIndex, cellColumsIndex] = grdChgFood.Columns[col].HeaderText;
                            worksheet.Cells[cellRowIndex, cellColumsIndex].Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Aqua);
                        }
                        else
                            continue;
                    }
                    cellColumsIndex++;
                }                

                cellColumsIndex = 1;
                cellRowIndex++;

                for (int row = 0; row < grdChgFood.Rows.Count; row++)
                {
                    for (int col = 1; col < grdChgFood.Columns.Count; col++)
                    {
                        if (grdChgFood.Columns[col].Visible != true)
                        {
                            continue;
                        }

                        if (grdChgFood.Rows[row].Cells[col].Value != null)
                            worksheet.Cells[cellRowIndex, cellColumsIndex] = grdChgFood.Rows[row].Cells[col].Value.ToString();
                        else
                            worksheet.Cells[cellRowIndex, cellColumsIndex] = "";

                        cellColumsIndex++;
                    }
                    cellColumsIndex = 1;
                    cellRowIndex++;
                }
                //셀 테두리 범위 설정
                string startRange = "A1"; 
                string endRange = "G" + (grdChgFood.Rows.Count + 1).ToString();

                // 전체  
                worksheet.get_Range(startRange, endRange).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
            
                SaveFileDialog saveFileDialog = GetExcelSave();

                if (saveFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    workbook.SaveAs(saveFileDialog.FileName);
                    MessageBox.Show("Export Successful!", "Inform", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    isExport = true;
                }

                //Export 성공했으면 객체들 해제
                if (isExport)
                {
                    workbook.Close();

                    excel.Quit();
                    workbook = null;
                    excel = null;
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private SaveFileDialog GetExcelSave()
        {
            //Getting the location and file name of the excel save from user.
            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.CheckFileExists = false;
            saveDialog.AddExtension = true;
            saveDialog.ValidateNames = true;
            saveDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

            saveDialog.DefaultExt = ".xlsx";
            saveDialog.Filter = "Microsoft Excel Workbook(*.xls)|*.xlsx";
            saveDialog.FileName = "JETSPURT_" + this.Text + "_" + DateTime.Today.ToString().Trim().Substring(0, 10);

            return saveDialog;
        }

        private void rbtnShowByMonth_CheckedChanged(object sender, EventArgs e)
        {
            if (this.grdChgFood.Rows.Count > 0)
            {
                if (rbtnShowByMonth.Checked == true && rbtnShowbyDay.Checked == false)
                {
                    btnSearch_Click(null,null);

                    //월별조회 일 때는 값 수정 불가.
                    foreach (DataGridViewRow row in this.grdChgFood.Rows)
                    {
                        row.ReadOnly = true;
                    }
                }
            }
        }

        private void rbtnShowbyDay_CheckedChanged(object sender, EventArgs e)
        {
            if (this.grdChgFood.Rows.Count > 0)
            {
                if (rbtnShowByMonth.Checked == false && rbtnShowbyDay.Checked == true)
                {
                    btnSearch_Click(null, null);

                    foreach (DataGridViewRow row in this.grdChgFood.Rows)
                    {
                        row.ReadOnly = false;
                    }
                }
            }
        }

        private void grdChgFood_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (rbtnShowByMonth.Checked == true && rbtnShowbyDay.Checked == false)
            {
                MessageBox.Show("월별조회 일 때는 값을 수정할 수 없습니다.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }

        private void grdChgFood_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
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

                
                cb.Size = new Size(nChkBoxWidth, nChkBoxHeight);
                cb.Location = pt;
                cb.CheckedChanged += new EventHandler(grdChgFood_CheckedChanged);

                ((DataGridView)sender).Controls.Add(cb);

                e.Handled = true;
            }
        }
        private void grdChgFood_CheckedChanged(object sender, EventArgs e)
        {
            grdChgFood.CurrentCell = null;

            if (cb.CheckState == CheckState.Checked)
                this.grdChgFood.SelectAll();
            else
                grdChgFood.ClearSelection();

            foreach (DataGridViewRow r in grdChgFood.Rows)
            {
                if (cb.CheckState == CheckState.Checked)
                    r.Cells["colCheck"].Value = true;
                else
                    r.Cells["colCheck"].Value = false;

            }            
        }
        private void ChargeFoodManagement_Shown(object sender, EventArgs e)
        {
            //grdStaff.ClearSelection();
            grdChgFood.CurrentCell = null;
        }
    }

}
