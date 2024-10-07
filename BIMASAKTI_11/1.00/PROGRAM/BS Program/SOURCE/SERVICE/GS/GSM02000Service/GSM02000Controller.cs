using System.Diagnostics;
using GSM02000Back;
using GSM02000Common;
using GSM02000Common.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using R_OpenTelemetry;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;

namespace GSM02000Service;

[ApiController]
[Route("api/[controller]/[action]")]
public class GSM02000Controller : ControllerBase, IGSM02000
{
    private LoggerGSM02000 _logger;
    private readonly ActivitySource _activitySource;

    public GSM02000Controller(ILogger<GSM02000Controller> logger)
    {
        //Initial and Get Logger
        LoggerGSM02000.R_InitializeLogger(logger);
        _logger = LoggerGSM02000.R_GetInstanceLogger();
        _activitySource =GSM02000Activity.R_InitializeAndGetActivitySource(nameof(GSM02000Controller));
    }

    [HttpPost]
    public R_ServiceGetRecordResultDTO<GSM02000DTO> R_ServiceGetRecord(
        R_ServiceGetRecordParameterDTO<GSM02000DTO> poParameter)
    {
        using Activity loActivity = _activitySource.StartActivity(nameof(R_ServiceGetRecord));
        _logger.LogInfo("Start - Get Tax Record");

        R_Exception loEx = new();
        R_ServiceGetRecordResultDTO<GSM02000DTO> loRtn = new R_ServiceGetRecordResultDTO<GSM02000DTO>();

        try
        {
            var loCls = new GSM02000Cls();
            
            _logger.LogInfo("Set Parameter");
            poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
            poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;
            
            _logger.LogInfo("Get Tax Record");
            loRtn.data = loCls.R_GetRecord(poParameter.Entity);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }

        loEx.ThrowExceptionIfErrors();
        _logger.LogInfo("End - Get Tax Record");
        return loRtn;
    }

    [HttpPost]
    public R_ServiceSaveResultDTO<GSM02000DTO> R_ServiceSave(R_ServiceSaveParameterDTO<GSM02000DTO> poParameter)
    {
        using var loActivity = _activitySource.StartActivity(nameof(R_ServiceSave));
        _logger.LogInfo("Start - Save Tax Entity");
        R_Exception loEx = new();
        R_ServiceSaveResultDTO<GSM02000DTO> loRtn = null;
        GSM02000Cls loCls;

        try
        {
            loCls = new GSM02000Cls();
            loRtn = new R_ServiceSaveResultDTO<GSM02000DTO>();

            _logger.LogInfo("Set Parameter");
            poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
            poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;
            // poParameter.Entity.CTAX_ID = "cukuku";
            
            _logger.LogInfo("Save Tax Entity");
            loRtn.data = loCls.R_Save(poParameter.Entity, poParameter.CRUDMode);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }

        loEx.ThrowExceptionIfErrors();
        _logger.LogInfo("End - Save Tax Entity");
        return loRtn;
    }

    [HttpPost]
    public R_ServiceDeleteResultDTO R_ServiceDelete(R_ServiceDeleteParameterDTO<GSM02000DTO> poParameter)
    {
        using var loActivity = _activitySource.StartActivity(nameof(R_ServiceDelete));
        _logger.LogInfo("Start - Delete Tax Entity");
        R_Exception loEx = new();
        R_ServiceDeleteResultDTO loRtn = new();
        GSM02000Cls loCls;

        try
        {
            loCls = new GSM02000Cls();
            
            _logger.LogInfo("Set Parameter");
            poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;
            
            _logger.LogInfo("Delete Tax Entity");
            loCls.R_Delete(poParameter.Entity);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }

        loEx.ThrowExceptionIfErrors();
        _logger.LogInfo("End - Delete Tax Entity");
        return loRtn;
    }

    [HttpPost]
    public IAsyncEnumerable<GSM02000GridDTO> GetAllSalesTaxStream()
    {
        using var loActivity = _activitySource.StartActivity(nameof(GetAllSalesTaxStream));
        _logger.LogInfo("Start - Get Tax List");
        R_Exception loEx = new();
        GSM02000ParameterDb loDbPar;
        List<GSM02000GridDTO> loRtnTmp;
        GSM02000Cls loCls;
        IAsyncEnumerable<GSM02000GridDTO> loRtn = null;

        try
        {
            loDbPar = new GSM02000ParameterDb();
            
            _logger.LogInfo("Set Parameter");
            loDbPar.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
            loDbPar.CUSER_ID = R_BackGlobalVar.USER_ID;

            loCls = new GSM02000Cls();
            _logger.LogInfo("Get Tax List");
            loRtnTmp = loCls.SalesTaxListDb(loDbPar);

            loRtn = GetStream(loRtnTmp);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }

        loEx.ThrowExceptionIfErrors();
        _logger.LogInfo("End - Get Tax List");
        return loRtn;
    }
    
