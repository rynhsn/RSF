using CBR00600COMMON;
using Microsoft.AspNetCore.Components;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Interfaces;

namespace CBR00600FRONT
{
    public partial class CBR00601 : R_Page
    {
        [Inject] private R_ILocalizer<CBR00600FrontResources.Resources_Dummy_Class> _localizer { get; set; }
        public List<string> FileType { get; } = new List<string> { "XLSX", "XLS", "CSV" };

        public CBR00600DTO SaveAsParam { get; set; } = new CBR00600DTO();

        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();

            try
            {
                SaveAsParam.CREPORT_FILETYPE = "";
                SaveAsParam.CREPORT_FILENAME = "";
                await Task.CompletedTask;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        private async Task OnClickOk()
        {
            R_Exception loEx = new R_Exception();
            try
            {
                await Close(true, SaveAsParam);
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
