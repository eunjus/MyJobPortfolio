using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SmartViceDev.CubridUtils;

namespace SmartViceDev
{
    public partial class GetAddressBookPopup : Form
    {
        public Dictionary<string, string> MessageSet = new Dictionary<string, string>();
        public List<string> PassSelectedAddr { get; set; } = new List<string>();
        public string GroupOption { get; set; }
        string EmailAddrs = string.Empty;
        RULE_GETADDRESSBOOKPOPUP smartvice = new RULE_GETADDRESSBOOKPOPUP();
    

        public GetAddressBookPopup()
        {
            InitializeComponent();
        }

        private void GetAddressBookPopup_Load(object sender, EventArgs e)
        {
            grdStaffColumnAdd();
            grdCustomColumnAdd();

            eventslist();
            setCombobox();

            this.grdCustomAddr.Visible = false;
            //btnSearch_Click(null,null);

            if (GroupOption != null)
            {
                if (PassSelectedAddr.Count > 0)
                {
                    if (PassSelectedAddr[0].Length != 0)
                    {                
                        if (GroupOption == "직원")
                        {
                            this.cmbGroupNm.SelectedItem = "직원";         

                            foreach (DataGridViewRow row in grdStaffAddr.Rows)
                            {
                                if (PassSelectedAddr.Contains(row.Cells["staff_email"].Value.ToString()))
                                    row.Cells["colCheck"].Value = true;
                            }
                        }
                        else
                        {
                            this.cmbGroupNm.SelectedItem = "거래처";

                            foreach (DataGridViewRow row in grdCustomAddr.Rows)
                            {
                                if (PassSelectedAddr.Contains(row.Cells["customer_staff_email"].Value.ToString()))
                                    row.Cells["colCheck"].Value = true;
                            }
                        }
                    }
                }                
            }
            grdStaffAddr.RowHeadersVisible = false;
            grdCustomAddr.RowHeadersVisible = false;
        }
        private void eventslist()
        {
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            this.grdCustomAddr.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grdCustomAddr_CellDoubleClick);
            this.btnCheck.Click += new System.EventHandler(this.btnCheck_Click);
            this.cmbGroupNm.SelectedIndexChanged += new System.EventHandler(this.cmbGroupNm_SelectedIndexChanged);
        }

        private void setCombobox()
        {
            this.cmbGroupNm.Items.Add("직원");
            this.cmbGroupNm.Items.Add("거래처");

            this.cmbGroupNm.SelectedItem = "직원";
        }

