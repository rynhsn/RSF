using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PMT06500Common;
using PMT06500Back;
using PMT06500Common.DTOs;
using PMT06500Common.Params;
using R_BackEnd;
using R_Common;

namespace PMT06500Service;

[ApiController]
[Route("api/[controller]/[action]")]
public class PMT06500InitController : ControllerBase, IPMT06500Init
{
    private LoggerPMT06500 _logger;
    private readonly ActivitySource _activitySource;

    public PMT06500InitController(ILogger<PMT06500InitController> logger)
    {
        //Initial and Get Logger
        LoggerPMT06500.R_InitializeLogger(logger);
        _logger = LoggerPMT06500.R_GetInstanceLogger();
        _activitySource = PMT06500Activity.R_InitializeAndGetActivitySource(nameof(PMT06500InitController));
    }
    
    [HttpPost]
    public PMT06500ListDTO<PMT06500PropertyDTO> PMT06500GetPropertyList()
    {
        using var loActivity = _activitySource.StartActivity(nameof(PMT06500GetPropertyList));
        
        _logger.LogInfo("Start - Get Property");
        var loEx = new R_Exception();
        var loCls = new PMT06500InitCls();
        var loDbParams = new PMT06500ParameterDb();
        var loReturn = new PMT06500ListDTO<PMT06500PropertyDTO>();

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
    public PMT06500ListDTO<PMT06500PeriodDTO> PMT06500GetPeriodList(PMT06500YearParam poParams)
    {
        using var loActivity = _activitySource.StartActivity(nameof(PMT06500GetPeriodList));
        
        _logger.LogInfo("Start - Get Property");
        var loEx = new R_Exception();
        var loCls = new PMT06500InitCls();
        var loDbParams = new PMT06500ParameterDb();
        var loReturn = new PMT06500ListDTO<PMT06500PeriodDTO>();

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
    public PMT06500SingleDTO<PMT06500YearRangeDTO> PMT06500GetYearRange()
    {
        using var loActivity = _activitySource.StartActivity(nameof(PMT06500GetYearRange));
        _logger.LogInfo("Start - Get Year Range Param");
        var loEx = new R_Exception();
        var loDbParams = new PMT06500ParameterDb();
        var loCls = new PMT06500InitCls();
        var loReturn = new PMT06500SingleDTO<PMT06500YearRangeDTO>();

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
    public PMT06500SingleDTO<PMT06500TransCodeDTO> PMT06500GetTransCode(PMT06500TransCodeParam poParam)
    {
        _logger.LogInfo("Start - Get Transaction Code Record");
        R_Exception loEx = new();
        PMT06500SingleDTO<PMT06500TransCodeDTO> loRtn = new();
        PMT06500ParameterDb loParam = new();

        try
        {
            var loCls = new PMT06500InitCls();
            
            _logger.LogInfo("Set Parameter");
            loParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
            loParam.CTRANS_CODE = poParam.CTRANS_CODE;

            _logger.LogInfo("Get Transaction Code Record");
            loRtn.Data = loCls.GetTransCodeInfo(loParam);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }
        
        loEx.ThrowExceptionIfErrors();
        _logger.LogInfo("End - Get Transaction Code Record");
        return loRtn;
    }
}