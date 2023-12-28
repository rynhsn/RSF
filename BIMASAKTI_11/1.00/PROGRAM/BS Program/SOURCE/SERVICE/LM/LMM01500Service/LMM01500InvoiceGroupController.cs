using LMM01500Back;
using LMM01500Common;
using LMM01500Common.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;

namespace LMM01500Service;

[ApiController]
[Route("api/[controller]/[action]")]
public class LMM01500InvoiceGroupController : ControllerBase, ILMM01500InvoiceGroup
{
    private LoggerLMM01500 _logger;

    public LMM01500InvoiceGroupController(ILogger<LMM01500InitController> logger)
    {
        //Initial and Get Logger
        LoggerLMM01500.R_InitializeLogger(logger);
        _logger = LoggerLMM01500.R_GetInstanceLogger();
    }

    [HttpPost]
    public R_ServiceGetRecordResultDTO<LMM01500InvGrpDTO> R_ServiceGetRecord(
        R_ServiceGetRecordParameterDTO<LMM01500InvGrpDTO> poParameter)
    {
        _logger.LogInfo("Start - Get Invoice Group Record");

        R_Exception loEx = new();
        R_ServiceGetRecordResultDTO<LMM01500InvGrpDTO> loRtn = new();

        try
        {
            var loCls = new LMM01500InvGrpCls();

            _logger.LogInfo("Set Parameter");
            poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
            poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;

            _logger.LogInfo("Get Invoice Group Record");
            loRtn.data = loCls.R_GetRecord(poParameter.Entity);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }

        loEx.ThrowExceptionIfErrors();
        _logger.LogInfo("End - Get Invoice Group Record");
        return loRtn;
    }

    [HttpPost]
    public R_ServiceSaveResultDTO<LMM01500InvGrpDTO> R_ServiceSave(
        R_ServiceSaveParameterDTO<LMM01500InvGrpDTO> poParameter)
    {
        _logger.LogInfo("Start - Save Invoice Group Entity");
        R_Exception loEx = new();
        R_ServiceSaveResultDTO<LMM01500InvGrpDTO> loRtn = null;
        LMM01500InvGrpCls loCls;

        try
        {
            loCls = new LMM01500InvGrpCls();
            loRtn = new R_ServiceSaveResultDTO<LMM01500InvGrpDTO>();

            _logger.LogInfo("Set Parameter");
            poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
            poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;

            _logger.LogInfo("Save Invoice Group Entity");
            loRtn.data = loCls.R_Save(poParameter.Entity, poParameter.CRUDMode);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }

        loEx.ThrowExceptionIfErrors();
        _logger.LogInfo("End - Save Invoice Group Entity");
        return loRtn;
    }

    [HttpPost]
    public R_ServiceDeleteResultDTO R_ServiceDelete(R_ServiceDeleteParameterDTO<LMM01500InvGrpDTO> poParameter)
    {
        _logger.LogInfo("Start - Delete Invoice Group Entity");
        R_Exception loEx = new();
        R_ServiceDeleteResultDTO loRtn = new();
        LMM01500InvGrpCls loCls;
        
        try
        {
            loCls = new LMM01500InvGrpCls();
        
            _logger.LogInfo("Set Parameter");
            poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;
        
            _logger.LogInfo("Delete Invoice Group Entity");
            loCls.R_Delete(poParameter.Entity);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }
        
        loEx.ThrowExceptionIfErrors();
        _logger.LogInfo("End - Delete Invoice Group Entity");
        return loRtn;
    }

    [HttpPost]
    public IAsyncEnumerable<LMM01500InvGrpGridDTO> LMM01500GetInvoiceGroupListStream()
    {
        _logger.LogInfo("Start - Get Invoice Group List");
        R_Exception loEx = new();
        LMM01500ParameterDb loDbPar;
        List<LMM01500InvGrpGridDTO> loRtnTmp;
        LMM01500InvGrpCls loCls;
        IAsyncEnumerable<LMM01500InvGrpGridDTO> loRtn = null;

        try
        {
            _logger.LogInfo("Set Parameter");
            
            loDbPar = new LMM01500ParameterDb(); 
            loDbPar.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
            loDbPar.CUSER_ID = R_BackGlobalVar.USER_ID;
            loDbPar.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(LMM01500ContextConstant.CPROPERTY_ID);

            loCls = new LMM01500InvGrpCls();
            _logger.LogInfo("Get Invoice Group List");
            loRtnTmp = loCls.GetInvoiceGroupList(loDbPar);

            loRtn = GetStream(loRtnTmp);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }

        loEx.ThrowExceptionIfErrors();
        _logger.LogInfo("End - Get Invoice Group List");
        return loRtn;
    }
    
    #region "Helper GetStream Function"

    private async IAsyncEnumerable<LMM01500InvGrpGridDTO> GetStream(List<LMM01500InvGrpGridDTO> poParameter)
    {
        foreach (LMM01500InvGrpGridDTO item in poParameter)
        {
            yield return item;
        }
    }

    #endregion
}