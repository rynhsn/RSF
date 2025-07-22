using GSM00300BACK;
using GSM00300COMMON;
using GSM00300COMMON.DTO_s;
using GSM00300COMMON.DTO_s.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;
using R_OpenTelemetry;

namespace GSM00300SERVICE
{
    #region "Non Async Version"
    // [Route("api/[controller]/[action]")]
    // [ApiController]
    // public class GSM00301Controller : ControllerBase, IGSM00301
    // {
    //     //variable & construcotr
    //     private LoggerGSM00300 _logger;
    //     private readonly ActivitySource _activitySource;
    //     public GSM00301Controller(ILogger<GSM00301Controller> logger)
    //     {
    //         //initiate
    //         LoggerGSM00300.R_InitializeLogger(logger);
    //         _logger = LoggerGSM00300.R_GetInstanceLogger();
    //         _activitySource = GSM00300Activity.R_InitializeAndGetActivitySource(nameof(GSM00301Controller));
    //     }
    //
    //     //method
    //     [HttpPost]
    //     public R_ServiceDeleteResultDTO R_ServiceDelete(R_ServiceDeleteParameterDTO<TaxInfoDTO> poParameter)
    //     {
    //         throw new NotImplementedException();
    //     }
    //     [HttpPost]
    //     public R_ServiceGetRecordResultDTO<TaxInfoDTO> R_ServiceGetRecord(R_ServiceGetRecordParameterDTO<TaxInfoDTO> poParameter)
    //     {
    //         using Activity activity = _activitySource.StartActivity($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");
    //         ShowLogStart();
    //         R_ServiceGetRecordResultDTO<TaxInfoDTO> loRtn = null;
    //         R_Exception loException = new R_Exception();
    //         GSM00301Cls loCls;
    //         try
    //         {
    //             loCls = new GSM00301Cls(); //create cls class instance
    //             loRtn = new R_ServiceGetRecordResultDTO<TaxInfoDTO>();
    //             poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
    //             poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;
    //             ShowLogExecute();
    //             loRtn.data = loCls.R_GetRecord(poParameter.Entity);
    //         }
    //         catch (Exception ex)
    //         {
    //             loException.Add(ex);
    //             ShowLogError(loException);
    //         }
    //         loException.ThrowExceptionIfErrors();
    //         ShowLogEnd();
    //         return loRtn;
    //     }
    //     [HttpPost]
    //     public R_ServiceSaveResultDTO<TaxInfoDTO> R_ServiceSave(R_ServiceSaveParameterDTO<TaxInfoDTO> poParameter)
    //     {
    //         using Activity activity = _activitySource.StartActivity($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");
    //         ShowLogStart();
    //         R_ServiceSaveResultDTO<TaxInfoDTO> loRtn = null;
    //         R_Exception loException = new R_Exception();
    //         GSM00301Cls loCls;
    //         try
    //         {
    //             loCls = new GSM00301Cls();
    //             loRtn = new R_ServiceSaveResultDTO<TaxInfoDTO>();
    //             poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
    //             poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;
    //             ShowLogExecute();
    //             loRtn.data = loCls.R_Save(poParameter.Entity, poParameter.CRUDMode);//call clsMethod to save
    //         }
    //         catch (Exception ex)
    //         {
    //             loException.Add(ex);
    //             ShowLogError(loException);
    //         }
    //         loException.ThrowExceptionIfErrors();
    //         ShowLogEnd();
    //         return loRtn;
    //     }
    //     [HttpPost]
    //     public IAsyncEnumerable<GSBCodeInfoDTO> GetRftGSBCodeInfoList()
    //     {
    //         using Activity activity = _activitySource.StartActivity($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");
    //         ShowLogStart();
    //         R_Exception loException = new R_Exception();
    //         List<GSBCodeInfoDTO> loRtnTemp = null;
    //         try
    //         {
    //             var loCls = new GSM00301Cls();
    //             ShowLogExecute();
    //             loRtnTemp = loCls.GetRftGSBCodeInfoList(new GSBCodeInfoParam()
    //             {
    //                 CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID,
    //                 CLASS_APPLICATION = R_Utility.R_GetStreamingContext<string>(GSM00300ContextConstant.CLASS_APPLICATION),
    //                 CLASS_ID = R_Utility.R_GetStreamingContext<string>(GSM00300ContextConstant.CLASS_ID),
    //                 REC_ID_LIST = R_Utility.R_GetStreamingContext<string>(GSM00300ContextConstant.REC_ID_LIST),
    //                 CLANG_ID = R_BackGlobalVar.CULTURE,
    //             });
    //         }
    //         catch (Exception ex)
    //         {
    //             loException.Add(ex);
    //             ShowLogError(loException);
    //         }
    //     EndBlock:
    //         loException.ThrowExceptionIfErrors();
    //         ShowLogEnd();
    //         return StreamListHelper(loRtnTemp);
    //     }
    //
    //     //helper method
    //     private async IAsyncEnumerable<T> StreamListHelper<T>(List<T> poList)
    //     {
    //         foreach (T loEntity in poList)
    //         {
    //             yield return loEntity;
    //         }
    //     }
    //     private void ShowLogStart([CallerMemberName] string pcMethodCallerName = "") => _logger.LogInfo($"Starting {pcMethodCallerName} in {GetType().Name}");
    //     private void ShowLogExecute([CallerMemberName] string pcMethodCallerName = "") => _logger.LogInfo($"Executing cls method in {GetType().Name}.{pcMethodCallerName}");
    //     private void ShowLogEnd([CallerMemberName] string pcMethodCallerName = "") => _logger.LogInfo($"End {pcMethodCallerName} in {GetType().Name}");
    //     private void ShowLogError(Exception exception, [CallerMemberName] string pcMethodCallerName = "") => _logger.LogError(exception);
    // }
    #endregion
    
