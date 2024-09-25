using System.Diagnostics;
using APR00500Back;
using APR00500Common;
using APR00500Common.DTOs;
using APR00500Common.Params;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using R_BackEnd;
using R_Common;

namespace APR00500Service;

[ApiController]
[Route("api/[controller]/[action]")]
public class APR00500Controller : ControllerBase, IAPR00500
{
    private LoggerAPR00500 _logger;
    private readonly ActivitySource _activitySource;

    public APR00500Controller(ILogger<APR00500Controller> logger)
    {
        //Initial and Get Logger
        LoggerAPR00500.R_InitializeLogger(logger);
        _logger = LoggerAPR00500.R_GetInstanceLogger();
        _activitySource = APR00500Activity.R_InitializeAndGetActivitySource(nameof(APR00500Controller));
    }

    [HttpPost]
    public APR00500ListDTO<APR00500PropertyDTO> APR00500GetPropertyList()
    {
        using var loActivity = _activitySource.StartActivity(nameof(APR00500GetPropertyList));
        
        _logger.LogInfo("Start - Get Property");
        var loEx = new R_Exception();
        var loCls = new APR00500Cls();
        var loDbParams = new APR00500ParameterDb();
        var loReturn = new APR00500ListDTO<APR00500PropertyDTO>();

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
    public APR00500SingleDTO<APR00500PeriodYearRangeDTO> APR00500GetYearRange()
    {
        using var loActivity = _activitySource.StartActivity(nameof(APR00500GetYearRange));
        _logger.LogInfo("Start - Get Year Range Param");
        var loEx = new R_Exception();
        var loDbParams = new APR00500ParameterDb();
        var loCls = new APR00500Cls();
        var loReturn = new APR00500SingleDTO<APR00500PeriodYearRangeDTO>();

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

    [HttpPost]
    public APR00500SingleDTO<APR00500SystemParamDTO> APR00500GetSystemParam()
    {
        using var loActivity = _activitySource.StartActivity(nameof(APR00500GetSystemParam));
        _logger.LogInfo("Start - Get System Param");
        var loEx = new R_Exception();
        var loCls = new APR00500Cls();
        var loDbParams = new APR00500ParameterDb();
        var loReturn = new APR00500SingleDTO<APR00500SystemParamDTO>();

        try
        {
            _logger.LogInfo("Set Parameter");
            loDbParams.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
            loDbParams.CLANGUAGE_ID = R_BackGlobalVar.CULTURE;

            _logger.LogInfo("Get System Param");
            loReturn.Data = loCls.GetSystemParam(loDbParams);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }

        loEx.ThrowExceptionIfErrors();
        _logger.LogInfo("End - Get System Param");
        return loReturn;
    }

    [HttpPost]
    public APR00500SingleDTO<APR00500TransCodeInfoDTO> APR00500GetTransCodeInfo()
    {
        using var loActivity = _activitySource.StartActivity(nameof(APR00500GetTransCodeInfo));
        _logger.LogInfo("Start - Get Trans Code Info");

        var loEx = new R_Exception();
        var loCls = new APR00500Cls();
        var loDbParams = new APR00500ParameterDb();
        var loReturn = new APR00500SingleDTO<APR00500TransCodeInfoDTO>();

        try
        {
            _logger.LogInfo("Set Parameter");
            loDbParams.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;

            _logger.LogInfo("Get Trans Code Info");
            loReturn.Data = loCls.GetTransCodeInfo(loDbParams);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }

        loEx.ThrowExceptionIfErrors();
        _logger.LogInfo("End - Get Trans Code Info");
        return loReturn;
    }

    [HttpPost]
    public APR00500ListDTO<APR00500PeriodDTO> APR00500GetPeriodList(APR00500PeriodParam poParam)
    {
        using var loActivity = _activitySource.StartActivity(nameof(APR00500GetPeriodList));
        _logger.LogInfo("Start - Get Period List");

        var loEx = new R_Exception();
        var loCls = new APR00500Cls();
        var loDbParams = new APR00500ParameterDb();
        var loReturn = new APR00500ListDTO<APR00500PeriodDTO>();

        try
        {
            _logger.LogInfo("Set Parameter");
            loDbParams.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
            loDbParams.CYEAR = poParam.CYEAR;

            _logger.LogInfo("Get Period List");
            loReturn.Data = loCls.GetPeriodList(loDbParams);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }

        loEx.ThrowExceptionIfErrors();
        _logger.LogInfo("End - Get Period List");
        return loReturn;
    }

    [HttpPost]
    public APR00500ListDTO<APR00500FunctDTO> APR00500GetCodeInfoList()
    {
        using var loActivity = _activitySource.StartActivity(nameof(APR00500GetCodeInfoList));
        _logger.LogInfo("Start - Get Code Info List");

        var loEx = new R_Exception();
        var loCls = new APR00500Cls();
        var loDbParams = new APR00500ParameterDb();
        var loReturn = new APR00500ListDTO<APR00500FunctDTO>();

        try
        {
            _logger.LogInfo("Set Parameter");
            loDbParams.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
            loDbParams.CLANGUAGE_ID = R_BackGlobalVar.CULTURE;

            _logger.LogInfo("Get Code Info List");
            loReturn.Data = loCls.GetCodeInfoList(loDbParams);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }

        loEx.ThrowExceptionIfErrors();
        _logger.LogInfo("End - Get Code Info List");
        return loReturn;
    }
}