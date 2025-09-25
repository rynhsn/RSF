using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PMM00100BACK;
using PMM00100COMMON;
using PMM00100COMMON.DTO_s;
using PMM00100COMMON.DTO_s.General;
using PMM00100COMMON.DTO_s.Helper;
using R_BackEnd;
using R_Common;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace PMM00100SERVICE
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PMM00100GeneralController : ControllerBase, IPMM00100
    {
        private LoggerPMM00100 _logger;

        private readonly ActivitySource _activitySource;

        public PMM00100GeneralController(ILogger<PMM00100GeneralController> logger)
        {
            //initiate
            LoggerPMM00100.R_InitializeLogger(logger);
            _logger = LoggerPMM00100.R_GetInstanceLogger();
            _activitySource = PMM00100Activity.R_InitializeAndGetActivitySource(GetType().Name);
        }

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

        //methods
        [HttpPost]
        public IAsyncEnumerable<PropertyDTO> GetPropertyList()
        {
            using Activity activity = _activitySource.StartActivity($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");
            ShowLogStart();
            R_Exception loException = new R_Exception();
            List<PropertyDTO> loRtnTemp = null;
            PMM00100 loCls;
            try
            {
                loCls = new PMM00100();
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
            PMM00100 loCls;
            try
            {
                loCls = new PMM00100();
                ShowLogExecute();
                loRtnTemp = loCls.GetPeriodDtList(new PeriodDtParamDTO()
                {
                    CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID,
                    CYEAR = R_Utility.R_GetStreamingContext<string>(PMM00100ContextConstant.CYEAR),
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
        public IAsyncEnumerable<GeneralTypeDTO> GetGSBCodeInfoList()
        {
            using Activity activity = _activitySource.StartActivity($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");
            ShowLogStart();
            R_Exception loException = new R_Exception();
            List<GeneralTypeDTO> loRtnTemp = null;
            PMM00100 loCls;
            try
            {
                loCls = new PMM00100();
                ShowLogExecute();
                loRtnTemp = loCls.GetGSBCodeInfoList(new GeneralParamDTO()
                {
                    CAPPLICATION = R_Utility.R_GetStreamingContext<string>(PMM00100ContextConstant.CAPPLICATION),
                    CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID,
                    CCLASS_ID = R_Utility.R_GetStreamingContext<string>(PMM00100ContextConstant.CCLASS_ID),
                    CLANG_ID = R_BackGlobalVar.CULTURE,
                    CREC_ID_LIST = R_Utility.R_GetStreamingContext<string>(PMM00100ContextConstant.CREC_ID_LIST)
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
                var loCls = new PMM00100();

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
    }
}
