using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using SmartViceDev.CubridUtils;
using System.Drawing;
using System.Drawing.Printing;
using WIA;
using System.IO;
using System.IO.Compression;


namespace SmartViceDev
{
    public partial class SpendingManagement : Form
    {
        RULE_SPENDINGMANAGEMENT smartViceData = new RULE_SPENDINGMANAGEMENT();

        Dictionary<string, string> MessageSet = new Dictionary<string, string>();
        DataTable dtForchk = new DataTable();
        bool chkSearched = true;
        bool chkMakeZip = false;
        DateTimePicker Spenddate_dtp = new DateTimePicker();
        DateTimePicker Give_dtp = new DateTimePicker();
        Rectangle _dtpRectangle = new Rectangle();

        string zipfile_path = string.Empty;
        CheckBox cb = new CheckBox();

        //* 인쇄 관련 변수 설정
        bool bFirstPage;
        bool bNewPage;
        double iTotalWidth;
        int iHeaderHeight;
        int iCellHeight;
        List<int> arrColumnWidths = new List<int>();
        List<int> arrColumnLefts = new List<int>();
        StringFormat strFormat;
        int iCount;

        public SpendingManagement()
        {
            InitializeComponent();

            eventslist(); //이벤트 처리 함수
            grdColumnAdd(); // 그리드 컬럼 형성
        }

        private void SpendingManagement_Load(object sender, EventArgs e)
        {

            this.StartDate.Value = Convert.ToDateTime((DateTime.Today.Year.ToString() + "-" + DateTime.Today.Month.ToString().PadLeft(2, '0') + "-" + "01"));
            this.EndDate.Value = Convert.ToDateTime((DateTime.Today.Year.ToString() + "-" + DateTime.Today.Month.ToString().PadLeft(2, '0') + "-" + DateTime.DaysInMonth(DateTime.Today.Year, DateTime.Today.Month).ToString().PadLeft(2, '0')));

            //그리드내 특정 셀에 캘린더 추가 
            grdReceipt.Controls.Add(Spenddate_dtp);
            Spenddate_dtp.Visible = false;
            Spenddate_dtp.Format = DateTimePickerFormat.Custom;
            Spenddate_dtp.TextChanged += new EventHandler(Spenddate_dtp_TextChange);

            grdReceipt.Controls.Add(Give_dtp);
            Give_dtp.Visible = false;
            Give_dtp.Format = DateTimePickerFormat.Custom;
            Give_dtp.TextChanged += new EventHandler(Give_dtp_TextChange);

            btnSearch_Click(null, null);
        }

