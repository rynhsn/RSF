using BlazorClientHelper;
using LMM06500COMMON;
using LMM06500MODEL;
using Lookup_LMCOMMON.DTOs;
using Lookup_LMFRONT;
using Microsoft.AspNetCore.Components;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.MessageBox;
using R_BlazorFrontEnd.Exceptions;

namespace LMM06500FRONT
{
    public partial class LMM06502 : R_Page
    {
        private LMM06502ViewModel _viewModel = new LMM06502ViewModel();
        private R_Conductor _Staff_Header_conductorRef;

        [Inject] IClientHelper clientHelper { get; set; }

        private R_Grid<LMM06502DetailDTO> _StaffMoveDetail_gridRef;
        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();

            try
            {
                var loData = (LMM06502HeaderDTO)poParameter;
                _viewModel.PropertyValueContext = loData.CPROPERTY_ID;
                _viewModel.StaffMoveHeader.CPROPERTY_ID = loData.CPROPERTY_ID;

                await Task.Delay(500);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        private void Staff_Before_Open_Lookup(R_BeforeOpenLookupEventArgs eventArgs)
        {
            var param = new LML00300ParameterDTO()
            {
                CPROPERTY_ID = _viewModel.PropertyValueContext,
            };

            eventArgs.Parameter = param;
            eventArgs.TargetPageType = typeof(LML00300);
        }

        private async void Staff_Old_After_Open_Lookup(R_AfterOpenLookupEventArgs eventArgs)
        {
            var loParam = new LMM06502DetailDTO();
            var loTempResult = (LML00300DTO)eventArgs.Result;

            if (loTempResult == null)
            {
                return;
            }

            _viewModel.StaffMoveHeader.COLD_SUPERVISOR_ID = loTempResult.CSUPERVISOR;
            _viewModel.StaffMoveHeader.COLD_SUPERVISOR_NAME = loTempResult.CSUPERVISOR_NAME;
            _viewModel.StaffMoveHeader.COLD_DEPT_CODE = loTempResult.CDEPT_CODE;
            _viewModel.StaffMoveHeader.COLD_DEPT_NAME = loTempResult.CDEPT_NAME;

            loParam.COLD_SUPERVISOR_ID = loTempResult.CSUPERVISOR;

            await _StaffMoveDetail_gridRef.R_RefreshGrid(loParam);
        }

        private void Staff_New_After_Open_Lookup(R_AfterOpenLookupEventArgs eventArgs)
        {
            var loTempResult = (LML00300DTO)eventArgs.Result;
            if (loTempResult == null)
            {
                return;
            }

            _viewModel.StaffMoveHeader.CNEW_SUPERVISOR_ID = loTempResult.CSUPERVISOR;
            _viewModel.StaffMoveHeader.CNEW_SUPERVISOR_NAME = loTempResult.CSUPERVISOR_NAME;
            _viewModel.StaffMoveHeader.CNEW_DEPT_CODE = loTempResult.CDEPT_CODE;
            _viewModel.StaffMoveHeader.CNEW_DEPT_NAME = loTempResult.CDEPT_NAME;
        }


        private async Task _Staff_Detail_ServiceGetListRecord(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await _viewModel.GetStaffMoveList((LMM06502DetailDTO)eventArgs.Parameter);

                eventArgs.ListEntityResult = _viewModel.StaffMoveGrid;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        public async Task Button_OnClickOkAsync()
        {
            var loEx = new R_Exception();
            var loData = new LMM06502DTO();
            var loDetailData = new List<LMM06502DetailDTO>();

            try
            {
                var loDetailTempData = _StaffMoveDetail_gridRef.DataSource;

                foreach (var item in loDetailTempData)
                {
                    if (item.LSELECTED)
                    {
                        loDetailData.Add(item);
                    }
                }

                if (!string.IsNullOrWhiteSpace(_viewModel.StaffMoveHeader.COLD_SUPERVISOR_ID) && !string.IsNullOrWhiteSpace(_viewModel.StaffMoveHeader.CNEW_SUPERVISOR_ID) && loDetailData.Count > 0)
                {
                    if (_viewModel.StaffMoveHeader.CNEW_SUPERVISOR_ID != _viewModel.StaffMoveHeader.COLD_SUPERVISOR_ID)
                    {
                        var loValidate = await R_MessageBox.Show("", "Staff department will be replacing with department from New Supervisor, are you want to process?", R_eMessageBoxButtonType.YesNo);
                        if (loValidate == R_eMessageBoxResult.Yes)
                        {
                            loData.Header = _viewModel.StaffMoveHeader;
                            loData.Detail = loDetailData;

                            loData.Header.CCOMPANY_ID = clientHelper.CompanyId;
                            loData.Header.CUSER_ID = clientHelper.UserId;

                            await _viewModel.SaveStaffMove(loData);

                            await this.Close(true, true);
                        }
                    }
                    else
                    {
                        await R_MessageBox.Show("", "Old Supervisor and New Supervisor Can't be the same!!!", R_eMessageBoxButtonType.OK);
                    }
                }
                else
                {
                    await R_MessageBox.Show("", "No data to process!!!", R_eMessageBoxButtonType.OK);
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task Button_OnClickCloseAsync()
        {
            await this.Close(true, false);
        }

    }
}
