using System.Diagnostics;
using GSM05000Back;
using GSM05000Common;
using GSM05000Common.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using R_OpenTelemetry;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;

namespace GSM05000Service;

#region "Non Async Version"
// [ApiController]
// [Route("api/[controller]/[action]")]
// public class GSM05000NumberingController : ControllerBase, IGSM05000Numbering
// {
//     private LoggerGSM05000 _logger;
//     private readonly ActivitySource _activitySource;
//
//     public GSM05000NumberingController(ILogger<GSM05000NumberingController> logger)
//     {
//         //Initial and Get Logger
//         LoggerGSM05000.R_InitializeLogger(logger);
//         _logger = LoggerGSM05000.R_GetInstanceLogger();
//         _activitySource = GSM05000Activity.R_InitializeAndGetActivitySource(nameof(GSM05000NumberingController));
//     }
//     
//     [HttpPost]
//     public R_ServiceGetRecordResultDTO<GSM05000NumberingGridDTO> R_ServiceGetRecord(R_ServiceGetRecordParameterDTO<GSM05000NumberingGridDTO> poParameter)
//     {
//         using var loActivity = _activitySource.StartActivity(nameof(R_ServiceGetRecord));
//         _logger.LogInfo("Start - Get Numbering Record");
//         R_Exception loEx = new();
//         R_ServiceGetRecordResultDTO<GSM05000NumberingGridDTO> loRtn = new();
//
//         try
//         {
//             var loCls = new GSM05000NumberingCls();
//             
//             _logger.LogInfo("Set Parameter");
//             poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
//             poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;
//             
//             _logger.LogInfo("Get Numbering Record");
//             loRtn.data = loCls.R_GetRecord(poParameter.Entity);
//         }
//         catch (Exception ex)
//         {
//             loEx.Add(ex);
//             _logger.LogError(loEx);
//         }
//         
//         loEx.ThrowExceptionIfErrors();
//         _logger.LogInfo("End - Get Numbering Record");
//         return loRtn;
//     }
//
//     [HttpPost]
//     public R_ServiceSaveResultDTO<GSM05000NumberingGridDTO> R_ServiceSave(R_ServiceSaveParameterDTO<GSM05000NumberingGridDTO> poParameter)
//     {
//         using var loActivity = _activitySource.StartActivity(nameof(R_ServiceSave));
//         _logger.LogInfo("Start - Save Numbering Entity");
//         R_Exception loEx = new();
//         R_ServiceSaveResultDTO<GSM05000NumberingGridDTO> loRtn = new();
//         GSM05000NumberingCls loCls;
//
//         try
//         {
//             loCls = new GSM05000NumberingCls();
//             loRtn = new R_ServiceSaveResultDTO<GSM05000NumberingGridDTO>();
//             
//             _logger.LogInfo("Set Parameter");
//             poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
//             poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;
//
//             _logger.LogInfo("Save Numbering Entity");
//             loRtn.data = loCls.R_Save(poParameter.Entity, poParameter.CRUDMode);
//         }
//         catch (Exception ex)
//         {
//             loEx.Add(ex);
//             _logger.LogError(loEx);
//         }
//         
//         loEx.ThrowExceptionIfErrors();
//         _logger.LogInfo("End - Save Numbering Entity");
//         return loRtn;
//     }
//
//     [HttpPost]
//     public R_ServiceDeleteResultDTO R_ServiceDelete(R_ServiceDeleteParameterDTO<GSM05000NumberingGridDTO> poParameter)
//     {
//         using var loActivity = _activitySource.StartActivity(nameof(R_ServiceDelete));
//         _logger.LogInfo("Start - Delete Numbering Entity");
//         R_Exception loEx = new();
//         R_ServiceDeleteResultDTO loRtn = new();
//         GSM05000NumberingCls loCls;
//
//         try
//         {
//             loCls = new GSM05000NumberingCls();
//             
//             _logger.LogInfo("Set Parameter");
//             poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
//             poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;
//             
//             _logger.LogInfo("Delete Numbering Entity");
//             loCls.R_Delete(poParameter.Entity);
//         }
//         catch (Exception ex)
//         {
//             loEx.Add(ex);
//             _logger.LogError(loEx);
//         }
//
//         loEx.ThrowExceptionIfErrors();
//         _logger.LogInfo("End - Delete Numbering Entity");
//         return loRtn;
//     }
//
//     [HttpPost]
//     public IAsyncEnumerable<GSM05000NumberingGridDTO> GetNumberingListStream()
//     {
//         using var loActivity = _activitySource.StartActivity(nameof(GetNumberingListStream));
//         _logger.LogInfo("Start - Get Numbering List");
//         R_Exception loEx = new();
//         IAsyncEnumerable<GSM05000NumberingGridDTO> loRtn = null;
//         List<GSM05000NumberingGridDTO> loResult;
//         GSM05000ParameterDb loDbPar;
//         GSM05000NumberingCls loCls;
//
//         try
//         {
//             loDbPar = new GSM05000ParameterDb();
//             
//             _logger.LogInfo("Set Parameter");
//             loDbPar.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
//             loDbPar.CUSER_LOGIN_ID = R_BackGlobalVar.USER_ID;
//             loDbPar.CTRANS_CODE = R_Utility.R_GetStreamingContext<string>(GSM05000ContextConstant.CTRANSACTION_CODE);
//
//             loCls = new GSM05000NumberingCls();
//             
//             _logger.LogInfo("Get Numbering List");
//             loResult = loCls.GetNumberingListDb(loDbPar);
//             loRtn = GetNumberingStream(loResult);
//         }
//         catch (Exception ex)
//         {
//             loEx.Add(ex);
//             _logger.LogError(loEx);
//         }
//
//         loEx.ThrowExceptionIfErrors();
//         _logger.LogInfo("End - Get Numbering List");
//         return loRtn;
//     }
//
//     [HttpPost]
//     public GSM05000NumberingHeaderDTO GetNumberingHeader(GSM05000TrxCodeParamsDTO poParams)
//     {
//         using var loActivity = _activitySource.StartActivity(nameof(GetNumberingHeader));
//         _logger.LogInfo("Start - Get Numbering Header");
//         R_Exception loEx = new();
//         GSM05000NumberingHeaderDTO loRtn = null;
//         GSM05000ParameterDb loDbPar;
//         GSM05000NumberingCls loCls;
//
//         try
//         {
//             loDbPar = new GSM05000ParameterDb();
//
//             _logger.LogInfo("Set Parameter");
//             loDbPar.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
//             loDbPar.CTRANS_CODE = poParams.CTRANS_CODE;
//
//             loCls = new GSM05000NumberingCls();
//             
//             _logger.LogInfo("Get Numbering Header");
//             loRtn = loCls.GetNumberingHeaderDb(loDbPar);
//         }
//         catch (Exception ex)
//         {
//             loEx.Add(ex);
//             _logger.LogError(loEx);
//         }
//
//         loEx.ThrowExceptionIfErrors();
//         _logger.LogInfo("End - Get Numbering Header");
//         return loRtn;
//     }
//     
//     #region "Helper ListStream Functions"
//     private async IAsyncEnumerable<GSM05000NumberingGridDTO> GetNumberingStream(List<GSM05000NumberingGridDTO> poParameter)
//     {
//         foreach (GSM05000NumberingGridDTO item in poParameter)
//         {
//             yield return item;
//         }
//     }
//     #endregion
// }
#endregion

