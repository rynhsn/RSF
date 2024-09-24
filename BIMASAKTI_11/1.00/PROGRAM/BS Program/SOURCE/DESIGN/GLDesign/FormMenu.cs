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
    }
}
