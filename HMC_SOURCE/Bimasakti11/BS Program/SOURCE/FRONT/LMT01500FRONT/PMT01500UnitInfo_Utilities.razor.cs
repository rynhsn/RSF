using BlazorClientHelper;
using PMT01500Common.DTO._3._Unit_Info;
using PMT01500Common.Utilities;
using PMT01500FrontResources;
using PMT01500Model.ViewModel;
using Lookup_GSCOMMON.DTOs;
using Lookup_GSFRONT;
using Lookup_PMFRONT;
using Microsoft.AspNetCore.Components;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.MessageBox;
using R_BlazorFrontEnd.Enums;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_CommonFrontBackAPI;
using Lookup_PMCOMMON.DTOs;
using Lookup_GSModel.ViewModel;
using Lookup_PMModel.ViewModel.LML00400;
using PMT01500Common.Utilities.Front;
using R_BlazorFrontEnd.Controls.Enums;
using R_LockingFront;
using PMT01500Common.DTO._2._Agreement;

namespace PMT01500Front;

public partial class PMT01500UnitInfo_Utilities
{
    private readonly PMT01500UnitInfo_UtilitiesViewModel _viewModelPMT01500UnitInfo_Utilities = new();
    [Inject] private IClientHelper? _clientHelper { get; set; }

    private R_Conductor? _conductorUnitInfo_Utilities;
    private R_Grid<PMT01500UnitInfoUnit_UtilitiesListDTO>? _gridUnitInfo_Utilities;
    //private R_NumericTextBox<decimal>? FocusLabelEdit;

    PMT01500EventCallBackDTO _oEventCallBack = new PMT01500EventCallBackDTO();
    
    private bool _lControlCMeterNo = true;

