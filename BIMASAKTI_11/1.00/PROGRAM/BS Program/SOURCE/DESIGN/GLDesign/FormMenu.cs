using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FastReport;

namespace GLDesign
{
    public partial class FormMenu : Form
    {
        private Report loReport;

        public FormMenu()
        {
            InitializeComponent();
        }


        private void APR00300_Click(object sender, EventArgs e)
        {
            var loData = new ArrayList();
            loData.Add(APR00300Common.Model.APR00300ModelReportDummyData.DefaultDataWithHeader());
            loReport.RegisterData(loData, "ResponseDataModel");
            loReport.Design();
        }

        private void APR00500_Click(object sender, EventArgs e)
        {
            var loData = new ArrayList();
            loData.Add(APR00500Common.Model.APR00500ModelReportDummyData.DefaultDataWithHeader());
            loReport.RegisterData(loData, "ResponseDataModel");
            loReport.Design();
        }

        private void GLI00100_Click(object sender, EventArgs e)
        {
            var loData = new ArrayList();
            loData.Add(GLI00100Common.Model.GLI00100ModelPrintDummyData.DefaultDataWithHeader());
            loReport.RegisterData(loData, "ResponseDataModel");
            loReport.Design();
        }

        private void FormMenu_Load(object sender, EventArgs e)
        {
            loReport = new Report();
        }

        private void BaseHeaderLandscape_Click(object sender, EventArgs e)
        {
            var loData = new ArrayList();
            loData.Add(BaseHeaderReportCOMMON.Models.GenerateDataModelHeader.DefaultData());
            loReport.RegisterData(loData, "ResponseDataModel");
            loReport.Design();
        }

        private void BaseHeader_Click(object sender, EventArgs e)
        {
            var loData = new ArrayList();
            loData.Add(BaseHeaderReportCOMMON.Models.GenerateDataModelHeader.DefaultData());
            loReport.RegisterData(loData, "ResponseDataModel");
            loReport.Design();
        }

        private void GLR00100_3_Click(object sender, EventArgs e)
        {
            var loData = new ArrayList();
            loData.Add(GLR00100Common.Model.GLR00100ModelReportDummyData.DefaultDataWithHeaderDate());
            loReport.RegisterData(loData, "ResponseDataModel");
            loReport.Design();
        }

        private void GLR00100_2_Click(object sender, EventArgs e)
        {
            var loData = new ArrayList();
            loData.Add(GLR00100Common.Model.GLR00100ModelReportDummyData.DefaultDataWithHeaderRefNo());
            loReport.RegisterData(loData, "ResponseDataModel");
            loReport.Design();
        }

        private void GLR00100_1_Click(object sender, EventArgs e)
        {
            var loData = new ArrayList();
            loData.Add(GLR00100Common.Model.GLR00100ModelReportDummyData.DefaultDataWithHeaderTransCode());
            loReport.RegisterData(loData, "ResponseDataModel");
            loReport.Design();
        }

        private void PMR00400_Click(object sender, EventArgs e)
        {
            var loData = new ArrayList();
            loData.Add(PMR00400Common.Model.PMR00400ModelReportPrintDummyData.DefaultDataWithHeader());
            loReport.RegisterData(loData, "ResponseDataModel");
            loReport.Design();
        }

        private void PMR02600_Click(object sender, EventArgs e)
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

        private void HDR00200_Click(object sender, EventArgs e)
        {
            var loData = new ArrayList();
            loData.Add(HDR00200Common.Model.HDR00200ModelReportDummyData.DefaultDataWithHeader());
            loReport.RegisterData(loData, "ResponseDataModel");
            loReport.Design();
        }

        private void PMT01300_Click(object sender, EventArgs e)
        {
            var loData = new ArrayList();
            loData.Add(PMT01300COMMON.Models.PMT01300ModelDummyDataReportServer.DefaultDataWithHeader());
            loReport.RegisterData(loData, "ResponseDataModel");
            loReport.Design();
        }

        private void PMB04000_Click(object sender, EventArgs e)
        {
            var loData = new ArrayList();
            loData.Add(PMB04000COMMON.Print.Model.GenarateDataModel.DefaultDataWithHeader());
            loReport.RegisterData(loData, "ResponseDataModel");
            loReport.Design();
        }

        private void APR00100DetailByDate_Click(object sender, EventArgs e)
        {
            var loData = new ArrayList();
            loData.Add(APR00100COMMON.Model.DetailByDateDummyData.Print());
            loReport.RegisterData(loData, "ResponseDataModel");
            loReport.Design();
        }

        private void APR00100SummaryByDate_Click(object sender, EventArgs e)
        {
            var loData = new ArrayList();
            loData.Add(APR00100COMMON.Model.SummaryDummyByDateData.Print());
            loReport.RegisterData(loData, "ResponseDataModel");
            loReport.Design();
        }

        private void APR00100DetailBySupplier_Click(object sender, EventArgs e)
        {
            var loData = new ArrayList();
            loData.Add(APR00100COMMON.Model.DetailDummyData.Print());
            loReport.RegisterData(loData, "ResponseDataModel");
            loReport.Design();
        }