        private void eventslist()
        {
            this.Shown += new System.EventHandler(this.SpendingManagement_Shown);

            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            this.btnExcelExport.Click += new System.EventHandler(this.btnExcelExport_Click);
            this.btnSendMail.Click += new System.EventHandler(this.btnSendMail_Click);
            this.btnMakeZip.Click += new System.EventHandler(this.btnMakeZip_Click);

            this.grdReceipt.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.grdReceipt_CellPainting);
            this.grdReceipt.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grdReceipt_CellClick);
            this.grdReceipt.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grdReceipt_CellDoubleClick);
            this.grdReceipt.Scroll += new System.Windows.Forms.ScrollEventHandler(this.grdReceipt_Scroll);
            this.grdReceipt.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.grdReceipt_CellValueChanged);
            // 그리드에 있는 콤보박스 형태의 열 값이 변경될 때 발생하는 이벤트
            this.grdReceipt.CurrentCellDirtyStateChanged += new System.EventHandler(this.grdReceipt_CurrentCellDirtyStateChanged);

            this.printDocument1.BeginPrint += new System.Drawing.Printing.PrintEventHandler(this.printDocument1_BeginPrint);
            this.printDocument1.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.printDocument1_PrintPage);

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
            this.grdReceipt.Columns.Add(chkCol);
            this.grdReceipt.Columns["colCheck"].Width = 50;

            //유효 아이디
            this.grdReceipt.Columns.Add("valid_id", "유효 아이디");
            this.grdReceipt.Columns["valid_id"].Visible = false;

            //지출 일자
            this.grdReceipt.Columns.Add("spend_date", "지출 일자");
            this.grdReceipt.Columns["spend_date"].Width = 150;

            //지출자 아이디            
            dt = smartViceData.SearchCommon("searchStaffInfo", MessageSet);

            //지출자 명
            DataGridViewComboBoxColumn cmbStaffNm = new DataGridViewComboBoxColumn();
            cmbStaffNm.Name = "cmbStaffNm";
            cmbStaffNm.HeaderText = "지출자 명";
            for (int row = 0; row < dt.Rows.Count; row++)
            {
                cmbStaffNm.Items.Add(dt.Rows[row]["staff_name"]);
            }
            //cmbStaffNm.Items.Add("cmbStaffNm");
            this.grdReceipt.Columns.Add(cmbStaffNm);

            //지출자 ID
            DataGridViewComboBoxColumn cmbStaffId = new DataGridViewComboBoxColumn();
            cmbStaffId.Name = "cmbStaffId";
            cmbStaffId.HeaderText = "지출자 ID";
            cmbStaffId.Width = 150;
            for (int row = 0; row < dt.Rows.Count; row++)
            {
                cmbStaffId.Items.Add(dt.Rows[row]["staff_id"]);
            }
            //cmbStaffId.Items.Add("cmbStaffId");
            this.grdReceipt.Columns.Add(cmbStaffId);

            //지출 구분 코드
            MessageSet.Clear();
            MessageSet.Add("Code_Type", "SPEND");
            dt = smartViceData.SearchCommon("searchSpendType", MessageSet);
            DataGridViewComboBoxColumn cmbSpendTypeCd = new DataGridViewComboBoxColumn();
            cmbSpendTypeCd.Name = "cmbSpendTypeCd";
            cmbSpendTypeCd.HeaderText = "지출 구분 코드";
            for (int row = 0; row < dt.Rows.Count; row++)
            {
                cmbSpendTypeCd.Items.Add(dt.Rows[row]["type_code"]);
            }
            this.grdReceipt.Columns.Add(cmbSpendTypeCd);
            this.grdReceipt.Columns["cmbSpendTypeCd"].Visible = false;

            //지출 구분 명
            DataGridViewComboBoxColumn cmbSpendTypeNm = new DataGridViewComboBoxColumn();
            cmbSpendTypeNm.Name = "cmbSpendTypeNm";
            cmbSpendTypeNm.HeaderText = "지출 구분 명";
            for (int row = 0; row < dt.Rows.Count; row++)
            {
                cmbSpendTypeNm.Items.Add(dt.Rows[row]["code_name"]);
            }
            this.grdReceipt.Columns.Add(cmbSpendTypeNm);

            //지출 내용
            this.grdReceipt.Columns.Add("spend_content", "지출 내용");
            this.grdReceipt.Columns["spend_content"].Width = 200;

            //지출 금액
            this.grdReceipt.Columns.Add("spend_cost", "지출 금액");
            this.grdReceipt.Columns["spend_cost"].Width = 150;
            this.grdReceipt.Columns["spend_cost"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            //비용처리 
            DataGridViewComboBoxColumn cmbExpense_yn = new DataGridViewComboBoxColumn();
            cmbExpense_yn.Name = "cmbExpense_yn";
            cmbExpense_yn.HeaderText = "비용 처리";
            cmbExpense_yn.ValueType = typeof(string);
            cmbExpense_yn.Items.Add("비용");
            cmbExpense_yn.Items.Add("계산서");
            //cmbStaffId.Items.Add("cmbStaffId");
            this.grdReceipt.Columns.Add(cmbExpense_yn);

            //지급여부
            DataGridViewComboBoxColumn cmbGive_yn = new DataGridViewComboBoxColumn();
            cmbGive_yn.Name = "cmbGive_yn";
            cmbGive_yn.HeaderText = "지급 여부";
            cmbGive_yn.ValueType = typeof(string);
            cmbGive_yn.Items.Add("O");
            cmbGive_yn.Items.Add("X");
            this.grdReceipt.Columns.Add(cmbGive_yn);
            this.grdReceipt.Columns["cmbGive_yn"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            //입금 날짜
            this.grdReceipt.Columns.Add("deposit_date", "입금 날짜");
            this.grdReceipt.Columns["deposit_date"].Width = 200;

            //이미지 제목
            DataGridViewLinkColumn receipt_photo = new DataGridViewLinkColumn();
            receipt_photo.Name = "receipt_photo";
            receipt_photo.HeaderText = "영수증 사진";
            receipt_photo.DataPropertyName = "receipt_photo";
            this.grdReceipt.Columns.Add(receipt_photo);
            this.grdReceipt.Columns["receipt_photo"].Width = 350;

            //this.grdReceipt.Columns.Add("receipt_photo", "영수증 사진");
            //this.grdReceipt.Columns["receipt_photo"].ReadOnly = true;            
            //this.grdReceipt.Columns["receipt_photo"].Width = 350;

            //이미지 스캔버튼
            DataGridViewButtonColumn btnGetImgcol = new DataGridViewButtonColumn();
            btnGetImgcol.Name = "btnGetImgcol";
            btnGetImgcol.HeaderText = "";
            btnGetImgcol.Text = "스캔하기";
            this.grdReceipt.Columns.Add(btnGetImgcol);
            //this.grdReceipt.Columns["PreviewImg"].Visible = true;

            //이미지 미리보기
            DataGridViewImageColumn PreviewImg = new DataGridViewImageColumn();
            PreviewImg.Name = "PreviewImg";
            PreviewImg.HeaderText = "이미지 미리보기";
            this.grdReceipt.Columns.Add(PreviewImg);
            this.grdReceipt.Columns["PreviewImg"].Visible = false;

            //이미지 파일 전체경로
            this.grdReceipt.Columns.Add("receiptImg_Path", "영수증 사진 경로");
            //this.grdReceipt.Columns["receipt_photo"].ReadOnly = true;            
            this.grdReceipt.Columns["receiptImg_Path"].Width = 400;
            this.grdReceipt.Columns["receiptImg_Path"].Visible = false;

            //ROWSTATE
            this.grdReceipt.Columns.Add("RowState", "RowState");
            this.grdReceipt.Columns["RowState"].Visible = false; // 행의 신규,기존,수정,삭제 등의 상태를 나타냄.


            this.grdReceipt.AllowUserToAddRows = false;
            this.grdReceipt.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

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

            MessageSet.Clear();
            MessageSet.Add("StartDate", this.StartDate.Value.ToShortDateString());
            MessageSet.Add("EndDate", this.EndDate.Value.ToShortDateString());

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
                        this.grdReceipt.Rows.Clear();
                        chkSearched = false;
                        MessageSet.Clear();
                        return;
                    }
                }
                else if (dialogResult == DialogResult.Cancel || dialogResult == DialogResult.No)
                {
                    MessageSet.Clear();
                    return;
                }
            }

            int resultchk2 = Search(MessageSet);

            if (resultchk2 == -1)
            {
                MessageBox.Show("조회된 데이터가 없습니다.", "Inform", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.grdReceipt.Rows.Clear();
                chkSearched = false;
                MessageSet.Clear();
                return;
            }
            MessageSet.Clear();
        }

        public int Chksearch()
        {
            if (chkSearched == false) // 조회한 이력이 있을때만 그리드 체크 
            {

                for (int grdrow = 0; grdrow < this.grdReceipt.Rows.Count; grdrow++)
                {
                    if (this.grdReceipt.Rows[grdrow].Cells["ROWSTATE"].Value != null)
                    {
                        if (this.grdReceipt.Rows[grdrow].Cells["ROWSTATE"].Value.ToString() == "Modified" || this.grdReceipt.Rows[grdrow].Cells["ROWSTATE"].Value.ToString() == "Added"
                            || this.grdReceipt.Rows[grdrow].Cells["ROWSTATE"].Value.ToString() == "Deleted")
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

            if (this.grdReceipt.Rows.Count > 0)
            {
                this.grdReceipt.Rows.Clear();
            }

            DataSet ds = new DataSet();
            ds = smartViceData.Search(MessageSet);

            DataTable dt = ds.Tables[0].Copy();

            if (dt.Rows.Count < 1)
                return -1;

            dtForchk = ds.Tables[0].Copy();

            if (this.grdReceipt.Rows.Count > 1)
                this.grdReceipt.Rows.Clear();

            for (int row = 0; row < dt.Rows.Count; row++)
            {
                MessageSet.Clear();
                MessageSet.Add("valid_id", dt.Rows[row]["valid_id"].ToString());
                Image staffimg = smartViceData.GetReceiptPhoto(MessageSet);

                this.grdReceipt.Rows.Add();
                this.grdReceipt["valid_id", row].Value = dt.Rows[row]["valid_id"].ToString();
                this.grdReceipt["spend_date", row].Value = dt.Rows[row]["spend_date"].ToString();
                this.grdReceipt["cmbSpendTypeCd", row].Value = dt.Rows[row]["spend_type_code"].ToString();
                this.grdReceipt["cmbSpendTypeNm", row].Value = dt.Rows[row]["spend_type_name"].ToString();
                this.grdReceipt["spend_content", row].Value = dt.Rows[row]["spend_content"].ToString();
                this.grdReceipt["cmbStaffId", row].Value = dt.Rows[row]["staff_id"].ToString();
                this.grdReceipt["cmbStaffNm", row].Value = dt.Rows[row]["staff_name"].ToString();
                this.grdReceipt["spend_cost", row].Value = dt.Rows[row]["spend_cost"].ToString();
                if (dt.Rows[row]["expense_yn"].ToString().ToUpper() == "Y")
                    this.grdReceipt["cmbExpense_yn", row].Value = "계산서";
                else if (dt.Rows[row]["expense_yn"].ToString().ToUpper() == "N")
                    this.grdReceipt["cmbExpense_yn", row].Value = "비용";

                this.grdReceipt["cmbGive_yn", row].Value = dt.Rows[row]["give_yn"].ToString();
                if(dt.Rows[row]["deposit_dt"] != DBNull.Value)
                    this.grdReceipt["deposit_date", row].Value = dt.Rows[row]["deposit_dt"].ToString();
                                
                if (staffimg != null)
                {
                    this.grdReceipt["receipt_photo", row].Value = DateTime.Today.Year + "_영수증\\" + dt.Rows[row]["spend_date"].ToString().Replace("-", "").Substring(0, 6) + "\\" +
                                                                dt.Rows[row]["spend_date"].ToString().Replace("-", "") + "_" + dt.Rows[row]["staff_name"].ToString() + "_" +
                                                                dt.Rows[row]["spend_content"].ToString() + ".jpg";
                    this.grdReceipt["PreviewImg", row].Value = staffimg;

                }
                this.grdReceipt["btnGetImgcol", row].Value = "이미지 등록";
                this.grdReceipt["ROWSTATE", row].Value = "";
            }

            // 데이터에 맞게 칼럼 사이즈 조정하기
            //for (int i = 0; i < grdDepart.Columns.Count; i++)
            //{
            //    grdDepart.AutoResizeColumn(i, DataGridViewAutoSizeColumnMode.AllCells);
            //}
            grdReceipt.RowHeadersVisible = false;
            grdReceipt.AllowUserToAddRows = false;  // 빈레코드 표시 안하기
            grdReceipt.AlternatingRowsDefaultCellStyle.BackColor = Color.AliceBlue; // 그리드 행에 색 입히기
            grdReceipt.CurrentCell = null;

            cb.Checked = false;
            chkSearched = false;

            return 0;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Guid new_guid = Guid.NewGuid();

            int IdxNewRow = this.grdReceipt.Rows.Add();
            this.grdReceipt["valid_id", IdxNewRow].Value = new_guid.ToString();
            this.grdReceipt["spend_date", IdxNewRow].Value = DateTime.Today.ToShortDateString();
            this.grdReceipt["btnGetImgcol", IdxNewRow].Value = "이미지 등록"; ;
            //this.grdReceipt["cmbStaffNm", IdxNewRow].Value = "이은주";
            //this.grdReceipt["cmbStaffId", IdxNewRow].Value = "JWP001_J";            
            this.grdReceipt["ROWSTATE", IdxNewRow].Value = "Added";
            //this.grdDepart["work_year", grdDepart.CurrentCell.RowIndex].Value = Convert.ToInt32(this.grdDepart["work_year", grdDepart.CurrentCell.RowIndex - 1].Value) + 1;            
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow dr in grdReceipt.Rows)
            {
                if (dr.Cells["colCheck"].Value != null && dr.Cells["colCheck"].Value.ToString().ToUpper() != "false".ToUpper())
                {
                    if (dr.Cells["ROWSTATE"].Value.ToString() == "Added")// 새롭게 추가된 행은 그리드에서 바로 삭제
                    {
                        grdReceipt.Rows.Remove(dr);
                        continue;
                    }

                    dr.Visible = false;
                    this.grdReceipt["ROWSTATE", dr.Index].Value = "Deleted";// 기존 데이터는 그리드에서 내용만 삭제하고 저장하면 DB에서 삭제 진행.
                }
            }
            MessageBox.Show("삭제되었습니다.", "Inform", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                MessageSet.Clear();
                grdReceipt.Rows.Clear();

                MessageSet.Add("StartDate", this.StartDate.Value.ToShortDateString());
                MessageSet.Add("EndDate", this.EndDate.Value.ToShortDateString());
                
                chkSearched = true;

                Search(MessageSet);

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

            if (grdReceipt.Rows.Count > 0)
            {
                //현재 그리드와 조회한 후 그리드를 비교 후 insert 또는 delete 작업            
                for (int grdrow = 0; grdrow < grdReceipt.Rows.Count; grdrow++)
                {
                    if (this.grdReceipt.Rows[grdrow].Cells["ROWSTATE"].Value.ToString() == string.Empty)
                        continue;

                    if (this.grdReceipt.Rows[grdrow].Cells["ROWSTATE"].Value.ToString() == "Added" || this.grdReceipt.Rows[grdrow].Cells["ROWSTATE"].Value.ToString() == "Modified")
                    {
                        MessageSet.Clear();
                        MessageSet.Add("QueryType", (this.grdReceipt.Rows[grdrow].Cells["ROWSTATE"].Value.ToString() == "Added" ? "Insert" : "Update"));
                        MessageSet.Add("valid_id", this.grdReceipt.Rows[grdrow].Cells["valid_id"].Value.ToString());
                        MessageSet.Add("spend_date", this.grdReceipt.Rows[grdrow].Cells["spend_date"].Value.ToString());
                        MessageSet.Add("spend_type_code", this.grdReceipt.Rows[grdrow].Cells["cmbSpendTypeCd"].Value.ToString());
                        MessageSet.Add("spend_type_name", this.grdReceipt.Rows[grdrow].Cells["cmbSpendTypeNm"].Value.ToString());
                        MessageSet.Add("spend_content", this.grdReceipt.Rows[grdrow].Cells["spend_content"].Value.ToString());
                        MessageSet.Add("staff_id", this.grdReceipt.Rows[grdrow].Cells["cmbStaffId"].Value.ToString());
                        MessageSet.Add("staff_name", this.grdReceipt.Rows[grdrow].Cells["cmbStaffNm"].Value.ToString());
                        MessageSet.Add("spend_cost", this.grdReceipt.Rows[grdrow].Cells["spend_cost"].Value.ToString());

                        if (this.grdReceipt.Rows[grdrow].Cells["cmbExpense_yn"].Value.ToString().ToUpper() == "계산서")
                            MessageSet.Add("expense_yn", "Y");
                        else if (this.grdReceipt.Rows[grdrow].Cells["cmbExpense_yn"].Value.ToString().ToUpper() == "비용")
                            MessageSet.Add("expense_yn", "N");

                        if (this.grdReceipt.Rows[grdrow].Cells["cmbGive_yn"].Value.ToString().ToUpper() == "O")
                            MessageSet.Add("deposit_date", this.grdReceipt.Rows[grdrow].Cells["deposit_date"].Value.ToString());

                        MessageSet.Add("give_yn", this.grdReceipt.Rows[grdrow].Cells["cmbGive_yn"].Value.ToString());

                        //이미지 변경
                        if (this.grdReceipt.Rows[grdrow].Cells["receipt_photo"].Value != null && this.grdReceipt.Rows[grdrow].Cells["receiptImg_Path"].Value != null 
                            && this.grdReceipt.Rows[grdrow].Cells["PreviewImg"].Value != null)
                            MessageSet.Add("receipt_photo", this.grdReceipt.Rows[grdrow].Cells["receiptImg_Path"].Value.ToString());
                        //이미지 삭제
                        else if (this.grdReceipt.Rows[grdrow].Cells["receipt_photo"].Value == null && this.grdReceipt.Rows[grdrow].Cells["PreviewImg"].Value == null
                                && this.grdReceipt.Rows[grdrow].Cells["receiptImg_Path"].Value == null)
                            MessageSet.Add("receipt_photo", "null");


                        chksave = smartViceData.Save(MessageSet);
                        MessageSet.Clear();
                        if (!chksave)
                        {
                            return -1;
                        }
                    }
                    else if (this.grdReceipt.Rows[grdrow].Cells["ROWSTATE"].Value.ToString() == "Deleted")
                    {
                        QueryType = "Delete";
                        MessageSet.Clear();
                        MessageSet.Add("QueryType", QueryType);
                        MessageSet.Add("valid_id", this.grdReceipt.Rows[grdrow].Cells["valid_id"].Value.ToString());
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
            for (int row = 0; row < this.grdReceipt.Rows.Count; row++)
            {
                if (this.grdReceipt.Rows[row].Cells["spend_date"].Value == null)
                {
                    MessageBox.Show(this.grdReceipt.Columns["spend_date"].HeaderText + " 값이 누락되었습니다.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return -1;
                }

                if (this.grdReceipt.Rows[row].Cells["cmbStaffId"].Value == null)
                {
                    MessageBox.Show(this.grdReceipt.Columns["cmbStaffId"].HeaderText + " 값이 누락되었습니다.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return -1;
                }

                if (this.grdReceipt.Rows[row].Cells["cmbStaffNm"].Value == null)
                {
                    MessageBox.Show(this.grdReceipt.Columns["cmbStaffNm"].HeaderText + " 값이 누락되었습니다.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return -1;
                }

                if (this.grdReceipt.Rows[row].Cells["spend_cost"].Value == null)
                {
                    MessageBox.Show(this.grdReceipt.Columns["spend_cost"].HeaderText + " 값이 누락되었습니다.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return -1;
                }

                if (this.grdReceipt.Rows[row].Cells["spend_content"].Value == null)
                {
                    MessageBox.Show(this.grdReceipt.Columns["spend_content"].HeaderText + " 값이 누락되었습니다.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return -1;
                }

                //if (this.grdReceipt.Rows[row].Cells["receipt_photo"].Value == null)
                //{
                //    MessageBox.Show(this.grdReceipt.Columns["receipt_photo"].HeaderText + " 값이 누락되었습니다.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //    return -1;
                //}

                if (this.grdReceipt.Rows[row].Cells["cmbGive_yn"].Value == null)
                {
                    MessageBox.Show(this.grdReceipt.Columns["cmbGive_yn"].HeaderText + " 값이 누락되었습니다.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return -1;
                }

                if (this.grdReceipt.Rows[row].Cells["cmbGive_yn"].Value != null && this.grdReceipt.Rows[row].Cells["cmbGive_yn"].Value.ToString() != "X")
                {
                    if (this.grdReceipt.Rows[row].Cells["deposit_date"].Value == null)
                    {
                        MessageBox.Show(this.grdReceipt.Columns["deposit_date"].HeaderText + " 값이 누락되었습니다.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return -1;
                    }
                }
            }
            return 0;
        }
        private void grdReceipt_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (chkSearched == true)
                return;

            MessageSet.Clear();
            DataTable dt = new DataTable();

            smartViceData = new RULE_SPENDINGMANAGEMENT();

            switch (grdReceipt.Columns[e.ColumnIndex].Name)
            {
                case "cmbStaffNm":
                    DataGridViewComboBoxCell cmbStaffNm = (DataGridViewComboBoxCell)grdReceipt.Rows[e.RowIndex].Cells["cmbStaffNm"];
                    if (cmbStaffNm.Value != null && cmbStaffNm.Value.ToString() != string.Empty)
                    {
                        // do stuff
                        MessageSet.Clear();
                        MessageSet.Add("Staff_Nm", this.grdReceipt.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString());
                        dt = smartViceData.SearchCommon("searchStaffInfo", MessageSet);

                        if (dt.Rows.Count > 0)
                            this.grdReceipt.Rows[e.RowIndex].Cells["cmbStaffId"].Value = dt.Rows[0].Field<string>("staff_id");

                        if (dt.Rows.Count > 2)
                            MessageBox.Show("동명이인이 존재합니다. 사번을 확인하여 주십시오.", "inform", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        grdReceipt.Invalidate();
                    }
                    break;
                case "cmbStaffId":
                    DataGridViewComboBoxCell cmbStaffId = (DataGridViewComboBoxCell)grdReceipt.Rows[e.RowIndex].Cells["cmbStaffId"];
                    if (cmbStaffId.Value != null && cmbStaffId.Value.ToString() != string.Empty)
                    {
                        // do stuff
                        MessageSet.Clear();
                        MessageSet.Add("Staff_Id", this.grdReceipt.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString());
                        dt = smartViceData.SearchCommon("searchStaffInfo", MessageSet);

                        if (dt.Rows.Count > 0)
                            this.grdReceipt.Rows[e.RowIndex].Cells["cmbStaffNm"].Value = dt.Rows[0].Field<string>("staff_name");

                        grdReceipt.Invalidate();
                    }
                    break;
                case "cmbSpendTypeNm":
                    DataGridViewComboBoxCell cmbSpendTypeNm = (DataGridViewComboBoxCell)grdReceipt.Rows[e.RowIndex].Cells["cmbSpendTypeNm"];
                    if (cmbSpendTypeNm.Value != null && cmbSpendTypeNm.Value.ToString() != string.Empty)
                    {
                        // do stuff
                        MessageSet.Clear();
                        MessageSet.Add("Spend_type_nm", this.grdReceipt.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString());
                        dt = smartViceData.SearchCommon("searchSpendType", MessageSet);

                        if (dt.Rows.Count > 0)
                            this.grdReceipt.Rows[e.RowIndex].Cells["cmbSpendTypeCd"].Value = dt.Rows[0].Field<string>("type_code");

                        grdReceipt.Invalidate();
                    }
                    break;
                case "cmbSpendTypeCd":
                    DataGridViewComboBoxCell cmbSpendTypeCd = (DataGridViewComboBoxCell)grdReceipt.Rows[e.RowIndex].Cells["cmbSpendTypeCd"];
                    if (cmbSpendTypeCd.Value != null && cmbSpendTypeCd.Value.ToString() != string.Empty)
                    {
                        // do stuff
                        MessageSet.Clear();
                        MessageSet.Add("Spend_type_cd", this.grdReceipt.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString());
                        dt = smartViceData.SearchCommon("searchSpendType", MessageSet);

                        if (dt.Rows.Count > 0)
                            this.grdReceipt.Rows[e.RowIndex].Cells["cmbSpendTypeNm"].Value = dt.Rows[0].Field<string>("code_name");

                        grdReceipt.Invalidate();
                    }
                    break;
                case "cmbGive_yn":
                    if (this.grdReceipt.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null && this.grdReceipt.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() != string.Empty)
                    {
                        if (this.grdReceipt.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() != "O")
                        {
                            this.grdReceipt.Rows[e.RowIndex].Cells[e.ColumnIndex + 1].Value = "";                            
                            Give_dtp.Visible = false;
                            //this.grdReceipt.Rows[e.RowIndex].Cells[e.ColumnIndex + 1].ReadOnly = true;
                        }
                        else
                        {
                            //this.grdReceipt.Rows[e.RowIndex].Cells[e.ColumnIndex + 1].ReadOnly = false;
                            //Give_dtp.Visible = true;
                            //Give_dtp.Value = DateTime.Today;
                        }
                    }
                    break;

            }

            if (e.ColumnIndex == grdReceipt.Columns["spend_date"].Index || e.ColumnIndex == grdReceipt.Columns["cmbStaffNm"].Index
                || e.ColumnIndex == grdReceipt.Columns["cmbStaffId"].Index || e.ColumnIndex == grdReceipt.Columns["spend_cost"].Index
                || e.ColumnIndex == grdReceipt.Columns["spend_content"].Index || e.ColumnIndex == grdReceipt.Columns["receipt_photo"].Index
                || e.ColumnIndex == grdReceipt.Columns["cmbExpense_yn"].Index || e.ColumnIndex == grdReceipt.Columns["cmbGive_yn"].Index 
                || e.ColumnIndex == grdReceipt.Columns["deposit_date"].Index)
            {
                //기존 데이터나 수정된 기존 데이터에만 modified 표시 
                if (this.grdReceipt.Rows[e.RowIndex].Cells["ROWSTATE"].Value == null || this.grdReceipt.Rows[e.RowIndex].Cells["ROWSTATE"].Value.ToString() == "Modified"
                    || this.grdReceipt.Rows[e.RowIndex].Cells["ROWSTATE"].Value.ToString() == "")
                    this.grdReceipt.Rows[e.RowIndex].Cells["ROWSTATE"].Value = "Modified";

                chkSearched = false;
            }

        }

        private void grdReceipt_Scroll(object sender, ScrollEventArgs e)
        {
            DataGridView gv = (DataGridView)sender;

            Rectangle rtHeader = gv.DisplayRectangle;

            rtHeader.Height = gv.ColumnHeadersHeight / 2;

            gv.Invalidate(rtHeader);

            Spenddate_dtp.Visible = false;
            Give_dtp.Visible = false;
        }

        private void grdReceipt_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)
                return;

            switch (grdReceipt.Columns[e.ColumnIndex].Name)
            {
                case "colCheck":                   
                        this.grdReceipt.CurrentRow.Selected = true;                
                    break;
                case "spend_date":

                    _dtpRectangle = grdReceipt.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true);
                    Spenddate_dtp.Size = new Size(_dtpRectangle.Width, _dtpRectangle.Height);
                    Spenddate_dtp.Location = new Point(_dtpRectangle.X, _dtpRectangle.Y);
                    Spenddate_dtp.Visible = true;
                    if (!this.grdReceipt.Rows[e.RowIndex].IsNewRow && this.grdReceipt.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
                    {
                        Spenddate_dtp.Value = Convert.ToDateTime(this.grdReceipt.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);
                    }
                    //else
                    //dtp.Value = DateTime.Today;

                    break;
                case "deposit_date":
                    //지급여부가 X 이면 입금날짜를 기입,수정할 수 없다.
                    if (this.grdReceipt.Rows[e.RowIndex].Cells[e.ColumnIndex - 1].Value != null)
                    {
                        if (this.grdReceipt.Rows[e.RowIndex].Cells[e.ColumnIndex - 1].Value.ToString() != "O")
                        {
                            MessageBox.Show("지급여부가 'X'일 경우에는 입금날짜를 기입할 수 없습니다.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }

                    }
                    _dtpRectangle = grdReceipt.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true);
                    Give_dtp.Size = new Size(_dtpRectangle.Width, _dtpRectangle.Height);
                    Give_dtp.Location = new Point(_dtpRectangle.X, _dtpRectangle.Y);
                    Give_dtp.Visible = true;
                    if (!this.grdReceipt.Rows[e.RowIndex].IsNewRow && this.grdReceipt.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
                    {
                        Give_dtp.Value = Convert.ToDateTime(this.grdReceipt.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);
                    }
                    //else
                    //dtp.Value = DateTime.Today;

                    break;

                case "receipt_photo":

                    if (this.grdReceipt.Rows[e.RowIndex].Cells["receipt_photo"].Value == null)
                        break;

                    ScanIMGFilePopup scanIMGFilePopup = new ScanIMGFilePopup();
                    scanIMGFilePopup.StartPosition = FormStartPosition.CenterParent;

                    if (this.grdReceipt.Rows[e.RowIndex].Cells["PreviewImg"].Value != null)
                        scanIMGFilePopup.ImgFile = (Image)this.grdReceipt.Rows[e.RowIndex].Cells["PreviewImg"].Value;

                    scanIMGFilePopup.ShowDialog();

                    //새로운 이미지가 추가되거나 기존 이미지가 변경되었을 경우
                    if (scanIMGFilePopup.ImgFile != null && (scanIMGFilePopup.fileFullPath != string.Empty && scanIMGFilePopup.fileFullPath != null))
                    {
                        this.grdReceipt.Rows[e.RowIndex].Cells["PreviewImg"].Value = scanIMGFilePopup.ImgFile;
                        this.grdReceipt.Rows[e.RowIndex].Cells["receiptImg_Path"].Value = scanIMGFilePopup.fileFullPath;
                        this.grdReceipt.Rows[e.RowIndex].Cells["receipt_photo"].Value = scanIMGFilePopup.fileFullPath.Split('\\')[scanIMGFilePopup.fileFullPath.Split('\\').Length - 1];
                    }
                    //아무 변경없이 그대로 창을 닫았을 경우
                    else if (scanIMGFilePopup.ImgFile != null && (scanIMGFilePopup.fileFullPath == string.Empty || scanIMGFilePopup.fileFullPath == null))
                    {
                        this.grdReceipt.Rows[e.RowIndex].Cells["receiptImg_Path"].Value = null;
                    }
                    else
                    {
                        this.grdReceipt.Rows[e.RowIndex].Cells["PreviewImg"].Value = null;
                        this.grdReceipt.Rows[e.RowIndex].Cells["receiptImg_Path"].Value = null;
                        this.grdReceipt.Rows[e.RowIndex].Cells["receipt_photo"].Value = null;
                    }
                    this.grdReceipt.CurrentCell = this.grdReceipt.Rows[e.RowIndex].Cells["cmbGive_yn"];
                    break;


                //GetIMGFilePopup regIMGFile = new GetIMGFilePopup();
                //regIMGFile.StartPosition = FormStartPosition.CenterParent;

                //if (this.grdReceipt.Rows[e.RowIndex].Cells["PreviewImg"].Value != null)
                //    regIMGFile.ImgFile = (Image)this.grdReceipt.Rows[e.RowIndex].Cells["PreviewImg"].Value;

                ////if (this.grdReceipt.Rows[e.RowIndex].Cells["receipt_photo"].Value != null)
                ////    regIMGFile.ImgFullPath = this.grdReceipt.Rows[e.RowIndex].Cells["receipt_photo"].Value.ToString();

                //regIMGFile.ShowDialog();

                ////새로운 이미지가 추가되거나 기존 이미지가 변경되었을 경우
                //if (regIMGFile.ImgFile != null && regIMGFile.ImgFullPath != null)
                //{
                //    this.grdReceipt.Rows[e.RowIndex].Cells["PreviewImg"].Value = regIMGFile.ImgFile;
                //    this.grdReceipt.Rows[e.RowIndex].Cells["receiptImg_Path"].Value = regIMGFile.ImgFullPath;
                //    this.grdReceipt.Rows[e.RowIndex].Cells["receipt_photo"].Value = regIMGFile.ImgFullPath.Split('\\')[regIMGFile.ImgFullPath.Split('\\').Length - 1];
                //}
                ////아무 변경없이 그대로 창을 닫았을 경우
                //else if (regIMGFile.ImgFile != null && regIMGFile.ImgFullPath == null)
                //{
                //    this.grdReceipt.Rows[e.RowIndex].Cells["receiptImg_Path"].Value = null;
                //}
                //else
                //{
                //    this.grdReceipt.Rows[e.RowIndex].Cells["PreviewImg"].Value = null;
                //    this.grdReceipt.Rows[e.RowIndex].Cells["receiptImg_Path"].Value = null;
                //    this.grdReceipt.Rows[e.RowIndex].Cells["receipt_photo"].Value = null;
                //}
                //this.grdReceipt.CurrentCell = this.grdReceipt.Rows[e.RowIndex].Cells["cmbGive_yn"];

                case "btnGetImgcol":                    

                    ScanIMGFilePopup GetIMGFilePopup = new ScanIMGFilePopup();
                    GetIMGFilePopup.StartPosition = FormStartPosition.CenterParent;

                    if (this.grdReceipt.Rows[e.RowIndex].Cells["PreviewImg"].Value != null)
                        GetIMGFilePopup.ImgFile = (Image)this.grdReceipt.Rows[e.RowIndex].Cells["PreviewImg"].Value;

                    GetIMGFilePopup.ShowDialog();

                    //새로운 이미지가 추가되거나 기존 이미지가 변경되었을 경우
                    if (GetIMGFilePopup.ImgFile != null && (GetIMGFilePopup.fileFullPath != string.Empty && GetIMGFilePopup.fileFullPath != null))
                    {
                        this.grdReceipt.Rows[e.RowIndex].Cells["PreviewImg"].Value = GetIMGFilePopup.ImgFile;
                        this.grdReceipt.Rows[e.RowIndex].Cells["receiptImg_Path"].Value = GetIMGFilePopup.fileFullPath;
                        this.grdReceipt.Rows[e.RowIndex].Cells["receipt_photo"].Value = GetIMGFilePopup.fileFullPath.Split('\\')[GetIMGFilePopup.fileFullPath.Split('\\').Length - 1];
                    }
                    //아무 변경없이 그대로 창을 닫았을 경우
                    else if (GetIMGFilePopup.ImgFile != null && (GetIMGFilePopup.fileFullPath == string.Empty || GetIMGFilePopup.fileFullPath == null))
                    {
                        this.grdReceipt.Rows[e.RowIndex].Cells["receiptImg_Path"].Value = null;
                    }
                    else
                    {
                        this.grdReceipt.Rows[e.RowIndex].Cells["PreviewImg"].Value = null;
                        this.grdReceipt.Rows[e.RowIndex].Cells["receiptImg_Path"].Value = null;
                        this.grdReceipt.Rows[e.RowIndex].Cells["receipt_photo"].Value = null;
                    }
                    this.grdReceipt.CurrentCell = this.grdReceipt.Rows[e.RowIndex].Cells["cmbGive_yn"];
                    break;
            }
        }

        private void Spenddate_dtp_TextChange(object sender, EventArgs e)
        {
            grdReceipt.CurrentCell.Value = Spenddate_dtp.Text.ToString();

        }
        private void Give_dtp_TextChange(object sender, EventArgs e)
        {
            grdReceipt.CurrentCell.Value = Give_dtp.Text.ToString();

        }

        private void grdReceipt_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)
                return;

            switch (grdReceipt.Columns[e.ColumnIndex].Name)
            {
                case "receipt_photo":
                    

                    if (this.grdReceipt.Rows[e.RowIndex].Cells["receipt_photo"].Value == null)
                        break;

                    ScanIMGFilePopup scanIMGFilePopup = new ScanIMGFilePopup();
                    scanIMGFilePopup.StartPosition = FormStartPosition.CenterParent;

                    if (this.grdReceipt.Rows[e.RowIndex].Cells["PreviewImg"].Value != null)
                        scanIMGFilePopup.ImgFile = (Image)this.grdReceipt.Rows[e.RowIndex].Cells["PreviewImg"].Value;

                    scanIMGFilePopup.ShowDialog();

                    //새로운 이미지가 추가되거나 기존 이미지가 변경되었을 경우
                    if (scanIMGFilePopup.ImgFile != null && (scanIMGFilePopup.fileFullPath != string.Empty && scanIMGFilePopup.fileFullPath != null))
                    {
                        this.grdReceipt.Rows[e.RowIndex].Cells["PreviewImg"].Value = scanIMGFilePopup.ImgFile;
                        this.grdReceipt.Rows[e.RowIndex].Cells["receiptImg_Path"].Value = scanIMGFilePopup.fileFullPath;
                        this.grdReceipt.Rows[e.RowIndex].Cells["receipt_photo"].Value = scanIMGFilePopup.fileFullPath.Split('\\')[scanIMGFilePopup.fileFullPath.Split('\\').Length - 1];
                    }
                    //아무 변경없이 그대로 창을 닫았을 경우
                    else if (scanIMGFilePopup.ImgFile != null && (scanIMGFilePopup.fileFullPath == string.Empty || scanIMGFilePopup.fileFullPath == null))
                    {
                        this.grdReceipt.Rows[e.RowIndex].Cells["receiptImg_Path"].Value = null;
                    }
                    else
                    {
                        this.grdReceipt.Rows[e.RowIndex].Cells["PreviewImg"].Value = null;
                        this.grdReceipt.Rows[e.RowIndex].Cells["receiptImg_Path"].Value = null;
                        this.grdReceipt.Rows[e.RowIndex].Cells["receipt_photo"].Value = null;
                    }
                    this.grdReceipt.CurrentCell = this.grdReceipt.Rows[e.RowIndex].Cells["cmbGive_yn"];
                    break;
                case "cmbGive_yn":
                    if (this.grdReceipt.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
                    {
                        if (this.grdReceipt.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() != "O")
                        {
                            Give_dtp.Visible = false;
                            this.grdReceipt.Rows[e.RowIndex].Cells[e.ColumnIndex + 1].Value = null;
                            this.grdReceipt.Rows[e.RowIndex].Cells[e.ColumnIndex + 1].ReadOnly = true;
                        }
                        else
                        {
                            this.grdReceipt.Rows[e.RowIndex].Cells[e.ColumnIndex + 1].ReadOnly = false;
                            Give_dtp.Visible = true;
                            Give_dtp.Value = DateTime.Today;
                        }
                    }
                    break;

            }
        }
        // This event handler manually raises the CellValueChanged event 
        // by calling the CommitEdit method. 
        private void grdReceipt_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (grdReceipt.IsCurrentCellDirty)
            {
                // This fires the cell value changed handler below
                grdReceipt.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }


        private void btnPrint_Click(object sender, EventArgs e)
        {
            //Open the print dialog
            PrintDialog printDialog = new PrintDialog();
            printDialog.Document = printDocument1;
            printDialog.UseEXDialog = true;
            //Get the document
            if (DialogResult.OK == printDialog.ShowDialog())
            {
                printDocument1.DocumentName = "Test Page Print";
                printDocument1.Print();
            }
            /*
            Note: In case you want to show the Print Preview Dialog instead of 
            Print Dialog then comment the above code and uncomment the following code
            */

            //Open the print preview dialog
            //PrintPreviewDialog objPPdialog = new PrintPreviewDialog();
            //objPPdialog.Document = printDocument1;
            //objPPdialog.ShowDialog();
        }

        #region Begin Print Event Handler
        /// <span class="code-SummaryComment"><summary></span>
        /// Handles the begin print event of print document
        /// <span class="code-SummaryComment"></summary></span>
        /// <span class="code-SummaryComment"><param name=""sender""></param></span>
        /// <span class="code-SummaryComment"><param name=""e""></param></span>
        private void printDocument1_BeginPrint(object sender,
            System.Drawing.Printing.PrintEventArgs e)
        {
            try
            {
                strFormat = new StringFormat();
                strFormat.Alignment = StringAlignment.Near;
                strFormat.LineAlignment = StringAlignment.Center;
                strFormat.Trimming = StringTrimming.EllipsisCharacter;

                arrColumnLefts.Clear();
                arrColumnWidths.Clear();
                iCellHeight = 0;
                iCount = 0;
                bFirstPage = true;
                bNewPage = true;

                // Calculating Total Widths
                iTotalWidth = 0;
                foreach (DataGridViewColumn dgvGridCol in grdReceipt.Columns)
                {
                    if (dgvGridCol.Visible == false || dgvGridCol.Name == this.grdReceipt.Columns["btnGetImgcol"].Name
                        || dgvGridCol.Name == this.grdReceipt.Columns["colCheck"].Name)
                        continue;

                    iTotalWidth += dgvGridCol.Width;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region Print Page Event
        /// <span class="code-SummaryComment"><summary></span>
        /// Handles the print page event of print document
        /// <span class="code-SummaryComment"></summary></span>
        /// <span class="code-SummaryComment"><param name=""sender""></param></span>
        /// <span class="code-SummaryComment"><param name=""e""></param></span>
        private void printDocument1_PrintPage(object sender,
    System.Drawing.Printing.PrintPageEventArgs e)
        {
            try
            {
                //Set the left margin
                int iLeftMargin = e.MarginBounds.Left;
                //Set the top margin
                int iTopMargin = e.MarginBounds.Top;
                //Whether more pages have to print or not
                bool bMorePagesToPrint = false;
                int iTmpWidth = 0;

                //For the first page to print set the cell width and header height
                if (bFirstPage)
                {
                    foreach (DataGridViewColumn GridCol in grdReceipt.Columns)
                    {
                        if (GridCol.Visible == false || GridCol.Name == this.grdReceipt.Columns["btnGetImgcol"].Name
                            || GridCol.Name == this.grdReceipt.Columns["colCheck"].Name)
                            continue;

                        iTmpWidth = (int)(Math.Floor((double)((double)GridCol.Width /
                            (double)iTotalWidth * (double)iTotalWidth *
                            ((double)e.MarginBounds.Width / (double)iTotalWidth))));

                        iHeaderHeight = (int)(e.Graphics.MeasureString(GridCol.HeaderText,
                            GridCol.InheritedStyle.Font, iTmpWidth).Height) + 11;

                        // Save width and height of headers
                        arrColumnLefts.Add(iLeftMargin);
                        arrColumnWidths.Add(iTmpWidth);
                        iLeftMargin += iTmpWidth;
                    }
                }
                int iRow = 0;

                //Loop till all the grid rows not get printed
                while (iRow <= grdReceipt.Rows.Count - 1)
                {
                    DataGridViewRow GridRow = grdReceipt.Rows[iRow];
                    //Set the cell height
                    iCellHeight = GridRow.Height + 5;
                    int iCount = 0;
                    //Check whether the current page settings allows more rows to print
                    if (iTopMargin + iCellHeight >= e.MarginBounds.Height + e.MarginBounds.Top)
                    {
                        bNewPage = true;
                        bFirstPage = false;
                        bMorePagesToPrint = true;
                        break;
                    }
                    else
                    {
                        if (bNewPage)
                        {
                            //Draw Header
                            e.Graphics.DrawString("영수증 사용 내역서",
                                new Font(grdReceipt.Font, FontStyle.Bold),
                                Brushes.Black, e.MarginBounds.Left,
                                e.MarginBounds.Top - e.Graphics.MeasureString("영수증 사용 내역서",
                                new Font(grdReceipt.Font, FontStyle.Bold),
                                e.MarginBounds.Width).Height - 13);

                            String strDate = "인쇄 날짜 : " + DateTime.Now.ToShortDateString() + " " +
                                DateTime.Now.ToShortTimeString();
                            //Draw Date
                            e.Graphics.DrawString(strDate,
                                new Font(grdReceipt.Font, FontStyle.Bold), Brushes.Black,
                                e.MarginBounds.Left +
                                (e.MarginBounds.Width - e.Graphics.MeasureString(strDate,
                                new Font(grdReceipt.Font, FontStyle.Bold),
                                e.MarginBounds.Width).Width),
                                e.MarginBounds.Top - e.Graphics.MeasureString("영수증 사용 내역서",
                                new Font(new Font(grdReceipt.Font, FontStyle.Bold),
                                FontStyle.Bold), e.MarginBounds.Width).Height - 13);

                            //Draw Columns                 
                            iTopMargin = e.MarginBounds.Top;
                            foreach (DataGridViewColumn GridCol in grdReceipt.Columns)
                            {
                                if (GridCol.Visible == false || GridCol.Name == this.grdReceipt.Columns["btnGetImgcol"].Name
                                    || GridCol.Name == this.grdReceipt.Columns["colCheck"].Name)
                                    continue;

                                e.Graphics.FillRectangle(new SolidBrush(Color.LightGray),
                                    new Rectangle((int)arrColumnLefts[iCount], iTopMargin,
                                    (int)arrColumnWidths[iCount], iHeaderHeight));

                                e.Graphics.DrawRectangle(Pens.Black,
                                    new Rectangle((int)arrColumnLefts[iCount], iTopMargin,
                                    (int)arrColumnWidths[iCount], iHeaderHeight));

                                e.Graphics.DrawString(GridCol.HeaderText,
                                    GridCol.InheritedStyle.Font,
                                    new SolidBrush(GridCol.InheritedStyle.ForeColor),
                                    new RectangleF((int)arrColumnLefts[iCount], iTopMargin,
                                    (int)arrColumnWidths[iCount], iHeaderHeight), strFormat);
                                iCount++;
                            }
                            bNewPage = false;
                            iTopMargin += iHeaderHeight;
                        }
                        iCount = 0;
                        //Draw Columns Contents                
                        foreach (DataGridViewCell Cel in GridRow.Cells)
                        {
                            if (Cel.Visible == false || Cel.ColumnIndex == this.grdReceipt.Columns["btnGetImgcol"].Index
                                || Cel.ColumnIndex == this.grdReceipt.Columns["colCheck"].Index)
                                continue;

                            if (Cel.Value != null)
                            {
                                e.Graphics.DrawString(Cel.Value.ToString(),
                                    Cel.InheritedStyle.Font,
                                    new SolidBrush(Cel.InheritedStyle.ForeColor),
                                    new RectangleF((int)arrColumnLefts[iCount],
                                    (float)iTopMargin,
                                    (int)arrColumnWidths[iCount], (float)iCellHeight),
                                    strFormat);
                            }
                            //Drawing Cells Borders 
                            e.Graphics.DrawRectangle(Pens.Black,
                                new Rectangle((int)arrColumnLefts[iCount], iTopMargin,
                                (int)arrColumnWidths[iCount], iCellHeight));
                            iCount++;
                        }
                    }
                    iRow++;
                    iTopMargin += iCellHeight;
                }
                //If more lines exist, print another page.
                if (bMorePagesToPrint)
                    e.HasMorePages = true;
                else
                    e.HasMorePages = false;
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        private void btnExcelExport_Click(object sender, EventArgs e)
        {
            if (this.grdReceipt.Rows.Count < 1)
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
            if (grdReceipt.Rows.Count == 1)
            {
                MessageBox.Show("엑셀 파일로 저장할 데이터가 없습니다.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                worksheet = workbook.ActiveSheet;

                int cellRowIndex = 1;
                int cellColumsIndex = 1;

                //Add Column
                for (int col = 1; col < grdReceipt.Columns.Count; col++)
                {
                    if (cellRowIndex == 1)
                    {
                        if (grdReceipt.Columns[col].Visible == true && grdReceipt.Columns[col].Name != this.grdReceipt.Columns["btnGetImgcol"].Name)
                        {
                            worksheet.Cells[cellRowIndex, cellColumsIndex] = grdReceipt.Columns[col].HeaderText;
                            worksheet.Cells[cellRowIndex, cellColumsIndex].Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Aqua);
                        }
                        else
                            continue;
                    }
                    cellColumsIndex++;
                }

                cellColumsIndex = 1;
                cellRowIndex++;

                for (int row = 0; row < grdReceipt.Rows.Count; row++)
                {
                    for (int col = 1; col < grdReceipt.Columns.Count; col++)
                    {
                        if (grdReceipt.Columns[col].Visible != true || grdReceipt.Columns[col].Name == this.grdReceipt.Columns["btnGetImgcol"].Name)
                        {
                            continue;
                        }

                        if (grdReceipt.Rows[row].Cells[col].Value != null)
                        {
                            string cellValue = grdReceipt.Rows[row].Cells[col].Value.ToString();

                            if (col == 1)
                                cellValue = Convert.ToDateTime(grdReceipt.Rows[row].Cells[col].Value.ToString()).ToString("yyyy년mm월dd일");

                            worksheet.Cells[cellRowIndex, cellColumsIndex] = cellValue;
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
                string endRange = "G" + (this.grdReceipt.Rows.Count + 1).ToString();

                // 전체  
                worksheet.get_Range(startRange, endRange).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;

                //컬럼 너비 조정
                worksheet.get_Range("C1").Columns.ColumnWidth = 15;
                worksheet.get_Range("D1").Columns.ColumnWidth = 15;
                worksheet.get_Range("E1").Columns.ColumnWidth = 30;
                worksheet.get_Range("F1").Columns.ColumnWidth = 50;

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

        private void btnSendMail_Click(object sender, EventArgs e)
        {
            //if (!chkMakeZip)
            //{
            //    MessageBox.Show("메일로 보낼 데이터들을 먼저 압축해주십시오.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    return;
            //}

            SendMailPopup sendMailPopup = new SendMailPopup();
            sendMailPopup.ZipFile = zipfile_path;

            sendMailPopup.ShowDialog();

           
        }

        private void btnMakeZip_Click(object sender, EventArgs e)
        {
            try
            {
                int cnt_selectedRow = 0;

                //압축 전에 데이터의 유효성 확인
                foreach (DataGridViewRow dr in grdReceipt.Rows)
                {
                    if (dr.Cells["colCheck"].Value != null && dr.Cells["colCheck"].Value.ToString().ToUpper() != "false".ToUpper())
                    {
                        if (dr.Cells["receipt_photo"].Value == null && dr.Cells["PreviewImg"].Value == null)
                        {
                            MessageBox.Show(dr.Cells["cmbStaffNm"].Value.ToString() + "님의 영수증 이미지를 추가하여 주십시오", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }

                        if (dr.Cells["PreviewImg"].Value != null)
                        {
                            cnt_selectedRow++;
                        }
                    }
                }

                if (cnt_selectedRow == 0)
                {
                    MessageBox.Show("선택된 레코드가 없습니다.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                FolderBrowserDialog dialog = new FolderBrowserDialog();
                dialog.ShowDialog();
                string SelectedPath = dialog.SelectedPath;

                List<string> lstImg = new List<string>();

                //그리드에서 선택된 모든 행에 첨부된 이미지를 로컬에 저장.
                foreach (DataGridViewRow dr in grdReceipt.Rows)
                {
                    if (dr.Cells["colCheck"].Value != null && dr.Cells["colCheck"].Value.ToString().ToUpper() != "false".ToUpper())
                    {
                        if (dr.Cells["PreviewImg"].Value != null)
                        {
                            try
                            {
                                Image img = (Image)dr.Cells["PreviewImg"].Value;
                                img.Save(SelectedPath + "\\" + dr.Cells["cmbStaffNm"].Value.ToString() + "_영수증_" + DateTime.Now.ToString("yyyy-MM-dd") + ".jpeg");

                                lstImg.Add(SelectedPath + "\\" + dr.Cells["cmbStaffNm"].Value.ToString() + "_영수증_" + DateTime.Now.ToString("yyyy-MM-dd") + ".jpeg");
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                        }
                    }
                }

                //선택한 레코드들로 엑셀 파일 생성.
                string excelfile_path = ExportToExcelForMail(SelectedPath);

                lstImg.Add(excelfile_path + ".xlsx");

                //압축
                CompressZipByIO(SelectedPath, SelectedPath + "\\" + "Jetspurt.zip", lstImg);
                
                MessageBox.Show("압축되었습니다.", "Inform", MessageBoxButtons.OK, MessageBoxIcon.Information);

                zipfile_path = SelectedPath + "\\" + "Jetspurt.zip";
                chkMakeZip = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                chkMakeZip = false;
            }
        }
        /// <param name="sourcePath"></param>
        /// <param name="zipPath"></param>
        public static void CompressZipByIO(string sourcePath, string zipPath, List<string> filelist)
        {
            // var filelist = GetFileList(sourcePath, new List<String>());
            using (FileStream fileStream = new FileStream(zipPath, FileMode.Create, FileAccess.ReadWrite))
            {
                using (ZipArchive zipArchive = new ZipArchive(fileStream, ZipArchiveMode.Create))
                {
                    foreach (string file in filelist)
                    {
                        string path = file.Substring(sourcePath.Length + 1);
                        zipArchive.CreateEntryFromFile(file, path);
                    }
                }
            }
        }
        /// <summary>
        /// 디렉토리내 파일 검색
        /// </summary>
        /// <param name="rootPath"></param>
        /// <param name="fileList"></param>
        /// <returns></returns>
        public static List<String> GetFileList(String rootPath, List<String> fileList)
        {
            if (fileList == null)
            {
                return null;
            }
            var attr = File.GetAttributes(rootPath);
            // 해당 path가 디렉토리이면
            if ((attr & FileAttributes.Directory) == FileAttributes.Directory)
            {
                var dirInfo = new DirectoryInfo(rootPath);
                // 하위 모든 디렉토리는
                foreach (var dir in dirInfo.GetDirectories())
                {
                    // 재귀로 통하여 list를 취득한다.
                    GetFileList(dir.FullName, fileList);
                }
                // 하위 모든 파일은
                foreach (var file in dirInfo.GetFiles())
                {
                    // 재귀를 통하여 list를 취득한다.
                    GetFileList(file.FullName, fileList);
                }
            }
            // 해당 path가 파일이면 (재귀를 통해 들어온 경로)
            else
            {
                var fileInfo = new FileInfo(rootPath);
                // 리스트에 full path를 저장한다.
                fileList.Add(fileInfo.FullName);
            }
            return fileList;
        }
        private string ExportToExcelForMail(string filepath)
        {
            bool isExport = false;

            //Creating a Excel object
            Microsoft.Office.Interop.Excel._Application excel = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel._Workbook workbook = excel.Workbooks.Add(Type.Missing);
            Microsoft.Office.Interop.Excel._Worksheet worksheet = null;     

            try
            {
                worksheet = workbook.ActiveSheet;

                int cellRowIndex = 1;
                int cellColumsIndex = 1;

                //Add Column
                for (int col = 1; col < grdReceipt.Columns.Count; col++)
                {
                    if (cellRowIndex == 1)
                    {
                        if (grdReceipt.Columns[col].Visible != true || grdReceipt.Columns[col].Name == this.grdReceipt.Columns["btnGetImgcol"].Name
                            || grdReceipt.Columns[col].Name == this.grdReceipt.Columns["cmbStaffId"].Name || grdReceipt.Columns[col].Name == this.grdReceipt.Columns["cmbExpense_yn"].Name
                            || grdReceipt.Columns[col].Name == this.grdReceipt.Columns["cmbGive_yn"].Name || grdReceipt.Columns[col].Name == this.grdReceipt.Columns["deposit_date"].Name)
                        {
                            continue;
                        }
                       
                        worksheet.Cells[cellRowIndex, cellColumsIndex] = grdReceipt.Columns[col].HeaderText;
                        worksheet.Cells[cellRowIndex, cellColumsIndex].Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Aqua);
                       
                    }
                    cellColumsIndex++;
                }

                cellColumsIndex = 1;
                cellRowIndex++;

                for (int row = 0; row < grdReceipt.Rows.Count; row++)
                {
                    for (int col = 1; col < grdReceipt.Columns.Count; col++)
                    {
                        if (grdReceipt.Columns[col].Visible != true || grdReceipt.Columns[col].Name == this.grdReceipt.Columns["btnGetImgcol"].Name
                            || grdReceipt.Columns[col].Name == this.grdReceipt.Columns["cmbStaffId"].Name || grdReceipt.Columns[col].Name == this.grdReceipt.Columns["cmbExpense_yn"].Name
                            || grdReceipt.Columns[col].Name == this.grdReceipt.Columns["cmbGive_yn"].Name || grdReceipt.Columns[col].Name == this.grdReceipt.Columns["deposit_date"].Name)
                        {
                            continue;
                        }

                        //체크안된건 패스
                        if (grdReceipt.Rows[row].Cells["colCheck"].Value == null || grdReceipt.Rows[row].Cells["colCheck"].Value.ToString().ToUpper() == "false".ToUpper())
                            continue;

                        if (grdReceipt.Rows[row].Cells[col].Value != null)
                        {
                            string cellValue = string.Empty;

                            //영수증 이미지 담는 컬럼에 하이퍼링크 추가
                            if (grdReceipt.Columns[col].Name == this.grdReceipt.Columns["receipt_photo"].Name)
                            {
                                //Get the allready exists sheet
                                Microsoft.Office.Interop.Excel.Worksheet mWSheet1 = (Microsoft.Office.Interop.Excel.Worksheet)worksheet;

                                AddHyperLink(mWSheet1, cellRowIndex, filepath + "\\" + grdReceipt.Rows[row].Cells["cmbStaffNm"].Value.ToString() + "_영수증_" + DateTime.Now.ToString("yyyy-MM-dd") + ".jpeg");
                                continue;
                            }

                            cellValue = grdReceipt.Rows[row].Cells[col].Value.ToString();

                            if (col == 1)
                                cellValue = Convert.ToDateTime(grdReceipt.Rows[row].Cells[col].Value.ToString()).ToString("yyyy년mm월dd일");

                            worksheet.Cells[cellRowIndex, cellColumsIndex] = cellValue;
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
                string endRange = "F" + (this.grdReceipt.Rows.Count + 1).ToString();

                // 전체  
                worksheet.get_Range(startRange, endRange).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;

                //컬럼 너비 조정
                worksheet.get_Range("C1").Columns.ColumnWidth = 15;
                worksheet.get_Range("D1").Columns.ColumnWidth = 15;
                worksheet.get_Range("E1").Columns.ColumnWidth = 30;
                worksheet.get_Range("F1").Columns.ColumnWidth = 100;


                //SaveFileDialog saveFileDialog = GetExcelSave();

                //if (saveFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                //{
                workbook.SaveAs(filepath + "\\" + "JETSPURT_" + this.Text + "_" + DateTime.Today.ToString().Trim().Substring(0, 10));
                    //MessageBox.Show("Export Successful!", "Inform", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    isExport = true;
                //}

                //Export 성공했으면 객체들 해제
                if (isExport)
                {
                    workbook.Close();

                    excel.Quit();
                    workbook = null;
                    excel = null;

                    return filepath + "\\" + "JETSPURT_" + this.Text + "_" + DateTime.Today.ToString().Trim().Substring(0, 10);
                }
                return "";
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return "";
            }
        }

        private static void AddHyperLink(Microsoft.Office.Interop.Excel.Worksheet sheet, int iCol, string fullfileName)
        {
            Microsoft.Office.Interop.Excel.Hyperlink link = sheet.Hyperlinks.Add(
               sheet.Range["F" + iCol.ToString(), Type.Missing], fullfileName,
               Type.Missing, fullfileName, fullfileName);

        }
        private void grdReceipt_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            MessageBox.Show(e.Exception.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        private void grdReceipt_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
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
                cb.CheckedChanged += new EventHandler(grdReceipt_CheckedChanged);

                this.grdReceipt.Controls.Add(cb);
                //((DataGridView)sender).Controls.Add(cb);

                e.Handled = true;
            }
        }
        private void grdReceipt_CheckedChanged(object sender, EventArgs e)
        {
            grdReceipt.CurrentCell = null;

            if (cb.CheckState == CheckState.Checked)
                this.grdReceipt.SelectAll();
            else
                grdReceipt.ClearSelection();

            foreach (DataGridViewRow r in grdReceipt.Rows)
            {
                if (cb.CheckState == CheckState.Checked)
                    r.Cells["colCheck"].Value = true;
                else
                    r.Cells["colCheck"].Value = false;

            }
        }

        private void SpendingManagement_Shown(object sender, EventArgs e)
        {
            //grdReceipt.ClearSelection();
            grdReceipt.CurrentCell = null;
        }
    }
}
