using BlazorClientHelper;
using Microsoft.AspNetCore.Components;
using PMR02400COMMON.DTO_s;
using PMR02400MODEL;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_BlazorFrontEnd.Interfaces;
using PMR02400FrontResources;
using PMR02400COMMON;
using R_BlazorFrontEnd.Controls.Validation;

namespace PMR02400FRONT
{
    public partial class SaveAsPopup : R_Page
    {
        private PMR02400ViewModel _viewModel = new();
        [Inject] private IClientHelper _clientHelper { get; set; }
        [Inject] private R_IReport _reportService { get; set; }
        [Inject] private R_ILocalizer<Resources_Dummy_Class> _localizer { get; set; }


        protected override async Task R_Init_From_Master(object poParameter)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                _viewModel._ReportParam = R_FrontUtility.ConvertObjectToObject<PMR02400ParamDTO>(poParameter);
                _viewModel._ReportParam.CREPORT_FILENAME = "";
                _viewModel._ReportParam.CREPORT_FILETYPE = _viewModel._checkBoxReportExtensionList[0].CTYPE_NAME;
                _viewModel._ReportParam.LIS_PRINT = false;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);
            await Task.CompletedTask;
        }

        private async Task Btn_SaveAsAsync()
        {
            R_Exception loEx = new R_Exception();
            try
            {
                string lcPostEndpoint = "";
                string lcGetReprotEndpoint = "";

                //route report
                switch (_viewModel._ReportParam.CREPORT_TYPE)
                {
                    case "S":
                        lcPostEndpoint = "rpt/PMR02410Print/DownloadResultPrintPost";
                        lcGetReprotEndpoint = "rpt/PMR02410Print/PenaltySummary_ReportListGet";
                        break;
                    case "D":
                        lcPostEndpoint = "rpt/PMR02420Print/DownloadResultPrintPost";
                        lcGetReprotEndpoint = "rpt/PMR02420Print/PenaltyDetail_ReportListGet";
                        break;
                }
                await GenerateReport(lcPostEndpoint, lcGetReprotEndpoint, _viewModel._ReportParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
            await Task.CompletedTask;
        }

        private async Task GenerateReport(string pcPostUrl, string pcGetUrl, PMR02400ParamDTO poParam)
        {
            var loEx = new R_Exception();

            try
            {
                await _reportService.GetReport(
                    PMR02400ContextConstant.DEFAULT_HTTP_NAME,
                    PMR02400ContextConstant.DEFAULT_MODULE,
                    pcPostUrl,
                    pcGetUrl,
                    poParam
                    );
                await Close(true, null);
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
