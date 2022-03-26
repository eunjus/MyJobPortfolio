using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using SmartViceDev.CubridUtils;
using System.Drawing;


namespace SmartViceDev
{
    public partial class DepartmentManagement : Form
    {
        //조회조건 리스트
        Dictionary<string, string> MessageSet = new Dictionary<string, string>();
        DataTable dtForchk = new DataTable();
        bool chkSearched = true;
        CheckBox cb = new CheckBox();

        public DepartmentManagement()
        {
            InitializeComponent();

            eventslist(); //이벤트 처리 함수
            grdColumnAdd(); // 그리드 컬럼 형성

        }

        private void DepartmentManagement_Load(object sender, EventArgs e)
        {           
            btnSearch_Click(null, null);
            grdDepart.RowHeadersVisible = false;
        }

        private void eventslist()
        {
            //this.Load += new System.EventHandler(this.DepartmentManagement_Load);
            this.Shown += new System.EventHandler(this.DepartmentManagement_Shown);

            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);            

            this.grdDepart.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.grdDepart_CellValueChanged);
            this.grdDepart.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.grdDepart_CellPainting);
        }

        private void grdColumnAdd()
        {
            DataSet ds = new DataSet();
            RULE_DEPARTMENTMANAGEMENT smartViceData = new RULE_DEPARTMENTMANAGEMENT();

            var chkCol = new DataGridViewCheckBoxColumn
            {
                Name = "colCheck",
                HeaderText = ""
            };
            this.grdDepart.Columns.Add(chkCol);
            this.grdDepart.Columns["colCheck"].Width = 50;

            //부서 아이디
            this.grdDepart.Columns.Add("depart_id", "부서 아이디");
            //부서 이름
            this.grdDepart.Columns.Add("depart_name", "부서 이름");
            //정렬 순서
            this.grdDepart.Columns.Add("depart_sort_order", "정렬 순서");
            this.grdDepart.Columns["depart_sort_order"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            //설명
            this.grdDepart.Columns.Add("comment", "설명");
            this.grdDepart.Columns["comment"].Width = 200;
            //등록 날짜
            this.grdDepart.Columns.Add("reg_dt", "등록 날짜");
            this.grdDepart.Columns["reg_dt"].ReadOnly = true;
            this.grdDepart.Columns["reg_dt"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //수정 날짜
            this.grdDepart.Columns.Add("update_dt", "수정 날짜");
            this.grdDepart.Columns["update_dt"].ReadOnly = true;
            this.grdDepart.Columns["update_dt"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //ROWSTATE
            this.grdDepart.Columns.Add("ROWSTATE", "ROWSTATE");
            this.grdDepart.Columns["ROWSTATE"].Visible = false;
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
                        this.grdDepart.Rows.Clear();
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
                this.grdDepart.Rows.Clear();
                chkSearched = false;
                return;
            }
            
        }

        public int Chksearch()
        {
            if (chkSearched == false) // 조회한 이력이 있을때만 그리드 체크 
            {

                for (int grdrow = 0; grdrow < this.grdDepart.Rows.Count; grdrow++)
                {
                    if (this.grdDepart.Rows[grdrow].Cells["ROWSTATE"].Value != null)
                    {
                        if (this.grdDepart.Rows[grdrow].Cells["ROWSTATE"].Value.ToString() == "Modified" || this.grdDepart.Rows[grdrow].Cells["ROWSTATE"].Value.ToString() == "Added"
                            || this.grdDepart.Rows[grdrow].Cells["ROWSTATE"].Value.ToString() == "Deleted")
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

            if (this.grdDepart.Rows.Count > 1)
            {
                this.grdDepart.Rows.Clear();
            }

            RULE_DEPARTMENTMANAGEMENT smartViceData = new RULE_DEPARTMENTMANAGEMENT();

            DataSet ds = new DataSet();
            ds = smartViceData.Search(MessageSet);

            DataTable dt = ds.Tables[0].Copy();

            if (dt.Rows.Count < 1)
                return -1;

            dtForchk = ds.Tables[0].Copy();

            if (this.grdDepart.Rows.Count > 1)
                this.grdDepart.Rows.Clear();

            for (int row = 0; row < dt.Rows.Count; row++)
            {
                this.grdDepart.Rows.Add();
                this.grdDepart["depart_id", row].Value = dt.Rows[row]["depart_id"].ToString();
                this.grdDepart["depart_name", row].Value = dt.Rows[row]["depart_name"].ToString();
                this.grdDepart["depart_sort_order", row].Value = dt.Rows[row]["depart_sort_order"].ToString();                
                this.grdDepart["comment", row].Value = dt.Rows[row]["comment"].ToString();
                this.grdDepart["reg_dt", row].Value = dt.Rows[row]["reg_dt"].ToString();
                this.grdDepart["update_dt", row].Value = dt.Rows[row]["update_dt"].ToString();
                this.grdDepart["ROWSTATE", row].Value = "";
            }

            // 데이터에 맞게 칼럼 사이즈 조정하기
            //for (int i = 0; i < grdDepart.Columns.Count; i++)
            //{
            //    grdDepart.AutoResizeColumn(i, DataGridViewAutoSizeColumnMode.AllCells);
            //}
            grdDepart.RowHeadersVisible = false;
            grdDepart.AllowUserToAddRows = false;  // 빈레코드 표시 안하기
            grdDepart.AlternatingRowsDefaultCellStyle.BackColor = Color.AliceBlue; // 그리드 행에 색 입히기
            grdDepart.CurrentCell = null;

            cb.Checked = false;
            chkSearched = false;

            return 0;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            int IdxNewRow = this.grdDepart.Rows.Add();
            this.grdDepart["reg_dt", IdxNewRow].Value = DateTime.Now;
            this.grdDepart["update_dt", IdxNewRow].Value = DateTime.Now;
            this.grdDepart["ROWSTATE", IdxNewRow].Value = "Added";
            //this.grdDepart["work_year", grdDepart.CurrentCell.RowIndex].Value = Convert.ToInt32(this.grdDepart["work_year", grdDepart.CurrentCell.RowIndex - 1].Value) + 1;            
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow dr in grdDepart.Rows) 
            {
                if (dr.Cells["colCheck"].Value != null && dr.Cells["colCheck"].Value.ToString().ToUpper() != "false".ToUpper()) 
                {
                    if (dr.Cells["ROWSTATE"].Value.ToString() == "Added")// 새롭게 추가된 행은 그리드에서 바로 삭제
                    {
                        grdDepart.Rows.Remove(dr);
                        continue;
                    }

                    dr.Visible = false;
                    this.grdDepart["ROWSTATE", dr.Index].Value = "Deleted";// 기존 데이터는 그리드에서 내용만 삭제하고 저장하면 DB에서 삭제 진행.
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

            RULE_DEPARTMENTMANAGEMENT smartViceData = new RULE_DEPARTMENTMANAGEMENT();
            resultdt = Save();

            if (resultdt == -1)
                MessageBox.Show("저장에 실패하였습니다.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else if (resultdt == 1)
            {
                MessageSet.Clear();
                grdDepart.Rows.Clear();

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
            RULE_DEPARTMENTMANAGEMENT smartViceData = new RULE_DEPARTMENTMANAGEMENT();

            bool chksave = true;
            DataSet ds = new DataSet();

            string QueryType = string.Empty;

            if (grdDepart.Rows.Count > 0)
            {
                //현재 그리드와 조회한 후 그리드를 비교 후 insert 또는 delete 작업            
                for (int grdrow = 0; grdrow < grdDepart.Rows.Count - 1; grdrow++)
                {
                    if (this.grdDepart.Rows[grdrow].Cells["ROWSTATE"].Value.ToString() == string.Empty)
                        continue;

                    if (this.grdDepart.Rows[grdrow].Cells["ROWSTATE"].Value.ToString() == "Added" || this.grdDepart.Rows[grdrow].Cells["ROWSTATE"].Value.ToString() == "Modified")
                    {

                        MessageSet.Clear();
                        MessageSet.Add("QueryType", (this.grdDepart.Rows[grdrow].Cells["ROWSTATE"].Value.ToString() == "Added" ? "Insert" : "Update"));
                        MessageSet.Add("Depart_id", this.grdDepart.Rows[grdrow].Cells["depart_id"].Value.ToString());
                        MessageSet.Add("Depart_name", this.grdDepart.Rows[grdrow].Cells["depart_name"].Value.ToString());
                        MessageSet.Add("Depart_sort_order", this.grdDepart.Rows[grdrow].Cells["depart_sort_order"].Value.ToString());                        
                        if (this.grdDepart.Rows[grdrow].Cells["comment"].Value.ToString() != string.Empty)      
                            MessageSet.Add("COMMENT", this.grdDepart.Rows[grdrow].Cells["comment"].Value.ToString());

                        MessageSet.Add("Reg_dt", this.grdDepart.Rows[grdrow].Cells["reg_dt"].Value.ToString());
                        MessageSet.Add("Update_dt", this.grdDepart.Rows[grdrow].Cells["update_dt"].Value.ToString());

                        chksave = smartViceData.Save(MessageSet);
                        
                        if (!chksave)
                        {
                            return -1;
                        }
                    }
                    else if (this.grdDepart.Rows[grdrow].Cells["ROWSTATE"].Value.ToString() == "Deleted")
                    {
                        QueryType = "Delete";
                        MessageSet.Clear();
                        MessageSet.Add("QueryType", QueryType);
                        MessageSet.Add("Depart_id", this.grdDepart.Rows[grdrow].Cells["depart_id"].Value.ToString());
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
            for (int row = 0; row < this.grdDepart.Rows.Count - 1; row++)
            {
                if (this.grdDepart.Rows[row].Cells["depart_id"].Value == null)
                {
                    MessageBox.Show(this.grdDepart.Columns["depart_id" ].HeaderText + " 값이 누락되었습니다.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return -1;
                }

                if (this.grdDepart.Rows[row].Cells["depart_name"].Value == null)
                {
                    MessageBox.Show(this.grdDepart.Columns["depart_name"].HeaderText + " 값이 누락되었습니다.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return -1;
                }

                if (this.grdDepart.Rows[row].Cells["depart_sort_order"].Value == null)
                {
                    MessageBox.Show(this.grdDepart.Columns["depart_sort_order"].HeaderText + " 값이 누락되었습니다.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return -1;
                }

                //새로 추가된 행 중에 그리드에 있는 work_year 값과 중복되는 데이터 입력시 Warning!
                if (this.grdDepart.Rows[row].Cells["ROWSTATE"].Value.ToString() == "Added")
                {
                    int checkdr = dtForchk.AsEnumerable().Count(dr => dr.Field<string>("depart_id").ToString() == this.grdDepart.Rows[row].Cells["depart_id"].Value.ToString());

                    if (checkdr > 0)
                    {
                        MessageBox.Show(this.grdDepart.Columns["depart_id"].HeaderText + " 값은 중복될 수 없습니다.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); // 키 중복확인
                        return 1;
                    }
                }
            }
            return 0;
        }
        private void grdDepart_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (chkSearched == true)
                return;

            if (e.ColumnIndex == grdDepart.Columns["depart_id"].Index || e.ColumnIndex == grdDepart.Columns["depart_name"].Index
                || e.ColumnIndex == grdDepart.Columns["depart_sort_order"].Index || e.ColumnIndex == grdDepart.Columns["COMMENT"].Index)
            {
                //기존 데이터나 수정된 기존 데이터에만 modified 표시 
                if (this.grdDepart.Rows[e.RowIndex].Cells["ROWSTATE"].Value == null || this.grdDepart.Rows[e.RowIndex].Cells["ROWSTATE"].Value.ToString() == "Modified"
                    || this.grdDepart.Rows[e.RowIndex].Cells["ROWSTATE"].Value.ToString() == "")
                    this.grdDepart.Rows[e.RowIndex].Cells["ROWSTATE"].Value = "Modified";

                chkSearched = false;
            }
        }
        private void grdDepart_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
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
                cb.CheckedChanged += new EventHandler(grdDepart_CheckedChanged);

                this.grdDepart.Controls.Add(cb);
                //((DataGridView)sender).Controls.Add(cb);

                e.Handled = true;
            }
        }
        private void grdDepart_CheckedChanged(object sender, EventArgs e)
        {
            grdDepart.CurrentCell = null;

            if (cb.CheckState == CheckState.Checked)
                this.grdDepart.SelectAll();
            else
                grdDepart.ClearSelection();

            foreach (DataGridViewRow r in grdDepart.Rows)
            {
                if (cb.CheckState == CheckState.Checked)
                    r.Cells["colCheck"].Value = true;
                else
                    r.Cells["colCheck"].Value = false;

            }
        }

        private void DepartmentManagement_Shown(object sender, EventArgs e)
        {
            //grdDepart.ClearSelection();
            grdDepart.CurrentCell = null;
        }

        private void grdDepart_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)
                return;

            switch (grdDepart.Columns[e.ColumnIndex].Name)
            {
                case "colCheck":
                    this.grdDepart.CurrentRow.Selected = true;
                    break;
            }
        }
    }
}
