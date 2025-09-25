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
public class GLI00100AccountJournalController : ControllerBase, IGLI00100AccountJournal
{
    private LoggerGLI00100 _logger;
    private readonly ActivitySource _activitySource;

    public GLI00100AccountJournalController(ILogger<GLI00100AccountJournalController> logger)
    {
        //Initial and Get Logger
        LoggerGLI00100.R_InitializeLogger(logger);
        _logger = LoggerGLI00100.R_GetInstanceLogger();
        _activitySource = GLI00100Activity.R_InitializeAndGetActivitySource(nameof(GLI00100AccountJournalController));
    }

    [HttpPost]
    public GLI00100AccountAnalysisDetailDTO GLI00100GetAccountAnalysisDetail(
        GLI00100AccountAnalysisDetailParamDTO poParams)
    {
        using var loActivity = _activitySource.StartActivity(nameof(GLI00100GetAccountAnalysisDetail));
        _logger.LogInfo("Start - Get Account Analysis Detail");
        var loEx = new R_Exception();
        var loCls = new GLI00100AccountJournalCls();
        var loDbParam = new GLI00100ParameterDb();
        var loDbOptParam = new GLI00100AccountAnalysisDetailTransactionParamDb();
        GLI00100AccountAnalysisDetailDTO loReturn = new();
        
        try
        {
            _logger.LogInfo("Set Parameter");
            loDbParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
            loDbParam.CLANGUAGE_ID = R_BackGlobalVar.CULTURE;
            loDbOptParam.CGLACCOUNT_NO = poParams.CGLACCOUNT_NO;
            loDbOptParam.CPERIOD = poParams.CPERIOD;
            loDbOptParam.CCURRENCY_TYPE = poParams.CCURRENCY_TYPE;
            loDbOptParam.CCENTER_CODE = poParams.CCENTER_CODE;
            
            _logger.LogInfo("Get Account Analysis Detail");
            var loTempReturn = loCls.GetAccountAnalysisDetailDb(loDbParam, loDbOptParam);
            
            // if (loTempReturn == null)
            // {
            //     loReturn.CGLACCOUNT_NO = poParams.CGLACCOUNT_NO;
            //     loReturn.CCENTER_CODE = poParams.CCENTER_CODE;
            //     loReturn.CCURRENCY = poParams.CCURRENCY_TYPE;
            //     loReturn.NOPENING = 0;
            //     loReturn.NPTD_DEBIT = 0;
            //     loReturn.NPTD_CREDIT = 0;
            //     loReturn.NPTD_DEBIT_ADJ = 0;
            //     loReturn.NPTD_CREDIT_ADJ = 0;
            //     loReturn.NENDING = 0;
            // }
            // else
            // {
                loReturn = loTempReturn;
            // }
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);;
        }
        
        loEx.ThrowExceptionIfErrors();
        _logger.LogInfo("End - Get Account Analysis Detail");
        return loReturn;
    }

    [HttpPost]
    public IAsyncEnumerable<GLI00100TransactionGridDTO> GLI00100GetTransactionGridStream()
    {
        using var loActivity = _activitySource.StartActivity(nameof(GLI00100GetTransactionGridStream));
        _logger.LogInfo("Start - Get Transaction Grid Stream");
        var loEx = new R_Exception();
        var loCls = new GLI00100AccountJournalCls();
        var loDbParam = new GLI00100ParameterDb();
        var loDbOptParam = new GLI00100AccountAnalysisDetailTransactionParamDb();
        List<GLI00100TransactionGridDTO> loResult;
        IAsyncEnumerable<GLI00100TransactionGridDTO> loReturn = null;

        try
        {
            _logger.LogInfo("Set Parameter");
            loDbParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
            loDbParam.CLANGUAGE_ID = R_BackGlobalVar.CULTURE;
            loDbOptParam.CGLACCOUNT_NO = R_Utility.R_GetStreamingContext<string>(GLI00100ContextConstant.CGLACCOUNT_NO);
            loDbOptParam.CPERIOD = R_Utility.R_GetStreamingContext<string>(GLI00100ContextConstant.CPERIOD);
            loDbOptParam.CCURRENCY_TYPE = R_Utility.R_GetStreamingContext<string>(GLI00100ContextConstant.CCURRENCY_TYPE);
            loDbOptParam.CCENTER_CODE = R_Utility.R_GetStreamingContext<string>(GLI00100ContextConstant.CCENTER_CODE);
            
            _logger.LogInfo("Get Transaction Grid Stream");
            loResult = loCls.GetTransactionListDb(loDbParam, loDbOptParam);
            loReturn = GetStream(loResult);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);;
        }
        
        loEx.ThrowExceptionIfErrors();
        _logger.LogInfo("End - Get Transaction Grid Stream");
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