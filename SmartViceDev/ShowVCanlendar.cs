using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SmartViceDev.CubridUtils;
using JETCalender;

namespace SmartViceDev
{
    public partial class ShowVCanlendar : Form
    {       
        List<DateTime> LeaveDates = new List<DateTime>();

        public ShowVCanlendar()
        {
            InitializeComponent();
        }

        //메인 폼과 주고 받는 데이터
        public DateTime MonthOfLeaveDt { get; set; }
        public int CntLeaveDt { get; set; }
        public List<DateTime> PassLeaveDtValue { get; set; }

        private void ShowVCanlendar_Load(object sender, EventArgs e)
        {
            LeaveDates = PassLeaveDtValue;

            if (LeaveDates.Count > 0)
            {                                
                jetCalendar.DrawCalendar(LeaveDates[0].Year.ToString(), LeaveDates[0].Month);
            }
            else
            {
                jetCalendar.DrawCalendar(MonthOfLeaveDt.Year.ToString(), MonthOfLeaveDt.Month);
            }

            foreach (DateTime dt in LeaveDates)
            {
                this.lstLeaveDays.Items.Add(dt);
                jetCalendar.BlackoutDates.Add(dt);
                this.jetCalendar.MakeBlackOutCell(dt); // 그리드에서 추가된 날짜 비활성화 표시      
            }

        }

        //private void btnDeleteLeave_Click(object sender, EventArgs e)
        //{            
        //    if (lstLeaveDays.SelectedItems.Count > 0)
        //    {
        //        jetCalendar.BlackoutDates.Remove((DateTime)lstLeaveDays.SelectedItem);
        //        lstLeaveDays.Items.Remove(lstLeaveDays.SelectedItem);               

        //        //멀티 삭제 기능
        //        //for (int dt = 0; dt< this.lstLeaveDays.SelectedItems.Count; dt++)
        //        //{
        //        //    if (lstLeaveDays.SelectedItems.Contains(lstLeaveDays.Items[dt]))
        //        //    {
        //        //        lstLeaveDays.Items.Remove(lstLeaveDays.Items[dt]);
        //        //        jetCalendar.BlackoutDates.Remove((DateTime)lstLeaveDays.Items[dt]);
        //        //    }
        //        //}
        //    }
        //}

        //private void btnReset_Click(object sender, EventArgs e)
        //{
        //    //아예 무지의 상태로 만듬.
        //    //lstLeaveDays.Items.Clear();
        //    //this.jetCalendar.BlackoutDates.Clear();

        //    lstLeaveDays.Items.Clear();
        //    jetCalendar.BlackoutDates.Clear();

        //    if (LeaveDates.Count > 0)
        //    {
        //        foreach (DateTime dt in LeaveDates)
        //        {
        //            this.lstLeaveDays.Items.Add(dt);
        //            jetCalendar.BlackoutDates.Add(dt);
        //        }

        //        jetCalendar.SelectedDate = new DateTime(LeaveDates[0].Year, LeaveDates[0].Month, 1);
        //    }
        //    else
        //        jetCalendar.SelectedDate = new DateTime(MonthOfLeaveDt.Year, MonthOfLeaveDt.Month, 1);
        //}

        //private void btnAddLeave_Click(object sender, EventArgs e)
        //{
        //    if (this.jetCalendar.SelectedDates.Count > 0)
        //    {
        //        for (int cntselectd = 0; cntselectd < this.jetCalendar.SelectedDates.Count; cntselectd++)
        //        {
        //            if (this.jetCalendar.SelectedDates[cntselectd].Month != MonthOfLeaveDt.Month)
        //            {
        //                MessageBox.Show(MonthOfLeaveDt.Month.ToString() + "월에 해당하는 날짜를 선택해주십시오.");
        //                break;
        //            }
        //            lstLeaveDays.Items.Add(this.jetCalendar.SelectedDates[cntselectd]);
        //            this.jetCalendar.BlackoutDates.Add(this.jetCalendar.SelectedDates[cntselectd]);
        //        }                
        //    }
           
