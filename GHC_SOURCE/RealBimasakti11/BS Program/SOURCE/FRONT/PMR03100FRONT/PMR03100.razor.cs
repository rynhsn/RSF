using PMR03100MODEL.View_Model_s;
using BlazorClientHelper;
using Microsoft.AspNetCore.Components;
using PMR03100FrontResources;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Interfaces;
using PMR03100COMMON.DTO_s.Print;
using R_BlazorFrontEnd.Controls.Events;

namespace PMR03100FRONT
{
    public partial class PMR03100 : R_Page
    {
        private PMR03100ViewModel _viewModel = new();

        [Inject] IClientHelper _clientHelper { get; set; }

        [Inject] private R_ILocalizer<Resources_Dummy_Class> _localizer { get; set; }

        [Inject] private R_IReport _reportService { get; set; }

        protected override async Task R_Init_From_Master(object poParameter)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                await _viewModel.InitialProcessAsync();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            R_DisplayException(loEx);
        }

        private async Task BtnPrint_OnclickAsync()
        {
            R_Exception loEx = new R_Exception();
            try
            {
                _viewModel.Validation();
                var loParam = _viewModel.GenerateParam();
                await Generate_Report(loParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        private async Task Generate_Report(ParamDTO poParam)
        {

            R_Exception loEx = new R_Exception();
            try
            {
                await _reportService.GetReport(
                    "R_DefaultServiceUrlPM",
                    "PM",
                    "rpt/PMR03101Print/DownloadResultPrintPost",
                    "rpt/PMR03101Print/GetFileStream_ARCollection",
                    poParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        #region Popup save as

        private void BeforeOpen_PopupSaveAsAsync(R_BeforeOpenPopupEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                _viewModel.Validation();
                eventArgs.PageTitle = _localizer["_title_saveas"];
                eventArgs.TargetPageType = typeof(PMR03101);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        private async Task AfterOpen_PopupSaveAsAsync(R_AfterOpenPopupEventArgs eventArgs)
        {
            R_Exception loEx = new R_Exception();
            try
            {
                string lsReportType = (string)eventArgs.Result;
                if (!string.IsNullOrWhiteSpace(lsReportType))
                {
                    string[] lcResultSaveAs = lsReportType.Split(',');
                    var loPrintParam = _viewModel.GenerateParam();
                    loPrintParam.CREPORT_FILENAME = lcResultSaveAs[0];
                    loPrintParam.CREPORT_FILEEXT = lcResultSaveAs[1];
                    await Generate_Report(loPrintParam);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }
            loEx.ThrowExceptionIfErrors();
        }

        #endregion
    }
}
