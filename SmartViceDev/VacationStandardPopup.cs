using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using SmartViceDev.CubridUtils;
using System.IO;
using System.Data.OleDb;
using System.Drawing;
//using Microsoft.Office.Interop.Excel;

namespace SmartViceDev
{
    public partial class VacationStandardPopup : Form
    {
        //조회조건 리스트
        Dictionary<string, string> MessageSet = new Dictionary<string, string>();
        DataTable dtForchk = new DataTable();
        bool chkSearched = true;
        CheckBox cb = new CheckBox();

        public VacationStandardPopup()
        {
            InitializeComponent();

            eventslist(); //이벤트 처리 함수
            grdColumnAdd(); // 그리드 컬럼 형성

        }

        private void VacationStandardPopup_Load(object sender, EventArgs e)
        {            
            this.btnSearch_Click(null,null); 
        }

        private void eventslist()
        {

            this.Load += new System.EventHandler(this.VacationStandardPopup_Load);
            this.Shown += new System.EventHandler(this.VacationStandardPopup_Shown);

            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            this.btnexportExcel.Click += new System.EventHandler(this.btnexportExcel_Click);
            this.btnImportExcel.Click += new System.EventHandler(this.btnImportExcel_Click);

            this.grdstdVacation.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.grdstdVacation_CellValueChanged);
            this.grdstdVacation.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.grdstdVacation_CellPainting);

        }

        private void grdColumnAdd()
        {
            DataSet ds = new DataSet();
            RULE_VACATIONSTANDARDPOPUP smartViceData = new RULE_VACATIONSTANDARDPOPUP();

            var chkCol = new DataGridViewCheckBoxColumn
            {
                Name = "colCheck",
                HeaderText = ""
            };
            this.grdstdVacation.Columns.Add(chkCol);
            this.grdstdVacation.Columns["colCheck"].Width = 50;

            //근속년수
            this.grdstdVacation.Columns.Add("work_year", "근속년수");
            //휴가생성일수
            this.grdstdVacation.Columns.Add("leave_create_day", "휴가생성일수");
            //휴가가산일수
            this.grdstdVacation.Columns.Add("leave_add_day", "휴가가산일수");
            //휴가합계일수
            this.grdstdVacation.Columns.Add("leave_total_day", "휴가합계일수");
            //갱신연차수
            this.grdstdVacation.Columns.Add("comment", "설명");
            //연차
            this.grdstdVacation.Columns.Add("reg_dt", "등록 날짜");
            this.grdstdVacation.Columns["reg_dt"].ReadOnly = true;
            //추가일수
            this.grdstdVacation.Columns.Add("update_dt", "수정 날짜");
            this.grdstdVacation.Columns["update_dt"].ReadOnly = true;
            //ROWSTATE
            this.grdstdVacation.Columns.Add("ROWSTATE", "ROWSTATE");
            //this.grdstdVacation.Columns["ROWSTATE"].Visible = false;
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
                        this.grdstdVacation.Rows.Clear();
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
                this.grdstdVacation.Rows.Clear();
                chkSearched = false;
                return;
            }
            
        }

        public int Chksearch()
        {
            if (chkSearched == false) // 조회한 이력이 있을때만 그리드 체크 
            {

                for (int grdrow = 0; grdrow < this.grdstdVacation.Rows.Count; grdrow++)
                {
                    if (this.grdstdVacation.Rows[grdrow].Cells["ROWSTATE"].Value != null)
                    {
                        if (this.grdstdVacation.Rows[grdrow].Cells["ROWSTATE"].Value.ToString() == "Modified" || this.grdstdVacation.Rows[grdrow].Cells["ROWSTATE"].Value.ToString() == "Added"
                            || this.grdstdVacation.Rows[grdrow].Cells["ROWSTATE"].Value.ToString() == "Deleted")
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

            if (this.grdstdVacation.Rows.Count > 1)
            {
                this.grdstdVacation.Rows.Clear();
            }

            RULE_VACATIONSTANDARDPOPUP smartViceData = new RULE_VACATIONSTANDARDPOPUP();

            DataSet ds = new DataSet();
            ds = smartViceData.Search(MessageSet);

            DataTable dt = ds.Tables[0].Copy();

            if (dt.Rows.Count < 1)
                return -1;

            dtForchk = ds.Tables[0].Copy();

            if (this.grdstdVacation.Rows.Count > 1)
                this.grdstdVacation.Rows.Clear();

            for (int row = 0; row < dt.Rows.Count; row++)
            {
                this.grdstdVacation.Rows.Add();
                this.grdstdVacation["work_year", row].Value = dt.Rows[row]["work_year"].ToString();
                this.grdstdVacation["leave_create_day", row].Value = dt.Rows[row]["leave_create_day"].ToString();
                this.grdstdVacation["leave_add_day", row].Value = dt.Rows[row]["leave_add_day"].ToString();
                this.grdstdVacation["leave_total_day", row].Value = dt.Rows[row]["leave_totoal_day"].ToString();
                this.grdstdVacation["comment", row].Value = dt.Rows[row]["comment"].ToString();
                this.grdstdVacation["reg_dt", row].Value = dt.Rows[row]["reg_dt"].ToString();
                this.grdstdVacation["update_dt", row].Value = dt.Rows[row]["update_dt"].ToString();
                this.grdstdVacation["ROWSTATE", row].Value = "";
            }

            // 데이터에 맞게 칼럼 사이즈 조정하기
            for (int i = 0; i < grdstdVacation.Columns.Count; i++)
            {
                grdstdVacation.AutoResizeColumn(i, DataGridViewAutoSizeColumnMode.AllCells);
            }
            grdstdVacation.RowHeadersVisible = false;
            grdstdVacation.AllowUserToAddRows = false;  // 빈레코드 표시 안하기
            grdstdVacation.AlternatingRowsDefaultCellStyle.BackColor = Color.AliceBlue; // 그리드 행에 색 입히기
            grdstdVacation.CurrentCell = null;

            cb.Checked = false;
            chkSearched = false;

            return 0;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            int IdxNewRow = this.grdstdVacation.Rows.Add();
            this.grdstdVacation["reg_dt", IdxNewRow].Value = DateTime.Now;
            this.grdstdVacation["update_dt", IdxNewRow].Value = DateTime.Now;
            this.grdstdVacation["ROWSTATE", IdxNewRow].Value = "Added";
            //this.grdstdVacation["work_year", grdstdVacation.CurrentCell.RowIndex].Value = Convert.ToInt32(this.grdstdVacation["work_year", grdstdVacation.CurrentCell.RowIndex - 1].Value) + 1;            
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow dr in grdstdVacation.Rows) // 마지막 빈 행때문에 오류생김 그거 없앨 수는 없나? 체크할때마다 갑자기 추가됨; == bool.TrueString.ToUpper()
            {
                if (dr.Cells["colCheck"].Value != null && dr.Cells["colCheck"].Value.ToString().ToUpper() != "false".ToUpper()) // 새롭게 추가된 행이며 체크되어있는 행만 삭제가능
                {
                    if (dr.Cells["ROWSTATE"].Value.ToString() == "Added")
                    {
                        grdstdVacation.Rows.Remove(dr);
                        continue;
                    }

                    dr.Visible = false;
                    this.grdstdVacation["ROWSTATE", dr.Index].Value = "Deleted";
                }
            }
            MessageBox.Show("삭제되었습니다.", "inform", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            int resultdt = 1;

            int chkdt = chkSave();

            if (chkdt == -1 || chkdt == 1)
            {                
                return;
            }     

            RULE_VACATIONSTANDARDPOPUP smartViceData = new RULE_VACATIONSTANDARDPOPUP();
            resultdt = Save();

            if (resultdt == -1)
                MessageBox.Show("저장에 실패하였습니다.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else if (resultdt == 1)
            {
                MessageSet.Clear();
                grdstdVacation.Rows.Clear();

                Search(MessageSet);
                chkSearched = false;

                MessageBox.Show("저장하였습니다.", "inform", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (resultdt == 0)
                MessageBox.Show("저장할 데이터가 존재하지 않습니다.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
                MessageBox.Show("중복된 항목이 존재합니다.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //저장실패 - cellvaluechanged 이벤트에서 기존 work_year 값과 중복되는 값을 입력시 Warning창 띄워줘!!!!!!!!!!!!!!
        }

        private int Save()
        {
            RULE_VACATIONSTANDARDPOPUP smartViceData = new RULE_VACATIONSTANDARDPOPUP();

            bool chksave = true;
            DataSet ds = new DataSet();

            string QueryType = string.Empty;

            if (grdstdVacation.Rows.Count > 0)
            {
                //현재 그리드와 조회한 후 그리드를 비교 후 insert 또는 delete 작업            
                for (int grdrow = 0; grdrow < grdstdVacation.Rows.Count; grdrow++)
                {
                    if (this.grdstdVacation.Rows[grdrow].Cells["ROWSTATE"].Value.ToString() == string.Empty)
                        continue;

                    if (this.grdstdVacation.Rows[grdrow].Cells["ROWSTATE"].Value.ToString() == "Added" || this.grdstdVacation.Rows[grdrow].Cells["ROWSTATE"].Value.ToString() == "Modified")
                    {
                        //저장하기전 중복데이터 확인 - chkSave()에서 함
                        //MessageSet.Add("work_year", this.grdstdVacation.Rows[grdrow].Cells["work_year"].Value.ToString());

                        //ds = smartViceData.Search("MainSearch", MessageSet);

                        //if (ds.Tables[0].Rows.Count > 0)
                        //    return 2;
                        MessageSet.Clear();
                        MessageSet.Add("QueryType", (this.grdstdVacation.Rows[grdrow].Cells["ROWSTATE"].Value.ToString() == "Added" ? "Insert" : "Update"));
                        MessageSet.Add("Work_year", this.grdstdVacation.Rows[grdrow].Cells["work_year"].Value.ToString());
                        MessageSet.Add("Leave_create_day", this.grdstdVacation.Rows[grdrow].Cells["leave_create_day"].Value.ToString());
                        MessageSet.Add("Leave_add_day", this.grdstdVacation.Rows[grdrow].Cells["leave_add_day"].Value.ToString());
                        MessageSet.Add("Leave_total_day", this.grdstdVacation.Rows[grdrow].Cells["leave_total_day"].Value.ToString());
                        if (this.grdstdVacation.Rows[grdrow].Cells["COMMENT"].Value == null)
                            this.grdstdVacation.Rows[grdrow].Cells["COMMENT"].Value = " ";
                        MessageSet.Add("COMMENT", this.grdstdVacation.Rows[grdrow].Cells["COMMENT"].Value.ToString());
                        MessageSet.Add("Reg_dt", this.grdstdVacation.Rows[grdrow].Cells["reg_dt"].Value.ToString());
                        MessageSet.Add("Update_dt", this.grdstdVacation.Rows[grdrow].Cells["update_dt"].Value.ToString());

                        chksave = smartViceData.Save(MessageSet);
                        
                        if (!chksave)
                        {
                            return -1;
                        }
                    }
                    else if (this.grdstdVacation.Rows[grdrow].Cells["ROWSTATE"].Value.ToString() == "Deleted")
                    {
                        QueryType = "Delete";
                        MessageSet.Clear();
                        MessageSet.Add("QueryType", QueryType);
                        MessageSet.Add("Work_year", this.grdstdVacation.Rows[grdrow].Cells["work_year"].Value.ToString());
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
            for (int row = 0; row < this.grdstdVacation.Rows.Count; row++)
            {
                if (this.grdstdVacation.Rows[row].Cells["work_year"].Value == null)
                {
                    MessageBox.Show(this.grdstdVacation.Columns["work_year"].HeaderText + " 값이 누락되었습니다.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return -1;
                }

                if (this.grdstdVacation.Rows[row].Cells["leave_create_day"].Value == null)
                {
                    MessageBox.Show(this.grdstdVacation.Columns["leave_create_day"].HeaderText + " 값이 누락되었습니다.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return -1;
                }

                if (this.grdstdVacation.Rows[row].Cells["leave_total_day"].Value == null)
                {
                    MessageBox.Show(this.grdstdVacation.Columns["leave_total_day"].HeaderText + " 값이 누락되었습니다.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return -1;
                }

                //새로 추가된 행 중에 그리드에 있는 work_year 값과 중복되는 데이터 입력시 Warning!
                if (this.grdstdVacation.Rows[row].Cells["ROWSTATE"].Value.ToString() == "Added")
                {
                    int checkdr = dtForchk.AsEnumerable().Count(dr => dr.Field<int>("work_year").ToString() == this.grdstdVacation.Rows[row].Cells["work_year"].Value.ToString());

                    if (checkdr > 0)
                    {
                        MessageBox.Show(this.grdstdVacation.Columns["work_year"].HeaderText + " 값은 중복될 수 없습니다.", "inform", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return 1;
                    }
                }
            }
            return 0;
        }
        private void grdstdVacation_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
             if (chkSearched == true)
                return;

            //조회를 누르면 이 이벤트를 타서 실제로 조회된 데이터에서 수정을 했을 때 표시를 할 수가 없네 -_- 
            if (e.ColumnIndex == grdstdVacation.Columns["work_year"].Index || e.ColumnIndex == grdstdVacation.Columns["leave_create_day"].Index
                || e.ColumnIndex == grdstdVacation.Columns["leave_add_day"].Index || e.ColumnIndex == grdstdVacation.Columns["leave_total_day"].Index
                || e.ColumnIndex == grdstdVacation.Columns["COMMENT"].Index)
            { 
                //기존 데이터나 수정된 기존 데이터에만 modified 표시 
                if (this.grdstdVacation.Rows[e.RowIndex].Cells["ROWSTATE"].Value == null || this.grdstdVacation.Rows[e.RowIndex].Cells["ROWSTATE"].Value.ToString() == "Modified"
                    || this.grdstdVacation.Rows[e.RowIndex].Cells["ROWSTATE"].Value.ToString() == "")
                    this.grdstdVacation.Rows[e.RowIndex].Cells["ROWSTATE"].Value = "Modified";

                chkSearched = false;
            }

        }

        private void btnexportExcel_Click(object sender, EventArgs e)
        {
            if (this.grdstdVacation.Rows.Count < 1)
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
            if (grdstdVacation.Rows.Count == 1)
            {
                MessageBox.Show("엑셀 파일로 저장할 데이터가 없습니다.", "inform", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                worksheet = workbook.ActiveSheet;

                int cellRowIndex = 1;
                int cellColumsIndex = 1;


                for (int col = 1; col < grdstdVacation.Columns.Count - 1; col++)
                {
                    if (cellRowIndex == 1)
                    {
                        worksheet.Cells[cellRowIndex, cellColumsIndex] = grdstdVacation.Columns[col].HeaderText;
                        worksheet.Cells[cellRowIndex, cellColumsIndex].Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Aqua);
                    }
                    cellColumsIndex++;
                }

                cellColumsIndex = 1;
                cellRowIndex++;

                for (int row = 0; row < grdstdVacation.Rows.Count; row++)
                {
                    for (int col = 1; col < grdstdVacation.Columns.Count; col++)
                    {
                        if (grdstdVacation.Rows[row].Cells[col].Value != null)
                        {
                            worksheet.Cells[cellRowIndex, cellColumsIndex] = grdstdVacation.Rows[row].Cells[col].Value.ToString();                            
                        }
                        else
                            worksheet.Cells[cellRowIndex, cellColumsIndex] = "";

                        cellColumsIndex++;
                    }
                    cellColumsIndex = 1;
                    cellRowIndex++;
                }

                //셀 테두리 범위 설정
                string startRange = "A1";
                string endRange = "H" + (this.grdstdVacation.Rows.Count + 1).ToString();

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

        private void btnImportExcel_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();

            try
            {
                using (OpenFileDialog dlg = new OpenFileDialog()) // 파일 선택창 객체를 생성
                {
                    dlg.Filter = "Excel Files(2007이상)|*.xlsx|Excel Files(97~2003)|*.xls";
                    dlg.InitialDirectory = @"C:\";
                    if (dlg.ShowDialog() == DialogResult.OK)
                    {
                        dt = ImportExcelData_Read(dlg.FileName, grdstdVacation); // 메서드를 호출
                    }
                }
            }
            catch (Exception ex)  // 엑셀파일이 다른 프로그렘에서 이미 열었거나 에러가 발생하면 에러를 출력해준다.
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            if (grdstdVacation.Rows.Count > 1)
                grdstdVacation.Rows.Clear();

            for (int row = 0; row < dt.Rows.Count; row++)
            {
                //빈 레코드 처리 
                if (dt.Rows[row]["근속년수"].ToString() == "")
                    continue;

                this.grdstdVacation.Rows.Add();
                this.grdstdVacation["work_year", row].Value = dt.Rows[row]["근속년수"].ToString();
                this.grdstdVacation["leave_create_day", row].Value = dt.Rows[row]["휴가생성일수"].ToString();
                this.grdstdVacation["leave_add_day", row].Value = dt.Rows[row]["휴가가산일수"].ToString();
                this.grdstdVacation["leave_total_day", row].Value = dt.Rows[row]["휴가합계일수"].ToString();
                this.grdstdVacation["comment", row].Value = dt.Rows[row]["설명"].ToString();
                this.grdstdVacation["reg_dt", row].Value = dt.Rows[row]["등록 날짜"].ToString();
                this.grdstdVacation["update_dt", row].Value = dt.Rows[row]["수정 날짜"].ToString();
                this.grdstdVacation["ROWSTATE", row].Value = "Modified";                
            }

            // 데이터에 맞게 칼럼 사이즈 조정하기
            for (int i = 0; i < grdstdVacation.Columns.Count; i++)
            {
                grdstdVacation.AutoResizeColumn(i, DataGridViewAutoSizeColumnMode.AllCells);
            }
            
            chkSearched = false;

        }

        public DataTable ImportExcelData_Read(string fileName,DataGridView dgv)
        {
            // 엑셀 문서 내용 추출
            string connectionString = string.Empty;

            if (File.Exists(fileName))  // 파일 확장자 검사
            {
                if (Path.GetExtension(fileName).ToLower() == ".xls")
                {   // Microsoft.Jet.OLEDB.4.0 은 32 bit 에서만 동작되므로 빌드할 때 64비트로 하면 에러가 발생함.
                    connectionString = string.Format("Provider=Microsoft.Jet.OLEDB.4.0; Data Source={0};Extended Properties=Excel 8.0;", fileName);
                }
                else if (Path.GetExtension(fileName).ToLower() == ".xlsx")
                {
                    connectionString = string.Format("Provider=Microsoft.ACE.OLEDB.12.0; Data Source={0};Extended Properties=Excel 12.0;", fileName);
                }
            }

            DataSet data = new DataSet();

            string strQuery = "SELECT * FROM [Sheet1$]";  // 엑셀 시트명 Sheet1의 모든 데이터를 가져오기
            OleDbConnection oleConn = new OleDbConnection(connectionString);
            oleConn.Open();

            OleDbCommand oleCmd = new OleDbCommand(strQuery, oleConn);
            OleDbDataAdapter dataAdapter = new OleDbDataAdapter(oleCmd);

            DataTable dataTable = new DataTable();
            dataAdapter.Fill(dataTable);
            data.Tables.Add(dataTable);

            //dgv.DataSource = data.Tables[0].DefaultView;

            // 데이터에 맞게 칼럼 사이즈 조정하기
            for (int i = 0; i < dgv.Columns.Count; i++)
            {
                dgv.AutoResizeColumn(i, DataGridViewAutoSizeColumnMode.AllCells);
            }
            dgv.AllowUserToAddRows = false;  // 빈레코드 표시 안하기
            dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.AliceBlue;
            //dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill; // 화면크기에 맞춰 채우기

            dataTable.Dispose();
            dataAdapter.Dispose();
            oleCmd.Dispose();

            oleConn.Close();
            oleConn.Dispose();

            return dataTable;
        }

        private void grdstdVacation_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
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
                cb.CheckedChanged += new EventHandler(grdstdVacation_CheckedChanged);

                this.grdstdVacation.Controls.Add(cb);
                //((DataGridView)sender).Controls.Add(cb);

                e.Handled = true;
            }
        }
        private void grdstdVacation_CheckedChanged(object sender, EventArgs e)
        {
            grdstdVacation.CurrentCell = null;

            if (cb.CheckState == CheckState.Checked)
                this.grdstdVacation.SelectAll();
            else
                grdstdVacation.ClearSelection();

            foreach (DataGridViewRow r in grdstdVacation.Rows)
            {
                if (cb.CheckState == CheckState.Checked)
                    r.Cells["colCheck"].Value = true;
                else
                    r.Cells["colCheck"].Value = false;

            }
        }

        private void VacationStandardPopup_Shown(object sender, EventArgs e)
        {
            //grdstdVacation.ClearSelection();
            grdstdVacation.CurrentCell = null;
        }

        private void grdstdVacation_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)
                return;

            switch (grdstdVacation.Columns[e.ColumnIndex].Name)
            {
                case "colCheck":
                    this.grdstdVacation.CurrentRow.Selected = true;
                    break;
            }
        }
    }
}