#region "Async Version"
[ApiController]
[Route("api/[controller]/[action]")]
public class GSM05000NumberingController : ControllerBase, IGSM05000NumberingAsync
{
    private LoggerGSM05000 _logger;
    private readonly ActivitySource _activitySource;

    public GSM05000NumberingController(ILogger<GSM05000NumberingController> logger)
    {
        //Initial and Get Logger
        LoggerGSM05000.R_InitializeLogger(logger);
        _logger = LoggerGSM05000.R_GetInstanceLogger();
        _activitySource = GSM05000Activity.R_InitializeAndGetActivitySource(nameof(GSM05000NumberingController));
    }
    
    [HttpPost]
    public async Task<R_ServiceGetRecordResultDTO<GSM05000NumberingGridDTO>> R_ServiceGetRecord(R_ServiceGetRecordParameterDTO<GSM05000NumberingGridDTO> poParameter)
    {
        using var loActivity = _activitySource.StartActivity(nameof(R_ServiceGetRecord));
        _logger.LogInfo("Start - Get Numbering Record");
        R_Exception loEx = new();
        R_ServiceGetRecordResultDTO<GSM05000NumberingGridDTO> loRtn = new();

        try
        {
            var loCls = new GSM05000NumberingCls();
            
            _logger.LogInfo("Set Parameter");
            poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
            poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;
            
            _logger.LogInfo("Get Numbering Record");
            loRtn.data = await loCls.R_GetRecordAsync(poParameter.Entity);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }
        
        loEx.ThrowExceptionIfErrors();
        _logger.LogInfo("End - Get Numbering Record");
        return loRtn;
    }

