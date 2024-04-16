
using System.Diagnostics;
using PMT03500Back;
using PMT03500Common;
using PMT03500Common.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using R_BackEnd;
using R_Common;

namespace LMT03500Service;

[ApiController]
[Route("api/[controller]/[action]")]
public class LMT03500InitController : ControllerBase, IPMT03500Init
{
    private LoggerPMT03500 _logger;
    private readonly ActivitySource _activitySource;

    public LMT03500InitController(ILogger<LMT03500InitController> logger)
    {
        //Initial and Get Logger
        LoggerPMT03500.R_InitializeLogger(logger);
        _logger = LoggerPMT03500.R_GetInstanceLogger();
        _activitySource = PMT03500Activity.R_InitializeAndGetActivitySource(nameof(LMT03500InitController));
    }
    
    [HttpPost]
    public PMT03500ListDTO<PMT03500PropertyDTO> LMT03500GetPropertyList()
    {
        using var loActivity = _activitySource.StartActivity(nameof(LMT03500GetPropertyList));
        
        _logger.LogInfo("Start - Get Property");
        var loEx = new R_Exception();
        var loCls = new PMT03500InitCls();
        var loDbParams = new PMT03500ParameterDb();
        var loReturn = new PMT03500ListDTO<PMT03500PropertyDTO>();

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
    public PMT03500ListDTO<PMT03500TransCodeDTO> LMT03500GetTransCodeList()
    {
        using var loActivity = _activitySource.StartActivity(nameof(LMT03500GetTransCodeList));
        
        _logger.LogInfo("Start - Get TransCode");
        var loEx = new R_Exception();
        var loCls = new PMT03500InitCls();
        var loDbParams = new PMT03500ParameterDb();
        var loReturn = new PMT03500ListDTO<PMT03500TransCodeDTO>();

        try
        {
            _logger.LogInfo("Set Parameter");
            loDbParams.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
            loDbParams.CTRANS_CODE = "";
            
            _logger.LogInfo("Get TransCode");
            loReturn.Data = loCls.GetTransCodeList(loDbParams);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }

        loEx.ThrowExceptionIfErrors();
        _logger.LogInfo("End - Get TransCode");
        return loReturn;
    }
    
}