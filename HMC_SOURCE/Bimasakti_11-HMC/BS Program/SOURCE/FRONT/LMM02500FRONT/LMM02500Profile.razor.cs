using BlazorClientHelper;
using LMM02500Common.DTO;
using LMM02500Model.ViewModel;
using Microsoft.AspNetCore.Components;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.DataControls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.MessageBox;
using R_BlazorFrontEnd.Enums;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_CommonFrontBackAPI;

namespace LMM02500Front;

public partial class LMM02500Profile : R_Page
{

    /* LMM02510 */


    private R_Conductor? _conductorFullLMM02500;
    private R_Grid<LMM02500ProfileAndTaxInfoDTO>? _gridRefLMM02510;

    private readonly LMM02510ViewModel _viewModelLMM02510 = new();

    private R_TabStrip? _tabStripRef;

    private bool IsSuccess;
    private bool IsChangedValueLMM02500Profile;

    [Inject] private IClientHelper? clientHelper { get; set; }

    protected override async Task R_Init_From_Master(object poParameter)
    {
        var loEx = new R_Exception();

        try
        {
            IsChangedValueLMM02500Profile = false;
            _viewModelLMM02510.loTabParameter = (LMM02500TabParameterDTO)poParameter;

            await _viewModelLMM02510.GetTaxTypeComboBox();
            await _viewModelLMM02510.GetIdTypeComboBox();
            await _viewModelLMM02510.GetTaxCodeComboBox();

            if (_viewModelLMM02510.loTabParameter.CTENANT_GROUP_ID != null)
            {
                await _conductorFullLMM02500?.R_GetEntity(null)!;
            }
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }
        loEx.ThrowExceptionIfErrors();
    }

    private async Task OnClickNextButton()
    {
        R_Exception loException = new R_Exception();
        try
        {
            _viewModelLMM02510.ProfileValidation((LMM02500ProfileAndTaxInfoDTO)_conductorFullLMM02500.R_GetCurrentData());
            if (!loException.HasError)
            {
                IsSuccess = true;
                await _tabStripRef?.SetActiveTabAsync("TaxInfo")!;
            }
        }
        catch (Exception ex)
        {
            loException.Add(ex);
        }
        loException.ThrowExceptionIfErrors();
    }

    private void Profile_BeforeCancel(R_BeforeCancelEventArgs eventArgs)
    {
        IsSuccess = false;
        IsChangedValueLMM02500Profile = false;
    }

    private void OnActiveTabIndexChanging(R_TabStripActiveTabIndexChangingEventArgs eventArgs)
    {
        R_Exception loException = new R_Exception();
        try
        {
            if (_conductorFullLMM02500 != null && _conductorFullLMM02500.R_ConductorMode is R_eConductorMode.Add or R_eConductorMode.Edit)
            {
                if (eventArgs.TabStripTab.Id == "TaxInfo" && IsSuccess)
                {
                    IsSuccess = false;
                }
                else
                {
                    eventArgs.Cancel = true;
                }
            }
        }
        catch (Exception ex)
        {
            loException.Add(ex);
        }
        loException.ThrowExceptionIfErrors();
    }

