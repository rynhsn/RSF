using GSM04500Back;
using GSM04500Common;
using GSM04500Common.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;

namespace GSM04500Service;

[ApiController]
[Route("api/[controller]/[action]")]
public class GSM04500AccountSettingDeptController : ControllerBase, IGSM04500AccountSettingDept
{
    
    private LoggerGSM04500 _logger;

    public GSM04500AccountSettingDeptController(ILogger<GSM04500AccountSettingDeptController> logger)
    {
        //Initial and Get Logger
        LoggerGSM04500.R_InitializeLogger(logger);
        _logger = LoggerGSM04500.R_GetInstanceLogger();
    }

    [HttpPost]
    public R_ServiceGetRecordResultDTO<GSM04500AccountSettingDeptDTO> R_ServiceGetRecord(
        R_ServiceGetRecordParameterDTO<GSM04500AccountSettingDeptDTO> poParameter)
    {
        _logger.LogInfo("Start - Get Account Setting Dept Record");

        R_Exception loEx = new();
        R_ServiceGetRecordResultDTO<GSM04500AccountSettingDeptDTO> loRtn = new();

        try
        {
            var loCls = new GSM04500AccountSettingDeptCls();

            _logger.LogInfo("Set Parameter");
            poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
            poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;

            _logger.LogInfo("Get Account Setting Dept Record");
            loRtn.data = loCls.R_GetRecord(poParameter.Entity);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }

        loEx.ThrowExceptionIfErrors();
        _logger.LogInfo("End - Get Account Setting Dept Record");
        return loRtn;
    }


    [HttpPost]
    public R_ServiceSaveResultDTO<GSM04500AccountSettingDeptDTO> R_ServiceSave(
        R_ServiceSaveParameterDTO<GSM04500AccountSettingDeptDTO> poParameter)
    {
        _logger.LogInfo("Start - Save Account Setting Dept Entity");
        R_Exception loEx = new();
        R_ServiceSaveResultDTO<GSM04500AccountSettingDeptDTO> loRtn = null;
        GSM04500AccountSettingDeptCls loCls;

        try
        {
            loCls = new GSM04500AccountSettingDeptCls();
            loRtn = new R_ServiceSaveResultDTO<GSM04500AccountSettingDeptDTO>();

            _logger.LogInfo("Set Parameter");
            poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
            poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;

            _logger.LogInfo("Save Account Setting Dept Entity");
            loRtn.data = loCls.R_Save(poParameter.Entity, poParameter.CRUDMode);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }

        loEx.ThrowExceptionIfErrors();
        _logger.LogInfo("End - Save Account Setting Dept Entity");
        return loRtn;
    }


    [HttpPost]
    public R_ServiceDeleteResultDTO R_ServiceDelete(R_ServiceDeleteParameterDTO<GSM04500AccountSettingDeptDTO> poParameter)
    {
        _logger.LogInfo("Start - Delete Account Setting Dept Entity");
        R_Exception loEx = new();
        R_ServiceDeleteResultDTO loRtn = new();
        GSM04500AccountSettingDeptCls loCls;

        try
        {
            loCls = new GSM04500AccountSettingDeptCls();

            _logger.LogInfo("Set Parameter");
            poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;

            _logger.LogInfo("Delete Account Setting Dept Entity");
            loCls.R_Delete(poParameter.Entity);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }

        loEx.ThrowExceptionIfErrors();
        _logger.LogInfo("End - Delete Account Setting Dept Entity");
        return loRtn;
    }


    [HttpPost]
    public IAsyncEnumerable<GSM04500AccountSettingDeptDTO> GSM04500GetAllAccountSettingDeptListStream()
    {
        _logger.LogInfo("Start - Get Account Setting Dept List");
        R_Exception loEx = new();
        GSM04500ParameterDb loDbPar;
        List<GSM04500AccountSettingDeptDTO> loRtnTmp;
        GSM04500AccountSettingDeptCls loCls;
        IAsyncEnumerable<GSM04500AccountSettingDeptDTO> loRtn = null;

        try
        {
            _logger.LogInfo("Set Parameter");
            
            loDbPar = new GSM04500ParameterDb(); 
            loDbPar.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
            loDbPar.CUSER_ID = R_BackGlobalVar.USER_ID;
            loDbPar.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(GSM04500ContextConstant.CPROPERTY_ID);
            loDbPar.CJOURNAL_GROUP_TYPE = R_Utility.R_GetStreamingContext<string>(GSM04500ContextConstant.CJRNGRP_TYPE);
            loDbPar.CJOURNAL_GROUP_CODE = R_Utility.R_GetStreamingContext<string>(GSM04500ContextConstant.CJRNGRP_CODE);
            loDbPar.CGOA_CODE = R_Utility.R_GetStreamingContext<string>(GSM04500ContextConstant.CGOA_CODE);

            loCls = new GSM04500AccountSettingDeptCls();
            _logger.LogInfo("Get Account Setting Dept List");
            loRtnTmp = loCls.GetAccountSettingDeptList(loDbPar);

            loRtn = GetStream(loRtnTmp);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }

        loEx.ThrowExceptionIfErrors();
        _logger.LogInfo("End - Get Account Setting Dept List");
        return loRtn;
    }
    
    #region "Helper GetStream Function"

    private async IAsyncEnumerable<GSM04500AccountSettingDeptDTO> GetStream(List<GSM04500AccountSettingDeptDTO> poParameter)
    {
        foreach (GSM04500AccountSettingDeptDTO item in poParameter)
        {
            yield return item;
        }
    }

    #endregion
}