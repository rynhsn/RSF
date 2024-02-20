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
public class GSM02000TaxController : ControllerBase, IGSM02000Tax
{
    
    private LoggerGSM02000 _logger;    
    private readonly ActivitySource _activitySource;


    public GSM02000TaxController(ILogger<GSM02000TaxController> logger)
    {
        LoggerGSM02000.R_InitializeLogger(logger);
        _logger = LoggerGSM02000.R_GetInstanceLogger();
        _activitySource =GSM02000Activity.R_InitializeAndGetActivitySource(nameof(GSM02000TaxController));
    }

    [HttpPost]
    public R_ServiceGetRecordResultDTO<GSM02000TaxDTO> R_ServiceGetRecord(
        R_ServiceGetRecordParameterDTO<GSM02000TaxDTO> poParameter)
    {
        using Activity loActivity = _activitySource.StartActivity(nameof(R_ServiceGetRecord));
        _logger.LogInfo("Start - Get Tax % Record");
        var loEx = new R_Exception();
        var loRtn = new R_ServiceGetRecordResultDTO<GSM02000TaxDTO>();

        try
        {
            var loCls = new GSM02000TaxCls();
            
            _logger.LogInfo("Set Parameter");
            poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
            poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;

            _logger.LogInfo("Get Tax % Record");
            loRtn.data = loCls.R_GetRecord(poParameter.Entity);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }

        loEx.ThrowExceptionIfErrors();
        _logger.LogInfo("End - Get Tax % Record");
        return loRtn;
    }

    [HttpPost]
    public R_ServiceSaveResultDTO<GSM02000TaxDTO> R_ServiceSave(R_ServiceSaveParameterDTO<GSM02000TaxDTO> poParameter)
    {
        using Activity loActivity = _activitySource.StartActivity(nameof(R_ServiceSave));
        _logger.LogInfo("Start - Save Tax % Entity");
        R_Exception loEx = new R_Exception();
        R_ServiceSaveResultDTO<GSM02000TaxDTO> loRtn = null;
        GSM02000TaxCls loCls;

        try
        {
            loCls = new GSM02000TaxCls();
            loRtn = new R_ServiceSaveResultDTO<GSM02000TaxDTO>();

            _logger.LogInfo("Set Parameter");
            poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
            poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;

            _logger.LogInfo("Save Tax % Entity");
            loRtn.data = loCls.R_Save(poParameter.Entity, poParameter.CRUDMode);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }

        loEx.ThrowExceptionIfErrors();
        _logger.LogInfo("End - Save Tax % Entity");
        return loRtn;
    }

    [HttpPost]
    public R_ServiceDeleteResultDTO R_ServiceDelete(R_ServiceDeleteParameterDTO<GSM02000TaxDTO> poParameter)
    {
        using Activity loActivity = _activitySource.StartActivity(nameof(R_ServiceDelete));
        _logger.LogInfo("Start - Delete Tax % Entity");
        R_Exception loEx = new R_Exception();
        R_ServiceDeleteResultDTO loRtn = null;
        GSM02000TaxCls loCls;

        try
        {
            loCls = new GSM02000TaxCls();
            loRtn = new R_ServiceDeleteResultDTO();

            _logger.LogInfo("Set Parameter");
            poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
            poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;
            
            _logger.LogInfo("Delete Tax % Entity");
            loCls.R_Delete(poParameter.Entity);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }

        loEx.ThrowExceptionIfErrors();
        _logger.LogInfo("End - Delete Tax % Entity");
        return loRtn;
    }

    [HttpPost]
    public IAsyncEnumerable<GSM02000TaxSalesDTO> GSM02000GetAllSalesTaxListStream()
    {
        using Activity loActivity = _activitySource.StartActivity(nameof(GSM02000GetAllSalesTaxListStream));
        _logger.LogInfo("Start - Get All Tax List");
        R_Exception loEx = new R_Exception();
        IAsyncEnumerable<GSM02000TaxSalesDTO> loRtn = null;
        GSM02000TaxCls loCls;

        List<GSM02000TaxSalesDTO> loResult;
        GSM02000ParameterDb loDbPar;

        try
        {
            loDbPar = new GSM02000ParameterDb();
            
            _logger.LogInfo("Set Parameter");
            loDbPar.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
            loDbPar.CUSER_ID = R_BackGlobalVar.USER_ID;

            loCls = new GSM02000TaxCls();
            
            _logger.LogInfo("Get All Tax List");
            loResult = loCls.SalesTaxListDb(loDbPar);

            loRtn = GetStream(loResult);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }

        loEx.ThrowExceptionIfErrors();
        _logger.LogInfo("End - Get All Tax List");
        return loRtn;
    }

    [HttpPost]
    public IAsyncEnumerable<GSM02000TaxDTO> GSM02000GetAllTaxListStream()
    {
        using Activity loActivity = _activitySource.StartActivity(nameof(GSM02000GetAllTaxListStream));
        _logger.LogInfo("Start - Get All Tax % List");
        R_Exception loEx = new R_Exception();
        IAsyncEnumerable<GSM02000TaxDTO> loRtn = null;
        GSM02000TaxCls loCls;
        
        List<GSM02000TaxDTO> loResult;
        GSM02000ParameterDb loDbPar;

        try
        {
            loDbPar = new GSM02000ParameterDb();
            
            _logger.LogInfo("Set Parameter");
            loDbPar.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
            loDbPar.CUSER_ID = R_BackGlobalVar.USER_ID;
            loDbPar.CTAX_ID = R_Utility.R_GetStreamingContext<string>(ContextConstant.CTAX_ID);

            loCls = new GSM02000TaxCls();
            
            _logger.LogInfo("Get All Tax % List");
            loResult = loCls.TaxListDb(loDbPar);

            loRtn = GetStream(loResult);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }

        loEx.ThrowExceptionIfErrors();
        _logger.LogInfo("End - Get All Tax % List");
        return loRtn;
    }

    #region "Helper ListStream Functions"

    private async IAsyncEnumerable<T> GetStream<T>(List<T> poParameter)
    {
        foreach (T item in poParameter)
        {
            yield return item;
        }
    }

    #endregion
}