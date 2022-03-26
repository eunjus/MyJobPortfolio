using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using SmartViceDev.CubridUtils;
using System.Drawing;


namespace SmartViceDev
{
    public partial class StaffInfoManagement : Form
    {
        RULE_STAFFINFOMANAGEMENT smartViceData = new RULE_STAFFINFOMANAGEMENT();

        //조회조건 리스트
        Dictionary<string, string> MessageSet = new Dictionary<string, string>();
        DataTable dtForchk = new DataTable();
        bool chkSearched = true;
        CheckBox cb = new CheckBox();   

        public StaffInfoManagement()
        {
            InitializeComponent();

            eventslist(); //이벤트 처리 함수
            grdColumnAdd(); // 그리드 컬럼 형성
            comboboxListAdd();//콤보박스 초기화

        }

        private void StaffInfoManagement_Load(object sender, EventArgs e)
        {
            btnSearch_Click(null, null);
        }

        private void eventslist()
        {
            this.Load += new System.EventHandler(this.StaffInfoManagement_Load);
            this.Shown += new System.EventHandler(this.StaffInfoManagement_Shown);

            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            this.btnExcelExport.Click += new System.EventHandler(this.btnExcelExport_Click);

            this.grdStaff.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.grdStaff_CellPainting);
            this.grdStaff.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.grdDepart_CellValueChanged);
            this.grdStaff.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grdStaff_CellDoubleClick);
            

        }
        private void comboboxListAdd()
        {
            DataSet ds = new DataSet();
            //부서명 콤보박스 리스트업 
            ds = smartViceData.SearchCommon("seachDepart");

            this.cmbdepart.Items.Add("전체");
            for (int row = 0; row < ds.Tables[0].Rows.Count; row++)
            {
                this.cmbdepart.Items.Add(ds.Tables[0].Rows[row].Field<string>("depart_name").ToString());
            }
            this.cmbdepart.SelectedItem = "전체";

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
            DataSet ds = new DataSet();

            var chkCol = new DataGridViewCheckBoxColumn
            {
                Name = "colCheck",                
            };
            this.grdStaff.Columns.Add(chkCol);
            this.grdStaff.Columns["colCheck"].Width = 50;

            //직원 아이디
            this.grdStaff.Columns.Add("staff_id", "직원 아이디");
            this.grdStaff.Columns["staff_id"].ReadOnly = true;
            this.grdStaff.Columns["staff_id"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //직원 이름
            this.grdStaff.Columns.Add("staff_name", "직원 이름");
            this.grdStaff.Columns["staff_name"].ReadOnly = true;
            this.grdStaff.Columns["staff_name"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //직원 이메일
            this.grdStaff.Columns.Add("staff_email", "직원 이메일");
            this.grdStaff.Columns["staff_email"].Width = 200;
            this.grdStaff.Columns["staff_email"].ReadOnly = true;
            //부서
            this.grdStaff.Columns.Add("depart_name", "부서");
            this.grdStaff.Columns["depart_name"].ReadOnly = true;
            this.grdStaff.Columns["depart_name"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //직급
            this.grdStaff.Columns.Add("position_name", "직급");
            this.grdStaff.Columns["position_name"].ReadOnly = true;
            this.grdStaff.Columns["position_name"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
           
            //등록 날짜
            this.grdStaff.Columns.Add("reg_dt", "등록 날짜");
            this.grdStaff.Columns["position_name"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.grdStaff.Columns["reg_dt"].ReadOnly = true;            
            //수정 날짜
            this.grdStaff.Columns.Add("update_dt", "수정 날짜");
            this.grdStaff.Columns["position_name"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.grdStaff.Columns["update_dt"].ReadOnly = true;
            //ROWSTATE
            this.grdStaff.Columns.Add("ROWSTATE", "ROWSTATE");
            this.grdStaff.Columns["ROWSTATE"].Visible = false;

            this.grdStaff.AllowUserToAddRows = false;
            this.grdStaff.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            chkSearched = true;

            //int chkgrddt = Chksearch();

            int chkgrddt = 1;

            if (chkgrddt == 0)
            {
                DialogResult dialogResult = MessageBox.Show("저장되지 않은 데이터가 존재합니다. 그래도 조회하시겠습니까?\n(수정한 데이터를 잃게됩니다.)", "Warning", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
                if (dialogResult == DialogResult.Yes)
                {                   
                    int resultchk = Search(MessageSet);

                    if (resultchk == -1)
                    {
                        MessageBox.Show("조회된 데이터가 없습니다.","Alarm");
                        this.grdStaff.Rows.Clear();
                        chkSearched = false;
                        return;
                    }
                }
                else if (dialogResult == DialogResult.Cancel || dialogResult == DialogResult.No)
                    return;
            }

            MessageSet.Clear();
            MessageSet.Add("Position_name", cmbPosition.SelectedItem.ToString().Replace("전체",""));
            MessageSet.Add("Depart_name", cmbdepart.SelectedItem.ToString().Replace("전체", ""));

            int resultchk2 = Search(MessageSet);

            if (resultchk2 == -1)
            {
                MessageBox.Show("조회된 데이터가 없습니다.","Alarm");
                this.grdStaff.Rows.Clear();
                chkSearched = false;
                return;
            }           

        }

        public int Chksearch()
        {
            if (chkSearched == false) // 조회한 이력이 있을때만 그리드 체크 
            {

                for (int grdrow = 0; grdrow < this.grdStaff.Rows.Count; grdrow++)
                {
                    if (this.grdStaff.Rows[grdrow].Cells["ROWSTATE"].Value != null)
                    {
                        if (this.grdStaff.Rows[grdrow].Cells["ROWSTATE"].Value.ToString() == "Modified" || this.grdStaff.Rows[grdrow].Cells["ROWSTATE"].Value.ToString() == "Added"
                            || this.grdStaff.Rows[grdrow].Cells["ROWSTATE"].Value.ToString() == "Deleted")
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

            if (this.grdStaff.Rows.Count > 1)
            {
                this.grdStaff.Rows.Clear();
            }

            DataSet ds = new DataSet();
            ds = smartViceData.Search(MessageSet);

            DataTable dt = ds.Tables[0].Copy();

            if (dt.Rows.Count < 1)
                return -1;

            dtForchk = ds.Tables[0].Copy();

            if (this.grdStaff.Rows.Count > 1)
                this.grdStaff.Rows.Clear();

            for (int row = 0; row < dt.Rows.Count; row++)
            {
                this.grdStaff.Rows.Add();
                this.grdStaff["colCheck", row].Value = null;
                this.grdStaff["staff_id", row].Value = dt.Rows[row]["staff_id"].ToString();
                this.grdStaff["staff_name", row].Value = dt.Rows[row]["staff_name"].ToString();
                this.grdStaff["staff_email", row].Value = dt.Rows[row]["staff_email"].ToString();
                this.grdStaff["depart_name", row].Value = dt.Rows[row]["depart_name"].ToString();
                this.grdStaff["position_name", row].Value = dt.Rows[row]["position_name"].ToString();
                this.grdStaff["reg_dt", row].Value = dt.Rows[row]["reg_dt"].ToString();
                this.grdStaff["update_dt", row].Value = dt.Rows[row]["update_dt"].ToString();
                this.grdStaff["ROWSTATE", row].Value = "";
            }

            // 데이터에 맞게 칼럼 사이즈 조정하기
            //for (int i = 0; i < grdDepart.Columns.Count; i++)
            //{
            //    grdDepart.AutoResizeColumn(i, DataGridViewAutoSizeColumnMode.AllCells);
            //}
            grdStaff.RowHeadersVisible = false;
            grdStaff.AllowUserToAddRows = false;  // 빈레코드 표시 안하기
            grdStaff.AlternatingRowsDefaultCellStyle.BackColor = Color.AliceBlue; // 그리드 행에 색 입히기
            grdStaff.CurrentCell = null;
            
            cb.Checked = false;
            chkSearched = false;

            return 0;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            NewStaffRegPopup staffRegPopup = new NewStaffRegPopup();
            staffRegPopup.StartPosition = FormStartPosition.CenterParent;

            staffRegPopup.ShowDialog();

            if (staffRegPopup.PassMessageSet.Keys.Count > 0)
            {
                int IdxNewRow = this.grdStaff.Rows.Add();
                this.grdStaff["staff_id", IdxNewRow].Value = staffRegPopup.PassMessageSet["staff_id"].ToString();
                this.grdStaff["staff_name", IdxNewRow].Value = staffRegPopup.PassMessageSet["staff_name"].ToString();
                this.grdStaff["staff_email", IdxNewRow].Value = staffRegPopup.PassMessageSet["staff_email"].ToString();
                this.grdStaff["depart_name", IdxNewRow].Value = staffRegPopup.PassMessageSet["staff_dep"].ToString();
                this.grdStaff["position_name", IdxNewRow].Value = staffRegPopup.PassMessageSet["staff_pos"].ToString();
                this.grdStaff["reg_dt", IdxNewRow].Value = DateTime.Now;
                this.grdStaff["update_dt", IdxNewRow].Value = DateTime.Now;
                this.grdStaff["ROWSTATE", IdxNewRow].Value = "New";
                //this.grdDepart["work_year", grdDepart.CurrentCell.RowIndex].Value = Convert.ToInt32(this.grdDepart["work_year", grdDepart.CurrentCell.RowIndex - 1].Value) + 1;            
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int resultdt = 1;

            foreach (DataGridViewRow dr in grdStaff.Rows) 
            {
                if (dr.Cells["colCheck"].Value != null && dr.Cells["colCheck"].Value.ToString().ToUpper() != "false".ToUpper()) 
                {
                    //if (dr.Cells["ROWSTATE"].Value.ToString() == "New")// 새롭게 추가된 행은 그리드에서 바로 삭제
                    //{
                        grdStaff.Rows.Remove(dr);
                        continue;
                    //}

                    //dr.Visible = false;
                    //this.grdStaff["ROWSTATE", dr.Index].Value = "Deleted";// 기존 데이터는 그리드에서 내용만 삭제하고 저장하면 DB에서 삭제 진행.
                    //resultdt = Save() * resultdt;
                }
            }

            if (resultdt == -1)
                MessageBox.Show("삭제 중에 오류가 발생하였습니다.");
            else if (resultdt == 1)
            {
                MessageSet.Clear();

                MessageBox.Show("정상적으로 삭제하였습니다.");
            }
            else if (resultdt == 0)
                MessageBox.Show("삭제할 데이터가 존재하지 않습니다.");
        }

        //private void btnSave_Click(object sender, EventArgs e)
        //{
        //    int resultdt = 1;

        //    int chkdt = chksave();

        //    if (chkdt == -1)
        //    {
        //        MessageBox.Show("필수 데이터가 누락되었습니다.", "Warning");
        //        return;
        //    }
        //    else if (chkdt == 1)
        //    {
        //        MessageBox.Show( this.grdStaff.Columns["staff_id"].HeaderText + " 값은 중복될 수 없습니다."); // 키 중복확인
        //        return;
        //    }

        //    resultdt = Save();

        //    if (resultdt == -1)
        //        MessageBox.Show("저장에 실패하였습니다.","Alarm");
        //    else if (resultdt == 1)
        //    {
        //        MessageSet.Clear();
        //        grdStaff.Rows.Clear();

        //        Search(MessageSet);
        //        chkSearched = false;

        //        MessageBox.Show("저장하였습니다.","Alarm");
        //    }
        //    else if (resultdt == 0)
        //        MessageBox.Show("저장할 데이터가 존재하지 않습니다.","Warning");
        //    else
        //        MessageBox.Show("중복된 항목이 존재합니다.");
        //    //저장실패 - cellvaluechanged 이벤트에서 기존 work_year 값과 중복되는 값을 입력시 Warning창 띄워줘!!!!!!!!!!!!!!
        //}

        private int Save()
        {
            bool chksave = true;
            DataSet ds = new DataSet();

            string QueryType = string.Empty;

            if (grdStaff.Rows.Count > 0)
            {
                //현재 그리드와 조회한 후 그리드를 비교 후 insert 또는 delete 작업            
                for (int grdrow = 0; grdrow < grdStaff.Rows.Count; grdrow++)
                {
                    if (this.grdStaff.Rows[grdrow].Cells["ROWSTATE"].Value.ToString() == string.Empty)
                        continue;

                    if (this.grdStaff.Rows[grdrow].Cells["ROWSTATE"].Value.ToString() == "Added" || this.grdStaff.Rows[grdrow].Cells["ROWSTATE"].Value.ToString() == "Modified")
                    {
                        MessageSet.Clear();
                        MessageSet.Add("QueryType", (this.grdStaff.Rows[grdrow].Cells["ROWSTATE"].Value.ToString() == "Added" ? "Insert" : "Update"));
                        MessageSet.Add("staff_id", this.grdStaff.Rows[grdrow].Cells["staff_id"].Value.ToString());
                        MessageSet.Add("staff_name", this.grdStaff.Rows[grdrow].Cells["staff_name"].Value.ToString());
                        MessageSet.Add("Depart_sort_order", this.grdStaff.Rows[grdrow].Cells["depart_sort_order"].Value.ToString());
                        if (this.grdStaff.Rows[grdrow].Cells["COMMENT"].Value == null)
                            this.grdStaff.Rows[grdrow].Cells["COMMENT"].Value = " ";
                        MessageSet.Add("COMMENT", this.grdStaff.Rows[grdrow].Cells["COMMENT"].Value.ToString());
                        MessageSet.Add("Reg_dt", this.grdStaff.Rows[grdrow].Cells["reg_dt"].Value.ToString());
                        MessageSet.Add("Update_dt", this.grdStaff.Rows[grdrow].Cells["update_dt"].Value.ToString());

                        chksave = smartViceData.Save(MessageSet);

                        if (!chksave)
                        {
                            return -1;
                        }
                    }
                    else if (this.grdStaff.Rows[grdrow].Cells["ROWSTATE"].Value.ToString() == "Deleted")
                    {
                        QueryType = "Delete";
                        MessageSet.Clear();
                        MessageSet.Add("QueryType", QueryType);
                        MessageSet.Add("staff_id", this.grdStaff.Rows[grdrow].Cells["staff_id"].Value.ToString());
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

        //public int chksave()
        //{
        //    for (int row = 0; row < this.grdStaff.Rows.Count; row++)
        //    {
        //        if (this.grdStaff.Rows[row].Cells["staff_id"].Value == null)
        //            return -1;

        //        if (this.grdStaff.Rows[row].Cells["staff_name"].Value == null)
        //            return -1;

        //        if (this.grdStaff.Rows[row].Cells["depart_sort_order"].Value == null)
        //            return -1;

        //        //새로 추가된 행 중에 그리드에 있는 work_year 값과 중복되는 데이터 입력시 Warning!
        //        if (this.grdStaff.Rows[row].Cells["ROWSTATE"].Value.ToString() == "Added")
        //        {
        //            int checkdr = dtForchk.AsEnumerable().Count(dr => dr.Field<string>("staff_id").ToString() == this.grdStaff.Rows[row].Cells["staff_id"].Value.ToString());

        //            if (checkdr > 0)
        //            {
        //                return 1;
        //            }
        //        }
        //    }
        //    return 0;
        //}

        private void grdDepart_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (chkSearched == true)
                return;

            //if (e.ColumnIndex == grdStaff.Columns["staff_id"].Index || e.ColumnIndex == grdStaff.Columns["staff_name"].Index
            //    || e.ColumnIndex == grdStaff.Columns["depart_sort_order"].Index || e.ColumnIndex == grdStaff.Columns["COMMENT"].Index)
            //{
            //    //기존 데이터나 수정된 기존 데이터에만 modified 표시 
            //    if (this.grdStaff.Rows[e.RowIndex].Cells["ROWSTATE"].Value == null || this.grdStaff.Rows[e.RowIndex].Cells["ROWSTATE"].Value.ToString() == "Modified"
            //        || this.grdStaff.Rows[e.RowIndex].Cells["ROWSTATE"].Value.ToString() == "")
            //        this.grdStaff.Rows[e.RowIndex].Cells["ROWSTATE"].Value = "Modified";

            //    chkSearched = false;
            //}

        }

        private void grdStaff_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {            
            NewStaffRegPopup staffRegPopup = new NewStaffRegPopup();
            staffRegPopup.StartPosition = FormStartPosition.CenterParent;

            if (this.grdStaff.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
                staffRegPopup.ReciveStaffID = this.grdStaff.Rows[e.RowIndex].Cells[this.grdStaff.Columns["staff_id"].Index].Value.ToString();

            staffRegPopup.ShowDialog();

            if (staffRegPopup.PassMessageSet.Keys.Count > 0)
            {
                this.grdStaff.Rows[e.RowIndex].Cells["staff_id"].Value = staffRegPopup.PassMessageSet["staff_id"].ToString();
                this.grdStaff.Rows[e.RowIndex].Cells["staff_name"].Value = staffRegPopup.PassMessageSet["staff_name"].ToString();
                this.grdStaff.Rows[e.RowIndex].Cells["staff_email"].Value = staffRegPopup.PassMessageSet["staff_email"].ToString();
                this.grdStaff.Rows[e.RowIndex].Cells["depart_name"].Value = staffRegPopup.PassMessageSet["staff_dep"].ToString();
                this.grdStaff.Rows[e.RowIndex].Cells["position_name"].Value = staffRegPopup.PassMessageSet["staff_pos"].ToString();
                this.grdStaff.Rows[e.RowIndex].Cells["ROWSTATE"].Value = "updated";
                
            }

        }

        private void btnExcelExport_Click(object sender, EventArgs e)
        {
            if (this.grdStaff.Rows.Count < 1)
            {
                MessageBox.Show("엑셀파일로 저장할 데이터가 없습니다.","Warning");
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
            if (grdStaff.Rows.Count == 1)
            {
                MessageBox.Show("Data does not exist.", "inform", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                worksheet = workbook.ActiveSheet;

                int cellRowIndex = 1;
                int cellColumsIndex = 1;


                for (int col = 1; col < grdStaff.Columns.Count; col++)
                {
                    if (cellRowIndex == 1)
                    {
                        if (grdStaff.Columns[col].Visible == true)
                        {
                            worksheet.Cells[cellRowIndex, cellColumsIndex] = grdStaff.Columns[col].HeaderText;
                            worksheet.Cells[cellRowIndex, cellColumsIndex].Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Aqua);
                        }
                        else
                            continue;
                    }
                    cellColumsIndex++;
                }

                cellColumsIndex = 1;
                cellRowIndex++;

                for (int row = 0; row < grdStaff.Rows.Count; row++)
                {
                    for (int col = 1; col < grdStaff.Columns.Count; col++)
                    {
                        if (grdStaff.Columns[col].Visible != true)
                        {
                            continue;
                        }

                        if (grdStaff.Rows[row].Cells[col].Value != null)
                            worksheet.Cells[cellRowIndex, cellColumsIndex] = grdStaff.Rows[row].Cells[col].Value.ToString();
                        else
                            worksheet.Cells[cellRowIndex, cellColumsIndex] = "";

                        cellColumsIndex++;
                    }
                    cellColumsIndex = 1;
                    cellRowIndex++;
                }

                //셀 테두리 범위 설정
                string startRange = "A1";
                string endRange = "G" + (this.grdStaff.Rows.Count + 1).ToString();

                // 전체  
                worksheet.get_Range(startRange, endRange).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;

                SaveFileDialog saveFileDialog = GetExcelSave();

                if (saveFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    workbook.SaveAs(saveFileDialog.FileName);
                    MessageBox.Show("Export Successful!");
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
                MessageBox.Show(ex.Message);
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

        private void grdStaff_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
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
                cb.CheckedChanged += new EventHandler(grdStaff_CheckedChanged);

                this.grdStaff.Controls.Add(cb);
                //((DataGridView)sender).Controls.Add(cb);

                e.Handled = true;
            }        
        }
        private void grdStaff_CheckedChanged(object sender, EventArgs e)
        {
            grdStaff.CurrentCell = null;

            if (cb.CheckState == CheckState.Checked)
                this.grdStaff.SelectAll();
            else
                grdStaff.ClearSelection();


            foreach (DataGridViewRow r in grdStaff.Rows)
            {                                
                if (cb.CheckState == CheckState.Checked)
                    r.Cells["colCheck"].Value = true;
                else
                    r.Cells["colCheck"].Value = false;                

            }
        }

        private void StaffInfoManagement_Shown(object sender, EventArgs e)
        {
            //grdStaff.ClearSelection();
            grdStaff.CurrentCell = null;
        }
    }
}
