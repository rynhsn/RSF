using BlazorClientHelper;
using Lookup_GSCOMMON.DTOs;
using Lookup_GSFRONT;
using Lookup_GSModel.ViewModel;
using Lookup_PMCOMMON.DTOs;
using Lookup_PMFRONT;
using Lookup_PMModel.ViewModel.LML00600;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using PMR03000Common.DTOs;
using PMR03000Common.DTOs.Print;
using PMR03000Model.ViewModel;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Controls.Events;
using R_BlazorFrontEnd.Controls.MessageBox;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_BlazorFrontEnd.Interfaces;

namespace PMR03000Front;

public partial class PMR03000 : R_Page
{
    private PMR03000ViewModel _viewModel = new();
    private PMR03000DistributeViewModel _viewModelDistribute = new();

    [Inject] private R_IReport _reportService { get; set; }
    [Inject] private IClientHelper _clientHelper { get; set; }


    private void StateChangeInvoke()
    {
        StateHasChanged();
    }

    #region HandleError

    private void DisplayErrorInvoke(R_Exception poException)
    {
        R_DisplayException(poException);
    }

    #endregion

    public async Task ShowSuccessInvoke()
    {
        // _processButton.Enabled = true;
        await R_MessageBox.Show("", _localizer["SuccessDistribute"], R_eMessageBoxButtonType.OK);
    }


