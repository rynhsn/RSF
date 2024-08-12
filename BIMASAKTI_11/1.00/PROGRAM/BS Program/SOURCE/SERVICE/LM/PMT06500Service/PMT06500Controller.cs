using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PMT06500Back;
using PMT06500Common;
using PMT06500Common.DTOs;
using PMT06500Common.Params;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;

namespace PMT06500Service;

[ApiController]
[Route("api/[controller]/[action]")]
public class PMT06500Controller : ControllerBase, IPMT06500
{
    private LoggerPMT06500 _logger;
    private readonly ActivitySource _activitySource;

    public PMT06500Controller(ILogger<PMT06500Controller> logger)
    {
        //Initial and Get Logger
        LoggerPMT06500.R_InitializeLogger(logger);
        _logger = LoggerPMT06500.R_GetInstanceLogger();
        _activitySource = PMT06500Activity.R_InitializeAndGetActivitySource(nameof(PMT06500Controller));
    }

    [HttpPost]
    public R_ServiceGetRecordResultDTO<PMT06500InvoiceDTO> R_ServiceGetRecord(
        R_ServiceGetRecordParameterDTO<PMT06500InvoiceDTO> poParameter)
    {
        using var loActivity = _activitySource.StartActivity(nameof(R_ServiceGetRecord));
        _logger.LogInfo("Start - Get Invoice Record");

        R_Exception loEx = new();
        var loRtn = new R_ServiceGetRecordResultDTO<PMT06500InvoiceDTO>();

        try
        {
            var loCls = new PMT06500Cls();

            _logger.LogInfo("Set Parameter");
            poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
            poParameter.Entity.CLANG_ID = R_BackGlobalVar.CULTURE;

            _logger.LogInfo("Get Invoice Record");
            loRtn.data = loCls.R_GetRecord(poParameter.Entity);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }

        loEx.ThrowExceptionIfErrors();
        _logger.LogInfo("End - Get Invoice Record");
        return loRtn;
    }

    [HttpPost]
    public R_ServiceSaveResultDTO<PMT06500InvoiceDTO> R_ServiceSave(
        R_ServiceSaveParameterDTO<PMT06500InvoiceDTO> poParameter)
    {
        using var loActivity = _activitySource.StartActivity(nameof(R_ServiceSave));
        _logger.LogInfo("Start - Save Invoice Entity");
        R_Exception loEx = new();
        R_ServiceSaveResultDTO<PMT06500InvoiceDTO> loRtn = null;

        try
        {
            var loCls = new PMT06500Cls();
            loRtn = new R_ServiceSaveResultDTO<PMT06500InvoiceDTO>();

            _logger.LogInfo("Set Parameter");
            poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
            poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;

            _logger.LogInfo("Save Invoice Entity");
            loRtn.data = loCls.R_Save(poParameter.Entity, poParameter.CRUDMode);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }

        loEx.ThrowExceptionIfErrors();
        _logger.LogInfo("End - Save Invoice Entity");
        return loRtn;
    }

    [HttpPost]
    public R_ServiceDeleteResultDTO R_ServiceDelete(R_ServiceDeleteParameterDTO<PMT06500InvoiceDTO> poParameter)
    {
        using Activity loActivity = _activitySource.StartActivity(nameof(R_ServiceDelete));
        _logger.LogInfo("Start - Delete Invoice Entity");
        R_Exception loEx = new();
        R_ServiceDeleteResultDTO loRtn = new();
        PMT06500Cls loCls;

        try
        {
            loCls = new PMT06500Cls();

            _logger.LogInfo("Set Parameter");
            poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
            poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;

            _logger.LogInfo("Delete Invoice Entity");
            loCls.R_Delete(poParameter.Entity);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }

        loEx.ThrowExceptionIfErrors();
        _logger.LogInfo("End - Delete Invoice Entity");
        return loRtn;
    }