    [HttpPost]
    public async Task<R_ServiceSaveResultDTO<GSM05000NumberingGridDTO>> R_ServiceSave(R_ServiceSaveParameterDTO<GSM05000NumberingGridDTO> poParameter)
    {
        using var loActivity = _activitySource.StartActivity(nameof(R_ServiceSave));
        _logger.LogInfo("Start - Save Numbering Entity");
        R_Exception loEx = new();
        R_ServiceSaveResultDTO<GSM05000NumberingGridDTO> loRtn = new();
        GSM05000NumberingCls loCls;

        try
        {
            loCls = new GSM05000NumberingCls();
            loRtn = new R_ServiceSaveResultDTO<GSM05000NumberingGridDTO>();
            
            _logger.LogInfo("Set Parameter");
            poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
            poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;

            _logger.LogInfo("Save Numbering Entity");
            loRtn.data = await loCls.R_SaveAsync(poParameter.Entity, poParameter.CRUDMode);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }
        
        loEx.ThrowExceptionIfErrors();
        _logger.LogInfo("End - Save Numbering Entity");
        return loRtn;
    }

    [HttpPost]
    public async Task<R_ServiceDeleteResultDTO> R_ServiceDelete(R_ServiceDeleteParameterDTO<GSM05000NumberingGridDTO> poParameter)
    {
        using var loActivity = _activitySource.StartActivity(nameof(R_ServiceDelete));
        _logger.LogInfo("Start - Delete Numbering Entity");
        R_Exception loEx = new();
        R_ServiceDeleteResultDTO loRtn = new();
        GSM05000NumberingCls loCls;

        try
        {
            loCls = new GSM05000NumberingCls();
            
            _logger.LogInfo("Set Parameter");
            poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
            poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;
            
            _logger.LogInfo("Delete Numbering Entity");
            await loCls.R_DeleteAsync(poParameter.Entity);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }

        loEx.ThrowExceptionIfErrors();
        _logger.LogInfo("End - Delete Numbering Entity");
        return loRtn;
    }

    [HttpPost]
    public async IAsyncEnumerable<GSM05000NumberingGridDTO> GetNumberingListStream()
    {
        using var loActivity = _activitySource.StartActivity(nameof(GetNumberingListStream));
        _logger.LogInfo("Start - Get Numbering List");
        R_Exception loEx = new();
        List<GSM05000NumberingGridDTO> loResult = new();
        GSM05000ParameterDb loDbPar;
        GSM05000NumberingCls loCls;

        try
        {
            loDbPar = new GSM05000ParameterDb();
            
            _logger.LogInfo("Set Parameter");
            loDbPar.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
            loDbPar.CUSER_LOGIN_ID = R_BackGlobalVar.USER_ID;
            loDbPar.CTRANS_CODE = R_Utility.R_GetStreamingContext<string>(GSM05000ContextConstant.CTRANSACTION_CODE);

            loCls = new GSM05000NumberingCls();
            
            _logger.LogInfo("Get Numbering List");
            loResult = await loCls.GetNumberingListDbAsync(loDbPar);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }

        loEx.ThrowExceptionIfErrors();
        _logger.LogInfo("End - Get Numbering List");
        
        foreach (var loItem in loResult)
        {
            yield return loItem;
        }
    }

    [HttpPost]
    public async Task<GSM05000NumberingHeaderDTO> GetNumberingHeader(GSM05000TrxCodeParamsDTO poParams)
    {
        using var loActivity = _activitySource.StartActivity(nameof(GetNumberingHeader));
        _logger.LogInfo("Start - Get Numbering Header");
        R_Exception loEx = new();
        GSM05000NumberingHeaderDTO loRtn = new();
        GSM05000ParameterDb loDbPar;
        GSM05000NumberingCls loCls;

        try
        {
            loDbPar = new GSM05000ParameterDb();

            _logger.LogInfo("Set Parameter");
            loDbPar.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
            loDbPar.CTRANS_CODE = poParams.CTRANS_CODE;

            loCls = new GSM05000NumberingCls();
            
            _logger.LogInfo("Get Numbering Header");
            loRtn = await loCls.GetNumberingHeaderDbAsync(loDbPar);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }

        loEx.ThrowExceptionIfErrors();
        _logger.LogInfo("End - Get Numbering Header");
        return loRtn;
    }
}
#endregion


public interface IGSM05000NumberingAsync : R_IServiceCRUDAsyncBase<GSM05000NumberingGridDTO>
{
    Task<GSM05000NumberingHeaderDTO> GetNumberingHeader(GSM05000TrxCodeParamsDTO poParams);
    IAsyncEnumerable<GSM05000NumberingGridDTO> GetNumberingListStream();
}