using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PMR02600Back;
using PMR02600Common;
using PMR02600Common.DTOs;
using R_BackEnd;
using R_Common;

namespace PMR02600Service;

[ApiController]
[Route("api/[controller]/[action]")]
public class PMR02600Controller : ControllerBase, IPMR02600
{
    private LoggerPMR02600 _logger;
    private readonly ActivitySource _activitySource;

    public PMR02600Controller(ILogger<PMR02600Controller> logger)
    {
        //Initial and Get Logger
        LoggerPMR02600.R_InitializeLogger(logger);
        _logger = LoggerPMR02600.R_GetInstanceLogger();
        _activitySource = PMR02600Activity.R_InitializeAndGetActivitySource(nameof(PMR02600Controller));
    }
    
    [HttpPost]
    public PMR02600ListDTO<PMR02600PropertyDTO> PMR02600GetPropertyList()
    {
        using var loActivity = _activitySource.StartActivity(nameof(PMR02600GetPropertyList));
        
        _logger.LogInfo("Start - Get Property");
        var loEx = new R_Exception();
        var loCls = new PMR02600Cls();
        var loDbParams = new PMR02600ParameterDb();
        var loReturn = new PMR02600ListDTO<PMR02600PropertyDTO>();

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
    

}