using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PMI00500Back;
using PMI00500Common;
using PMI00500Common.DTOs;
using R_BackEnd;
using R_Common;

namespace PMI00500Service;

[ApiController]
[Route("api/[controller]/[action]")]
public class PMI00500Controller : ControllerBase, IPMI00500
{
    private LoggerPMI00500 _logger;
    private readonly ActivitySource _activitySource;

    public PMI00500Controller(ILogger<PMI00500Controller> logger)
    {
        //ial and Get Logger
        LoggerPMI00500.R_InitializeLogger(logger);
        _logger = LoggerPMI00500.R_GetInstanceLogger();
        _activitySource = PMI00500Activity.R_InitializeAndGetActivitySource(nameof(PMI00500Controller));
    }
    
    [HttpPost]
    public PMI00500ListDTO<PMI00500PropertyDTO> PMI00500GetPropertyList()
    {
        using var loActivity = _activitySource.StartActivity(nameof(PMI00500GetPropertyList));
        
        _logger.LogInfo("Start - Get Property");
        var loEx = new R_Exception();
        var loCls = new PMI00500Cls();
        var loDbParams = new PMI00500ParameterDb();
        var loReturn = new PMI00500ListDTO<PMI00500PropertyDTO>();

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
    public IAsyncEnumerable<PMI00500HeaderDTO> PMI00500GetHeaderListStream()
    {
        using var loActivity = _activitySource.StartActivity(nameof(PMI00500GetHeaderListStream));
        _logger.LogInfo("Start - Get Header List Stream");
        var loEx = new R_Exception();
        var loCls = new PMI00500Cls();
        var loDbParams = new PMI00500ParameterDb();
        List<PMI00500HeaderDTO> loResult = null;
        IAsyncEnumerable<PMI00500HeaderDTO> loReturn = null;


        try
        {
            _logger.LogInfo("Set Parameter");
            loDbParams.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
            loDbParams.CLANG_ID = R_BackGlobalVar.CULTURE;
            loDbParams.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(PMI00500ContextConstant.CPROPERTY_ID);
            loDbParams.CDEPT_CODE = R_Utility.R_GetStreamingContext<string>(PMI00500ContextConstant.CDEPT_CODE);

            _logger.LogInfo("Get Header List Stream");
            loResult = loCls.GetHeaderList(loDbParams);
            loReturn = GetStream(loResult);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }

        loEx.ThrowExceptionIfErrors();
        _logger.LogInfo("End - Get Header List Stream");
        return loReturn;
    }

    [HttpPost]
    public IAsyncEnumerable<PMI00500DTAgreementDTO> PMI00500GetDTAgreementListStream()
    {
        using var loActivity = _activitySource.StartActivity(nameof(PMI00500GetDTAgreementListStream));
        _logger.LogInfo("Start - Get DTAgreement List Stream");
        var loEx = new R_Exception();
        var loCls = new PMI00500Cls();
        var loDbParams = new PMI00500ParameterDb();
        List<PMI00500DTAgreementDTO> loResult = null;
        IAsyncEnumerable<PMI00500DTAgreementDTO> loReturn = null;


        try
        {
            _logger.LogInfo("Set Parameter");
            loDbParams.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
            loDbParams.CLANG_ID = R_BackGlobalVar.CULTURE;
            loDbParams.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(PMI00500ContextConstant.CPROPERTY_ID);
            loDbParams.CDEPT_CODE = R_Utility.R_GetStreamingContext<string>(PMI00500ContextConstant.CDEPT_CODE);
            loDbParams.CTENANT_ID = R_Utility.R_GetStreamingContext<string>(PMI00500ContextConstant.CTENANT_ID);

            _logger.LogInfo("Get DTAgreement List Stream");
            loResult = loCls.GetDTAgreementList(loDbParams);
            loReturn = GetStream(loResult);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }

        loEx.ThrowExceptionIfErrors();
        _logger.LogInfo("End - Get DTAgreement List Stream");
        return loReturn;
    }

    [HttpPost]
    public IAsyncEnumerable<PMI00500DTReminderDTO> PMI00500GetDTReminderListStream()
    {
        using var loActivity = _activitySource.StartActivity(nameof(PMI00500GetDTReminderListStream));
        _logger.LogInfo("Start - Get DTReminder List Stream");
        var loEx = new R_Exception();
        var loCls = new PMI00500Cls();
        var loDbParams = new PMI00500ParameterDb();
        List<PMI00500DTReminderDTO> loResult = null;
        IAsyncEnumerable<PMI00500DTReminderDTO> loReturn = null;


        try
        {
            _logger.LogInfo("Set Parameter");
            loDbParams.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
            loDbParams.CLANG_ID = R_BackGlobalVar.CULTURE;
            loDbParams.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(PMI00500ContextConstant.CPROPERTY_ID);
            loDbParams.CDEPT_CODE = R_Utility.R_GetStreamingContext<string>(PMI00500ContextConstant.CDEPT_CODE);
            loDbParams.CTENANT_ID = R_Utility.R_GetStreamingContext<string>(PMI00500ContextConstant.CTENANT_ID);
            loDbParams.CAGREEMENT_NO = R_Utility.R_GetStreamingContext<string>(PMI00500ContextConstant.CAGREEMENT_NO);

            _logger.LogInfo("Get DTReminder List Stream");
            loResult = loCls.GetDTReminderList(loDbParams);
            loReturn = GetStream(loResult);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }

        loEx.ThrowExceptionIfErrors();
        _logger.LogInfo("End - Get DTReminder List Stream");
        return loReturn;
    }

    [HttpPost]
    public IAsyncEnumerable<PMI00500DTInvoiceDTO> PMI00500GetDTInvoiceListStream()
    {
        using var loActivity = _activitySource.StartActivity(nameof(PMI00500GetDTInvoiceListStream));
        _logger.LogInfo("Start - Get DTReminder List Stream");
        var loEx = new R_Exception();
        var loCls = new PMI00500Cls();
        var loDbParams = new PMI00500ParameterDb();
        List<PMI00500DTInvoiceDTO> loResult = null;
        IAsyncEnumerable<PMI00500DTInvoiceDTO> loReturn = null;


        try
        {
            _logger.LogInfo("Set Parameter");
            loDbParams.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
            loDbParams.CLANG_ID = R_BackGlobalVar.CULTURE;
            loDbParams.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(PMI00500ContextConstant.CPROPERTY_ID);
            loDbParams.CDEPT_CODE = R_Utility.R_GetStreamingContext<string>(PMI00500ContextConstant.CDEPT_CODE);
            loDbParams.CTENANT_ID = R_Utility.R_GetStreamingContext<string>(PMI00500ContextConstant.CTENANT_ID);
            loDbParams.CAGREEMENT_NO = R_Utility.R_GetStreamingContext<string>(PMI00500ContextConstant.CAGREEMENT_NO);
            loDbParams.CREMINDER_NO = R_Utility.R_GetStreamingContext<string>(PMI00500ContextConstant.CREMINDER_NO);

            _logger.LogInfo("Get DTInvoice List Stream");
            loResult = loCls.GetDTInvoiceList(loDbParams);
            loReturn = GetStream(loResult);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }

        loEx.ThrowExceptionIfErrors();
        _logger.LogInfo("End - Get DTInvoice List Stream");
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