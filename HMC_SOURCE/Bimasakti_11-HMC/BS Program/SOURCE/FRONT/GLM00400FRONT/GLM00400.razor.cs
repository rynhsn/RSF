using BlazorClientHelper;
using GLM00400COMMON;
using GLM00400MODEL;
using Microsoft.AspNetCore.Components;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.Grid;
using R_BlazorFrontEnd.Exceptions;

namespace GLM00400FRONT
{
    public partial class GLM00400 : R_Page
    {
        private GLM00400ViewModel _AllocationJournalHD_viewModel = new GLM00400ViewModel();
        private R_Grid<GLM00400DTO> _AllocationJournalHD_gridRef;
        private R_Conductor _conductorRef;
        [Inject] IClientHelper clientHelper { get; set; }

        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();

            try
            {
                await AllocationJournal_InitialVar_ServiceGetListRecord(null);
                await AllocationJournal_SystemParam_ServiceGetListRecord(null);
                await _AllocationJournalHD_gridRef.R_RefreshGrid(null);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        private async Task AllocationJournal_InitialVar_ServiceGetListRecord(object eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = new GLM00400InitialDTO() { CUSER_LANGUAGE = clientHelper.CultureUI.TwoLetterISOLanguageName };
                await _AllocationJournalHD_viewModel.GetInitialVar(loParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        private async Task AllocationJournal_SystemParam_ServiceGetListRecord(object eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = new GLM00400GLSystemParamDTO() { CUSER_LANGUAGE = clientHelper.CultureUI.TwoLetterISOLanguageName };
                await _AllocationJournalHD_viewModel.GetSystemParam(loParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }


        private async Task AllocationJournal_HD_ServiceGetListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = new GLM00400DTO() { CUSER_LANGUAGE = clientHelper.CultureUI.TwoLetterISOLanguageName };
                await _AllocationJournalHD_viewModel.GetAllocationJournalHDList(loParam);

                eventArgs.ListEntityResult = _AllocationJournalHD_viewModel.AllocationJournalHDGrid;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private void Allocation_ServiceGetRecord(R_ServiceGetRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                eventArgs.Result = eventArgs.Data;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }
        public async Task ResfreshGrid_OnClickOkAsync()
        {
            var loEx = new R_Exception();

            try
            {
                await _AllocationJournalHD_gridRef.R_RefreshGrid(null);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        private void AllocationEntry_InstantiateDock(R_InstantiateDockEventArgs eventArgs)
        {
            var loData = (GLM00400DTO)_conductorRef.R_GetCurrentData();
          
            eventArgs.Parameter = loData;
            eventArgs.TargetPageType = typeof(GLM00410);
        }

        private void R_RowRender(R_GridRowRenderEventArgs eventArgs)
        {
            var loData = (GLM00400DTO)eventArgs.Data;

            if (!loData.LALLOW_EDIT)
            {
                eventArgs.RowStyle = new R_GridRowRenderStyle
                {
                    BackgroundColor = "yellow",
                    BackgroundColorHover = "yellow",
                };
            }
        }

        private async Task AllocationEntry_AfterOpenPredefinedDock(R_AfterOpenPredefinedDockEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await _AllocationJournalHD_gridRef.R_RefreshGrid(null);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        private void Allocation_R_Before_Open_Popup_Print(R_BeforeOpenPopupEventArgs eventArgs)
        {
            var loParam = (GLM00400DTO)_AllocationJournalHD_gridRef.GetCurrentData();
            loParam.IMAX_YEAR = _AllocationJournalHD_viewModel.InitialVar.IMAX_YEAR;
            loParam.IMIN_YEAR = _AllocationJournalHD_viewModel.InitialVar.IMIN_YEAR;
            loParam.CSOFT_PERIOD_YY = _AllocationJournalHD_viewModel.SystemParam.CSOFT_PERIOD_YY;

            eventArgs.Parameter = loParam;
            eventArgs.TargetPageType = typeof(GLM00401);
        }
    }
}
