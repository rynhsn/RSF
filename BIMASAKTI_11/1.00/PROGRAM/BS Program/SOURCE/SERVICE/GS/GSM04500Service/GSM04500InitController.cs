using GSM04500Back;
using GSM04500Common;
using GSM04500Common.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using R_BackEnd;
using R_Common;

namespace GSM04500Service;

[ApiController]
[Route("api/[controller]/[action]")]
public class GSM04500InitController : IGSM04500Init
{
    private LoggerGSM04500 _logger;

    public GSM04500InitController(ILogger<GSM04500InitController> logger)
    {
        //Initial and Get Logger
        LoggerGSM04500.R_InitializeLogger(logger);
        _logger = LoggerGSM04500.R_GetInstanceLogger();
    }

    [HttpPost]
    public GSM04500ListDTO<GSM04500PropertyDTO> GSM04500GetPropertyList()
    {
        _logger.LogInfo("Start - Get Property List");
        R_Exception loEx = new();
        GSM04500ParameterDb loDbPar;
        GSM04500InitCls loCls;
        GSM04500ListDTO<GSM04500PropertyDTO> loRtn = null;

        try
        {
            _logger.LogInfo("Set Parameter");
            
            loDbPar = new GSM04500ParameterDb(); 
            loDbPar.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
            loDbPar.CUSER_ID = R_BackGlobalVar.USER_ID;

            loCls = new GSM04500InitCls();
            _logger.LogInfo("Get Property List");
            var loResult = loCls.GetPropertyList(loDbPar);
            loRtn = new GSM04500ListDTO<GSM04500PropertyDTO> { Data =  loResult};
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
    
    [HttpPost]
    public GSM04500ListDTO<GSM04500FunctionDTO> GSM04500GetTypeList()
    {
        _logger.LogInfo("Start - Get Journal Group Type List");
        R_Exception loEx = new();
        GSM04500ParameterDb loDbPar;
        GSM04500InitCls loCls;
        GSM04500ListDTO<GSM04500FunctionDTO> loRtn = null;

        try
        {
            _logger.LogInfo("Set Parameter");
            
            loDbPar = new GSM04500ParameterDb(); 
            loDbPar.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
            loDbPar.CLANGUAGE_ID = R_BackGlobalVar.CULTURE_MENU;

            loCls = new GSM04500InitCls();
            _logger.LogInfo("Get Journal Group Type List");
            var loResult = loCls.GetTypeList(loDbPar);
            loRtn = new GSM04500ListDTO<GSM04500FunctionDTO> { Data =  loResult};
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }

        loEx.ThrowExceptionIfErrors();
        _logger.LogInfo("End - Get Journal Group Type List");
        return loRtn;
    }
}