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

#region "Not Async Version"
// [ApiController]
// [Route("api/[controller]/[action]")]
// public class GSM05000ApprovalReplacementController : ControllerBase, IGSM05000ApprovalReplacement
// {
//     private LoggerGSM05000 _logger;
//     private readonly ActivitySource _activitySource;
//     
//     public GSM05000ApprovalReplacementController(ILogger<GSM05000ApprovalReplacementController> logger)
//     {
//         //Initial and Get Logger
//         LoggerGSM05000.R_InitializeLogger(logger);
//         _logger = LoggerGSM05000.R_GetInstanceLogger();
//         _activitySource =GSM05000Activity.R_InitializeAndGetActivitySource(nameof(GSM05000ApprovalReplacementController));
//     }
//
//     [HttpPost]
//     public R_ServiceGetRecordResultDTO<GSM05000ApprovalReplacementDTO> R_ServiceGetRecord(R_ServiceGetRecordParameterDTO<GSM05000ApprovalReplacementDTO> poParameter)
//     {
//         using var loActivity = _activitySource.StartActivity(nameof(R_ServiceGetRecord));
//         _logger.LogInfo("Start - Get User Replacement Record");
//         R_Exception loEx = new();
//         R_ServiceGetRecordResultDTO<GSM05000ApprovalReplacementDTO> loRtn = new();
//
//         try
//         {
//             var loCls = new GSM05000ApprovalReplacementCls();
//             
//             _logger.LogInfo("Set Parameter");
//             poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
//             poParameter.Entity.CUSER_LOGIN_ID = R_BackGlobalVar.USER_ID;
//
//             _logger.LogInfo("Get User Replacement Record");
//             loRtn.data = loCls.R_GetRecord(poParameter.Entity);
//         }
//         catch (Exception ex)
//         {
//             loEx.Add(ex);
//             _logger.LogError(loEx);
//         }
//         
//         loEx.ThrowExceptionIfErrors();
//         _logger.LogInfo("End - Get User Replacement Record");
//         return loRtn;
//     }
//
//     [HttpPost]
//     public R_ServiceSaveResultDTO<GSM05000ApprovalReplacementDTO> R_ServiceSave(R_ServiceSaveParameterDTO<GSM05000ApprovalReplacementDTO> poParameter)
//     {
//         using var loActivity = _activitySource.StartActivity(nameof(R_ServiceSave));
//         _logger.LogInfo("Start - Save User Replacement Entity");
//         R_Exception loEx = new();
//         R_ServiceSaveResultDTO<GSM05000ApprovalReplacementDTO> loRtn = null;
//         GSM05000ApprovalReplacementCls loCls;
//
//         try
//         {
//             loCls = new GSM05000ApprovalReplacementCls();
//             loRtn = new R_ServiceSaveResultDTO<GSM05000ApprovalReplacementDTO>();
//             
//             _logger.LogInfo("Set Parameter");
//             poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
//             poParameter.Entity.CUSER_LOGIN_ID = R_BackGlobalVar.USER_ID;
//
//             _logger.LogInfo("Save User Replacement Entity");
//             loRtn.data = loCls.R_Save(poParameter.Entity, poParameter.CRUDMode);
//         }
//         catch (Exception ex)
//         {
//             loEx.Add(ex);
//             _logger.LogError(loEx);
//         }
//         
//         loEx.ThrowExceptionIfErrors();
//         _logger.LogInfo("End - Save User Replacement Entity");
//         return loRtn;
//     }
//
//     [HttpPost]
//     public R_ServiceDeleteResultDTO R_ServiceDelete(R_ServiceDeleteParameterDTO<GSM05000ApprovalReplacementDTO> poParameter)
//     {
//         using var loActivity = _activitySource.StartActivity(nameof(R_ServiceDelete));
//         _logger.LogInfo("Start - Delete User Replacement Entity");
//         R_Exception loEx = new();
//         R_ServiceDeleteResultDTO loRtn = new();
//         GSM05000ApprovalReplacementCls loCls;
//
//         try
//         {
//             loCls = new GSM05000ApprovalReplacementCls();
//             
//             _logger.LogInfo("Set Parameter");
//             poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
//             poParameter.Entity.CUSER_LOGIN_ID = R_BackGlobalVar.USER_ID;
//             
//             _logger.LogInfo("Delete User Replacement Entity");
//             loCls.R_Delete(poParameter.Entity);
//         }
//         catch (Exception ex)
//         {
//             loEx.Add(ex);
//             _logger.LogError(loEx);
//         }
//
//         loEx.ThrowExceptionIfErrors();
//         _logger.LogInfo("End - Delete User Replacement Entity");
//         return loRtn;
//     }
//
//     [HttpPost]
//     public IAsyncEnumerable<GSM05000ApprovalReplacementDTO> GSM05000GetApprovalReplacementListStream()
//     {
//         using var loActivity = _activitySource.StartActivity(nameof(GSM05000GetApprovalReplacementListStream));
//         _logger.LogInfo("Start - Get Approval Replacement List Stream");
//         R_Exception loEx = new();
//         IAsyncEnumerable<GSM05000ApprovalReplacementDTO> loRtn = null;
//         List<GSM05000ApprovalReplacementDTO> loResult;
//         GSM05000ParameterDb loDbPar;
//         GSM05000ApprovalReplacementCls loCls;
//
//         try
//         {
//             _logger.LogInfo("Set Parameter");
//             loDbPar = new GSM05000ParameterDb
//             {
//                 CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID,
//                 CTRANS_CODE = R_Utility.R_GetStreamingContext<string>(GSM05000ContextConstant.CTRANSACTION_CODE),
//                 CDEPT_CODE = R_Utility.R_GetStreamingContext<string>(GSM05000ContextConstant.CDEPT_CODE),
//                 CUSER_ID = R_Utility.R_GetStreamingContext<string>(GSM05000ContextConstant.CUSER_ID),
//                 CUSER_LOGIN_ID = R_BackGlobalVar.USER_ID,
//             };
//
//             loCls = new GSM05000ApprovalReplacementCls();
//             
//             _logger.LogInfo("Get Approval Replacement List Stream");
//             loResult = loCls.GSM05000GetApprovalReplacement(loDbPar);
//             loRtn = GetStream(loResult);
//         }
//         catch (Exception ex)
//         {
//             loEx.Add(ex);
//             _logger.LogError(loEx);
//         }
//
//         loEx.ThrowExceptionIfErrors();
//         _logger.LogInfo("End - Get Approval Replacement List Stream");
//         return loRtn;
//     }
//
//     #region "Helper ListStream Functions"
//
//     private async IAsyncEnumerable<T> GetStream<T>(List<T> poParameter)
//     {
//         foreach (T item in poParameter)
//         {
//             yield return item;
//         }
//     }
//     
//     #endregion
// }
#endregion

