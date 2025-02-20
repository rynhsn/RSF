using FastReport;
using System.Collections;

namespace PMDesign
{
    public partial class Form1 : Form
    {
        private Report loReport;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            loReport = new Report();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var loData = new ArrayList();
            loData.Add(PMR00400Common.Model.PMR00400ModelReportPrintDummyData.DefaultDataWithHeader());
            loReport.RegisterData(loData, "ResponseDataModel");
            loReport.Design();
        }

        private void PMR02600Button_Click(object sender, EventArgs e)
        {
            var loData = new ArrayList();
            loData.Add(PMR02600Common.Model.PMR02600ModelReportDummyData.DefaultDataWithHeader());
            loReport.RegisterData(loData, "ResponseDataModel");
            loReport.Design();
        }

        private void PMR00460_Click(object sender, EventArgs e)
        {
            var loData = new ArrayList();
            loData.Add(PMR00460Common.Model.PMR00460ModelReportDummyData.DefaultDataWithHeader());
            loReport.RegisterData(loData, "ResponseDataModel");
            loReport.Design();
        }
    }
}