
using System.Collections;
using FastReport;

namespace WinFormsApp1
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

        private void PMR00100_Click(object sender, EventArgs e)
        {
            var loData = new ArrayList();
            loData.Add(GLI00100Common.Model.GLI00100ModelPrintDummyData.DefaultDataWithHeader());
            loReport.RegisterData(loData, "ResponseDataModel");
            loReport.Design();
        }
    }
}