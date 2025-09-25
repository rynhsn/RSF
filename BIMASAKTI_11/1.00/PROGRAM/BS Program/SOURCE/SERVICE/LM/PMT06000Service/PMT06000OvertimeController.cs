using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PMT06000Back;
using PMT06000Common;
using PMT06000Common.DTOs;
using PMT06000Common.Params;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;

namespace PMT06000Service;

[ApiController]
[Route("api/[controller]/[action]")]
public class PMT06000OvertimeController : ControllerBase, IPMT06000Overtime
{
    private LoggerPMT06000 _logger;
    private readonly ActivitySource _activitySource;

    public PMT06000OvertimeController(ILogger<PMT06000OvertimeController> logger)
    {
        //Initial and Get Logger
        LoggerPMT06000.R_InitializeLogger(logger);
        _logger = LoggerPMT06000.R_GetInstanceLogger();
        _activitySource = PMT06000Activity.R_InitializeAndGetActivitySource(nameof(PMT06000OvertimeController));
    }

    [HttpPost]
    public R_ServiceGetRecordResultDTO<PMT06000OvtDTO> R_ServiceGetRecord(
        R_ServiceGetRecordParameterDTO<PMT06000OvtDTO> poParameter)
    {
        using Activity loActivity = _activitySource.StartActivity(nameof(R_ServiceGetRecord));
        _logger.LogInfo("Start - Get Ovt Record");

        R_Exception loEx = new();
        R_ServiceGetRecordResultDTO<PMT06000OvtDTO> loRtn = new R_ServiceGetRecordResultDTO<PMT06000OvtDTO>();

        try
        {
            var loCls = new PMT06000OvertimeCls();

            _logger.LogInfo("Set Parameter");
            poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
            poParameter.Entity.CLANG_ID = R_BackGlobalVar.CULTURE;

            _logger.LogInfo("Get Ovt Record");
            loRtn.data = loCls.R_GetRecord(poParameter.Entity);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }

        loEx.ThrowExceptionIfErrors();
        _logger.LogInfo("End - Get Ovt Record");
        return loRtn;
    }

    [HttpPost]
    public R_ServiceSaveResultDTO<PMT06000OvtDTO> R_ServiceSave(R_ServiceSaveParameterDTO<PMT06000OvtDTO> poParameter)
    {
        using Activity loActivity = _activitySource.StartActivity(nameof(R_ServiceSave));
        _logger.LogInfo("Start - Save Ovt Entity");
        R_Exception loEx = new();
        R_ServiceSaveResultDTO<PMT06000OvtDTO> loRtn = null;

        try
        {
            var loCls = new PMT06000OvertimeCls();
            loRtn = new R_ServiceSaveResultDTO<PMT06000OvtDTO>();

            _logger.LogInfo("Set Parameter");
            poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
            poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;
            poParameter.Entity.CLANG_ID = R_BackGlobalVar.CULTURE;

            _logger.LogInfo("Save Ovt Entity");
            loRtn.data = loCls.R_Save(poParameter.Entity, poParameter.CRUDMode);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }

        loEx.ThrowExceptionIfErrors();
        _logger.LogInfo("End - Save Ovt Entity");
        return loRtn;
    }

    [HttpPost]
    public R_ServiceDeleteResultDTO R_ServiceDelete(R_ServiceDeleteParameterDTO<PMT06000OvtDTO> poParameter)
    {
        using Activity loActivity = _activitySource.StartActivity(nameof(R_ServiceDelete));
        _logger.LogInfo("Start - Delete Ovt Entity");
        R_Exception loEx = new();
        R_ServiceDeleteResultDTO loRtn = new();
        PMT06000OvertimeCls loCls;

        try
        {
            loCls = new PMT06000OvertimeCls();

            _logger.LogInfo("Set Parameter");
            poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
            poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;

            _logger.LogInfo("Delete Ovt Entity");
            loCls.R_Delete(poParameter.Entity);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }

        loEx.ThrowExceptionIfErrors();
        _logger.LogInfo("End - Delete Ovt Entity");
        return loRtn;
    }

