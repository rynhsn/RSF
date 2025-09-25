using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;
using System.Runtime.CompilerServices;
using System.Diagnostics;
using System.Reflection;
using PMM07500COMMON.Interfaces;
using PMM07500COMMON;
using PMM07500BACK;
using PMM07500COMMON.DTO_s.stamp_code;
using PMM07500COMMON.DTO_s;
using PMM07500COMMON.DTO_s.stamp_date;
using PMM07500COMMON.DTO_s.stamp_amount;

namespace PMM07500SERVICE
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PMM07520Controller : ControllerBase, IPMM07520
    {
        private LoggerPMM07500 _logger;

        private readonly ActivitySource _activitySource;

        public PMM07520Controller(ILogger<LoggerPMM07500> logger)
        {
            //initiate
            LoggerPMM07500.R_InitializeLogger(logger);
            _logger = LoggerPMM07500.R_GetInstanceLogger();
            _activitySource = PMM07500Activity.R_InitializeAndGetActivitySource(GetType().Name);
        }

        [HttpPost]
        public IAsyncEnumerable<PMM07520GridDTO> GetStampAmountList()
        {
            using Activity activity = _activitySource.StartActivity($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");
            ShowLogStart();
            R_Exception loException = new R_Exception();
            List<PMM07520GridDTO> loRtnTemp = null;
            PMM07520Cls loCls;
            try
            {
                loCls = new PMM07520Cls();
                ShowLogExecute();
                loRtnTemp = loCls.GetStampRateAmountList(new PMR07500ParamDTO()
                {
                    CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID,
                    CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(PMM07500ContextConstant.CPROPERTY_ID),
                    CPARENT_ID = R_Utility.R_GetStreamingContext<string>(PMM07500ContextConstant.CPARENT_ID),
                    CGRAND_PARENT_ID = R_Utility.R_GetStreamingContext<string>(PMM07500ContextConstant.CGRAND_PARENT_ID),
                    CLANGUAGE_ID = R_BackGlobalVar.CULTURE
                });
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                ShowLogError(loException);
            }
            loException.ThrowExceptionIfErrors();
            ShowLogEnd();
            return StreamListHelper(loRtnTemp);
        }

        [HttpPost]
        public R_ServiceDeleteResultDTO R_ServiceDelete(R_ServiceDeleteParameterDTO<PMM07520GridDTO> poParameter)
        {
            using Activity activity = _activitySource.StartActivity($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");
            ShowLogStart();
            R_ServiceDeleteResultDTO loRtn = null;
            R_Exception loException = new R_Exception();
            PMM07520Cls loCls;
            try
            {
                loRtn = new R_ServiceDeleteResultDTO();
                loCls = new PMM07520Cls(); //create cls class instance
                poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                ShowLogExecute();
                loCls.R_Delete(poParameter.Entity);
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
        public R_ServiceGetRecordResultDTO<PMM07520GridDTO> R_ServiceGetRecord(R_ServiceGetRecordParameterDTO<PMM07520GridDTO> poParameter)
        {
            using Activity activity = _activitySource.StartActivity($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");
            ShowLogStart();
            R_ServiceGetRecordResultDTO<PMM07520GridDTO> loRtn = null;
            R_Exception loException = new R_Exception();
            try
            {
                var loCls = new PMM07520Cls(); //create cls class instance
                poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;
                poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.Entity.CLANGUAGE_ID= R_BackGlobalVar.CULTURE;
                loRtn = new R_ServiceGetRecordResultDTO<PMM07520GridDTO>();
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
        public R_ServiceSaveResultDTO<PMM07520GridDTO> R_ServiceSave(R_ServiceSaveParameterDTO<PMM07520GridDTO> poParameter)
        {
            using Activity activity = _activitySource.StartActivity($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");
            ShowLogStart();
            R_ServiceSaveResultDTO<PMM07520GridDTO> loRtn = null;
            R_Exception loException = new R_Exception();
            PMM07520Cls loCls;
            try
            {
                loCls = new PMM07520Cls();
                loRtn = new R_ServiceSaveResultDTO<PMM07520GridDTO>();
                poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;
                poParameter.Entity.CLANGUAGE_ID = R_BackGlobalVar.CULTURE;
                ShowLogExecute();
                loRtn.data = loCls.R_Save(poParameter.Entity, poParameter.CRUDMode);
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


        #region helper

        private async IAsyncEnumerable<T> StreamListHelper<T>(List<T> poList)
        {
            foreach (T loEntity in poList)
            {
                yield return loEntity;
            }
        }

        private void ShowLogStart([CallerMemberName] string pcMethodCallerName = "") => _logger.LogInfo($"Starting {pcMethodCallerName} in {GetType().Name}");

        private void ShowLogExecute([CallerMemberName] string pcMethodCallerName = "") => _logger.LogInfo($"Executing cls method in {GetType().Name}.{pcMethodCallerName}");

        private void ShowLogEnd([CallerMemberName] string pcMethodCallerName = "") => _logger.LogInfo($"End {pcMethodCallerName} in {GetType().Name}");

        private void ShowLogError(Exception exception, [CallerMemberName] string pcMethodCallerName = "") => _logger.LogError(exception);
        #endregion
    }

}