    #region "Async Version"
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class GSM00301Controller : ControllerBase, IGSM00301Async
    {
        //variable & construcotr
        private LoggerGSM00300 _logger;
        private readonly ActivitySource _activitySource;
        public GSM00301Controller(ILogger<GSM00301Controller> logger)
        {
            //initiate
            LoggerGSM00300.R_InitializeLogger(logger);
            _logger = LoggerGSM00300.R_GetInstanceLogger();
            _activitySource = GSM00300Activity.R_InitializeAndGetActivitySource(nameof(GSM00301Controller));
        }

        //method
        [HttpPost]
        public async Task<R_ServiceDeleteResultDTO> R_ServiceDelete(R_ServiceDeleteParameterDTO<TaxInfoDTO> poParameter)
        {
            throw new NotImplementedException();
        }
        [HttpPost]
        public async Task<R_ServiceGetRecordResultDTO<TaxInfoDTO>> R_ServiceGetRecord(R_ServiceGetRecordParameterDTO<TaxInfoDTO> poParameter)
        {
            using Activity activity = _activitySource.StartActivity($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");
            ShowLogStart();
            R_ServiceGetRecordResultDTO<TaxInfoDTO> loRtn = null;
            R_Exception loException = new R_Exception();
            GSM00301Cls loCls;
            try
            {
                loCls = new GSM00301Cls(); //create cls class instance
                loRtn = new R_ServiceGetRecordResultDTO<TaxInfoDTO>();
                poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;
                ShowLogExecute();
                loRtn.data = await loCls.R_GetRecordAsync(poParameter.Entity);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                ShowLogError(loException);
            }
            loException.ThrowExceptionIfErrors();
            ShowLogEnd();
            return loRtn;
        }
        [HttpPost]
        public async Task<R_ServiceSaveResultDTO<TaxInfoDTO>> R_ServiceSave(R_ServiceSaveParameterDTO<TaxInfoDTO> poParameter)
        {
            using Activity activity = _activitySource.StartActivity($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");
            ShowLogStart();
            R_ServiceSaveResultDTO<TaxInfoDTO> loRtn = null;
            R_Exception loException = new R_Exception();
            GSM00301Cls loCls;
            try
            {
                loCls = new GSM00301Cls();
                loRtn = new R_ServiceSaveResultDTO<TaxInfoDTO>();
                poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;
                ShowLogExecute();
                loRtn.data = await loCls.R_SaveAsync(poParameter.Entity, poParameter.CRUDMode);//call clsMethod to save
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                ShowLogError(loException);
            }
            loException.ThrowExceptionIfErrors();
            ShowLogEnd();
            return loRtn;
        }
        [HttpPost]
        public async IAsyncEnumerable<GSBCodeInfoDTO> GetRftGSBCodeInfoList()
        {
            using Activity activity = _activitySource.StartActivity($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");
            ShowLogStart();
            R_Exception loException = new R_Exception();
            List<GSBCodeInfoDTO> loRtnTemp = null;
            try
            {
                var loCls = new GSM00301Cls();
                ShowLogExecute();
                loRtnTemp = await loCls.GetRftGSBCodeInfoListAsync(new GSBCodeInfoParam()
                {
                    CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID,
                    CLASS_APPLICATION = R_Utility.R_GetStreamingContext<string>(GSM00300ContextConstant.CLASS_APPLICATION),
                    CLASS_ID = R_Utility.R_GetStreamingContext<string>(GSM00300ContextConstant.CLASS_ID),
                    REC_ID_LIST = R_Utility.R_GetStreamingContext<string>(GSM00300ContextConstant.REC_ID_LIST),
                    CLANG_ID = R_BackGlobalVar.CULTURE,
                });
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                ShowLogError(loException);
            }
        EndBlock:
            loException.ThrowExceptionIfErrors();
            ShowLogEnd();
            
            foreach (var loItem in loRtnTemp)
            {
                yield return loItem;
            }
        }

        private void ShowLogStart([CallerMemberName] string pcMethodCallerName = "") => _logger.LogInfo($"Starting {pcMethodCallerName} in {GetType().Name}");
        private void ShowLogExecute([CallerMemberName] string pcMethodCallerName = "") => _logger.LogInfo($"Executing cls method in {GetType().Name}.{pcMethodCallerName}");
        private void ShowLogEnd([CallerMemberName] string pcMethodCallerName = "") => _logger.LogInfo($"End {pcMethodCallerName} in {GetType().Name}");
        private void ShowLogError(Exception exception, [CallerMemberName] string pcMethodCallerName = "") => _logger.LogError(exception);
    }
    #endregion
    
    
    public interface IGSM00301Async : R_IServiceCRUDAsyncBase<TaxInfoDTO>
    {
        IAsyncEnumerable<GSBCodeInfoDTO> GetRftGSBCodeInfoList();
    }
}
