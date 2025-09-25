using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Exceptions;
using Microsoft.AspNetCore.Components;
using PMR03100FrontResources;
using R_BlazorFrontEnd.Interfaces;

namespace PMR03100FRONT
{
    public partial class PMR03101 : R_Page
    {
        [Inject] private R_ILocalizer<Resources_Dummy_Class> _localizer { get; set; }

        private string _reportType, _reportName = "";

        private List<string> _listFileType = new List<string> { "XLSX", "XLS", "CSV" };

        protected override Task R_Init_From_Master(object poParameter)
        {
            return base.R_Init_From_Master(poParameter);
        }

        private async Task OnClickOk()
        {
            R_Exception loEx = new R_Exception();
            try
            {
                await this.Close(true, _reportName + "," + _reportType);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        private async Task OnClickCancel()
        {
            R_Exception loEx = new R_Exception();
            try
            {
                await Close(false, null);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }
    }
}
