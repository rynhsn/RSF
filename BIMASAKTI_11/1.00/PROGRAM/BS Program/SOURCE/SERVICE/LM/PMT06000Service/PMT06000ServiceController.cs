using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PMT06000Back;
using PMT06000Common;
using PMT06000Common.DTOs;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;

namespace PMT06000Service;

[ApiController]
[Route("api/[controller]/[action]")]
public class PMT06000ServiceController : ControllerBase, IPMT06000Service
{
    private LoggerPMT06000 _logger;
    private readonly ActivitySource _activitySource;

    public PMT06000ServiceController(ILogger<PMT06000ServiceController> logger)
    {
        //Initial and Get Logger
        LoggerPMT06000.R_InitializeLogger(logger);
        _logger = LoggerPMT06000.R_GetInstanceLogger();
        _activitySource = PMT06000Activity.R_InitializeAndGetActivitySource(nameof(PMT06000ServiceController));
    }

    [HttpPost]
    public R_ServiceGetRecordResultDTO<PMT06000OvtServiceDTO> R_ServiceGetRecord(
        R_ServiceGetRecordParameterDTO<PMT06000OvtServiceDTO> poParameter)
    {
        using Activity loActivity = _activitySource.StartActivity(nameof(R_ServiceGetRecord));
        _logger.LogInfo("Start - Get Service Record");

        R_Exception loEx = new();
        R_ServiceGetRecordResultDTO<PMT06000OvtServiceDTO> loRtn = new R_ServiceGetRecordResultDTO<PMT06000OvtServiceDTO>();

        try
        {
            var loCls = new PMT06000ServiceCls();

            _logger.LogInfo("Get Service Record");
            loRtn.data = loCls.R_GetRecord(poParameter.Entity);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }

        loEx.ThrowExceptionIfErrors();
        _logger.LogInfo("End - Get Service Record");
        return loRtn;
    }

    [HttpPost]
    public R_ServiceSaveResultDTO<PMT06000OvtServiceDTO> R_ServiceSave(
        R_ServiceSaveParameterDTO<PMT06000OvtServiceDTO> poParameter)
    {
        using Activity loActivity = _activitySource.StartActivity(nameof(R_ServiceSave));
        _logger.LogInfo("Start - Save Service Entity");
        R_Exception loEx = new();
        R_ServiceSaveResultDTO<PMT06000OvtServiceDTO> loRtn = null;

        try
        {
            var loCls = new PMT06000ServiceCls();
            loRtn = new R_ServiceSaveResultDTO<PMT06000OvtServiceDTO>();

            _logger.LogInfo("Set Parameter");
            poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
            poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;

            _logger.LogInfo("Save Service Entity");
            loRtn.data = loCls.R_Save(poParameter.Entity, poParameter.CRUDMode);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }

        loEx.ThrowExceptionIfErrors();
        _logger.LogInfo("End - Save Service Entity");
        return loRtn;
    }

    [HttpPost]
    public R_ServiceDeleteResultDTO R_ServiceDelete(R_ServiceDeleteParameterDTO<PMT06000OvtServiceDTO> poParameter)
    {
        using Activity loActivity = _activitySource.StartActivity(nameof(R_ServiceDelete));
        _logger.LogInfo("Start - Delete Service Entity");
        R_Exception loEx = new();
        R_ServiceDeleteResultDTO loRtn = new();

        try
        {
            var loCls = new PMT06000ServiceCls();

            _logger.LogInfo("Set Parameter");
            poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
            poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;

            _logger.LogInfo("Delete Service Entity");
            loCls.R_Delete(poParameter.Entity);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }

        loEx.ThrowExceptionIfErrors();
        _logger.LogInfo("End - Delete Service Entity");
        return loRtn;
    }
}