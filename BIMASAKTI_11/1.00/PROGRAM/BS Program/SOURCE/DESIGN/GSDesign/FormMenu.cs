﻿using System;
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

namespace GSDesign
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
    }
}