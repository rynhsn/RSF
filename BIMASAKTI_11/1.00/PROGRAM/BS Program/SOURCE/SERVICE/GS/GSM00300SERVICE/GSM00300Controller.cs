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
    // public class GSM00300Controller : ControllerBase, IGSM00300
    // {
    //     private LoggerGSM00300 _logger;
    //
    //     private readonly ActivitySource _activitySource;
    //
    //     public GSM00300Controller(ILogger<GSM00300Controller> logger)
    //     {
    //         //initiate
    //         LoggerGSM00300.R_InitializeLogger(logger);
    //         _logger = LoggerGSM00300.R_GetInstanceLogger();
    //         _activitySource = GSM00300Activity.R_InitializeAndGetActivitySource(nameof(GSM00300Controller));
    //     }
    //
    //     [HttpPost]
    //     public GeneralAPIResultDTO<CheckPrimaryAccountDTO> CheckIsPrimaryAccount()
    //     {
    //         using Activity activity = _activitySource.StartActivity($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");
    //         ShowLogStart();
    //         R_Exception loException = new R_Exception();
    //         GeneralAPIResultDTO<CheckPrimaryAccountDTO> loRtn = null;
    //         GSM00300Cls loCls = null;
    //         try
    //         {
    //             loCls = new();
    //             loRtn = new();
    //             ShowLogExecute();
    //             loRtn.data = loCls.CheckIsPrimaryAccount(R_BackGlobalVar.COMPANY_ID);
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
    //
    //     [HttpPost]
    //     public R_ServiceDeleteResultDTO R_ServiceDelete(R_ServiceDeleteParameterDTO<CompanyParamRecordDTO> poParameter)
    //     {
    //         throw new NotImplementedException();
    //     }
    //
    //     [HttpPost]
    //     public R_ServiceGetRecordResultDTO<CompanyParamRecordDTO> R_ServiceGetRecord(R_ServiceGetRecordParameterDTO<CompanyParamRecordDTO> poParameter)
    //     {
    //         using Activity activity = _activitySource.StartActivity($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");
    //         ShowLogStart();
    //         R_ServiceGetRecordResultDTO<CompanyParamRecordDTO> loRtn = null;
    //         R_Exception loException = new R_Exception();
    //         GSM00300Cls loCls;
    //         try
    //         {
    //             loCls = new GSM00300Cls(); //create cls class instance
    //             loRtn = new R_ServiceGetRecordResultDTO<CompanyParamRecordDTO>();
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
    //
    //     [HttpPost]
    //     public R_ServiceSaveResultDTO<CompanyParamRecordDTO> R_ServiceSave(R_ServiceSaveParameterDTO<CompanyParamRecordDTO> poParameter)
    //     {
    //         using Activity activity = _activitySource.StartActivity($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");
    //         ShowLogStart();
    //         R_ServiceSaveResultDTO<CompanyParamRecordDTO> loRtn = null;
    //         R_Exception loException = new R_Exception();
    //         GSM00300Cls loCls;
    //         try
    //         {
    //             loCls = new GSM00300Cls();
    //             loRtn = new R_ServiceSaveResultDTO<CompanyParamRecordDTO>();
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
    //
    //     [HttpPost]
    //     
    //     public GeneralAPIResultDTO<ValidateCompanyDTO> CheckIsCompanyParamEditable()
    //     {
    //         using Activity activity = _activitySource.StartActivity($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");
    //         ShowLogStart();
    //         R_Exception loException = new R_Exception();
    //         GeneralAPIResultDTO<ValidateCompanyDTO> loRtn = null;
    //         GSM00300Cls loCls = null;
    //         try
    //         {
    //             loCls = new();
    //             ShowLogExecute();
    //             loRtn = new()
    //             {
    //                 data = loCls.ValidateCompanyParamEditable(R_BackGlobalVar.COMPANY_ID, R_BackGlobalVar.USER_ID)
    //             };
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
    //
    //     //helper method
    //     private void ShowLogStart([CallerMemberName] string pcMethodCallerName = "") => _logger.LogInfo($"Starting {pcMethodCallerName} in {GetType().Name}");
    //
    //     private void ShowLogExecute([CallerMemberName] string pcMethodCallerName = "") => _logger.LogInfo($"Executing cls method in {GetType().Name}.{pcMethodCallerName}");
    //
    //     private void ShowLogEnd([CallerMemberName] string pcMethodCallerName = "") => _logger.LogInfo($"End {pcMethodCallerName} in {GetType().Name}");
    //
    //     private void ShowLogError(Exception exception, [CallerMemberName] string pcMethodCallerName = "") => _logger.LogError(exception);
    //
    //     
    // }
    #endregion
    
    #region "Async Version"
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class GSM00300Controller : ControllerBase, IGSM00300Async
    {
        private LoggerGSM00300 _logger;

        private readonly ActivitySource _activitySource;

        public GSM00300Controller(ILogger<GSM00300Controller> logger)
        {
            //initiate
            LoggerGSM00300.R_InitializeLogger(logger);
            _logger = LoggerGSM00300.R_GetInstanceLogger();
            _activitySource = GSM00300Activity.R_InitializeAndGetActivitySource(nameof(GSM00300Controller));
        }

        [HttpPost]
        public async Task<GeneralAPIResultDTO<CheckPrimaryAccountDTO>> CheckIsPrimaryAccount()
        {
            using Activity activity = _activitySource.StartActivity($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");
            ShowLogStart();
            R_Exception loException = new R_Exception();
            GeneralAPIResultDTO<CheckPrimaryAccountDTO> loRtn = null;
            GSM00300Cls loCls = null;
            try
            {
                loCls = new();
                loRtn = new();
                ShowLogExecute();
                loRtn.data = await loCls.CheckIsPrimaryAccountAsync(R_BackGlobalVar.COMPANY_ID);
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
        public async Task<R_ServiceDeleteResultDTO> R_ServiceDelete(R_ServiceDeleteParameterDTO<CompanyParamRecordDTO> poParameter)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public async Task<R_ServiceGetRecordResultDTO<CompanyParamRecordDTO>> R_ServiceGetRecord(R_ServiceGetRecordParameterDTO<CompanyParamRecordDTO> poParameter)
        {
            using Activity activity = _activitySource.StartActivity($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");
            ShowLogStart();
            R_ServiceGetRecordResultDTO<CompanyParamRecordDTO> loRtn = null;
            R_Exception loException = new R_Exception();
            GSM00300Cls loCls;
            try
            {
                loCls = new GSM00300Cls(); //create cls class instance
                loRtn = new R_ServiceGetRecordResultDTO<CompanyParamRecordDTO>();
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
        public async Task<R_ServiceSaveResultDTO<CompanyParamRecordDTO>> R_ServiceSave(R_ServiceSaveParameterDTO<CompanyParamRecordDTO> poParameter)
        {
            using Activity activity = _activitySource.StartActivity($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");
            ShowLogStart();
            R_ServiceSaveResultDTO<CompanyParamRecordDTO> loRtn = null;
            R_Exception loException = new R_Exception();
            GSM00300Cls loCls;
            try
            {
                loCls = new GSM00300Cls();
                loRtn = new R_ServiceSaveResultDTO<CompanyParamRecordDTO>();
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
        
        public async Task<GeneralAPIResultDTO<ValidateCompanyDTO>> CheckIsCompanyParamEditable()
        {
            using Activity activity = _activitySource.StartActivity($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");
            ShowLogStart();
            R_Exception loException = new R_Exception();
            GeneralAPIResultDTO<ValidateCompanyDTO> loRtn = null;
            GSM00300Cls loCls = null;
            try
            {
                loCls = new();
                ShowLogExecute();
                loRtn = new()
                {
                    data = await loCls.ValidateCompanyParamEditableAsync(R_BackGlobalVar.COMPANY_ID, R_BackGlobalVar.USER_ID)
                };
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

        //helper method
        private void ShowLogStart([CallerMemberName] string pcMethodCallerName = "") => _logger.LogInfo($"Starting {pcMethodCallerName} in {GetType().Name}");

        private void ShowLogExecute([CallerMemberName] string pcMethodCallerName = "") => _logger.LogInfo($"Executing cls method in {GetType().Name}.{pcMethodCallerName}");

        private void ShowLogEnd([CallerMemberName] string pcMethodCallerName = "") => _logger.LogInfo($"End {pcMethodCallerName} in {GetType().Name}");

        private void ShowLogError(Exception exception, [CallerMemberName] string pcMethodCallerName = "") => _logger.LogError(exception);

        
    }
    #endregion
    
    
    public interface IGSM00300Async : R_IServiceCRUDAsyncBase<CompanyParamRecordDTO>
    {
        Task<GeneralAPIResultDTO<CheckPrimaryAccountDTO>> CheckIsPrimaryAccount();
        Task<GeneralAPIResultDTO<ValidateCompanyDTO>> CheckIsCompanyParamEditable();
    }
}
