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
public class LMM01500InvoiceGroupDeptController : ControllerBase, ILMM01500InvoiceGroupDept
{
    private LoggerLMM01500 _logger;

    public LMM01500InvoiceGroupDeptController(ILogger<LMM01500InitController> logger)
    {
        //Initial and Get Logger
        LoggerLMM01500.R_InitializeLogger(logger);
        _logger = LoggerLMM01500.R_GetInstanceLogger();
    }

    [HttpPost]
    public R_ServiceGetRecordResultDTO<LMM01500InvGrpDeptDTO> R_ServiceGetRecord(
        R_ServiceGetRecordParameterDTO<LMM01500InvGrpDeptDTO> poParameter)
    {
        _logger.LogInfo("Start - Get Invoice Group Record");

        R_Exception loEx = new();
        R_ServiceGetRecordResultDTO<LMM01500InvGrpDeptDTO> loRtn = new();

        try
        {
            var loCls = new LMM01500InvGrpDeptCls();

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
    public R_ServiceSaveResultDTO<LMM01500InvGrpDeptDTO> R_ServiceSave(
        R_ServiceSaveParameterDTO<LMM01500InvGrpDeptDTO> poParameter)
    {
        _logger.LogInfo("Start - Save Invoice Group Entity");
        R_Exception loEx = new();
        R_ServiceSaveResultDTO<LMM01500InvGrpDeptDTO> loRtn = null;
        LMM01500InvGrpDeptCls loCls;

        try
        {
            loCls = new LMM01500InvGrpDeptCls();
            loRtn = new R_ServiceSaveResultDTO<LMM01500InvGrpDeptDTO>();

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
    public R_ServiceDeleteResultDTO R_ServiceDelete(R_ServiceDeleteParameterDTO<LMM01500InvGrpDeptDTO> poParameter)
    {
        _logger.LogInfo("Start - Delete Invoice Group Entity");
        R_Exception loEx = new();
        R_ServiceDeleteResultDTO loRtn = new();
        LMM01500InvGrpDeptCls loCls;
        
        try
        {
            loCls = new LMM01500InvGrpDeptCls();
        
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
    public IAsyncEnumerable<LMM01500InvGrpDeptGridDTO> LMM01500GetInvoiceGroupDeptListStream()
    {
        _logger.LogInfo("Start - Get Invoice Group List");
        R_Exception loEx = new();
        LMM01500ParameterDb loDbPar;
        List<LMM01500InvGrpDeptGridDTO> loRtnTmp;
        LMM01500InvGrpDeptCls loCls;
        IAsyncEnumerable<LMM01500InvGrpDeptGridDTO> loRtn = null;

        try
        {
            _logger.LogInfo("Set Parameter");
            
            loDbPar = new LMM01500ParameterDb(); 
            loDbPar.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
            loDbPar.CUSER_ID = R_BackGlobalVar.USER_ID;
            loDbPar.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(LMM01500ContextConstant.CPROPERTY_ID);

            loCls = new LMM01500InvGrpDeptCls();
            _logger.LogInfo("Get Invoice Group List");
            loRtnTmp = loCls.GetInvoiceGroupDeptList(loDbPar);

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

    private async IAsyncEnumerable<LMM01500InvGrpDeptGridDTO> GetStream(List<LMM01500InvGrpDeptGridDTO> poParameter)
    {
        foreach (LMM01500InvGrpDeptGridDTO item in poParameter)
        {
            yield return item;
        }
    }

    #endregion
}