    [HttpPost]
    public IAsyncEnumerable<PMT06000OvtGridDTO> PMT06000GetOvertimeListStream()
    {
        using var loActivity = _activitySource.StartActivity(nameof(PMT06000GetOvertimeListStream));
        _logger.LogInfo("Start - Get Overtime List Stream");
        var loEx = new R_Exception();
        var loCls = new PMT06000OvertimeCls();
        var loDbParams = new PMT06000ParameterDb();
        List<PMT06000OvtGridDTO> loResult;
        IAsyncEnumerable<PMT06000OvtGridDTO> loReturn = null;

        try
        {
            _logger.LogInfo("Set Parameter");
            loDbParams.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
            loDbParams.CLANG_ID = R_BackGlobalVar.CULTURE;
            loDbParams.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(PMT06000ContextConstant.CPROPERTY_ID);
            loDbParams.CTRANS_CODE = R_Utility.R_GetStreamingContext<string>(PMT06000ContextConstant.CTRANS_CODE);
            loDbParams.CPERIOD = R_Utility.R_GetStreamingContext<string>(PMT06000ContextConstant.CPERIOD);
            // loDbParams.CTRANS_STATUS = R_Utility.R_GetStreamingContext<string>(PMT06000ContextConstant.CTRANS_STATUS);
            // loDbParams.COVERTIME_STATUS =
            //     R_Utility.R_GetStreamingContext<string>(PMT06000ContextConstant.COVERTIME_STATUS);
            // loDbParams.CAGREEMENT_NO = R_Utility.R_GetStreamingContext<string>(PMT06000ContextConstant.CAGREEMENT_NO);

            _logger.LogInfo("Get Overtime List Stream");
            loResult = loCls.GetOvtList(loDbParams);
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
    public IAsyncEnumerable<PMT06000OvtServiceGridDTO> PMT06000GetOvertimeServiceListStream()
    {
        using var loActivity = _activitySource.StartActivity(nameof(PMT06000GetOvertimeServiceListStream));
        _logger.LogInfo("Start - Get Overtime Service List Stream");
        var loEx = new R_Exception();
        var loCls = new PMT06000OvertimeCls();
        var loDbParams = new PMT06000ParameterDb();
        List<PMT06000OvtServiceGridDTO> loResult;
        IAsyncEnumerable<PMT06000OvtServiceGridDTO> loReturn = null;

        try
        {
            _logger.LogInfo("Set Parameter");
            loDbParams.CPARENT_ID = R_Utility.R_GetStreamingContext<string>(PMT06000ContextConstant.CPARENT_ID);

            _logger.LogInfo("Get Overtime Service List Stream");
            loResult = loCls.GetOvtServiceList(loDbParams);
            loReturn = GetStream(loResult);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }

        loEx.ThrowExceptionIfErrors();
        _logger.LogInfo("End - Get Overtime Service List Stream");
        return loReturn;
    }

    [HttpPost]
    public IAsyncEnumerable<PMT06000OvtUnitDTO> PMT06000GetOvertimeUnitListStream()
    {
        using var loActivity = _activitySource.StartActivity(nameof(PMT06000GetOvertimeServiceListStream));
        _logger.LogInfo("Start - Get Overtime Unit List Stream");
        var loEx = new R_Exception();
        var loCls = new PMT06000OvertimeCls();
        var loDbParams = new PMT06000ParameterDb();
        List<PMT06000OvtUnitDTO> loResult;
        IAsyncEnumerable<PMT06000OvtUnitDTO> loReturn = null;

        try
        {
            _logger.LogInfo("Set Parameter");
            loDbParams.CPARENT_ID = R_Utility.R_GetStreamingContext<string>(PMT06000ContextConstant.CPARENT_ID);

            _logger.LogInfo("Get Overtime Unit List Stream");
            loResult = loCls.GetOvtUnitList(loDbParams);
            loReturn = GetStream(loResult);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }

        loEx.ThrowExceptionIfErrors();
        _logger.LogInfo("End - Get Overtime Service List Stream");
        return loReturn;
    }

    [HttpPost]
    public PMT06000SingleDTO<PMT06000PropertyDTO> PMT06000ProcessSubmit(PMT06000ProcessSubmitParam poParameter)
    {
        using var loActivity = _activitySource.StartActivity(nameof(PMT06000ProcessSubmit));
        _logger.LogInfo("Start - Process Submit");
        var loEx = new R_Exception();
        var loCls = new PMT06000OvertimeCls();
        var loDbParams = new PMT06000ParameterDb();
        var loReturn = new PMT06000SingleDTO<PMT06000PropertyDTO>();

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