#region "Async Version"
[ApiController]
[Route("api/[controller]/[action]")]
public class GSM05000ApprovalReplacementController : ControllerBase, IGSM05000ApprovalReplacementAsync
{
    private LoggerGSM05000 _logger;
    private readonly ActivitySource _activitySource;
    
    public GSM05000ApprovalReplacementController(ILogger<GSM05000ApprovalReplacementController> logger)
    {
        //Initial and Get Logger
        LoggerGSM05000.R_InitializeLogger(logger);
        _logger = LoggerGSM05000.R_GetInstanceLogger();
        _activitySource =GSM05000Activity.R_InitializeAndGetActivitySource(nameof(GSM05000ApprovalReplacementController));
    }

    [HttpPost]
    public async Task<R_ServiceGetRecordResultDTO<GSM05000ApprovalReplacementDTO>> R_ServiceGetRecord(R_ServiceGetRecordParameterDTO<GSM05000ApprovalReplacementDTO> poParameter)
    {
        using var loActivity = _activitySource.StartActivity(nameof(R_ServiceGetRecord));
        _logger.LogInfo("Start - Get User Replacement Record");
        R_Exception loEx = new();
        R_ServiceGetRecordResultDTO<GSM05000ApprovalReplacementDTO> loRtn = new();

        try
        {
            var loCls = new GSM05000ApprovalReplacementCls();
            
            _logger.LogInfo("Set Parameter");
            poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
            poParameter.Entity.CUSER_LOGIN_ID = R_BackGlobalVar.USER_ID;

            _logger.LogInfo("Get User Replacement Record");
            loRtn.data = await loCls.R_GetRecordAsync(poParameter.Entity);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }
        
        loEx.ThrowExceptionIfErrors();
        _logger.LogInfo("End - Get User Replacement Record");
        return loRtn;
    }