        private void APR00100SummaryBySupplier_Click(object sender, EventArgs e)
        {
            var loData = new ArrayList();
            loData.Add(APR00100COMMON.Model.SummaryDummyData.Print());
            loReport.RegisterData(loData, "ResponseDataModel");
            loReport.Design();
        }

        private void ICR00600_Click(object sender, EventArgs e)
        {
            var loData = new ArrayList();
            loData.Add(ICR00600Common.Model.ICR00600ModelReportDummyData.DefaultDataWithHeader());
            loReport.RegisterData(loData, "ResponseDataModel");
            loReport.Design();
        }

        private void ICR00100_Click(object sender, EventArgs e)
        {
            var loData = new ArrayList();
            loData.Add(ICR00100Common.Model.ICR00100ModelReportDummyData.DefaultDataWithHeader());
            loReport.RegisterData(loData, "ResponseDataModel");
            loReport.Design();
        }

        private void GLM00200_Click(object sender, EventArgs e)
        {
            var loData = new ArrayList();
            loData.Add(GLM00200COMMON.Models.GenerateDataModelGLR00200.DefaultDataWithHeader());
            loReport.RegisterData(loData, "ResponseDataModel");
            loReport.Design();
        }

        private void PMR02000Summary_Click(object sender, EventArgs e)
        {
            var loData = new ArrayList();
            loData.Add(PMR02000COMMON.Model.SummaryDummyData.GenerateDummyData());
            loReport.RegisterData(loData, "ResponseDataModel");
            loReport.Design();
        }

        private void PMR02000Detail_Click(object sender, EventArgs e)
        {
            var loData = new ArrayList();
            loData.Add(PMR02000COMMON.Model.DetailDummyData.GenerateDummyData());
            loReport.RegisterData(loData, "ResponseDataModel");
            loReport.Design();
        }

        private void PMR03000_Click(object sender, EventArgs e)
        {
            var loData = new ArrayList();
            loData.Add(PMR03000Common.Model.GenerateDataModel.DefaultDataWithHeader());
            loReport.RegisterData(loData, "ResponseDataModel");
            loReport.Design();
        }

        private void GSM04000_Click(object sender, EventArgs e)
        {
            var loData = new ArrayList();
            loData.Add(GSM04000Common.Model.GenerateDataModel.DefaultDataWithHeader());
            loReport.RegisterData(loData, "ResponseDataModel");
            loReport.Design();
        }

        private void APR00500_Click_1(object sender, EventArgs e)
        {
            var loData = new ArrayList();
            loData.Add(APR00500Common.Model.APR00500ModelReportDummyData.DefaultDataWithHeader());
            loReport.RegisterData(loData, "ResponseDataModel");
            loReport.Design();
        }

        private void APR00300_Click_1(object sender, EventArgs e)
        {
            var loData = new ArrayList();
            loData.Add(APR00300Common.Model.APR00300ModelReportDummyData.DefaultDataWithHeader());
            loReport.RegisterData(loData, "ResponseDataModel");
            loReport.Design();
        }

        private void PMR00220Summary_Click(object sender, EventArgs e)
        {
            var loData = new ArrayList();
            loData.Add(PMR00220COMMON.Model.PMR00200DummyData.PMR00200PrintDislpayWithBaseHeader());
            loReport.RegisterData(loData, "ResponseDataModel");
            loReport.Design();
        }

        private void PMR00220Detail_Click(object sender, EventArgs e)
        {
            var loData = new ArrayList();
            loData.Add(PMR00220COMMON.Model.PMR00220DummyData.PMR00220PrintDislpayWithBaseHeader());
            loReport.RegisterData(loData, "ResponseDataModel");
            loReport.Design();
        }

        private void PMR00210Summary_Click(object sender, EventArgs e)
        {
            var loData = new ArrayList();
            loData.Add(PMR00210COMMON.Model.PMR00200DummyData.PMR00200PrintDislpayWithBaseHeader());
            loReport.RegisterData(loData, "ResponseDataModel");
            loReport.Design();
        }

        private void PMR00210Detail_Click(object sender, EventArgs e)
        {
            var loData = new ArrayList();
            loData.Add(PMR00210COMMON.Model.PMR00210DummyData.PMR00210PrintDislpayWithBaseHeader());
            loReport.RegisterData(loData, "ResponseDataModel");
            loReport.Design();
        }

        private void PMR00200Summary_Click(object sender, EventArgs e)
        {
            var loData = new ArrayList();
            loData.Add(PMR00200COMMON.Model.PMR00200DummyData.PMR00200PrintDislpayWithBaseHeader());
            loReport.RegisterData(loData, "ResponseDataModel");
            loReport.Design();
        }

        private void PMR00200Detail_Click(object sender, EventArgs e)
        {
            var loData = new ArrayList();
            loData.Add(PMR00200COMMON.Model.PMR00210DummyData.PMR00210PrintDislpayWithBaseHeader());
            loReport.RegisterData(loData, "ResponseDataModel");
            loReport.Design();
        }
    }
}