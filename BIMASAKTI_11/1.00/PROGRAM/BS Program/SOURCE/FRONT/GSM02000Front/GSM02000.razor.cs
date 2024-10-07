using BlazorClientHelper;
using GFF00900COMMON.DTOs;
using GSM02000Common.DTOs;
using GSM02000FrontResources;
using GSM02000Model.ViewModel;
using Lookup_GSCOMMON.DTOs;
using Lookup_GSFRONT;
using Lookup_GSModel.ViewModel;
using Microsoft.AspNetCore.Components;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Enums;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.Popup;
using R_BlazorFrontEnd.Enums;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_BlazorFrontEnd.Interfaces;
using R_CommonFrontBackAPI;
using R_LockingFront;

namespace GSM02000Front;

public partial class GSM02000 : R_Page
{
    private GSM02000ViewModel _viewModel = new();
    private R_Conductor _conductorRef;
    private R_Grid<GSM02000GridDTO> _gridRef = new();

    private R_ConductorGrid _conductorDeductionRef;
    private R_Grid<GSM02000DeductionGridDTO> _gridDeductionRef = new();
    private string loLabel;

    // private R_ILocalizer<Resources_Dummy_Class> _localizer;

    [Inject] private IClientHelper _clientHelper { get; set; }
    [Inject] private R_PopupService PopupService { get; set; }

