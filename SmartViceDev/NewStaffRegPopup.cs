using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SmartViceDev.CubridUtils;

namespace SmartViceDev
{
    public partial class NewStaffRegPopup : Form
    {
        RULE_NEWSTAFFREGPOPUP smartViceData = new RULE_NEWSTAFFREGPOPUP();
        DateTimePicker dtp = new DateTimePicker();
        bool chkstaffid = false;
        bool chkSaved = false;
        bool chkExistfile = false;

        Dictionary<string, string> MessageSet = new Dictionary<string, string>();
        public string ReciveStaffID { get; set; }
        public Dictionary<string, string> PassMessageSet { get; set; } = new Dictionary<string, string>();

        byte[] ImgrawData;
        Image staffimg;
        string ImgfileFullPath = string.Empty;
        uint FileSize;

        byte[] filerawData;
        string fileFullPath = string.Empty;

        public NewStaffRegPopup()
        {
            InitializeComponent();
        }


        private void NewStaffRegPopup_Load(object sender, EventArgs e)
        {
            eventslist();
            comboboxListAdd();

            if (ReciveStaffID != null)
            {
                setStaffInfo(ReciveStaffID); //기존 직원 정보 불러오기 
                chkstaffid = true;
            }                    

        }
        private void eventslist()
        {
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            this.btnDupChk.Click += new System.EventHandler(this.btnDupChk_Click);
            this.btnAutoMake.Click += new System.EventHandler(this.btnAutoMake_Click);            
            this.btnGetFile.Click += new System.EventHandler(this.btnGetFile_Click);            
            this.btnRegImg.Click += new System.EventHandler(this.btnRegImg_Click);
            this.btnRemoveImg.Click += new System.EventHandler(this.btnRemoveImg_Click);            
            this.btnZipNum.Click += new System.EventHandler(this.btnZipNum_Click);
            this.btnDownloadFile.Click += new System.EventHandler(this.btnDownloadFile_Click);
            this.chkSingle.CheckedChanged += new System.EventHandler(this.chkSingle_CheckedChanged);
            this.chkRetireYN.CheckedChanged += new System.EventHandler(this.chkRetireYN_CheckedChanged);
            this.btnRemoveImg.TabStopChanged += new System.EventHandler(this.btnRemoveImg_Click);
            this.tbstaffId.TextChanged += new System.EventHandler(this.tbstaffId_TextChanged);
            this.tbstaffPhoneNb.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbstaffPhoneNb_KeyPress);

        }

            private void comboboxListAdd()
        {
            DataTable ds = new DataTable();
            //고용형태 콤보박스 리스트업 
            MessageSet.Clear();
            ds = smartViceData.SearchCommon("GetStaffType", MessageSet);

            this.cbstafftype.Items.Add("전체");
            for (int row = 0; row < ds.Rows.Count; row++)
            {
                this.cbstafftype.Items.Add(ds.Rows[row].Field<string>("staff_type_name").ToString());
            }
            this.cbstafftype.SelectedItem = "전체";

            //부서 콤보박스 리스트업 
            ds = smartViceData.SearchCommon("GetDepart", MessageSet);

            this.cbStaffdepart.Items.Add("전체");
            for (int row = 0; row < ds.Rows.Count; row++)
            {
                this.cbStaffdepart.Items.Add(ds.Rows[row].Field<string>("depart_name").ToString());
            }
            this.cbStaffdepart.SelectedItem = "전체";

            //부서 콤보박스 리스트업 
            ds = smartViceData.SearchCommon("GetPosition", MessageSet);

            this.cbstaffPos.Items.Add("전체");
            for (int row = 0; row < ds.Rows.Count; row++)
            {
                this.cbstaffPos.Items.Add(ds.Rows[row].Field<string>("position_name").ToString());
            }
            this.cbstaffPos.SelectedItem = "전체";


            //은행 리스트업
            this.cbBankNm.Items.Add("전체");
            this.cbBankNm.Items.Add("농협은행");
            this.cbBankNm.Items.Add("국민은행");
            this.cbBankNm.Items.Add("신한은행");
            this.cbBankNm.Items.Add("우리은행");
            this.cbBankNm.Items.Add("하나은행");
            this.cbBankNm.Items.Add("기업은행");
            this.cbBankNm.Items.Add("시티은행");
            this.cbBankNm.Items.Add("우체국");
            this.cbBankNm.Items.Add("SC제일은행");
            this.cbBankNm.Items.Add("새마을금고");
            this.cbBankNm.Items.Add("대구은행");
            this.cbBankNm.Items.Add("광주은행");
            this.cbBankNm.Items.Add("신협은행");
            this.cbBankNm.Items.Add("전북은행");
            this.cbBankNm.Items.Add("경남은행");
            this.cbBankNm.Items.Add("부산은행");
            this.cbBankNm.Items.Add("수협은행");
            this.cbBankNm.Items.Add("제주은행");
            this.cbBankNm.Items.Add("저축은행");
            this.cbBankNm.Items.Add("산림조합");
            this.cbBankNm.Items.Add("케이뱅크");
            this.cbBankNm.Items.Add("카카오뱅크");
        }