    protected override async Task R_Init_From_Master(object poParameter)
    {
        var loEx = new R_Exception();

        try
        {
            PMT01500FrontParameterForUnitInfo_UtilitiesDTO loParameter = (PMT01500FrontParameterForUnitInfo_UtilitiesDTO)poParameter;
            //GET PARAMETER
            _viewModelPMT01500UnitInfo_Utilities.loParameterList = loParameter;
            await _viewModelPMT01500UnitInfo_Utilities.GetComboBoxDataCCHARGES_TYPE();
            await _viewModelPMT01500UnitInfo_Utilities.GetComboBoxDataCSTART_INV_PRDYear();
            await _viewModelPMT01500UnitInfo_Utilities.GetComboBoxDataCSTART_INV_PRDMonth();
            _lControlCMeterNo = false;

            //   _viewModelPMT01500UnitInfo_Utilities.loParameterList = (PMT01500GetHeaderParameterDTO)poParameter;

            if (!string.IsNullOrEmpty(_viewModelPMT01500UnitInfo_Utilities.loParameterList.CREF_NO))
            {
                //   _viewModelPMT01500UnitInfo_Utilities.();
                await _gridUnitInfo_Utilities.R_RefreshGrid(null);
            }
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        R_DisplayException(loEx);
    }



    #region Locking

    private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrlPM";
    private const string DEFAULT_MODULE_NAME = "PM";

    protected async override Task<bool> R_LockUnlock(R_LockUnlockEventArgs eventArgs)
    {
        var loEx = new R_Exception();
        var llRtn = false;
        R_LockingFrontResult? loLockResult = null;

        try
        {
            var loData = (PMT01500UnitInfoUnit_UtilitiesDetailDTO)eventArgs.Data;

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
                    Program_Id = "PMT01500",
                    Table_Name = "PMT_AGREEMENT",
                    Key_Value = string.Join("|", _clientHelper.CompanyId, loData.CPROPERTY_ID, loData.CREF_NO)
                };

                loLockResult = await loCls.R_Lock(loLockPar);
            }
            else
            {
                var loUnlockPar = new R_ServiceLockingUnLockParameterDTO
                {
                    Company_Id = _clientHelper.CompanyId,
                    User_Id = _clientHelper.UserId,
                    Program_Id = "PMT01500",
                    Table_Name = "PMT_AGREEMENT",
                    Key_Value = string.Join("|", _clientHelper.CompanyId, loData.CPROPERTY_ID, loData.CREF_NO)
                };

                loLockResult = await loCls.R_UnLock(loUnlockPar);
            }

            llRtn = loLockResult.IsSuccess;
            if (!loLockResult.IsSuccess && loLockResult.Exception != null)
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

    private async Task GetListUnitInfo_Utilities(R_ServiceGetListRecordEventArgs eventArgs)
    {
        var loEx = new R_Exception();
        try
        {
            await _viewModelPMT01500UnitInfo_Utilities.GetUnitInfoList();
            eventArgs.ListEntityResult = _viewModelPMT01500UnitInfo_Utilities.loListPMT01500UnitInfo_Utilities;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }
        loEx.ThrowExceptionIfErrors();
    }
    
    private async Task ServiceGetOneRecord_Utilities(R_ServiceGetRecordEventArgs eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            var loParam = R_FrontUtility.ConvertObjectToObject<PMT01500UnitInfoUnit_UtilitiesDetailDTO>(eventArgs.Data);

            await _viewModelPMT01500UnitInfo_Utilities.GetEntity(loParam);
            eventArgs.Result = _viewModelPMT01500UnitInfo_Utilities.loEntityUnitInfo_Utilities;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }
    
    private async Task ServiceR_Display(R_DisplayEventArgs eventArgs)
    {
        var loException = new R_Exception();

        try
        {
            var loData = (PMT01500UnitInfoUnit_UtilitiesDetailDTO)eventArgs.Data;
            switch (eventArgs.ConductorMode)
            {
                case R_eConductorMode.Normal:
                    if (!string.IsNullOrEmpty(loData.CCHARGES_TYPE))
                    {
                        await OnChangedCCHARGES_TYPE(pcParam: loData.CCHARGES_TYPE!, "DISPLAY");
                    }
                    break;
                case R_eConductorMode.Edit:
                    if (!string.IsNullOrEmpty(loData.CCHARGES_TYPE))
                    {
                        await OnChangedCCHARGES_TYPE(pcParam: loData.CCHARGES_TYPE!, "EDIT");
                    }
                    break;
            }

        }
        catch (Exception ex)
        {
            loException.Add(ex);
        }

        loException.ThrowExceptionIfErrors();
    }

    #region External Handle Function

    private async Task OnChangedCCHARGES_TYPE(string pcParam, string pcMode = "")
    {
        R_Exception loException = new R_Exception();
        PMT01500GetUnitInfo_UtilitiesCMeterNoParameterDTO? loParam;

        try
        {
            var loDataParameter = _viewModelPMT01500UnitInfo_Utilities.loParameterList;
            PMT01500UnitInfoUnit_UtilitiesDetailDTO loData = _viewModelPMT01500UnitInfo_Utilities.Data;
            loData.CCHARGES_TYPE = pcParam;

            loParam = new()
            {
                CPROPERTY_ID = loDataParameter.CPROPERTY_ID,
                CBUILDING_ID = loDataParameter.CBUILDING_ID,
                CFLOOR_ID = loDataParameter.CFLOOR_ID,
                CUNIT_ID = loDataParameter.CUNIT_ID,
                CUTILITY_TYPE = loData.CCHARGES_TYPE
            };
            await _viewModelPMT01500UnitInfo_Utilities.GetComboBoxDataCMETER_NO(loParam);

            if (_viewModelPMT01500UnitInfo_Utilities.loComboBoxDataCMETER_NO.Any())
            {
                _lControlCMeterNo = true;
                if (string.IsNullOrEmpty(pcMode))
                {
                    loData.CMETER_NO = _viewModelPMT01500UnitInfo_Utilities.loComboBoxDataCMETER_NO.FirstOrDefault().CMETER_NO;
                }
                else if (pcMode == "DISPLAY")
                {
                    _lControlCMeterNo = false;
                }
                else if (pcMode == "EDIT")
                {
                    _lControlCMeterNo = true;
                }
            }
            else
            {
                loData.CMETER_NO = "";
                _lControlCMeterNo = false;
                if (string.IsNullOrEmpty(pcMode))
                {
                    loException.Add("", "Data Not Found For \"Meter No\" From Selected Utility Type");
                }
            }


        }
        catch (Exception ex)
        {
            loException.Add(ex);
        }

        R_DisplayException(loException);
    }

    #endregion

    #region Conductor Function

    public async Task AfterDelete()
    {
        await R_MessageBox.Show("", "Delete Success", R_eMessageBoxButtonType.OK);
    }

    private async Task AfterAdd(R_AfterAddEventArgs eventArgs)
    {
        R_Exception loException = new R_Exception();

        try
        {
            var loData = (PMT01500UnitInfoUnit_UtilitiesDetailDTO)eventArgs.Data;
            var loDataViewModel = _viewModelPMT01500UnitInfo_Utilities;
            loData.CCHARGES_TYPE = loDataViewModel.loComboBoxDataCCHARGES_TYPE.First().CCODE;
            await OnChangedCCHARGES_TYPE(loData.CCHARGES_TYPE!, "ADD");
            loData.CMETER_NO = _viewModelPMT01500UnitInfo_Utilities.loComboBoxDataCMETER_NO.Any() ? _viewModelPMT01500UnitInfo_Utilities.loComboBoxDataCMETER_NO.FirstOrDefault().CMETER_NO : "";
            loData.CSTART_INV_PRD_YEAR = loDataViewModel.loComboBoxDataCSTART_INV_PRD_YEAR.First().CYEAR;
            loData.CSTART_INV_PRD_MONTH = loDataViewModel.loComboBoxDataCSTART_INV_PRD_MONTH.First().CPERIOD_NO;

            //await _componentCCHARGES_TYPEDropDownList.FocusAsync();
            //await _componentCBUILDING_IDTextBox.FocusAsync();

        }
        catch (Exception ex)
        {
            loException.Add(ex);
        }

        R_DisplayException(loException);
    }

    private async Task SetOther(R_SetEventArgs eventArgs)
    {
        R_Exception loException = new R_Exception();

        try
        {
            _lControlCMeterNo = false;
            _oEventCallBack.LContractorOnCRUDmode = eventArgs.Enable;
            await InvokeTabEventCallbackAsync(_oEventCallBack);
        }
        catch (Exception ex)
        {
            loException.Add(ex);
        }

        R_DisplayException(loException);
    }

    #endregion

    #region Delete
    
    public async Task ServiceDelete(R_ServiceDeleteEventArgs eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            var loParam = (PMT01500UnitInfoUnit_UtilitiesDetailDTO)eventArgs.Data;
            await _viewModelPMT01500UnitInfo_Utilities.ServiceDelete(loParam);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }
    
    public async Task ServiceAfterDelete()
    {
        await R_MessageBox.Show("", "Delete Success", R_eMessageBoxButtonType.OK);
    }
    
    #endregion

    #region SAVE
    
    private async Task ServiceSave(R_ServiceSaveEventArgs eventArgs)
    {
        var loEx = new R_Exception();
        try
        {
            var loParam = (PMT01500UnitInfoUnit_UtilitiesDetailDTO)eventArgs.Data;
            await _viewModelPMT01500UnitInfo_Utilities.ServiceSave(loParam, (eCRUDMode)eventArgs.ConductorMode);
            eventArgs.Result = _viewModelPMT01500UnitInfo_Utilities.loEntityUnitInfo_Utilities;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }
    
    private void R_Validation(R_ValidationEventArgs eventArgs)
    {
        var loException = new R_Exception();

        try
        {
            var loData = (PMT01500UnitInfoUnit_UtilitiesDetailDTO)eventArgs.Data;
            //await LostFocusCUNIT_ID();


            if (string.IsNullOrWhiteSpace(loData.CCHARGES_ID))
            {
                var loErr = R_FrontUtility.R_GetError(typeof(Resources_PMT01500_Class), "ValidationCharge");
                loException.Add(loErr);
            }

            if (string.IsNullOrWhiteSpace(loData.CMETER_NO))
            {
                var loErr = R_FrontUtility.R_GetError(typeof(Resources_PMT01500_Class), "ValidationMeterNo");
                loException.Add(loErr);
            }

        }
        catch (Exception ex)
        {
            loException.Add(ex);
        }

        eventArgs.Cancel = loException.HasError;


        loException.ThrowExceptionIfErrors();
    }
    
    #endregion

    #region LookupCharge

    private R_Lookup? R_Lookup_Charge;

    private void BeforeOpenLookUp_ChargeID(R_BeforeOpenLookupEventArgs eventArgs)
    {
        var loEx = new R_Exception();
        try
        {
            var param = new LML00400ParameterDTO()
            {
                CCOMPANY_ID = _clientHelper.CompanyId,
                CPROPERTY_ID = _viewModelPMT01500UnitInfo_Utilities.loParameterList.CPROPERTY_ID!,
                CCHARGE_TYPE_ID = _viewModelPMT01500UnitInfo_Utilities.Data.CCHARGES_TYPE!,
                CUSER_ID = _clientHelper.UserId
            };
            eventArgs.Parameter = param;
            eventArgs.TargetPageType = typeof(LML00400);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }
        loEx.ThrowExceptionIfErrors();

    }

    private void AfterOpenLookUp_ChargeID(R_AfterOpenLookupEventArgs eventArgs)
    {
        var loTempResult = (LML00400DTO)eventArgs.Result;
        if (loTempResult == null)
            return;

        var loGetData = (PMT01500UnitInfoUnit_UtilitiesDetailDTO)_conductorUnitInfo_Utilities.R_GetCurrentData();

        loGetData.CCHARGES_ID = loTempResult.CCHARGES_ID;
        loGetData.CCHARGES_NAME = loTempResult.CCHARGES_NAME;

    }

    private async Task OnLostFocusChargeID()
    {
        R_Exception loEx = new R_Exception();

        try
        {
            PMT01500UnitInfoUnit_UtilitiesDetailDTO loGetData = (PMT01500UnitInfoUnit_UtilitiesDetailDTO)_viewModelPMT01500UnitInfo_Utilities.Data;

            if (string.IsNullOrWhiteSpace(_viewModelPMT01500UnitInfo_Utilities.Data.CCHARGES_ID))
            {
                loGetData.CCHARGES_ID = "";
                loGetData.CCHARGES_NAME = "";
                return;
            }

            LookupLML00400ViewModel loLookupViewModel = new LookupLML00400ViewModel();
            LML00400ParameterDTO loParam = new LML00400ParameterDTO()
            {
                CCOMPANY_ID = _clientHelper.CompanyId,
                CPROPERTY_ID = _viewModelPMT01500UnitInfo_Utilities.loParameterList.CPROPERTY_ID!,
                CCHARGE_TYPE_ID = _viewModelPMT01500UnitInfo_Utilities.Data.CCHARGES_TYPE!,
                CUSER_ID = _clientHelper.UserId,
                CSEARCH_TEXT = loGetData.CCHARGES_ID ?? "",
            };

            var loResult = await loLookupViewModel.GetUtitlityCharges(loParam);

            if (loResult == null)
            {
                loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Lookup_GSFrontResources.Resources_Dummy_Class),
                        "_ErrLookup01"));
                loGetData.CCHARGES_ID = "";
                loGetData.CCHARGES_NAME = "";
                //await GLAccount_TextBox.FocusAsync();
            }
            else
            {
                loGetData.CCHARGES_ID = loResult.CCHARGES_ID;
                loGetData.CCHARGES_NAME = loResult.CCHARGES_NAME;
            }
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        R_DisplayException(loEx);
    }

