using PMM04500BACK;
using PMM04500COMMON;
using PMM04500COMMON.DTO_s;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace PMM04500SERVICE
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PMM04501Controller : ControllerBase, IPMM04501
    {
        private LoggerPMM04500 _logger;

        private readonly ActivitySource _activitySource;

        public PMM04501Controller(ILogger<LoggerPMM04500> logger)
        {
            LoggerPMM04500.R_InitializeLogger(logger);
            _logger = LoggerPMM04500.R_GetInstanceLogger();
            _activitySource = PMM04500Activity.R_InitializeAndGetActivitySource(GetType().Name);
        }

        [HttpPost]
        public IAsyncEnumerable<PricingRateDTO> GetPricingRateDateList()
        {
            using Activity activity = _activitySource.StartActivity($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");
            ShowLogStart();
            R_Exception loException = new R_Exception();
            List<PricingRateDTO> loRtnTemp = null;
            PMM04501Cls loCls;
            try
            {
                loCls = new PMM04501Cls();
                ShowLogExecute();
                loRtnTemp = loCls.GetPricingRateDateList(new PricingRateSaveParamDTO()
                {
                    CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID,
                    CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstantPMM04500.CPROPERTY_ID),
                    CPRICE_TYPE = R_Utility.R_GetStreamingContext<string>(ContextConstantPMM04500.CPRICE_TYPE),
                    CUSER_ID = R_BackGlobalVar.USER_ID,
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
            return StreamListHelper(loRtnTemp);
        }

        [HttpPost]
        public IAsyncEnumerable<PricingRateDTO> GetPricingRateList()
        {
            using Activity activity = _activitySource.StartActivity($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");
            ShowLogStart();
            R_Exception loException = new R_Exception();
            List<PricingRateDTO> loRtnTemp = null;
            PMM04501Cls loCls;
            try
            {
                loCls = new PMM04501Cls();
                ShowLogExecute();
                loRtnTemp = loCls.GetPricingRateList(new PricingRateSaveParamDTO()
                {
                    CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID,
                    CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstantPMM04500.CPROPERTY_ID),
                    CUNIT_TYPE_CATEGORY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstantPMM04500.CUNIT_TYPE_CATEGORY_ID),
                    CPRICE_TYPE = R_Utility.R_GetStreamingContext<string>(ContextConstantPMM04500.CPRICE_TYPE),
                    CRATE_DATE= R_Utility.R_GetStreamingContext<string>(ContextConstantPMM04500.CRATE_DATE),
                    CUSER_ID = R_BackGlobalVar.USER_ID,
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
            return StreamListHelper(loRtnTemp);
        }

        [HttpPost]
        public PricingDumpResultDTO SavePricingRate(PricingRateSaveParamDTO poParam)
        {
            using Activity activity = _activitySource.StartActivity($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");
            ShowLogStart();
            PricingDumpResultDTO loRtn = new();
            R_Exception loException = new R_Exception();
            PMM04501Cls loCls;
            try
            {
                loCls = new PMM04501Cls();
                poParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParam.CUSER_ID = R_BackGlobalVar.USER_ID;
                ShowLogExecute();
                loCls.SavePricingRate(poParam);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                ShowLogError(loException);
            }
        EndBlock:
            loException.ThrowExceptionIfErrors();
            ShowLogEnd();
            return loRtn;
        }

        private async IAsyncEnumerable<T> StreamListHelper<T>(List<T> poList)
        {
            foreach (T loEntity in poList)
            {
                yield return loEntity;
            }
        }   

        [HttpPost]
        public R_ServiceDeleteResultDTO R_ServiceDelete(R_ServiceDeleteParameterDTO<PricingRateSaveParamDTO> poParameter)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public R_ServiceGetRecordResultDTO<PricingRateSaveParamDTO> R_ServiceGetRecord(R_ServiceGetRecordParameterDTO<PricingRateSaveParamDTO> poParameter)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public R_ServiceSaveResultDTO<PricingRateSaveParamDTO> R_ServiceSave(R_ServiceSaveParameterDTO<PricingRateSaveParamDTO> poParameter)
        {
            throw new NotImplementedException();
        }

        #region logger

        private void ShowLogStart([CallerMemberName] string pcMethodCallerName = "") => _logger.LogInfo($"Starting {pcMethodCallerName} in {GetType().Name}");

        private void ShowLogExecute([CallerMemberName] string pcMethodCallerName = "") => _logger.LogInfo($"Executing cls method in {GetType().Name}.{pcMethodCallerName}");

        private void ShowLogEnd([CallerMemberName] string pcMethodCallerName = "") => _logger.LogInfo($"End {pcMethodCallerName} in {GetType().Name}");

        private void ShowLogError(Exception exception, [CallerMemberName] string pcMethodCallerName = "") => _logger.LogError(exception);

        #endregion
    }
}
