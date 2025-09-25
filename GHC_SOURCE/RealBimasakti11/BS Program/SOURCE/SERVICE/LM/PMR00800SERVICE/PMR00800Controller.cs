using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PMR00800BACK;
using PMR00800COMMON;
using PMR00800COMMON.DTO_s;
using PMR00800COMMON.DTO_s.General;
using PMR00800COMMON.DTO_s.Helper;
using R_BackEnd;
using R_Common;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace PMR00800SERVICE
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PMR00800Controller : ControllerBase, IPMR00800Init
    {
        private PMR00800Logger _logger;

        private readonly ActivitySource _activitySource;

        public PMR00800Controller(ILogger<PMR00801PrintController> logger)
        {
            //initiate
            PMR00800Logger.R_InitializeLogger(logger);
            _logger = PMR00800Logger.R_GetInstanceLogger();
            _activitySource = PMR00800Activity.R_InitializeAndGetActivitySource(GetType().Name);
        }

        [HttpPost]
        public IAsyncEnumerable<PropertyDTO> GetPropertyList()
        {
            using Activity activity = _activitySource.StartActivity($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");
            ShowLogStart();
            R_Exception loException = new R_Exception();
            List<PropertyDTO> loRtnTemp = null;
            PMR00800GeneralCls loCls;
            try
            {
                loCls = new PMR00800GeneralCls();
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
        public IAsyncEnumerable<PeriodDtDTO> GetPeriodDtList()
        {
            using Activity activity = _activitySource.StartActivity($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");
            ShowLogStart();
            R_Exception loException = new R_Exception();
            List<PeriodDtDTO> loRtnTemp = null;
            PMR00800GeneralCls loCls;
            try
            {
                loCls = new PMR00800GeneralCls();
                ShowLogExecute();
                loRtnTemp = loCls.GetPeriodDtList(new PeriodDtParamDTO()
                {
                    CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID,
                    CYEAR = R_Utility.R_GetStreamingContext<string>(PMR00800ContextConstant.CYEAR),
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
        public GeneralAPIResultDTO<PeriodYearRangeDTO> GetPeriodYearRangeRecord(PeriodYearRangeParamDTO poParam)
        {
            using Activity activity = _activitySource.StartActivity($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");
            ShowLogStart();
            var loEx = new R_Exception();
            GeneralAPIResultDTO<PeriodYearRangeDTO> loRtn = new GeneralAPIResultDTO<PeriodYearRangeDTO>();

            try
            {
                var loCls = new PMR00800GeneralCls();

                ShowLogExecute();
                poParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loRtn.data = loCls.GetPeriodYearRecord(poParam);
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

        //helper

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

    }
}
