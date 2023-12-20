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
public class GSM04500AccountSettingDeptController : ControllerBase, IGSM04500AccountSetting
{
    
    private LoggerGSM04500 _logger;

    public GSM04500AccountSettingController(ILogger<GSM04500AccountSettingController> logger)
    {
        //Initial and Get Logger
        LoggerGSM04500.R_InitializeLogger(logger);
        _logger = LoggerGSM04500.R_GetInstanceLogger();
    }

    [HttpPost]
    public R_ServiceGetRecordResultDTO<GSM04500AccountSettingDTO> R_ServiceGetRecord(
        R_ServiceGetRecordParameterDTO<GSM04500AccountSettingDTO> poParameter)
    {
        _logger.LogInfo("Start - Get Account Setting Record");

        R_Exception loEx = new();
        R_ServiceGetRecordResultDTO<GSM04500AccountSettingDTO> loRtn = new();

        try
        {
            var loCls = new GSM04500AccountSettingCls();

            _logger.LogInfo("Set Parameter");
            poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
            poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;

            _logger.LogInfo("Get Account Setting Record");
            loRtn.data = loCls.R_GetRecord(poParameter.Entity);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }

        loEx.ThrowExceptionIfErrors();
        _logger.LogInfo("End - Get Account Setting Record");
        return loRtn;
    }


    [HttpPost]
    public R_ServiceSaveResultDTO<GSM04500AccountSettingDTO> R_ServiceSave(
        R_ServiceSaveParameterDTO<GSM04500AccountSettingDTO> poParameter)
    {
        _logger.LogInfo("Start - Save Account Setting Entity");
        R_Exception loEx = new();
        R_ServiceSaveResultDTO<GSM04500AccountSettingDTO> loRtn = null;
        GSM04500AccountSettingCls loCls;

        try
        {
            loCls = new GSM04500AccountSettingCls();
            loRtn = new R_ServiceSaveResultDTO<GSM04500AccountSettingDTO>();

            _logger.LogInfo("Set Parameter");
            poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
            poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;

            _logger.LogInfo("Save Account Setting Entity");
            loRtn.data = loCls.R_Save(poParameter.Entity, poParameter.CRUDMode);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }

        loEx.ThrowExceptionIfErrors();
        _logger.LogInfo("End - Save Account Setting Entity");
        return loRtn;
    }


    [HttpPost]
    public R_ServiceDeleteResultDTO R_ServiceDelete(R_ServiceDeleteParameterDTO<GSM04500AccountSettingDTO> poParameter)
    {
        _logger.LogInfo("Start - Delete Account Setting Entity");
        R_Exception loEx = new();
        R_ServiceDeleteResultDTO loRtn = new();
        GSM04500AccountSettingCls loCls;

        try
        {
            loCls = new GSM04500AccountSettingCls();

            _logger.LogInfo("Set Parameter");
            poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;

            _logger.LogInfo("Delete Account Setting Entity");
            loCls.R_Delete(poParameter.Entity);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }

        loEx.ThrowExceptionIfErrors();
        _logger.LogInfo("End - Delete Account Setting Entity");
        return loRtn;
    }


    [HttpPost]
    public IAsyncEnumerable<GSM04500AccountSettingDTO> GSM04500GetAllAccountSettingListStream()
    {
        _logger.LogInfo("Start - Get Account Setting List");
        R_Exception loEx = new();
        GSM04500ParameterDb loDbPar;
        List<GSM04500AccountSettingDTO> loRtnTmp;
        GSM04500AccountSettingCls loCls;
        IAsyncEnumerable<GSM04500AccountSettingDTO> loRtn = null;

        try
        {
            _logger.LogInfo("Set Parameter");
            
            loDbPar = new GSM04500ParameterDb(); 
            loDbPar.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
            loDbPar.CUSER_ID = R_BackGlobalVar.USER_ID;
            loDbPar.CPROPERTY_ID = R_Utility.R_GetContext<string>(ContextConstant.CPROPERTY_ID);
            loDbPar.CJOURNAL_GROUP_TYPE = R_Utility.R_GetContext<string>(ContextConstant.CJRNGRP_TYPE);

            loCls = new GSM04500AccountSettingCls();
            _logger.LogInfo("Get Account Setting List");
            loRtnTmp = loCls.GetAccountSettingList(loDbPar);

            loRtn = GetStream(loRtnTmp);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }

        loEx.ThrowExceptionIfErrors();
        _logger.LogInfo("End - Get Account Setting List");
        return loRtn;
    }
    
    #region "Helper GetStream Function"

    private async IAsyncEnumerable<GSM04500AccountSettingDTO> GetStream(List<GSM04500AccountSettingDTO> poParameter)
    {
        foreach (GSM04500AccountSettingDTO item in poParameter)
        {
            yield return item;
        }
    }

    #endregion
}