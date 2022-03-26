using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using SmartViceDev.CubridUtils;
using System.Drawing;


namespace SmartViceDev
{
    public partial class JobPositionManagement : Form
    {
        //조회조건 리스트
        Dictionary<string, string> MessageSet = new Dictionary<string, string>();
        DataTable dtForchk = new DataTable();
        bool chkSearched = true;

        CheckBox cb = new CheckBox();

        public JobPositionManagement()
        {
            InitializeComponent();

            eventslist(); //이벤트 처리 함수
            grdColumnAdd(); // 그리드 컬럼 형성
        }

        private void JobPositionManagement_Load(object sender, EventArgs e)
        {
            btnSearch_Click(null, null);
        }

        private void eventslist()
        {
            this.Load += new System.EventHandler(this.JobPositionManagement_Load);
            this.Shown += new System.EventHandler(this.JobPositionManagement_Shown);

            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);            

            this.grdPosition.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.grdPosition_CellValueChanged);
            this.grdPosition.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.grdPosition_CellPainting);
        }

        private void grdColumnAdd()
        {
            DataSet ds = new DataSet();
            RULE_JOBPOSITIONMANAGEMENT smartViceData = new RULE_JOBPOSITIONMANAGEMENT();

            var chkCol = new DataGridViewCheckBoxColumn
            {
                Name = "colCheck",
                HeaderText = ""
            };
            this.grdPosition.Columns.Add(chkCol);
            this.grdPosition.Columns["colCheck"].Width = 50;

            //직책 아이디
            this.grdPosition.Columns.Add("position_id", "직책 아이디");
            this.grdPosition.Columns["position_id"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //직책 이름
            this.grdPosition.Columns.Add("position_name", "직책 이름");
            //정렬 순서
            this.grdPosition.Columns.Add("position_sort_order", "정렬 순서");
            this.grdPosition.Columns["position_sort_order"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            //설명
            this.grdPosition.Columns.Add("comment", "설명");
            this.grdPosition.Columns["comment"].Width = 200;
            //등록 날짜
            this.grdPosition.Columns.Add("reg_dt", "등록 날짜");
            this.grdPosition.Columns["reg_dt"].ReadOnly = true;
            this.grdPosition.Columns["reg_dt"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //수정 날짜
            this.grdPosition.Columns.Add("update_dt", "수정 날짜");
            this.grdPosition.Columns["update_dt"].ReadOnly = true;
            this.grdPosition.Columns["update_dt"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //ROWSTATE
            this.grdPosition.Columns.Add("ROWSTATE", "ROWSTATE");
            this.grdPosition.Columns["ROWSTATE"].Visible = false;
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
                        MessageBox.Show("조회된 데이터가 없습니다.","Alarm");
                        this.grdPosition.Rows.Clear();
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
                MessageBox.Show("조회된 데이터가 없습니다.","Alarm");
                this.grdPosition.Rows.Clear();
                chkSearched = false;
                return;
            }
            
        }

        public int Chksearch()
        {
            if (chkSearched == false) // 조회한 이력이 있을때만 그리드 체크 
            {

                for (int grdrow = 0; grdrow < this.grdPosition.Rows.Count; grdrow++)
                {
                    if (this.grdPosition.Rows[grdrow].Cells["ROWSTATE"].Value != null)
                    {
                        if (this.grdPosition.Rows[grdrow].Cells["ROWSTATE"].Value.ToString() == "Modified" || this.grdPosition.Rows[grdrow].Cells["ROWSTATE"].Value.ToString() == "Added"
                            || this.grdPosition.Rows[grdrow].Cells["ROWSTATE"].Value.ToString() == "Deleted")
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

            if (this.grdPosition.Rows.Count > 1)
            {
                this.grdPosition.Rows.Clear();
            }

            RULE_JOBPOSITIONMANAGEMENT smartViceData = new RULE_JOBPOSITIONMANAGEMENT();

            DataSet ds = new DataSet();
            ds = smartViceData.Search(MessageSet);

            DataTable dt = ds.Tables[0].Copy();

            if (dt.Rows.Count < 1)
                return -1;

            dtForchk = ds.Tables[0].Copy();

            if (this.grdPosition.Rows.Count > 1)
                this.grdPosition.Rows.Clear();

            for (int row = 0; row < dt.Rows.Count; row++)
            {
                this.grdPosition.Rows.Add();
                this.grdPosition["position_id", row].Value = dt.Rows[row]["position_id"].ToString();
                this.grdPosition["position_name", row].Value = dt.Rows[row]["position_name"].ToString();
                this.grdPosition["position_sort_order", row].Value = dt.Rows[row]["position_sort_order"].ToString();                
                this.grdPosition["comment", row].Value = dt.Rows[row]["comment"].ToString();
                this.grdPosition["reg_dt", row].Value = dt.Rows[row]["reg_dt"].ToString();
                this.grdPosition["update_dt", row].Value = dt.Rows[row]["update_dt"].ToString();
                this.grdPosition["ROWSTATE", row].Value = "";
            }

            // 데이터에 맞게 칼럼 사이즈 조정하기
            //for (int i = 0; i < grdPosition.Columns.Count; i++)
            //{
            //    grdPosition.AutoResizeColumn(i, DataGridViewAutoSizeColumnMode.AllCells);
            //}
            grdPosition.RowHeadersVisible = false;
            grdPosition.AllowUserToAddRows = false;  // 빈레코드 표시 안하기
            grdPosition.AlternatingRowsDefaultCellStyle.BackColor = Color.AliceBlue; // 그리드 행에 색 입히기
            grdPosition.CurrentCell = null;

            cb.Checked = false;
            chkSearched = true;

            return 0;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            int IdxNewRow = this.grdPosition.Rows.Add();
            this.grdPosition["reg_dt", IdxNewRow].Value = DateTime.Now;
            this.grdPosition["update_dt", IdxNewRow].Value = DateTime.Now;
            this.grdPosition["ROWSTATE", IdxNewRow].Value = "Added";
            //this.grdPosition["work_year", grdPosition.CurrentCell.RowIndex].Value = Convert.ToInt32(this.grdPosition["work_year", grdPosition.CurrentCell.RowIndex - 1].Value) + 1;            
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow dr in grdPosition.Rows) 
            {
                if (dr.Cells["colCheck"].Value != null) 
                {
                    if (dr.Cells["ROWSTATE"].Value.ToString() == "Added")// 새롭게 추가된 행은 그리드에서 바로 삭제
                    {
                        grdPosition.Rows.Remove(dr);
                        continue;
                    }

                    dr.Visible = false;
                    this.grdPosition["ROWSTATE", dr.Index].Value = "Deleted";// 기존 데이터는 그리드에서 내용만 삭제하고 저장하면 DB에서 삭제 진행.
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            int resultdt = 1;

            int chkdt = chksave();

            if (chkdt == -1 || chkdt == 1)
            {                
                return;
            }           

            RULE_JOBPOSITIONMANAGEMENT smartViceData = new RULE_JOBPOSITIONMANAGEMENT();
            resultdt = Save();

            if (resultdt == -1)
                MessageBox.Show("저장에 실패하였습니다.","Alarm");
            else if (resultdt == 1)
            {
                MessageSet.Clear();
                grdPosition.Rows.Clear();

                Search(MessageSet);
                chkSearched = false;

                MessageBox.Show("저장하였습니다.","Alarm");
            }
            else if (resultdt == 0)
                MessageBox.Show("저장할 데이터가 존재하지 않습니다.","Warning");
            else
                MessageBox.Show("중복된 항목이 존재합니다.");
            //저장실패 - cellvaluechanged 이벤트에서 기존 work_year 값과 중복되는 값을 입력시 Warning창 띄워줘!!!!!!!!!!!!!!
        }

        private int Save()
        {
            RULE_JOBPOSITIONMANAGEMENT smartViceData = new RULE_JOBPOSITIONMANAGEMENT();

            bool chksave = true;
            DataSet ds = new DataSet();

            string QueryType = string.Empty;

            if (grdPosition.Rows.Count > 0)
            {
                //현재 그리드와 조회한 후 그리드를 비교 후 insert 또는 delete 작업            
                for (int grdrow = 0; grdrow < grdPosition.Rows.Count - 1; grdrow++)
                {
                    if (this.grdPosition.Rows[grdrow].Cells["ROWSTATE"].Value.ToString() == string.Empty)
                        continue;

                    if (this.grdPosition.Rows[grdrow].Cells["ROWSTATE"].Value.ToString() == "Added" || this.grdPosition.Rows[grdrow].Cells["ROWSTATE"].Value.ToString() == "Modified")
                    {
                        MessageSet.Clear();
                        MessageSet.Add("QueryType", (this.grdPosition.Rows[grdrow].Cells["ROWSTATE"].Value.ToString() == "Added" ? "Insert" : "Update"));
                        MessageSet.Add("Position_id", this.grdPosition.Rows[grdrow].Cells["position_id"].Value.ToString());
                        MessageSet.Add("Position_name", this.grdPosition.Rows[grdrow].Cells["position_name"].Value.ToString());
                        MessageSet.Add("Position_sort_order", this.grdPosition.Rows[grdrow].Cells["position_sort_order"].Value.ToString());                        
                        if (this.grdPosition.Rows[grdrow].Cells["COMMENT"].Value == null)
                            this.grdPosition.Rows[grdrow].Cells["COMMENT"].Value = " ";
                        MessageSet.Add("COMMENT", this.grdPosition.Rows[grdrow].Cells["COMMENT"].Value.ToString());
                        MessageSet.Add("Reg_dt", this.grdPosition.Rows[grdrow].Cells["reg_dt"].Value.ToString());
                        MessageSet.Add("Update_dt", this.grdPosition.Rows[grdrow].Cells["update_dt"].Value.ToString());

                        chksave = smartViceData.Save(MessageSet);
                        
                        if (!chksave)
                        {
                            return -1;
                        }
                    }
                    else if (this.grdPosition.Rows[grdrow].Cells["ROWSTATE"].Value.ToString() == "Deleted")
                    {
                        QueryType = "Delete";
                        MessageSet.Clear();
                        MessageSet.Add("QueryType", QueryType);
                        MessageSet.Add("Position_id", this.grdPosition.Rows[grdrow].Cells["position_id"].Value.ToString());
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

        public int chksave()
        {
            for (int row = 0; row < this.grdPosition.Rows.Count - 1; row++)
            {
                if (this.grdPosition.Rows[row].Cells["position_id"].Value == null)
                {
                    MessageBox.Show(this.grdPosition.Columns["position_id"].HeaderText + " 값이 누락되었습니다.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return -1;
                }

                if (this.grdPosition.Rows[row].Cells["position_name"].Value == null)
                {
                    MessageBox.Show(this.grdPosition.Columns["position_name"].HeaderText + " 값이 누락되었습니다.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return -1;
                }

                if (this.grdPosition.Rows[row].Cells["position_sort_order"].Value == null)
                {
                    MessageBox.Show(this.grdPosition.Columns["position_sort_order"].HeaderText + " 값이 누락되었습니다.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return -1;
                }

                //새로 추가된 행 중에 그리드에 있는 work_year 값과 중복되는 데이터 입력시 Warning!
                if (this.grdPosition.Rows[row].Cells["ROWSTATE"].Value.ToString() == "Added")
                {
                    int checkdr = dtForchk.AsEnumerable().Count(dr => dr.Field<string>("position_id").ToString() == this.grdPosition.Rows[row].Cells["position_id"].Value.ToString());

                    if (checkdr > 0)
                    {
                        MessageBox.Show(this.grdPosition.Columns["position_id"].HeaderText + " 값은 중복될 수 없습니다."); // 키 중복확인
                        return 1;
                    }
                }
            }
            return 0;
        }
        private void grdPosition_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (chkSearched == true)
                return;

            //조회를 누르면 이 이벤트를 타서 실제로 조회된 데이터에서 수정을 했을 때 표시를 할 수가 없네 -_- 
            if (e.ColumnIndex == grdPosition.Columns["position_id"].Index || e.ColumnIndex == grdPosition.Columns["position_name"].Index
                || e.ColumnIndex == grdPosition.Columns["position_sort_order"].Index || e.ColumnIndex == grdPosition.Columns["COMMENT"].Index)
            {
                //기존 데이터나 수정된 기존 데이터에만 modified 표시 
                if (this.grdPosition.Rows[e.RowIndex].Cells["ROWSTATE"].Value == null || this.grdPosition.Rows[e.RowIndex].Cells["ROWSTATE"].Value.ToString() == "Modified" 
                    || this.grdPosition.Rows[e.RowIndex].Cells["ROWSTATE"].Value.ToString() == "")
                    this.grdPosition.Rows[e.RowIndex].Cells["ROWSTATE"].Value = "Modified";

                chkSearched = false;
            }
        }
        private void grdPosition_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
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
                cb.CheckedChanged += new EventHandler(grdPosition_CheckedChanged);

                this.grdPosition.Controls.Add(cb);
                //((DataGridView)sender).Controls.Add(cb);

                e.Handled = true;
            }
        }
        private void grdPosition_CheckedChanged(object sender, EventArgs e)
        {
            grdPosition.CurrentCell = null;

            if (cb.CheckState == CheckState.Checked)
                this.grdPosition.SelectAll();
            else
                grdPosition.ClearSelection();

            foreach (DataGridViewRow r in grdPosition.Rows)
            {
                if (cb.CheckState == CheckState.Checked)
                    r.Cells["colCheck"].Value = true;
                else
                    r.Cells["colCheck"].Value = false;

            }
        }

        private void JobPositionManagement_Shown(object sender, EventArgs e)
        {
            //grdPosition.ClearSelection();
            grdPosition.CurrentCell = null;
        }

        private void grdPosition_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)
                return;

            switch (grdPosition.Columns[e.ColumnIndex].Name)
            {
                case "colCheck":
                    this.grdPosition.CurrentRow.Selected = true;
                    break;
            }
        }
    }
}
