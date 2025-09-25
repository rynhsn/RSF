using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PMT01800BACK;
using PMT01800BACK.Activity;
using PMT01800COMMON;
using PMT01800COMMON.DTO;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;

namespace PMT01800SERVICE;

[Route("api/[controller]/[action]")]
[ApiController]
public class PMT01810Controller : ControllerBase, IPMT01810
{
    private LogPMT01800Common _logger;
    private readonly ActivitySource _activitySource;

    public PMT01810Controller(ILogger<PMT01810Controller> logger)
    {
        LogPMT01800Common.R_InitializeLogger(logger);
        _logger = LogPMT01800Common.R_GetInstanceLogger();
        _activitySource = PMT01800Activity.R_InitializeAndGetActivitySource(nameof(PMT01810Controller));
    }

    [HttpPost]
    public R_ServiceGetRecordResultDTO<PMT01810DTO> R_ServiceGetRecord(
        R_ServiceGetRecordParameterDTO<PMT01810DTO> poParameter)
    {
        throw new NotImplementedException();
    }

    [HttpPost]
    public R_ServiceSaveResultDTO<PMT01810DTO> R_ServiceSave(R_ServiceSaveParameterDTO<PMT01810DTO> poParameter)
    {
        throw new NotImplementedException();
    }

    [HttpPost]
    public R_ServiceDeleteResultDTO R_ServiceDelete(R_ServiceDeleteParameterDTO<PMT01810DTO> poParameter)
    {
        using Activity activity = _activitySource.StartActivity("R_ServiceDelete");
        _logger.LogInfo("Begin || ServiceDeleteAssignLoo(Controller)");
        R_Exception loException = new R_Exception();
        R_ServiceDeleteResultDTO loRtn = new R_ServiceDeleteResultDTO();
        PMT01810Cls loCls;

        try
        {
            _logger.LogInfo("Set Parameter || ServiceDeleteAssignLoo(Controller)");
            poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
            poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;

            //poParameter.Entity.CCOMPANY_ID = "RCD";
            //poParameter.Entity.CUSER_ID = "Admin";
            loCls = new PMT01810Cls();
            _logger.LogInfo("Run DeleteSalesmanCls || ServiceDeleteAssignLoo(Controller)");
            loCls.R_Delete(poParameter.Entity);
        }
        catch (Exception ex)
        {
            loException.Add(ex);
            _logger.LogError(loException);
        };
        EndBlock:
        loException.ThrowExceptionIfErrors();
        _logger.LogInfo("End || ServiceDeleteAssignLoo(Controller)");
        return loRtn;
    }

    [HttpPost]
    public IAsyncEnumerable<PMT01810DTO> GetPMT01810Stream()
    {
         using Activity activity = _activitySource.StartActivity("GetPMT01810Stream");
        _logger.LogInfo("Begin || GetPMT01810Stream(Controller)");
        R_Exception loException = new R_Exception();
        PMT01800DBParameter loDbPar;
        List<PMT01810DTO> loRtnTmp;
        PMT01810Cls loCls;
        IAsyncEnumerable<PMT01810DTO> loRtn = null;

        try
        {
            loDbPar = new PMT01800DBParameter();
            _logger.LogInfo("Set Parameter || GetPMT01810Stream(Controller)");
            loDbPar.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
            loDbPar.CUSER_ID = R_BackGlobalVar.USER_ID;
            loDbPar.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstantPMT01800.CPROPERTY_ID);
            loDbPar.CTRANS_CODE = R_Utility.R_GetStreamingContext<string>(ContextConstantPMT01800.CLOC_TRANS_CODE);
            //loDbPar.CCOMPANY_ID = "RCD";
            //loDbPar.CUSER_ID = "Admin";

            loCls = new PMT01810Cls();
            _logger.LogInfo("Run GetAllSalesmanListCls || GetPMT01810Stream(Controller)");
            loRtnTmp = loCls.GetAllLocList(loDbPar);
            _logger.LogInfo("End Run GetPMT01810Stream || GetPMT01810Stream(Controller)");
            loRtn = GetLOC(loRtnTmp);
        }
        catch (Exception ex)
        {
            loException.Add(ex);
            _logger.LogError(loException);
        }

        EndBlock:
        loException.ThrowExceptionIfErrors();
        _logger.LogInfo("End || GetPMT01810Stream(Controller)");
        return loRtn;
    }

    [HttpPost]
    public IAsyncEnumerable<PMT01810DTO> GetPMT01810DetailStream()
    {
        using Activity activity = _activitySource.StartActivity("GetPMT01810DetailStream");
        _logger.LogInfo("Begin || GetPMT01810DetailStream(Controller)");
        R_Exception loException = new R_Exception();
        PMT01800DBParameter loDbPar;
        List<PMT01810DTO> loRtnTmp;
        PMT01810Cls loCls;
        IAsyncEnumerable<PMT01810DTO> loRtn = null;

        try
        {
            loDbPar = new PMT01800DBParameter();
            _logger.LogInfo("Set Parameter || GetPMT01810DetailStream(Controller)");
            loDbPar.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
            loDbPar.CUSER_ID = R_BackGlobalVar.USER_ID;
            loDbPar.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstantPMT01800.CPROPERTY_ID);
            loDbPar.CTRANS_CODE = R_Utility.R_GetStreamingContext<string>(ContextConstantPMT01800.CTRANS_CODE);
            loDbPar.CDEPT_CODE = R_Utility.R_GetStreamingContext<string>(ContextConstantPMT01800.CDEPT_CODE);
            loDbPar.CREF_NO = R_Utility.R_GetStreamingContext<string>(ContextConstantPMT01800.CREF_NO);
            //loDbPar.CCOMPANY_ID = "RCD";
            //loDbPar.CUSER_ID = "Admin";

            loCls = new PMT01810Cls();
            _logger.LogInfo("Run GetAllSalesmanListCls || GetPMT01810DetailStream(Controller)");
            loRtnTmp = loCls.GetLocListDetail(loDbPar);
            _logger.LogInfo("End Run GetPMT01810DetailStream || GetPMT01810DetailStream(Controller)");
            loRtn = GetLOC(loRtnTmp);
        }
        catch (Exception ex)
        {
            loException.Add(ex);
            _logger.LogError(loException);
        }

        EndBlock:
        loException.ThrowExceptionIfErrors();
        _logger.LogInfo("End || GetPMT01810DetailStream(Controller)");
        return loRtn;
    }
    
    
    private async IAsyncEnumerable<PMT01810DTO> GetLOC(List<PMT01810DTO> poParameter)
    {
        foreach (PMT01810DTO item in poParameter)
        {
            yield return item;
        }
    }
}