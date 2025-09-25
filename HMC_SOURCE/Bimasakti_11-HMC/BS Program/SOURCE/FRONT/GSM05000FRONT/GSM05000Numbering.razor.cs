using BlazorClientHelper;
using GFF00900COMMON.DTOs;
using GSM05000Common.DTOs;
using GSM05000Model.ViewModel;
using Lookup_GSCOMMON.DTOs;
using Lookup_GSFRONT;
using Microsoft.AspNetCore.Components;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Enums;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.Grid.Columns;
using R_BlazorFrontEnd.Controls.Popup;
using R_BlazorFrontEnd.Enums;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_CommonFrontBackAPI;
using R_LockingFront;

namespace GSM05000Front
{
    public partial class GSM05000Numbering : R_Page
    {
        private GSM05000NumberingViewModel _viewModel = new();
        private R_ConductorGrid _conductorRef;
        private R_Grid<GSM05000NumberingGridDTO> _gridRef;
        [Inject] public R_PopupService PopupService { get; set; }
        [Inject] public IClientHelper _clientHelper { get; set; }

        protected override async Task R_Init_From_Master(object poParameter)
        {
            var loEx = new R_Exception();

            try
            {
                // _viewModel.HeaderEntity = (GSM05000NumberingHeaderDTO)poParameter;
                _viewModel.TransactionCode = ((GSM05000NumberingHeaderDTO)poParameter).CTRANS_CODE;
                await _viewModel.GetNumberingHeader();
                await _gridRef.R_RefreshGrid(null);
                // await _gridRefNumbering.AutoFitAllColumnsAsync();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task GetList(R_ServiceGetListRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await _viewModel.GetNumberingList();
                eventArgs.ListEntityResult = _viewModel.GridList;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task GetRecord(R_ServiceGetRecordEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                var loParam = R_FrontUtility.ConvertObjectToObject<GSM05000NumberingGridDTO>(eventArgs.Data);
                await _viewModel.GetEntityNumbering(loParam);
                eventArgs.Result = _viewModel.Entity;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private void AfterAdd(R_AfterAddEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = (GSM05000NumberingGridDTO)eventArgs.Data;
                var loData = _viewModel.GeneratePeriod(loParam);
                loParam.ISTART_NUMBER = 1;
                loParam.DCREATE_DATE = DateTime.Now;
                loParam.DUPDATE_DATE = DateTime.Now;
                loParam.CPERIOD = loData.CPERIOD;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task BeforeEdit(R_BeforeEditEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            GFF00900ParameterDTO loParam = null;
            R_PopupResult loResult = null;

            try
            {
                var loValidateViewModel = new GFF00900Model.ViewModel.GFF00900ViewModel();
                loValidateViewModel.ACTIVATE_INACTIVE_ACTIVITY_CODE =
                    "GSM05001"; //Ubah Approval Code sesuai Spec masing masing
                await loValidateViewModel
                    .RSP_ACTIVITY_VALIDITYMethodAsync(); //Jika IAPPROVAL_CODE == 3, maka akan keluar RSP_ERROR disini

                //Jika Approval User ALL dan Approval Code 1, maka akan langsung menjalankan ActiveInactive
                if (loValidateViewModel.loRspActivityValidityList.FirstOrDefault().CAPPROVAL_USER == "ALL" &&
                    loValidateViewModel.loRspActivityValidityResult.Data.FirstOrDefault().IAPPROVAL_MODE == 1)
                {
                    eventArgs.Cancel = false;
                }
                else //Disini Approval Code yang didapat adalah 2, yang berarti Active Inactive akan dijalankan jika User yang diinput ada di RSP_ACTIVITY_VALIDITY
                {
                    loParam = new GFF00900ParameterDTO()
                    {
                        Data = loValidateViewModel.loRspActivityValidityList,
                        IAPPROVAL_CODE = "GSM05001" //Uabh Approval Code sesuai Spec masing masing
                    };
                    loResult = await PopupService.Show(typeof(GFF00900FRONT.GFF00900), loParam);

                    if (loResult.Success == false)
                    {
                        eventArgs.Cancel = true;
                        return;
                    }

                    eventArgs.Cancel = !(bool)loResult.Result;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private void Saving(R_SavingEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loData = (GSM05000NumberingGridDTO)eventArgs.Data;

                if (eventArgs.ConductorMode == R_eConductorMode.Add)
                {
                    loData.CTRANSACTION_CODE = _viewModel.TransactionCode;

                    if (_viewModel.HeaderEntity.CPERIOD_MODE != "N")
                    {
                        loData.CCYEAR = loData.CPERIOD[..4];

                        if (_viewModel.HeaderEntity.CPERIOD_MODE == "P")
                        {
                            loData.CPERIOD_NO = loData.CPERIOD[^2..];
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task ServiceSave(R_ServiceSaveEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            try
            {
                var loParam = (GSM05000NumberingGridDTO)eventArgs.Data;
                await _viewModel.SaveEntity(loParam, (eCRUDMode)eventArgs.ConductorMode);
                eventArgs.Result = _viewModel.Entity;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task AfterSave(R_AfterSaveEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                await _gridRef.R_RefreshGrid((GSM05000NumberingGridDTO)eventArgs.Data);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task ServiceDelete(R_ServiceDeleteEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loParam = R_FrontUtility.ConvertObjectToObject<GSM05000NumberingGridDTO>(eventArgs.Data);
                await _viewModel.DeleteEntity(loParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private void BeforeLookupNumbering(R_BeforeOpenGridLookupColumnEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                eventArgs.Parameter = new GSL00700ParameterDTO();
                eventArgs.TargetPageType = typeof(GSL00700);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private void AfterLookupNumbering(R_AfterOpenGridLookupColumnEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loTempResult = (GSL00700DTO)eventArgs.Result;
                var loGetData = (GSM05000NumberingGridDTO)eventArgs.ColumnData;
                if (loTempResult == null)
                    return;

                loGetData.CDEPT_CODE = loTempResult.CDEPT_CODE;
                loGetData.CDEPT_NAME = loTempResult.CDEPT_NAME;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }

        private async Task Validation(R_ValidationEventArgs eventArgs)
        {
            var loEx = new R_Exception();

            try
            {
                var loData = (GSM05000NumberingGridDTO)eventArgs.Data;
                if (eventArgs.ConductorMode == R_eConductorMode.Add)
                {
                    foreach (var loItem in _viewModel.GridList)
                    {
                        if (loItem.CPERIOD == loData.CPERIOD && loItem.CDEPT_CODE == loData.CDEPT_CODE)
                        {
                            var loMsg = await R_MessageBox.Show("Error", "Period already exists");
                            eventArgs.Cancel = true;
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }


        #region Locking

        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrl";
        private const string DEFAULT_MODULE_NAME = "GS";

        protected async override Task<bool> R_LockUnlock(R_LockUnlockEventArgs eventArgs)
        {
            var loEx = new R_Exception();
            var llRtn = false;
            R_LockingFrontResult loLockResult;

            try
            {
                var loData = (GSM05000NumberingGridDTO)eventArgs.Data;

                var loCls = new R_LockingServiceClient(pcModuleName: DEFAULT_MODULE_NAME,
                    plSendWithContext: true,
                    plSendWithToken: true,
                    pcHttpClientName: DEFAULT_HTTP_NAME);

                var Company_Id = _clientHelper.CompanyId;
                var User_Id = _clientHelper.UserId;
                var Program_Id = "GSM05000";
                var Table_Name = "GSM_TRANSACTION_NUMBER";
                var Key_Value = string.Join("|", _clientHelper.CompanyId, loData.CTRANSACTION_CODE, loData.CCYEAR,
                    loData.CPERIOD_NO, loData.CDEPT_CODE);

                if (eventArgs.Mode == R_eLockUnlock.Lock)
                {
                    var loLockPar = new R_ServiceLockingLockParameterDTO
                    {
                        Company_Id = Company_Id,
                        User_Id = User_Id,
                        Program_Id = Program_Id,
                        Table_Name = Table_Name,
                        Key_Value = Key_Value
                    };
                    loLockResult = await loCls.R_Lock(loLockPar);
                }
                else
                {
                    var loUnlockPar = new R_ServiceLockingUnLockParameterDTO
                    {
                        Company_Id = Company_Id,
                        User_Id = User_Id,
                        Program_Id = Program_Id,
                        Table_Name = Table_Name,
                        Key_Value = Key_Value
                    };
                    loLockResult = await loCls.R_UnLock(loUnlockPar);
                }

                llRtn = loLockResult.IsSuccess;
                if (loLockResult is { IsSuccess: false, Exception: not null })
                    throw loLockResult.Exception;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return llRtn;
        }

        #endregion
    }
}