    private void OnActiveTabIndexChanged(R_TabStripTab eventArgs)
    {
        R_Exception loException = new R_Exception();
        try
        {
            if (_conductorFullLMM02500 != null && (_conductorFullLMM02500.R_ConductorMode == R_eConductorMode.Add || _conductorFullLMM02500.R_ConductorMode == R_eConductorMode.Edit))
            {
                if (eventArgs.Id == "TaxInfo")
                {
                    //new changes
                    if (IsChangedValueLMM02500Profile)
                    {
                        _viewModelLMM02510.Data.TaxInfo.CTAX_TYPE = "";
                        _viewModelLMM02510.Data.TaxInfo.CTAX_ID = "";
                        _viewModelLMM02510.Data.TaxInfo.CTAX_NAME = "";
                        _viewModelLMM02510.Data.TaxInfo.LPPH = false;
                        _viewModelLMM02510.Data.TaxInfo.CID_NO = "";
                        _viewModelLMM02510.Data.TaxInfo.CID_TYPE = "";
                        _viewModelLMM02510.ltCID_EXPIRED_DATE = DateTime.Now;
                        _viewModelLMM02510.Data.TaxInfo.CTAX_CODE = "";
                        _viewModelLMM02510.Data.TaxInfo.NTAX_CODE_LIMIT_AMOUNT = 0;
                        _viewModelLMM02510.Data.TaxInfo.CTAX_ADDRESS = "";
                        _viewModelLMM02510.Data.TaxInfo.CTAX_PHONE1 = "";
                        _viewModelLMM02510.Data.TaxInfo.CTAX_PHONE2 = "";
                        _viewModelLMM02510.Data.TaxInfo.CTAX_EMAIL = "";
                        IsChangedValueLMM02500Profile = false;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            loException.Add(ex);
        }
        loException.ThrowExceptionIfErrors();
    }

    private async Task ServiceGetRecordLMM02510(R_ServiceGetRecordEventArgs eventArgs)
    {
        var loEx = new R_Exception();

        try
        {

            await _viewModelLMM02510.GetEntity();
            eventArgs.Result = _viewModelLMM02510.loEntityLMM02510;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }


    private void R_ValidationLMM02510(R_ValidationEventArgs eventArgs)
    {
        var loException = new R_Exception();

        try
        {
            var loData = (LMM02500ProfileAndTaxInfoDTO)eventArgs.Data;

            if (string.IsNullOrEmpty(loData.Profile.CTENANT_GROUP_ID))
                loException.Add("", "Tenant Group Id cannot be null.");

            if (string.IsNullOrEmpty(loData.Profile.CTENANT_GROUP_NAME))
                loException.Add("", "Tenant Group Name cannot be null.");

            if (string.IsNullOrEmpty(loData.Profile.CADDRESS))
                loException.Add("", "Address cannot be null.");

            if (string.IsNullOrEmpty(loData.Profile.CPIC_NAME))
                loException.Add("", "PIC Name cannot be null.");
        }
        catch (Exception ex)
        {
            loException.Add(ex);
        }

        eventArgs.Cancel = loException.HasError;

        loException.ThrowExceptionIfErrors();
    }

    private async Task ServiceSaveLMM02510(R_ServiceSaveEventArgs eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            var loParam = R_FrontUtility.ConvertObjectToObject<LMM02500ProfileAndTaxInfoDTO>(eventArgs.Data);

            await _viewModelLMM02510.SaveCashTenantGroup(loParam, (eCRUDMode)eventArgs.ConductorMode);
            eventArgs.Result = _viewModelLMM02510.loEntityLMM02510;
            await _conductorFullLMM02500.R_GetEntity(null);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    private async Task OnChanged_LUSE_GROUP_TAX_LMMFull02500(bool poParam)
    {
        var loException = new R_Exception();

        try
        {
            if (poParam)
            {
                var llTrue = await R_MessageBox.Show("", "Please check all tenants tax info under this tenant group!",
                    R_eMessageBoxButtonType.OKCancel);

                switch (llTrue)
                {
                    case R_eMessageBoxResult.Cancel:
                        _viewModelLMM02510.Data.Profile.LUSE_GROUP_TAX = false;
                        break;
                    case R_eMessageBoxResult.OK:
                        _viewModelLMM02510.Data.Profile.LUSE_GROUP_TAX = true;
                        IsChangedValueLMM02500Profile = true;
                        break;
                }
            }
            else
            {
                var llFalse = await R_MessageBox.Show("",
                    "All tenants tax info will overwrite with this tenant group tax info!",
                    R_eMessageBoxButtonType.OKCancel);
                switch (llFalse)
                {
                    case R_eMessageBoxResult.Cancel:
                        _viewModelLMM02510.Data.Profile.LUSE_GROUP_TAX = true;
                        break;
                    case R_eMessageBoxResult.OK:
                        _viewModelLMM02510.Data.Profile.LUSE_GROUP_TAX = false;
                        break;
                }
            }
        }
        catch (Exception ex)
        {
            loException.Add(ex);
        }

        loException.ThrowExceptionIfErrors();
    }

    private async Task ProfileAndTaxInfo_SetOther(R_SetEventArgs eventArgs)
    {
        await InvokeTabEventCallbackAsync(eventArgs.Enable);
    }
    private Task R_AfterAddLMM02510(R_AfterAddEventArgs eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            IsChangedValueLMM02500Profile = false;
            var loParam = (LMM02500ProfileAndTaxInfoDTO)eventArgs.Data;
            _viewModelLMM02510.ltCID_EXPIRED_DATE = DateTime.Now;
            loParam.Profile.CPROPERTY_ID = _viewModelLMM02510.loTabParameter.CPROPERTY_ID;
            loParam.Profile.LUSE_GROUP_TAX = false;
            loParam.Profile.CCREATE_BY = clientHelper.UserId;
            loParam.Profile.DCREATE_DATE = DateTime.Now;
            loParam.Profile.CUPDATE_BY = clientHelper.UserId;
            loParam.Profile.DUPDATE_DATE = DateTime.Now;

            loParam.TaxInfo.LPPH = false;
            loParam.TaxInfo.NTAX_CODE_LIMIT_AMOUNT = 0;
            loParam.TaxInfo.CCREATE_BY = clientHelper.UserId;
            loParam.TaxInfo.DCREATE_DATE = DateTime.Now;
            loParam.TaxInfo.CUPDATE_BY = clientHelper.UserId;
            loParam.TaxInfo.DUPDATE_DATE = DateTime.Now;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();

        return Task.CompletedTask;
    }

    public async Task R_AfterDeleteLMM02510()
    {
        await R_MessageBox.Show("", "Delete Success", R_eMessageBoxButtonType.OK);
    }

    private async Task ServiceDeleteGSM06010(R_ServiceDeleteEventArgs eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            var loData = (LMM02500ProfileAndTaxInfoDTO)eventArgs.Data;
            _viewModelLMM02510.loTabParameter.CTENANT_GROUP_ID = loData.Profile.CTENANT_GROUP_ID;
            _viewModelLMM02510.loTabParameter.CTENANT_GROUP_NAME = loData.Profile.CTENANT_GROUP_NAME;

            await _viewModelLMM02510.GetEntity();
            
            if (_viewModelLMM02510.loEntityLMM02510 != null)
                await _viewModelLMM02510.DeleteTenantGroup(_viewModelLMM02510.loEntityLMM02510);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

}