    [HttpPost]
    public IAsyncEnumerable<PMT06500AgreementDTO> PMT06500GetAgreementListStream()
    {
        using var loActivity = _activitySource.StartActivity(nameof(PMT06500GetAgreementListStream));
        _logger.LogInfo("Start - Get Agreement List Stream");
        var loEx = new R_Exception();
        var loCls = new PMT06500Cls();
        var loDbParams = new PMT06500ParameterDb();
        List<PMT06500AgreementDTO> loResult;
        IAsyncEnumerable<PMT06500AgreementDTO> loReturn = null;

        try
        {
            _logger.LogInfo("Set Parameter");
            loDbParams.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
            loDbParams.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(PMT06500ContextConstant.CPROPERTY_ID);
            loDbParams.CPERIOD = R_Utility.R_GetStreamingContext<string>(PMT06500ContextConstant.CPERIOD);
            
            loDbParams.COVT_TRANS_CODE = R_Utility.R_GetStreamingContext<string>(PMT06500ContextConstant.COVT_TRANS_CODE);
            loDbParams.CTRANS_STATUS = R_Utility.R_GetStreamingContext<string>(PMT06500ContextConstant.CTRANS_STATUS);
            loDbParams.COVERTIME_STATUS = R_Utility.R_GetStreamingContext<string>(PMT06500ContextConstant.COVERTIME_STATUS);


            _logger.LogInfo("Get Agreement List Stream");
            loResult = loCls.GetAgreementList(loDbParams);
            loReturn = GetStream(loResult);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }

        loEx.ThrowExceptionIfErrors();
        _logger.LogInfo("End - Get Agreement List Stream");
        return loReturn;
    }

    [HttpPost]
    public IAsyncEnumerable<PMT06500OvtDTO> PMT06500GetOvertimeListStream()
    {
        using var loActivity = _activitySource.StartActivity(nameof(PMT06500GetOvertimeListStream));
        _logger.LogInfo("Start - Get Overtime List Stream");
        var loEx = new R_Exception();
        var loCls = new PMT06500Cls();
        var loDbParams = new PMT06500ParameterDb();
        List<PMT06500OvtDTO> loResult;
        IAsyncEnumerable<PMT06500OvtDTO> loReturn = null;

        try
        {
            _logger.LogInfo("Set Parameter");
            loDbParams.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
            loDbParams.CLANG_ID = R_BackGlobalVar.CULTURE;
            loDbParams.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(PMT06500ContextConstant.CPROPERTY_ID);
            loDbParams.CPERIOD = R_Utility.R_GetStreamingContext<string>(PMT06500ContextConstant.CPERIOD);
            loDbParams.CAGREEMENT_NO = R_Utility.R_GetStreamingContext<string>(PMT06500ContextConstant.CAGREEMENT_NO);
            
            loDbParams.COVT_TRANS_CODE = R_Utility.R_GetStreamingContext<string>(PMT06500ContextConstant.COVT_TRANS_CODE);
            loDbParams.CTRANS_STATUS = R_Utility.R_GetStreamingContext<string>(PMT06500ContextConstant.CTRANS_STATUS);
            loDbParams.COVERTIME_STATUS = R_Utility.R_GetStreamingContext<string>(PMT06500ContextConstant.COVERTIME_STATUS);

            _logger.LogInfo("Get Overtime List Stream");
            loResult = loCls.GetOvertimeList(loDbParams);
            loReturn = GetStream(loResult);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }

        loEx.ThrowExceptionIfErrors();
        _logger.LogInfo("End - Get Overtime List Stream");
        return loReturn;
    }

    [HttpPost]
    public IAsyncEnumerable<PMT06500ServiceDTO> PMT06500GetServiceListStream()
    {
        using var loActivity = _activitySource.StartActivity(nameof(PMT06500GetServiceListStream));
        _logger.LogInfo("Start - Get Service List Stream");
        var loEx = new R_Exception();
        var loCls = new PMT06500Cls();
        var loDbParams = new PMT06500ParameterDb();
        List<PMT06500ServiceDTO> loResult;
        IAsyncEnumerable<PMT06500ServiceDTO> loReturn = null;

        try
        {
            _logger.LogInfo("Set Parameter");
            loDbParams.CPARENT_ID = R_Utility.R_GetStreamingContext<string>(PMT06500ContextConstant.CPARENT_ID);

            _logger.LogInfo("Get Service List Stream");
            loResult = loCls.GetServiceList(loDbParams);
            loReturn = GetStream(loResult);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }

        loEx.ThrowExceptionIfErrors();
        _logger.LogInfo("End - Get Service List Stream");
        return loReturn;
    }

    [HttpPost]
    public IAsyncEnumerable<PMT06500UnitDTO> PMT06500GetUnitListStream()
    {
        using var loActivity = _activitySource.StartActivity(nameof(PMT06500GetUnitListStream));
        _logger.LogInfo("Start - Get Unit List Stream");
        var loEx = new R_Exception();
        var loCls = new PMT06500Cls();
        var loDbParams = new PMT06500ParameterDb();
        List<PMT06500UnitDTO> loResult;
        IAsyncEnumerable<PMT06500UnitDTO> loReturn = null;

        try
        {
            _logger.LogInfo("Set Parameter");
            loDbParams.CPARENT_ID = R_Utility.R_GetStreamingContext<string>(PMT06500ContextConstant.CPARENT_ID);

            _logger.LogInfo("Get Unit List Stream");
            loResult = loCls.GetUnitList(loDbParams);
            loReturn = GetStream(loResult);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }

        loEx.ThrowExceptionIfErrors();
        _logger.LogInfo("End - Get Unit List Stream");
        return loReturn;
    }

