using BlazorClientHelper;
using GLM00100Common.DTOs;
using GLM00100Model.ViewModel;
using Lookup_GSCOMMON.DTOs;
using Lookup_GSFRONT;
using Microsoft.AspNetCore.Components;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.MessageBox;
using R_BlazorFrontEnd.Controls.Popup;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_CommonFrontBackAPI;

namespace GLM00100Front;

public partial class GLM00100 : R_Page
{
    private readonly GLM00100ViewModel _viewModelGLM00100 = new();
    private R_TextBox? _componentCEDTextBox;
    private R_TextBox? _componentCRTTextBox;
    private R_TextBox? _componentREATextBox;
    private R_TextBox? _componentSATextBox;
    private R_Conductor? _conductorGLM00100;

    [Inject] private IClientHelper? _clientHelper { get; set; }


    protected override async Task R_Init_From_Master(object poParameter)
    {
        var loEx = new R_Exception();
        
        try
        {
            await _conductorGLM00100.R_GetEntity(null);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        R_DisplayException(loEx);
    }

    private async Task ValidationGLM00100Async(R_ValidationEventArgs eventArgs)
    {
        var loException = new R_Exception();

        try
        {
            var loData = (GLM00100DTO)eventArgs.Data;

            var _iCounter = 0;

            if (string.IsNullOrEmpty(loData.CRATETYPE_CODE))
            {
                loException.Add("", "Currency Rate Type is required!");
                if (_iCounter == 0)
                {
                    await _componentCRTTextBox.FocusAsync();
                    _iCounter++;
                }
            }

            if (string.IsNullOrEmpty(loData.CSUSPENSE_ACCOUNT_NO))
            {
                loException.Add("", "Suspense Account is required!");
                if (_iCounter == 0)
                {
                    await _componentSATextBox.FocusAsync();
                    _iCounter++;
                }
            }

            if (string.IsNullOrEmpty(loData.CCLOSE_DEPT_CODE))
            {
                loException.Add("", "Closing Entries Department is required!");

                if (_iCounter == 0)
                {
                    await _componentCEDTextBox.FocusAsync();
                    _iCounter++;
                }
            }

            if (string.IsNullOrEmpty(loData.CRETAINED_ACCOUNT_NO))
            {
                loException.Add("", "Retained Earning Account is required!");
                if (_iCounter == 0)
                {
                    await _componentREATextBox.FocusAsync();
                    _iCounter++;
                }
            }
        }
        catch (Exception ex)
        {
            loException.Add(ex);
        }

        eventArgs.Cancel = loException.HasError;

        loException.ThrowExceptionIfErrors();
    }



    #region CoreGLM00100

    [Inject] public R_PopupService? PopupService { get; set; }


    public async Task Conductor_BeforeEdit(R_BeforeEditEventArgs? eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            var loResult = await PopupService.Show(typeof(GLM00100Front.GLM00100SystemParameterPopUp), "GLM00100");

            eventArgs.Cancel = !(bool)loResult.Result;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }
    
    private async Task R_ServiceGetRecordGLM00100(R_ServiceGetRecordEventArgs eventArgs)
    {
        var loEx = new R_Exception();

        StartSystem:
        try
        {
            var loParam = new GLM00100DTO();
            loParam.CLANGUAGE_ID = _clientHelper.CultureUI.TwoLetterISOLanguageName;
            await _viewModelGLM00100.GetEntity(loParam);

            bool? llChecker = await _viewModelGLM00100.GLM00100GetCheckerSystemParameterAvailable();

            if (llChecker == false)
            {
                await Conductor_BeforeEdit(null);
                goto StartSystem;
            }
            
            eventArgs.Result = _viewModelGLM00100.loEntityGLM00100;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        //R_DisplayException(loEx);
        loEx.ThrowExceptionIfErrors();
    }
    
    private async Task ServiceSaveLMM00100(R_ServiceSaveEventArgs eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            var loParam = R_FrontUtility.ConvertObjectToObject<GLM00100DTO>(eventArgs.Data);

            await _viewModelGLM00100.SaveCashSystemParameter(loParam, (eCRUDMode)eventArgs.ConductorMode);
            eventArgs.Result = _viewModelGLM00100.loEntityGLM00100;
            _conductorGLM00100.R_GetCurrentData();
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    #endregion

    #region Checker Lenght and UPPERCASE

    private void GetUpperCaseAndCheckLengthCRT()
    {
        var loEx = new R_Exception();

        try
        {
            if (_viewModelGLM00100.Data.CRATETYPE_CODE.Length > 4)
            {
                loEx.Add("", "[INPUT Rate Type Code] length cannot exceed 4 characters");
                _viewModelGLM00100.Data.CRATETYPE_CODE = _viewModelGLM00100.Data.CRATETYPE_CODE.Substring(0, 4);
            }
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        R_DisplayException(loEx);
    }

    private async Task GetUpperCaseAndCheckLengthCED()
    {
        var loEx = new R_Exception();

        try
        {
            var _iMaxLenght = 20;
            var _cMessage = "[INPUT Closing Entries Department Code]";

            _viewModelGLM00100.Data.CCLOSE_DEPT_CODE = _viewModelGLM00100.Data.CCLOSE_DEPT_CODE.ToUpper();

            if (_viewModelGLM00100.Data.CCLOSE_DEPT_CODE.Length > _iMaxLenght)
            {
                loEx.Add("", $"{_cMessage} length cannot exceed {_iMaxLenght} characters");
                _viewModelGLM00100.Data.CCLOSE_DEPT_CODE =
                    _viewModelGLM00100.Data.CCLOSE_DEPT_CODE.Substring(0, _iMaxLenght);
            }
            await Task.CompletedTask;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        R_DisplayException(loEx);
    }

    private async Task GetUpperCaseAndCheckLengthSA()
    {
        var loEx = new R_Exception();

        try
        {
            var _iMaxLenght = 20;
            var _cMessage = "[INPUT Suspense Account No]";

            _viewModelGLM00100.Data.CSUSPENSE_ACCOUNT_NO = _viewModelGLM00100.Data.CSUSPENSE_ACCOUNT_NO.ToUpper();

            if (_viewModelGLM00100.Data.CSUSPENSE_ACCOUNT_NO.Length > _iMaxLenght)
            {
                loEx.Add("", $"{_cMessage} length cannot exceed {_iMaxLenght} characters");
                _viewModelGLM00100.Data.CSUSPENSE_ACCOUNT_NO =
                    _viewModelGLM00100.Data.CSUSPENSE_ACCOUNT_NO.Substring(0, _iMaxLenght);
            }
            await Task.CompletedTask;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        R_DisplayException(loEx);
    }

    private async Task GetUpperCaseAndCheckLengthREA()
    {
        var loEx = new R_Exception();

        try
        {
            var _iMaxLenght = 20;
            var _cMessage = "[INPUT Retained Earning Account No]";

            _viewModelGLM00100.Data.CRETAINED_ACCOUNT_NO = _viewModelGLM00100.Data.CRETAINED_ACCOUNT_NO.ToUpper();

            if (_viewModelGLM00100.Data.CRETAINED_ACCOUNT_NO.Length > _iMaxLenght)
            {
                loEx.Add("", $"{_cMessage} length cannot exceed {_iMaxLenght} characters");
                _viewModelGLM00100.Data.CRETAINED_ACCOUNT_NO =
                    _viewModelGLM00100.Data.CRETAINED_ACCOUNT_NO.Substring(0, _iMaxLenght);
            }
            await Task.CompletedTask;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        R_DisplayException(loEx);
    }

    #endregion

    #region Button Trigger Warning

    private async Task OnChangedLCOMMIT_IMPJRN(object poParameter)
    {
        if (!_viewModelGLM00100.Data.LCOMMIT_IMPJRN) _viewModelGLM00100.Data.LALLOW_EDIT_IMPJRN_DESC = false;
        await Task.CompletedTask;
    }

    private async Task OnChangedLALLOW_MULTIPLE_JRN(object poParameter)
    {
        if (!_viewModelGLM00100.Data.LALLOW_MULTIPLE_JRN) _viewModelGLM00100.Data.LWARNING_MULTIPLE_JRN = false;
        await Task.CompletedTask;
    }

    private async Task OnChangedLALLOW_DIFF_INTERCOAsync(object poParameter)
    {
        if (!_viewModelGLM00100.Data.LALLOW_DIFF_INTERCO) _viewModelGLM00100.Data.LWARNING_DIFF_INTERCO = false;
        await Task.CompletedTask;
    }

    #endregion

    #region Lookup Button Currency Rate Type Lookup

    private R_Lookup? R_LookupCurrencyRateTypeLookup;

    private void BeforeOpenLookUpCurrencyRateTypeLookup(R_BeforeOpenLookupEventArgs eventArgs)
    {
        var param = new GSL00800ParameterDTO
        {
            CCOMPANY_ID = _clientHelper.CompanyId,
            CUSER_ID = _clientHelper.UserId
        };
        eventArgs.Parameter = param;
        eventArgs.TargetPageType = typeof(GSL00800);
    }

    private void AfterOpenLookUpCurrencyRateTypeLookup(R_AfterOpenLookupEventArgs eventArgs)
    {
        var loTempResult = (GSL00800DTO)eventArgs.Result;
        if (loTempResult == null)
            return;

        var loGetData = (GLM00100DTO)_conductorGLM00100.R_GetCurrentData();
        loGetData.CRATETYPE_CODE = loTempResult.CRATETYPE_CODE;
        loGetData.CRATETYPE_DESCRIPTION = loTempResult.CRATETYPE_DESCRIPTION;

        GetUpperCaseAndCheckLengthCRT();
    }

    #endregion

    #region Lookup Button Closing Entries Department

    private R_Lookup? R_LookupClosingEntriesDepartmentLookup;

    private void BeforeOpenLookUpClosingEntriesDepartmentLookup(R_BeforeOpenLookupEventArgs eventArgs)
    {
        var param = new GSL00700ParameterDTO
        {
            CCOMPANY_ID = _clientHelper.CompanyId,
            CUSER_ID = _clientHelper.UserId
        };
        eventArgs.Parameter = param;
        eventArgs.TargetPageType = typeof(GSL00700);
    }

    private async Task AfterOpenLookUpClosingEntriesDepartmentLookupAsync(R_AfterOpenLookupEventArgs eventArgs)
    {
        var loTempResult = (GSL00700DTO)eventArgs.Result;
        if (loTempResult == null)
            return;

        var loGetData = (GLM00100DTO)_conductorGLM00100.R_GetCurrentData();
        loGetData.CCLOSE_DEPT_CODE = loTempResult.CDEPT_CODE;
        loGetData.CCLOSE_DEPT_NAME = loTempResult.CDEPT_NAME;

        await GetUpperCaseAndCheckLengthCED();
    }

    #endregion

    #region Lookup Button Suspense Account

    private R_Lookup? R_LookupSuspenseAccountLookup;

    private void BeforeOpenLookUpSuspenseAccountLookup(R_BeforeOpenLookupEventArgs eventArgs)
    {
        var param = new GSL00500ParameterDTO
        {
            CCOMPANY_ID = _clientHelper.CompanyId,
            CPROPERTY_ID = "",
            CPROGRAM_CODE = "GLM00100",
            CBSIS = "",
            CDBCR = "",
            LCENTER_RESTR = false,
            LUSER_RESTR = false,
            CUSER_ID = _clientHelper.UserId,
            CCENTER_CODE = "",
            CUSER_LANGUAGE = _clientHelper.CultureUI.TwoLetterISOLanguageName
        };
        eventArgs.Parameter = param;
        eventArgs.TargetPageType = typeof(GSL00500);
    }

    private async Task AfterOpenLookUpSuspenseAccountLookupAsync(R_AfterOpenLookupEventArgs eventArgs)
    {
        var loTempResult = (GSL00500DTO)eventArgs.Result;
        if (loTempResult == null)
            return;

        var loGetData = (GLM00100DTO)_conductorGLM00100.R_GetCurrentData();
        loGetData.CSUSPENSE_ACCOUNT_NO = loTempResult.CGLACCOUNT_NO;
        loGetData.CSUSPENSE_ACCOUNT_NAME = loTempResult.CGLACCOUNT_NAME;

        await GetUpperCaseAndCheckLengthSA();
    }

    #endregion

    #region Lookup Button Retained Earning Account

    private R_Lookup? R_LookupRetainedEarningAccountLookup;

    private void BeforeOpenLookUpRetainedEarningAccountLookup(R_BeforeOpenLookupEventArgs eventArgs)
    {
        var param = new GSL00500ParameterDTO
        {
            CCOMPANY_ID = _clientHelper.CompanyId,
            CPROPERTY_ID = "",
            CPROGRAM_CODE = "GLM00100",
            CBSIS = "",
            CDBCR = "",
            LCENTER_RESTR = false,
            LUSER_RESTR = false,
            CUSER_ID = _clientHelper.UserId,
            CCENTER_CODE = "",
            CUSER_LANGUAGE = _clientHelper.CultureUI.TwoLetterISOLanguageName
        };
        eventArgs.Parameter = param;
        eventArgs.TargetPageType = typeof(GSL00500);
    }

    private async Task AfterOpenLookUpRetainedEarningAccountLookupAsync(R_AfterOpenLookupEventArgs eventArgs)
    {
        var loTempResult = (GSL00500DTO)eventArgs.Result;
        if (loTempResult == null)
            return;

        var loGetData = (GLM00100DTO)_conductorGLM00100.R_GetCurrentData();
        loGetData.CRETAINED_ACCOUNT_NO = loTempResult.CGLACCOUNT_NO;
        loGetData.CRETAINED_ACCOUNT_NAME = loTempResult.CGLACCOUNT_NAME;

        await GetUpperCaseAndCheckLengthREA();
    }

    #endregion
}