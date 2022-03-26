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
    public partial class SalaryPAPManagement : Form
    {
        RULE_SALARYPAPMANAGEMENT smartViceData = new RULE_SALARYPAPMANAGEMENT();
       
        Dictionary<string, string> MessageSet = new Dictionary<string, string>();
        DataTable dtForchk = new DataTable();
        bool chkSearched = true;

        CheckBox cb = new CheckBox();
        public SalaryPAPManagement()
        {
            InitializeComponent();
        }

        private void SalaryPAPManagement_Load(object sender, EventArgs e)
        {
            eventslist(); //이벤트 처리 함수
            grdColumnAdd(); // 그리드 컬럼 형성
            comboboxListAdd();//검색 조건 초기화

            //this.StartDate.Value = Convert.ToDateTime((DateTime.Today.Year.ToString() + "-" + DateTime.Today.Month.ToString().PadLeft(2,'0') + "-" + "01"));
            //this.EndDate.Value = Convert.ToDateTime((DateTime.Today.Year.ToString() + "-" + DateTime.Today.Month.ToString().PadLeft(2, '0') + "-" + DateTime.DaysInMonth(DateTime.Today.Year,DateTime.Today.Month).ToString().PadLeft(2,'0')));

            //this.grdSumDuty.ClearSelection();
            
            this.cmbMonth.SelectedIndex = DateTime.Today.Month;
            lbByMonth.Text = "*" + this.cmbMonth.SelectedItem.ToString() + "월분 급여 대장";

            btnSearch_Click(null, null);

        }

        private void eventslist()
        {
            this.Load += new System.EventHandler(this.SalaryPAPManagement_Load);
            this.Shown += new System.EventHandler(this.SalaryPAPManagement_Shown);

            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            this.btnExcelExport.Click += new System.EventHandler(this.btnExcelExport_Click);
            //this.btnSave.Click += new System.EventHandler(this.btnSave_Click);            

            this.grdSalary.Scroll += new System.Windows.Forms.ScrollEventHandler(this.grdSalary_Scroll);
            this.grdSalary.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.grdSalary_CellValueChanged);
            this.grdSalary.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.grdSalary_CellPainting);
            // 그리드에 있는 콤보박스 형태의 열 값이 변경될 때 발생하는 이벤트
            this.grdSalary.CurrentCellDirtyStateChanged += new System.EventHandler(this.grdSalary_CurrentCellDirtyStateChanged);           

        }
        private void comboboxListAdd()
        {
            DataSet ds = new DataSet();          
            //직책명 콤보박스 리스트업 
            //ds = smartViceData.SearchCommon("seachPosition");

            //this.cmbPosition.Items.Add("전체");
            //for (int row = 0; row < ds.Tables[0].Rows.Count; row++)
            //{
            //    this.cmbPosition.Items.Add(ds.Tables[0].Rows[row].Field<string>("position_name").ToString());
            //}

            //this.cmbPosition.SelectedItem = "전체";

            this.cmbMonth.Items.Add("전체");
            for (int cnt_month = 1; cnt_month < 13; cnt_month++)
            {
                this.cmbMonth.Items.Add( cnt_month.ToString());
            }

            this.cmbMonth.SelectedItem = "전체";
      
            for (int cnt_year = DateTime.Today.Year; cnt_year > 2017; cnt_year--)
            {
                this.cmbYear.Items.Add(cnt_year.ToString());
            }

            this.cmbYear.SelectedItem = DateTime.Today.Year.ToString();
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
            this.grdSalary.Columns.Add(chkCol);
            this.grdSalary.Columns["colCheck"].Width = 50;
            // 입금은행
            //this.grdSalary.Columns.Add("deposit_bank", "입금 은행");
            //this.grdSalary.Columns["deposit_bank"].Width = 100;

            DataGridViewComboBoxColumn deposit_bank_col = new DataGridViewComboBoxColumn
            {
                Name = "deposit_bank_col",
                HeaderText = "입금은행"
            };
            this.grdSalary.Columns.Add(deposit_bank_col);
            this.grdSalary.Columns["deposit_bank_col"].Width = 100;
            //은행 리스트업
            deposit_bank_col.Items.Add(" ");
            deposit_bank_col.Items.Add("농협은행");
            deposit_bank_col.Items.Add("국민은행");
            deposit_bank_col.Items.Add("신한은행");
            deposit_bank_col.Items.Add("우리은행");
            deposit_bank_col.Items.Add("하나은행");
            deposit_bank_col.Items.Add("기업은행");
            deposit_bank_col.Items.Add("시티은행");
            deposit_bank_col.Items.Add("우체국");
            deposit_bank_col.Items.Add("SC제일은행");
            deposit_bank_col.Items.Add("새마을금고");
            deposit_bank_col.Items.Add("대구은행");
            deposit_bank_col.Items.Add("광주은행");
            deposit_bank_col.Items.Add("신협은행");
            deposit_bank_col.Items.Add("전북은행");
            deposit_bank_col.Items.Add("경남은행");
            deposit_bank_col.Items.Add("부산은행");
            deposit_bank_col.Items.Add("수협은행");
            deposit_bank_col.Items.Add("제주은행");
            deposit_bank_col.Items.Add("저축은행");
            deposit_bank_col.Items.Add("산림조합");
            deposit_bank_col.Items.Add("케이뱅크");
            deposit_bank_col.Items.Add("카카오뱅크");
      
            //입금계좌
            this.grdSalary.Columns.Add("deposit_account", "입금 계좌");
            this.grdSalary.Columns["deposit_account"].Width = 100;
            //이체금액
            this.grdSalary.Columns.Add("allowance", "이체금액");
            this.grdSalary.Columns["allowance"].Width = 100;
            this.grdSalary.Columns["allowance"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            //직원 사번
            this.grdSalary.Columns.Add("staff_id", "직원 ID");
            this.grdSalary.Columns["staff_id"].Visible = false;
            //직원명
            this.grdSalary.Columns.Add("staff_name", "직원명");
            this.grdSalary.Columns["staff_name"].Width = 100;
            this.grdSalary.Columns["staff_name"].ReadOnly = true;
            this.grdSalary.Columns["staff_name"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //통장기재내용
            this.grdSalary.Columns.Add("Commentofbankbook", "통장기재내용");
            this.grdSalary.Columns["Commentofbankbook"].Width = 110;
            //비고
            this.grdSalary.Columns.Add("Comment", "비고");
            this.grdSalary.Columns["Comment"].Width = 100;            
            
            //ROWSTATE
            this.grdSalary.Columns.Add("RowState", "RowState");
            this.grdSalary.Columns["RowState"].Visible = false; // 행의 신규,기존,수정,삭제 등의 상태를 나타냄.

            this.grdSalary.AllowUserToAddRows = false;
            
        }        

        private void btnSearch_Click(object sender, EventArgs e)
        {
            chkSearched = true;

            //int chkgrddt = Chksearch();

            //if (chkgrddt == 1)
            //{
            //    DialogResult dialogResult = MessageBox.Show("저장되지 않은 데이터가 존재합니다. 그래도 조회하시겠습니까?\n(수정한 데이터를 잃게됩니다.)", "Warning", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
            //    if (dialogResult == DialogResult.Yes)
            //    {
            //        int resultchk = Search(MessageSet);

            //        if (resultchk == -1)
            //        {
            //            MessageBox.Show("조회된 데이터가 없습니다.", "Inform", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //            this.grdSalary.Rows.Clear();
            //            chkSearched = false;                       
            //            return;
            //        }
            //    }
            //    else if (dialogResult == DialogResult.Cancel || dialogResult == DialogResult.No)
            //    {
            //        return;
            //    }
            //}
         
            int resultchk2 = Search(MessageSet);

            if (resultchk2 == -1)
            {
                MessageBox.Show("조회된 데이터가 없습니다.", "Inform", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.grdSalary.Rows.Clear();
                chkSearched = false;               
                return;
            }
        }

        public int Chksearch()
        {
            if (chkSearched == false) // 조회한 이력이 있을때만 그리드 체크 
            {

                for (int grdrow = 0; grdrow < this.grdSalary.Rows.Count; grdrow++)
                {
                    if (this.grdSalary.Rows[grdrow].Cells["ROWSTATE"].Value != null)
                    {
                        if (this.grdSalary.Rows[grdrow].Cells["ROWSTATE"].Value.ToString() == "Modified" || this.grdSalary.Rows[grdrow].Cells["ROWSTATE"].Value.ToString() == "Added"
                            || this.grdSalary.Rows[grdrow].Cells["ROWSTATE"].Value.ToString() == "Deleted")
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
            if (this.grdSalary.Rows.Count > 0)
            {
                this.grdSalary.Rows.Clear();
            }
       
            //해당 월에 데이터가 있는지 확인 후, 있으면 가져오고 없으면 새로 추가
            MessageSet.Clear();
            MessageSet.Add("StartDate", this.cmbYear.Text + "-" + this.cmbMonth.Text.PadLeft(2, '0') + "-01");
            MessageSet.Add("EndDate", this.cmbYear.Text + "-" + this.cmbMonth.Text.PadLeft(2, '0') + "-" +
                                        DateTime.DaysInMonth(Convert.ToInt32(this.cmbYear.Text), Convert.ToInt32(this.cmbMonth.Text)).ToString().PadLeft(2, '0'));
            MessageSet.Add("SearchType", "chkNewMonth");

            smartViceData = new RULE_SALARYPAPMANAGEMENT();
            DataSet chkdt = smartViceData.Search(MessageSet);
            MessageSet.Clear();
            //기존 데이터가 있을 때 
            if (chkdt.Tables[0].Rows.Count > 0)
            {
                MessageSet.Clear();

                MessageSet.Add("StartDate", this.cmbYear.Text + "-" + this.cmbMonth.Text.PadLeft(2, '0') + "-01");
                MessageSet.Add("EndDate", this.cmbYear.Text + "-" + this.cmbMonth.Text.PadLeft(2, '0') + "-" +
                                            DateTime.DaysInMonth(Convert.ToInt32(this.cmbYear.Text), Convert.ToInt32(this.cmbMonth.Text)).ToString().PadLeft(2, '0'));

                DataSet ds = new DataSet();
                ds = smartViceData.Search(MessageSet);

                DataTable dt = ds.Tables[0].Copy();

                if (dt.Rows.Count < 1)
                    return -1;

                dtForchk = ds.Tables[0].Copy();

                if (this.grdSalary.Rows.Count > 1)
                    this.grdSalary.Rows.Clear();

                for (int row = 0; row < dt.Rows.Count; row++)
                {               
                    this.grdSalary.Rows.Add();

                    //입금은행            
                    this.grdSalary["deposit_bank_col", row].Value = dt.Rows[row]["bank_name"].ToString();
                    //입금계좌
                    this.grdSalary["deposit_account", row].Value = dt.Rows[row]["account_number"].ToString();
                    this.grdSalary["staff_id", row].Value = dt.Rows[row]["staff_id"].ToString();
                    this.grdSalary["staff_name", row].Value = dt.Rows[row]["staff_name"].ToString();

                    int contract_price = 0;
                    int income_tax = 0;
                    double business_tax = 0.0;

                    // 정규직
                    if (dt.Rows[row]["staff_id"].ToString().Substring(0, 5) == "JWP01")
                    {
                        //계약 금액 = 기본급여 ~ 야간수당 까지 합한 금액 (정규직이므로 식사비 포함)
                        contract_price = Convert.ToInt32(dt.Rows[row]["basic_pay"]) + Convert.ToInt32(dt.Rows[row]["bonus_cost"]) + Convert.ToInt32(dt.Rows[row]["position_cost"])
                            + Convert.ToInt32(dt.Rows[row]["month_leave_cost"]) + Convert.ToInt32(dt.Rows[row]["food_cost"]) + Convert.ToInt32(dt.Rows[row]["self_driving_subsidy"]) + Convert.ToInt32(dt.Rows[row]["overtime_cost"]);
                        //근로소득세 ( 국민연금비 ~ 특별세 까지 합한 금액, but 연도 말 (지방)소득세 제외 )+ 환급액( 년도 별 조회할 때만 값 존재 )
                        income_tax = Convert.ToInt32(dt.Rows[row]["nps_cost"]) + Convert.ToInt32(dt.Rows[row]["nhis_cost"]) + Convert.ToInt32(dt.Rows[row]["care_cost"])
                            + Convert.ToInt32(dt.Rows[row]["income_tax"]) + Convert.ToInt32(dt.Rows[row]["local_income_tax"]) + Convert.ToInt32(dt.Rows[row]["empoly_cost"]) + Convert.ToInt32(dt.Rows[row]["special_tax"]);
                        //지급액 = 계약금액 - 근로소득세
                        this.grdSalary["allowance", row].Value = contract_price - income_tax;
                    }
                    // 프리랜서
                    else if (dt.Rows[row]["staff_id"].ToString().Substring(0, 5) == "JWP02")
                    {
                        //계약 금액 = 기본급여 ~ 야간수당 까지 합한 금액 (프리랜서이므로 식사비 제외)
                        contract_price = Convert.ToInt32(dt.Rows[row]["basic_pay"]) + Convert.ToInt32(dt.Rows[row]["bonus_cost"]) + Convert.ToInt32(dt.Rows[row]["position_cost"])
                            + Convert.ToInt32(dt.Rows[row]["month_leave_cost"]) + Convert.ToInt32(dt.Rows[row]["self_driving_subsidy"]) + Convert.ToInt32(dt.Rows[row]["overtime_cost"]);
                        //사업소득세 ( 계약금액의 3.3% )
                        business_tax = contract_price * 0.033;
                        //지급액 = 계약금액 - 사업소득세 ( 지급액의 3.3% ) - 중식금액 
                        this.grdSalary["allowance", row].Value = contract_price - Convert.ToInt32(business_tax) - Convert.ToInt32(dt.Rows[row]["food_cost"]);
                    }
                    this.grdSalary["Commentofbankbook", row].Value = "젯스퍼트㈜";
                    this.grdSalary["Comment", row].Value = this.cmbMonth.SelectedItem.ToString() + "월분 급여";
                    this.grdSalary["ROWSTATE", row].Value = "";
                }
            }
            //새로운 달에 데이터 추가
            else
            {
                MessageSet.Clear();
                MessageSet.Add("SearchType", "seachStaffInfo");

                smartViceData = new RULE_SALARYPAPMANAGEMENT();
                DataTable dt = smartViceData.SearchCommon(MessageSet);

                if (dt.Rows.Count > 0)
                {
                    for (int row = 0; row < dt.Rows.Count; row++)
                    {
                        this.grdSalary.Rows.Add();
                        this.grdSalary["deposit_bank_col", row].Value = dt.Rows[row]["bank_name"].ToString();
                        this.grdSalary["deposit_account", row].Value = dt.Rows[row]["account_number"].ToString();
                        this.grdSalary["allowance", row].Value = "0";
                        this.grdSalary["staff_id", row].Value = dt.Rows[row]["staff_id"].ToString();
                        this.grdSalary["staff_name", row].Value = dt.Rows[row]["staff_name"].ToString();
                        this.grdSalary["Commentofbankbook", row].Value = "젯스퍼트㈜";
                        this.grdSalary["Comment", row].Value = this.cmbMonth.SelectedItem.ToString() + "월분 급여"; ;                        
                        this.grdSalary["ROWSTATE", row].Value = "";

                    }
                }
            }
            // 데이터에 맞게 칼럼 사이즈 조정하기
            //for (int i = 0; i < grdDepart.Columns.Count; i++)
            //{
            //    grdDepart.AutoResizeColumn(i, DataGridViewAutoSizeColumnMode.AllCells);
            //}
            grdSalary.RowHeadersVisible = false;
            grdSalary.AllowUserToAddRows = false;  // 빈레코드 표시 안하기
            grdSalary.AlternatingRowsDefaultCellStyle.BackColor = Color.AliceBlue; // 그리드 행에 색 입히기
            grdSalary.CurrentCell = null;

            cb.Checked = false;
            chkSearched = false;

            return 0;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Guid new_guid = Guid.NewGuid();

            int IdxNewRow = this.grdSalary.Rows.Add();
            this.grdSalary["allowance", IdxNewRow].Value = "0";
            this.grdSalary["Commentofbankbook", IdxNewRow].Value = "젯스퍼트㈜";
            this.grdSalary["Comment", IdxNewRow].Value = this.cmbMonth.SelectedItem.ToString() + "월분 급여"; 
            this.grdSalary["ROWSTATE", IdxNewRow].Value = "Added";
            //this.grdDepart["work_year", grdDepart.CurrentCell.RowIndex].Value = Convert.ToInt32(this.grdDepart["work_year", grdDepart.CurrentCell.RowIndex - 1].Value) + 1;            
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            for (int row = grdSalary.Rows.Count - 1 ; row > -1; row--) 
            {
                if (this.grdSalary.Rows[row].Cells["colCheck"].Value != null) 
                {                    
                        grdSalary.Rows.RemoveAt(row);                        
                }                                    
            }

            MessageBox.Show("삭제되었습니다.", "inform", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        //private void btnSave_Click(object sender, EventArgs e)
        //{
        //    int resultdt = 1;

        //    int chkdt = chkSave();

        //    if (chkdt == -1)
        //    {                
        //        return;
        //    }

        //    resultdt = Save();

        //    if (resultdt == -1)
        //        MessageBox.Show("저장에 실패하였습니다.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //    else if (resultdt == 1)
        //    {
        //        MessageSet.Clear();
        //        grdSalary.Rows.Clear();

        //        //MessageSet.Add("StartDate", this.StartDate.Value.ToShortDateString());
        //        //MessageSet.Add("EndDate", this.EndDate.Value.ToShortDateString());

        //        Search(MessageSet);
        //        chkSearched = false;

        //        MessageBox.Show("저장하였습니다.", "Inform", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //    }
        //    else if (resultdt == 0)
        //        MessageBox.Show("저장할 데이터가 존재하지 않습니다.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //    else
        //        MessageBox.Show("중복된 항목이 존재합니다.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //}

        //private int Save()
        //{
        //    bool chksave = true;
        //    DataSet ds = new DataSet();

        //    string QueryType = string.Empty;

        //    if (grdSalary.Rows.Count > 0)
        //    {
        //        //현재 그리드와 조회한 후 그리드를 비교 후 insert 또는 delete 작업            
        //        for (int grdrow = 0; grdrow < grdSalary.Rows.Count; grdrow++)
        //        {
        //            if (this.grdSalary.Rows[grdrow].Cells["ROWSTATE"].Value.ToString() == string.Empty)
        //                continue;

        //            if (this.grdSalary.Rows[grdrow].Cells["ROWSTATE"].Value.ToString() == "Added" || this.grdSalary.Rows[grdrow].Cells["ROWSTATE"].Value.ToString() == "Modified")
        //            {
        //                MessageSet.Clear();
        //                MessageSet.Add("QueryType", (this.grdSalary.Rows[grdrow].Cells["ROWSTATE"].Value.ToString() == "Added" ? "Insert" : "Update"));
        //                MessageSet.Add("valid_id", this.grdSalary.Rows[grdrow].Cells["valid_id"].Value.ToString());
        //                MessageSet.Add("monthly_date", this.grdSalary.Rows[grdrow].Cells["monthly_date"].Value.ToString().Substring(0,7));
        //                MessageSet.Add("staff_id", this.grdSalary.Rows[grdrow].Cells["cmbStaffId"].Value.ToString());
        //                MessageSet.Add("staff_name", this.grdSalary.Rows[grdrow].Cells["cmbStaffNm"].Value.ToString());
        //                MessageSet.Add("food_place", this.grdSalary.Rows[grdrow].Cells["food_place"].Value.ToString());
        //                MessageSet.Add("use_times", this.grdSalary.Rows[grdrow].Cells["use_times"].Value.ToString());                        
        //                MessageSet.Add("food_cost", this.grdSalary.Rows[grdrow].Cells["food_cost"].Value.ToString());

        //                chksave = smartViceData.Save(MessageSet);
                        
        //                if (!chksave)
        //                {
        //                    return -1;
        //                }
        //            }
        //            else if (this.grdSalary.Rows[grdrow].Cells["ROWSTATE"].Value.ToString() == "Deleted")
        //            {
        //                QueryType = "Delete";
        //                MessageSet.Clear();
        //                MessageSet.Add("QueryType", QueryType);
        //                MessageSet.Add("valid_id", this.grdSalary.Rows[grdrow].Cells["valid_id"].Value.ToString());
        //                chksave = smartViceData.Save(MessageSet);
                        
        //                if (!chksave)
        //                {
        //                    return -1;
        //                }
        //            }
        //        }
        //        return 1;
        //    }
        //    else
        //    {
        //        return 0;
        //    }            
        //}

        public int chkSave()
        {
            for (int row = 0; row < this.grdSalary.Rows.Count; row++)
            {
                if (this.grdSalary.Rows[row].Cells["monthly_date"].Value == null)
                {
                    MessageBox.Show(this.grdSalary.Columns["monthly_date"].HeaderText + " 값이 누락되었습니다.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return -1;
                }

                if (this.grdSalary.Rows[row].Cells["cmbStaffId"].Value == null)
                {
                    MessageBox.Show(this.grdSalary.Columns["cmbStaffId"].HeaderText + " 값이 누락되었습니다.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return -1;
                }

                if (this.grdSalary.Rows[row].Cells["cmbStaffNm"].Value == null)
                {
                    MessageBox.Show(this.grdSalary.Columns["cmbStaffNm"].HeaderText + " 값이 누락되었습니다.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return -1;
                }

                if (this.grdSalary.Rows[row].Cells["food_place"].Value == null)
                {
                    MessageBox.Show(this.grdSalary.Columns["food_place"].HeaderText + " 값이 누락되었습니다.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return -1;
                }

                if (this.grdSalary.Rows[row].Cells["use_times"].Value == null)
                {
                    MessageBox.Show(this.grdSalary.Columns["use_times"].HeaderText + " 값이 누락되었습니다.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return -1;
                }

                if (this.grdSalary.Rows[row].Cells["food_cost"].Value == null)
                {
                    MessageBox.Show(this.grdSalary.Columns["food_cost"].HeaderText + " 값이 누락되었습니다.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return -1;
                }
            }
            return 0;
        }
        private void grdSalary_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {

            if (chkSearched == true)
                return;

            //if (e.ColumnIndex == grdSalary.Columns["monthly_date"].Index || e.ColumnIndex == grdSalary.Columns["cmbStaffNm"].Index
            //     || e.ColumnIndex == grdSalary.Columns["cmbStaffId"].Index || e.ColumnIndex == grdSalary.Columns["food_place"].Index
            //     || e.ColumnIndex == grdSalary.Columns["use_times"].Index  || e.ColumnIndex == grdSalary.Columns["food_cost"].Index)
            // {
            //기존 데이터나 수정된 기존 데이터에만 modified 표시 
            if (this.grdSalary.Rows[e.RowIndex].Cells["ROWSTATE"].Value == null || this.grdSalary.Rows[e.RowIndex].Cells["ROWSTATE"].Value.ToString() == "Modified"
                    || this.grdSalary.Rows[e.RowIndex].Cells["ROWSTATE"].Value.ToString() == "")
                this.grdSalary.Rows[e.RowIndex].Cells["ROWSTATE"].Value = "Modified";

                chkSearched = false;
            //}

        }

        private void grdSalary_Scroll(object sender, ScrollEventArgs e)
        {
            DataGridView gv = (DataGridView)sender;

            Rectangle rtHeader = gv.DisplayRectangle;

            rtHeader.Height = gv.ColumnHeadersHeight / 2;

            gv.Invalidate(rtHeader);
         
        }
  
      
        // This event handler manually raises the CellValueChanged event 
        // by calling the CommitEdit method. 
        private void grdSalary_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (grdSalary.IsCurrentCellDirty)
            {
                // This fires the cell value changed handler below
                grdSalary.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }        

        private void btnExcelExport_Click(object sender, EventArgs e)
        {
            if (this.grdSalary.Rows.Count < 1)
            {
                MessageBox.Show("엑셀 파일로 저장할 데이터가 없습니다.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
            if (grdSalary.Rows.Count == 1)
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
                for (int col = 1; col < grdSalary.Columns.Count; col++)
                {
                    if (cellRowIndex == 1)
                    {
                        if (grdSalary.Columns[col].Visible == true)
                        {
                            worksheet.Cells[cellRowIndex, cellColumsIndex] = grdSalary.Columns[col].HeaderText;
                            worksheet.Cells[cellRowIndex, cellColumsIndex].Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Aqua);
                        }
                        else
                            continue;
                    }
                    cellColumsIndex++;
                }                

                cellColumsIndex = 1;
                cellRowIndex++;

                for (int row = 0; row < grdSalary.Rows.Count; row++)
                {
                    for (int col = 1; col < grdSalary.Columns.Count; col++)
                    {
                        if (grdSalary.Columns[col].Visible != true)
                        {
                            continue;
                        }

                        if (grdSalary.Rows[row].Cells[col].Value != null)
                            worksheet.Cells[cellRowIndex, cellColumsIndex] = grdSalary.Rows[row].Cells[col].Value.ToString();
                        else
                            worksheet.Cells[cellRowIndex, cellColumsIndex] = "";

                        cellColumsIndex++;
                    }
                    cellColumsIndex = 1;
                    cellRowIndex++;
                }
                //셀 테두리 범위 설정
                string startRange = "A1"; 
                string endRange = "F" + (grdSalary.Rows.Count + 1).ToString();

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

        private void cmbMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbMonth.SelectedIndex != 0)
                lbByMonth.Text = "*" + this.cmbMonth.SelectedItem.ToString() + "월분 급여 대장";
        }
        private void grdSalary_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
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
                cb.CheckedChanged += new EventHandler(grdSalary_CheckedChanged);

                this.grdSalary.Controls.Add(cb);
                //((DataGridView)sender).Controls.Add(cb);

                e.Handled = true;
            }
        }
        private void grdSalary_CheckedChanged(object sender, EventArgs e)
        {
            grdSalary.CurrentCell = null;

            if (cb.CheckState == CheckState.Checked)
                this.grdSalary.SelectAll();
            else
                grdSalary.ClearSelection();

            foreach (DataGridViewRow r in grdSalary.Rows)
            {
                if (cb.CheckState == CheckState.Checked)
                    r.Cells["colCheck"].Value = true;
                else
                    r.Cells["colCheck"].Value = false;

            }
        }

        private void SalaryPAPManagement_Shown(object sender, EventArgs e)
        {
            //grdSalary.ClearSelection();
            grdSalary.CurrentCell = null;
        }

        private void grdSalary_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)
                return;

            switch (grdSalary.Columns[e.ColumnIndex].Name)
            {
                case "colCheck":
                    this.grdSalary.CurrentRow.Selected = true;
                    break;
            }
        }
    }
}
