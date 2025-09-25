using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PMR03100BACK;
using PMR03100BACK.OpenTelemetry;
using PMR03100COMMON.DTO_s.General;
using PMR03100COMMON.DTO_s.Helper;
using PMR03100COMMON.Interfaces;
using PMR03100COMMON.Log;
using R_BackEnd;
using R_Common;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace PMR03100SERVICE
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PMR03100Controller : ControllerBase, IPMR03100
    {
        private Logger _logger;

        private readonly ActivitySource _activitySource;

        public PMR03100Controller(ILogger<PMR03100Controller> logger)
        {
            Logger.R_InitializeLogger(logger);
            _logger = Logger.R_GetInstanceLogger();
            _activitySource = PMR03100Activity.R_InitializeAndGetActivitySource(GetType().Name);
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
                var loCls = new PMR03100Cls();

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

        [HttpPost]
        public IAsyncEnumerable<PropertyDTO> GetPropertyList()
        {
            using Activity activity = _activitySource.StartActivity($"{GetType().Name}.{MethodBase.GetCurrentMethod().Name}");
            ShowLogStart();
            R_Exception loException = new R_Exception();
            List<PropertyDTO> loRtnTemp = null;
            PMR03100Cls loCls;
            try
            {
                loCls = new PMR03100Cls();
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
