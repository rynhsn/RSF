using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PMT06000Back;
using PMT06000Common;
using PMT06000Common.DTOs;
using PMT06000Common.Params;
using R_BackEnd;
using R_Common;

namespace PMT06000Service;

[ApiController]
[Route("api/[controller]/[action]")]
public class PMT06000InitController : ControllerBase, IPMT06000Init
{
    private LoggerPMT06000 _logger;
    private readonly ActivitySource _activitySource;

    public PMT06000InitController(ILogger<PMT06000InitController> logger)
    {
        //Initial and Get Logger
        LoggerPMT06000.R_InitializeLogger(logger);
        _logger = LoggerPMT06000.R_GetInstanceLogger();
        _activitySource = PMT06000Activity.R_InitializeAndGetActivitySource(nameof(PMT06000InitController));
    }
    
    [HttpPost]
    public PMT06000ListDTO<PMT06000PropertyDTO> PMT06000GetPropertyList()
    {
        using var loActivity = _activitySource.StartActivity(nameof(PMT06000GetPropertyList));
        
        _logger.LogInfo("Start - Get Property");
        var loEx = new R_Exception();
        var loCls = new PMT06000InitCls();
        var loDbParams = new PMT06000ParameterDb();
        var loReturn = new PMT06000ListDTO<PMT06000PropertyDTO>();

        try
        {
            _logger.LogInfo("Set Parameter");
            loDbParams.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
            loDbParams.CUSER_ID = R_BackGlobalVar.USER_ID;
            
            _logger.LogInfo("Get Property");
            loReturn.Data = loCls.GetPropertyList(loDbParams);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }

        loEx.ThrowExceptionIfErrors();
        _logger.LogInfo("End - Get Property");
        return loReturn;
    }
    
    [HttpPost]
    public PMT06000ListDTO<PMT06000PeriodDTO> PMT06000GetPeriodList(PMT06000YearParam poParams)
    {
        using var loActivity = _activitySource.StartActivity(nameof(PMT06000GetPeriodList));
        
        _logger.LogInfo("Start - Get Property");
        var loEx = new R_Exception();
        var loCls = new PMT06000InitCls();
        var loDbParams = new PMT06000ParameterDb();
        var loReturn = new PMT06000ListDTO<PMT06000PeriodDTO>();

        try
        {
            _logger.LogInfo("Set Parameter");
            loDbParams.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
            loDbParams.CYEAR = poParams.CYEAR;
            
            _logger.LogInfo("Get Property");
            loReturn.Data = loCls.GetPeriodList(loDbParams);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }

        loEx.ThrowExceptionIfErrors();
        _logger.LogInfo("End - Get Property");
        return loReturn;
    }

    [HttpPost]
    public PMT06000SingleDTO<PMT06000YearRangeDTO> PMT06000GetYearRange()
    {
        using var loActivity = _activitySource.StartActivity(nameof(PMT06000GetYearRange));
        _logger.LogInfo("Start - Get Year Range Param");
        var loEx = new R_Exception();
        var loDbParams = new PMT06000ParameterDb();
        var loCls = new PMT06000InitCls();
        var loReturn = new PMT06000SingleDTO<PMT06000YearRangeDTO>();

        try
        {
            _logger.LogInfo("Set Parameter");
            loDbParams.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;

            _logger.LogInfo("Get Year Range Param");
            loReturn.Data = loCls.GetYearRange(loDbParams);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }

        loEx.ThrowExceptionIfErrors();
        _logger.LogInfo("End - Get Year Range Param");
        return loReturn;
    }
}
