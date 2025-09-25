using BlazorClientHelper;
using GSM06000Common;
using GSM06000Model.ViewModel;
using Lookup_GSCOMMON.DTOs;
using Lookup_GSFRONT;
using Microsoft.AspNetCore.Components;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.MessageBox;
using R_BlazorFrontEnd.Enums;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_CommonFrontBackAPI;

namespace GSM06000Front;

public partial class GSM06010Info : R_Page
{
    #region GSM06010

    private R_Conductor? _conductorRefGSM06010;
    private R_Grid<GSM06010GridDTO>? _gridRefGSM06010;
    private readonly GSM06010ViewModel _viewModelGSM06010 = new();
    [Inject] private IClientHelper? clientHelper { get; set; }

    protected override async Task<Task> R_Init_From_Master(object poParameter)
    {
        var loEx = new R_Exception();

        try
        {
            var loParam = R_FrontUtility.ConvertObjectToObject<GSM06000DTO>(poParameter);
            if (loParam.CCB_CODE != null) await _viewModelGSM06010.GetParameterInfo(loParam);
            await _viewModelGSM06010.GetTypeList();
            await _gridRefGSM06010.R_RefreshGrid(loParam);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        return Task.CompletedTask;
    }

    private async Task R_DisplayGSM06010(R_DisplayEventArgs eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            if (eventArgs.ConductorMode == R_eConductorMode.Normal)
            {
                await _viewModelGSM06010.GetEntity((GSM06010DTO)eventArgs.Data);
                GetUpdateSample();
            }
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }


    private void GetUpdateSample()
    {
        var loEx = new R_Exception();

        try
        {
            _viewModelGSM06010.getRefNo();
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        R_DisplayException(loEx);
    }

    private void AfterAddGSM06000CashBankInfo(R_AfterAddEventArgs eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            var loParam = (GSM06010DTO)eventArgs.Data;
            loParam.CUSER_LOGIN_ID = clientHelper.UserId;
            _viewModelGSM06010.AfterAddValidationGSM06010(loParam);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
        
    }
    
    private async Task ServiceGetRecordGSM06010(R_ServiceGetRecordEventArgs eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            var loParam = R_FrontUtility.ConvertObjectToObject<GSM06010DTO>(eventArgs.Data);
            await _viewModelGSM06010.GetEntity(loParam);

            eventArgs.Result = _viewModelGSM06010.loEntity;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    private async Task ServiceSaveGSM06010(R_ServiceSaveEventArgs eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            var loParam = (GSM06010DTO)eventArgs.Data;
            loParam.CSEQUENCE01 = _viewModelGSM06010.PropertyTempSequence01 == 1 ? "01" : "02";
            loParam.CSEQUENCE02 = _viewModelGSM06010.PropertyTempSequence02 == 1 ? "01" : "02";

            loParam.CUSER_LOGIN_ID = clientHelper.UserId;
            loParam.CCB_CODE = _viewModelGSM06010.loParameterGSM06010.CCB_CODE;
            if (loParam.CCUST_SUPP_ID == null)
            {
                loParam.CCUST_SUPP_ID = "";
                loParam.CBCHG_GLACCOUNT_NO = "";
            }

            await _viewModelGSM06010.SaveCashBank(loParam, (eCRUDMode)eventArgs.ConductorMode);

            eventArgs.Result = _viewModelGSM06010.loEntity;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    private bool IsListExist;
    private void R_Before_OpenDocNumbering_Detail(R_BeforeOpenDetailEventArgs eventArgs)
    {
        _viewModelGSM06010.ConvertParamGSM06000DocNumbering(_viewModelGSM06010.Data);
        eventArgs.Parameter = _viewModelGSM06010.loParamGSM06020Parameter;
        eventArgs.TargetPageType = typeof(GSM06010DocNumbering);
    }

    private void R_After_OpenDocNumbering_Detail()
    {

    }

    private async Task ServiceDeleteGSM06010(R_ServiceDeleteEventArgs eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            var loData = (GSM06010DTO)eventArgs.Data;
            await _viewModelGSM06010.DeleteCashBank(loData);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    public async Task R_AfterDeleteGSM06010()
    {
        _viewModelGSM06010.loEntity = new GSM06010DTO();
        await R_MessageBox.Show("", "Delete Success", R_eMessageBoxButtonType.OK);
    }

    private async Task ServiceGetListRecordGSM06010(R_ServiceGetListRecordEventArgs eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            await _viewModelGSM06010.GetAllCashBankList();
            eventArgs.ListEntityResult = _viewModelGSM06010.loGridList;
            if (_viewModelGSM06010.loGridList.Count == 0)
                IsListExist = false;
            else if (_viewModelGSM06010.loGridList.Any())
                IsListExist = true;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        R_DisplayException(loEx);
    }

    #region Lookup Button GGL Account Cash

    private R_Lookup? R_LookupCCBAccountNoButton;

    private void BeforeOpenLookUpCCBAccountNo(R_BeforeOpenLookupEventArgs eventArgs)
    {
        var param = new GSL00500ParameterDTO
        {
            CCOMPANY_ID = clientHelper.CompanyId,
            CPROPERTY_ID = "",
            CPROGRAM_CODE = "GSM03000",
            CBSIS = "",
            CDBCR = "C",
            LCENTER_RESTR = false,
            LUSER_RESTR = false,
            CUSER_ID = clientHelper.UserId,
            CCENTER_CODE = "",
            CUSER_LANGUAGE = clientHelper.CultureUI.TwoLetterISOLanguageName
        };
        eventArgs.Parameter = param;
        eventArgs.TargetPageType = typeof(GSL00500);
    }

    private void AfterOpenLookUpCCBAccountNo(R_AfterOpenLookupEventArgs eventArgs)
    {
        var loTempResult = (GSL00500DTO)eventArgs.Result;
        if (loTempResult == null)
            return;

        var loGetData = (GSM06010DTO)_conductorRefGSM06010.R_GetCurrentData();
        loGetData.CCB_ACCOUNT_NAME = loTempResult.CGLACCOUNT_NAME;
        loGetData.CCB_GLACCOUNT_NO = loTempResult.CGLACCOUNT_NO;
    }

    #endregion

    #region Lookup Button GGL Account Bank

    private R_Lookup? R_LookupCBCHG_GLACCOUNT_NOButton;

    private void BeforeOpenLookUpCBCHG_GLACCOUNT_NO(R_BeforeOpenLookupEventArgs eventArgs)
    {
        var param = new GSL00500ParameterDTO
        {
            CCOMPANY_ID = clientHelper.CompanyId,
            CPROPERTY_ID = "",
            CPROGRAM_CODE = "GSM03000",
            CBSIS = "",
            CDBCR = "C",
            LCENTER_RESTR = false,
            LUSER_RESTR = false,
            CUSER_ID = clientHelper.UserId,
            CCENTER_CODE = "",
            CUSER_LANGUAGE = clientHelper.CultureUI.TwoLetterISOLanguageName
        };
        eventArgs.Parameter = param;
        eventArgs.TargetPageType = typeof(GSL00500);
    }

    private void AfterOpenLookUpCBCHG_GLACCOUNT_NO(R_AfterOpenLookupEventArgs eventArgs)
    {
        var loTempResult = (GSL00500DTO)eventArgs.Result;
        if (loTempResult == null)
            return;

        var loGetData = (GSM06010DTO)_conductorRefGSM06010.R_GetCurrentData();
        loGetData.CCB_ACCOUNT_NAME = loTempResult.CGLACCOUNT_NAME;
        loGetData.CBCHG_GLACCOUNT_NO = loTempResult.CGLACCOUNT_NO;
    }

    #endregion

    #region Lookup Button CDEPT_CODE

    private R_Lookup? R_LookupCdeptCodeButton;

    private void BeforeOpenLookUpCDeptCode(R_BeforeOpenLookupEventArgs eventArgs)
    {
        var param = new GSL00700ParameterDTO
        {
            CCOMPANY_ID = clientHelper.CompanyId,
            CUSER_ID = clientHelper.UserId
        };
        eventArgs.Parameter = param;
        eventArgs.TargetPageType = typeof(GSL00700);
    }

    private void AfterOpenLookUpCDeptCode(R_AfterOpenLookupEventArgs eventArgs)
    {
        var loTempResult = (GSL00700DTO)eventArgs.Result;
        if (loTempResult == null)
            return;

        var loGetData = (GSM06010DTO)_conductorRefGSM06010.R_GetCurrentData();
        loGetData.CDEPT_CODE = loTempResult.CDEPT_CODE;
        loGetData.CDEPT_NAME = loTempResult.CDEPT_NAME;
    }

    #endregion

    #region Lookup Button CCURRENCY_CODE

    private R_Lookup? R_LookupCCurrencyCodeButton;

    private void BeforeOpenLookUpCCurrencyCode(R_BeforeOpenLookupEventArgs eventArgs)
    {
        eventArgs.TargetPageType = typeof(GSL00300);
    }

    private void AfterOpenLookUpCCurrencyCode(R_AfterOpenLookupEventArgs eventArgs)
    {
        var loTempResult = (GSL00300DTO)eventArgs.Result;
        if (loTempResult == null)
            return;

        var loGetData = (GSM06010DTO)_conductorRefGSM06010.R_GetCurrentData();
        loGetData.CCURRENCY_CODE = loTempResult.CCURRENCY_CODE;
        loGetData.CCURRENCY_NAME = loTempResult.CCURRENCY_NAME;
    }

    #endregion

    #endregion

}