    protected override async Task R_Init_From_Master(object poParam)
    {
        var loEx = new R_Exception();

        try
        {
            loLabel = _localizer["BTN_ACTIVATE"];
            // await _gridRef.AutoFitAllColumnsAsync();
            await _viewModel.GetRoundingMode();
            await _viewModel.GetProperty();
            await _gridRef.R_RefreshGrid(null);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    private async Task Grid_Display(R_DisplayEventArgs eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            if (eventArgs.ConductorMode == R_eConductorMode.Normal)
            {
                var loParam = (GSM02000DTO)eventArgs.Data;
                _viewModel.ActiveInactiveEntity.CTAX_ID = loParam.CTAX_ID;
                if (loParam.LACTIVE)
                {
                    loLabel = _localizer["BTN_INACTIVE"];
                    _viewModel.ActiveInactiveEntity.LACTIVE = false;
                }
                else
                {
                    loLabel = _localizer["BTN_ACTIVATE"];
                    _viewModel.ActiveInactiveEntity.LACTIVE = true;
                }
            }
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    private async Task Grid_R_ServiceGetListRecord(R_ServiceGetListRecordEventArgs eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            await _viewModel.GetGridList();
            eventArgs.ListEntityResult = _viewModel.GridList;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    private async Task Conductor_ServiceGetRecord(R_ServiceGetRecordEventArgs eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            var loParam = R_FrontUtility.ConvertObjectToObject<GSM02000DTO>(eventArgs.Data);

            await _viewModel.GetEntity(loParam);
            eventArgs.Result = _viewModel.Entity;

            await _gridDeductionRef.R_RefreshGrid(null);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    private async Task Conductor_ServiceSave(R_ServiceSaveEventArgs eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            var loParam = R_FrontUtility.ConvertObjectToObject<GSM02000DTO>(eventArgs.Data);
            loParam.ODEDUCTION_LIST = _gridDeductionRef.DataSource.ToList();
            await _viewModel.SaveEntity(loParam, (eCRUDMode)eventArgs.ConductorMode);

            eventArgs.Result = _viewModel.Entity;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    private async Task Conductor_ServiceDelete(R_ServiceDeleteEventArgs eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            var loParam = (GSM02000DTO)eventArgs.Data;
            loParam.ODEDUCTION_LIST = _gridDeductionRef.DataSource.ToList();
            // loParam.CDESCRIPTION ??= String.Empty;
            // loParam.CTAXIN_GLACCOUNT_NO ??= String.Empty;
            // loParam.CTAXOUT_GLACCOUNT_NO ??= String.Empty;
            await _viewModel.DeleteEntity(loParam);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    private async Task Conductor_AfterSave(R_AfterSaveEventArgs eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            await _gridRef.R_RefreshGrid((GSM02000DTO)eventArgs.Data);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    #region Lookup Button

    private R_AddButton R_AddBtn;
    private R_Button R_ActiveInActiveBtn;
    public R_Lookup R_LookupBtnIn;
    private R_Lookup R_LookupBtnOut;
    private R_Lookup R_LookupBtnDeduction;


    private R_TextBox _textTaxIn;

    private async Task OnLostFocusGLAccountIn()
    {
        var loEx = new R_Exception();

        LookupGSL00500ViewModel loLookupViewModel = new LookupGSL00500ViewModel();
        try
        {
            if (string.IsNullOrEmpty(_viewModel.Data.CTAXIN_GLACCOUNT_NO))
            {
                _viewModel.Data.CTAXIN_GLACCOUNT_NAME = "";
                goto EndBlock;
            }

            var param = new GSL00500ParameterDTO
            {
                CPROPERTY_ID = "",
                CPROGRAM_CODE = "GSM02000",
                CBSIS = "",
                CDBCR = "",
                LCENTER_RESTR = false,
                LUSER_RESTR = false,
                CCENTER_CODE = "",
                CUSER_LANGUAGE = _clientHelper.CultureUI.TwoLetterISOLanguageName,
                CSEARCH_TEXT = _viewModel.Data.CTAXIN_GLACCOUNT_NO
            };

            GSL00500DTO loResult = null;

            loResult = await loLookupViewModel.GetGLAccount(param);

            if (loResult == null)
            {
                loEx.Add(R_FrontUtility.R_GetError(
                    typeof(Lookup_GSFrontResources.Resources_Dummy_Class),
                    "_ErrLookup01"));
                _viewModel.Data.CTAXIN_GLACCOUNT_NO = "";
                // await _textTaxIn.FocusAsync();
                _viewModel.Data.CTAXIN_GLACCOUNT_NAME = "";
                goto EndBlock;
            }

            _viewModel.Data.CTAXIN_GLACCOUNT_NAME = loResult.CGLACCOUNT_NAME;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        EndBlock:
        R_DisplayException(loEx);
    }

    private void BeforeOpenLookupIn(R_BeforeOpenLookupEventArgs eventArgs)
    {
        var param = new GSL00500ParameterDTO
        {
            CPROPERTY_ID = "",
            CPROGRAM_CODE = "GSM02000",
            CBSIS = "",
            CDBCR = "",
            LCENTER_RESTR = false,
            LUSER_RESTR = false,
            CCENTER_CODE = "",
            CUSER_LANGUAGE = _clientHelper.CultureUI.TwoLetterISOLanguageName
        };
        eventArgs.Parameter = param;
        eventArgs.TargetPageType = typeof(GSL00500);
    }

    private void AfterOpenLookupIn(R_AfterOpenLookupEventArgs eventArgs)
    {
        var loTempResult = (GSL00500DTO)eventArgs.Result;
        if (loTempResult == null)
            return;

        var loGetData = (GSM02000DTO)_conductorRef.R_GetCurrentData();
        loGetData.CTAXIN_GLACCOUNT_NO = loTempResult.CGLACCOUNT_NO;
        loGetData.CTAXIN_GLACCOUNT_NAME = loTempResult?.CGLACCOUNT_NAME;
    }

    private async Task OnLostFocusGLAccountOut()
    {
        var loEx = new R_Exception();

        LookupGSL00500ViewModel loLookupViewModel = new LookupGSL00500ViewModel();
        try
        {
            if (string.IsNullOrEmpty(_viewModel.Data.CTAXOUT_GLACCOUNT_NO))
            {
                _viewModel.Data.CTAXOUT_GLACCOUNT_NAME = "";
                goto EndBlock;
            }

            var param = new GSL00500ParameterDTO
            {
                CPROPERTY_ID = "",
                CPROGRAM_CODE = "GSM02000",
                CBSIS = "",
                CDBCR = "",
                LCENTER_RESTR = false,
                LUSER_RESTR = false,
                CCENTER_CODE = "",
                CUSER_LANGUAGE = _clientHelper.CultureUI.TwoLetterISOLanguageName,
                CSEARCH_TEXT = _viewModel.Data.CTAXOUT_GLACCOUNT_NO
            };

            var loResult = await loLookupViewModel.GetGLAccount(param);

            if (loResult == null)
            {
                loEx.Add(
                    R_FrontUtility.R_GetError(
                        typeof(Lookup_GSFrontResources.Resources_Dummy_Class),
                        "_ErrLookup01"));
                _viewModel.Data.CTAXOUT_GLACCOUNT_NO = "";
                _viewModel.Data.CTAXOUT_GLACCOUNT_NAME = "";
                goto EndBlock;
            }

            _viewModel.Data.CTAXOUT_GLACCOUNT_NAME = loResult.CGLACCOUNT_NAME;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        EndBlock:
        R_DisplayException(loEx);
    }

    private void BeforeOpenLookupOut(R_BeforeOpenLookupEventArgs eventArgs)
    {
        var param = new GSL00500ParameterDTO
        {
            CPROPERTY_ID = "",
            CPROGRAM_CODE = "GSM02000",
            CBSIS = "",
            CDBCR = "",
            LCENTER_RESTR = false,
            LUSER_RESTR = false,
            CCENTER_CODE = "",
            CUSER_LANGUAGE = _clientHelper.CultureUI.TwoLetterISOLanguageName
        };
        eventArgs.Parameter = param;
        eventArgs.TargetPageType = typeof(GSL00500);
    }

    private void AfterOpenLookupOut(R_AfterOpenLookupEventArgs eventArgs)
    {
        var loTempResult = (GSL00500DTO)eventArgs.Result;
        if (loTempResult == null)
            return;

        var loGetData = (GSM02000DTO)_conductorRef.R_GetCurrentData();
        loGetData.CTAXOUT_GLACCOUNT_NO = loTempResult.CGLACCOUNT_NO;
        loGetData.CTAXOUT_GLACCOUNT_NAME = loTempResult?.CGLACCOUNT_NAME;
    }

    // private async Task OnLostFocusDeduction()
    // {
    //     var loEx = new R_Exception();
    //
    //     LookupGSL01400ViewModel loLookupViewModel = new LookupGSL01400ViewModel();
    //     try
    //     {
    //         if (string.IsNullOrEmpty(_viewModel.Data.CDEDUCTION_TAX_ID))
    //         {
    //             _viewModel.Data.CDEDUCTION_TAX_NAME = "";
    //             goto EndBlock;
    //         }
    //
    //         var param = new GSL01400ParameterDTO
    //             {
    //                 CPROPERTY_ID = "",
    //                 CCHARGES_TYPE_ID = "D",
    //                 CSEARCH_TEXT = _viewModel.Data.CDEDUCTION_TAX_ID
    //             };
    //
    //             var loResult = await loLookupViewModel.GetOtherCharges(param);
    //
    //             if (loResult == null)
    //             {
    //                 loEx.Add(
    //                     R_FrontUtility.R_GetError(
    //                     typeof(Lookup_GSFrontResources.Resources_Dummy_Class),
    //                     "_ErrLookup01"));
    //                 _viewModel.Data.CDEDUCTION_TAX_ID = "";
    //                 _viewModel.Data.CDEDUCTION_TAX_NAME = "";
    //                 goto EndBlock;
    //             }
    //
    //             _viewModel.Data.CDEDUCTION_TAX_NAME = loResult.CCHARGES_NAME;
    //     }
    //     catch (Exception ex)
    //     {
    //         loEx.Add(ex);
    //     }
    //
    //     EndBlock:
    //     R_DisplayException(loEx);
    // }
    //
    // private void BeforeOpenLookupDeduction(R_BeforeOpenLookupEventArgs eventArgs)
    // {
    //     var param = new GSL01400ParameterDTO
    //     {
    //         CPROPERTY_ID = "",
    //         CCHARGES_TYPE_ID = "D",
    //     };
    //     eventArgs.Parameter = param;
    //     eventArgs.TargetPageType = typeof(GSL01400);
    // }
    //
    // private void AfterOpenLookupDeduction(R_AfterOpenLookupEventArgs eventArgs)
    // {
    //     var loTempResult = (GSL01400DTO)eventArgs.Result;
    //     if (loTempResult == null)
    //         return;
    //
    //     var loGetData = (GSM02000DTO)_conductorRef.R_GetCurrentData();
    //     loGetData.CDEDUCTION_TAX_ID = loTempResult.CCHARGES_ID;
    //     loGetData.CDEDUCTION_TAX_NAME = loTempResult?.CCHARGES_NAME;
    // }

    private void R_SetAdd(R_SetEventArgs eventArgs)
    {
        if (R_LookupBtnOut != null) R_LookupBtnOut.Enabled = eventArgs.Enable;
        if (R_LookupBtnIn != null) R_LookupBtnIn.Enabled = eventArgs.Enable;
        if (R_LookupBtnDeduction != null) R_LookupBtnDeduction.Enabled = eventArgs.Enable;
    }

    private void R_SetEdit(R_SetEventArgs eventArgs)
    {
        if (R_LookupBtnOut != null) R_LookupBtnOut.Enabled = eventArgs.Enable;
        if (R_LookupBtnIn != null) R_LookupBtnIn.Enabled = eventArgs.Enable;
        if (R_LookupBtnDeduction != null) R_LookupBtnDeduction.Enabled = eventArgs.Enable;
    }

    #endregion

    private async Task BeforeOpenActiveInactive(R_BeforeOpenPopupEventArgs eventArgs)
    {
        var loException = new R_Exception();
        try
        {
            var loValidateViewModel = new GFF00900Model.ViewModel.GFF00900ViewModel();
            loValidateViewModel.ACTIVATE_INACTIVE_ACTIVITY_CODE =
                "GSM02001"; //Uabh Approval Code sesuai Spec masing masing
            await loValidateViewModel
                .RSP_ACTIVITY_VALIDITYMethodAsync(); //Jika IAPPROVAL_CODE == 3, maka akan keluar RSP_ERROR disini

            //Jika Approval User ALL dan Approval Code 1, maka akan langsung menjalankan ActiveInactive
            if (loValidateViewModel.loRspActivityValidityList.FirstOrDefault().CAPPROVAL_USER == "ALL" &&
                loValidateViewModel.loRspActivityValidityResult.Data.FirstOrDefault().IAPPROVAL_MODE == 1)
            {
                await _viewModel.SetActiveInactive();
                await _gridRef.R_RefreshGrid(null);
                return;
            }
            else //Disini Approval Code yang didapat adalah 2, yang berarti Active Inactive akan dijalankan jika User yang diinput ada di RSP_ACTIVITY_VALIDITY
            {
                eventArgs.Parameter = new GFF00900ParameterDTO()
                {
                    Data = loValidateViewModel.loRspActivityValidityList,
                    IAPPROVAL_CODE = "GSM02001" //Uabh Approval Code sesuai Spec masing masing
                };
                eventArgs.TargetPageType = typeof(GFF00900FRONT.GFF00900);
            }
        }
        catch (Exception ex)
        {
            loException.Add(ex);
        }

        loException.ThrowExceptionIfErrors();
    }

    private async Task AfterOpenActiveInactive(R_AfterOpenPopupEventArgs eventArgs)
    {
        var loException = new R_Exception();
        try
        {
            if (eventArgs.Success == false)
                return;

            var result = (bool)eventArgs.Result;
            if (result)
            {
                await _viewModel.SetActiveInactive();
                await _gridRef.R_RefreshGrid(null);
            }
        }
        catch (Exception ex)
        {
            loException.Add(ex);
        }

        loException.ThrowExceptionIfErrors();
    }

    private Task InstanceTaxTab(R_InstantiateDockEventArgs eventArgs)
    {
        // eventArgs.Parameter = R_FrontUtility.ConvertObjectToObject<GSM02000DTO>(_viewModel.Entity);
        eventArgs.TargetPageType = typeof(GSM02000Tax);
        return Task.CompletedTask;
    }

    private async Task Validation(R_ValidationEventArgs eventArgs)
    {
        var loEx = new R_Exception();

        R_PopupResult loResult = null;
        GFF00900ParameterDTO loParam = null;
        GSM02000DTO loData = null;
        var lsApprovalCode = "GSM02001";
        try
        {
            loData = (GSM02000DTO)eventArgs.Data;
            // Validate(loData);

            loData.CTAX_ID ??= "";
            loData.CTAX_NAME ??= "";
            loData.CTAXIN_GLACCOUNT_NO ??= "";
            loData.CTAXOUT_GLACCOUNT_NO ??= "";

            if (loData.LACTIVE == true && _conductorRef.R_ConductorMode == R_eConductorMode.Add)
            {
                var loValidateViewModel = new GFF00900Model.ViewModel.GFF00900ViewModel();
                loValidateViewModel.ACTIVATE_INACTIVE_ACTIVITY_CODE = lsApprovalCode;
                await loValidateViewModel.RSP_ACTIVITY_VALIDITYMethodAsync();

                if (loValidateViewModel.loRspActivityValidityList.FirstOrDefault().CAPPROVAL_USER == "ALL" &&
                    loValidateViewModel.loRspActivityValidityResult.Data.FirstOrDefault().IAPPROVAL_MODE == 1)
                {
                    eventArgs.Cancel = false;
                }
                else
                {
                    loParam = new GFF00900ParameterDTO()
                    {
                        Data = loValidateViewModel.loRspActivityValidityList,
                        IAPPROVAL_CODE = lsApprovalCode
                    };
                    loResult = await PopupService.Show(typeof(GFF00900FRONT.GFF00900), loParam);
                    if (loResult.Success == false || (bool)loResult.Result == false)
                    {
                        eventArgs.Cancel = true;
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

    private void ChangeRoundingMode(object obj)
    {
        if ((string)obj == "03")
        {
            _viewModel.Data.IROUNDING = 0;
        }
    }

    private void Conductor_AfterAdd(R_AfterAddEventArgs eventArgs)
    {
        var loData = (GSM02000DTO)eventArgs.Data;
        loData.LACTIVE = true;
        loData.CROUNDING_MODE = "01";
        // loData.CROUNDING_MODE = _viewModel.RoundingModeList.FirstOrDefault().CCODE;
        // eventArgs.Data = tes;
        _gridDeductionRef.DataSource.Clear();
    }

    private void Validate(GSM02000DTO poParam)
    {
        var loEx = new R_Exception();

        try
        {
            if (poParam.CTAX_ID == null)
            {
                loEx.Add("Err01", _localizer["Err01"]);
            }

            if (poParam.CTAX_NAME == null)
            {
                loEx.Add("Err02", _localizer["Err02"]);
            }

            if (poParam.CROUNDING_MODE == null)
            {
                loEx.Add("Err03", _localizer["Err03"]);
            }

            if (poParam.IROUNDING == null)
            {
                loEx.Add("Err04", _localizer["Err04"]);
            }

            if (poParam.CTAXIN_GLACCOUNT_NO == null)
            {
                loEx.Add("Err07", _localizer["Err07"]);
            }

            if (poParam.CTAXOUT_GLACCOUNT_NO == null)
            {
                loEx.Add("Err08", _localizer["Err08"]);
            }
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    private bool _gridEnabled;
    private bool _gridDeductionEnabled;

    private void SetOther(R_SetEventArgs eventArgs)
    {
        _gridEnabled = eventArgs.Enable;
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
            var loData = (GSM02000DTO)eventArgs.Data;

            var loCls = new R_LockingServiceClient(pcModuleName: DEFAULT_MODULE_NAME,
                plSendWithContext: true,
                plSendWithToken: true,
                pcHttpClientName: DEFAULT_HTTP_NAME);

            if (eventArgs.Mode == R_eLockUnlock.Lock)
            {
                var loLockPar = new R_ServiceLockingLockParameterDTO
                {
                    Company_Id = _clientHelper.CompanyId,
                    User_Id = _clientHelper.UserId,
                    Program_Id = "GSM02000",
                    Table_Name = "GSM_TAX",
                    Key_Value = string.Join("|", _clientHelper.CompanyId, loData.CTAX_ID)
                };

                loLockResult = await loCls.R_Lock(loLockPar);
            }
            else
            {
                var loUnlockPar = new R_ServiceLockingUnLockParameterDTO
                {
                    Company_Id = _clientHelper.CompanyId,
                    User_Id = _clientHelper.UserId,
                    Program_Id = "GSM02000",
                    Table_Name = "GSM_TAX",
                    Key_Value = string.Join("|", _clientHelper.CompanyId, loData.CTAX_ID)
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

    #region Deduction

    private async Task GetListDeduction(R_ServiceGetListRecordEventArgs eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            await _viewModel.GetDeductionGridList();
            eventArgs.ListEntityResult = _viewModel.DeductionGridList;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    private void BeforeLookupDeduction(R_BeforeOpenGridLookupColumnEventArgs eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            var loEntity = (GSM02000DeductionGridDTO)_conductorDeductionRef.R_GetCurrentData();
            eventArgs.Parameter = new GSL01400ParameterDTO()
            {
                CPROPERTY_ID = loEntity.CPROPERTY_ID,
                CCHARGES_TYPE_ID = "D"
            };
            eventArgs.TargetPageType = typeof(GSL01400);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    private void AfterLookupDeduction(R_AfterOpenGridLookupColumnEventArgs eventArgs)
    {
        var loEx = new R_Exception();
        try
        {
            var loTempResult = (GSL01400DTO)eventArgs.Result;
            var loGetData = (GSM02000DeductionGridDTO)eventArgs.ColumnData;
            if (loTempResult == null)
                return;

            loGetData.CDEDUCTION_TAX_ID = loTempResult.CCHARGES_ID;
            loGetData.CDEDUCTION_TAX_NAME = loTempResult.CCHARGES_NAME;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    private void PropertyCellValueChanged(R_CellValueChangedEventArgs eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            if (eventArgs.ColumnName == "CPROPERTY_ID")
            {
                var loData = (GSM02000DeductionGridDTO)eventArgs.CurrentRow;
                var loCode = eventArgs.Value.ToString();
                loData.CPROPERTY_NAME = _viewModel.PropertyList.FirstOrDefault(x => x.CPROPERTY_ID == loCode)?.CPROPERTY_NAME;
                
                var loDeductionColumn = _gridDeductionRef.Columns.FirstOrDefault(x =>
                    x.Name == nameof(GSM02000DeductionGridDTO.CDEDUCTION_TAX_ID));
                loDeductionColumn.Enabled = !string.IsNullOrEmpty(loCode);
            }
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    private void DeductionValidate(R_ValidationEventArgs eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            var loData = (GSM02000DeductionGridDTO)eventArgs.Data;
            if (string.IsNullOrEmpty(loData.CPROPERTY_ID))
            {
                loEx.Add("Err09", _localizer["Err09"]);
            }

            if (string.IsNullOrEmpty(loData.CDEDUCTION_TAX_ID))
            {
                loEx.Add("Err10", _localizer["Err10"]);
            }
            
            //cek apakah data yang dimasukkan sekarang sudah ada di grid
            if (_gridDeductionRef.DataSource.Any(x => x.CPROPERTY_ID == loData.CPROPERTY_ID && x.CDEDUCTION_TAX_ID == loData.CDEDUCTION_TAX_ID))
            {
                loEx.Add("Err11", _localizer["Err11"]);
            }
            
            if(loEx.HasError) eventArgs.Cancel = true;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }
    #endregion

    private void DeductionGetRecord(R_ServiceGetRecordEventArgs eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            var loParam = R_FrontUtility.ConvertObjectToObject<GSM02000DeductionGridDTO>(eventArgs.Data);
            var loData = _viewModel.DeductionGridList.FirstOrDefault(x => x.CPROPERTY_ID == loParam.CPROPERTY_ID && x.CDEDUCTION_TAX_ID == loParam.CDEDUCTION_TAX_ID);
            eventArgs.Result = loData;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    private void DeductionDelete(R_ServiceDeleteEventArgs eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            var loParam = (GSM02000DeductionGridDTO)eventArgs.Data;
            _viewModel.DeductionGridList.Remove(loParam);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    private async Task BeforeCancel(R_BeforeCancelEventArgs eventArgs)
    {

        await _gridDeductionRef.R_RefreshGrid(null);
    }
}