using FastReport;
using System.Collections;
namespace DesignFormGL
{
    public partial class GLR00300DesignReport : Form
    {
        private Report _loReport;
        public GLR00300DesignReport()
        {
            InitializeComponent();
        }
        private void GLDesainReport_Load(object sender, EventArgs e)
        {
            _loReport = new Report();
        }
        private void ButtonFormatA_D_Click(object sender, EventArgs e)
        {
            ArrayList loData = new ArrayList
            {
                GLR00300Common.Model.GenerateDataModel.DefaultDataWithHeaderFormat_AtoD()
            };
            _loReport.RegisterData(loData, "ResponseDataModel");
            _loReport.Design();
        }

        private void ButtonFormatE_H_Click(object sender, EventArgs e)
        {
            ArrayList loData = new ArrayList
            {
                GLR00300Common.Model.GenerateDataModel.DefaultDataWithHeaderFormat_EtoH()
            };
            _loReport.RegisterData(loData, "ResponseDataModel");
            _loReport.Design();
        }

        private void PMR0150Summary_Click(object sender, EventArgs e)
        {
            ArrayList loData = new ArrayList
            {
                PMR00150COMMON.GenerateDataModel.DefaultDataSummaryWithHeader()
            };
            _loReport.RegisterData(loData, "ResponseDataModel");
            _loReport.Design();
        }

        private void PMR00150Detail_Click(object sender, EventArgs e)
        {
            ArrayList loData = new ArrayList
            {
                PMR00150COMMON.GenerateDataModel.DefaultDataDetailWithHeader()
            };
            _loReport.RegisterData(loData, "ResponseDataModel");
            _loReport.Design();
        }

        private void PMB04000_Click(object sender, EventArgs e)
        {
            ArrayList loData = new ArrayList
            {
                PMB04000COMMON.Print.Model.GenarateDataModel.DefaultDataWithHeader()
            };
            _loReport.RegisterData(loData, "ResponseDataModel");
            _loReport.Design();
        }

        private void PMT01700_Click(object sender, EventArgs e)
        {
            ArrayList loData = new ArrayList
            {
                PMT01700COMMON.Models.GenerateDataModel.DefaultDataWithHeader()
            };
            _loReport.RegisterData(loData, "ResponseDataModel");
            _loReport.Design();
        }

        private void Dummy_Data_report(object sender, EventArgs e)
        {
            ArrayList loData = new ArrayList
            {
                PMR00150COMMON.GenerateDataModel.DefaultDataSummaryWithHeader()
            };
            _loReport.RegisterData(loData, "ResponseDataModel");
            _loReport.Design();
        }
    }


}