    [HttpPost]
    public async Task<R_ServiceSaveResultDTO<GSM05000ApprovalReplacementDTO>> R_ServiceSave(R_ServiceSaveParameterDTO<GSM05000ApprovalReplacementDTO> poParameter)
    {
        using var loActivity = _activitySource.StartActivity(nameof(R_ServiceSave));
        _logger.LogInfo("Start - Save User Replacement Entity");
        R_Exception loEx = new();
        R_ServiceSaveResultDTO<GSM05000ApprovalReplacementDTO> loRtn = null;
        GSM05000ApprovalReplacementCls loCls;

        try
        {
            loCls = new GSM05000ApprovalReplacementCls();
            loRtn = new R_ServiceSaveResultDTO<GSM05000ApprovalReplacementDTO>();
            
            _logger.LogInfo("Set Parameter");
            poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
            poParameter.Entity.CUSER_LOGIN_ID = R_BackGlobalVar.USER_ID;

            _logger.LogInfo("Save User Replacement Entity");
            loRtn.data = await loCls.R_SaveAsync(poParameter.Entity, poParameter.CRUDMode);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }
        
        loEx.ThrowExceptionIfErrors();
        _logger.LogInfo("End - Save User Replacement Entity");
        return loRtn;
    }

    [HttpPost]
    public async Task<R_ServiceDeleteResultDTO> R_ServiceDelete(R_ServiceDeleteParameterDTO<GSM05000ApprovalReplacementDTO> poParameter)
    {
        using var loActivity = _activitySource.StartActivity(nameof(R_ServiceDelete));
        _logger.LogInfo("Start - Delete User Replacement Entity");
        R_Exception loEx = new();
        R_ServiceDeleteResultDTO loRtn = new();
        GSM05000ApprovalReplacementCls loCls;

        try
        {
            loCls = new GSM05000ApprovalReplacementCls();
            
            _logger.LogInfo("Set Parameter");
            poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
            poParameter.Entity.CUSER_LOGIN_ID = R_BackGlobalVar.USER_ID;
            
            _logger.LogInfo("Delete User Replacement Entity");
            await loCls.R_DeleteAsync(poParameter.Entity);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }

        loEx.ThrowExceptionIfErrors();
        _logger.LogInfo("End - Delete User Replacement Entity");
        return loRtn;
    }

    [HttpPost]
    public async IAsyncEnumerable<GSM05000ApprovalReplacementDTO> GSM05000GetApprovalReplacementListStream()
    {
        using var loActivity = _activitySource.StartActivity(nameof(GSM05000GetApprovalReplacementListStream));
        _logger.LogInfo("Start - Get Approval Replacement List Stream");
        R_Exception loEx = new();
        List<GSM05000ApprovalReplacementDTO> loResult = new();
        GSM05000ParameterDb loDbPar;
        GSM05000ApprovalReplacementCls loCls;

        try
        {
            _logger.LogInfo("Set Parameter");
            loDbPar = new GSM05000ParameterDb
            {
                CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID,
                CTRANS_CODE = R_Utility.R_GetStreamingContext<string>(GSM05000ContextConstant.CTRANSACTION_CODE),
                CDEPT_CODE = R_Utility.R_GetStreamingContext<string>(GSM05000ContextConstant.CDEPT_CODE),
                CUSER_ID = R_Utility.R_GetStreamingContext<string>(GSM05000ContextConstant.CUSER_ID),
                CUSER_LOGIN_ID = R_BackGlobalVar.USER_ID,
            };

            loCls = new GSM05000ApprovalReplacementCls();
            
            _logger.LogInfo("Get Approval Replacement List Stream");
            loResult = await loCls.GSM05000GetApprovalReplacementAsync(loDbPar);
        }
        catch (Exception ex)
        {
            loEx.Add(ex);
            _logger.LogError(loEx);
        }

        loEx.ThrowExceptionIfErrors();
        _logger.LogInfo("End - Get Approval Replacement List Stream");
        
        foreach (var loItem in loResult)
        {
            yield return loItem;
        }
    }

}
#endregion

public interface IGSM05000ApprovalReplacementAsync : R_IServiceCRUDAsyncBase<GSM05000ApprovalReplacementDTO>
{
    IAsyncEnumerable<GSM05000ApprovalReplacementDTO> GSM05000GetApprovalReplacementListStream();
}