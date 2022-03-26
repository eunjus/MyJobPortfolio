using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using SmartViceDev.CubridUtils;
using System.Drawing;


namespace SmartViceDev
{
    public partial class SmartViceMenuForm : Form
    {
        //조회조건 리스트
        Dictionary<string, string> MessageSet = new Dictionary<string, string>();
        DataTable dtForchk = new DataTable();
        string parent_node_name = string.Empty;
        string current_node_name = string.Empty;

        public SmartViceMenuForm()
        {
            InitializeComponent();
        }

        private void SmartViceMenuForm_Load(object sender, EventArgs e)
        {
            eventslist(); //이벤트 처리 함수
            SetTreeviewData();
        }

        private void eventslist()
        {
            this.JetTreeMenu.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.JetTreeMenu_AfterSelect);            
        }

        public void SetTreeviewData()
        {
            JetTreeMenu.Nodes.Add("조직 관리", "조직 관리");
            JetTreeMenu.Nodes["조직 관리"].NodeFont = new Font(JetTreeMenu.Font, FontStyle.Bold);
            JetTreeMenu.Nodes["조직 관리"].Nodes.Add("직원정보 관리", "직원정보 관리");
            JetTreeMenu.Nodes["조직 관리"].Nodes.Add("직책 관리", "직책 관리");
            JetTreeMenu.Nodes["조직 관리"].Nodes.Add("부서 관리", "부서 관리");
            JetTreeMenu.Nodes["조직 관리"].Nodes.Add("휴가 관리", "휴가 관리");
            JetTreeMenu.Nodes["조직 관리"].Nodes.Add("급여 관리", "급여 관리");
            JetTreeMenu.Nodes["조직 관리"].Nodes.Add("급여명세서", "급여명세서");
            JetTreeMenu.Nodes["조직 관리"].Nodes.Add("급여자동이체 내역", "급여자동이체 내역");

            JetTreeMenu.Nodes.Add("매출·입 관리", "매출·입 관리");
            JetTreeMenu.Nodes["매출·입 관리"].NodeFont = new Font(JetTreeMenu.Font, FontStyle.Bold);
            JetTreeMenu.Nodes["매출·입 관리"].Nodes.Add("지출(비용) 관리", "지출(비용) 관리");
            JetTreeMenu.Nodes["매출·입 관리"].Nodes.Add("세금계산서 발행", "세금계산서 발행(준비중)");
            JetTreeMenu.Nodes["매출·입 관리"].Nodes["세금계산서 발행"].ForeColor = Color.LightSteelBlue;
            JetTreeMenu.Nodes["매출·입 관리"].Nodes.Add("세금계산서 발행 메일발송", "세금계산서 발행 메일발송(준비중)");
            JetTreeMenu.Nodes["매출·입 관리"].Nodes["세금계산서 발행 메일발송"].ForeColor = Color.LightSteelBlue;

            JetTreeMenu.Nodes.Add("사업 관리", "사업 관리");
            JetTreeMenu.Nodes["사업 관리"].NodeFont = new Font(JetTreeMenu.Font, FontStyle.Bold);
            JetTreeMenu.Nodes["사업 관리"].Nodes.Add("거래처 관리", "거래처 관리");
            JetTreeMenu.Nodes["사업 관리"].Nodes.Add("미수금 관리", "미수금 관리(준비중)");
            JetTreeMenu.Nodes["사업 관리"].Nodes["미수금 관리"].ForeColor = Color.LightSteelBlue;
            JetTreeMenu.Nodes["사업 관리"].Nodes.Add("프로젝트 관리", "프로젝트 관리(준비중)");
            JetTreeMenu.Nodes["사업 관리"].Nodes["프로젝트 관리"].ForeColor = Color.LightSteelBlue;


            JetTreeMenu.Nodes.Add("영수증 관리", "영수증 관리");
            JetTreeMenu.Nodes["영수증 관리"].NodeFont = new Font(JetTreeMenu.Font, FontStyle.Bold);
            JetTreeMenu.Nodes["영수증 관리"].Nodes.Add("중식대장 관리","중식대장 관리");
            JetTreeMenu.Nodes["영수증 관리"].Nodes.Add("각종 법인 서류보관", "각종 법인 서류보관(준비중)");
            JetTreeMenu.Nodes["영수증 관리"].Nodes["각종 법인 서류보관"].ForeColor = Color.LightSteelBlue;
            JetTreeMenu.Nodes["영수증 관리"].Nodes.Add("법인 서류 작성", "법인 서류 작성(준비중)");
            JetTreeMenu.Nodes["영수증 관리"].Nodes["법인 서류 작성"].ForeColor = Color.LightSteelBlue;

            JetTreeMenu.Nodes.Add("비품 관리", "비품 관리");
            JetTreeMenu.Nodes["비품 관리"].NodeFont = new Font(JetTreeMenu.Font, FontStyle.Bold);
            JetTreeMenu.Nodes["비품 관리"].Nodes.Add("자산성 비품 관리", "자산성 비품 관리(준비중)");
            JetTreeMenu.Nodes["비품 관리"].Nodes["자산성 비품 관리"].ForeColor = Color.LightSteelBlue;
            JetTreeMenu.Nodes["비품 관리"].Nodes.Add("도서 관리","도서 관리");

            JetTreeMenu.Nodes.Add("스케줄 관리", "스케줄 관리(준비중)");
            JetTreeMenu.Nodes["스케줄 관리"].NodeFont = new Font(JetTreeMenu.Font, FontStyle.Bold);
            JetTreeMenu.Nodes["스케줄 관리"].ForeColor = Color.LightSteelBlue;
            JetTreeMenu.Nodes["스케줄 관리"].Nodes.Add("일정 관리", "일정 관리");
            JetTreeMenu.Nodes["스케줄 관리"].Nodes["일정 관리"].ForeColor = Color.LightSteelBlue;
            JetTreeMenu.Nodes["스케줄 관리"].Nodes.Add("알람 서비스", "알람 서비스");
            JetTreeMenu.Nodes["스케줄 관리"].Nodes["알람 서비스"].ForeColor = Color.LightSteelBlue;
            JetTreeMenu.Nodes["스케줄 관리"].Nodes.Add("회사메일 연동", "회사메일 연동");
            JetTreeMenu.Nodes["스케줄 관리"].Nodes["회사메일 연동"].ForeColor = Color.LightSteelBlue;

            JetTreeMenu.ExpandAll();
            JetTreeMenu.Nodes["스케줄 관리"].Collapse();
        }
   

