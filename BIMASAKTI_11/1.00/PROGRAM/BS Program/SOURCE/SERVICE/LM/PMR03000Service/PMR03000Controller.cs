using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PMR03000Back;
using PMR03000Common;
using PMR03000Common.DTOs;
using PMR03000Common.Params;
using R_Common;

namespace PMR03000Service;

[ApiController]
[Route("api/[controller]/[action]")]
public class PMR03000Controller : ControllerBase, IPMR03000
{
    private LoggerPMR03000 _logger; // Logger object
    private readonly ActivitySource _activitySource; // ActivitySource object

    public PMR03000Controller(ILogger<PMR03000Controller> logger)
    {
        // Initialize and get logger
        LoggerPMR03000.R_InitializeLogger(logger);
        _logger = LoggerPMR03000.R_GetInstanceLogger();
        // Initialize and get activity source
        _activitySource = PMR03000Activity.R_InitializeAndGetActivitySource(nameof(PMR03000Controller));
    }

    [HttpPost]
    public async Task<PMR03000ListDTO<PMR03000PropertyDTO>> PMR03000GetPropertyList()
    {
        using var loActivity = _activitySource.StartActivity(nameof(PMR03000GetPropertyList));
        R_Exception loEx = new();
        PMR03000ListDTO<PMR03000PropertyDTO> loReturn = new();
        PMR03000Cls loCls = new();

        try
        {
            _logger.LogInfo("Start - Get Property List");
            loReturn.Data = await loCls.GetPropertyList();
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }

        _logger.LogInfo("End - Get Property List");
        loEx.ThrowExceptionIfErrors();
        return loReturn;
    }

    [HttpPost]
    public async Task<PMR03000ListDTO<PMR03000ReportTemplateDTO>> PMR03000GetReportTemplateList(PMR03000ReportTemplateParam poParam)
    {
        using var loActivity = _activitySource.StartActivity(nameof(PMR03000GetReportTemplateList));
        R_Exception loEx = new();
        PMR03000ListDTO<PMR03000ReportTemplateDTO> loReturn = new();
        PMR03000Cls loCls = new();

        try
        {
            //Param
            _logger.LogInfo("Set Parameter");
            var loParam = R_Utility.R_ConvertObjectToObject<PMR03000ReportTemplateParam, PMR03000ParameterDb>(poParam);
            _logger.LogInfo("Start - Get Report Template List");
            loReturn.Data = await loCls.GetReportTemplate(loParam);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }

        _logger.LogInfo("End - Get Report Template List");
        loEx.ThrowExceptionIfErrors();
        return loReturn;
    }

    [HttpPost]
    public async Task<PMR03000ListDTO<PMR03000PeriodDTO>> PMR03000GetPeriodList(PMR03000PeriodParam poParam)
    {
        using var loActivity = _activitySource.StartActivity(nameof(PMR03000GetPeriodList));
        R_Exception loEx = new();
        PMR03000ListDTO<PMR03000PeriodDTO> loReturn = new();
        PMR03000Cls loCls = new();

        try
        {
            //Param
            _logger.LogInfo("Set Parameter");
            var loParam = R_Utility.R_ConvertObjectToObject<PMR03000PeriodParam, PMR03000ParameterDb>(poParam);
            _logger.LogInfo("Start - Get Period List");
            loReturn.Data = await loCls.GetPeriodList(loParam);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }

        _logger.LogInfo("End - Get Period List");
        loEx.ThrowExceptionIfErrors();
        return loReturn;
    }
}


