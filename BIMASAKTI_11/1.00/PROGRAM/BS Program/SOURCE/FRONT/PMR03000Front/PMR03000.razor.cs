using BlazorClientHelper;
using Lookup_PMCOMMON.DTOs;
using Lookup_PMFRONT;
using Lookup_PMModel.ViewModel.LML00600;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using PMR03000Common.DTOs;
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

    [Inject] private R_IReport _reportService { get; set; }
    [Inject] private IClientHelper _clientHelper { get; set; }

    protected override async Task R_Init_From_Master(object poParam)
    {
        var loEx = new R_Exception();

        try
        {
            await _viewModel.Init();
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
            await _viewModel.GetReportTemplateList();
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

    private void _valueChangedTemplate(string value)
    {
        if (_viewModel.ReportParam.ReportTemplate.CTEMPLATE_ID == value) return;
        _viewModel.ReportParam.ReportTemplate = _viewModel.ReportTemplateList
            .FirstOrDefault(x => x.CTEMPLATE_ID == value) ?? new PMR03000ReportTemplateDTO();
    }

    private async Task SendEmail(MouseEventArgs eventArgs)
    {
        var loEx = new R_Exception();

        try
        {
            var loConfrim = await R_MessageBox.Show(_localizer["Confirmation"], _localizer["ConfirmSendEmail"],
                R_eMessageBoxButtonType.YesNo);
            if (loConfrim == R_eMessageBoxResult.No) return;
            
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
            
            setParameterBeforePrint();
            
            loParameterPrint = _viewModel.ReportParam;
            loParameterPrint.ReportTemplate = _viewModel.ReportTemplateList
                .FirstOrDefault(x => x.CTEMPLATE_ID == _viewModel.ReportParam.ReportTemplate.CTEMPLATE_ID) ?? new PMR03000ReportTemplateDTO();
            loParameterPrint.CFILE_NAME = _viewModel.ReportTemplateList
                .FirstOrDefault(x => x.CTEMPLATE_ID == _viewModel.ReportParam.ReportTemplate.CTEMPLATE_ID)?
                .CFILE_NAME;
            // loParameterPrint.CTITLE = "LETTER OF INTENT";
            
            // var storageIds = new[] { nameof(PMR03000ReportTemplateDTO.CSIGN_STORAGE_ID01), nameof(PMR03000ReportTemplateDTO.CSIGN_STORAGE_ID02), nameof(PMR03000ReportTemplateDTO.CSIGN_STORAGE_ID03), nameof(PMR03000ReportTemplateDTO.CSIGN_STORAGE_ID04), nameof(PMR03000ReportTemplateDTO.CSIGN_STORAGE_ID05), nameof(PMR03000ReportTemplateDTO.CSIGN_STORAGE_ID06) };
            // var signNames = new[] { nameof(PMR03000ReportTemplateDTO.CSIGN_NAME01), nameof(PMR03000ReportTemplateDTO.CSIGN_NAME02), nameof(PMR03000ReportTemplateDTO.CSIGN_NAME03), nameof(PMR03000ReportTemplateDTO.CSIGN_NAME04), nameof(PMR03000ReportTemplateDTO.CSIGN_NAME05), nameof(PMR03000ReportTemplateDTO.CSIGN_NAME06) };
            // var signPositions = new[] { nameof(PMR03000ReportTemplateDTO.CSIGN_POSITION01), nameof(PMR03000ReportTemplateDTO.CSIGN_POSITION02), nameof(PMR03000ReportTemplateDTO.CSIGN_POSITION03), nameof(PMR03000ReportTemplateDTO.CSIGN_POSITION04), nameof(PMR03000ReportTemplateDTO.CSIGN_POSITION05), nameof(PMR03000ReportTemplateDTO.CSIGN_POSITION06) };

            // for (var i = 0; i < storageIds.Length; i++)
            // {
            //     loParameterPrint.GetType().GetProperty(storageIds[i])!.SetValue(loParameterPrint, _viewModel.ReportTemplateList.Where(x => x.CTEMPLATE_ID == _viewModel.TemplateId).Select(x => x.GetType().GetProperty(storageIds[i])!.GetValue(x)).FirstOrDefault());
            //     loParameterPrint.GetType().GetProperty(signNames[i])!.SetValue(loParameterPrint, _viewModel.ReportTemplateList.Where(x => x.CTEMPLATE_ID == _viewModel.TemplateId).Select(x => x.GetType().GetProperty(signNames[i])!.GetValue(x)).FirstOrDefault());
            //     loParameterPrint.GetType().GetProperty(signPositions[i])!.SetValue(loParameterPrint, _viewModel.ReportTemplateList.Where(x => x.CTEMPLATE_ID == _viewModel.TemplateId).Select(x => x.GetType().GetProperty(signPositions[i])!.GetValue(x)).FirstOrDefault());
            // }

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
    
    private void setParameterBeforePrint()
    {
        _viewModel.ReportParam.CPROPERTY_NAME = _viewModel.PropertyList
            .FirstOrDefault(x => x.CPROPERTY_ID == _viewModel.ReportParam.CPROPERTY_ID)?.CPROPERTY_NAME ?? string.Empty;
        _viewModel.ReportParam.CPERIOD = _viewModel.ReportParam.IPERIOD_YEAR.ToString() + _viewModel.ReportParam.CPERIOD_MONTH;
    }
}