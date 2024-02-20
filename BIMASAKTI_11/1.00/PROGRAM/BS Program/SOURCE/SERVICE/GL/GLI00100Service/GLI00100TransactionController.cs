using System.Diagnostics;
using GLI00100Back;
using GLI00100Common;
using GLI00100Common.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using R_OpenTelemetry;
using R_BackEnd;
using R_Common;

namespace GLI00100Service;

[ApiController]
[Route("api/[controller]/[action]")]
public class GLI00100TransactionController : ControllerBase, IGLI00100Transaction
{
    private LoggerGLI00100 _logger;
    private readonly ActivitySource _activitySource;

    public GLI00100TransactionController(ILogger<GLI00100TransactionController> logger)
    {
        //Initial and Get Logger
        LoggerGLI00100.R_InitializeLogger(logger);
        _logger = LoggerGLI00100.R_GetInstanceLogger();
        _activitySource = GLI00100Activity.R_InitializeAndGetActivitySource(nameof(GLI00100TransactionController));
    }

    [HttpPost]
    public GLI00100JournalDTO GLI00100GetJournalDetail(GLI00100JournalParamDTO poParams)
    {
        using var loActivity = _activitySource.StartActivity(nameof(GLI00100GetJournalDetail));
        _logger.LogInfo("Start - Get Journal Detail");
        var loEx = new R_Exception();
        var loCls = new GLI00100TransactionCls();
        var loDbParam = new GLI00100ParameterDb();
        var loDbOptParam = new GLI00100JournalParamDb();
        GLI00100JournalDTO loReturn = null;
            
        try
        {
            _logger.LogInfo("Set Parameter");
            loDbParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
            loDbOptParam.CUSER_ID = R_BackGlobalVar.USER_ID;
            loDbParam.CLANGUAGE_ID = R_BackGlobalVar.CULTURE;
            loDbOptParam.CREC_ID = poParams.CREC_ID;
            
            _logger.LogInfo("Get Journal Detail");
            loReturn = loCls.GetJournalDetailDb(loDbParam, loDbOptParam);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }
        
        loEx.ThrowExceptionIfErrors();
        _logger.LogInfo("End - Get Journal Detail");
        return loReturn;
    }

    [HttpPost]
    public IAsyncEnumerable<GLI00100JournalGridDTO> GLI00100GetJournalGridStream()
    {
        using var loActivity = _activitySource.StartActivity(nameof(GLI00100GetJournalGridStream));
        _logger.LogInfo("Start - Get Journal List Stream");
        var loEx = new R_Exception();
        var loCls = new GLI00100TransactionCls();
        var loDbParam = new GLI00100ParameterDb();
        var loDbOptParam = new GLI00100JournalParamDb();
        List<GLI00100JournalGridDTO> loResult;
        IAsyncEnumerable<GLI00100JournalGridDTO> loReturn = null;

        try
        {
            _logger.LogInfo("Set Parameter");
            loDbParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
            loDbParam.CLANGUAGE_ID = R_BackGlobalVar.CULTURE;
            loDbOptParam.CREC_ID = R_Utility.R_GetStreamingContext<string>(GLI00100ContextConstant.CREC_ID);
            
            _logger.LogInfo("Get Journal List Stream");
            loResult = loCls.GetJournalListDb(loDbParam, loDbOptParam);
            loReturn = GetStream(loResult);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }

        loEx.ThrowExceptionIfErrors();
        _logger.LogInfo("End - Get Journal List Stream");
        return loReturn;
    }

    #region "Helper ListStream Functions"
    private async IAsyncEnumerable<T> GetStream<T>(List<T> poParameter)
    {
        foreach (var item in poParameter)
        {
            yield return item;
        }
    }
    #endregion
}