        private void setStaffInfo(string staffid)
        {
            DataTable dt = new DataTable();

            MessageSet.Clear();

            MessageSet.Add("Staff_Id", staffid);
            dt = smartViceData.Search(MessageSet);
  
            byte[] imgbytes = smartViceData.GetStaffPhoto(MessageSet); 
            if (imgbytes != null)
                 staffimg = BlobToImg(imgbytes);

            byte[] jobfile = smartViceData.GetJobFile(MessageSet);
            filerawData = jobfile;
            chkExistfile = false;

            if (dt.Rows.Count > 0)
            {
                tbstaffId.Text = dt.Rows[0].Field<string>("staff_id");
                tbstaffNm.Text = dt.Rows[0].Field<string>("staff_name");
                tbstaffEmail.Text = dt.Rows[0].Field<string>("staff_email").Split('@')[dt.Rows[0].Field<string>("staff_email").Split('@').Length - 2];
                cbStaffdepart.SelectedItem = dt.Rows[0].Field<string>("depart_name");
                cbstaffPos.SelectedItem = dt.Rows[0].Field<string>("position_name");
                tbstaffPhoneNb.Text = dt.Rows[0].Field<int>("cel_number").ToString().PadLeft(11, '0');
                tbAcctHolder.Text = dt.Rows[0].Field<string>("account_name");
                tbAcctNum.Text = dt.Rows[0].Field<string>("account_number").ToString();
                cbBankNm.SelectedItem = dt.Rows[0].Field<string>("bank_name");

                if (dt.Rows[0].Field<string>("tel_number") != null)
                {
                    tbStaffTelNb.Text = dt.Rows[0].Field<string>("tel_number").ToString().PadLeft(10, '0');
                }
                else
                {
                    tbStaffTelNb.Text = string.Empty;
                }
                if (dt.Rows[0].Field<string>("gender") == "W")
                    rbtnWoman.Checked = true;
                else
                    rbtnMan.Checked = true;

                if (dt.Rows[0].Field<string>("marriage_yn") == "Y")
                {
                    chkSingle.Checked = false;
                    chkmarried.Checked = true;
                }
                else
                {
                    chkSingle.Checked = true;
                    chkmarried.Checked = false;
                }
                dtMarriege.Value = dt.Rows[0].Field<DateTime>("marriage_dt");
                dtJoin.Value = dt.Rows[0].Field<DateTime>("join_dt");
                if(dt.Rows[0].Field<string>("resign_dt") != "")
                    dtRetire.Value = Convert.ToDateTime(dt.Rows[0].Field<string>("resign_dt"));
                else
                    chkRetireYN.Checked = true;

                dtstaffBirth.Value = dt.Rows[0].Field<DateTime>("birth_dt");
                cbstafftype.SelectedItem = dt.Rows[0].Field<string>("staff_type_name");
                if (staffimg != null) 
                {
                    PicStaff.SizeMode = PictureBoxSizeMode.StretchImage;
                    PicStaff.Image = staffimg;
                }
                if (jobfile != null)
                    tbjobfile.Text = tbstaffNm.Text + "_이력서.xls";
                    //tbjobfile.Text = BlobToFile(jobfile);
                if (dt.Rows[0].Field<string>("address") != null)
                {
                    tbZipcode.Text = dt.Rows[0].Field<string>("address").Split('\\')[0];
                    tbMainAddr.Text = dt.Rows[0].Field<string>("address").Split('\\')[1];
                    tbDetailaddr.Text = dt.Rows[0].Field<string>("address").Split('\\')[2];
                }
                else
                {
                    tbZipcode.Text = "";
                    tbMainAddr.Text = "";
                    tbDetailaddr.Text = "";
                }
            }                        
        }
        private string BlobToFile(byte[] fileblob)
        {
            try
            {
                string filePath = @"C:\Jetspurt\career_files\" + tbstaffNm.Text + "_이력서.xls";

            //기존 파일을 지우고 새로 생성.
            if (File.Exists(filePath))
            {
                File.Delete(filePath);  
            }            
            using (Stream file = File.OpenWrite(filePath))
            {
                file.Write(fileblob, 0, fileblob.Length);
            }

            return filePath;

            }
            catch (Exception ex)
            {
                MessageBox.Show("이력서 파일 불러오기에 실패하였습니다.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }

        }

        private Image BlobToImg(byte[] imgblob)
        {
            try
            {
                byte[] image = imgblob;
                MemoryStream ms = new MemoryStream(image);
                Image staff_img = Image.FromStream(ms);

                return staff_img;
            }
            catch (Exception ex)
            {
                MessageBox.Show("이미지를 불러오기에 실패하였습니다.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }            
        }

        private void dtp_TextChange(object sender, EventArgs e)
        {
            //tbMarriegeDt.Text = dtp.Text.ToString(); 
        }

        private void btnDupChk_Click(object sender, EventArgs e)
        {
            if (tbstaffId.Text == string.Empty)
            {
                MessageBox.Show("사번을 먼저 입력하여 주십시오.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
                
            if (!chkstaffid)
            {
                DataTable dt = new DataTable();

                MessageSet.Clear();
                MessageSet.Add("Staff_Id", tbstaffId.Text);

                dt = smartViceData.SearchCommon("searchStaffId", MessageSet);

                if (dt.Rows.Count > 0)
                {
                    MessageBox.Show("중복된 사번이 존재합니다.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                chkstaffid = true;
            }
            MessageBox.Show("중복확인이 되었습니다.", "Inform", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
                        
        }

        private void btnAutoMake_Click(object sender, EventArgs e)
        {
            //기존에 저장되어 있는 사번정보가 있는 경우 
            if (tbstaffId.Text == string.Empty)
            {
                string strJetIdNm = string.Empty;
                //string strmaxId = dt.Rows[0].Field<string>("JetId");
                if (cbstafftype.Text != "전체" && cbstafftype.Text != string.Empty)
                {
                    if (cbstafftype.Text == "정규직")
                        strJetIdNm = "JWP01_2018";
                    else if (cbstafftype.Text == "계약직")
                        strJetIdNm = "JWP02_2018";
                }
                else
                {
                    MessageBox.Show("고용형태를 먼저 선택하여 주십시오.", "Inform", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                DataTable dt = new DataTable();

                MessageSet.Clear();
                dt = smartViceData.SearchCommon("GetStaffId",MessageSet);

                if (dt.Rows.Count > 0)
                {                    
                    Int32.TryParse(dt.Rows[0].Field<string>("max_staff_Id"), out int maxId);
                    tbstaffId.Text = strJetIdNm + (maxId + 1).ToString().PadLeft(2, '0');
                }
                chkstaffid = true;
            }
            else
            {
                MessageBox.Show("이미 입력된 값이 있습니다.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            int chksave = chkSave();

            if (chksave == -1)
            {
                return;            
            }

            chksave = Save();

            if (chksave == 0)
                MessageBox.Show("저장할 데이터가 존재하지 않습니다.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else if (chksave == 1)
            {           
                setStaffInfo(tbstaffId.Text); //기존 직원 정보 불러오기 
                chkstaffid = true;

                MessageBox.Show("저장하였습니다.", "Inform", MessageBoxButtons.OK, MessageBoxIcon.Information);
                chkSaved = true;

            }
            else
                MessageBox.Show("저장에 실패하였습니다.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);

        }

        private int Save()
        {
            bool chksave = true;

            MessageSet.Clear();

            //저장할 정보 딕셔너리에 담기.
            if (ReciveStaffID != null)
            {
                MessageSet.Add("QueryType", "Update_bank_Acct");
                MessageSet.Add("staff_Type", cbstafftype.Text);
                MessageSet.Add("Staff_Id", tbstaffId.Text);
                MessageSet.Add("account_name", tbAcctHolder.Text);
                MessageSet.Add("account_number", tbAcctNum.Text);
                MessageSet.Add("bank_name", cbBankNm.Text);
                chksave = smartViceData.Save(MessageSet);

            }
            else
            {
                MessageSet.Add("QueryType", "Insert_bank_Acct");
                MessageSet.Add("staff_Type", cbstafftype.Text);
                MessageSet.Add("Staff_Id", tbstaffId.Text);
                MessageSet.Add("account_name", tbAcctHolder.Text);
                MessageSet.Add("account_number", tbAcctNum.Text);
                MessageSet.Add("bank_name", cbBankNm.Text);
                chksave = smartViceData.Save(MessageSet);
            }

            if (!chksave)
            {
                return -1;
            }

            MessageSet.Clear();
            //저장할 정보 딕셔너리에 담기.
            if (ReciveStaffID != null)            
                MessageSet.Add("QueryType", "Update");            
            else            
                MessageSet.Add("QueryType", "Insert");
            
            MessageSet.Add("Staff_Id", tbstaffId.Text);
            MessageSet.Add("staff_name", tbstaffNm.Text);
            MessageSet.Add("staff_email", tbstaffEmail.Text + lbJetEMail.Text);
            MessageSet.Add("staff_dep", cbStaffdepart.Text);
            MessageSet.Add("staff_pos", cbstaffPos.Text);
            MessageSet.Add("staff_celnum", tbstaffPhoneNb.Text);
            if (tbStaffTelNb.Text != string.Empty)
                MessageSet.Add("staff_telnum", tbStaffTelNb.Text);

            if (rbtnMan.Checked == true && rbtnWoman.Checked == false)
                MessageSet.Add("staff_gender", "M");
            else if (rbtnMan.Checked == false && rbtnWoman.Checked == true)
                MessageSet.Add("staff_gender", "W");

            if (chkSingle.Checked == true && chkmarried.Checked == false)
            {
                MessageSet.Add("staff_YNmarriage", "N");
                MessageSet.Add("staff_Dtmarriage", string.Empty);
            }
            else if (chkSingle.Checked == false && chkmarried.Checked == true)
            {
                MessageSet.Add("staff_YNmarriage", "Y");
                MessageSet.Add("staff_Dtmarriage", dtMarriege.Value.ToShortDateString());
            }

            MessageSet.Add("staff_DtJoin", dtJoin.Value.ToShortDateString());
            if (chkRetireYN.Checked != true)
                MessageSet.Add("staff_DtRetire", dtRetire.Value.ToShortDateString());            
                
            MessageSet.Add("staff_birth", dtstaffBirth.Value.ToShortDateString());
            MessageSet.Add("staff_Type", cbstafftype.Text);

            //기존 이미지 있을 때 .. Update
            if (ReciveStaffID != null)
            {
                if (staffimg != null)
                {
                    MessageSet.Add("chk_IMG", "unedited"); //변경없음
                }
                else
                {
                    //변경
                    if (ImgfileFullPath != string.Empty)
                    {
                        MessageSet.Add("staff_IMGPath", ImgfileFullPath);
                        MessageSet.Add("staff_IMGSize", FileSize.ToString());
                    }                   
                }
            }
            else //새로운 직원 정보 추가 .. Insert
            {
                //새로운 이미지 추가 
                if (ImgfileFullPath != string.Empty)
                {
                    MessageSet.Add("staff_IMGPath", ImgfileFullPath);
                    MessageSet.Add("staff_IMGSize", FileSize.ToString());
                }
            }
           
            if (tbjobfile.Text != string.Empty)
                MessageSet.Add("staff_JobFile", tbjobfile.Text);

            if (tbMainAddr.Text != string.Empty)
                MessageSet.Add("staff_addr",tbMainAddr.Text + "\\" + tbDetailaddr.Text + "\\" + tbZipcode.Text);
            
            chksave = smartViceData.Save(MessageSet);

            if (!chksave)
            {
                return -1;
            }

            return 1;
        }

        private byte[] ImgToBlob(Image image)
        {
            FileStream fs = new FileStream(ImgfileFullPath, FileMode.Open, FileAccess.Read);
            FileSize = (uint)fs.Length;

            ImgrawData = new byte[FileSize];
            fs.Read(ImgrawData, 0, (int)FileSize);
            fs.Close();

            return ImgrawData;

        }


        private int chkSave()
        {
            //필수 정보 입력 누락 확인
            if (tbstaffId.Text == string.Empty)
            {
                MessageBox.Show(lbstaffId.Text + " 값이 누락되었습니다.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return -1;
            }
            //중복확인을 안했거나 했을 때 중복된 값일 경우
            if (!chkstaffid)
            {
                MessageBox.Show(lbstaffId.Text + " 중복확인을 해주십시오.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return -1;
            }
            //직원 이름
            if (tbstaffNm.Text == string.Empty)
            {
                MessageBox.Show(lbstaffNm.Text + " 값이 누락되었습니다.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return -1;
            }
            //성별
            if (rbtnMan.Checked == false && rbtnWoman.Checked == false)
            {
                MessageBox.Show("성별 값이 누락되었습니다.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return -1;
            }
            //이메일
            if (tbstaffEmail.Text == string.Empty)
            {
                MessageBox.Show(lbEmail.Text + " 값이 누락되었습니다.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return -1;
            }
            //결혼 여부
            if (chkSingle.Checked == true && chkmarried.Checked == true)
            {
                MessageBox.Show(lbMarriage.Text + "를 하나만 선택하여 주십시오.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return -1;
            }
            //예금주
            if (tbAcctHolder.Text == string.Empty)
            {
                MessageBox.Show(lbAcctHolder.Text + " 값이 누락되었습니다.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return -1;
            }
            //계좌번호
            if (tbAcctNum.Text == string.Empty)
            {
                MessageBox.Show(lbacctNum.Text + " 값이 누락되었습니다.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return -1;
            }
            //부서
            if (cbStaffdepart.Text == string.Empty || cbStaffdepart.Text == "전체")
            {
                MessageBox.Show(lbdepart.Text + " 선택이 잘못되었습니다.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return -1;
            }
            //직급
            if (cbstaffPos.Text == string.Empty || cbstaffPos.Text == "전체" )
            {
                MessageBox.Show(lbjobPos.Text + " 선택이 잘못되었습니다.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return -1;
            }           
            //핸드폰 번호
            if (tbstaffPhoneNb.Text == string.Empty)
            {
                MessageBox.Show(lbphoneNum.Text + " 값이 누락되었습니다.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return -1;
            }
            //은행명
            if (cbBankNm.Text == string.Empty || cbBankNm.Text == "전체")
            {
                MessageBox.Show(lbBankNm.Text + " 선택이 잘못되었습니다.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return -1;
            }
            //고용 형태
            if (cbstafftype.Text == string.Empty || cbstafftype.Text == "전체" )
            {
                MessageBox.Show(lbhiretype.Text + "선택이 잘못되었습니다.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return -1;
            }

            return 1;
        }


        private void btnClose_Click(object sender, EventArgs e)
        {
            if (chkSaved)
            {
                PassMessageSet.Clear();
                PassMessageSet.Add("staff_id", tbstaffId.Text);
                PassMessageSet.Add("staff_name", tbstaffNm.Text);
                PassMessageSet.Add("staff_email", tbstaffEmail.Text + lbJetEMail.Text);
                PassMessageSet.Add("staff_pos", cbstaffPos.SelectedItem.ToString());
                PassMessageSet.Add("staff_dep", cbStaffdepart.SelectedItem.ToString());

            }
            this.Close();
        }

        private void tbstaffId_TextChanged(object sender, EventArgs e)
       {
            //사번을 수정할때마다 새롭게 중복확인을 해줘야 함.            
            chkstaffid = false;
            
        }
        
        private void btnRegImg_Click(object sender, EventArgs e)
        {
            
            string filepath = ShowFileOpenDialog();

            PicStaff.Load(filepath);
            PicStaff.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        public string ShowFileOpenDialog()
        {
            //파일오픈창 생성 및 설정
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "파일 오픈 예제창";
            ofd.FileName = "test";
            ofd.Filter = "그림 파일 (*.jpg, *.gif, *.bmp) | *.jpg; *.gif; *.bmp; | 모든 파일 (*.*) | *.*";

            //파일 오픈창 로드
            DialogResult dr = ofd.ShowDialog();

            //OK버튼 클릭시
            if (dr == DialogResult.OK)
            {
                //File명과 확장자를 가지고 온다.
                string fileName = ofd.SafeFileName;
                //File경로와 File명을 모두 가지고 온다.
                ImgfileFullPath = ofd.FileName;
                //File경로만 가지고 온다.
                string filePath = ImgfileFullPath.Replace(fileName, "");

                if(staffimg != null)
                    staffimg = null; //기존 이미지 삭제

                //File경로 + 파일명 리턴
                return ImgfileFullPath;
            }
            //취소버튼 클릭시 또는 ESC키로 파일창을 종료 했을경우
            else if (dr == DialogResult.Cancel)
            {
                return "";
            }

            return "";
        }

        private void chkSingle_CheckedChanged(object sender, EventArgs e)
        {
            //미혼일 때는 결혼 날짜 설정 컨트롤 비활성화 
            if (chkSingle.Checked == true)            
                dtMarriege.Enabled = false;
            else
                dtMarriege.Enabled = true;
        }

        private void btnRemoveImg_Click(object sender, EventArgs e)
        {
            if(PicStaff.Image != null)
                PicStaff.Image = null;
        }

        private void btnGetFile_Click(object sender, EventArgs e)
        {
            fileFullPath = string.Empty;
            openStaffFileDialog.InitialDirectory = "C:\\";

            if (openStaffFileDialog.ShowDialog() == DialogResult.OK)
            {
                //선택된 파일의 풀 경로를 불러와 저장
                fileFullPath = openStaffFileDialog.FileName;
                //해당 경로에서 파일명만 추출하여 표시
                //tbjobfile.Text = fileFullPath.Split('\\')[fileFullPath.Split('\\').Length - 1]
                tbjobfile.Text = fileFullPath;

                chkExistfile = true;
            }
            
        }

        private void tbstaffPhoneNb_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsDigit(e.KeyChar) || e.KeyChar == Convert.ToChar(Keys.Back)))    //숫자와 백스페이스를 제외한 나머지를 바로 처리
            {
                e.Handled = true;
            }
        
        }

        private void btnZipNum_Click(object sender, EventArgs e)
        {
            GetZipNAddressPopup getZipNAddressPopup = new GetZipNAddressPopup();
            getZipNAddressPopup.StartPosition = FormStartPosition.CenterParent;

            getZipNAddressPopup.ShowDialog();

            if (getZipNAddressPopup.FullAddr != null)
            {
                tbZipcode.Text = getZipNAddressPopup.FullAddr.Split('/')[1].ToString();
                tbMainAddr.Text = getZipNAddressPopup.FullAddr.Split('/')[0].ToString();
            }
        }

        private void btnDownloadFile_Click(object sender, EventArgs e)
        {
            if (chkExistfile == true)
            {
                FileInfo fileInfo = new FileInfo(tbjobfile.Text);
                if (fileInfo.Exists)
                {
                    MessageBox.Show("이미 로컬에 존재하는 파일입니다.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            if (tbjobfile.Text == string.Empty)
            {
                MessageBox.Show("내려받을 파일이 존재하지 않습니다.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.DefaultExt = ".xlsx";
            saveFileDialog1.Filter = "Word Documents|*.doc|Excel Worksheets|*.xls|PowerPoint Presentations|*.ppt" +
                                      "|Office Files|*.doc;*.xls;*.xlsx;*.ppt" +
                                      "|All Files|*.*";
            saveFileDialog1.Title = "저장하기";
            saveFileDialog1.FileName = tbjobfile.Text;
            DialogResult dr = saveFileDialog1.ShowDialog();

            if (dr == DialogResult.OK)
            {
                if (saveFileDialog1.FileName != "")
                {
                    string dir = saveFileDialog1.FileName; //경로 + 파일명
                    FileStream file = new FileStream(dir, FileMode.Create);
                    file.Write(filerawData, 0, filerawData.Length);
                    file.Close();
                }
            }
        }

        private void chkRetireYN_CheckedChanged(object sender, EventArgs e)
        {
            if (chkRetireYN.Checked == true)
            {
                dtRetire.Enabled = false;
            }
            else
                dtRetire.Enabled = true;
        }
    }
}
