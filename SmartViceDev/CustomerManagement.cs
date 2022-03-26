using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using SmartViceDev.CubridUtils;
using System.Drawing;


namespace SmartViceDev
{
    public partial class CustomerManagement : Form
    {
        //조회조건 리스트
        Dictionary<string, string> MessageSet = new Dictionary<string, string>();
        DataTable dtForchk = new DataTable();
        bool chkSearched = true;
        CheckBox cb = new CheckBox();

        public CustomerManagement()
        {
            InitializeComponent();

            eventslist(); //이벤트 처리 함수
            grdColumnAdd(); // 그리드 컬럼 형성
        }

        private void CustomerManagement_Load(object sender, EventArgs e)
        {           
            btnSearch_Click(null, null);
            grdCustomer.RowHeadersVisible = false;
        }

        private void eventslist()
        {
            this.Load += new System.EventHandler(this.CustomerManagement_Load);
            this.Shown += new System.EventHandler(this.CustomerManagement_Shown);

            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);

            this.grdCustomer.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.grdCustomer_CellPainting);
            this.grdCustomer.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.grdCustomer_CellValueChanged);
            this.grdCustomer.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grdCustomer_CellClick);
            this.grdCustomer.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.grdCustomer_KeyPress);
            this.grdCustomer.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.grdCustomer_EditingControlShowing);

        }

        private void grdColumnAdd()
        {
            DataSet ds = new DataSet();
            RULE_CUSTOMERMANAGEMENT smartViceData = new RULE_CUSTOMERMANAGEMENT();

            var chkCol = new DataGridViewCheckBoxColumn
            {
                Name = "colCheck",
                HeaderText = ""
            };
            this.grdCustomer.Columns.Add(chkCol);
            this.grdCustomer.Columns["colCheck"].Width = 50;

            //거래처 아이디
            this.grdCustomer.Columns.Add("customer_id", "거래처 아이디");
            this.grdCustomer.Columns["customer_id"].Width = 110;
            this.grdCustomer.Columns["customer_id"].ReadOnly = true;
            //거래처 이름
            this.grdCustomer.Columns.Add("customer_name", "거래처 이름");
            //거래처 전화
            this.grdCustomer.Columns.Add("customer_tel", "거래처 전화");
            this.grdCustomer.Columns["customer_tel"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            //거래처 팩스
            this.grdCustomer.Columns.Add("customer_fax", "거래처 팩스");
            this.grdCustomer.Columns["customer_fax"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            //사업자 번호
            this.grdCustomer.Columns.Add("business_number", "사업자 번호");
            this.grdCustomer.Columns["business_number"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            //우편번호검색
            DataGridViewButtonColumn btnSearchZipNumCol = new DataGridViewButtonColumn();
            btnSearchZipNumCol.Name = "btnGetzipnumCol";
            btnSearchZipNumCol.HeaderText = "";
            btnSearchZipNumCol.Text = "주소검색";
            this.grdCustomer.Columns.Add(btnSearchZipNumCol);

            //거래처 주소
            this.grdCustomer.Columns.Add("customer_address", "거래처 주소");
            this.grdCustomer.Columns["customer_address"].Width = 300;
            this.grdCustomer.Columns["customer_address"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            //거래처 직원이름
            this.grdCustomer.Columns.Add("customer_staff_name", "직원이름");
            this.grdCustomer.Columns["customer_staff_name"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            //거래처 직원전화
            this.grdCustomer.Columns.Add("customer_staff_tel", "직원전화");
            this.grdCustomer.Columns["customer_staff_tel"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            //거래처 직원 이메일
            this.grdCustomer.Columns.Add("customer_staff_email", "직원 이메일");
            this.grdCustomer.Columns["customer_staff_email"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            //거래처 홈페이지
            this.grdCustomer.Columns.Add("customer_homepage", "거래처 홈페이지");
            this.grdCustomer.Columns["customer_homepage"].Width = 150;
            this.grdCustomer.Columns["customer_homepage"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            //설명
            this.grdCustomer.Columns.Add("comment", "설명");
            this.grdCustomer.Columns["comment"].Width = 200;
            //등록 날짜
            this.grdCustomer.Columns.Add("reg_dt", "등록 날짜");
            this.grdCustomer.Columns["reg_dt"].ReadOnly = true;
            this.grdCustomer.Columns["reg_dt"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //수정 날짜
            this.grdCustomer.Columns.Add("update_dt", "수정 날짜");
            this.grdCustomer.Columns["update_dt"].ReadOnly = true;
            this.grdCustomer.Columns["update_dt"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //ROWSTATE
            this.grdCustomer.Columns.Add("ROWSTATE", "ROWSTATE");
            this.grdCustomer.Columns["ROWSTATE"].Visible = false;
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
                        this.grdCustomer.Rows.Clear();
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
                this.grdCustomer.Rows.Clear();
                chkSearched = false;
                return;
            }

        }

        public int Chksearch()
        {
            if (chkSearched == false) // 조회한 이력이 있을때만 그리드 체크 
            {

                for (int grdrow = 0; grdrow < this.grdCustomer.Rows.Count; grdrow++)
                {
                    if (this.grdCustomer.Rows[grdrow].Cells["ROWSTATE"].Value != null)
                    {
                        if (this.grdCustomer.Rows[grdrow].Cells["ROWSTATE"].Value.ToString() == "Modified" || this.grdCustomer.Rows[grdrow].Cells["ROWSTATE"].Value.ToString() == "Added"
                            || this.grdCustomer.Rows[grdrow].Cells["ROWSTATE"].Value.ToString() == "Deleted")
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

            if (this.grdCustomer.Rows.Count > 1)
            {
                this.grdCustomer.Rows.Clear();
            }

            RULE_CUSTOMERMANAGEMENT smartViceData = new RULE_CUSTOMERMANAGEMENT();

            DataSet ds = new DataSet();
            MessageSet.Clear();
            MessageSet.Add("customer_name", tbCustomerNm.Text);
            MessageSet.Add("customer_staff_name", tbStaffNm.Text);
            MessageSet.Add("SearchType", "MainSearch");
            ds = smartViceData.Search(MessageSet);

            DataTable dt = ds.Tables[0].Copy();

            if (dt.Rows.Count < 1)
                return -1;

            dtForchk = ds.Tables[0].Copy();

            for (int row = 0; row < dt.Rows.Count; row++)
            {
                this.grdCustomer.Rows.Add();
                this.grdCustomer["customer_id", row].Value = dt.Rows[row]["customer_id"].ToString();
                this.grdCustomer["customer_name", row].Value = dt.Rows[row]["customer_name"].ToString();
                this.grdCustomer["customer_tel", row].Value = dt.Rows[row]["customer_tel"].ToString();
                this.grdCustomer["customer_fax", row].Value = dt.Rows[row]["customer_fax"].ToString();
                this.grdCustomer["business_number", row].Value = dt.Rows[row]["business_number"].ToString();
                this.grdCustomer["btnGetzipnumCol", row].Value = "주소 검색";
                this.grdCustomer["customer_address", row].Value = dt.Rows[row]["customer_address"].ToString();
                this.grdCustomer["customer_staff_name", row].Value = dt.Rows[row]["customer_staff_name"].ToString();
                this.grdCustomer["customer_staff_tel", row].Value = dt.Rows[row]["customer_staff_tel"].ToString();
                this.grdCustomer["customer_staff_email", row].Value = dt.Rows[row]["customer_staff_email"].ToString();
                this.grdCustomer["customer_homepage", row].Value = dt.Rows[row]["customer_homepage"].ToString();
                this.grdCustomer["comment", row].Value = dt.Rows[row]["comment"].ToString();
                this.grdCustomer["reg_dt", row].Value = dt.Rows[row]["reg_dt"].ToString();
                this.grdCustomer["update_dt", row].Value = dt.Rows[row]["update_dt"].ToString();
                this.grdCustomer["ROWSTATE", row].Value = "";
            }

            // 데이터에 맞게 칼럼 사이즈 조정하기
            //for (int i = 0; i < grdDepart.Columns.Count; i++)
            //{
            //    grdDepart.AutoResizeColumn(i, DataGridViewAutoSizeColumnMode.AllCells);
            //}
            this.grdCustomer.RowHeadersVisible = false;
            this.grdCustomer.AllowUserToAddRows = false;  // 빈레코드 표시 안하기
            this.grdCustomer.AlternatingRowsDefaultCellStyle.BackColor = Color.AliceBlue; // 그리드 행에 색 입히기
            this.grdCustomer.CurrentCell = null;

            cb.Checked = false;
            chkSearched = false;

            return 0;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            int IdxNewRow = this.grdCustomer.Rows.Add();

            RULE_CUSTOMERMANAGEMENT smartViceData = new RULE_CUSTOMERMANAGEMENT();
            MessageSet.Clear();
            MessageSet.Add("SearchType", "GetCustomerId");
            DataSet ds = smartViceData.Search(MessageSet);

            if (ds.Tables[0].Rows.Count > 0)
                this.grdCustomer["customer_id", IdxNewRow].Value = "JWP03_" + ds.Tables[0].Rows[0].Field<decimal>("new_id").ToString();
            this.grdCustomer["btnGetzipnumCol", IdxNewRow].Value = "주소 검색";
            this.grdCustomer["reg_dt", IdxNewRow].Value = DateTime.Now;
            this.grdCustomer["update_dt", IdxNewRow].Value = DateTime.Now;
            this.grdCustomer["ROWSTATE", IdxNewRow].Value = "Added";
            //this.grdDepart["work_year", grdDepart.CurrentCell.RowIndex].Value = Convert.ToInt32(this.grdDepart["work_year", grdDepart.CurrentCell.RowIndex - 1].Value) + 1;            
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow dr in grdCustomer.Rows)
            {
                if (dr.Cells["colCheck"].Value != null && dr.Cells["colCheck"].Value.ToString().ToUpper() != "false".ToUpper())
                {
                    if (dr.Cells["ROWSTATE"].Value.ToString() == "Added")// 새롭게 추가된 행은 그리드에서 바로 삭제
                    {
                        grdCustomer.Rows.Remove(dr);
                        continue;
                    }

                    dr.Visible = false;
                    this.grdCustomer["ROWSTATE", dr.Index].Value = "Deleted";// 기존 데이터는 그리드에서 내용만 삭제하고 저장하면 DB에서 삭제 진행.
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
            
            RULE_CUSTOMERMANAGEMENT smartViceData = new RULE_CUSTOMERMANAGEMENT();
            resultdt = Save();

            if (resultdt == -1)
                MessageBox.Show("저장에 실패하였습니다.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else if (resultdt == 1)
            {
                MessageSet.Clear();
                grdCustomer.Rows.Clear();

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
            RULE_CUSTOMERMANAGEMENT smartViceData = new RULE_CUSTOMERMANAGEMENT();

            bool chksave = true;
            DataSet ds = new DataSet();

            string QueryType = string.Empty;

            if (grdCustomer.Rows.Count > 0)
            {
                //현재 그리드와 조회한 후 그리드를 비교 후 insert 또는 delete 작업            
                for (int grdrow = 0; grdrow < grdCustomer.Rows.Count; grdrow++)
                {
                    if (this.grdCustomer.Rows[grdrow].Cells["ROWSTATE"].Value.ToString() == string.Empty)
                        continue;

                    if (this.grdCustomer.Rows[grdrow].Cells["ROWSTATE"].Value.ToString() == "Added" || this.grdCustomer.Rows[grdrow].Cells["ROWSTATE"].Value.ToString() == "Modified")
                    {
                        MessageSet.Clear();
                        MessageSet.Add("QueryType", (this.grdCustomer.Rows[grdrow].Cells["ROWSTATE"].Value.ToString() == "Added" ? "Insert" : "Update"));
                        MessageSet.Add("customer_id", this.grdCustomer.Rows[grdrow].Cells["customer_id"].Value.ToString());                        
                        MessageSet.Add("customer_name", this.grdCustomer.Rows[grdrow].Cells["customer_name"].Value.ToString());
                        if (this.grdCustomer.Rows[grdrow].Cells["customer_tel"].Value != null && this.grdCustomer.Rows[grdrow].Cells["customer_tel"].Value.ToString() != string.Empty)
                            MessageSet.Add("customer_tel", this.grdCustomer.Rows[grdrow].Cells["customer_tel"].Value.ToString());
                        if (this.grdCustomer.Rows[grdrow].Cells["customer_fax"].Value != null && this.grdCustomer.Rows[grdrow].Cells["customer_fax"].Value.ToString() != string.Empty)
                            MessageSet.Add("customer_fax", this.grdCustomer.Rows[grdrow].Cells["customer_fax"].Value.ToString());
                        if (this.grdCustomer.Rows[grdrow].Cells["business_number"].Value != null && this.grdCustomer.Rows[grdrow].Cells["business_number"].Value.ToString() != string.Empty)
                            MessageSet.Add("business_number", this.grdCustomer.Rows[grdrow].Cells["business_number"].Value.ToString());
                        if (this.grdCustomer.Rows[grdrow].Cells["customer_address"].Value != null && this.grdCustomer.Rows[grdrow].Cells["customer_address"].Value.ToString() != string.Empty)
                            MessageSet.Add("customer_address", this.grdCustomer.Rows[grdrow].Cells["customer_address"].Value.ToString());
                        if (this.grdCustomer.Rows[grdrow].Cells["customer_staff_name"].Value != null && this.grdCustomer.Rows[grdrow].Cells["customer_staff_name"].Value.ToString() != string.Empty)
                            MessageSet.Add("customer_staff_name", this.grdCustomer.Rows[grdrow].Cells["customer_staff_name"].Value.ToString());
                        if (this.grdCustomer.Rows[grdrow].Cells["customer_staff_tel"].Value != null && this.grdCustomer.Rows[grdrow].Cells["customer_staff_tel"].Value.ToString() != string.Empty)
                            MessageSet.Add("customer_staff_tel", this.grdCustomer.Rows[grdrow].Cells["customer_staff_tel"].Value.ToString());
                        if (this.grdCustomer.Rows[grdrow].Cells["customer_staff_email"].Value != null && this.grdCustomer.Rows[grdrow].Cells["customer_staff_email"].Value.ToString() != string.Empty)
                            MessageSet.Add("customer_staff_email", this.grdCustomer.Rows[grdrow].Cells["customer_staff_email"].Value.ToString());
                        if (this.grdCustomer.Rows[grdrow].Cells["customer_homepage"].Value != null && this.grdCustomer.Rows[grdrow].Cells["customer_homepage"].Value.ToString() != string.Empty)
                            MessageSet.Add("customer_homepage", this.grdCustomer.Rows[grdrow].Cells["customer_homepage"].Value.ToString());                                                
                        if (this.grdCustomer.Rows[grdrow].Cells["COMMENT"].Value != null && this.grdCustomer.Rows[grdrow].Cells["COMMENT"].Value.ToString() != string.Empty)
                            MessageSet.Add("COMMENT", this.grdCustomer.Rows[grdrow].Cells["COMMENT"].Value.ToString());

                        chksave = smartViceData.Save(MessageSet);

                        if (!chksave)
                        {
                            return -1;
                        }
                    }
                    else if (this.grdCustomer.Rows[grdrow].Cells["ROWSTATE"].Value.ToString() == "Deleted")
                    {
                        QueryType = "Delete";
                        MessageSet.Clear();
                        MessageSet.Add("QueryType", QueryType);
                        MessageSet.Add("customer_id", this.grdCustomer.Rows[grdrow].Cells["customer_id"].Value.ToString());
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
            for (int row = 0; row < this.grdCustomer.Rows.Count; row++)
            {
                if (this.grdCustomer.Rows[row].Cells["ROWSTATE"].Value.ToString() == "Added")
                {
                    RULE_CUSTOMERMANAGEMENT smartViceData = new RULE_CUSTOMERMANAGEMENT();
                    MessageSet.Clear();
                    MessageSet.Add("SearchType", "MainSearch");
                    MessageSet.Add("customer_id", this.grdCustomer.Rows[row].Cells["customer_id"].Value.ToString());
                    DataSet ds = smartViceData.Search(MessageSet);

                    //거래처 id 중복 확인
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        MessageBox.Show(this.grdCustomer.Columns["customer_id"].HeaderText + " 값이 이미 존재합니다.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return -1;
                    }
                }

                if (this.grdCustomer.Rows[row].Cells["customer_id"].Value == null)
                {
                    MessageBox.Show(this.grdCustomer.Columns["customer_id"].HeaderText + " 값이 누락되었습니다.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return -1;
                }

                if (this.grdCustomer.Rows[row].Cells["customer_name"].Value == null)
                {
                    MessageBox.Show(this.grdCustomer.Columns["customer_name"].HeaderText + " 값이 누락되었습니다.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return -1;
                }

                //if (this.grdCustomer.Rows[row].Cells["depart_sort_order"].Value == null)
                //{
                //    MessageBox.Show(this.grdCustomer.Columns["depart_sort_order"].HeaderText + " 값이 누락되었습니다.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //    return -1;
                //}               
            }
            return 0;
        }
        private void grdCustomer_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (chkSearched == true)
                return;

            //if (e.ColumnIndex == grdCustomer.Columns["customer_id"].Index || e.ColumnIndex == grdCustomer.Columns["customer_name"].Index
            //    || e.ColumnIndex == grdCustomer.Columns["customer_tel"].Index || e.ColumnIndex == grdCustomer.Columns["COMMENT"].Index)
            //{
            //기존 데이터나 수정된 기존 데이터에만 modified 표시 
            if (this.grdCustomer.Rows[e.RowIndex].Cells["ROWSTATE"].Value == null || this.grdCustomer.Rows[e.RowIndex].Cells["ROWSTATE"].Value.ToString() == "Modified"
                || this.grdCustomer.Rows[e.RowIndex].Cells["ROWSTATE"].Value.ToString() == "")
                this.grdCustomer.Rows[e.RowIndex].Cells["ROWSTATE"].Value = "Modified";

            chkSearched = false;
            //}

        }

        private void grdCustomer_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)
                return;

            switch (grdCustomer.Columns[e.ColumnIndex].Name)
            {
                case "colCheck":                    
                        this.grdCustomer.CurrentRow.Selected = true;           
                    break;
                case "btnGetzipnumCol":
                    GetZipNAddressPopup getZipNAddressPopup = new GetZipNAddressPopup();
                    getZipNAddressPopup.StartPosition = FormStartPosition.CenterParent;

                    getZipNAddressPopup.ShowDialog();

                    if (getZipNAddressPopup.FullAddr != null)
                    {
                        this.grdCustomer.Rows[e.RowIndex].Cells[e.ColumnIndex + 1].Value = getZipNAddressPopup.FullAddr.Replace('/','\\');                        
                    }
                    break;
            }
        }

        private void grdCustomer_KeyPress(object sender, KeyPressEventArgs e)
        {
            //숫자가 아닌 값이거나 백스페이스를 제외한 나머지를 바로 처리
            if (!(char.IsDigit(e.KeyChar) || e.KeyChar == Convert.ToChar(Keys.Back)))
            {
                e.Handled = true; //Key Press 를 처리했으니 더이상 작동하지 말라는 것
            }
        }

        private void grdCustomer_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            //세금관련 컬럼에 숫자 이 외의 값을 받을 수 없도록 하는 이벤트 동작             
            string name = grdCustomer.CurrentCell.OwningColumn.Name;

            if (name != "customer_id" && name != "customer_name" && name != "customer_address" && name != "customer_staff_name" && name != "customer_staff_email"
                && name != "customer_homepage" && name != "comment")
            {
                e.Control.KeyPress += new KeyPressEventHandler(grdCustomer_KeyPress);
            }
            else
            {
                e.Control.KeyPress -= new KeyPressEventHandler(grdCustomer_KeyPress);
            }
        }

        private void btnSendMail_Click(object sender, EventArgs e)
        {
            int cnt_chkrow = 0;
            List<string> selectedEmailAddrs = new List<string>();

            foreach (DataGridViewRow dr in grdCustomer.Rows)
            {
                if (dr.Cells["colCheck"].Value != null && dr.Cells["colCheck"].Value.ToString().ToUpper() != "false".ToUpper())
                {
                    if (dr.Cells["customer_staff_email"].Value == null || dr.Cells["customer_staff_email"].Value.ToString() == string.Empty)
                    {
                        MessageBox.Show("\"" + dr.Cells["customer_name"].Value.ToString()+"\" 거래처의 이메일이 없습니다." , "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    selectedEmailAddrs.Add(dr.Cells["customer_staff_email"].Value.ToString());
                    cnt_chkrow ++;
                }
            }
            if (cnt_chkrow == 0)
            {
                MessageBox.Show("메일을 발송할 거래처를 선택하여 주십시오.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            SendMailPopup sendMailPopup = new SendMailPopup();
            sendMailPopup.RecipientType = "거래처";
            sendMailPopup.lst_emailAddr = selectedEmailAddrs;
            sendMailPopup.ShowDialog();
        }

        private void grdCustomer_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
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
                cb.CheckedChanged += new EventHandler(grdCustomer_CheckedChanged);

                this.grdCustomer.Controls.Add(cb);
                //((DataGridView)sender).Controls.Add(cb);

                e.Handled = true;
            }
        }
        private void grdCustomer_CheckedChanged(object sender, EventArgs e)
        {
            grdCustomer.CurrentCell = null;
            
            if (cb.CheckState == CheckState.Checked)
                this.grdCustomer.SelectAll();
            else
                grdCustomer.ClearSelection();

            foreach (DataGridViewRow r in grdCustomer.Rows)
            {
                if (cb.CheckState == CheckState.Checked)
                    r.Cells["colCheck"].Value = true;
                else
                    r.Cells["colCheck"].Value = false;

            }
        }

        private void CustomerManagement_Shown(object sender, EventArgs e)
        {
            //grdCustomer.ClearSelection();
            grdCustomer.CurrentCell = null;
        }
    }
}
