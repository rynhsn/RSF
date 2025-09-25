using System.Diagnostics;
using GSM05000Back;
using GSM05000Common;
using GSM05000Common.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using R_OpenTelemetry;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;

namespace GSM05000Service;

[ApiController]
[Route("api/[controller]/[action]")]
public class GSM05000Controller : ControllerBase, IGSM05000
{
    private LoggerGSM05000 _logger;
    private readonly ActivitySource _activitySource;
    
    public GSM05000Controller(ILogger<GSM05000Controller> logger)
    {
        //Initial and Get Logger
        LoggerGSM05000.R_InitializeLogger(logger);
        _logger = LoggerGSM05000.R_GetInstanceLogger();
        _activitySource = GSM05000Activity.R_InitializeAndGetActivitySource(nameof(GSM05000Controller));
    }
    
    [HttpPost]
    public R_ServiceGetRecordResultDTO<GSM05000DTO> R_ServiceGetRecord(R_ServiceGetRecordParameterDTO<GSM05000DTO> poParameter)
    {
        using var loActivity = _activitySource.StartActivity(nameof(R_ServiceGetRecord));
        _logger.LogInfo("Start - Get Transaction Code Record");
        R_Exception loEx = new();
        R_ServiceGetRecordResultDTO<GSM05000DTO> loRtn = new();

        try
        {
            var loCls = new GSM05000Cls();
            
            _logger.LogInfo("Set Parameter");
            poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
            
            _logger.LogInfo("Get Transaction Code Record");
            loRtn.data = loCls.R_GetRecord(poParameter.Entity);
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
    
    [HttpPost]
    public R_ServiceSaveResultDTO<GSM05000DTO> R_ServiceSave(R_ServiceSaveParameterDTO<GSM05000DTO> poParameter)
    {
        using var loActivity = _activitySource.StartActivity(nameof(R_ServiceSave));
        _logger.LogInfo("Start - Save Transaction Code Entity");
        R_Exception loEx = new R_Exception();
        R_ServiceSaveResultDTO<GSM05000DTO> loRtn = new();
        GSM05000Cls loCls;

        try
        {
            loCls = new GSM05000Cls();
            
            _logger.LogInfo("Set Parameter");
            poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
            poParameter.Entity.CUPDATE_BY = R_BackGlobalVar.USER_ID;
            
            _logger.LogInfo("Save Transaction Code Entity");
            loRtn.data = loCls.R_Save(poParameter.Entity, poParameter.CRUDMode);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }
        
        loEx.ThrowExceptionIfErrors();
        _logger.LogInfo("End - Save Transaction Code Entity");
        return loRtn;
    }

    [HttpPost]
    public R_ServiceDeleteResultDTO R_ServiceDelete(R_ServiceDeleteParameterDTO<GSM05000DTO> poParameter)
    {
        throw new NotImplementedException();
    }
    
    [HttpPost]
    public IAsyncEnumerable<GSM05000GridDTO> GetTransactionCodeListStream()
    {
        using var loActivity = _activitySource.StartActivity(nameof(GetTransactionCodeListStream));
        _logger.LogInfo("Start - Get Transaction Code List");
        R_Exception loEx = new R_Exception();
        IAsyncEnumerable<GSM05000GridDTO> loRtn = null;
        List<GSM05000GridDTO> loResult;
        GSM05000ParameterDb loDbPar;
        GSM05000Cls loCls;

        try
        {
            loDbPar = new GSM05000ParameterDb();
            
            _logger.LogInfo("Set Parameter");
            loDbPar.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
            loDbPar.CUSER_ID = R_BackGlobalVar.USER_ID;
            
            loCls = new GSM05000Cls();
            
            _logger.LogInfo("Get Transaction Code List");
            loResult = loCls.GetTransactionCodeListDb(loDbPar);
            loRtn = GetTransactionCodeStream(loResult);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }
        
        loEx.ThrowExceptionIfErrors();
        _logger.LogInfo("End - Get Transaction Code List");
        return loRtn;
    }

    [HttpPost]
    public GSM05000ListDTO<GSM05000DelimiterDTO> GetDelimiterList()
    {
        using var loActivity = _activitySource.StartActivity(nameof(GetDelimiterList));
        _logger.LogInfo("Start - Get Delimiter List");
        R_Exception loEx = new();
        GSM05000ListDTO<GSM05000DelimiterDTO> loRtn = null;
        List<GSM05000DelimiterDTO> loResult;
        GSM05000ParameterDb loDbPar;
        GSM05000Cls loCls;

        try
        {
            loDbPar = new GSM05000ParameterDb();
            
            _logger.LogInfo("Set Parameter");
            loDbPar.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
            loDbPar.CLANGUAGE_ID = R_BackGlobalVar.CULTURE_MENU;
            
            loCls = new GSM05000Cls();
            
            _logger.LogInfo("Get Delimiter List");
            loResult = loCls.GetDelimiterListDb(loDbPar);
            loRtn = new GSM05000ListDTO<GSM05000DelimiterDTO> { Data = loResult };
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }

        loEx.ThrowExceptionIfErrors();
        _logger.LogInfo("End - Get Delimiter List");
        return loRtn;
    }

    [HttpPost]
    public GSM05000ExistDTO CheckExistData(GSM05000TrxCodeParamsDTO poParams)
    {
        using var loActivity = _activitySource.StartActivity(nameof(CheckExistData));
        _logger.LogInfo("Start - Check Exist Data");
        R_Exception loEx = new();
        GSM05000ExistDTO loRtn = null;
        GSM05000ExistDTO loResult;
        GSM05000ParameterDb loDbPar;
        GSM05000Cls loCls;

        try
        {
            loDbPar = new GSM05000ParameterDb();
            
            _logger.LogInfo("Set Parameter");
            loDbPar.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
            loDbPar.CTRANS_CODE = poParams.CTRANS_CODE;
            loDbPar.ETAB_NAME = poParams.ETAB_NAME;
            
            loCls = new GSM05000Cls();
            
            _logger.LogInfo("Check Exist Data");
            loResult = loCls.GetValidateUpdateDb(loDbPar);
            loRtn = loResult ?? new GSM05000ExistDTO() { EXIST = 0 };
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }
        
        loEx.ThrowExceptionIfErrors();
        _logger.LogInfo("End - Check Exist Data");
        return loRtn;
    }
    
    
    #region "Helper ListStream Functions"
    private async IAsyncEnumerable<GSM05000GridDTO> GetTransactionCodeStream(List<GSM05000GridDTO> poParameter)
    {
        foreach (GSM05000GridDTO item in poParameter)
        {
            yield return item;
        }
    }
    #endregion
}