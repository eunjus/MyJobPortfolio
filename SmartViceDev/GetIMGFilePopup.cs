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
    public partial class GetIMGFilePopup : Form
    {
        RULE_NEWSTAFFREGPOPUP smartViceData = new RULE_NEWSTAFFREGPOPUP();       

        Dictionary<string, string> MessageSet = new Dictionary<string, string>();
        public Image ImgFile { get; set; }
        public string ImgFullPath { get; set; }
        public Dictionary<string, string> PassMessageSet { get; set; } = new Dictionary<string, string>();

        byte[] ImgrawData;
        string ImgfileFullPath = string.Empty;
        uint FileSize;

        string fileFullPath = string.Empty;

        public GetIMGFilePopup()
        {
            InitializeComponent();
        }


        private void RegIMGFilePopup_Load(object sender, EventArgs e)
        {
            eventslist();

            if (ImgFile != null)
            {
                this.Picturebox.Image = ImgFile;
                this.Picturebox.SizeMode = PictureBoxSizeMode.StretchImage;

                //setStaffInfo(ReciveStaffID); //기존 직원 정보 불러오기 
            }                    

        }
        private void eventslist()
        {
            this.btnSaveNClose.Click += new System.EventHandler(this.btnSaveNClose_Click);
            //this.btnSave.Click += new System.EventHandler(this.btnSave_Click);                        
            this.btnRegImg.Click += new System.EventHandler(this.btnRegImg_Click);
            this.btnRemoveImg.Click += new System.EventHandler(this.btnRemoveImg_Click);
            
        }

        private Image BlobToImg(byte[] imgblob)
        {
            byte[] image = imgblob;
            MemoryStream ms = new MemoryStream(image);
            Image staff_img = Image.FromStream(ms);

            return staff_img;

        }


        private void btnSaveNClose_Click(object sender, EventArgs e)
        {
            if (Picturebox.Image != null)
            {
                ImgFullPath = ImgfileFullPath.ToString();
                ImgFile = Picturebox.Image;
            }
            this.Close();
        }
        
        private void btnRegImg_Click(object sender, EventArgs e)
        {            
            string filepath = ShowFileOpenDialog();

            if (filepath != string.Empty)
            {
                Picturebox.Load(filepath);
                Picturebox.SizeMode = PictureBoxSizeMode.StretchImage;
            }
        }

        public string ShowFileOpenDialog()
        {
            //파일오픈창 생성 및 설정
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "파일 오픈 예제창";
            ofd.FileName = "test";
            ofd.Filter = "그림 파일 (*.jpg, *.gif, *.bmp, *.png) | *.jpg; *.gif; *.bmp; *.png; | 모든 파일 (*.*) | *.*";

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
       
        private void btnRemoveImg_Click(object sender, EventArgs e)
        {
            if(Picturebox.Image != null)
                Picturebox.Image = null;
        }

        private void RegIMGFilePopup_FormClosing(object sender, FormClosingEventArgs e)
        {
            //this.Close();
        }
    }
}
