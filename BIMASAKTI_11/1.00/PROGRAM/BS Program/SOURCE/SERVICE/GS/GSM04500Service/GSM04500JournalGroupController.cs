using System.Reflection;
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
public class GSM04500JournalGroupController : ControllerBase, IGSM04500JournalGroup
{
    private LoggerGSM04500 _logger;

    public GSM04500JournalGroupController(ILogger<GSM04500JournalGroupController> logger)
    {
        //Initial and Get Logger
        LoggerGSM04500.R_InitializeLogger(logger);
        _logger = LoggerGSM04500.R_GetInstanceLogger();
    }

    [HttpPost]
    public R_ServiceGetRecordResultDTO<GSM04500JournalGroupDTO> R_ServiceGetRecord(
        R_ServiceGetRecordParameterDTO<GSM04500JournalGroupDTO> poParameter)
    {
        _logger.LogInfo("Start - Get Journal Group Record");

        R_Exception loEx = new();
        R_ServiceGetRecordResultDTO<GSM04500JournalGroupDTO> loRtn = new();

        try
        {
            var loCls = new GSM04500JournalGroupCls();

            _logger.LogInfo("Set Parameter");
            poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
            poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;

            _logger.LogInfo("Get Journal Group Record");
            loRtn.data = loCls.R_GetRecord(poParameter.Entity);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }

        loEx.ThrowExceptionIfErrors();
        _logger.LogInfo("End - Get Journal Group Record");
        return loRtn;
    }

    [HttpPost]
    public R_ServiceSaveResultDTO<GSM04500JournalGroupDTO> R_ServiceSave(
        R_ServiceSaveParameterDTO<GSM04500JournalGroupDTO> poParameter)
    {
        _logger.LogInfo("Start - Save Journal Group Entity");
        R_Exception loEx = new();
        R_ServiceSaveResultDTO<GSM04500JournalGroupDTO> loRtn = null;
        GSM04500JournalGroupCls loCls;

        try
        {
            loCls = new GSM04500JournalGroupCls();
            loRtn = new R_ServiceSaveResultDTO<GSM04500JournalGroupDTO>();

            _logger.LogInfo("Set Parameter");
            poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
            poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;

            _logger.LogInfo("Save Journal Group Entity");
            loRtn.data = loCls.R_Save(poParameter.Entity, poParameter.CRUDMode);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }

        loEx.ThrowExceptionIfErrors();
        _logger.LogInfo("End - Save Journal Group Entity");
        return loRtn;
    }


    [HttpPost]
    public R_ServiceDeleteResultDTO R_ServiceDelete(R_ServiceDeleteParameterDTO<GSM04500JournalGroupDTO> poParameter)
    {
        _logger.LogInfo("Start - Delete Journal Group Entity");
        R_Exception loEx = new();
        R_ServiceDeleteResultDTO loRtn = new();
        GSM04500JournalGroupCls loCls;

        try
        {
            loCls = new GSM04500JournalGroupCls();

            _logger.LogInfo("Set Parameter");
            poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;

            _logger.LogInfo("Delete Journal Group Entity");
            loCls.R_Delete(poParameter.Entity);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }

        loEx.ThrowExceptionIfErrors();
        _logger.LogInfo("End - Delete Journal Group Entity");
        return loRtn;
    }

    [HttpPost]
    public GSM04500JournalGroupExcelDTO GSM04500DownloadTemplateFile()
    {
        _logger.LogInfo("Start - Download Template File");
        var loEx = new R_Exception();
        var loRtn = new GSM04500JournalGroupExcelDTO();

        try
        {
            _logger.LogInfo("Get Template File");
            var loAsm = Assembly.Load("BIMASAKTI_GS_API");
            
            _logger.LogInfo("Set Resource File");
            var lcResourceFile = "BIMASAKTI_GS_API.Template.Journal Group.xlsx";

            _logger.LogInfo("Get Resource File");
            using (Stream resFilestream = loAsm.GetManifestResourceStream(lcResourceFile))
            {
                var ms = new MemoryStream();
                resFilestream.CopyTo(ms);
                var bytes = ms.ToArray();

                loRtn.FileBytes = bytes;
            }
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }
        
        loEx.ThrowExceptionIfErrors();
        _logger.LogInfo("End - Download Template File");
        return loRtn;
    }

    [HttpPost]
    public IAsyncEnumerable<GSM04500JournalGroupDTO> GSM04500GetAllJournalGroupListStream()
    {
        _logger.LogInfo("Start - Get Journal Group List");
        R_Exception loEx = new();
        GSM04500ParameterDb loDbPar;
        List<GSM04500JournalGroupDTO> loRtnTmp;
        GSM04500JournalGroupCls loCls;
        IAsyncEnumerable<GSM04500JournalGroupDTO> loRtn = null;

        try
        {
            _logger.LogInfo("Set Parameter");
            
            loDbPar = new GSM04500ParameterDb(); 
            loDbPar.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
            loDbPar.CUSER_ID = R_BackGlobalVar.USER_ID;
            loDbPar.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(GSM04500ContextConstant.CPROPERTY_ID);
            loDbPar.CJOURNAL_GROUP_TYPE = R_Utility.R_GetStreamingContext<string>(GSM04500ContextConstant.CJRNGRP_TYPE);

            loCls = new GSM04500JournalGroupCls();
            _logger.LogInfo("Get Journal Group List");
            loRtnTmp = loCls.GetJournalGroupList(loDbPar);

            loRtn = GetStream(loRtnTmp);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }

        loEx.ThrowExceptionIfErrors();
        _logger.LogInfo("End - Get Journal Group List");
        return loRtn;
    }


    #region "Helper GetStream Function"

    private async IAsyncEnumerable<GSM04500JournalGroupDTO> GetStream(List<GSM04500JournalGroupDTO> poParameter)
    {
        foreach (GSM04500JournalGroupDTO item in poParameter)
        {
            yield return item;
        }
    }

    #endregion
}