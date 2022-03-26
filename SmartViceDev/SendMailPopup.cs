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
using System.Net.Mail;

namespace SmartViceDev
{
    public partial class SendMailPopup : Form
    {       

        Dictionary<string, string> MessageSet = new Dictionary<string, string>();
        public string ZipFile { get; set; }
        public string fileFullPath { get; set; }
        public List<string> lst_emailAddr { get; set; } = new List<string>();
        public Dictionary<string, string> PassMessageSet { get; set; } = new Dictionary<string, string>();
        public string RecipientType { get; set; }

        string ImgfileFullPath = string.Empty;
        

        public List<string> ToEmailAddressList
        {
            get
            {
                List<string> lst = new List<string>();
                string[] strAR = this.tbRecipientAddr.Text.Trim().Replace(" ", "").Split(';');
                for (int i = 0; i < strAR.Length; i++)
                    lst.Add(strAR[i]);
                return lst;
            }
            set
            {
                StringBuilder sb = new StringBuilder();
                foreach (object itm in value)
                {
                    if (sb.Length > 0) sb.Append(";");
                    sb.Append(itm.ToString());
                }
            }
        }


        public SendMailPopup()
        {
            InitializeComponent();
        }


        private void SendMailPopup_Load(object sender, EventArgs e)
        {
            eventslist();

            DataTable dt = new DataTable();
            dt.Columns.Add("Value", typeof(string));
            dt.Columns.Add("Text", typeof(string));
            lstAttachFiles.ValueMember = "Value";
            lstAttachFiles.DisplayMember = "Text";
            lstAttachFiles.DataSource = dt;

            if (ZipFile != string.Empty && ZipFile != null)
            {
                DataTable newdt = (DataTable)lstAttachFiles.DataSource;
                DataRow dr = dt.NewRow();
                dr["Value"] = ZipFile;
                dr["Text"] = ZipFile.Substring(openFileDialog.FileName.LastIndexOf('\\') + 1);
                newdt.Rows.Add(dr);
                lstAttachFiles.DataSource = newdt;

                //lstAttachFiles.Items.Clear();
                //lstAttachFiles.Items.Add(ZipFile);
            }

            string totalAddr = string.Empty;

            if (lst_emailAddr.Count > 0)
            {
                foreach (string selectedaddr in lst_emailAddr)
                {
                    totalAddr += selectedaddr + ";";
                }
                this.tbRecipientAddr.Text = totalAddr.Substring(0, totalAddr.Length - 1);

                ToEmailAddressList = lst_emailAddr;                
            }

        }
        private void eventslist()
        {
            this.btnCancle.Click += new System.EventHandler(this.btnCancle_Click);
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            this.btnGetFile.Click += new System.EventHandler(this.btnGetFile_Click);
        }
        public List<string> AttachedFileList
        {
            get
            {
                List<string> lst = new List<string>();
                DataTable dt = (DataTable)lstAttachFiles.DataSource;
                for (int i = 0; i < dt.Rows.Count; i++)
                    lst.Add(Convert.ToString(dt.Rows[i]["Value"]));
                return lst;
            }
        }
        private void btnSend_Click(object sender, EventArgs e)
        {
            if (chkSave() == -1)
                return;

            try
            {
                Cursor = Cursors.WaitCursor;

                List<string> toList = ToEmailAddressList;

                if (toList != null && toList.Count > 0)
                {
                    foreach (string sTo in toList)
                    {
                        string SendMail = tbSenderAddr.Text;    // 보내는사람
                        //string ArrMail = tbRecipientAddr.Text;    // 받는사람
                        string Subject = tbtitle.Text;    // 주제,제목
                        string Body = tbContent.Text;    // 본문

                        string mailserverip = "127.0.0.1"; //보내는 사람 PC IP

                        //파라미터 : (보내는사람, 받는사람, 주제, 본문)
                        MailMessage msg = new MailMessage();

                        // SmtpClient 셋업 (Live SMTP 서버, 포트)
                        SmtpClient smtp = new SmtpClient(mailserverip, 25);
                        smtp.EnableSsl = true;

                        //메세지
                        msg.From = new MailAddress(SendMail, "Jetspurt", System.Text.Encoding.UTF8);
                        msg.To.Add(sTo); //받는사람주소
                        msg.Subject = Subject;
                        msg.Body = Body;
                        msg.IsBodyHtml = false;
                        msg.SubjectEncoding = System.Text.Encoding.UTF8;
                        msg.BodyEncoding = System.Text.Encoding.UTF8;

                        //첨부파일
                        foreach (string sFile in AttachedFileList)
                        {
                            System.Net.Mail.Attachment att;
                            att = new System.Net.Mail.Attachment(sFile);
                            att.NameEncoding = Encoding.UTF8;                    
                            msg.Attachments.Add(att);
                        }
                        //발송
                        //smtp.Port = 25;
                        smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                        smtp.Credentials = new System.Net.NetworkCredential("ID", "PASS"); //계정ID,PASS [익명으로 설정시 필요없음]
                        smtp.EnableSsl = false;
                        smtp.Send(msg);

                    }
                Cursor = Cursors.Default;
               
                }
                MessageBox.Show("성공적으로 메일을 전송하였습니다.", "Inform", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                ex.ToString();
            }        
        }
        private void btnGetFile_Click(object sender, EventArgs e)
        {
            fileFullPath = string.Empty;
            openFileDialog.InitialDirectory = "C:\\";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                DataTable dt = (DataTable)lstAttachFiles.DataSource;
                DataRow dr = dt.NewRow();
                dr["Value"] = openFileDialog.FileName;
                dr["Text"] = openFileDialog.FileName.Substring(openFileDialog.FileName.LastIndexOf('\\') + 1);
                dt.Rows.Add(dr);
                lstAttachFiles.DataSource = dt;
                ////선택된 파일의 풀 경로를 불러와 저장
                //fileFullPath = openStaffFileDialog.FileName;
                ////해당 경로에서 파일명만 추출하여 표시
                ////tbjobfile.Text = fileFullPath.Split('\\')[fileFullPath.Split('\\').Length - 1]
                //tbAttachment.Text = fileFullPath;
            }
        }
        private int chkSave()
        {
            //필수 정보 입력 누락 확인
            if (tbSenderAddr.Text == string.Empty)
            {
                MessageBox.Show(lbSenderAddr.Text + " 값이 누락되었습니다.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tbSenderAddr.Focus();
                return -1;
            }

            if (tbRecipientAddr.Text == string.Empty)
            {
                MessageBox.Show(lbRecipientAddr.Text + " 값이 누락되었습니다.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tbRecipientAddr.Focus();
                return -1;
            }

            if (tbtitle.Text == string.Empty)
            {
                MessageBox.Show(lbTitle.Text + " 값이 누락되었습니다.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tbtitle.Focus();
                return -1;
            }

            if (tbContent.Text == string.Empty)
            {
                MessageBox.Show(lbTitle.Text + " 값이 누락되었습니다.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tbContent.Focus();
                return -1;
            }

            //if (cbStaffdepart.Text == string.Empty || cbStaffdepart.Text == "전체")
            //{
            //    MessageBox.Show(lbdepart.Name + " 선택이 잘못되었습니다.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    return -1;
            //}
         
            return 1;
        }


        private void RegIMGFilePopup_FormClosing(object sender, FormClosingEventArgs e)
        {
            //this.Close();
        }

        private void btnCancle_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnRemoveFile_Click(object sender, EventArgs e)
        {
            if (lstAttachFiles.SelectedIndex >= 0)
            {
                //lstAttachFiles.Items.RemoveAt(lstAttachFiles.SelectedIndex);
                DataTable dt = (DataTable)lstAttachFiles.DataSource;
                dt.Rows.RemoveAt(lstAttachFiles.SelectedIndex);
                lstAttachFiles.DataSource = dt;

            }
        }

        private void btnAddrBook_Click(object sender, EventArgs e)
        {
            GetAddressBookPopup getAddressBookPopup = new GetAddressBookPopup();
            getAddressBookPopup.StartPosition = FormStartPosition.CenterScreen;

            if (RecipientType != string.Empty && RecipientType != null)
            {
                getAddressBookPopup.GroupOption = RecipientType;
                if (ToEmailAddressList.Count > 0)
                    getAddressBookPopup.PassSelectedAddr = ToEmailAddressList;
            }

            getAddressBookPopup.ShowDialog();
            
            string totalAddr = string.Empty;

            if (getAddressBookPopup.PassSelectedAddr.Count > 0)
            {
                foreach (string selectedaddr in getAddressBookPopup.PassSelectedAddr)
                {
                    totalAddr += selectedaddr + ";";
                }
                this.tbRecipientAddr.Text = totalAddr.Substring(0, totalAddr.Length - 1);

                ToEmailAddressList = getAddressBookPopup.PassSelectedAddr;

                if (getAddressBookPopup.GroupOption != null)
                    RecipientType = getAddressBookPopup.GroupOption;
            }
        }

    }
}