    [HttpPost]
    public IAsyncEnumerable<GSM02000DeductionGridDTO> GetAllDeductionStream()
    {
        using var loActivity = _activitySource.StartActivity(nameof(GetAllDeductionStream));
        _logger.LogInfo("Start - Get Deduction List");
        R_Exception loEx = new();
        GSM02000ParameterDb loDbPar;
        List<GSM02000DeductionGridDTO> loRtnTmp;
        GSM02000Cls loCls;
        IAsyncEnumerable<GSM02000DeductionGridDTO> loRtn = null;

        try
        {
            loDbPar = new GSM02000ParameterDb();
            
            _logger.LogInfo("Set Parameter");
            loDbPar.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
            loDbPar.CUSER_ID = R_BackGlobalVar.USER_ID;
            loDbPar.CTAX_ID = R_Utility.R_GetStreamingContext<string>(GSM02000ContextConstant.CTAX_ID);

            loCls = new GSM02000Cls();
            _logger.LogInfo("Get Deduction List");
            loRtnTmp = loCls.DeductionListDb(loDbPar);

            loRtn = GetStream(loRtnTmp);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }

        loEx.ThrowExceptionIfErrors();
        _logger.LogInfo("End - Get Deduction List");
        return loRtn;
    }

    [HttpPost]
    public GSM02000ListDTO<GSM02000RoundingDTO> GetAllRounding()
    {
        using var loActivity = _activitySource.StartActivity(nameof(GetAllRounding));
        _logger.LogInfo("Start - Get Rounding List");
        R_Exception loEx = new();
        GSM02000ListDTO<GSM02000RoundingDTO> loRtn = null;
        List<GSM02000RoundingDTO> loResult;
        GSM02000ParameterDb loDbPar;
        GSM02000Cls loCls;

        try
        {
            loDbPar = new GSM02000ParameterDb();
            
            _logger.LogInfo("Set Parameter");
            loDbPar.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
            loDbPar.CUSER_LANGUAGE = R_BackGlobalVar.CULTURE_MENU;

            loCls = new GSM02000Cls();
            
            _logger.LogInfo("Get Rounding List");
            loResult = loCls.RoundingListDb(loDbPar);
            loRtn = new GSM02000ListDTO<GSM02000RoundingDTO> { Data = loResult };
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }

        loEx.ThrowExceptionIfErrors();
        _logger.LogInfo("End - Get Rounding List");
        return loRtn;
    }
    
    [HttpPost]
    public GSM02000ListDTO<GSM02000PropertyDTO> GetAllProperty()
    {
        using var loActivity = _activitySource.StartActivity(nameof(GetAllProperty));
        _logger.LogInfo("Start - Get Property List");
        R_Exception loEx = new();
        GSM02000ListDTO<GSM02000PropertyDTO> loRtn = null;
        List<GSM02000PropertyDTO> loResult;
        GSM02000ParameterDb loDbPar;
        GSM02000Cls loCls;

        try
        {
            loDbPar = new GSM02000ParameterDb();
            
            _logger.LogInfo("Set Parameter");
            loDbPar.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
            loDbPar.CUSER_ID = R_BackGlobalVar.USER_ID;

            loCls = new GSM02000Cls();
            
            _logger.LogInfo("Get Property List");
            loResult = loCls.PropertyListDb(loDbPar);
            loRtn = new GSM02000ListDTO<GSM02000PropertyDTO> { Data = loResult };
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
    public GSM02000ActiveInactiveDTO SetActiveInactive(GSM02000ActiveInactiveParamsDTO poParams)
    {
        using Activity loActivity = _activitySource.StartActivity(nameof(SetActiveInactive));
        _logger.LogInfo("Start - Set Active/Inactive");
        R_Exception loEx = new();
        GSM02000ActiveInactiveDb loDbPar = new GSM02000ActiveInactiveDb();
        GSM02000ActiveInactiveDTO loRtn = new GSM02000ActiveInactiveDTO();
        GSM02000Cls loCls = new GSM02000Cls();

        try
        {
            _logger.LogInfo("Set Parameter");
            loDbPar.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
            loDbPar.CUSER_ID = R_BackGlobalVar.USER_ID;
            loDbPar.CTAX_ID = poParams.CTAX_ID;
            loDbPar.LACTIVE = poParams.LACTIVE;
            
            _logger.LogInfo("Set Active/Inactive");
            loCls.SetActiveInactiveDb(loDbPar);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }

        loEx.ThrowExceptionIfErrors();
        _logger.LogInfo("End - Set Active/Inactive");
        return loRtn;
    }

    #region "Helper GetSalesTaxStream Functions"

    private async IAsyncEnumerable<T> GetStream<T>(List<T> poParameter)
    {
        foreach (var item in poParameter)
        {
            yield return item;
        }
    }

    #endregion
}