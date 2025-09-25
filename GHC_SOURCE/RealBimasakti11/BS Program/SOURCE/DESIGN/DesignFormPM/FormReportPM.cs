using FastReport;
using System.Collections;

namespace DesignFormPM
{
    public partial class FastReportPM : Form
    {
        private Report _report;
        public FastReportPM()
        {
            InitializeComponent();
        }
        private void DesignFormPMR_Load(object sender, EventArgs e)
        {
            _report = new Report();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ArrayList loData = new ArrayList();
            loData.Add(PMR00200COMMON.Model.PMR00200DummyData.PMR00200PrintDislpayWithBaseHeader());
            _report.RegisterData(loData, "ResponseDataModel");
            _report.Design();
        }


        private void button1_Click_1(object sender, EventArgs e)
        {
            ArrayList loData = new ArrayList();
            loData.Add(PMR00600COMMON.DTO_s.Model.PMR00600DummyData.PMR00600PrintDislpayWithBaseHeader());
            _report.RegisterData(loData, "ResponseDataModel");
            _report.Design();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ArrayList loData = new ArrayList();
            loData.Add(PMR00600COMMON.Model.PMR00601DummyData.PMR00601PrintDislpayWithBaseHeader());
            _report.RegisterData(loData, "ResponseDataModel");
            _report.Design();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ArrayList loData = new ArrayList();
            loData.Add(PMR02400COMMON.DTO_s.Model.PMR02400DummyData.PMR02400PrintDislpayWithBaseHeader());
            _report.RegisterData(loData, "ResponseDataModel");
            _report.Design();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ArrayList loData = new ArrayList();
            loData.Add(PMR02400COMMON.DTO_s.Model.PMR02410DummyData.PMR02410PrintDislpayWithBaseHeader());
            _report.RegisterData(loData, "ResponseDataModel");
            _report.Design();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            ArrayList loData = new ArrayList();
            loData.Add(PMR00200COMMON.Model.PMR00210DummyData.PMR00210PrintDislpayWithBaseHeader());
            _report.RegisterData(loData, "ResponseDataModel");
            _report.Design();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            ArrayList loData = new ArrayList();
            loData.Add(PMR02000COMMON.Model.SummaryDummyData.GenerateDummyData());
            _report.RegisterData(loData, "ResponseDataModel");
            _report.Design();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            ArrayList loData = new ArrayList();
            loData.Add(PMR00800COMMON.Model.PMR00800DummyData.PrintDummyData());
            _report.RegisterData(loData, "ResponseDataModel");
            _report.Design();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            ArrayList loData = new ArrayList();
            loData.Add(PMR00170COMMON.GenerateDataModel.DefaultDataDetailWithHeader());
            _report.RegisterData(loData, "ResponseDataModel");
            _report.Design();
        }

        private void button9_Click_1(object sender, EventArgs e)
        {
            ArrayList loData = new ArrayList();
            loData.Add(PMR00170COMMON.GenerateDataModel.DefaultDataDetailWithHeader());
            _report.RegisterData(loData, "ResponseDataModel");
            _report.Design();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            ArrayList loData = new ArrayList();
            loData.Add(PMR00170COMMON.GenerateDataModel.DefaultDataSummaryWithHeader());
            _report.RegisterData(loData, "ResponseDataModel");
            _report.Design();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            ArrayList loData = new ArrayList();
            loData.Add(PMR03100COMMON.Model.GenerateDataModel.GenerateDummyData());
            _report.RegisterData(loData, "ResponseDataModel");
            _report.Design();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            ArrayList loData = new ArrayList();
            loData.Add(PMR02000COMMON.Model.DetailDummyData.GenerateDummyData());
            _report.RegisterData(loData, "ResponseDataModel");
            _report.Design();
        }
    }
}
