using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Xml;

namespace SmartViceDev
{
    public partial class GetZipNAddressPopup : Form
    {
        //우편번호 검색 초기 설정값
        string currentPage = "1";  //현재 페이지
        string countPerPage = "1000"; //1페이지당 출력 갯수
        string confmKey = "U01TX0FVVEgyMDIwMDkyOTE2NTIzNjExMDI0NTI="; //테스트 Key
        string keyword = string.Empty;
        string apiurl = string.Empty;
               
        public string FullAddr { get; set; }
        //public Dictionary<string, string> PassMessageSet { get; set; } = new Dictionary<string, string>();
        
        public GetZipNAddressPopup()
        {
            InitializeComponent();
        }
        private void GetZipNAddressPopup_Load(object sender, EventArgs e)
        {
            eventslist();
            grdColumnAdd();

            grdAddress.RowHeadersVisible = false;
        }
        private void grdColumnAdd()
        {
            //var chkCol = new DataGridViewCheckBoxColumn
            //{
            //    Name = "colCheck",
            //    HeaderText = ""
            //};
            //this.grdAddress.Columns.Add(chkCol);
            //this.grdAddress.Columns["colCheck"].Width = 50;

            //도로명주소
            this.grdAddress.Columns.Add("roadNm_Addr", "도로명주소");
            this.grdAddress.Columns["roadNm_Addr"].Width = 300;
            //지번주소
            this.grdAddress.Columns.Add("jibun_Addr", "지번주소");
            this.grdAddress.Columns["jibun_Addr"].Width = 300;
            //우편번호
            this.grdAddress.Columns.Add("zipcode", "우편번호");
            this.grdAddress.Columns["zipcode"].Width = 100;
            this.grdAddress.Columns["zipcode"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }
            private void eventslist()
        {
            this.btnSaveNClose.Click += new System.EventHandler(this.btnSaveNClose_Click);
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            this.tbRoadNm.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbRoadNm_KeyDown);
            this.grdAddress.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grdAddress_CellDoubleClick);
        }

        //사용안함 - btnSaveNClose_Click
        #region
        private void btnSaveNClose_Click(object sender, EventArgs e)
        {
            string full_Addr = string.Empty;
            int cntchk = 0;

            foreach (DataGridViewRow dr in this.grdAddress.Rows)
            {                
                if (dr.Cells["colCheck"].Value != null)
                {
                    cntchk++;
                    full_Addr = dr.Cells["roadNm_Addr"].Value.ToString() + "/" + dr.Cells["zipcode"].Value.ToString();              
                }                
            }
            if (cntchk != 1)
            {
                MessageBox.Show("한개의 주소만 선택하여 주십시오.");
                return;
            }

            FullAddr = full_Addr;
            this.Close();
        }
        #endregion

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                keyword = tbRoadNm.Text.Trim();
                //오픈API사용
                apiurl = "http://www.juso.go.kr/addrlink/addrLinkApi.do?currentPage=" + currentPage + "&countPerPage=" + countPerPage + "&keyword=" + keyword + "&confmKey=" + confmKey;

                //textBox2.Text = apiurl + "\r\n";

                WebClient wc = new WebClient();

                XmlReader read = new XmlTextReader(wc.OpenRead(apiurl));

                DataSet ds = new DataSet();
                ds.ReadXml(read);

                grdPageInfos.DataSource = ds.Tables[0];

                DataRow[] rows = ds.Tables[0].Select();

                //textBox2.Text += rows[0]["totalCount"].ToString();

                //총 건수
                lbCntDt.Text = rows[0]["totalCount"].ToString() + "건";

                if (rows[0]["totalCount"].ToString() != "0")
                {
                    this.grdAddress.Rows.Clear();

                    DataTable dt = ds.Tables[1];
                    foreach (DataRow row in ds.Tables[1].Rows)
                    {
                        int rowIdx = this.grdAddress.Rows.Add();
                        this.grdAddress["roadNm_Addr", rowIdx].Value = row["roadAddr"].ToString();
                        this.grdAddress["jibun_Addr", rowIdx].Value = row["jibunAddr"].ToString();
                        this.grdAddress["zipcode", rowIdx].Value = row["zipNo"].ToString();
                    }
                }
                else
                    MessageBox.Show("정확한 명칭을 입력하여 주십시오.");
            }
            catch (Exception ex)
            {
            }
        }

        private void tbRoadNm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.btnSearch_Click(sender, e);
            }        
        }

        private void grdAddress_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {            
            FullAddr = this.grdAddress.Rows[e.RowIndex].Cells["roadNm_Addr"].Value.ToString() + "/" +
                            this.grdAddress.Rows[e.RowIndex].Cells["zipcode"].Value.ToString();
            
            this.Close();
        }
    }
}
