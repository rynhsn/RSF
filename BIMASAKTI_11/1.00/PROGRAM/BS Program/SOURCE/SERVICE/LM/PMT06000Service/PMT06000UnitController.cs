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
public class PMT06000UnitController : ControllerBase, IPMT06000Unit
{
    private LoggerPMT06000 _logger;
    private readonly ActivitySource _activitySource;

    public PMT06000UnitController(ILogger<PMT06000UnitController> logger)
    {
        //Initial and Get Logger
        LoggerPMT06000.R_InitializeLogger(logger);
        _logger = LoggerPMT06000.R_GetInstanceLogger();
        _activitySource = PMT06000Activity.R_InitializeAndGetActivitySource(nameof(PMT06000UnitController));
    }

    [HttpPost]
    public R_ServiceGetRecordResultDTO<PMT06000OvtUnitDTO> R_ServiceGetRecord(
        R_ServiceGetRecordParameterDTO<PMT06000OvtUnitDTO> poParameter)
    {
        using var loActivity = _activitySource.StartActivity(nameof(R_ServiceGetRecord));
        _logger.LogInfo("Start - Get Unit Record");

        var loEx = new R_Exception();
        var loRtn = new R_ServiceGetRecordResultDTO<PMT06000OvtUnitDTO>();

        try
        {
            var loCls = new PMT06000UnitCls();

            _logger.LogInfo("Set Parameter");
            poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
            poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;

            _logger.LogInfo("Get Unit Record");
            loRtn.data = loCls.R_GetRecord(poParameter.Entity);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }

        loEx.ThrowExceptionIfErrors();
        _logger.LogInfo("End - Get Unit Record");
        return loRtn;
    }

    [HttpPost]
    public R_ServiceSaveResultDTO<PMT06000OvtUnitDTO> R_ServiceSave(
        R_ServiceSaveParameterDTO<PMT06000OvtUnitDTO> poParameter)
    {
        using var loActivity = _activitySource.StartActivity(nameof(R_ServiceSave));
        _logger.LogInfo("Start - Save Unit Entity");
        var loEx = new R_Exception();
        R_ServiceSaveResultDTO<PMT06000OvtUnitDTO> loRtn = null;

        try
        {
            var loCls = new PMT06000UnitCls();
            loRtn = new R_ServiceSaveResultDTO<PMT06000OvtUnitDTO>();

            _logger.LogInfo("Set Parameter");
            poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
            poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;

            _logger.LogInfo("Save Unit Entity");
            loRtn.data = loCls.R_Save(poParameter.Entity, poParameter.CRUDMode);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }

        loEx.ThrowExceptionIfErrors();
        _logger.LogInfo("End - Save Unit Entity");
        return loRtn;
    }

    [HttpPost]
    public R_ServiceDeleteResultDTO R_ServiceDelete(R_ServiceDeleteParameterDTO<PMT06000OvtUnitDTO> poParameter)
    {
        using var loActivity = _activitySource.StartActivity(nameof(R_ServiceDelete));
        _logger.LogInfo("Start - Delete Unit Entity");
        var loEx = new R_Exception();
        R_ServiceDeleteResultDTO loRtn = new();

        try
        {
            var loCls = new PMT06000UnitCls();

            _logger.LogInfo("Set Parameter");
            poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
            poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;

            _logger.LogInfo("Delete Unit Entity");
            loCls.R_Delete(poParameter.Entity);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }

        loEx.ThrowExceptionIfErrors();
        _logger.LogInfo("End - Delete Unit Entity");
        return loRtn;
    }
}