    protected override async Task R_Init_From_Master(object poParam)
    {
        var loEx = new R_Exception();

        try
        {
            await _viewModel.Init();
            await _setDefaultCustomer();
            await _setDefaultMessage();


            _viewModelDistribute.StateChangeAction = StateChangeInvoke;
            _viewModelDistribute.DisplayErrorAction = DisplayErrorInvoke;
            _viewModelDistribute.ShowSuccessAction = async () => { await ShowSuccessInvoke(); };
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    private async Task _valueChangedProperty(string value)
    {
        var loEx = new R_Exception();
        try
        {
            if (value == _viewModel.ReportParam.CPROPERTY_ID) return;

            _viewModel.ReportParam.CFROM_TENANT = string.Empty;
            _viewModel.ReportParam.CFROM_TENANT_NAME = string.Empty;
            _viewModel.ReportParam.CTO_TENANT = string.Empty;
            _viewModel.ReportParam.CTO_TENANT_NAME = string.Empty;
            _viewModel.ReportParam.CPROPERTY_ID = value;
            _viewModel.ReportParam.CPROPERTY_NAME =
                _viewModel.PropertyList.FirstOrDefault(x => x.CPROPERTY_ID == value)?.CPROPERTY_NAME ?? string.Empty;

            await _setDefaultCustomer();
            await _viewModel.GetReportTemplateList();
            // await _viewModel.GetMessageInfoList();
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    #region Lookup From Customer

    private async Task OnLostFocusFromCustomer()
    {
        var loEx = new R_Exception();

        var loLookupViewModel = new LookupLML00600ViewModel();
        try
        {
            if (string.IsNullOrEmpty(_viewModel.ReportParam.CFROM_TENANT))
            {
                _viewModel.ReportParam.CFROM_TENANT_NAME = "";
                return;
            }

            var param = new LML00600ParameterDTO
            {
                CPROPERTY_ID = _viewModel.ReportParam.CPROPERTY_ID,
                CCUSTOMER_TYPE = _viewModel.CustomerType,
                CSEARCH_TEXT = _viewModel.ReportParam.CFROM_TENANT
            };

            var loResult = await loLookupViewModel.GetTenant(param);

            if (loResult == null)
            {
                loEx.Add(R_FrontUtility.R_GetError(
                    typeof(Lookup_PMFrontResources.Resources_Dummy_Class_LookupPM),
                    "_NotFound"));
                _viewModel.ReportParam.CFROM_TENANT = "";
                _viewModel.ReportParam.CFROM_TENANT_NAME = "";
                goto EndBlock;
            }

            _viewModel.ReportParam.CFROM_TENANT = loResult.CTENANT_ID;
            _viewModel.ReportParam.CFROM_TENANT_NAME = loResult.CTENANT_NAME;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        EndBlock:
        await R_DisplayExceptionAsync(loEx);
    }

    private void BeforeOpenLookupFromCustomer(R_BeforeOpenLookupEventArgs eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            var loParameter = new LML00600ParameterDTO
            {
                CPROPERTY_ID = _viewModel.ReportParam.CPROPERTY_ID,
                CCUSTOMER_TYPE = _viewModel.CustomerType
            };

            eventArgs.Parameter = loParameter;
            eventArgs.TargetPageType = typeof(LML00600);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    private void AfterOpenLookupFromCustomer(R_AfterOpenLookupEventArgs eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            if (eventArgs.Result == null) return;

            var loTempResult = (LML00600DTO)eventArgs.Result;
            _viewModel.ReportParam.CFROM_TENANT = loTempResult.CTENANT_ID;
            _viewModel.ReportParam.CFROM_TENANT_NAME = loTempResult.CTENANT_NAME;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    #endregion

    #region Lookup To Customer

    private async Task OnLostFocusToCustomer()
    {
        var loEx = new R_Exception();

        var loLookupViewModel = new LookupLML00600ViewModel();
        try
        {
            if (string.IsNullOrEmpty(_viewModel.ReportParam.CTO_TENANT))
            {
                _viewModel.ReportParam.CTO_TENANT_NAME = "";
                return;
            }

            var param = new LML00600ParameterDTO
            {
                CPROPERTY_ID = _viewModel.ReportParam.CPROPERTY_ID,
                CCUSTOMER_TYPE = _viewModel.CustomerType,
                CSEARCH_TEXT = _viewModel.ReportParam.CTO_TENANT
            };

            var loResult = await loLookupViewModel.GetTenant(param);

            if (loResult == null)
            {
                loEx.Add(R_FrontUtility.R_GetError(
                    typeof(Lookup_PMFrontResources.Resources_Dummy_Class_LookupPM),
                    "_NotFound"));
                _viewModel.ReportParam.CTO_TENANT = "";
                _viewModel.ReportParam.CTO_TENANT_NAME = "";
                goto EndBlock;
            }

            _viewModel.ReportParam.CTO_TENANT = loResult.CTENANT_ID;
            _viewModel.ReportParam.CTO_TENANT_NAME = loResult.CTENANT_NAME;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        EndBlock:
        await R_DisplayExceptionAsync(loEx);
    }

    private void BeforeOpenLookupToCustomer(R_BeforeOpenLookupEventArgs eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            var loParameter = new LML00600ParameterDTO
            {
                CPROPERTY_ID = _viewModel.ReportParam.CPROPERTY_ID,
                CCUSTOMER_TYPE = _viewModel.CustomerType
            };

            eventArgs.Parameter = loParameter;
            eventArgs.TargetPageType = typeof(LML00600);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    private void AfterOpenLookupToCustomer(R_AfterOpenLookupEventArgs eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            if (eventArgs.Result == null) return;

            var loTempResult = (LML00600DTO)eventArgs.Result;
            _viewModel.ReportParam.CTO_TENANT = loTempResult.CTENANT_ID;
            _viewModel.ReportParam.CTO_TENANT_NAME = loTempResult.CTENANT_NAME;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    #endregion

    #region Lookup Message

    private async Task OnLostFocusMessage()
    {
        var loEx = new R_Exception();

        var loLookupViewModel = new LookupGSL03700ViewModel();
        try
        {
            if (string.IsNullOrEmpty(_viewModel.ReportParam.CMESSAGE_NO))
            {
                _viewModel.ReportParam.CMESSAGE_NAME = "";
                return;
            }

            var param = new GSL03700ParameterDTO
            {
                CMESSAGE_TYPE = "03",
                CSEARCH_TEXT = _viewModel.ReportParam.CMESSAGE_NO,
            };

            var loResult = await loLookupViewModel.GetMessage(param);

            if (loResult == null)
            {
                loEx.Add(R_FrontUtility.R_GetError(
                    typeof(Lookup_GSFrontResources.Resources_Dummy_Class),
                    "_ErrLookup01"));
                _viewModel.ReportParam.CMESSAGE_NO = "";
                _viewModel.ReportParam.CMESSAGE_NAME = "";
                goto EndBlock;
            }

            _viewModel.ReportParam.CMESSAGE_NO = loResult.CMESSAGE_NO;
            _viewModel.ReportParam.CMESSAGE_NAME = loResult.CMESSAGE_NAME;
            _viewModel.ReportParam.TMESSAGE_DESCR_RTF = loResult.TMESSAGE_DESCR_RTF;
            _viewModel.ReportParam.TADDITIONAL_DESCR_RTF = loResult.TADDITIONAL_DESCR_RTF;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        EndBlock:
        await R_DisplayExceptionAsync(loEx);
    }

    private void BeforeOpenLookupMessage(R_BeforeOpenLookupEventArgs eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            var loParameter = new GSL03700ParameterDTO()
            {
                CMESSAGE_TYPE = "03"
            };

            eventArgs.Parameter = loParameter;
            eventArgs.TargetPageType = typeof(GSL03700);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    private void AfterOpenLookupMessage(R_AfterOpenLookupEventArgs eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            if (eventArgs.Result == null) return;

            var loTempResult = (GSL03700DTO)eventArgs.Result;
            _viewModel.ReportParam.CMESSAGE_NO = loTempResult.CMESSAGE_NO;
            _viewModel.ReportParam.CMESSAGE_NAME = loTempResult.CMESSAGE_NAME;
            _viewModel.ReportParam.TMESSAGE_DESCR_RTF = loTempResult.TMESSAGE_DESCR_RTF;
            _viewModel.ReportParam.TADDITIONAL_DESCR_RTF = loTempResult.TADDITIONAL_DESCR_RTF;
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    #endregion

    private void _valueChangedTemplate(string value)
    {
        if (_viewModel.ReportParam.ReportTemplate.CTEMPLATE_ID == value) return;
        _viewModel.ReportParam.ReportTemplate = _viewModel.ReportTemplateList
            .FirstOrDefault(x => x.CTEMPLATE_ID == value) ?? new PMR03000ReportTemplateDTO();
    }

    private async Task Distribute(MouseEventArgs eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            var loConfrim = await R_MessageBox.Show(_localizer["Confirmation"], _localizer["ConfirmSendEmail"],
                R_eMessageBoxButtonType.YesNo);
            if (loConfrim == R_eMessageBoxResult.No) return;

            _setParamBeforePrint();

            _viewModel.ReportParam.CFILE_NAME =
                _viewModel.ReportTemplateList.FirstOrDefault(x =>
                    x.CTEMPLATE_ID == _viewModel.ReportParam.ReportTemplate.CTEMPLATE_ID)!.CFILE_NAME;

            _viewModel.ReportParam.CCOMPANY_ID = _clientHelper.CompanyId;
            _viewModel.ReportParam.CUSER_ID = _clientHelper.UserId;
            await _viewModelDistribute.DistributeReport(_viewModel.ReportParam);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    private async Task Print(MouseEventArgs arg)
    {
        var loEx = new R_Exception();
        var loParameterPrint = new PMR03000ReportParamDTO();

        try
        {
            _setParamBeforePrint();

            loParameterPrint = _viewModel.ReportParam;

            //parameter object kaya gini gak bakal ketangkep sama controller report nya (dibiarin biar tahu aja kalo ini gak bisa)
            loParameterPrint.ReportTemplate = _viewModel.ReportTemplateList
                                                  .FirstOrDefault(x =>
                                                      x.CTEMPLATE_ID == _viewModel.ReportParam.ReportTemplate
                                                          .CTEMPLATE_ID) ??
                                              new PMR03000ReportTemplateDTO();
            loParameterPrint.CFILE_NAME =
                _viewModel.ReportTemplateList.FirstOrDefault(x =>
                    x.CTEMPLATE_ID == _viewModel.ReportParam.ReportTemplate.CTEMPLATE_ID)!.CFILE_NAME;

            await _reportService!.GetReport(
                "R_DefaultServiceUrlPM",
                "PM",
                "rpt/PMR03000Print/PrintReportPost",
                "rpt/PMR03000Print/PrintReportGet",
                loParameterPrint);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    private void _setParamBeforePrint()
    {
        _viewModel.ReportParam.CPROPERTY_NAME = _viewModel.PropertyList
            .FirstOrDefault(x => x.CPROPERTY_ID == _viewModel.ReportParam.CPROPERTY_ID)?.CPROPERTY_NAME ?? string.Empty;
        _viewModel.ReportParam.CPERIOD =
            _viewModel.ReportParam.IPERIOD_YEAR.ToString() + _viewModel.ReportParam.CPERIOD_MONTH;
        // _viewModel.ReportParam.CMESSAGE_NO = _viewModel.MessageInfo.CMESSAGE_NO;
        // _viewModel.ReportParam.CMESSAGE_TYPE = _viewModel.MessageInfo.CMESSAGE_TYPE;
    }

    private async Task _setDefaultCustomer()
    {
        var loEx = new R_Exception();

        try
        {
            if (string.IsNullOrEmpty(_viewModel.ReportParam.CPROPERTY_ID)) return;

            var loLookupViewModel = new LookupLML00600ViewModel();
            var param = new LML00600ParameterDTO
            {
                CPROPERTY_ID = _viewModel.ReportParam.CPROPERTY_ID,
                CCUSTOMER_TYPE = _viewModel.CustomerType,
                CSEARCH_TEXT = _viewModel.ReportParam.CTO_TENANT
            };
            await loLookupViewModel.GetTenantList(param);
            if (loLookupViewModel.TenantList.Count > 0)
            {
                _viewModel.ReportParam.CFROM_TENANT = loLookupViewModel.TenantList.FirstOrDefault()?.CTENANT_ID;
                _viewModel.ReportParam.CFROM_TENANT_NAME = loLookupViewModel.TenantList
                    .Where(x => x.CTENANT_ID == _viewModel.ReportParam.CFROM_TENANT)
                    .Select(x => x.CTENANT_NAME).FirstOrDefault() ?? string.Empty;
                _viewModel.ReportParam.CTO_TENANT = loLookupViewModel.TenantList.LastOrDefault()?.CTENANT_ID;
                _viewModel.ReportParam.CTO_TENANT_NAME = loLookupViewModel.TenantList
                    .Where(x => x.CTENANT_ID == _viewModel.ReportParam.CTO_TENANT)
                    .Select(x => x.CTENANT_NAME).FirstOrDefault() ?? string.Empty;
            }
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    private async Task _setDefaultMessage()
    {
        var loEx = new R_Exception();

        try
        {
            var loLookupViewModel = new LookupGSL03700ViewModel();
            var loParameter = new GSL03700ParameterDTO()
            {
                CMESSAGE_TYPE = "03"
            };

            await loLookupViewModel.GetMessageList(loParameter);
            if (loLookupViewModel.MessageGrid.Count > 0)
            {
                _viewModel.ReportParam.CMESSAGE_NO = loLookupViewModel.MessageGrid.FirstOrDefault()?.CMESSAGE_NO;
                _viewModel.ReportParam.CMESSAGE_NAME = loLookupViewModel.MessageGrid
                    .Where(x => x.CMESSAGE_NO == _viewModel.ReportParam.CMESSAGE_NO)
                    .Select(x => x.CMESSAGE_NAME).FirstOrDefault() ?? string.Empty;
                _viewModel.ReportParam.TMESSAGE_DESCR_RTF = loLookupViewModel.MessageGrid
                    .Where(x => x.CMESSAGE_NO == _viewModel.ReportParam.CMESSAGE_NO)
                    .Select(x => x.TMESSAGE_DESCR_RTF).FirstOrDefault() ?? string.Empty;
                _viewModel.ReportParam.TADDITIONAL_DESCR_RTF = loLookupViewModel.MessageGrid
                    .Where(x => x.CMESSAGE_NO == _viewModel.ReportParam.CMESSAGE_NO)
                    .Select(x => x.TADDITIONAL_DESCR_RTF).FirstOrDefault() ?? string.Empty;
            }
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }

    // private void _valueChangedMessage(string value)
    // {
    //     if (_viewModel.MessageInfo.CMESSAGE_NO == value) return;
    //     _viewModel.MessageInfo = _viewModel.MessageInfoList.FirstOrDefault(x => x.CMESSAGE_NO == value) ?? new PMR03000MessageInfoDTO();
    // }
}