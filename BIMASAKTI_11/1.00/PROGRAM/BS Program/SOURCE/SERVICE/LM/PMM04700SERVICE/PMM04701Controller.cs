using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using R_Common;
using R_CommonFrontBackAPI;
using R_BackEnd;
using PMM04700Back;
using PMM04700BACK;
using PMM04700Common;
using PMM04700Common.DTOs;

namespace PMM04700Service
{
  
    
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class PMM04701Controller : ControllerBase, IPMM04701
    {
        private LoggerPMM04700 _logger;

        private readonly ActivitySource _activitySource;

        public PMM04701Controller(ILogger<LoggerPMM04700> logger)
        {
            //Initial and Get Logger
            LoggerPMM04700.R_InitializeLogger(logger);
            _logger = LoggerPMM04700.R_GetInstanceLogger();
            _activitySource = PMM04700Activity.R_InitializeAndGetActivitySource(GetType().Name);

        }
      [HttpPost]
        public IAsyncEnumerable<PricingRateDTO> GetPricingRateDateList()
        {
            using Activity activity = _activitySource.StartActivity($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");
            ShowLogStart();
            R_Exception loException = new R_Exception();
            List<PricingRateDTO> loRtnTemp = null;
            PMM04701Cls loCls;
            try
            {
                loCls = new PMM04701Cls();
                ShowLogExecute();
                loRtnTemp = loCls.GetPricingRateDateList(new PricingRateSaveParamDTO()
                {
                    CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID,
                    CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstant.CPROPERTY_ID),
                    CPRICE_TYPE = R_Utility.R_GetStreamingContext<string>(ContextConstant.CPRICE_TYPE),
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
            PMM04701Cls loCls;
            try
            {
                loCls = new PMM04701Cls();
                ShowLogExecute();
                loRtnTemp = loCls.GetPricingRateList(new PricingRateSaveParamDTO()
                {
                    CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID,
                    CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstant.CPROPERTY_ID),
                    CUNIT_TYPE_CATEGORY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstant.COTHER_UNIT_TYPE_ID),
                    CPRICE_TYPE = R_Utility.R_GetStreamingContext<string>(ContextConstant.CPRICE_TYPE),
                    CRATE_DATE= R_Utility.R_GetStreamingContext<string>(ContextConstant.CRATE_DATE),
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
            PMM04701Cls loCls;
            try
            {
                loCls = new PMM04701Cls();
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
        public PricingDumpResultDTO SavePricing(PricingSaveParamDTO poParam)
        {
            using Activity activity = _activitySource.StartActivity($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");
            ShowLogStart();
            PricingDumpResultDTO loRtn = new();
            R_Exception loException = new R_Exception();
            PMM04700Cls loCls;
            try
            {
                loCls = new PMM04700Cls();
                poParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParam.CUSER_ID = R_BackGlobalVar.USER_ID;
                ShowLogExecute();
                loCls.SavePricing(poParam);
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

        [HttpPost]
        public IAsyncEnumerable<TypeDTO> GetPriceChargesType()
        {
            using Activity activity = _activitySource.StartActivity($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");
            ShowLogStart();
            R_Exception loException = new R_Exception();
            List<TypeDTO> loRtnTemp = null;
            PMM04700Cls loCls;
            try
            {
                loCls = new PMM04700Cls();
                ShowLogExecute();
                loRtnTemp = loCls.GetChargesPriceTypeList(new TypeParam()
                {
                    CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID,
                    CLASS_APPLICATION = R_Utility.R_GetStreamingContext<string>(ContextConstant.CLASS_APPLICATION),
                    CLASS_ID = R_Utility.R_GetStreamingContext<string>(ContextConstant.CLASS_ID),
                    REC_ID_LIST = R_Utility.R_GetStreamingContext<string>(ContextConstant.REC_ID_LIST),
                    LANG_ID = R_BackGlobalVar.CULTURE,
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
        public PricingDumpResultDTO ActiveInactivePricing(PricingParamDTO poParam)
        {
            using Activity activity = _activitySource.StartActivity($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");
            ShowLogStart();
            R_Exception loException = new R_Exception();
            PricingDumpResultDTO loRtn = null;
            PMM04700Cls loCls;
            try
            {
                loRtn = new();
                loCls = new PMM04700Cls();
                ShowLogExecute();
                poParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParam.CUSER_ID = R_BackGlobalVar.USER_ID;
                loCls.ActiveInactivePricing(poParam);
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

        #region Stream List Data
        private async IAsyncEnumerable<T> StreamListData<T>(List<T> poParameter)
        {
            foreach (var item in poParameter)
            {
                yield return item;
            }
        }
        #endregion
        #region logger

        private void ShowLogStart([CallerMemberName] string pcMethodCallerName = "") => _logger.LogInfo($"Starting {pcMethodCallerName} in {GetType().Name}");

        private void ShowLogExecute([CallerMemberName] string pcMethodCallerName = "") => _logger.LogInfo($"Executing cls method in {GetType().Name}.{pcMethodCallerName}");

        private void ShowLogEnd([CallerMemberName] string pcMethodCallerName = "") => _logger.LogInfo($"End {pcMethodCallerName} in {GetType().Name}");

        private void ShowLogError(Exception exception, [CallerMemberName] string pcMethodCallerName = "") => _logger.LogError(exception);


        #endregion
    }
}

