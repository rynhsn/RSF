using FastReport;
using System.Collections;

namespace APDesign;

public partial class FormMenu : Form
{

    private Report loReport;
    public FormMenu()
    {
        InitializeComponent();
    }
    private void FormMenu_Load(object sender, EventArgs e)
    {
        loReport = new Report();
    }

    private void APR00500_Button_Click(object sender, EventArgs e)
    {
        var loData = new ArrayList();
        loData.Add(APR00500Common.Model.APR00500ModelReportDummyData.DefaultDataWithHeader());
        loReport.RegisterData(loData, "ResponseDataModel");
        loReport.Design();
    }

    private void APR00300_Button_Click(object sender, EventArgs e)
    {
        var loData = new ArrayList();
        loData.Add(APR00300Common.Model.APR00300ModelReportDummyData.DefaultDataWithHeader());
        loReport.RegisterData(loData, "ResponseDataModel");
        loReport.Design();
    }

    private void APR00700_Click(object sender, EventArgs e)
    {
        var loData = new ArrayList();
        loData.Add(APR00700COMMON.Model.APR00700DummyData.APR00700PrintDislpayWithBaseHeader());
    }
    private void BTN_APR00600_Click(object sender, EventArgs e)
    {
        var loData = new ArrayList();
        loData.Add(APR00600COMMON.Model.APR00600DummyData.DefaultDataWithHeader());
        loReport.RegisterData(loData, "ResponseDataModel");
        loReport.Design();
    }
}