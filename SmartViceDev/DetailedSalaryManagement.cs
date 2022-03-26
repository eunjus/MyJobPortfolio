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
    public partial class DetailedSalaryManagement : Form
    {
        RULE_DETAILEDSALARYMANAGEMENT smartViceData = new RULE_DETAILEDSALARYMANAGEMENT();

        Dictionary<string, string> MessageSet = new Dictionary<string, string>();
        DataTable dtForchk = new DataTable();
        bool chkSearched = true;
                 

        public DetailedSalaryManagement()
        {
            InitializeComponent();

            eventslist(); //이벤트 처리 함수
            grdColumnAdd(); // 그리드 컬럼 형성
            comboboxListAdd();//검색 조건 초기화
        }

        private void DetailedSalaryManagement_Load(object sender, EventArgs e)
        {
            
            //this.StartDate.Value = Convert.ToDateTime((DateTime.Today.Year.ToString() + "-" + DateTime.Today.Month.ToString().PadLeft(2,'0') + "-" + "01"));
            //this.EndDate.Value = Convert.ToDateTime((DateTime.Today.Year.ToString() + "-" + DateTime.Today.Month.ToString().PadLeft(2, '0') + "-" + DateTime.DaysInMonth(DateTime.Today.Year,DateTime.Today.Month).ToString().PadLeft(2,'0')));

            this.grdDetailSalary.ClearSelection();
            
            this.cmbMonth.SelectedIndex = DateTime.Today.Month;
            lbByMonth.Text = "*" + this.cmbMonth.SelectedItem.ToString() + "월분 급여 대장";

            grdDetailSalary.RowHeadersVisible = false;

            btnSearch_Click(null, null);
        }

        private void eventslist()
        {            
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);            
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);            
            this.btnExcelExport.Click += new System.EventHandler(this.btnExcelExport_Click);
            this.btnDeleteAll.Click += new System.EventHandler(this.btnDeleteAll_Click);

            this.grdDetailSalary.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.grdDetailSalary_CellPainting);
            this.grdDetailSalary.ColumnWidthChanged += new System.Windows.Forms.DataGridViewColumnEventHandler(this.grdDetailSalary_ColumnWidthChanged);            
            this.grdDetailSalary.Paint += new System.Windows.Forms.PaintEventHandler(this.grdDetailSalary_Paint);
            this.grdDetailSalary.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grdDetailSalary_CellClick);
            this.grdDetailSalary.Scroll += new System.Windows.Forms.ScrollEventHandler(this.grdDetailSalary_Scroll);
            this.grdDetailSalary.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.grdDetailSalary_CellValueChanged);
            this.grdDetailSalary.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.grdDetailSalary_EditingControlShowing);
            
            this.cmbMonth.SelectedIndexChanged += new System.EventHandler(this.cmbMonth_SelectedIndexChanged);

            // 그리드에 있는 콤보박스 형태의 열 값이 변경될 때 발생하는 이벤트
            //this.grdDetailSalary.CurrentCellDirtyStateChanged += new System.EventHandler(this.grdDetailSalary_CurrentCellDirtyStateChanged);
            //this.grdDetailSalary.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.grdDetailSalary_KeyPress);
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

            ////근로소득세 합계
            //this.grdDetailSalary.Columns.Add("Sum_columns", "합계 종목");
            //this.grdDetailSalary.Columns["Sum_columns"].Width = 150;
            //this.grdDetailSalary.Columns["Sum_columns"].ReadOnly = true;
            ////사업소득세 합계
            //this.grdDetailSalary.Columns.Add("Sum_values", "합계");
            //this.grdDetailSalary.Columns["Sum_values"].Width = 120;
            //this.grdDetailSalary.Columns["Sum_values"].ReadOnly = true;

            //int IdxNewRow = this.grdDetailSalary.Rows.Add();
            //this.grdDetailSalary["Sum_columns", IdxNewRow].Value = "근로소득세 합계";
            //IdxNewRow = this.grdDetailSalary.Rows.Add();
            //this.grdDetailSalary["Sum_columns", IdxNewRow].Value = "사업소득세 합계";
            //IdxNewRow = this.grdDetailSalary.Rows.Add();
            //this.grdDetailSalary["Sum_columns", IdxNewRow].Value = "급여 지급 합계";

            ////합계 테이블 종목 셀 색상 추가
            //foreach (DataGridViewRow row in this.grdDetailSalary.Rows)
            //{
            //    row.Cells["Sum_columns"].Style.BackColor = Color.GreenYellow;
            //}
     
            var chkCol = new DataGridViewCheckBoxColumn
            {
                Name = "colCheck",
                HeaderText = ""
            };
            this.grdDetailSalary.Columns.Add(chkCol);
            this.grdDetailSalary.Columns["colCheck"].Width = 50;
            this.grdDetailSalary.Columns["colCheck"].Frozen = true;        
            //직책
            this.grdDetailSalary.Columns.Add("position_name", "직책");
            this.grdDetailSalary.Columns["position_name"].Width = 80;
            this.grdDetailSalary.Columns["position_name"].ReadOnly = true;
            this.grdDetailSalary.Columns["position_name"].Frozen = true;
            //부서
            this.grdDetailSalary.Columns.Add("depart_name", "부서");
            this.grdDetailSalary.Columns["depart_name"].Width = 80;
            this.grdDetailSalary.Columns["depart_name"].ReadOnly = true;
            this.grdDetailSalary.Columns["depart_name"].Frozen = true;
            //직원 사번
            this.grdDetailSalary.Columns.Add("staff_id", "직원 ID");
            this.grdDetailSalary.Columns["staff_id"].Visible = false;
            this.grdDetailSalary.Columns["staff_id"].ReadOnly = true;
            //직원명
            this.grdDetailSalary.Columns.Add("staff_name", "직원명");
            this.grdDetailSalary.Columns["staff_name"].Width = 100;
            this.grdDetailSalary.Columns["staff_name"].ReadOnly = true;
            this.grdDetailSalary.Columns["staff_name"].Frozen = true;
            //입사일
            this.grdDetailSalary.Columns.Add("join_dt", "입사일");
            this.grdDetailSalary.Columns["join_dt"].Width = 100;
            this.grdDetailSalary.Columns["join_dt"].ReadOnly = true;
            this.grdDetailSalary.Columns["join_dt"].Frozen = true;
            //퇴사일
            this.grdDetailSalary.Columns.Add("resign_dt", "퇴사일");
            this.grdDetailSalary.Columns["resign_dt"].Width = 100;
            this.grdDetailSalary.Columns["resign_dt"].ReadOnly = true;
            this.grdDetailSalary.Columns["resign_dt"].Frozen = true;
            //this.grdDetailSalary.Columns["resign_dt"].DividerWidth = 3; // 구분선 폭 설정        
            //기본 급여
            this.grdDetailSalary.Columns.Add("basic_pay", "기본 급여");            
            this.grdDetailSalary.Columns["basic_pay"].Width = 100;            
            //상여금
            this.grdDetailSalary.Columns.Add("bonus_cost", "상여금");
            this.grdDetailSalary.Columns["bonus_cost"].Width = 100;
            //직책수당
            this.grdDetailSalary.Columns.Add("position_cost", "직책수당");
            this.grdDetailSalary.Columns["position_cost"].Width = 100;
            //월차수당
            this.grdDetailSalary.Columns.Add("month_leave_cost", "월차수당");
            this.grdDetailSalary.Columns["month_leave_cost"].Width = 100;
            //식대
            this.grdDetailSalary.Columns.Add("food_cost", "식대");
            this.grdDetailSalary.Columns["food_cost"].Width = 100;
            //자가운전보조금
            this.grdDetailSalary.Columns.Add("self_driving_subsidy", "자가운전보조금");
            this.grdDetailSalary.Columns["self_driving_subsidy"].Width = 130;
            //야간근로수당
            this.grdDetailSalary.Columns.Add("overtime_cost", "야간근로수당");
            this.grdDetailSalary.Columns["overtime_cost"].Width = 130;
            //지급합계
            this.grdDetailSalary.Columns.Add("sum_givelist", "지급합계");
            this.grdDetailSalary.Columns["sum_givelist"].Width = 100;
            this.grdDetailSalary.Columns["sum_givelist"].ReadOnly = true;
            this.grdDetailSalary.Columns["sum_givelist"].HeaderCell.Style.BackColor = Color.GreenYellow;
            
            //국민연금
            this.grdDetailSalary.Columns.Add("nps_cost", "국민연금");
            this.grdDetailSalary.Columns["nps_cost"].Width = 100;
            //건강보험
            this.grdDetailSalary.Columns.Add("nhis_cost", "건강보험");
            this.grdDetailSalary.Columns["nhis_cost"].Width = 100;
            //장기요양보험
            this.grdDetailSalary.Columns.Add("care_cost", "장기요양보험");
            this.grdDetailSalary.Columns["care_cost"].Width = 130;
            //고용보험
            this.grdDetailSalary.Columns.Add("empoly_cost", "고용보험");
            this.grdDetailSalary.Columns["empoly_cost"].Width = 100;
            //소득세
            this.grdDetailSalary.Columns.Add("income_tax", "소득세");
            this.grdDetailSalary.Columns["income_tax"].Width = 90;
            //지방소득세
            this.grdDetailSalary.Columns.Add("local_income_tax", "지방소득세");
            this.grdDetailSalary.Columns["local_income_tax"].Width = 100;            
            //연말정산 소득세
            this.grdDetailSalary.Columns.Add("year_end_income_tax", "연말정산 소득세");
            this.grdDetailSalary.Columns["year_end_income_tax"].Width = 90;
            //연말정산 지방소득세
            this.grdDetailSalary.Columns.Add("year_end_local_tax", "연말정산 지방소득세");
            this.grdDetailSalary.Columns["year_end_local_tax"].Width = 100;            
            //농특세
            this.grdDetailSalary.Columns.Add("special_tax", "농특세");
            this.grdDetailSalary.Columns["special_tax"].Width = 100;
            //미제출비과세
            this.grdDetailSalary.Columns.Add("unsub_nontax", "미제출비과세");
            this.grdDetailSalary.Columns["unsub_nontax"].Width = 130;
            //공제합계
            this.grdDetailSalary.Columns.Add("sum_deduct", "공제합계");
            this.grdDetailSalary.Columns["sum_deduct"].Width = 100;
            this.grdDetailSalary.Columns["sum_deduct"].ReadOnly = true;
            this.grdDetailSalary.Columns["sum_deduct"].HeaderCell.Style.BackColor = Color.GreenYellow;
            //차인지급액
            this.grdDetailSalary.Columns.Add("diffpay_amount", "차인지급액");
            this.grdDetailSalary.Columns["diffpay_amount"].Width = 100;
            this.grdDetailSalary.Columns["diffpay_amount"].ReadOnly = true;
            this.grdDetailSalary.Columns["diffpay_amount"].HeaderCell.Style.BackColor
                = Color.GreenYellow;
            //비고
            this.grdDetailSalary.Columns.Add("COMMENT", "비고");
            this.grdDetailSalary.Columns["COMMENT"].Width = 100;
            //ROWSTATE
            this.grdDetailSalary.Columns.Add("RowState", "RowState");
            this.grdDetailSalary.Columns["RowState"].Visible = false; // 행의 신규,기존,수정,삭제 등의 상태를 나타냄.

            this.grdDetailSalary.AllowUserToAddRows = false;
            
        }

        private void grdDetailSalary_Paint(object sender, PaintEventArgs e)
        {
            DataGridView gv = (DataGridView)sender;

            string[] strHeaders = { "인적사항", "지급내역", "공제내역 및 차인지급액" };

            StringFormat format = new StringFormat();

            format.Alignment = StringAlignment.Center;

            format.LineAlignment = StringAlignment.Center;


            // 인적사항 Painting
            {

                Rectangle r1 = gv.GetCellDisplayRectangle(this.grdDetailSalary.Columns["colCheck"].Index, -1, false);

                int width1 = gv.GetCellDisplayRectangle(this.grdDetailSalary.Columns["staff_id"].Index, -1, false).Width;

                int width2 = gv.GetCellDisplayRectangle(this.grdDetailSalary.Columns["staff_name"].Index, -1, false).Width;

                int width3 = gv.GetCellDisplayRectangle(this.grdDetailSalary.Columns["depart_name"].Index, -1, false).Width;

                int width4 = gv.GetCellDisplayRectangle(this.grdDetailSalary.Columns["position_name"].Index, -1, false).Width;

                int width5 = gv.GetCellDisplayRectangle(this.grdDetailSalary.Columns["join_dt"].Index, -1, false).Width;

                int width6 = gv.GetCellDisplayRectangle(this.grdDetailSalary.Columns["resign_dt"].Index, -1, false).Width;

                r1.X += 1;

                r1.Y += 1;

                r1.Width = r1.Width + width1 + width2 + width3 + width4 + width5 + width6 - 2;

                r1.Height = (r1.Height / 2) - 2;

                e.Graphics.DrawRectangle(new Pen(gv.BackgroundColor), r1);

                e.Graphics.FillRectangle(new SolidBrush(gv.ColumnHeadersDefaultCellStyle.BackColor), r1);

                e.Graphics.DrawString(strHeaders[0],

                    gv.ColumnHeadersDefaultCellStyle.Font,

                    new SolidBrush(gv.ColumnHeadersDefaultCellStyle.ForeColor),

                    r1,

                    format);
            }

            //지급내역... Painting
            {
                Rectangle r2 = gv.GetCellDisplayRectangle(this.grdDetailSalary.Columns["basic_pay"].Index, -1, false);

                int width1 = gv.GetCellDisplayRectangle(this.grdDetailSalary.Columns["bonus_cost"].Index, -1, false).Width;
                int width2 = gv.GetCellDisplayRectangle(this.grdDetailSalary.Columns["position_cost"].Index, -1, false).Width;
                int width3 = gv.GetCellDisplayRectangle(this.grdDetailSalary.Columns["month_leave_cost"].Index, -1, false).Width;
                int width4 = gv.GetCellDisplayRectangle(this.grdDetailSalary.Columns["food_cost"].Index, -1, false).Width;
                int width5 = gv.GetCellDisplayRectangle(this.grdDetailSalary.Columns["self_driving_subsidy"].Index, -1, false).Width;
                int width6 = gv.GetCellDisplayRectangle(this.grdDetailSalary.Columns["overtime_cost"].Index, -1, false).Width;
                int width7 = gv.GetCellDisplayRectangle(this.grdDetailSalary.Columns["sum_givelist"].Index, -1, false).Width;

                r2.X += 1;

                r2.Y += 1;

                r2.Width = r2.Width + width1 + width2 + width3 + width4 + width5 + width6 + width7 - 2;

                r2.Height = (r2.Height / 2) - 2;

                e.Graphics.DrawRectangle(new Pen(gv.BackgroundColor), r2);

                e.Graphics.FillRectangle(new SolidBrush(gv.ColumnHeadersDefaultCellStyle.BackColor),

                    r2);

                e.Graphics.DrawString(strHeaders[1],

                    gv.ColumnHeadersDefaultCellStyle.Font,

                    new SolidBrush(gv.ColumnHeadersDefaultCellStyle.ForeColor),

                    r2,

                    format);

                //지급합계 컬럼 헤더 배경색 넣기
                Rectangle r2_sum_givelist = gv.GetCellDisplayRectangle(this.grdDetailSalary.Columns["sum_givelist"].Index, -1, false);
                
                r2_sum_givelist.Y += r2_sum_givelist.Height / 2;
                
                r2_sum_givelist.Height = (r2_sum_givelist.Height / 2) - 1; 


                e.Graphics.FillRectangle(new SolidBrush(Color.GreenYellow),

                r2_sum_givelist);

                e.Graphics.DrawString("지급합계",

                    gv.ColumnHeadersDefaultCellStyle.Font,

                    new SolidBrush(gv.ColumnHeadersDefaultCellStyle.ForeColor),

                    r2_sum_givelist,

                    format);               

            }

            //공제내역 및 차인지급액 Painting
            {
                Rectangle r3 = gv.GetCellDisplayRectangle(this.grdDetailSalary.Columns["nps_cost"].Index, -1, false);

                int width0 = gv.GetCellDisplayRectangle(this.grdDetailSalary.Columns["nhis_cost"].Index, -1, false).Width;
                int width1 = gv.GetCellDisplayRectangle(this.grdDetailSalary.Columns["care_cost"].Index, -1, false).Width;
                int width2 = gv.GetCellDisplayRectangle(this.grdDetailSalary.Columns["empoly_cost"].Index, -1, false).Width;
                int width3 = gv.GetCellDisplayRectangle(this.grdDetailSalary.Columns["income_tax"].Index, -1, false).Width;
                int width4 = gv.GetCellDisplayRectangle(this.grdDetailSalary.Columns["local_income_tax"].Index, -1, false).Width;
                int width5 = gv.GetCellDisplayRectangle(this.grdDetailSalary.Columns["year_end_income_tax"].Index, -1, false).Width;
                int width6 = gv.GetCellDisplayRectangle(this.grdDetailSalary.Columns["year_end_local_tax"].Index, -1, false).Width;
                int width7 = gv.GetCellDisplayRectangle(this.grdDetailSalary.Columns["special_tax"].Index, -1, false).Width;
                int width8 = gv.GetCellDisplayRectangle(this.grdDetailSalary.Columns["unsub_nontax"].Index, -1, false).Width;
                int width9 = gv.GetCellDisplayRectangle(this.grdDetailSalary.Columns["sum_deduct"].Index, -1, false).Width;
                int width10 = gv.GetCellDisplayRectangle(this.grdDetailSalary.Columns["diffpay_amount"].Index, -1, false).Width;
                int width11 = gv.GetCellDisplayRectangle(this.grdDetailSalary.Columns["COMMENT"].Index, -1, false).Width;

                r3.X += 1;

                r3.Y += 1;

                r3.Width = r3.Width + width0 + width1 + width2 + width3 + width4 + width5 + width6 + width7 + width8 + width9 + width10 + width11 - 2;

                r3.Height = (r3.Height / 2) - 2;

                e.Graphics.DrawRectangle(new Pen(gv.BackgroundColor), r3);

                e.Graphics.FillRectangle(new SolidBrush(gv.ColumnHeadersDefaultCellStyle.BackColor),

                    r3);

                e.Graphics.DrawString(strHeaders[2],

                    gv.ColumnHeadersDefaultCellStyle.Font,

                    new SolidBrush(gv.ColumnHeadersDefaultCellStyle.ForeColor),

                    r3,

                    format);

                //공제합계 컬럼 헤더 배경색 넣기
                Rectangle r3_sum_deduct = gv.GetCellDisplayRectangle(this.grdDetailSalary.Columns["sum_deduct"].Index, -1, false);

                r3_sum_deduct.Y += r3_sum_deduct.Height / 2;

                r3_sum_deduct.Width -= 1;
                r3_sum_deduct.Height = (r3_sum_deduct.Height / 2) - 1;


                e.Graphics.FillRectangle(new SolidBrush(Color.GreenYellow),

                r3_sum_deduct);

                e.Graphics.DrawString("공제합계",

                    gv.ColumnHeadersDefaultCellStyle.Font,

                    new SolidBrush(gv.ColumnHeadersDefaultCellStyle.ForeColor),

                    r3_sum_deduct,

                    format);

                //차인지급액 컬럼 헤더 배경색 넣기
                Rectangle r3_diffpay_amount = gv.GetCellDisplayRectangle(this.grdDetailSalary.Columns["diffpay_amount"].Index, -1, false);

                r3_diffpay_amount.Y += r3_diffpay_amount.Height / 2;

                r3_diffpay_amount.Height = (r3_diffpay_amount.Height / 2) - 1;


                e.Graphics.FillRectangle(new SolidBrush(Color.GreenYellow),

                r3_diffpay_amount);

                e.Graphics.DrawString("차인지급액",

                    gv.ColumnHeadersDefaultCellStyle.Font,

                    new SolidBrush(gv.ColumnHeadersDefaultCellStyle.ForeColor),

                    r3_diffpay_amount,

                    format);
            }
        }

        private void grdDetailSalary_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            //체크박스 컬럼에 전체선택 기능 넣기 위한 페인팅
            //if (e.ColumnIndex == 0 && e.RowIndex == -1)
            //{
            //    e.PaintBackground(e.ClipBounds, false);

            //    Point pt = e.CellBounds.Location;  // where you want the bitmap in the cell

            //    int nChkBoxWidth = 15;
            //    int nChkBoxHeight = 15;
            //    int offsetx = (e.CellBounds.Width - nChkBoxWidth) / 2;
            //    int offsety = (e.CellBounds.Height - nChkBoxHeight) / 2;

            //    pt.X += offsetx;
            //    pt.Y += offsety;

            //    CheckBox cb = new CheckBox();
            //    cb.Size = new Size(nChkBoxWidth, nChkBoxHeight);
            //    cb.Location = pt;
            //    cb.CheckedChanged += new EventHandler(grdDetailSalaryCheckBox_CheckedChanged);

            //    ((DataGridView)sender).Controls.Add(cb);

            //    e.Handled = true;
            //}

            if (e.RowIndex == -1 && e.ColumnIndex > -1)
            {

                Rectangle r = e.CellBounds;

                r.Y += e.CellBounds.Height / 2;

                r.Height = e.CellBounds.Height / 2;


                e.PaintBackground(r, true);


                e.PaintContent(r);

                e.Handled = true;

            }
        }

        private void grdDetailSalary_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
        {
            DataGridView gv = (DataGridView)sender;

            Rectangle rtHeader = gv.DisplayRectangle;

            rtHeader.Height = gv.ColumnHeadersHeight / 2;

            gv.Invalidate(rtHeader);            

        }
        private void grdDetailSalary_Scroll(object sender, ScrollEventArgs e)
        {
            DataGridView gv = (DataGridView)sender;

            Rectangle rtHeader = gv.DisplayRectangle;

            rtHeader.Height = gv.ColumnHeadersHeight / 2;

            gv.Invalidate(rtHeader);            
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
                    int resultchk = Search();

                    if (resultchk == -1)
                    {
                        MessageBox.Show("조회된 데이터가 없습니다.", "Inform", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.grdDetailSalary.Rows.Clear();
                        chkSearched = false;                       
                        return;
                    }
                }
                else if (dialogResult == DialogResult.Cancel || dialogResult == DialogResult.No)
                {
                    return;
                }
            }
         
            int resultchk2 = Search();

            if (resultchk2 == -1)
            {
                MessageBox.Show("조회된 데이터가 없습니다.", "Inform", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.grdDetailSalary.Rows.Clear();
                chkSearched = false;               
                return;
            }
        }

        public int Chksearch()
        {
            if (chkSearched == false) // 조회한 이력이 있을때만 그리드 체크 
            {

                for (int grdrow = 0; grdrow < this.grdDetailSalary.Rows.Count; grdrow++)
                {
                    if (this.grdDetailSalary.Rows[grdrow].Cells["ROWSTATE"].Value != null)
                    {
                        if (this.grdDetailSalary.Rows[grdrow].Cells["ROWSTATE"].Value.ToString() == "Modified" || this.grdDetailSalary.Rows[grdrow].Cells["ROWSTATE"].Value.ToString() == "Added"
                            || this.grdDetailSalary.Rows[grdrow].Cells["ROWSTATE"].Value.ToString() == "Deleted")
                        {
                            return 1;
                        }
                    }
                }   
            }
            return 0;
        }

        public int Search()
        {     
            if (this.grdDetailSalary.Rows.Count > 0)
            {
                this.grdDetailSalary.Rows.Clear();
            }
            //해당 월에 데이터가 있는지 확인 후, 있으면 가져오고 없으면 새로 추가
            MessageSet.Clear();
            MessageSet.Add("StartDate", this.cmbYear.Text + "-" + this.cmbMonth.Text.PadLeft(2, '0') + "-01");
            MessageSet.Add("EndDate", this.cmbYear.Text + "-" + this.cmbMonth.Text.PadLeft(2, '0') + "-" +
                                        DateTime.DaysInMonth(Convert.ToInt32(this.cmbYear.Text), Convert.ToInt32(this.cmbMonth.Text)).ToString().PadLeft(2, '0'));
            MessageSet.Add("SearchType", "chkNewMonth");

            smartViceData = new RULE_DETAILEDSALARYMANAGEMENT();
            DataSet chkdt = smartViceData.Search(MessageSet);
            MessageSet.Clear();
            //기존 데이터가 있을 때 
            if (chkdt.Tables[0].Rows.Count > 0)
            {
                MessageSet.Add("StartDate", this.cmbYear.Text + "-" + this.cmbMonth.Text.PadLeft(2, '0') + "-01");
                MessageSet.Add("EndDate", this.cmbYear.Text + "-" + this.cmbMonth.Text.PadLeft(2, '0') + "-" +
                                            DateTime.DaysInMonth(Convert.ToInt32(this.cmbYear.Text), Convert.ToInt32(this.cmbMonth.Text)).ToString().PadLeft(2, '0'));
                MessageSet.Add("SearchType", "MainSearch");

                DataSet ds = new DataSet();
                ds = smartViceData.Search(MessageSet);

                DataTable dt = ds.Tables[0].Copy();

                if (dt.Rows.Count < 1)
                    return -1;

                dtForchk = ds.Tables[0].Copy();

                if (this.grdDetailSalary.Rows.Count > 1)
                    this.grdDetailSalary.Rows.Clear();

                for (int row = 0; row < dt.Rows.Count; row++)
                {
                    this.grdDetailSalary.Rows.Add();
                    this.grdDetailSalary["staff_id", row].Value = dt.Rows[row]["staff_id"].ToString();
                    this.grdDetailSalary["staff_name", row].Value = dt.Rows[row]["staff_name"].ToString();
                    this.grdDetailSalary["depart_name", row].Value = dt.Rows[row]["depart_name"].ToString();
                    this.grdDetailSalary["position_name", row].Value = dt.Rows[row]["position_name"].ToString();
                    this.grdDetailSalary["join_dt", row].Value = dt.Rows[row]["join_dt"].ToString();
                    this.grdDetailSalary["resign_dt", row].Value = dt.Rows[row]["resign_dt"].ToString();
                    this.grdDetailSalary["basic_pay", row].Value = dt.Rows[row]["basic_pay"].ToString();
                    this.grdDetailSalary["bonus_cost", row].Value = dt.Rows[row]["bonus_cost"].ToString();
                    this.grdDetailSalary["position_cost", row].Value = dt.Rows[row]["position_cost"].ToString();
                    this.grdDetailSalary["month_leave_cost", row].Value = dt.Rows[row]["month_leave_cost"].ToString();
                    this.grdDetailSalary["food_cost", row].Value = dt.Rows[row]["food_cost"].ToString();
                    this.grdDetailSalary["self_driving_subsidy", row].Value = dt.Rows[row]["self_driving_subsidy"].ToString();
                    this.grdDetailSalary["overtime_cost", row].Value = dt.Rows[row]["overtime_cost"].ToString();
                    this.grdDetailSalary["sum_givelist", row].Value = Convert.ToInt32(dt.Rows[row]["basic_pay"]) + Convert.ToInt32(dt.Rows[row]["bonus_cost"]) + Convert.ToInt32(dt.Rows[row]["position_cost"])
                    + Convert.ToInt32(dt.Rows[row]["month_leave_cost"]) + Convert.ToInt32(dt.Rows[row]["food_cost"]) + Convert.ToInt32(dt.Rows[row]["self_driving_subsidy"]) + Convert.ToInt32(dt.Rows[row]["overtime_cost"]);
                    this.grdDetailSalary["nps_cost", row].Value = dt.Rows[row]["nps_cost"].ToString();
                    this.grdDetailSalary["nhis_cost", row].Value = dt.Rows[row]["nhis_cost"].ToString();
                    this.grdDetailSalary["care_cost", row].Value = dt.Rows[row]["care_cost"].ToString();
                    this.grdDetailSalary["empoly_cost", row].Value = dt.Rows[row]["empoly_cost"].ToString();
                    this.grdDetailSalary["income_tax", row].Value = dt.Rows[row]["income_tax"].ToString();
                    this.grdDetailSalary["local_income_tax", row].Value = dt.Rows[row]["local_income_tax"].ToString();
                    this.grdDetailSalary["year_end_income_tax", row].Value = dt.Rows[row]["year_end_income_tax"].ToString();
                    this.grdDetailSalary["year_end_local_tax", row].Value = dt.Rows[row]["year_end_local_income_tax"].ToString();
                    this.grdDetailSalary["special_tax", row].Value = dt.Rows[row]["special_tax"].ToString();
                    this.grdDetailSalary["sum_deduct", row].Value = Convert.ToInt32(dt.Rows[row]["nps_cost"]) + Convert.ToInt32(dt.Rows[row]["nhis_cost"]) + Convert.ToInt32(dt.Rows[row]["care_cost"]) 
                    +Convert.ToInt32(dt.Rows[row]["empoly_cost"]) + Convert.ToInt32(dt.Rows[row]["income_tax"]) + Convert.ToInt32(dt.Rows[row]["local_income_tax"])
                    + Convert.ToInt32(dt.Rows[row]["year_end_income_tax"]) + Convert.ToInt32(dt.Rows[row]["year_end_local_income_tax"]) + Convert.ToInt32(dt.Rows[row]["special_tax"]);
                    this.grdDetailSalary["diffpay_amount", row].Value = Convert.ToInt32(this.grdDetailSalary["sum_givelist", row].Value) - Convert.ToInt32(this.grdDetailSalary["sum_deduct", row].Value);
                    this.grdDetailSalary["COMMENT", row].Value = dt.Rows[row]["COMMENT"].ToString();
                    this.grdDetailSalary["ROWSTATE", row].Value = "";                   

                }
            }
            //새로운 달에 데이터 추가
            else
            {
                MessageSet.Clear();
                MessageSet.Add("SearchType", "seachStaffInfo");

                smartViceData = new RULE_DETAILEDSALARYMANAGEMENT();
                DataTable dt = smartViceData.SearchCommon(MessageSet);

                if (dt.Rows.Count > 0)
                {
                    for (int row = 0; row < dt.Rows.Count; row++)
                    {
                        this.grdDetailSalary.Rows.Add();
                        this.grdDetailSalary["staff_id", row].Value = dt.Rows[row]["staff_id"].ToString();
                        this.grdDetailSalary["staff_name", row].Value = dt.Rows[row]["staff_name"].ToString();
                        this.grdDetailSalary["depart_name", row].Value = dt.Rows[row]["depart_name"].ToString();
                        this.grdDetailSalary["position_name", row].Value = dt.Rows[row]["position_name"].ToString();
                        this.grdDetailSalary["join_dt", row].Value = dt.Rows[row]["join_dt"].ToString();
                        this.grdDetailSalary["resign_dt", row].Value = dt.Rows[row]["resign_dt"].ToString();
                        this.grdDetailSalary["ROWSTATE", row].Value = "";

                    }
                }
            }

            // 데이터에 맞게 칼럼 사이즈 조정하기
            //for (int i = 0; i < grdDepart.Columns.Count; i++)
            //{
            //    grdDepart.AutoResizeColumn(i, DataGridViewAutoSizeColumnMode.AllCells);
            //}
            grdDetailSalary.RowHeadersVisible = false;
            grdDetailSalary.AllowUserToAddRows = false;  // 빈레코드 표시 안하기
            grdDetailSalary.AlternatingRowsDefaultCellStyle.BackColor = Color.AliceBlue; // 그리드 행에 색 입히기

            chkSearched = false;

            return 0;
        }

        //private void btnAdd_Click(object sender, EventArgs e)
        //{
        //    Guid new_guid = Guid.NewGuid();

        //    int IdxNewRow = this.grdDetailSalary.Rows.Add();
        //    this.grdDetailSalary["valid_id", IdxNewRow].Value = new_guid.ToString();
        //    //this.grdDetailSalary["cmbStaffNm", IdxNewRow].Value = "이은주";
        //    //this.grdDetailSalary["cmbStaffId", IdxNewRow].Value = "JWP001_J";
        //    this.grdDetailSalary["contract_price", IdxNewRow].Value = "0";
        //    this.grdDetailSalary["business_tax", IdxNewRow].Value = "0";
        //    this.grdDetailSalary["income_tax", IdxNewRow].Value = "0";
        //    this.grdDetailSalary["charge_food", IdxNewRow].Value = "0";
        //    this.grdDetailSalary["cnt_meal_ticket", IdxNewRow].Value = "0";
        //    this.grdDetailSalary["year_end_income_tax", IdxNewRow].Value = "0";
        //    this.grdDetailSalary["year_end_local_tax", IdxNewRow].Value = "0";
        //    this.grdDetailSalary["year_end_carryback", IdxNewRow].Value = "0";
        //    this.grdDetailSalary["allowance", IdxNewRow].Value = "0";
        //    this.grdDetailSalary["ROWSTATE", IdxNewRow].Value = "Added";
        //    //this.grdDepart["work_year", grdDepart.CurrentCell.RowIndex].Value = Convert.ToInt32(this.grdDepart["work_year", grdDepart.CurrentCell.RowIndex - 1].Value) + 1;            
        //}

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (DataGridViewRow dr in grdDetailSalary.Rows)
                {
                    if (dr.Cells["colCheck"].Value != null) // 체크된 행 자체가 삭제되는 것이 아니라 급여명세 관련 셀들의 데이터만 삭제됨. 
                    {
                        for(int col = this.grdDetailSalary.Columns["basic_pay"].Index; col < this.grdDetailSalary.Columns["diffpay_amount"].Index + 1; col++)
                        {
                            dr.Cells[col].Value = null;
                        }                                     
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            MessageBox.Show("삭제되었습니다.", "inform", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            int resultdt = 1;

            int chkdt = chkSave();

            resultdt = Save();

            if (resultdt == -1)
                MessageBox.Show("저장에 실패하였습니다.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else if (resultdt == 1)
            {                               
                Search();
                chkSearched = false;

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

            ////현재 저장하려는 값들이 기존에 있던 데이터인지 확인
            //MessageSet.Clear();
            //MessageSet.Add("SearchType", "chkNewMonth");

            //DataSet chkdt = smartViceData.Search(MessageSet);
            //MessageSet.Clear();
            //if (chkdt.Tables[0].Rows.Count > 0)            
            //    MessageSet.Add("QueryType", "Update");           
            //else
            //    MessageSet.Add("QueryType", "Insert");


            if (grdDetailSalary.Rows.Count > 0)
            {
                //현재 그리드와 조회한 후 그리드를 비교 후 insert 또는 delete 작업            
                for (int grdrow = 0; grdrow < grdDetailSalary.Rows.Count; grdrow++)
                {
                        //CHECK INSERT OR UPDATE 
                        MessageSet.Clear();
                        MessageSet.Add("StartDate", this.cmbYear.Text + "-" + this.cmbMonth.Text.PadLeft(2, '0') + "-01");
                        MessageSet.Add("EndDate", this.cmbYear.Text + "-" + this.cmbMonth.Text.PadLeft(2, '0') + "-" +
                                                    DateTime.DaysInMonth(Convert.ToInt32(this.cmbYear.Text), Convert.ToInt32(this.cmbMonth.Text)).ToString().PadLeft(2, '0'));
                        MessageSet.Add("SearchType", "chkNewMonth");
                        MessageSet.Add("staff_id", this.grdDetailSalary.Rows[grdrow].Cells["staff_id"].Value.ToString());

                        RULE_DETAILEDSALARYMANAGEMENT smartViceData = new RULE_DETAILEDSALARYMANAGEMENT();
                        DataSet chkdt = smartViceData.Search(MessageSet);

                        MessageSet.Clear();

                        if (chkdt.Tables[0].Rows.Count > 0)
                            MessageSet.Add("QueryType", "Update");
                        else
                            MessageSet.Add("QueryType", "Insert");
                    
                        MessageSet.Add("pay_month", this.cmbYear.Text + "-" + this.cmbMonth.Text.PadLeft(2, '0') + "-01");
                        MessageSet.Add("staff_id", this.grdDetailSalary.Rows[grdrow].Cells["staff_id"].Value.ToString());
                        
                        if(this.grdDetailSalary.Rows[grdrow].Cells["basic_pay"].Value != null && this.grdDetailSalary.Rows[grdrow].Cells["basic_pay"].Value.ToString() != string.Empty)
                        MessageSet.Add("basic_pay", this.grdDetailSalary.Rows[grdrow].Cells["basic_pay"].Value.ToString());
                        if (this.grdDetailSalary.Rows[grdrow].Cells["bonus_cost"].Value != null && this.grdDetailSalary.Rows[grdrow].Cells["bonus_cost"].Value.ToString() != string.Empty)
                        MessageSet.Add("bonus_cost", this.grdDetailSalary.Rows[grdrow].Cells["bonus_cost"].Value.ToString());
                        if (this.grdDetailSalary.Rows[grdrow].Cells["position_cost"].Value != null && this.grdDetailSalary.Rows[grdrow].Cells["position_cost"].Value.ToString() != string.Empty)
                        MessageSet.Add("position_cost", this.grdDetailSalary.Rows[grdrow].Cells["position_cost"].Value.ToString());
                        if (this.grdDetailSalary.Rows[grdrow].Cells["month_leave_cost"].Value != null && this.grdDetailSalary.Rows[grdrow].Cells["month_leave_cost"].Value.ToString() != string.Empty)
                        MessageSet.Add("month_leave_cost", this.grdDetailSalary.Rows[grdrow].Cells["month_leave_cost"].Value.ToString());
                        if (this.grdDetailSalary.Rows[grdrow].Cells["food_cost"].Value != null && this.grdDetailSalary.Rows[grdrow].Cells["food_cost"].Value.ToString() != string.Empty)
                        MessageSet.Add("food_cost", this.grdDetailSalary.Rows[grdrow].Cells["food_cost"].Value.ToString());
                        if (this.grdDetailSalary.Rows[grdrow].Cells["self_driving_subsidy"].Value != null && this.grdDetailSalary.Rows[grdrow].Cells["self_driving_subsidy"].Value.ToString() != string.Empty)
                        MessageSet.Add("self_driving_subsidy", this.grdDetailSalary.Rows[grdrow].Cells["self_driving_subsidy"].Value.ToString());
                        if (this.grdDetailSalary.Rows[grdrow].Cells["overtime_cost"].Value != null && this.grdDetailSalary.Rows[grdrow].Cells["overtime_cost"].Value.ToString() != string.Empty)
                        MessageSet.Add("overtime_cost", this.grdDetailSalary.Rows[grdrow].Cells["overtime_cost"].Value.ToString());
                        if (this.grdDetailSalary.Rows[grdrow].Cells["nps_cost"].Value != null && this.grdDetailSalary.Rows[grdrow].Cells["nps_cost"].Value.ToString() != string.Empty)
                        MessageSet.Add("nps_cost", this.grdDetailSalary.Rows[grdrow].Cells["nps_cost"].Value.ToString());
                        if (this.grdDetailSalary.Rows[grdrow].Cells["nhis_cost"].Value != null && this.grdDetailSalary.Rows[grdrow].Cells["nhis_cost"].Value.ToString() != string.Empty)
                        MessageSet.Add("nhis_cost", this.grdDetailSalary.Rows[grdrow].Cells["nhis_cost"].Value.ToString());
                        if (this.grdDetailSalary.Rows[grdrow].Cells["care_cost"].Value != null && this.grdDetailSalary.Rows[grdrow].Cells["care_cost"].Value.ToString() != string.Empty)
                        MessageSet.Add("care_cost", this.grdDetailSalary.Rows[grdrow].Cells["care_cost"].Value.ToString());
                        if (this.grdDetailSalary.Rows[grdrow].Cells["empoly_cost"].Value != null && this.grdDetailSalary.Rows[grdrow].Cells["empoly_cost"].Value.ToString() != string.Empty)
                        MessageSet.Add("empoly_cost", this.grdDetailSalary.Rows[grdrow].Cells["empoly_cost"].Value.ToString());
                        if (this.grdDetailSalary.Rows[grdrow].Cells["income_tax"].Value != null && this.grdDetailSalary.Rows[grdrow].Cells["income_tax"].Value.ToString() != string.Empty)
                        MessageSet.Add("income_tax", this.grdDetailSalary.Rows[grdrow].Cells["income_tax"].Value.ToString());
                        if (this.grdDetailSalary.Rows[grdrow].Cells["local_income_tax"].Value != null && this.grdDetailSalary.Rows[grdrow].Cells["local_income_tax"].Value.ToString() != string.Empty)
                        MessageSet.Add("local_income_tax", this.grdDetailSalary.Rows[grdrow].Cells["local_income_tax"].Value.ToString());
                        if (this.grdDetailSalary.Rows[grdrow].Cells["year_end_income_tax"].Value != null && this.grdDetailSalary.Rows[grdrow].Cells["year_end_income_tax"].Value.ToString() != string.Empty)
                        MessageSet.Add("year_end_income_tax", this.grdDetailSalary.Rows[grdrow].Cells["year_end_income_tax"].Value.ToString());
                        if (this.grdDetailSalary.Rows[grdrow].Cells["year_end_local_tax"].Value != null && this.grdDetailSalary.Rows[grdrow].Cells["year_end_local_tax"].Value.ToString() != string.Empty)
                        MessageSet.Add("year_end_local_tax", this.grdDetailSalary.Rows[grdrow].Cells["year_end_local_tax"].Value.ToString());
                        if (this.grdDetailSalary.Rows[grdrow].Cells["special_tax"].Value != null && this.grdDetailSalary.Rows[grdrow].Cells["special_tax"].Value.ToString() != string.Empty)
                        MessageSet.Add("special_tax", this.grdDetailSalary.Rows[grdrow].Cells["special_tax"].Value.ToString());
                        if (this.grdDetailSalary.Rows[grdrow].Cells["COMMENT"].Value != null && this.grdDetailSalary.Rows[grdrow].Cells["COMMENT"].Value.ToString() != string.Empty)
                        MessageSet.Add("COMMENT", this.grdDetailSalary.Rows[grdrow].Cells["COMMENT"].Value.ToString());
                    
                        smartViceData = new RULE_DETAILEDSALARYMANAGEMENT();
                        chksave = smartViceData.Save(MessageSet);
                        MessageSet.Clear();

                    if (!chksave)
                        {
                            return -1;
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
            for (int row = 0; row < this.grdDetailSalary.Rows.Count; row++)
            {
                //if (this.grdDetailSalary.Rows[row].Cells["monthly_date"].Value == null)
                //{
                //    MessageBox.Show(this.grdDetailSalary.Columns["monthly_date"].HeaderText + " 값이 누락되었습니다.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //    return -1;
                //}

                //if (this.grdDetailSalary.Rows[row].Cells["cmbStaffId"].Value == null)
                //{
                //    MessageBox.Show(this.grdDetailSalary.Columns["cmbStaffId"].HeaderText + " 값이 누락되었습니다.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //    return -1;
                //}

                //if (this.grdDetailSalary.Rows[row].Cells["cmbStaffNm"].Value == null)
                //{
                //    MessageBox.Show(this.grdDetailSalary.Columns["cmbStaffNm"].HeaderText + " 값이 누락되었습니다.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //    return -1;
                //}

                //if (this.grdDetailSalary.Rows[row].Cells["food_place"].Value == null)
                //{
                //    MessageBox.Show(this.grdDetailSalary.Columns["food_place"].HeaderText + " 값이 누락되었습니다.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //    return -1;
                //}

                //if (this.grdDetailSalary.Rows[row].Cells["use_times"].Value == null)
                //{
                //    MessageBox.Show(this.grdDetailSalary.Columns["use_times"].HeaderText + " 값이 누락되었습니다.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //    return -1;
                //}

                //if (this.grdDetailSalary.Rows[row].Cells["food_cost"].Value == null)
                //{
                //    MessageBox.Show(this.grdDetailSalary.Columns["food_cost"].HeaderText + " 값이 누락되었습니다.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //    return -1;
                //}
            }
            return 0;
        }
        private void grdDetailSalary_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {            
            MessageSet.Clear();
            DataTable dt = new DataTable();
            smartViceData = new RULE_DETAILEDSALARYMANAGEMENT();

            switch (grdDetailSalary.Columns[e.ColumnIndex].Name)
            {
                //개개인 지급내역 합
                case "basic_pay":                    
                case "bonus_cost":                  
                case "position_cost":                   
                case "month_leave_cost":               
                case "food_cost":                  
                case "self_driving_subsidy":               
                case "overtime_cost":
                    if (this.grdDetailSalary.Rows[e.RowIndex].Cells[e.ColumnIndex].Value == null)
                        break;

                    if (this.grdDetailSalary.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() != "")
                    {
                        decimal sum_givelist = 0;

                        for (int give_col = this.grdDetailSalary.Columns["basic_pay"].Index; give_col < this.grdDetailSalary.Columns["overtime_cost"].Index + 1; give_col++)
                        {
                            if (this.grdDetailSalary.Rows[e.RowIndex].Cells[give_col].Value == null)
                                continue;

                            if (this.grdDetailSalary.Rows[e.RowIndex].Cells[give_col].Value.ToString() != "")
                                sum_givelist += Convert.ToDecimal(this.grdDetailSalary.Rows[e.RowIndex].Cells[give_col].Value.ToString());
                        }

                        this.grdDetailSalary.Rows[e.RowIndex].Cells["sum_givelist"].Value = sum_givelist;
                    }
                    break;
                case "nps_cost":
                case "nhis_cost":
                case "care_cost":
                case "empoly_cost":
                case "local_income_tax":
                case "year_end_income_tax":
                case "year_end_local_tax":
                case "special_tax":
                    if (this.grdDetailSalary.Rows[e.RowIndex].Cells[e.ColumnIndex].Value == null)
                        break;

                    if (this.grdDetailSalary.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() != "")
                    {
                        decimal sum_deduct = 0;

                        for (int deduct_col = this.grdDetailSalary.Columns["nps_cost"].Index; deduct_col < this.grdDetailSalary.Columns["year_end_local_tax"].Index + 1; deduct_col++)
                        {
                            if (this.grdDetailSalary.Rows[e.RowIndex].Cells[deduct_col].Value == null)
                                continue;

                            if (this.grdDetailSalary.Rows[e.RowIndex].Cells[deduct_col].Value.ToString() != "")
                                sum_deduct += Convert.ToDecimal(this.grdDetailSalary.Rows[e.RowIndex].Cells[deduct_col].Value.ToString());
                        }

                        this.grdDetailSalary.Rows[e.RowIndex].Cells["sum_deduct"].Value = sum_deduct;
                    }
                    break;
                case "sum_givelist":
                case "sum_deduct":
                    if (this.grdDetailSalary.Rows[e.RowIndex].Cells["sum_givelist"].Value == null)                    
                        break;

                    if ( this.grdDetailSalary.Rows[e.RowIndex].Cells["sum_givelist"].Value.ToString() != "" )
                    {
                        if (this.grdDetailSalary.Rows[e.RowIndex].Cells["sum_givelist"].Value != null)
                        {
                            if (this.grdDetailSalary.Rows[e.RowIndex].Cells["sum_givelist"].Value.ToString() != "")
                            {
                                this.grdDetailSalary.Rows[e.RowIndex].Cells["diffpay_amount"].Value = Convert.ToInt32(this.grdDetailSalary.Rows[e.RowIndex].Cells["sum_givelist"].Value)
                                                                                         - Convert.ToInt32(this.grdDetailSalary.Rows[e.RowIndex].Cells["sum_deduct"].Value);
                            }
                            else
                            {
                                this.grdDetailSalary.Rows[e.RowIndex].Cells["diffpay_amount"].Value = Convert.ToInt32(this.grdDetailSalary.Rows[e.RowIndex].Cells["sum_givelist"].Value);
                            }
                        }
                    }
                        break;

            }

            if (chkSearched == true)
                return;

            //if (e.ColumnIndex == grdDetailSalary.Columns["monthly_date"].Index || e.ColumnIndex == grdDetailSalary.Columns["cmbStaffNm"].Index
            //     || e.ColumnIndex == grdDetailSalary.Columns["cmbStaffId"].Index || e.ColumnIndex == grdDetailSalary.Columns["food_place"].Index
            //     || e.ColumnIndex == grdDetailSalary.Columns["use_times"].Index || e.ColumnIndex == grdDetailSalary.Columns["food_cost"].Index)
            //{
            //기존 데이터나 수정된 기존 데이터에만 modified 표시
            if (this.grdDetailSalary.Rows[e.RowIndex].Cells["ROWSTATE"].Value == null || this.grdDetailSalary.Rows[e.RowIndex].Cells["ROWSTATE"].Value.ToString() == "Modified"
                    || this.grdDetailSalary.Rows[e.RowIndex].Cells["ROWSTATE"].Value.ToString() == "")
                    this.grdDetailSalary.Rows[e.RowIndex].Cells["ROWSTATE"].Value = "Modified";

                chkSearched = false;
            //}

        }
   
        private void grdDetailSalary_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)
                return;

            switch (grdDetailSalary.Columns[e.ColumnIndex].Name)
            {
                case "colCheck":                    
                        this.grdDetailSalary.CurrentRow.Selected = true;             
                    break;
            }
        }      
      
        //// This event handler manually raises the CellValueChanged event 
        //// by calling the CommitEdit method. 
        //private void grdDetailSalary_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        //{
        //    if (grdDetailSalary.IsCurrentCellDirty)
        //    {
        //        // This fires the cell value changed handler below
        //        grdDetailSalary.CommitEdit(DataGridViewDataErrorContexts.Commit);
        //    }
        //}        

        private void btnExcelExport_Click(object sender, EventArgs e)
        {
            if (this.grdDetailSalary.Rows.Count < 1)
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
            if (grdDetailSalary.Rows.Count == 1)
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
                for (int col = 1; col < grdDetailSalary.Columns.Count; col++)
                {
                    if (cellRowIndex == 1)
                    {
                        if (grdDetailSalary.Columns[col].Visible == true)
                        {
                            worksheet.Cells[cellRowIndex, cellColumsIndex] = grdDetailSalary.Columns[col].HeaderText;
                            worksheet.Cells[cellRowIndex, cellColumsIndex].Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Aqua);
                        }
                        else
                            continue;
                    }
                    cellColumsIndex++;
                }                

                cellColumsIndex = 1;
                cellRowIndex++;

                for (int row = 0; row < grdDetailSalary.Rows.Count; row++)
                {
                    for (int col = 1; col < grdDetailSalary.Columns.Count; col++)
                    {
                        if (grdDetailSalary.Columns[col].Visible != true)
                        {
                            continue;
                        }

                        if (grdDetailSalary.Rows[row].Cells[col].Value != null)
                            worksheet.Cells[cellRowIndex, cellColumsIndex] = grdDetailSalary.Rows[row].Cells[col].Value.ToString();
                        else
                            worksheet.Cells[cellRowIndex, cellColumsIndex] = "";

                        cellColumsIndex++;
                    }
                    cellColumsIndex = 1;
                    cellRowIndex++;
                }
                //셀 테두리 범위 설정
                string startRange = "A1"; 
                string endRange = "Z" + (grdDetailSalary.Rows.Count + 1).ToString();

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

        private void grdDetailSalary_KeyPress(object sender, KeyPressEventArgs e)
        {
            //숫자가 아닌 값이거나 백스페이스를 제외한 나머지를 바로 처리
            if (!(char.IsDigit(e.KeyChar) || e.KeyChar == Convert.ToChar(Keys.Back)))    
            {
                e.Handled = true;
            }            
        }

        private void grdDetailSalary_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            //세금관련 컬럼에 숫자 이 외의 값을 받을 수 없도록 하는 이벤트 동작             
            string name = grdDetailSalary.CurrentCell.OwningColumn.Name;

            if (name != "sum_givelist" && name != "sum_deduct" && name != "diffpay_amount" && name != "COMMENT")
            {
                e.Control.KeyPress += new KeyPressEventHandler(grdDetailSalary_KeyPress);
            }
            else
            {
                e.Control.KeyPress -= new KeyPressEventHandler(grdDetailSalary_KeyPress);
            }
        }        

        private void btnDeleteAll_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (DataGridViewRow dr in grdDetailSalary.Rows)
                {                    
                    for (int col = this.grdDetailSalary.Columns["basic_pay"].Index; col < this.grdDetailSalary.Columns["diffpay_amount"].Index + 1; col++)
                    {
                        dr.Cells[col].Value = null;
                    }                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            MessageBox.Show("삭제되었습니다.", "inform", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
