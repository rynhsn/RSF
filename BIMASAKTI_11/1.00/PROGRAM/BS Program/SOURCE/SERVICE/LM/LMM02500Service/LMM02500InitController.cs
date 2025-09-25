using LMM02500Back;
using LMM02500Common;
using LMM02500Common.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using R_BackEnd;
using R_Common;

namespace LMM02500Service;

[ApiController]
[Route("api/[controller]/[action]")]
public class LMM02500InitController : ILMM02500Init
{
    private LoggerLMM02500 _logger;

    public LMM02500InitController(ILogger<LMM02500InitController> logger)
    {
        LoggerLMM02500.R_InitializeLogger(logger);
        _logger = LoggerLMM02500.R_GetInstanceLogger();
    }

    [HttpPost]
    public LMM02500ListDTO<LMM02500PropertyDTO> LMM02500GetPropertyList()
    {
        _logger.LogInfo("Start - Get Property List");
        R_Exception loEx = new();
        LMM02500ParameterDb loDbPar;
        LMM02500InitCls loCls;
        LMM02500ListDTO<LMM02500PropertyDTO> loRtn = null;

        try
        {
            _logger.LogInfo("Set Parameter");

            loDbPar = new LMM02500ParameterDb();
            loDbPar.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
            loDbPar.CUSER_ID = R_BackGlobalVar.USER_ID;

            loCls = new LMM02500InitCls();
            _logger.LogInfo("Get Property List");
            var loResult = loCls.GetPropertyList(loDbPar);
            loRtn = new LMM02500ListDTO<LMM02500PropertyDTO> { Data = loResult };
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