        private void JetTreeMenu_AfterSelect(object sender, TreeViewEventArgs e)
        {
            this.mainPanel.Controls.Clear();

            //if (parent_node_name != string.Empty && current_node_name != string.Empty)
            //{
            //    JetTreeMenu.Nodes[parent_node_name].Nodes[current_node_name].BackColor = JetTreeMenu.BackColor;
            //    JetTreeMenu.Nodes[parent_node_name].Nodes[current_node_name].NodeFont = new Font(JetTreeMenu.Font, FontStyle.Regular);
            //}

            switch (JetTreeMenu.SelectedNode.Name)
            {
                case "직원정보 관리":
                    StaffInfoManagement staffInfoManagement = new StaffInfoManagement();
                    staffInfoManagement.TopLevel = false;
                    staffInfoManagement.FormBorderStyle = FormBorderStyle.None;

                    this.mainPanel.Controls.Add(staffInfoManagement);
                    lbFormNm.Text = staffInfoManagement.Text;
                    staffInfoManagement.Dock = DockStyle.Fill;
                    staffInfoManagement.Show();

                    break;
                case "직책 관리":
                    JobPositionManagement jobPositionManagement = new JobPositionManagement();
                    jobPositionManagement.TopLevel = false;
                    jobPositionManagement.FormBorderStyle = FormBorderStyle.None;

                    this.mainPanel.Controls.Add(jobPositionManagement);
                    lbFormNm.Text = jobPositionManagement.Text;
                    jobPositionManagement.Dock = DockStyle.Fill;
                    jobPositionManagement.Show();

                    break;
                case "부서 관리":
                    DepartmentManagement departmentManagement = new DepartmentManagement();
                    departmentManagement.TopLevel = false;
                    departmentManagement.FormBorderStyle = FormBorderStyle.None;

                    this.mainPanel.Controls.Add(departmentManagement);
                    lbFormNm.Text = departmentManagement.Text;
                    departmentManagement.Dock = DockStyle.Fill;
                    departmentManagement.Show();

                    break;
                case "휴가 관리":
                    VacationManagement vacationManagement = new VacationManagement();
                    vacationManagement.TopLevel = false;
                    vacationManagement.FormBorderStyle = FormBorderStyle.None;

                    this.mainPanel.Controls.Add(vacationManagement);
                    lbFormNm.Text = vacationManagement.Text;
                    vacationManagement.Dock = DockStyle.Fill;
                    vacationManagement.Show();

                    break;
                case "급여 관리":
                    StaffSalaryManagement staffSalaryManagement = new StaffSalaryManagement();
                    staffSalaryManagement.TopLevel = false;
                    staffSalaryManagement.FormBorderStyle = FormBorderStyle.None;

                    this.mainPanel.Controls.Add(staffSalaryManagement);
                    lbFormNm.Text = staffSalaryManagement.Text;
                    staffSalaryManagement.Dock = DockStyle.Fill;
                    staffSalaryManagement.Show();

                    break;
                case "급여명세서":
                    DetailedSalaryManagement detailedSalaryManagement = new DetailedSalaryManagement();
                    detailedSalaryManagement.TopLevel = false;
                    detailedSalaryManagement.FormBorderStyle = FormBorderStyle.None;

                    this.mainPanel.Controls.Add(detailedSalaryManagement);
                    lbFormNm.Text = detailedSalaryManagement.Text;
                    detailedSalaryManagement.Dock = DockStyle.Fill;
                    detailedSalaryManagement.Show();

                    break;
                case "급여자동이체 내역":
                    SalaryPAPManagement salaryPAPManagement = new SalaryPAPManagement();
                    salaryPAPManagement.TopLevel = false;
                    salaryPAPManagement.FormBorderStyle = FormBorderStyle.None;

                    this.mainPanel.Controls.Add(salaryPAPManagement);
                    lbFormNm.Text = salaryPAPManagement.Text;
                    salaryPAPManagement.Dock = DockStyle.Fill;
                    salaryPAPManagement.Show();

                    break;
                case "지출(비용) 관리":
                    SpendingManagement spendingManagement = new SpendingManagement();
                    spendingManagement.TopLevel = false;
                    spendingManagement.FormBorderStyle = FormBorderStyle.None;

                    this.mainPanel.Controls.Add(spendingManagement);
                    lbFormNm.Text = spendingManagement.Text;
                    spendingManagement.Dock = DockStyle.Fill;
                    spendingManagement.Show();

                    break;
                case "세금계산서 발행":
                    MessageBox.Show("화면을 준비 중입니다.");

                    return;
                case "세금계산서 발행 메일발송":
                    MessageBox.Show("화면을 준비 중입니다.");

                    return;
                case "거래처 관리":
                    CustomerManagement customerManagement = new CustomerManagement();
                    customerManagement.TopLevel = false;
                    customerManagement.FormBorderStyle = FormBorderStyle.None;

                    this.mainPanel.Controls.Add(customerManagement);
                    lbFormNm.Text = customerManagement.Text;
                    customerManagement.Dock = DockStyle.Fill;
                    customerManagement.Show();

                    break;
                case "미수금 관리":
                    MessageBox.Show("화면을 준비 중입니다.");

                    return;
                case "프로젝트 관리;":
                    MessageBox.Show("화면을 준비 중입니다.");

                    return;
                case "중식대장 관리":
                    ChargeFoodManagement chargeFoodManagement = new ChargeFoodManagement();
                    chargeFoodManagement.TopLevel = false;
                    chargeFoodManagement.FormBorderStyle = FormBorderStyle.None;

                    this.mainPanel.Controls.Add(chargeFoodManagement);
                    lbFormNm.Text = chargeFoodManagement.Text;
                    chargeFoodManagement.Dock = DockStyle.Fill;
                    chargeFoodManagement.Show();

                    break;
                case "각종 법인 서류보관":
                    MessageBox.Show("화면을 준비 중입니다.");

                    return;
                case "법인 서류 작성":
                    MessageBox.Show("화면을 준비 중입니다.");

                    return;
                case "자산성 비품 관리":
                    MessageBox.Show("화면을 준비 중입니다.");

                    return;
                case "도서 관리":
                    BookManagement bookManagement = new BookManagement();
                    bookManagement.TopLevel = false;
                    bookManagement.FormBorderStyle = FormBorderStyle.None;

                    this.mainPanel.Controls.Add(bookManagement);
                    lbFormNm.Text = bookManagement.Text;
                    bookManagement.Dock = DockStyle.Fill;
                    bookManagement.Show();

                    break;
                case "일정 관리":
                    MessageBox.Show("화면을 준비 중입니다.");

                    return;
                case "알람 서비스":
                    MessageBox.Show("화면을 준비 중입니다.");

                    return;
                case "회사메일 연동":
                    MessageBox.Show("화면을 준비 중입니다.");

                    return;
            }
            
            //if (JetTreeMenu.SelectedNode.Parent != null)
            //{
            //    JetTreeMenu.SelectedNode.BackColor = Color.White;
            //    JetTreeMenu.SelectedNode.NodeFont = new Font(JetTreeMenu.Font, FontStyle.Bold);

            //    parent_node_name = JetTreeMenu.SelectedNode.Parent.Name;
            //    current_node_name = JetTreeMenu.SelectedNode.Name;
            //}
        }
    }
}
