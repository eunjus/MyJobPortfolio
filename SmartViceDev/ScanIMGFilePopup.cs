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
using WIA; 

namespace SmartViceDev
{
    public partial class ScanIMGFilePopup : Form
    {       

        Dictionary<string, string> MessageSet = new Dictionary<string, string>();
        public Image ImgFile { get; set; }
        public string fileFullPath { get; set; }
        public Dictionary<string, string> PassMessageSet { get; set; } = new Dictionary<string, string>();

        string ImgfileFullPath = string.Empty;

        public ScanIMGFilePopup()
        {
            InitializeComponent();
        }


        private void ScanIMGFilePopup_Load(object sender, EventArgs e)
        {
            eventslist();

            if (ImgFile != null)
            {
                this.pic_scan.Image = ImgFile;
                this.pic_scan.SizeMode = PictureBoxSizeMode.StretchImage;

            }

        }
        private void eventslist()
        {
            this.btnSaveNClose.Click += new System.EventHandler(this.btnSaveNClose_Click);                      
            this.btnScanImg.Click += new System.EventHandler(this.btnScanImg_Click);
            this.btnRemoveImg.Click += new System.EventHandler(this.btnRemoveImg_Click);
            this.btnRegImg.Click += new System.EventHandler(this.btnRegImg_Click);
            this.btnDownloadImg.Click += new System.EventHandler(this.btnDownloadImg_Click);
            this.btnRotateImage.Click += new System.EventHandler(this.btnRotateImage_Click);
        }

        private void btnSaveNClose_Click(object sender, EventArgs e)
        {
            if (pic_scan.Image != null)
            {
                fileFullPath = ImgfileFullPath.ToString();
                ImgFile = pic_scan.Image;
            }
            else
                ImgFile = null;

            this.Close();
        }
        
        private void btnScanImg_Click(object sender, EventArgs e)
        {
            try
            {
                //get list of devices available
                
                List<string> devices = WIAScanner.GetDevices();

                foreach (string device in devices)
                {
                    tbDevices.Items.Add(device);
                }
                //check if device is not available
                if (tbDevices.Items.Count == 0)
                {
                    MessageBox.Show("You do not have any WIA devices.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    this.Close();
                }
                else
                {
                    tbDevices.SelectedIndex = 0;
                }
                //get images from scanner
                List<Image> images = WIAScanner.Scan((string)tbDevices.SelectedItem);

                foreach (Image image in images)
                {
                    pic_scan.Image = image;
                    pic_scan.Show();
                    pic_scan.SizeMode = PictureBoxSizeMode.StretchImage;

                    FolderBrowserDialog dialog = new FolderBrowserDialog();
                    dialog.ShowDialog();
                    string SelectedPath = dialog.SelectedPath;

                    image.Save(SelectedPath + DateTime.Now.ToString("yyyy-MM-dd") + ".jpeg");
                    ImgfileFullPath = SelectedPath + DateTime.Now.ToString("yyyy-MM-dd") + ".jpeg";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRotateImage_Click(object sender, EventArgs e)
        {
            Bitmap img = new Bitmap(pic_scan.Image);

            pic_scan.Image = fnRotateImage(img, -90);
            pic_scan.Invalidate();
        }

        private Bitmap fnRotateImage(Bitmap b, float angle)
        {
            Bitmap returnBitmap = new Bitmap(b.Height, b.Width);
            Graphics g = Graphics.FromImage(returnBitmap);
            g.TranslateTransform((float)returnBitmap.Width / 2, (float)returnBitmap.Height / 2);
            g.RotateTransform(angle);

            g.TranslateTransform(-(float)b.Width / 2, -(float)b.Height / 2);
            g.DrawImage(b, new Point(0, 0));

            return returnBitmap;
        }
        private void btnRegImg_Click(object sender, EventArgs e)
        {
            string filepath = ShowFileOpenDialog();

            if (filepath != string.Empty)
            {
                pic_scan.Load(filepath);
                pic_scan.SizeMode = PictureBoxSizeMode.StretchImage;
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
            if(pic_scan.Image != null)
                pic_scan.Image = null;
        }

        private void RegIMGFilePopup_FormClosing(object sender, FormClosingEventArgs e)
        {
            //this.Close();
        }

        private void btnDownloadImg_Click(object sender, EventArgs e)
        {
            if (pic_scan.Image != null)
            {
                //save scanned image into specific folder
                try
                {
                    FolderBrowserDialog dialog = new FolderBrowserDialog();
                    dialog.ShowDialog();
                    string SelectedPath = dialog.SelectedPath;

                    pic_scan.Image.Save(SelectedPath + "\\" + DateTime.Now.ToString("yyyy-MM-dd") + ".jpeg");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                MessageBox.Show("이미지를 성공적으로 내려받았습니다", "Inform", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                MessageBox.Show("내려받을 이미지가 없습니다.", "Warning", MessageBoxButtons.OK , MessageBoxIcon.Warning);
                return;
            }
        }
    }
}
