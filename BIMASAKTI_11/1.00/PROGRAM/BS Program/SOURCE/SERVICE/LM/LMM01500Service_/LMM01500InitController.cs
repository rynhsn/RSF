using LMM01500Back;
using LMM01500Common;
using LMM01500Common.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using R_BackEnd;
using R_Common;

namespace LMM01500Service;

[ApiController]
[Route("api/[controller]/[action]")]
public class LMM01500InitController : ILMM01500Init
{
    private LoggerLMM01500 _logger;

    public LMM01500InitController(ILogger<LMM01500InitController> logger)
    {
        //Initial and Get Logger
        LoggerLMM01500.R_InitializeLogger(logger);
        _logger = LoggerLMM01500.R_GetInstanceLogger();
    }

    [HttpPost]
    public LMM01500ListDTO<LMM01500PropertyDTO> LMM01500GetPropertyList()
    {
        _logger.LogInfo("Start - Get Property List");
        R_Exception loEx = new();
        LMM01500ParameterDb loDbPar;
        LMM01500InitCls loCls;
        LMM01500ListDTO<LMM01500PropertyDTO> loRtn = null;

        try
        {
            _logger.LogInfo("Set Parameter");

            loDbPar = new LMM01500ParameterDb();
            loDbPar.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
            loDbPar.CUSER_ID = R_BackGlobalVar.USER_ID;

            loCls = new LMM01500InitCls();
            _logger.LogInfo("Get Property List");
            var loResult = loCls.GetPropertyList(loDbPar);
            loRtn = new LMM01500ListDTO<LMM01500PropertyDTO> { Data = loResult };
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }

        loEx.ThrowExceptionIfErrors();
        _logger.LogInfo("End - Get Property List");
        return loRtn;
    }
}