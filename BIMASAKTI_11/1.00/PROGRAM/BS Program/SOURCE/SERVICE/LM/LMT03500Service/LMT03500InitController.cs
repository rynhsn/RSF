﻿
using System.Diagnostics;
using LMT03500Back;
using LMT03500Common;
using LMT03500Common.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using R_BackEnd;
using R_Common;

namespace LMT03500Service;

[ApiController]
[Route("api/[controller]/[action]")]
public class LMT03500InitController : ControllerBase, ILMT03500Init
{
    private LoggerLMT03500 _logger;
    private readonly ActivitySource _activitySource;

    public LMT03500InitController(ILogger<LMT03500InitController> logger)
    {
        //Initial and Get Logger
        LoggerLMT03500.R_InitializeLogger(logger);
        _logger = LoggerLMT03500.R_GetInstanceLogger();
        _activitySource = LMT03500Activity.R_InitializeAndGetActivitySource(nameof(LMT03500InitController));
    }
    
    [HttpPost]
    public LMT03500ListDTO<LMT03500PropertyDTO> LMT03500GetPropertyList()
    {
        using var loActivity = _activitySource.StartActivity(nameof(LMT03500GetPropertyList));
        
        _logger.LogInfo("Start - Get Property");
        var loEx = new R_Exception();
        var loCls = new LMT03500InitCls();
        var loDbParams = new LMT03500ParameterDb();
        var loReturn = new LMT03500ListDTO<LMT03500PropertyDTO>();

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
    public LMT03500ListDTO<LMT03500TransCodeDTO> LMT03500GetTransCodeList()
    {
        using var loActivity = _activitySource.StartActivity(nameof(LMT03500GetTransCodeList));
        
        _logger.LogInfo("Start - Get TransCode");
        var loEx = new R_Exception();
        var loCls = new LMT03500InitCls();
        var loDbParams = new LMT03500ParameterDb();
        var loReturn = new LMT03500ListDTO<LMT03500TransCodeDTO>();

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