    [HttpPost]
    public IAsyncEnumerable<PMT06500InvoiceDTO> PMT06500GetInvoiceListStream()
    {
        using var loActivity = _activitySource.StartActivity(nameof(PMT06500GetInvoiceListStream));
        _logger.LogInfo("Start - Get Invoice Plant List Stream");
        var loEx = new R_Exception();
        var loCls = new PMT06500Cls();
        var loDbParams = new PMT06500ParameterDb();
        List<PMT06500InvoiceDTO> loResult;
        IAsyncEnumerable<PMT06500InvoiceDTO> loReturn = null;

        try
        {
            _logger.LogInfo("Set Parameter");
            loDbParams.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
            loDbParams.CLANG_ID = R_BackGlobalVar.CULTURE;
            loDbParams.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(PMT06500ContextConstant.CPROPERTY_ID);
            
            loDbParams.CTRANS_CODE = R_Utility.R_GetStreamingContext<string>(PMT06500ContextConstant.CTRANS_CODE);
            loDbParams.CTRANS_STATUS = R_Utility.R_GetStreamingContext<string>(PMT06500ContextConstant.CTRANS_STATUS);

            _logger.LogInfo("Get Invoice Plant List Stream");
            loResult = loCls.GetInvoiceList(loDbParams);
            loReturn = GetStream(loResult);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }

        loEx.ThrowExceptionIfErrors();
        _logger.LogInfo("End - Get Invoice Plant List Stream");
        return loReturn;
    }

    [HttpPost]
    public IAsyncEnumerable<PMT06500SummaryDTO> PMT06500GetSummaryListStream()
    {
        using var loActivity = _activitySource.StartActivity(nameof(PMT06500GetUnitListStream));
        _logger.LogInfo("Start - Get Summary List Stream");
        var loEx = new R_Exception();
        var loCls = new PMT06500Cls();
        var loDbParams = new PMT06500ParameterDb();
        List<PMT06500SummaryDTO> loResult;
        IAsyncEnumerable<PMT06500SummaryDTO> loReturn = null;

        try
        {
            _logger.LogInfo("Set Parameter");
            loDbParams.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
            loDbParams.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(PMT06500ContextConstant.CPROPERTY_ID);
            loDbParams.CPERIOD = R_Utility.R_GetStreamingContext<string>(PMT06500ContextConstant.CPERIOD);
            loDbParams.CAGREEMENT_NO = R_Utility.R_GetStreamingContext<string>(PMT06500ContextConstant.CAGREEMENT_NO);

            _logger.LogInfo("Get Summary List Stream");
            loResult = loCls.GetSummaryList(loDbParams);
            loReturn = GetStream(loResult);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }

        loEx.ThrowExceptionIfErrors();
        _logger.LogInfo("End - Get Summary List Stream");
        return loReturn;
    }
    
    [HttpPost]
    public PMT06500SingleDTO<PMT06500PropertyDTO> PMT06500ProcessSubmit(PMT06500ProcessSubmitParam poParameter)
    {
        using var loActivity = _activitySource.StartActivity(nameof(PMT06500ProcessSubmit));
        _logger.LogInfo("Start - Process Submit");
        var loEx = new R_Exception();
        var loCls = new PMT06500Cls();
        var loDbParams = new PMT06500ParameterDb();
        var loReturn = new PMT06500SingleDTO<PMT06500PropertyDTO>();

        try
        {
            _logger.LogInfo("Set Parameter");
            loDbParams.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
            loDbParams.CUSER_ID = R_BackGlobalVar.USER_ID;
            loDbParams.CPROPERTY_ID = poParameter.CPROPERTY_ID;
            loDbParams.CREC_ID = poParameter.CREC_ID;
            loDbParams.CNEW_STATUS = poParameter.CNEW_STATUS;

            _logger.LogInfo("Process Submit");
            loCls.ProcessSubmit(loDbParams);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }

        loEx.ThrowExceptionIfErrors();
        _logger.LogInfo("End - Process Submit");
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