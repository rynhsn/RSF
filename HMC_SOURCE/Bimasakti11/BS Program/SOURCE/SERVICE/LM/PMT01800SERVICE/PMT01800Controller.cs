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
public class PMT01800Controller : ControllerBase, IPMT01800
{
    private LogPMT01800Common _logger;
    private readonly ActivitySource _activitySource;

    public PMT01800Controller(ILogger<PMT01800Controller> logger)
    {
        LogPMT01800Common.R_InitializeLogger(logger);
        _logger = LogPMT01800Common.R_GetInstanceLogger();
        _activitySource = PMT01800Activity.R_InitializeAndGetActivitySource(nameof(PMT01800Controller));
    }

    [HttpPost]
    public R_ServiceGetRecordResultDTO<PMT01800DTO> R_ServiceGetRecord(R_ServiceGetRecordParameterDTO<PMT01800DTO> poParameter)
    {
        throw new NotImplementedException();
    }

    [HttpPost]
    public R_ServiceSaveResultDTO<PMT01800DTO> R_ServiceSave(R_ServiceSaveParameterDTO<PMT01800DTO> poParameter)
    {
        throw new NotImplementedException();
    }

    [HttpPost]
    public R_ServiceDeleteResultDTO R_ServiceDelete(R_ServiceDeleteParameterDTO<PMT01800DTO> poParameter)
    {
        throw new NotImplementedException();
    }

    [HttpPost]
    public IAsyncEnumerable<PMT01800PropertyDTO> GetPropertySream()
    {
        using Activity activity = _activitySource.StartActivity("GetPropertySream");
        _logger.LogInfo("Begin || GetPropertySream(Controller)");
        R_Exception loException = new R_Exception();
        PMT01800DBParameter loDbPar;
        List<PMT01800PropertyDTO> loRtnTmp;
        PMT01800Cls loCls;
        IAsyncEnumerable<PMT01800PropertyDTO> loRtn = null;

        try
        {
            loDbPar = new PMT01800DBParameter();
            _logger.LogInfo("Set Parameter || GetPropertySream(Controller)");
            loDbPar.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
            loDbPar.CUSER_ID = R_BackGlobalVar.USER_ID;
            //loDbPar.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstantPMT01800.CPROPERTY_ID);
            //loDbPar.CCOMPANY_ID = "RCD";
            //loDbPar.CUSER_ID = "Admin";

            loCls = new PMT01800Cls();
            _logger.LogInfo("Run GetAllPropertyListCls || GetPropertySream(Controller)");
            loRtnTmp = loCls.GetAllPropertyList(loDbPar);
            _logger.LogInfo("Run GetPropertyStream || GetPropertySream(Controller)");
            loRtn = GetProperty(loRtnTmp);
        }
        catch (Exception ex)
        {
            loException.Add(ex);
            _logger.LogError(loException);
        }

        EndBlock:
        loException.ThrowExceptionIfErrors();
        _logger.LogInfo("End || GetPropertySream(Controller)");
        return loRtn;
    }

    [HttpPost]
    public IAsyncEnumerable<PMT01800DTO> GetPMT01800Stream()
    {
        using Activity activity = _activitySource.StartActivity("GetPMT01800Stream");
        _logger.LogInfo("Begin || GetPMT01800Stream(Controller)");
        R_Exception loException = new R_Exception();
        PMT01800DBParameter loDbPar;
        List<PMT01800DTO> loRtnTmp;
        PMT01800Cls loCls;
        IAsyncEnumerable<PMT01800DTO> loRtn = null;

        try
        {
            loDbPar = new PMT01800DBParameter();
            _logger.LogInfo("Set Parameter || GetPMT01800Stream(Controller)");
            loDbPar.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
            loDbPar.CUSER_ID = R_BackGlobalVar.USER_ID;
            loDbPar.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstantPMT01800.CPROPERTY_ID);

            loDbPar.CLOC_TRANS_CODE = R_Utility.R_GetStreamingContext<string>(ContextConstantPMT01800.CLOC_TRANS_CODE);
            loDbPar.CLOO_TRANS_CODE = R_Utility.R_GetStreamingContext<string>(ContextConstantPMT01800.CLOO_TRANS_CODE);
            
            loDbPar.CFROM_DATE = R_Utility.R_GetStreamingContext<string>(ContextConstantPMT01800.CFROM_DATE);
            loDbPar.CLANG_ID = R_BackGlobalVar.CULTURE;
            //loDbPar.CCOMPANY_ID = "RCD";
            //loDbPar.CUSER_ID = "Admin";

            loCls = new PMT01800Cls();
            _logger.LogInfo("Run GetAllSalesmanListCls || GetPMT01800Stream(Controller)");
            loRtnTmp = loCls.GetAllLooList(loDbPar);
            _logger.LogInfo("End Run GetPMT01800Stream || GetPMT01800Stream(Controller)");
            loRtn = GetLOO(loRtnTmp);
        }
        catch (Exception ex)
        {
            loException.Add(ex);
            _logger.LogError(loException);
        }

