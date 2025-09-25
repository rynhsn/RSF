using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PMM00100BACK;
using PMM00100COMMON;
using PMM00100COMMON.DTO_s;
using PMM00100COMMON.DTO_s.Helper;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace PMM00100SERVICE
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PMM00102Controller : ControllerBase, IPMM00103
    {
        //var&const
        private LoggerPMM00100 _logger;
        private readonly ActivitySource _activitySource;
        public PMM00102Controller(ILogger<PMM00102Controller> logger)
        {
            //initiate
            LoggerPMM00100.R_InitializeLogger(logger);
            _logger = LoggerPMM00100.R_GetInstanceLogger();
            _activitySource = PMM00100Activity.R_InitializeAndGetActivitySource(GetType().Name);
        }

        //helpers
        private void ShowLogStart([CallerMemberName] string pcMethodCallerName = "") => _logger.LogInfo($"Starting {pcMethodCallerName} in {GetType().Name}");
        private void ShowLogExecute([CallerMemberName] string pcMethodCallerName = "") => _logger.LogInfo($"Executing cls method in {GetType().Name}.{pcMethodCallerName}");
        private void ShowLogEnd([CallerMemberName] string pcMethodCallerName = "") => _logger.LogInfo($"End {pcMethodCallerName} in {GetType().Name}");
        private void ShowLogError(Exception exception, [CallerMemberName] string pcMethodCallerName = "") => _logger.LogError(exception);

        //methods
        [HttpPost]
        public R_ServiceGetRecordResultDTO<SystemParamBillingDTO> R_ServiceGetRecord(R_ServiceGetRecordParameterDTO<SystemParamBillingDTO> poParameter)
        {
            using Activity activity = _activitySource.StartActivity($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");
            ShowLogStart();
            R_ServiceGetRecordResultDTO<SystemParamBillingDTO> loRtn = null;
            R_Exception loException = new R_Exception();
            try
            {
                var loCls = new PMM00103Cls(); //create cls class instance
                loRtn = new R_ServiceGetRecordResultDTO<SystemParamBillingDTO>();
                poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;
                ShowLogExecute();
                loRtn.data = loCls.R_GetRecord(poParameter.Entity);
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
        public R_ServiceSaveResultDTO<SystemParamBillingDTO> R_ServiceSave(R_ServiceSaveParameterDTO<SystemParamBillingDTO> poParameter)
        {
            using Activity activity = _activitySource.StartActivity($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");
            ShowLogStart();
            R_ServiceSaveResultDTO<SystemParamBillingDTO> loRtn = null;
            R_Exception loException = new R_Exception();
            try
            {
                var loCls = new PMM00103Cls();
                loRtn = new R_ServiceSaveResultDTO<SystemParamBillingDTO>();
                poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;
                ShowLogExecute();
                loRtn.data = loCls.R_Save(poParameter.Entity, poParameter.CRUDMode);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                ShowLogError(loException);
            }
            ShowLogEnd();
            loException.ThrowExceptionIfErrors();
            return loRtn;
        }
        [HttpPost]
        public R_ServiceDeleteResultDTO R_ServiceDelete(R_ServiceDeleteParameterDTO<SystemParamBillingDTO> poParameter)
        {
            throw new NotImplementedException();
        }
    }
}
