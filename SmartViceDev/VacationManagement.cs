using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using SmartViceDev.CubridUtils;
//using Microsoft.Office.Interop.Excel;

namespace SmartViceDev
{
    public partial class VacationManagement : Form
    {
        RULE_VACATIONMANAGEMENT smartViceData = new RULE_VACATIONMANAGEMENT();

        DateTimePicker dtp = new DateTimePicker();
        Rectangle _dtpRectangle = new Rectangle();

        Dictionary<string, string> MessageSet = new Dictionary<string, string>();

        DataTable detaildatadt = new DataTable();

        public VacationManagement()
        {
            InitializeComponent();
            //this.Load += Form1_Load;

            eventslist(); //이벤트 처리 함수
            grdColumnAdd(); // 그리드 컬럼 형성
            comboboxListAdd(); // 폼 콤보박스 리스트 업


        }

        private void eventslist() 
        {
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            //this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            this.btnDeleteAll.Click += new System.EventHandler(this.btnDeleteAll_Click);            
            this.btnExcelExport.Click += new System.EventHandler(this.btnExcelExport_Click);
            this.btnVacationstdPop.Click += new System.EventHandler(this.btnVacationstdPop_Click);

            this.grdVacation.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grdVacation_CellClick);
            this.grdVacation.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grdVacation_CellDoubleClick);
            this.grdVacation.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.grdVacation_CellPainting);
            this.grdVacation.ColumnWidthChanged += new System.Windows.Forms.DataGridViewColumnEventHandler(this.grdVacation_ColumnWidthChanged);
            this.grdVacation.Scroll += new System.Windows.Forms.ScrollEventHandler(this.grdVacation_Scroll);
            this.grdVacation.Paint += new System.Windows.Forms.PaintEventHandler(this.grdVacation_Paint);
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
            //그리드내 특정 셀에 캘린더 추가 
            grdVacation.Controls.Add(dtp);
            dtp.Visible = false;
            dtp.Format = DateTimePickerFormat.Custom;
            dtp.TextChanged += new EventHandler(dtp_TextChange);

            btnSearch_Click(null, null);
        }

        private void comboboxListAdd()
        {
            DataSet ds = new DataSet();
             //사원명 콤보박스 리스트업 
            ds = smartViceData.SearchCommon("seachEmpNm");

            this.cbempNm.Items.Add("전체");
            for (int row = 0; row < ds.Tables[0].Rows.Count; row++)
            {
                this.cbempNm.Items.Add(ds.Tables[0].Rows[row].Field<string>("staff_name").ToString());
            }
            this.cbempNm.SelectedItem = "전체";

            //직책명 콤보박스 리스트업 
            
            ds = smartViceData.SearchCommon("seachPosition");

            this.cmbPosition.Items.Add("전체");
            for (int row = 0; row < ds.Tables[0].Rows.Count; row++)
            {
                this.cmbPosition.Items.Add(ds.Tables[0].Rows[row].Field<string>("position_name").ToString());
            }

            this.cmbPosition.SelectedItem = "전체";
        }

        private void grdColumnAdd()
        {            
            var chkCol = new DataGridViewCheckBoxColumn
            {
                Name = "colCheck",
                HeaderText = ""
            };
            this.grdVacation.Columns.Add(chkCol);
            this.grdVacation.Columns["colCheck"].Width = 50;
            this.grdVacation.Columns["colCheck"].Frozen = true;


            this.grdVacation.Columns.Add("EmpId", "사번");
            this.grdVacation.Columns["EmpId"].Visible = false;
            this.grdVacation.Columns.Add("EmpNm", "사원명");
            this.grdVacation.Columns["EmpNm"].Width = 80;
            this.grdVacation.Columns["EmpNm"].ReadOnly = true;
            this.grdVacation.Columns["EmpNm"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.grdVacation.Columns["EmpNm"].Frozen = true;

            //DataSet ds = new DataSet();
            //RULE_VACATIONMANAGEMENT smartViceData = new RULE_VACATIONMANAGEMENT();
            //ds = smartViceData.SearchCommon("seachPosition");
            //그리드 직책 컬럼 콤보박스 설정
            //DataGridViewComboBoxColumn EmpPosition = new DataGridViewComboBoxColumn();
            //EmpPosition.Name = "EmpPosition";
            //EmpPosition.HeaderText = "직책";
            //DataTable dt = ds.Tables[0].Copy();
            //for (int row = 0; row < dt.Rows.Count; row++)
            //{
            //    EmpPosition.Items.Add(dt.Rows[row]["position_name"]);
            //}            
            //this.grdVacation.Columns.Add(EmpPosition);

            //직책
            this.grdVacation.Columns.Add("EmpPosition", "직책");
            this.grdVacation.Columns["EmpPosition"].Width = 80;
            this.grdVacation.Columns["EmpPosition"].ReadOnly = true;
            this.grdVacation.Columns["EmpPosition"].Frozen = true;
            //입사일
            this.grdVacation.Columns.Add("EmpJoinDate", "입사일");            
            this.grdVacation.Columns["EmpJoinDate"].Width = 200;
            this.grdVacation.Columns["EmpJoinDate"].ReadOnly = true;
            this.grdVacation.Columns["EmpJoinDate"].Frozen = true;
            //갱신연차수
            this.grdVacation.Columns.Add("UpdatedDayOff", "갱신연차수");
            this.grdVacation.Columns["UpdatedDayOff"].ReadOnly = true;
            this.grdVacation.Columns["UpdatedDayOff"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.grdVacation.Columns["UpdatedDayOff"].Frozen = true;
            //연차
            this.grdVacation.Columns.Add("DayOff", "연차");
            this.grdVacation.Columns["DayOff"].Width = 80;
            this.grdVacation.Columns["DayOff"].ReadOnly = true;
            this.grdVacation.Columns["DayOff"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.grdVacation.Columns["DayOff"].Frozen = true;
            //추가일수
            this.grdVacation.Columns.Add("AddedDays", "추가일수");
            this.grdVacation.Columns["AddedDays"].Width = 80;
            this.grdVacation.Columns["AddedDays"].ReadOnly = true;
            this.grdVacation.Columns["AddedDays"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.grdVacation.Columns["AddedDays"].Frozen = true;
            //공제일수
            this.grdVacation.Columns.Add("DeductedDays", "공제일수");
            this.grdVacation.Columns["DeductedDays"].Width = 80;
            this.grdVacation.Columns["DeductedDays"].ReadOnly = true;
            this.grdVacation.Columns["DeductedDays"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.grdVacation.Columns["DeductedDays"].Frozen = true;
            //사용일수
            this.grdVacation.Columns.Add("TotalUsedDays", "사용일수");
            this.grdVacation.Columns["TotalUsedDays"].Width = 80;
            this.grdVacation.Columns["TotalUsedDays"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.grdVacation.Columns["TotalUsedDays"].Frozen = true;
            //잔여일수
            this.grdVacation.Columns.Add("RestDays", "잔여일수");
            this.grdVacation.Columns["RestDays"].Width = 80;            
            this.grdVacation.Columns["RestDays"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.grdVacation.Columns["RestDays"].Frozen = true;
            //1월
            this.grdVacation.Columns.Add("January", "1월");
            this.grdVacation.Columns["January"].Width = 60;
            this.grdVacation.Columns["January"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.grdVacation.Columns.Add("January_Dates", "1월");
            this.grdVacation.Columns["January_Dates"].Visible = false;
            //2월
            this.grdVacation.Columns.Add("February", "2월");
            this.grdVacation.Columns["February"].Width = 60;
            this.grdVacation.Columns["February"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.grdVacation.Columns.Add("February_Dates", "2월");
            this.grdVacation.Columns["February_Dates"].Visible = false;
            //3월
            this.grdVacation.Columns.Add("March", "3월");
            this.grdVacation.Columns["March"].Width = 60;
            this.grdVacation.Columns["March"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.grdVacation.Columns.Add("March_Dates", "3월");
            this.grdVacation.Columns["March_Dates"].Visible = false;
            //4월
            this.grdVacation.Columns.Add("April", "4월");
            this.grdVacation.Columns["April"].Width = 60;
            this.grdVacation.Columns["April"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.grdVacation.Columns.Add("April_Dates", "4월");
            this.grdVacation.Columns["April_Dates"].Visible = false;
            //5월
            this.grdVacation.Columns.Add("May", "5월");
            this.grdVacation.Columns["May"].Width = 60;
            this.grdVacation.Columns["May"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.grdVacation.Columns.Add("May_Dates", "5월");
            this.grdVacation.Columns["May_Dates"].Visible = false;
            //6월
            this.grdVacation.Columns.Add("June", "6월");
            this.grdVacation.Columns["June"].Width = 60;
            this.grdVacation.Columns["June"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.grdVacation.Columns.Add("June_Dates", "6월");
            this.grdVacation.Columns["June_Dates"].Visible = false;
            //7월
            this.grdVacation.Columns.Add("July", "7월");
            this.grdVacation.Columns["July"].Width = 60;
            this.grdVacation.Columns["July"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.grdVacation.Columns.Add("July_Dates", "7월");
            this.grdVacation.Columns["July_Dates"].Visible = false;
            //8월
            this.grdVacation.Columns.Add("August", "8월");
            this.grdVacation.Columns["August"].Width = 60;
            this.grdVacation.Columns["August"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.grdVacation.Columns.Add("August_Dates", "8월");
            this.grdVacation.Columns["August_Dates"].Visible = false;
            //9월
            this.grdVacation.Columns.Add("September", "9월");
            this.grdVacation.Columns["September"].Width = 60;
            this.grdVacation.Columns["September"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.grdVacation.Columns.Add("September_Dates", "9월");
            this.grdVacation.Columns["September_Dates"].Visible = false;
            //10월
            this.grdVacation.Columns.Add("October", "10월");
            this.grdVacation.Columns["October"].Width = 60;
            this.grdVacation.Columns["October"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.grdVacation.Columns.Add("October_Dates", "10월");
            this.grdVacation.Columns["October_Dates"].Visible = false;
            //11월
            this.grdVacation.Columns.Add("November", "11월");
            this.grdVacation.Columns["November"].Width = 60;
            this.grdVacation.Columns["November"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.grdVacation.Columns.Add("November_Dates", "11월");
            this.grdVacation.Columns["November_Dates"].Visible = false;
            //12월
            this.grdVacation.Columns.Add("December", "12월");
            this.grdVacation.Columns["December"].Width = 60;
            this.grdVacation.Columns["December"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.grdVacation.Columns.Add("December_Dates", "12월");
            this.grdVacation.Columns["December_Dates"].Visible = false;
            //합계
            this.grdVacation.Columns.Add("UsedDays", "합계");
            this.grdVacation.Columns["UsedDays"].Width = 70;
            this.grdVacation.Columns["UsedDays"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            //this.grdVacation.Columns.Add("RowState", "RowState");
            //this.grdVacation.Columns["RowState"].Visible = false; // 행의 신규,기존,수정,삭제 등의 상태를 나타냄.

            this.grdVacation.RowHeadersVisible = false; 
            this.grdVacation.AllowUserToAddRows = false;  // 빈레코드 표시 안하기
            this.grdVacation.AlternatingRowsDefaultCellStyle.BackColor = Color.AliceBlue; // 그리드 행에 색 입히기
        }



        private void grdVacation_Paint(object sender, PaintEventArgs e)
        {
            DataGridView gv = (DataGridView)sender;

            string[] strHeaders = { "사원정보", "금일기준 연차수 및 사용/ 잔여 일수", DateTime.Today.Year.ToString() + "년도 휴가 사용 현황" };

            StringFormat format = new StringFormat();

            format.Alignment = StringAlignment.Center;

            format.LineAlignment = StringAlignment.Center;


            // 사원정보 Painting
            {                

                Rectangle r1 = gv.GetCellDisplayRectangle(this.grdVacation.Columns["colCheck"].Index , -1, false);

                int width1 = gv.GetCellDisplayRectangle(this.grdVacation.Columns["EmpNm"].Index, -1, false).Width;

                int width2 = gv.GetCellDisplayRectangle(this.grdVacation.Columns["EmpPosition"].Index, -1, false).Width;

                int width3 = gv.GetCellDisplayRectangle(this.grdVacation.Columns["EmpJoinDate"].Index, -1, false).Width;

                int width4 = gv.GetCellDisplayRectangle(this.grdVacation.Columns["UpdatedDayOff"].Index, -1, false).Width;

                r1.X += 1;

                r1.Y += 1;

                r1.Width = r1.Width + width1 + width2 + width3 + width4 - 2;

                r1.Height = (r1.Height / 2) - 2;

                e.Graphics.DrawRectangle(new Pen(gv.BackgroundColor), r1);

                e.Graphics.FillRectangle(new SolidBrush(gv.ColumnHeadersDefaultCellStyle.BackColor),

                    r1);



                e.Graphics.DrawString(strHeaders[0],

                    gv.ColumnHeadersDefaultCellStyle.Font,

                    new SolidBrush(gv.ColumnHeadersDefaultCellStyle.ForeColor),

                    r1,

                    format);

            }           

            //금일기준... Painting
            {
                Rectangle r2 = gv.GetCellDisplayRectangle(this.grdVacation.Columns["DayOff"].Index, -1, false);

                int width1 = gv.GetCellDisplayRectangle(this.grdVacation.Columns["AddedDays"].Index, -1, false).Width;
                int width2 = gv.GetCellDisplayRectangle(this.grdVacation.Columns["DeductedDays"].Index, -1, false).Width;
                int width3 = gv.GetCellDisplayRectangle(this.grdVacation.Columns["TotalUsedDays"].Index, -1, false).Width;
                int width4 = gv.GetCellDisplayRectangle(this.grdVacation.Columns["RestDays"].Index, -1, false).Width;

                r2.X += 1;

                r2.Y += 1;

                r2.Width = r2.Width + width1 + width2 + width3 + width4 - 2;

                r2.Height = (r2.Height / 2) - 2;

                e.Graphics.DrawRectangle(new Pen(gv.BackgroundColor), r2);

                e.Graphics.FillRectangle(new SolidBrush(gv.ColumnHeadersDefaultCellStyle.BackColor),

                    r2);

                e.Graphics.DrawString(strHeaders[1],

                    gv.ColumnHeadersDefaultCellStyle.Font,

                    new SolidBrush(gv.ColumnHeadersDefaultCellStyle.ForeColor),

                    r2,

                    format);

            }

            //2020년도... Painting
            {
                Rectangle r3 = gv.GetCellDisplayRectangle(this.grdVacation.Columns["January"].Index, -1, false);

                int width0 = gv.GetCellDisplayRectangle(this.grdVacation.Columns["February"].Index, -1, false).Width;
                int width1 = gv.GetCellDisplayRectangle(this.grdVacation.Columns["March"].Index, -1, false).Width;
                int width2 = gv.GetCellDisplayRectangle(this.grdVacation.Columns["April"].Index, -1, false).Width;
                int width3 = gv.GetCellDisplayRectangle(this.grdVacation.Columns["May"].Index, -1, false).Width;
                int width4 = gv.GetCellDisplayRectangle(this.grdVacation.Columns["June"].Index, -1, false).Width;
                int width5 = gv.GetCellDisplayRectangle(this.grdVacation.Columns["July"].Index, -1, false).Width;
                int width6 = gv.GetCellDisplayRectangle(this.grdVacation.Columns["August"].Index, -1, false).Width;
                int width7 = gv.GetCellDisplayRectangle(this.grdVacation.Columns["September"].Index, -1, false).Width;
                int width8 = gv.GetCellDisplayRectangle(this.grdVacation.Columns["October"].Index, -1, false).Width;
                int width9 = gv.GetCellDisplayRectangle(this.grdVacation.Columns["November"].Index, -1, false).Width;
                int width10 = gv.GetCellDisplayRectangle(this.grdVacation.Columns["December"].Index, -1, false).Width;
                int width11 = gv.GetCellDisplayRectangle(this.grdVacation.Columns["TotalUsedDays"].Index, -1, false).Width;
                
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

            }
        }

        private void grdVacation_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
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
            //    cb.CheckedChanged += new EventHandler(grdVacationCheckBox_CheckedChanged);

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

        private void grdVacation_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
        {
            DataGridView gv = (DataGridView)sender;

            Rectangle rtHeader = gv.DisplayRectangle;

            rtHeader.Height = gv.ColumnHeadersHeight / 2;

            gv.Invalidate(rtHeader);

            dtp.Visible = false;

        }
        private void grdVacation_Scroll(object sender, ScrollEventArgs e)
        {            
            DataGridView gv = (DataGridView)sender;

            Rectangle rtHeader = gv.DisplayRectangle;

            rtHeader.Height = gv.ColumnHeadersHeight / 2;

            gv.Invalidate(rtHeader);

            dtp.Visible = false;
        }

        //그리드의 체크박스 전체선택/전체해제를 위한 이벤트(스크롤이 움직일때마다 이중헤더를 다시 그리면서 해당 체크박스가 계속 생기는 단점이 있음)
        //private void grdVacation_CheckedChanged(object sender, EventArgs e)
        //{
        //    foreach (DataGridViewRow r in grdVacation.Rows)
        //    {
        //        r.Cells["colCheck"].Value = ((CheckBox)sender).Checked;
        //    }
        //}
        
        private void btnSearch_Click(object sender, EventArgs e)
        {
            //조회하기전 수정되었으나 저장되지 않은 데이터를 검열
            if (Chksearch() == 1)
            {
                DialogResult dialogResult = MessageBox.Show("저장되지 않은 데이터가 존재합니다. 그래도 조회하시겠습니까?\n(수정한 데이터를 잃게됩니다.)", "Warning", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
                if (dialogResult == DialogResult.Yes)
                {
                    detaildatadt = Search("DetailSearch", MessageSet);
                    Search("MainSearch", MessageSet);
                }
                else if (dialogResult == DialogResult.Cancel || dialogResult == DialogResult.No)
                    return;
            }
            else
            {
                detaildatadt = Search("DetailSearch", MessageSet);
                Search("MainSearch", MessageSet);
            }
        }

        public DataTable Search(string searchType, Dictionary<string, string> MessageSet)
        {
            MessageSet.Clear();
            MessageSet.Add("EmpName", this.cbempNm.SelectedItem.ToString() == "전체" ? "" : this.cbempNm.SelectedItem.ToString()) ;         
            MessageSet.Add("PositionName", this.cmbPosition.SelectedItem.ToString() == "전체" ? "" : this.cmbPosition.SelectedItem.ToString());

            DataSet ds = new DataSet();
            ds = smartViceData.Search(searchType, MessageSet);
 
            DataTable dt = ds.Tables[0].Copy();

            if (searchType == "MainSearch")
            {
                if(dt.Rows.Count < 1)
                {
                    MessageBox.Show("조회된 데이터가 없습니다.", "Inform", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.grdVacation.Rows.Clear();
                    MessageSet.Clear();
                    return null;
                }
                if (this.grdVacation.Rows.Count > 0)
                    this.grdVacation.Rows.Clear();
                                                  
                PivotSearchData(dt);//가져온 데이터를 그리드 형식에 받게 재배열.               
            }

            MessageSet.Clear();
            return dt;

        }

        public int Chksearch()
        {
            if (this.grdVacation.Rows.Count < 1)
                return 0;

            for (int row = 0; row < this.grdVacation.Rows.Count; row++)
            {
                //저장되지 않은 데이터가 있는지 확인
                for (int months = this.grdVacation.Columns["January"].Index; months < this.grdVacation.Columns["December"].Index; months += 2)
                {
                    if (this.grdVacation[months + 1, row].Value != null)
                        return 1;
                }
            }
                       
            return 0;
        }

        public void PivotSearchData(DataTable dt)
        {
            int GridRowIdx = 0;

            for (int row = 0; row < dt.Rows.Count; row++)
            {
                if (row == 0)
                {
                    this.grdVacation.Rows.Add();
                    this.grdVacation["EmpId", 0].Value = dt.Rows[row]["EmpId"].ToString();
                    this.grdVacation["EmpNm", 0].Value = dt.Rows[row]["EmpNm"].ToString();
                    this.grdVacation["EmpPosition", 0].Value = dt.Rows[row]["EmpPosition"].ToString();
                    this.grdVacation["EmpJoinDate", 0].Value = dt.Rows[row]["EmpJoinDate"].ToString();
                    this.grdVacation["UpdatedDayOff", 0].Value = dt.Rows[row]["UpdatedDayOff"].ToString();
                    this.grdVacation["DayOff", 0].Value = dt.Rows[row]["DayOff"].ToString();
                    this.grdVacation["AddedDays", 0].Value = dt.Rows[row]["AddedDays"].ToString();
                    this.grdVacation["TotalUsedDays", 0].Value = dt.Rows[row]["TotalUsedDays"].ToString();
                    this.grdVacation["RestDays", 0].Value = dt.Rows[row]["RestDays"].ToString();
                    this.grdVacation["UsedDays", 0].Value = dt.Rows[row]["TotalUsedDays"].ToString();
                    

                    switch (dt.Rows[row]["leaveyyyymm"].ToString().Substring(dt.Rows[row]["leaveyyyymm"].ToString().Length - 2))
                    {
                        case "01":
                            this.grdVacation["January", 0].Value = dt.Rows[row]["UsedDaysByMonth"].ToString();                            
                            break;
                        case "02":
                            this.grdVacation["February", 0].Value = dt.Rows[row]["UsedDaysByMonth"].ToString();
                            break;
                        case "03":
                            this.grdVacation["March", 0].Value = dt.Rows[row]["UsedDaysByMonth"].ToString();
                            break;
                        case "04":
                            this.grdVacation["April", 0].Value = dt.Rows[row]["UsedDaysByMonth"].ToString();
                            break;
                        case "05":
                            this.grdVacation["May", 0].Value = dt.Rows[row]["UsedDaysByMonth"].ToString();
                            break;
                        case "06":
                            this.grdVacation["June", 0].Value = dt.Rows[row]["UsedDaysByMonth"].ToString();
                            break;
                        case "07":
                            this.grdVacation["July", 0].Value = dt.Rows[row]["UsedDaysByMonth"].ToString();
                            break;
                        case "08":
                            this.grdVacation["August", 0].Value = dt.Rows[row]["UsedDaysByMonth"].ToString();
                            break;
                        case "09":
                            this.grdVacation["September", 0].Value = dt.Rows[row]["UsedDaysByMonth"].ToString();
                            break;
                        case "10":
                            this.grdVacation["October", 0].Value = dt.Rows[row]["UsedDaysByMonth"].ToString();
                            break;
                        case "11":
                            this.grdVacation["November", 0].Value = dt.Rows[row]["UsedDaysByMonth"].ToString();
                            break;
                        case "12":
                            this.grdVacation["December", 0].Value = dt.Rows[row]["UsedDaysByMonth"].ToString();
                            break;
                    }
                    GridRowIdx++;

                    continue;
                }

                // 테이블에 각 사원의 휴가 데이터가 여러 행으로 존재하기때문에 해당 데이터들을 한 행으로 표현하기 위한 기능 추가 
                if (GridRowIdx != 0 && this.grdVacation.Rows[GridRowIdx - 1].Cells["EmpId"].Value.ToString() == dt.Rows[row]["EmpId"].ToString())
                {
                    switch (dt.Rows[row]["leaveyyyymm"].ToString().Substring(dt.Rows[row]["leaveyyyymm"].ToString().Length - 2))
                    {
                        case "01":
                            this.grdVacation["January", GridRowIdx - 1].Value = dt.Rows[row]["UsedDaysByMonth"].ToString();
                            break;
                        case "02":
                            this.grdVacation["February", GridRowIdx - 1].Value = dt.Rows[row]["UsedDaysByMonth"].ToString();
                            break;
                        case "03":
                            this.grdVacation["March", GridRowIdx - 1].Value = dt.Rows[row]["UsedDaysByMonth"].ToString();
                            break;
                        case "04":
                            this.grdVacation["April", GridRowIdx - 1].Value = dt.Rows[row]["UsedDaysByMonth"].ToString();
                            break;
                        case "05":
                            this.grdVacation["May", GridRowIdx - 1].Value = dt.Rows[row]["UsedDaysByMonth"].ToString();
                            break;
                        case "06":
                            this.grdVacation["June", GridRowIdx - 1].Value = dt.Rows[row]["UsedDaysByMonth"].ToString();
                            break;
                        case "07":
                            this.grdVacation["July", GridRowIdx - 1].Value = dt.Rows[row]["UsedDaysByMonth"].ToString();
                            break;
                        case "08":
                            this.grdVacation["August", GridRowIdx - 1].Value = dt.Rows[row]["UsedDaysByMonth"].ToString();
                            break;
                        case "09":
                            this.grdVacation["September", GridRowIdx - 1].Value = dt.Rows[row]["UsedDaysByMonth"].ToString();
                            break;
                        case "10":
                            this.grdVacation["October", GridRowIdx - 1].Value = dt.Rows[row]["UsedDaysByMonth"].ToString();
                            break;
                        case "11":
                            this.grdVacation["November", GridRowIdx - 1].Value = dt.Rows[row]["UsedDaysByMonth"].ToString();
                            break;
                        case "12":
                            this.grdVacation["December", GridRowIdx - 1].Value = dt.Rows[row]["UsedDaysByMonth"].ToString();
                            break;
                    }
                    continue;
                }

                this.grdVacation.Rows.Add();
                this.grdVacation["EmpId", GridRowIdx].Value = dt.Rows[row]["EmpId"].ToString();
                this.grdVacation["EmpNm", GridRowIdx].Value = dt.Rows[row]["EmpNm"].ToString();
                this.grdVacation["EmpPosition", GridRowIdx].Value = dt.Rows[row]["EmpPosition"].ToString();
                this.grdVacation["EmpJoinDate", GridRowIdx].Value = dt.Rows[row]["EmpJoinDate"].ToString();
                this.grdVacation["UpdatedDayOff", GridRowIdx].Value = dt.Rows[row]["UpdatedDayOff"].ToString();
                this.grdVacation["DayOff", GridRowIdx].Value = dt.Rows[row]["DayOff"].ToString();
                this.grdVacation["AddedDays", GridRowIdx].Value = dt.Rows[row]["AddedDays"].ToString();
                this.grdVacation["TotalUsedDays", GridRowIdx].Value = dt.Rows[row]["TotalUsedDays"].ToString();
                this.grdVacation["RestDays", GridRowIdx].Value = dt.Rows[row]["RestDays"].ToString();
                this.grdVacation["UsedDays", GridRowIdx].Value = dt.Rows[row]["TotalUsedDays"].ToString();

                switch (dt.Rows[row]["leaveyyyymm"].ToString().Substring(dt.Rows[row]["leaveyyyymm"].ToString().Length - 2))
                {
                    case "01":
                        this.grdVacation["January", GridRowIdx].Value = dt.Rows[row]["UsedDaysByMonth"].ToString();
                        break;
                    case "02":
                        this.grdVacation["February", GridRowIdx].Value = dt.Rows[row]["UsedDaysByMonth"].ToString();
                        break;
                    case "03":
                        this.grdVacation["March", GridRowIdx].Value = dt.Rows[row]["UsedDaysByMonth"].ToString();
                        break;
                    case "04":
                        this.grdVacation["April", GridRowIdx].Value = dt.Rows[row]["UsedDaysByMonth"].ToString();
                        break;
                    case "05":
                        this.grdVacation["May", GridRowIdx].Value = dt.Rows[row]["UsedDaysByMonth"].ToString();
                        break;
                    case "06":
                        this.grdVacation["June", GridRowIdx].Value = dt.Rows[row]["UsedDaysByMonth"].ToString();
                        break;
                    case "07":
                        this.grdVacation["July", GridRowIdx].Value = dt.Rows[row]["UsedDaysByMonth"].ToString();
                        break;
                    case "08":
                        this.grdVacation["August", GridRowIdx].Value = dt.Rows[row]["UsedDaysByMonth"].ToString();
                        break;
                    case "09":
                        this.grdVacation["September", GridRowIdx].Value = dt.Rows[row]["UsedDaysByMonth"].ToString();
                        break;
                    case "10":
                        this.grdVacation["October", GridRowIdx].Value = dt.Rows[row]["UsedDaysByMonth"].ToString();
                        break;
                    case "11":
                        this.grdVacation["November", GridRowIdx].Value = dt.Rows[row]["UsedDaysByMonth"].ToString();
                        break;
                    case "12":
                        this.grdVacation["December", GridRowIdx].Value = dt.Rows[row]["UsedDaysByMonth"].ToString();
                        break;
                }
                GridRowIdx++;
            }
            
        }
        //* 사용안함 *//
        private void btnAdd_Click(object sender, EventArgs e)
        {
            //this.grdVacation.Rows.Add();
            //this.grdVacation["TotalUsedDays", grdVacation.CurrentCell.RowIndex].Value = 0;
            //this.grdVacation.Rows[this.grdVacation.Rows.Count-1].GetState 
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (DataGridViewRow dr in grdVacation.Rows) 
                {
                
                    if (dr.Cells["colCheck"].Value != null) // 체크된 행이 삭제되는 것이 아니라 휴가 관련 데이터만 삭제됨. 
                    {

                        dr.Cells["TotalUsedDays"].Value = 0;
                        dr.Cells["UsedDays"].Value = 0;
                        dr.Cells["RestDays"].Value = dr.Cells["UpdatedDayOff"].Value;

                        int calender = 1;

                        for (int months = this.grdVacation.Columns["January"].Index; months < this.grdVacation.Columns["December"].Index; months += 2)
                        {
                            if (dr.Cells[months].Value != null)
                            {
                               dr.Cells[months].Value = 0;
                               dr.Cells[months + 1].Value =  DateTime.Now.Year + "-" + calender.ToString().PadLeft(2,'0');
                            }
                            else
                                dr.Cells[months].Value = null;
                            
                            calender++;
                        }
                    }                
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            MessageBox.Show("삭제되었습니다.", "inform", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        //사원 관리 화면에 입사일 등록할 때 사용할 기능
        private void grdVacation_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)
                return;

            switch (grdVacation.Columns[e.ColumnIndex].Name)
            {
                case "colCheck":
                    this.grdVacation.CurrentRow.Selected = true;
                    break;
                    //case "EmpJoinDate":

                    //    _dtpRectangle = grdVacation.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true);
                    //    dtp.Size = new Size(_dtpRectangle.Width, _dtpRectangle.Height);
                    //    dtp.Location = new Point(_dtpRectangle.X, _dtpRectangle.Y);
                    //    dtp.Visible = true;
                    //    if (!this.grdVacation.Rows[e.RowIndex].IsNewRow && this.grdVacation.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
                    //    {
                    //        dtp.Value = Convert.ToDateTime(this.grdVacation.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);
                    //    }
                    //    else
                    //        dtp.Value = DateTime.Today;

                    //    break;
            }
        }

        private void dtp_TextChange(object sender, EventArgs e)
        {
            grdVacation.CurrentCell.Value = dtp.Text.ToString();
            
            DataSet ds = new DataSet();
            Dictionary<string, string> Joindt = new Dictionary<string, string>();
            Joindt.Add("JoinDt", dtp.Text.ToString());

            ds = smartViceData.Search("GetLeaveData", Joindt);

            if(ds.Tables[0].Rows.Count < 1)
            {
                return;
            }
            DataTable dt = ds.Tables[0].Copy();

            this.grdVacation["UpdatedDayOff", grdVacation.CurrentCell.RowIndex].Value = dt.Rows[0]["UpdatedDayOff"].ToString();
            this.grdVacation["DayOff", grdVacation.CurrentCell.RowIndex].Value = dt.Rows[0]["DayOff"].ToString();
            this.grdVacation["AddedDays", grdVacation.CurrentCell.RowIndex].Value = dt.Rows[0]["AddedDays"].ToString();

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            int resultdt = 1;

            resultdt = Save();

            if (resultdt == 0)
                MessageBox.Show("저장할 데이터가 존재하지 않습니다.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else if(resultdt == 1)
            {
                MessageSet.Clear();
                grdVacation.Rows.Clear();

                detaildatadt = Search("DetailSearch", MessageSet);
                Search("MainSearch", MessageSet);
                MessageBox.Show("저장하였습니다.", "Inform", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
                MessageBox.Show("저장에 실패하였습니다.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);

        }

        private int Save()
        {            
            bool chksave = true;            

            //Dictionary<string, string> MessageSet = new Dictionary<string, string>();
            //Dictionary<string, Dictionary<string, string>> lstMessageSet = new Dictionary<string, Dictionary<string, string>>();
            string QueryType = string.Empty;

            if (grdVacation.Rows.Count > 0)
            {
                //현재 그리드와 조회한 후 그리드를 비교 후 insert 또는 delete 작업            
                for (int grdrow = 0; grdrow < grdVacation.Rows.Count; grdrow++)
                {
                    ////새로 추가된 행이 아닐때는 수정된 값 Insert또는 Delete 작업 필요
                    //if (!(this.grdVacation.Rows[grdrow].Cells["TotalUsedDays"].Value == null || this.grdVacation.Rows[grdrow].Cells["TotalUsedDays"].Value.ToString() == "0"))
                    //{
                        //월별 체크
                        for (int months = this.grdVacation.Columns["January"].Index; months < this.grdVacation.Columns["December"].Index; months += 2)
                        {
                            //각 사원의 휴가 사용 데이터 조회
                            var selectedDt = from dr in detaildatadt.AsEnumerable()
                                             where dr.Field<string>("EmpId") == this.grdVacation.Rows[grdrow].Cells["EmpId"].Value.ToString()
                                             select new
                                             {
                                                 EmpId = dr.Field<string>("EmpId"),
                                                 LeaveYYYYMM = dr.Field<string>("leaveyyyymm"),
                                                 detailLeaveDate = dr.Field<string>("detailLeaveDate")
                                             };

                            
                            if (this.grdVacation.Rows[grdrow].Cells[months].Value != null)
                            {
                                //그리드 상에 월별 휴가 사용 일 수가 '0'일 때 기존 데이터 Delete 처리. 
                                if (this.grdVacation.Rows[grdrow].Cells[months].Value.ToString() == "0")
                                {
                                    foreach (var dr in selectedDt)
                                    {
                                        QueryType = "Delete";
                                        if (dr.detailLeaveDate.ToString().Contains(this.grdVacation.Rows[grdrow].Cells[months + 1].Value.ToString().Substring(0,7)))
                                        {
                                            MessageSet.Clear();
                                            MessageSet.Add("Emp_Id", this.grdVacation.Rows[grdrow].Cells["EmpId"].Value.ToString());
                                            MessageSet.Add("Use_Leave_Dt", "'" + dr.detailLeaveDate + "'");

                                            chksave = smartViceData.Save(QueryType, MessageSet);                     

                                            if (!chksave)
                                            {
                                                return -1;
                                            }
                                                                          
                                        }
                                    }                                   
                                }
                                //그리드 상에 월별 휴가 사용 일 수가 '0'이 아닌 값일 때 기존 사용일수와는 같지만 실제 날짜는 달라졌을 수도 있음. 
                                else
                                {
                                    if (this.grdVacation.Rows[grdrow].Cells[months + 1].Value == null || this.grdVacation.Rows[grdrow].Cells[months + 1].Value.ToString() == string.Empty)
                                        continue;

                                    string[] strCntLeavedt = this.grdVacation.Rows[grdrow].Cells[months + 1].Value.ToString().Split(',');

                                    //추가된 휴가날짜 Insert
                                    foreach (string strEachdt in strCntLeavedt)
                                    {
                                        //기존 휴가날짜와 중복되는 데이터가 있는지 확인
                                        int checkdt = detaildatadt.AsEnumerable().Count(dr => dr.Field<string>("EmpId") == this.grdVacation.Rows[grdrow].Cells["EmpId"].Value.ToString()
                                                            && dr.Field<string>("detailLeaveDate").Contains(strEachdt.Replace("'", "")));                            
                                        
                                        if (checkdt > 0)
                                            continue;

                                        //새로운 휴가 일정이 추가되었을 때 Insert 작업해주기
                                        QueryType = "Insert";
                                        MessageSet.Clear();
                                        MessageSet.Add("Emp_Id", this.grdVacation.Rows[grdrow].Cells["EmpId"].Value.ToString());
                                        MessageSet.Add("Use_Leave_Dt", strEachdt);                                        

                                        chksave = smartViceData.Save(QueryType, MessageSet);
                                        
                                        if (!chksave)
                                        {
                                            return -1;
                                        }
                                    }
                                    foreach (var dr in selectedDt)
                                    {
                                        QueryType = "Delete";
                                        
                                        if (!strCntLeavedt[0].ToString().Replace("'", "").Contains(dr.LeaveYYYYMM))
                                            continue;

                                        if (!strCntLeavedt.Contains("'" + dr.detailLeaveDate.ToString()+ "'"))
                                        {
                                            MessageSet.Clear();
                                            MessageSet.Add("Emp_Id", this.grdVacation.Rows[grdrow].Cells["EmpId"].Value.ToString());
                                            MessageSet.Add("Use_Leave_Dt", "'" + dr.detailLeaveDate + "'");

                                            chksave = smartViceData.Save(QueryType, MessageSet);
                                            
                                            if (!chksave)
                                            {
                                                return -1;
                                            }
                                        }
                                    }
                                }
                            }                         
                        }
                    //}
                    //else // NewRow를 저장할 때는 Only Insert만 존재.
                    //{
                    //    //월별 체크
                    //    for (int months = this.grdVacation.Columns["January"].Index; months < this.grdVacation.Columns["December"].Index; months += 2)
                    //    {
                    //        if (this.grdVacation.Rows[grdrow].Cells[months].Value != null)
                    //        {
                    //            if (this.grdVacation.Rows[grdrow].Cells[months + 1].Value == null || this.grdVacation.Rows[grdrow].Cells[months + 1].Value.ToString() == string.Empty)
                    //                continue;

                    //            string[] strCntLeavedt = this.grdVacation.Rows[grdrow].Cells[months + 1].Value.ToString().Split(',');

                    //            //추가된 휴가날짜 Insert
                    //            foreach (string strEachdt in strCntLeavedt)
                    //            {
                    //                //기존 휴가날짜와 중복되는 데이터가 있는지 확인
                    //                int checkdt = detaildatadt.AsEnumerable().Count(dr => dr.Field<string>("EmpId") == this.grdVacation.Rows[grdrow].Cells["EmpId"].Value.ToString()
                    //                                    && dr.Field<string>("detailLeaveDate").Contains(strEachdt.Replace("'", "")));
                        
                    //                //새로운 휴가 일정이 추가되었을 때 Insert 작업해주기
                    //                if (checkdt > 0)
                    //                    continue;

                    //                QueryType = "Insert";
                    //                MessageSet.Add("Emp_Id", this.grdVacation.Rows[grdrow].Cells["EmpId"].Value.ToString());
                    //                MessageSet.Add("Use_Leave_Dt", strEachdt);
                    //                //lstMessageSet.Add(this.grdVacation.Rows[grdrow].Cells["EmpId"].Value.ToString() + "_" + strEachdt, MessageSet);

                    //                chksave = smartViceData.Save(QueryType, MessageSet);
                    //                MessageSet.Clear();
                    //                if (!chksave)
                    //                {
                    //                    return -1;
                    //                }
                    //            }

                    //        }
                    //    }
                    //        return 1;
                    //}

                }
                return 1;
            }
            else
            {
                return 0;
            }
        }

        private void grdVacation_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            ShowVCanlendar dlgcalendar = new ShowVCanlendar();
            
            var selectedDt = from dr in detaildatadt.AsEnumerable()
                             where dr.Field<string>("EmpId") == this.grdVacation.Rows[this.grdVacation.CurrentCell.RowIndex].Cells["EmpId"].Value.ToString()
                             select new
                             {
                                 EmpId = dr.Field<string>("EmpId"),
                                 LeaveYYYYMM = dr.Field<string>("leaveyyyymm"),
                                 detailLeaveDate = dr.Field<string>("detailLeaveDate")
                             };

            List<DateTime> lstLeaveDt = new List<DateTime>();

            switch (grdVacation.Columns[e.ColumnIndex].Name)
            {
                case "January":
                    if (this.grdVacation.CurrentCell.Value != null)
                    {                                                
                        foreach(var row in selectedDt)
                        { 
                            if(row.LeaveYYYYMM.Substring(row.LeaveYYYYMM.Length - 2) == "01")
                                   lstLeaveDt.Add(Convert.ToDateTime(row.detailLeaveDate));
                        }
                    }
                    dlgcalendar.MonthOfLeaveDt = new DateTime(DateTime.Today.Year,01,01);
                    break;
            
                case "February":
                    if (this.grdVacation.CurrentCell.Value != null)
                    {                                                
                        foreach (var row in selectedDt)
                        {
                            if (row.LeaveYYYYMM.Substring(row.LeaveYYYYMM.Length - 2) == "02")
                                lstLeaveDt.Add(Convert.ToDateTime(row.detailLeaveDate));
                        }
                    }

                    dlgcalendar.MonthOfLeaveDt = new DateTime(DateTime.Today.Year, 02, 01);
                    break;
                case "March":
                    if (this.grdVacation.CurrentCell.Value != null)
                    {                                                
                        foreach (var row in selectedDt)
                        {
                            if (row.LeaveYYYYMM.Substring(row.LeaveYYYYMM.Length - 2) == "03")
                                lstLeaveDt.Add(Convert.ToDateTime(row.detailLeaveDate));
                        }
                    }

                    dlgcalendar.MonthOfLeaveDt = new DateTime(DateTime.Today.Year, 03, 01);
                    break;
                case "April":
                    if (this.grdVacation.CurrentCell.Value != null)
                    {                                                
                        foreach (var row in selectedDt)
                        {
                            if (row.LeaveYYYYMM.Substring(row.LeaveYYYYMM.Length - 2) == "04")
                                lstLeaveDt.Add(Convert.ToDateTime(row.detailLeaveDate));
                        }
                    }

                    dlgcalendar.MonthOfLeaveDt = new DateTime(DateTime.Today.Year, 04, 01);
                    break;
                case "May":
                    if (this.grdVacation.CurrentCell.Value != null)
                    {                                                
                        foreach (var row in selectedDt)
                        {
                            if (row.LeaveYYYYMM.Substring(row.LeaveYYYYMM.Length - 2) == "05")
                                lstLeaveDt.Add(Convert.ToDateTime(row.detailLeaveDate));
                        }                        
                    }

                    dlgcalendar.MonthOfLeaveDt = new DateTime(DateTime.Today.Year, 05, 01);
                    break;
                case "June":
                    if (this.grdVacation.CurrentCell.Value != null)
                    {                                                
                        foreach (var row in selectedDt)
                        {
                            if (row.LeaveYYYYMM.Substring(row.LeaveYYYYMM.Length - 2) == "06")
                                lstLeaveDt.Add(Convert.ToDateTime(row.detailLeaveDate));
                        }
                    }

                    dlgcalendar.MonthOfLeaveDt = new DateTime(DateTime.Today.Year, 06, 01);
                    break;
                case "July":
                    if (this.grdVacation.CurrentCell.Value != null)
                    {                                       
                        foreach (var row in selectedDt)
                        {
                            if (row.LeaveYYYYMM.Substring(row.LeaveYYYYMM.Length - 2) == "07")
                                lstLeaveDt.Add(Convert.ToDateTime(row.detailLeaveDate));
                        }
                    }

                    dlgcalendar.MonthOfLeaveDt = new DateTime(DateTime.Today.Year, 07, 01);
                    break;
                case "August":
                    if (this.grdVacation.CurrentCell.Value != null)
                    {
                        foreach (var row in selectedDt)
                        {
                            if (row.LeaveYYYYMM.Substring(row.LeaveYYYYMM.Length - 2) == "08")
                                lstLeaveDt.Add(Convert.ToDateTime(row.detailLeaveDate));
                        }
                    }

                    dlgcalendar.MonthOfLeaveDt = new DateTime(DateTime.Today.Year, 08, 01);
                    break;
                case "September":
                    if (this.grdVacation.CurrentCell.Value != null)
                    {
                        foreach (var row in selectedDt)
                        {
                            if (row.LeaveYYYYMM.Substring(row.LeaveYYYYMM.Length - 2) == "09")
                                lstLeaveDt.Add(Convert.ToDateTime(row.detailLeaveDate));
                        }
                    }

                    dlgcalendar.MonthOfLeaveDt = new DateTime(DateTime.Today.Year, 09, 01);
                    break;
                case "October":
                    if (this.grdVacation.CurrentCell.Value != null)
                    {
                        foreach (var row in selectedDt)
                        {
                            if (row.LeaveYYYYMM.Substring(row.LeaveYYYYMM.Length - 2) == "10")
                                lstLeaveDt.Add(Convert.ToDateTime(row.detailLeaveDate));
                        }
                    }

                    dlgcalendar.MonthOfLeaveDt = new DateTime(DateTime.Today.Year, 10, 01);
                    break;
                case "November":
                    if (this.grdVacation.CurrentCell.Value != null)
                    {                                                
                        foreach (var row in selectedDt)
                        {
                            if (row.LeaveYYYYMM.Substring(row.LeaveYYYYMM.Length - 2) == "11")
                                lstLeaveDt.Add(Convert.ToDateTime(row.detailLeaveDate));
                        }
                    }

                    dlgcalendar.MonthOfLeaveDt = new DateTime(DateTime.Today.Year, 11, 01);
                    break;
                case "December":
                    if (this.grdVacation.CurrentCell.Value != null)
                    {                                               
                        foreach (var row in selectedDt)
                        {
                            if (row.LeaveYYYYMM.Substring(row.LeaveYYYYMM.Length - 2) == "12")
                                lstLeaveDt.Add(Convert.ToDateTime(row.detailLeaveDate));
                        }
                    }

                    dlgcalendar.MonthOfLeaveDt = new DateTime(DateTime.Today.Year, 12, 01);
                    break;
                default:
                    return;
            }           
            dlgcalendar.PassLeaveDtValue = lstLeaveDt;
            dlgcalendar.StartPosition = FormStartPosition.CenterParent;

            dlgcalendar.ShowDialog();

            if (dlgcalendar.PassLeaveDtValue.Count > 0)
            {                     
                string strdetaildt = string.Empty;

                //월별 상세 휴가 날짜 저장.
                if (dlgcalendar.CntLeaveDt > 0)
                {                   
                        for (int cntdt = 0; cntdt < dlgcalendar.CntLeaveDt; cntdt++)
                    {
                        strdetaildt += "'" + dlgcalendar.PassLeaveDtValue[cntdt].ToShortDateString() + "',";
                    }

                    strdetaildt = strdetaildt.Substring(0, strdetaildt.Length - 1);
                }
                
                //변경사항이 있는 월의 사용일수를 총 휴가사용일수에 반영해줌(해당 월의 사용 수 빼고 새로운 값 더해줌)
                this.grdVacation.Rows[this.grdVacation.CurrentCell.RowIndex].Cells["UsedDays"].Value = Convert.ToInt32(this.grdVacation.Rows[this.grdVacation.CurrentCell.RowIndex].Cells["UsedDays"].Value) - Convert.ToInt32(this.grdVacation.CurrentCell.Value) ;
                
                this.grdVacation.Rows[this.grdVacation.CurrentCell.RowIndex].Cells["UsedDays"].Value = Convert.ToInt32(this.grdVacation.Rows[this.grdVacation.CurrentCell.RowIndex].Cells["UsedDays"].Value) + Convert.ToInt32(dlgcalendar.CntLeaveDt);
                //총 휴가사용일수 갱신
                this.grdVacation.Rows[this.grdVacation.CurrentCell.RowIndex].Cells["TotalUsedDays"].Value = this.grdVacation.Rows[this.grdVacation.CurrentCell.RowIndex].Cells["UsedDays"].Value;
                //잔여일수 
                this.grdVacation.Rows[this.grdVacation.CurrentCell.RowIndex].Cells["RestDays"].Value = Convert.ToInt32(this.grdVacation.Rows[this.grdVacation.CurrentCell.RowIndex].Cells["UpdatedDayoff"].Value) - Convert.ToInt32(this.grdVacation.Rows[this.grdVacation.CurrentCell.RowIndex].Cells["TotalUsedDays"].Value);
                //해당 월에 업데이트된 일수 저장
                this.grdVacation.CurrentCell.Value = dlgcalendar.CntLeaveDt;
                //해당 월에 업데이트된 휴가 날짜 데이터 저장
                this.grdVacation.Rows[this.grdVacation.CurrentCell.RowIndex].Cells[this.grdVacation.CurrentCell.ColumnIndex + 1].Value = strdetaildt;

            }
            else // 저장된 휴가 날짜가 하나도 없는 경우
            {  
                //변경사항이 있는 월의 사용일수를 총 휴가사용일수에 반영해줌(해당 월의 사용 수 빼고 새로운 값 더해줌)
                this.grdVacation.Rows[this.grdVacation.CurrentCell.RowIndex].Cells["UsedDays"].Value = Convert.ToInt32(this.grdVacation.Rows[this.grdVacation.CurrentCell.RowIndex].Cells["UsedDays"].Value) - Convert.ToInt32(this.grdVacation.CurrentCell.Value);

                this.grdVacation.Rows[this.grdVacation.CurrentCell.RowIndex].Cells["UsedDays"].Value = Convert.ToInt32(this.grdVacation.Rows[this.grdVacation.CurrentCell.RowIndex].Cells["UsedDays"].Value) + Convert.ToInt32(dlgcalendar.CntLeaveDt);
                //총 휴가사용일수 갱신
                this.grdVacation.Rows[this.grdVacation.CurrentCell.RowIndex].Cells["TotalUsedDays"].Value = this.grdVacation.Rows[this.grdVacation.CurrentCell.RowIndex].Cells["UsedDays"].Value;
                //잔여일수 
                this.grdVacation.Rows[this.grdVacation.CurrentCell.RowIndex].Cells["RestDays"].Value = Convert.ToInt32(this.grdVacation.Rows[this.grdVacation.CurrentCell.RowIndex].Cells["UpdatedDayoff"].Value) - Convert.ToInt32(this.grdVacation.Rows[this.grdVacation.CurrentCell.RowIndex].Cells["TotalUsedDays"].Value);
                //기존 휴가 데이터가 있었는데 삭제된 경우는 '0'으로 수정되었음을 표시.
                if (this.grdVacation.CurrentCell.Value != null)
                    this.grdVacation.CurrentCell.Value = dlgcalendar.CntLeaveDt;

                //선택한 월의 1일 날짜 
                this.grdVacation.Rows[this.grdVacation.CurrentCell.RowIndex].Cells[this.grdVacation.CurrentCell.ColumnIndex + 1].Value = dlgcalendar.MonthOfLeaveDt.ToShortDateString();
            }
        }

        private void btnExcelExport_Click(object sender, EventArgs e)
        {
            if (this.grdVacation.Rows.Count < 1)
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
            if (grdVacation.Rows.Count == 1)
            {
                MessageBox.Show("엑셀 파일로 저장할 데이터가 없습니다.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                worksheet = workbook.ActiveSheet;

                int cellRowIndex = 1;
                int cellColumsIndex = 1;


                for (int col = 1; col < grdVacation.Columns.Count; col++)
                {
                    if (cellRowIndex == 1)
                    {
                        if (grdVacation.Columns[col].Visible == true)
                        {
                            worksheet.Cells[cellRowIndex, cellColumsIndex] = grdVacation.Columns[col].HeaderText;
                            worksheet.Cells[cellRowIndex, cellColumsIndex].Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Aqua);
                        }
                        else
                            continue;
                    }
                    cellColumsIndex++;
                }

                cellColumsIndex = 1;
                cellRowIndex++;

                for (int row = 0; row < grdVacation.Rows.Count; row++)
                {
                    for (int col = 1; col < grdVacation.Columns.Count; col++)
                    {
                        if (grdVacation.Columns[col].Visible != true)
                        {
                            continue;
                        }

                            if (grdVacation.Rows[row].Cells[col].Value != null)
                            worksheet.Cells[cellRowIndex, cellColumsIndex] = grdVacation.Rows[row].Cells[col].Value.ToString();
                        else
                            worksheet.Cells[cellRowIndex, cellColumsIndex] = "";

                        cellColumsIndex++;
                    }
                    cellColumsIndex = 1;
                    cellRowIndex++;
                }

                //셀 테두리 범위 설정
                string startRange = "A1";
                string endRange = "V" + (this.grdVacation.Rows.Count + 1).ToString();

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
            saveDialog.FileName = "JETSPURT_" + this.Text + "_" + DateTime.Today.ToString().Trim().Substring(0,10);

            return saveDialog;
        }

        private void btnVacationstdPop_Click(object sender, EventArgs e)
        {
            VacationStandardPopup vacationStandardPopup = new VacationStandardPopup();
            vacationStandardPopup.StartPosition = FormStartPosition.CenterParent;

            vacationStandardPopup.ShowDialog();
        }

        private void btnDeleteAll_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (DataGridViewRow dr in grdVacation.Rows)
                {
                    dr.Cells["TotalUsedDays"].Value = 0;
                    dr.Cells["UsedDays"].Value = 0;
                    dr.Cells["RestDays"].Value = dr.Cells["UpdatedDayOff"].Value;

                    int calender = 1;

                    for (int months = this.grdVacation.Columns["January"].Index; months < this.grdVacation.Columns["December"].Index; months += 2)
                    {
                        if (dr.Cells[months].Value != null)
                        {
                            dr.Cells[months].Value = 0;
                            dr.Cells[months + 1].Value = DateTime.Now.Year + "-" + calender.ToString().PadLeft(2, '0');
                        }
                        else
                            dr.Cells[months].Value = null;

                        calender++;
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
