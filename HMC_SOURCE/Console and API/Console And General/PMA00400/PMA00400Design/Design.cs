using FastReport;
using System.Collections;

namespace PMA00400Design
{
    public partial class Design : Form
    {
        private Report _loReport;
        public Design()
        {
            InitializeComponent();
        }

        private void Design_Load(object sender, EventArgs e)
        {
            _loReport = new Report();
        }

        private void PMA00400_Click(object sender, EventArgs e)
        {
            ArrayList loData = new ArrayList
            {
                PMA00400Common.Models.GenerateDataModel.DefaultDataWithHeader()
            };
            _loReport.RegisterData(loData, "ResponseDataModel");
            _loReport.Design();
        }
    }
}