    #endregion

    #region LookupTax

    private R_Lookup? R_Lookup_Tax;

    private void BeforeOpenLookUp_Tax(R_BeforeOpenLookupEventArgs eventArgs)
    {

        var param = new GSL00100ParameterDTO()
        {
            CCOMPANY_ID = _clientHelper.CompanyId,
            CUSER_ID = _clientHelper.UserId
        };
        eventArgs.Parameter = param;
        eventArgs.TargetPageType = typeof(GSL00100);

    }

    private void AfterOpenLookUp_Tax(R_AfterOpenLookupEventArgs eventArgs)
    {
        var loTempResult = (GSL00100DTO)eventArgs.Result;
        if (loTempResult == null)
            return;

        var loGetData = (PMT01500UnitInfoUnit_UtilitiesDetailDTO)_conductorUnitInfo_Utilities.R_GetCurrentData();

        loGetData.CTAX_ID = loTempResult.CTAX_ID;
        loGetData.CTAX_NAME = loTempResult.CTAX_NAME;

    }

    private async Task OnLostFocusTax()
    {
        R_Exception loEx = new R_Exception();

        try
        {
            PMT01500UnitInfoUnit_UtilitiesDetailDTO loGetData = (PMT01500UnitInfoUnit_UtilitiesDetailDTO)_viewModelPMT01500UnitInfo_Utilities.Data;

            if (string.IsNullOrWhiteSpace(_viewModelPMT01500UnitInfo_Utilities.Data.CTAX_ID))
            {
                loGetData.CTAX_ID = "";
                loGetData.CTAX_NAME = "";
                return;
            }

            LookupGSL00100ViewModel loLookupViewModel = new LookupGSL00100ViewModel();
            GSL00100ParameterDTO loParam = new GSL00100ParameterDTO()
            {
                CCOMPANY_ID = _clientHelper.CompanyId,
                CUSER_ID = _clientHelper.UserId,
                CSEARCH_TEXT = loGetData.CTAX_ID ?? "",
            };

            var loResult = await loLookupViewModel.GetSalesTax(loParam);

            if (loResult == null)
            {
                loEx.Add(R_FrontUtility.R_GetError(
                        typeof(Lookup_GSFrontResources.Resources_Dummy_Class),
                        "_ErrLookup01"));
                loGetData.CTAX_ID = "";
                loGetData.CTAX_NAME = "";
                //await GLAccount_TextBox.FocusAsync();
            }
            else
            {
                loGetData.CTAX_ID = loResult.CTAX_ID;
                loGetData.CTAX_NAME = loResult.CTAX_NAME;
            }
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        R_DisplayException(loEx);
    }

    #endregion

}

