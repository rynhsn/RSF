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

    }
}
