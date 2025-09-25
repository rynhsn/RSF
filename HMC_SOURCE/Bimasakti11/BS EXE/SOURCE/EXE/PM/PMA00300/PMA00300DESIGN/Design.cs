using FastReport;
using System.Collections;

namespace PMA00300DESIGN
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

        private void buttonPMA00300_Click(object sender, EventArgs e)
        {
            ArrayList loData = new ArrayList
            {
                PMA00300COMMON.Model.GenarateDataModel.DefaultDataWithHeader()
            };
            _loReport.RegisterData(loData, "ResponseDataModel");
            _loReport.Design();
        }
    }
}