        EndBlock:
        loException.ThrowExceptionIfErrors();
        _logger.LogInfo("End || GetPMT01800Stream(Controller)");
        return loRtn;
    }

    [HttpPost]
    public IAsyncEnumerable<PMT01800DTO> GetPMT01800DetailStream()
    {
        using Activity activity = _activitySource.StartActivity("GetPMT01800DetailStream");
        _logger.LogInfo("Begin || GetPMT01800DetailStream(Controller)");
        R_Exception loException = new R_Exception();
        PMT01800DBParameter loDbPar;
        List<PMT01800DTO> loRtnTmp;
        PMT01800Cls loCls;
        IAsyncEnumerable<PMT01800DTO> loRtn = null;

        try
        {
            loDbPar = new PMT01800DBParameter();
            _logger.LogInfo("Set Parameter || GetPMT01800DetailStream(Controller)");
            loDbPar.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
            loDbPar.CUSER_ID = R_BackGlobalVar.USER_ID;
            loDbPar.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstantPMT01800.CPROPERTY_ID);
            loDbPar.CTRANS_CODE = R_Utility.R_GetStreamingContext<string>(ContextConstantPMT01800.CTRANS_CODE);
            loDbPar.CDEPT_CODE = R_Utility.R_GetStreamingContext<string>(ContextConstantPMT01800.CDEPT_CODE);
            loDbPar.CREF_NO = R_Utility.R_GetStreamingContext<string>(ContextConstantPMT01800.CREF_NO);
            //loDbPar.CCOMPANY_ID = "RCD";
            //loDbPar.CUSER_ID = "Admin";

            loCls = new PMT01800Cls();
            _logger.LogInfo("Run GetAllSalesmanListCls || GetPMT01800DetailStream(Controller)");
            loRtnTmp = loCls.GetLooListDetail(loDbPar);
            _logger.LogInfo("End Run GetPMT01800DetailStream || GetPMT01800DetailStream(Controller)");
            loRtn = GetLOO(loRtnTmp);
        }
        catch (Exception ex)
        {
            loException.Add(ex);
            _logger.LogError(loException);
        }

        EndBlock:
        loException.ThrowExceptionIfErrors();
        _logger.LogInfo("End || GetPMT01800DetailStream(Controller)");
        return loRtn;
    }

    [HttpPost]
    public PMT01800InitialProcessDTO GetInitDayStream()
    {
        using Activity activity = _activitySource.StartActivity(nameof(GetInitDayStream));
        _logger.LogInfo("Start GetInitYearStream");
        var loException = new R_Exception();
        PMT01800InitialProcessDTO loRtn = null;
        try
        {
            var loCls = new PMT01800Cls();
            var poParam = new PMT01800DBParameter();

            _logger.LogInfo("Set Param GetInitYearStream");
            poParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;

            _logger.LogInfo("Call Back Method GetInitYearStream");
            loRtn = loCls.InitSpinnerYear(poParam);
        }
        catch (Exception ex)
        {
            loException.Add(ex);
        }

        loException.ThrowExceptionIfErrors();

        _logger.LogInfo("End GetInitYearStream");
        return loRtn;
    }

    private async IAsyncEnumerable<PMT01800PropertyDTO> GetProperty(List<PMT01800PropertyDTO> poParameter)
    {
        foreach (PMT01800PropertyDTO item in poParameter)
        {
            yield return item;
        }
    }
    
    private async IAsyncEnumerable<PMT01800DTO> GetLOO(List<PMT01800DTO> poParameter)
    {
        foreach (PMT01800DTO item in poParameter)
        {
            yield return item;
        }
    }
}