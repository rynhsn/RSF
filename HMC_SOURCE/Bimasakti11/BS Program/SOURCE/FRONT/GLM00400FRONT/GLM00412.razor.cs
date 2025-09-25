using BlazorClientHelper;
using GLM00400COMMON;
using GLM00400MODEL;
using Microsoft.AspNetCore.Components;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.Tab;
using R_BlazorFrontEnd.Enums;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;

namespace GLM00400FRONT
{
    public partial class GLM00412 : R_Page, R_ITabPage
    {
        private GLM00412ViewModel _AllocationPeriod_viewModel = new GLM00412ViewModel();
        private R_Grid<GLM00414DTO> _AllocationPeriod_gridRef;
        private R_Grid<GLM00415DTO> _AllocationPeriodCenter_gridRef;
        private R_Conductor _AllocationPeriod_condutorRef;
        private bool _SetHasDataDT;

        [Inject] IClientHelper clientHelper { get; set; }

        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();

            try
            {
                var loTempParam = (GLM00410DTO)poParameter;

                _AllocationPeriod_viewModel.AllocationId = loTempParam.CREC_ID_ALLOCATION_ID;
                var loParam = new GLM00414DTO();

                await _AllocationPeriod_gridRef.R_RefreshGrid(loParam);

                _SetHasDataDT = !string.IsNullOrWhiteSpace(loTempParam.CREC_ID_ALLOCATION_ID);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        private async Task Allocation_Period_ServiceGetListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {

                var loParam = (GLM00414DTO)eventArgs.Parameter;

                await _AllocationPeriod_viewModel.GetAllocationPeriodList(loParam);

                eventArgs.ListEntityResult = _AllocationPeriod_viewModel.AllocationPeriodGrid;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        private void Allocation_Period_ServiceGetRecord(R_ServiceGetRecordEventArgs eventArgs)
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

        private void Allocation_Period_Display(R_DisplayEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                if (eventArgs.ConductorMode == R_eConductorMode.Normal)
                {
                    var loParam = R_FrontUtility.ConvertObjectToObject<GLM00415DTO>(eventArgs.Data);
                    _AllocationPeriodCenter_gridRef.R_RefreshGrid(loParam);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        private async Task Allocation_CenterPeriod_ServiceGetListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = (GLM00415DTO)eventArgs.Parameter;
                loParam.CUSER_LANGUAGE = clientHelper.CultureUI.TwoLetterISOLanguageName;
                loParam.CYEAR = _AllocationPeriod_viewModel.Year.ToString();
                loParam.CREC_ID_ALLOCATION_ID = _AllocationPeriod_viewModel.AllocationId;

                await _AllocationPeriod_viewModel.GetAllocationPeriodCenterList(loParam);

                eventArgs.ListEntityResult = _AllocationPeriod_viewModel.AllocationPeriodCenterGrid;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        private async Task AllocationPeriod_Refresh_OnClick()
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = new GLM00414DTO();

                await _AllocationPeriod_gridRef.R_RefreshGrid(loParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }

        public async Task RefreshTabPageAsync(object poParam)
        {
            var loEx = new R_Exception();

            try
            {
                var loTempParam = (GLM00410DTO)poParam;

                _AllocationPeriod_viewModel.AllocationId = loTempParam.CREC_ID_ALLOCATION_ID;
                var loParam = new GLM00414DTO();

                await _AllocationPeriod_gridRef.R_RefreshGrid(loParam);

                _SetHasDataDT = !string.IsNullOrWhiteSpace(loTempParam.CREC_ID_ALLOCATION_ID);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            R_DisplayException(loEx);
        }
    }
}