        private void grdStaffColumnAdd()
        {         
            var chkCol = new DataGridViewCheckBoxColumn
            {
                Name = "colCheck",
                HeaderText = ""
            };
            this.grdStaffAddr.Columns.Add(chkCol);
            this.grdStaffAddr.Columns["colCheck"].Width = 50;

            //직원 아이디
            this.grdStaffAddr.Columns.Add("staff_id", "직원 아이디");
            this.grdStaffAddr.Columns["staff_id"].Visible = false;

            //직원 이름
            this.grdStaffAddr.Columns.Add("staff_name", "이름");
            this.grdStaffAddr.Columns["staff_name"].Width = 100;

            //부서명
            this.grdStaffAddr.Columns.Add("depart_name", "부서명");
            this.grdStaffAddr.Columns["depart_name"].Width = 100;

            //직원 이메일
            this.grdStaffAddr.Columns.Add("staff_email", "이메일");
            this.grdStaffAddr.Columns["staff_email"].Width = 150;

            grdStaffAddr.AllowUserToAddRows = false;  // 빈레코드 표시 안하기
            grdStaffAddr.AlternatingRowsDefaultCellStyle.BackColor = Color.AliceBlue; // 그리드 행에 색 입히기
            this.grdStaffAddr.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }
        private void grdCustomColumnAdd()
        {
            var chkCol = new DataGridViewCheckBoxColumn
            {
                Name = "colCheck",
                HeaderText = ""
            };
            this.grdCustomAddr.Columns.Add(chkCol);
            this.grdCustomAddr.Columns["colCheck"].Width = 50;

            //거래처 아이디
            this.grdCustomAddr.Columns.Add("customer_id", "거래처 아이디");
            this.grdCustomAddr.Columns["customer_id"].Visible = false;

            //거래처 이름
            this.grdCustomAddr.Columns.Add("customer_name", "거래처 명");
            this.grdCustomAddr.Columns["customer_name"].Width = 100;

            //직원 이름
            this.grdCustomAddr.Columns.Add("customer_staff_name", "직원 이름");
            this.grdCustomAddr.Columns["customer_staff_name"].Width = 100;

            //직원 이메일
            this.grdCustomAddr.Columns.Add("customer_staff_email", "직원 이메일");
            this.grdCustomAddr.Columns["customer_staff_email"].Width = 150;

            grdCustomAddr.AllowUserToAddRows = false;  // 빈레코드 표시 안하기
            grdCustomAddr.AlternatingRowsDefaultCellStyle.BackColor = Color.AliceBlue; // 그리드 행에 색 입히기
            this.grdCustomAddr.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            MessageSet.Clear();
            
            if(EmailAddrs != string.Empty)
                MessageSet.Add("EmailAddrs", EmailAddrs);

            if (this.cmbGroupNm.SelectedItem.ToString() == "직원")
            {
                this.grdStaffAddr.Visible = true;
                this.grdCustomAddr.Visible = false;
                this.grdStaffAddr.Rows.Clear();

                MessageSet.Add("SearchType", "SearchStaffAddr");

                if(this.tbName.Text != string.Empty)
                    MessageSet.Add("Staff_name", this.tbName.Text);

                DataTable dt = smartvice.Search(MessageSet);

                for (int row = 0; row < dt.Rows.Count; row++)
                {
                    this.grdStaffAddr.Rows.Add();
                    this.grdStaffAddr["staff_id", row].Value = dt.Rows[row]["staff_id"].ToString();
                    this.grdStaffAddr["staff_name", row].Value = dt.Rows[row]["staff_name"].ToString();
                    this.grdStaffAddr["staff_email", row].Value = dt.Rows[row]["staff_email"].ToString();
                    this.grdStaffAddr["depart_name", row].Value = dt.Rows[row]["depart_name"].ToString();
                }
            }
            else
            {
                this.grdStaffAddr.Visible = false;
                this.grdCustomAddr.Visible = true;
                this.grdCustomAddr.Rows.Clear();

                MessageSet.Add("SearchType", "SearchCustomAddr");

                if (this.tbName.Text != string.Empty)
                    MessageSet.Add("Customer_name", this.tbName.Text);

                DataTable dt = smartvice.Search(MessageSet);

                for (int row = 0; row < dt.Rows.Count; row++)
                {
                    this.grdCustomAddr.Rows.Add();
                    this.grdCustomAddr["customer_id", row].Value = dt.Rows[row]["customer_id"].ToString();
                    this.grdCustomAddr["customer_name", row].Value = dt.Rows[row]["customer_name"].ToString();
                    this.grdCustomAddr["customer_staff_name", row].Value = dt.Rows[row]["customer_staff_name"].ToString();
                    this.grdCustomAddr["customer_staff_email", row].Value = dt.Rows[row]["customer_staff_email"].ToString();
                }
                // 데이터에 맞게 칼럼 사이즈 조정하기
                //for (int i = 0; i < grdDepart.Columns.Count; i++)
                //{
                //    grdCustomAddr.AutoResizeColumn(i, DataGridViewAutoSizeColumnMode.AllCells);
                //}
               
            }                     
        }

        private void cmbGroupNm_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnSearch_Click(null, null);
        }

        private void grdCustomAddr_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (this.cmbGroupNm.SelectedItem.ToString() == "직원")
            {
                PassSelectedAddr.Add(this.grdStaffAddr.SelectedRows[0].Cells["staff_email"].Value.ToString());
            }
            else
            {
                PassSelectedAddr.Add(this.grdStaffAddr.SelectedRows[0].Cells["customoer_staff_email"].Value.ToString());
            }
        }

        private void btnCheck_Click(object sender, EventArgs e)
        {
            PassSelectedAddr.Clear();

            if (this.cmbGroupNm.SelectedItem.ToString() == "직원")
            {
                foreach (DataGridViewRow dr in grdStaffAddr.Rows)
                {
                    if (dr.Cells["colCheck"].Value != null && dr.Cells["colCheck"].Value.ToString().ToUpper() != "false".ToUpper())
                    {
                        PassSelectedAddr.Add(dr.Cells["staff_email"].Value.ToString());
                    }
                }                
            }
            else
            {
                foreach (DataGridViewRow dr in grdCustomAddr.Rows)
                {
                    if (dr.Cells["colCheck"].Value != null && dr.Cells["colCheck"].Value.ToString().ToUpper() != "false".ToUpper())
                    {
                        PassSelectedAddr.Add(this.grdCustomAddr.SelectedRows[0].Cells["customoer_staff_email"].Value.ToString());
                    }
                }
            }
            GroupOption = this.cmbGroupNm.SelectedItem.ToString();
            this.Close();
        }

    }
}
