using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using APR00100BACK;
using APR00100COMMON;
using APR00100COMMON.DTO_s;
using APR00100COMMON.DTO_s.Print;
using R_BackEnd;
using R_Common;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace APR00100SERVICE
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class APR00100Controller : ControllerBase, IAPR00100
    {
        private APR00100Logger _logger;

        private readonly ActivitySource _activitySource;

        //constructor
        public APR00100Controller(ILogger<APR00100Controller> logger)
        {
            //initiate
            APR00100Logger.R_InitializeLogger(logger);
            _logger = APR00100Logger.R_GetInstanceLogger();
            _activitySource = APR00100Activity.R_InitializeAndGetActivitySource(GetType().Name);
        }

        [HttpPost]
        public IAsyncEnumerable<PropertyDTO> GetPropertyList()
        {
            using Activity activity = _activitySource.StartActivity($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");
            ShowLogStart();
            R_Exception loException = new R_Exception();
            List<PropertyDTO> loRtnTemp = null;
            APR00100GeneralCls loCls;
            try
            {
                loCls = new APR00100GeneralCls();
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
            loException.ThrowExceptionIfErrors();
            ShowLogEnd();
            return StreamListHelper(loRtnTemp);
        }

        [HttpPost]
        public APR00100ResultDTO<TodayDTO> GetTodayDate()
        {
            using Activity activity = _activitySource.StartActivity($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");
            ShowLogStart();
            var loEx = new R_Exception();
            APR00100ResultDTO<TodayDTO> loRtn = new APR00100ResultDTO<TodayDTO>();

            try
            {
                var loCls = new APR00100GeneralCls();

                ShowLogExecute();
                loRtn.Data = loCls.GetTodayDateRecord(R_BackGlobalVar.COMPANY_ID);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                ShowLogError(loEx);

            }

            loEx.ThrowExceptionIfErrors();
            ShowLogEnd();

            return loRtn;
        }

        [HttpPost]
        public APR00100ResultDTO<PeriodYearDTO> GetPeriodYearRecord(PeriodYearDTO poParam)
        {
            using Activity activity = _activitySource.StartActivity($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");
            ShowLogStart();
            var loEx = new R_Exception();
            APR00100ResultDTO<PeriodYearDTO> loRtn = new APR00100ResultDTO<PeriodYearDTO>();

            try
            {
                var loCls = new APR00100GeneralCls();

                ShowLogExecute();
                poParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loRtn.Data = loCls.GetPeriodYearRecord(poParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                ShowLogError(loEx);

            }

            loEx.ThrowExceptionIfErrors();
            ShowLogEnd();

            return loRtn;
        }

        [HttpPost]
        public IAsyncEnumerable<CustomerTypeDTO> GetCustomerTypeList()
        {
            using Activity activity = _activitySource.StartActivity($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");
            ShowLogStart();
            R_Exception loException = new R_Exception();
            List<CustomerTypeDTO> loRtnTemp = null;
            APR00100GeneralCls loCls;
            try
            {
                loCls = new APR00100GeneralCls();
                ShowLogExecute();
                loRtnTemp = loCls.GetCustomerTypeList(new CustomerTypeParamDTO()
                {
                    CLASS_APPLICATION = APR00100ContextConstant.CLASS_APPLICATION,
                    COMPANY_ID = R_BackGlobalVar.COMPANY_ID,
                    CLASS_ID = APR00100ContextConstant.CLASS_ID,
                    REC_ID_LIST=APR00100ContextConstant.REC_ID_LIST,
                    LANG_ID = R_BackGlobalVar.CULTURE,
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

        public IAsyncEnumerable<TransTypeSuppCattDTO> GetTransTypeList()
        {
            using Activity activity = _activitySource.StartActivity($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");
            ShowLogStart();
            R_Exception loException = new R_Exception();
            List<TransTypeSuppCattDTO> loRtnTemp = null;
            APR00100GeneralCls loCls;
            try
            {
                loCls = new APR00100GeneralCls();
                ShowLogExecute();
                loRtnTemp = loCls.GetTransTypeList(new TransTypeSuppCattParamDTO()
                {
                     CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID,
                     CLANG_ID = R_BackGlobalVar.CULTURE
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

        public IAsyncEnumerable<TransTypeSuppCattDTO> GetSuppCattegoryList()
        {
            using Activity activity = _activitySource.StartActivity($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");
            ShowLogStart();
            R_Exception loException = new R_Exception();
            List<TransTypeSuppCattDTO> loRtnTemp = null;
            APR00100GeneralCls loCls;
            try
            {
                loCls = new APR00100GeneralCls();
                ShowLogExecute();
                loRtnTemp = loCls.GetSuppCattegoryList(new TransTypeSuppCattParamDTO()
                {
                    CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID,
                    CLANG_ID = R_BackGlobalVar.CULTURE
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

        //stream list helper
        private async IAsyncEnumerable<T> StreamListHelper<T>(List<T> poList)
        {
            foreach (T loEntity in poList)
            {
                yield return loEntity;
            }
        }

        #region logger

        private void ShowLogStart([CallerMemberName] string pcMethodCallerName = "") => _logger.LogInfo($"Starting {pcMethodCallerName} in {GetType().Name}");

        private void ShowLogExecute([CallerMemberName] string pcMethodCallerName = "") => _logger.LogInfo($"Executing cls method in {GetType().Name}.{pcMethodCallerName}");

        private void ShowLogEnd([CallerMemberName] string pcMethodCallerName = "") => _logger.LogInfo($"End {pcMethodCallerName} in {GetType().Name}");

        private void ShowLogError(Exception exception, [CallerMemberName] string pcMethodCallerName = "") => _logger.LogError(exception);



        #endregion

    }
}
