using BlazorClientHelper;
using Microsoft.AspNetCore.Components;
using PMT01300COMMON;
using PMT01300FrontResources;
using PMT01300MODEL;
using PMT01300ReportCommon;
using R_BlazorFrontEnd.Controls;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd.Helpers;
using R_BlazorFrontEnd.Interfaces;

namespace PMT01300FRONT;

public partial class PMT01300PrintPopup : R_Page
{
    readonly PMT01300ViewModel _viewModel = new();
    [Inject] private R_ILocalizer<Resources_Dummy_Class> _localizer { get; set; }
    [Inject] private R_IReport? _reportService { get; set; }
    [Inject] private IClientHelper? ClientHelper { get; set; }

    protected override async Task R_Init_From_Master(object poParameter)
    {
        var loEx = new R_Exception();

        try
        {
            _viewModel.PrintParam = R_FrontUtility.ConvertObjectToObject<PMT01300ReportParamDTO>(poParameter);

            var poParam =
                new PMT01300ReportTemplateParamDTO()
                {
                    CPROPERTY_ID = _viewModel.PrintParam.CPROPERTY_ID,
                };
            await _viewModel.GetReportTemplateList(poParam);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        R_DisplayException(loEx);
    }

    private async Task OnClickOk()
    {
        R_Exception loEx = new R_Exception();
        try
        {
            await PrintProcess();
            if (!loEx.HasError)
            {
                await Close(true, "SUCCESS");
            }
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }
    
    private async Task PrintProcess()
    {
        var loEx = new R_Exception();
        var loParameterPrint = new PMT01300ReportParamDTO();

        try
        {
            loParameterPrint = _viewModel.PrintParam;
            loParameterPrint.CFILE_NAME = _viewModel.ReportTemplateList
                .FirstOrDefault(x => x.CTEMPLATE_ID == _viewModel.TemplateId)?
                .CFILE_NAME;
            loParameterPrint.CTITLE = "LETTER OF INTENT";
            
            var storageIds = new[] { nameof(PMT01300ReportTemplateDTO.CSIGN_STORAGE_ID01), nameof(PMT01300ReportTemplateDTO.CSIGN_STORAGE_ID02), nameof(PMT01300ReportTemplateDTO.CSIGN_STORAGE_ID03), nameof(PMT01300ReportTemplateDTO.CSIGN_STORAGE_ID04), nameof(PMT01300ReportTemplateDTO.CSIGN_STORAGE_ID05), nameof(PMT01300ReportTemplateDTO.CSIGN_STORAGE_ID06) };
            var signNames = new[] { nameof(PMT01300ReportTemplateDTO.CSIGN_NAME01), nameof(PMT01300ReportTemplateDTO.CSIGN_NAME02), nameof(PMT01300ReportTemplateDTO.CSIGN_NAME03), nameof(PMT01300ReportTemplateDTO.CSIGN_NAME04), nameof(PMT01300ReportTemplateDTO.CSIGN_NAME05), nameof(PMT01300ReportTemplateDTO.CSIGN_NAME06) };
            var signPositions = new[] { nameof(PMT01300ReportTemplateDTO.CSIGN_POSITION01), nameof(PMT01300ReportTemplateDTO.CSIGN_POSITION02), nameof(PMT01300ReportTemplateDTO.CSIGN_POSITION03), nameof(PMT01300ReportTemplateDTO.CSIGN_POSITION04), nameof(PMT01300ReportTemplateDTO.CSIGN_POSITION05), nameof(PMT01300ReportTemplateDTO.CSIGN_POSITION06) };

            for (var i = 0; i < storageIds.Length; i++)
            {
                loParameterPrint.GetType().GetProperty(storageIds[i])!.SetValue(loParameterPrint, _viewModel.ReportTemplateList.Where(x => x.CTEMPLATE_ID == _viewModel.TemplateId).Select(x => x.GetType().GetProperty(storageIds[i])!.GetValue(x)).FirstOrDefault());
                loParameterPrint.GetType().GetProperty(signNames[i])!.SetValue(loParameterPrint, _viewModel.ReportTemplateList.Where(x => x.CTEMPLATE_ID == _viewModel.TemplateId).Select(x => x.GetType().GetProperty(signNames[i])!.GetValue(x)).FirstOrDefault());
                loParameterPrint.GetType().GetProperty(signPositions[i])!.SetValue(loParameterPrint, _viewModel.ReportTemplateList.Where(x => x.CTEMPLATE_ID == _viewModel.TemplateId).Select(x => x.GetType().GetProperty(signPositions[i])!.GetValue(x)).FirstOrDefault());
            }

            await _reportService!.GetReport(
                "R_DefaultServiceUrlPM",
                "PM",
                "rpt/PMT01300Print/PrintReportPost",
                "rpt/PMT01300Print/PrintReportGet",
                loParameterPrint);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
        }

        loEx.ThrowExceptionIfErrors();
    }
}