        //}

        private void btn_Click(object sender, EventArgs e)
        {
            List<DateTime> lstSelectedDt = new List<DateTime>();

            Button oBtn = sender as Button;

            switch (oBtn.Name)
            {
                case "btnAdd":
                    if (this.jetCalendar.SelectedDates.Count > 0)
                    {
                        foreach (DateTime dt in this.jetCalendar.SelectedDates)
                        {
                            if (dt.Month != MonthOfLeaveDt.Month)
                            {
                                MessageBox.Show(MonthOfLeaveDt.Month.ToString() + "월에 해당하는 날짜를 선택해주십시오.");
                                break;
                            }

                            //이미 추가한 날짜를 다시 선택해서 추가려는 경우 
                            if (lstLeaveDays.Items.Contains(dt))
                                continue;

                            this.jetCalendar.BlackoutDates.Add(dt);
                            this.jetCalendar.MakeBlackOutCell(dt); // 그리드에서 추가된 날짜 비활성화 표시      

                            lstLeaveDays.Items.Add(dt);

                        }
                    }
                    break;
                case "btnDelete":
                    //멀티삭제
                    if (this.jetCalendar.SelectedDates.Count > 0)
                    {
                        foreach (DateTime dt in this.jetCalendar.SelectedDates)
                        {

                            //이미 추가한 날짜를 다시 선택해서 추가려는 경우 
                            if (!lstLeaveDays.Items.Contains(dt))
                                continue;

                            this.jetCalendar.ReturnDefaultCell(dt);
                            this.jetCalendar.BlackoutDates.Remove((DateTime)dt);

                            lstLeaveDays.Items.Remove(dt);

                        }                        
                    }
                    else
                    {
                        MessageBox.Show("삭제할 데이터가 없습니다.");
                    }
                    break;                                                  
                case "btnReset":
                    //전체 삭제 후 처음으로 되돌리기
                    foreach (DateTime dt in this.lstLeaveDays.Items)
                    {
                        this.jetCalendar.ReturnDefaultCell(dt);
                        this.jetCalendar.BlackoutDates.Remove((DateTime)dt);
                    }                    
                    lstLeaveDays.Items.Clear();

                    if (LeaveDates.Count > 0)
                    {
                        foreach (DateTime dt in LeaveDates)
                        {
                            this.lstLeaveDays.Items.Add(dt);
                            this.jetCalendar.BlackoutDates.Add(dt);
                            this.jetCalendar.MakeBlackOutCell(dt); // 그리드에서 추가된 날짜 비활성화 표시      
                        }

                        //jetCalendar.SelectedDate = new DateTime(LeaveDates[0].Year, LeaveDates[0].Month, 1);
                    }                
                    break;
                case "btnSaveNClose":
                    List<DateTime> tempget = new List<DateTime>();

                    if (lstLeaveDays.Items.Count > 0)
                    {
                        foreach (DateTime dt in lstLeaveDays.Items)
                        {
                            tempget.Add(dt);
                        }

                    }
                    PassLeaveDtValue = tempget;
                    CntLeaveDt = PassLeaveDtValue.Count;

                    this.Close();
                    break;
                default:
                    break;
            }

            this.jetCalendar.ClearSelectCell();
        }

        //private void btnSaveNClose_Click(object sender, EventArgs e)
        //{
        //    List<DateTime> tempget = new List<DateTime>();

        //    if (lstLeaveDays.Items.Count > 0)
        //    {
        //        foreach (DateTime dt in lstLeaveDays.Items)
        //        {                  
        //            tempget.Add(dt);
        //        }
                
        //    }
        //    PassLeaveDtValue = tempget;
        //    CntLeaveDt = PassLeaveDtValue.Count;
    
        //    this.Close();
        //}

        private void ShowVCanlendar_FormClosing(object sender, FormClosingEventArgs e)
        {       
            
            CntLeaveDt = PassLeaveDtValue.Count;
        }
    }
}
