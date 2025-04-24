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
    public class PMM04500Controller : ControllerBase, IPMM04500
    {
        private LoggerPMM04500 _logger;

        private readonly ActivitySource _activitySource;
        public PMM04500Controller(ILogger<PMM04500Controller> logger)
        {
            //initiate
            LoggerPMM04500.R_InitializeLogger(logger);
            _logger = LoggerPMM04500.R_GetInstanceLogger();
            _activitySource = PMM04500Activity.R_InitializeAndGetActivitySource(GetType().Name);
        }

        [HttpPost]
        public IAsyncEnumerable<PropertyDTO> GetPropertyList()
        {
            using Activity activity = _activitySource.StartActivity($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");
            ShowLogStart();
            R_Exception loException = new R_Exception();
            List<PropertyDTO> loRtnTemp = null;
            PMM04500InitCls loCls;
            try
            {
                loCls = new PMM04500InitCls();
                ShowLogExecute();
                loRtnTemp = loCls.GetPropertyList(new PropertyDTO()
                {
                    CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID,
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
        public IAsyncEnumerable<UnitTypeCategoryDTO> GetUnitTypeCategoryList()
        {
            using Activity activity = _activitySource.StartActivity($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");
            ShowLogStart();
            R_Exception loException = new R_Exception();
            List<UnitTypeCategoryDTO> loRtnTemp = null;
            PMM04500Cls loCls;
            try
            {
                loCls = new PMM04500Cls();
                ShowLogExecute();
                loRtnTemp = loCls.GetUnitTypeCategoryList(new UnitTypeCategoryParamDTO()
                {
                    CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID,
                    CUSER_ID = R_BackGlobalVar.USER_ID,
                    CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstantPMM04500.CPROPERTY_ID)
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
        public IAsyncEnumerable<PricingDTO> GetPricingDateList()
        {
            using Activity activity = _activitySource.StartActivity($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");
            ShowLogStart();
            R_Exception loException = new R_Exception();
            List<PricingDTO> loRtnTemp = null;
            PMM04500Cls loCls;
            try
            {
                loCls = new PMM04500Cls();
                ShowLogExecute();
                loRtnTemp = loCls.GetPricingDateList(new PricingParamDTO()
                {
                    CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID,
                    CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstantPMM04500.CPROPERTY_ID),
                    CUNIT_TYPE_CATEGORY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstantPMM04500.CUNIT_TYPE_CATEGORY_ID),
                    CPRICE_TYPE = R_Utility.R_GetStreamingContext<string>(ContextConstantPMM04500.CPRICE_TYPE),
                    CTYPE = R_Utility.R_GetStreamingContext<string>(ContextConstantPMM04500.CTYPE),
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
        public IAsyncEnumerable<PricingDTO> GetPricingList()
        {
            using Activity activity = _activitySource.StartActivity($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");
            ShowLogStart();
            R_Exception loException = new R_Exception();
            List<PricingDTO> loRtnTemp = null;
            PMM04500Cls loCls;
            try
            {
                loCls = new PMM04500Cls();
                ShowLogExecute();
                loRtnTemp = loCls.GetPricingList(new PricingParamDTO()
                {
                    CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID,
                    CUSER_ID = R_BackGlobalVar.USER_ID,
                    CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstantPMM04500.CPROPERTY_ID),
                    CUNIT_TYPE_CATEGORY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstantPMM04500.CUNIT_TYPE_CATEGORY_ID),
                    CPRICE_TYPE = R_Utility.R_GetStreamingContext<string>(ContextConstantPMM04500.CPRICE_TYPE),
                    LACTIVE = R_Utility.R_GetStreamingContext<bool>(ContextConstantPMM04500.LACTIVE),
                    CTYPE = R_Utility.R_GetStreamingContext<string>(ContextConstantPMM04500.CTYPE),
                    CVALID_DATE = R_Utility.R_GetStreamingContext<string>(ContextConstantPMM04500.CVALID_DATE),
                    CVALID_INTERNAL_ID = R_Utility.R_GetStreamingContext<string>(ContextConstantPMM04500.CVALID_ID),
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
            PMM04500Cls loCls;
            try
            {
                loCls = new PMM04500Cls();
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
        public IAsyncEnumerable<TypeDTO> GetRftGSBCodeInfoList()
        {
            using Activity activity = _activitySource.StartActivity($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");
            ShowLogStart();
            R_Exception loException = new R_Exception();
            List<TypeDTO> loRtnTemp = null;
            PMM04500InitCls loCls;
            try
            {
                loCls = new PMM04500InitCls();
                ShowLogExecute();
                loRtnTemp = loCls.GetRftGSBCodeInfoList(new TypeParam()
                {
                    CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID,
                    CLASS_APPLICATION = R_Utility.R_GetStreamingContext<string>(ContextConstantPMM04500.CLASS_APPLICATION),
                    CLASS_ID = R_Utility.R_GetStreamingContext<string>(ContextConstantPMM04500.CLASS_ID),
                    REC_ID_LIST = R_Utility.R_GetStreamingContext<string>(ContextConstantPMM04500.REC_ID_LIST),
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
            PMM04500Cls loCls;
            try
            {
                loRtn = new();
                loCls = new PMM04500Cls();
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


        #region (CRUD)NotImplementedException

        [HttpPost]
        public R_ServiceDeleteResultDTO R_ServiceDelete(R_ServiceDeleteParameterDTO<PricingSaveParamDTO> poParameter)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public R_ServiceGetRecordResultDTO<PricingSaveParamDTO> R_ServiceGetRecord(R_ServiceGetRecordParameterDTO<PricingSaveParamDTO> poParameter)
        {
            throw new NotImplementedException();

        }

        [HttpPost]
        public R_ServiceSaveResultDTO<PricingSaveParamDTO> R_ServiceSave(R_ServiceSaveParameterDTO<PricingSaveParamDTO> poParameter)
        {
            throw new NotImplementedException();

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
